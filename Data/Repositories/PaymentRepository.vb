Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET repository for bursar fee collection, receipting, transaction verification, and financial reconciliation.
    ''' </summary>
    Public Class PaymentRepository
        Public Function GetPayments(Optional allocationId As Integer? = Nothing,
                                    Optional semesterId As Integer? = Nothing,
                                    Optional method As String = Nothing,
                                    Optional search As String = Nothing) As List(Of Payment)
            Dim list As New List(Of Payment)()
            Dim sql As String =
                "SELECT p.payment_id, p.allocation_id, p.receipt_number, p.amount_paid, " &
                "       p.payment_method, p.transaction_reference, p.payment_date, " &
                "       p.recorded_by, p.payment_status, p.notes, p.created_at, p.updated_at, " &
                "       s.registration_number, " &
                "       LTRIM(RTRIM(CONCAT(s.first_name, ' ', ISNULL(s.middle_name, ''), ' ', s.last_name))) AS student_name, " &
                "       CONCAT(h.hostel_code, ' - Rm ', r.room_number, ' - Bed ', b.bed_number) AS bed_identifier, " &
                "       sem.semester_name, " &
                "       a.semester_fee, " &
                "       u.full_name AS recorded_by_name " &
                "FROM dbo.payments p " &
                "INNER JOIN dbo.bed_allocations a ON p.allocation_id = a.allocation_id " &
                "INNER JOIN dbo.students s ON a.student_id = s.student_id " &
                "INNER JOIN dbo.beds b ON a.bed_id = b.bed_id " &
                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "INNER JOIN dbo.semesters sem ON a.semester_id = sem.semester_id " &
                "LEFT JOIN dbo.users u ON p.recorded_by = u.user_id " &
                "WHERE 1=1 "

            Dim parameters As New List(Of SqlParameter)()

            If allocationId.HasValue AndAlso allocationId.Value > 0 Then
                sql &= " AND p.allocation_id = @allocId "
                parameters.Add(DatabaseConnection.CreateParam("@allocId", allocationId.Value, SqlDbType.Int))
            End If

            If semesterId.HasValue AndAlso semesterId.Value > 0 Then
                sql &= " AND a.semester_id = @semId "
                parameters.Add(DatabaseConnection.CreateParam("@semId", semesterId.Value, SqlDbType.Int))
            End If

            If Not String.IsNullOrEmpty(method) AndAlso method <> "ALL" Then
                sql &= " AND p.payment_method = @method "
                parameters.Add(DatabaseConnection.CreateParam("@method", method, SqlDbType.NVarChar))
            End If

            If Not String.IsNullOrEmpty(search) Then
                sql &= " AND (p.receipt_number LIKE @search OR p.transaction_reference LIKE @search OR s.registration_number LIKE @search OR s.first_name LIKE @search OR s.last_name LIKE @search) "
                parameters.Add(DatabaseConnection.CreateParam("@search", "%" & search.Trim() & "%", SqlDbType.NVarChar))
            End If

            sql &= " ORDER BY p.payment_date DESC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, parameters.ToArray())
            For Each row As DataRow In dt.Rows
                list.Add(MapPaymentFromRow(row))
            Next

            Return list
        End Function

        Public Function GetPaymentById(paymentId As Integer) As Payment
            Dim sql As String =
                "SELECT p.payment_id, p.allocation_id, p.receipt_number, p.amount_paid, " &
                "       p.payment_method, p.transaction_reference, p.payment_date, " &
                "       p.recorded_by, p.payment_status, p.notes, p.created_at, p.updated_at, " &
                "       s.registration_number, " &
                "       LTRIM(RTRIM(CONCAT(s.first_name, ' ', ISNULL(s.middle_name, ''), ' ', s.last_name))) AS student_name, " &
                "       CONCAT(h.hostel_code, ' - Rm ', r.room_number, ' - Bed ', b.bed_number) AS bed_identifier, " &
                "       sem.semester_name, " &
                "       a.semester_fee, " &
                "       u.full_name AS recorded_by_name " &
                "FROM dbo.payments p " &
                "INNER JOIN dbo.bed_allocations a ON p.allocation_id = a.allocation_id " &
                "INNER JOIN dbo.students s ON a.student_id = s.student_id " &
                "INNER JOIN dbo.beds b ON a.bed_id = b.bed_id " &
                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "INNER JOIN dbo.semesters sem ON a.semester_id = sem.semester_id " &
                "LEFT JOIN dbo.users u ON p.recorded_by = u.user_id " &
                "WHERE p.payment_id = @id;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@id", paymentId, SqlDbType.Int)})
            If dt.Rows.Count > 0 Then
                Return MapPaymentFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Generates an institutional receipt number in the format RCP-YYYY-XXXXX.
        ''' </summary>
        Public Function GenerateReceiptNumber() As String
            Dim year As Integer = DateTime.UtcNow.Year
            Dim prefix As String = $"RCP-{year}-"
            Dim sql As String = "SELECT COUNT(*) FROM dbo.payments WHERE receipt_number LIKE @prefix;"
            Dim count As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(sql, {DatabaseConnection.CreateParam("@prefix", prefix & "%", SqlDbType.NVarChar)}))
            Return $"{prefix}{(count + 1001).ToString("D5")}"
        End Function

        ''' <summary>
        ''' Atomically records a bursar student accommodation payment.
        ''' </summary>
        Public Function RecordPayment(allocationId As Integer,
                                      amount As Decimal,
                                      method As String,
                                      txRef As String,
                                      notes As String,
                                      userId As Integer,
                                      Optional receiptNo As String = Nothing) As (Success As Boolean, Message As String, PaymentId As Integer, ReceiptNumber As String)
            If amount <= 0 Then
                Return (False, "Payment amount must be greater than zero.", 0, String.Empty)
            End If

            If String.IsNullOrWhiteSpace(txRef) Then
                Return (False, "Transaction reference / bank reference is required.", 0, String.Empty)
            End If

            Dim finalReceiptNo As String = If(String.IsNullOrWhiteSpace(receiptNo), GenerateReceiptNumber(), receiptNo.Trim().ToUpperInvariant())

            Using conn = DatabaseConnection.GetOpenConnection()
                Using trans = conn.BeginTransaction()
                    Try
                        ' 1. Check duplicate receipt number
                        Dim checkRcptCmd As New SqlCommand("SELECT COUNT(*) FROM dbo.payments WHERE receipt_number = @rcpt;", conn, trans)
                        checkRcptCmd.Parameters.Add(DatabaseConnection.CreateParam("@rcpt", finalReceiptNo, SqlDbType.NVarChar))
                        Dim rcptExists = Convert.ToInt32(checkRcptCmd.ExecuteScalar())
                        If rcptExists > 0 Then
                            trans.Rollback()
                            Return (False, $"Receipt number '{finalReceiptNo}' already exists.", 0, String.Empty)
                        End If

                        ' 2. Check duplicate transaction reference
                        Dim checkTxCmd As New SqlCommand("SELECT COUNT(*) FROM dbo.payments WHERE transaction_reference = @txRef;", conn, trans)
                        checkTxCmd.Parameters.Add(DatabaseConnection.CreateParam("@txRef", txRef.Trim(), SqlDbType.NVarChar))
                        Dim txExists = Convert.ToInt32(checkTxCmd.ExecuteScalar())
                        If txExists > 0 Then
                            trans.Rollback()
                            Return (False, $"Transaction reference '{txRef}' has already been recorded in the system.", 0, String.Empty)
                        End If

                        ' 3. Validate Allocation & Outstanding Balance
                        Dim allocCmd As New SqlCommand(
                            "SELECT a.semester_fee, ISNULL(SUM(p.amount_paid), 0.00) AS total_paid " &
                            "FROM dbo.bed_allocations a " &
                            "LEFT JOIN dbo.payments p ON a.allocation_id = p.allocation_id AND p.payment_status = 'CONFIRMED' " &
                            "WHERE a.allocation_id = @aId " &
                            "GROUP BY a.semester_fee;", conn, trans)
                        allocCmd.Parameters.Add(DatabaseConnection.CreateParam("@aId", allocationId, SqlDbType.Int))

                        Dim semFee As Decimal = 0.0D
                        Dim alreadyPaid As Decimal = 0.0D
                        Using rdr = allocCmd.ExecuteReader()
                            If Not rdr.Read() Then
                                rdr.Close()
                                trans.Rollback()
                                Return (False, "Allocation record not found.", 0, String.Empty)
                            End If
                            semFee = Convert.ToDecimal(rdr("semester_fee"))
                            alreadyPaid = Convert.ToDecimal(rdr("total_paid"))
                        End Using

                        ' 4. Insert Payment
                        Dim insertSql As String =
                            "INSERT INTO dbo.payments (allocation_id, receipt_number, amount_paid, payment_method, transaction_reference, payment_date, recorded_by, payment_status, notes, created_at, updated_at) " &
                            "VALUES (@aId, @rcpt, @amt, @method, @txRef, SYSUTCDATETIME(), @userId, 'CONFIRMED', @notes, SYSUTCDATETIME(), SYSUTCDATETIME()); " &
                            "SELECT CAST(SCOPE_IDENTITY() AS INT);"

                        Dim insertCmd As New SqlCommand(insertSql, conn, trans)
                        insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@aId", allocationId, SqlDbType.Int))
                        insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@rcpt", finalReceiptNo, SqlDbType.NVarChar))
                        insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@amt", amount, SqlDbType.Decimal))
                        insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@method", method.ToUpperInvariant(), SqlDbType.NVarChar))
                        insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@txRef", txRef.Trim(), SqlDbType.NVarChar))
                        insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int))
                        insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@notes", If(String.IsNullOrEmpty(notes), DBNull.Value, notes), SqlDbType.NVarChar))

                        Dim paymentId = Convert.ToInt32(insertCmd.ExecuteScalar())

                        ' 5. Audit Log
                        Dim logSql As String =
                            "INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description, action_timestamp, ip_address) " &
                            "VALUES (@userId, 'RECORD_PAYMENT', 'PAYMENT', @payId, @desc, SYSUTCDATETIME(), '127.0.0.1');"
                        Dim logCmd As New SqlCommand(logSql, conn, trans)
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int))
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@payId", paymentId, SqlDbType.Int))
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@desc", $"Recorded payment of KES {amount:N2} via {method} (Receipt: {finalReceiptNo}, Ref: {txRef}) for Allocation ID {allocationId}.", SqlDbType.NVarChar))
                        logCmd.ExecuteNonQuery()

                        trans.Commit()
                        Return (True, "Payment recorded successfully.", paymentId, finalReceiptNo)
                    Catch ex As Exception
                        trans.Rollback()
                        Return (False, $"Failed to record payment: {ex.Message}", 0, String.Empty)
                    End Try
                End Using
            End Using
        End Function

        Public Function GetFinancialSummary(Optional semesterId As Integer? = Nothing) As (TotalBilled As Decimal, TotalPaid As Decimal, TotalArrears As Decimal, ConfirmedCount As Integer)
            Dim sql As String =
                "SELECT " &
                "    ISNULL(SUM(a.semester_fee), 0.00) AS total_billed, " &
                "    ISNULL(( " &
                "        SELECT SUM(p.amount_paid) " &
                "        FROM dbo.payments p " &
                "        INNER JOIN dbo.bed_allocations ba ON p.allocation_id = ba.allocation_id " &
                "        WHERE (@semId IS NULL OR ba.semester_id = @semId) AND p.payment_status = 'CONFIRMED' " &
                "    ), 0.00) AS total_paid, " &
                "    ISNULL(( " &
                "        SELECT COUNT(*) " &
                "        FROM dbo.payments p " &
                "        INNER JOIN dbo.bed_allocations ba ON p.allocation_id = ba.allocation_id " &
                "        WHERE (@semId IS NULL OR ba.semester_id = @semId) AND p.payment_status = 'CONFIRMED' " &
                "    ), 0) AS confirmed_count " &
                "FROM dbo.bed_allocations a " &
                "WHERE (@semId IS NULL OR a.semester_id = @semId) AND a.allocation_status IN ('ACTIVE', 'CHECKED_OUT');"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@semId", If(semesterId.HasValue AndAlso semesterId.Value > 0, semesterId.Value, DBNull.Value), SqlDbType.Int)
            }

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, parameters)
            If dt.Rows.Count > 0 Then
                Dim row = dt.Rows(0)
                Dim billed = Convert.ToDecimal(row("total_billed"))
                Dim paid = Convert.ToDecimal(row("total_paid"))
                Dim count = Convert.ToInt32(row("confirmed_count"))
                Dim arrears = Math.Max(0.0D, billed - paid)
                Return (billed, paid, arrears, count)
            End If

            Return (0.0D, 0.0D, 0.0D, 0)
        End Function

        Private Function MapPaymentFromRow(row As DataRow) As Payment
            Return New Payment With {
                .PaymentId = Convert.ToInt32(row("payment_id")),
                .AllocationId = Convert.ToInt32(row("allocation_id")),
                .ReceiptNumber = row("receipt_number").ToString(),
                .AmountPaid = Convert.ToDecimal(row("amount_paid")),
                .PaymentMethod = row("payment_method").ToString(),
                .TransactionReference = row("transaction_reference").ToString(),
                .PaymentDate = Convert.ToDateTime(row("payment_date")),
                .RecordedBy = Convert.ToInt32(row("recorded_by")),
                .PaymentStatus = row("payment_status").ToString(),
                .Notes = If(IsDBNull(row("notes")), String.Empty, row("notes").ToString()),
                .CreatedAt = Convert.ToDateTime(row("created_at")),
                .UpdatedAt = Convert.ToDateTime(row("updated_at")),
                .RegistrationNumber = row("registration_number").ToString(),
                .StudentName = row("student_name").ToString(),
                .BedIdentifier = row("bed_identifier").ToString(),
                .SemesterName = row("semester_name").ToString(),
                .SemesterFee = Convert.ToDecimal(row("semester_fee")),
                .RecordedByName = If(IsDBNull(row("recorded_by_name")), "System", row("recorded_by_name").ToString())
            }
        End Function
    End Class
End Namespace
