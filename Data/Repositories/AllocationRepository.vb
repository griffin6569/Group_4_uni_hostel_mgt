Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET repository for managing semester bed allocations, check-ins, check-outs, and transactional transfers.
    ''' Crucial Rule: semester_fee MUST snapshot the bed's semester_rate at allocation time.
    ''' </summary>
    Public Class AllocationRepository
        Public Function GetAllocations(Optional semesterId As Integer? = Nothing,
                                       Optional hostelId As Integer? = Nothing,
                                       Optional status As String = Nothing,
                                       Optional search As String = Nothing) As List(Of BedAllocation)
            Dim list As New List(Of BedAllocation)()
            Dim sql As String =
                "SELECT a.allocation_id, a.student_id, a.bed_id, a.semester_id, a.semester_fee, " &
                "       a.allocation_date, a.check_in_date, a.check_out_date, a.allocation_status, " &
                "       a.notes, a.created_by, a.created_at, a.updated_at, " &
                "       s.registration_number, " &
                "       LTRIM(RTRIM(CONCAT(s.first_name, ' ', ISNULL(s.middle_name, ''), ' ', s.last_name))) AS student_name, " &
                "       s.course, s.department, s.phone_number, " &
                "       b.bed_number, b.bed_label, b.bed_type, " &
                "       r.room_number, r.floor_number, " &
                "       h.hostel_id, h.hostel_name, h.hostel_code, " &
                "       sem.semester_name, sem.academic_year, " &
                "       ISNULL(p.total_paid, 0.00) AS total_paid, " &
                "       (a.semester_fee - ISNULL(p.total_paid, 0.00)) AS balance_owed " &
                "FROM dbo.bed_allocations a " &
                "INNER JOIN dbo.students s ON a.student_id = s.student_id " &
                "INNER JOIN dbo.beds b ON a.bed_id = b.bed_id " &
                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "INNER JOIN dbo.semesters sem ON a.semester_id = sem.semester_id " &
                "LEFT JOIN ( " &
                "    SELECT allocation_id, SUM(amount_paid) AS total_paid " &
                "    FROM dbo.payments " &
                "    WHERE payment_status = 'CONFIRMED' " &
                "    GROUP BY allocation_id " &
                ") p ON a.allocation_id = p.allocation_id " &
                "WHERE 1=1 "

            Dim parameters As New List(Of SqlParameter)()

            If semesterId.HasValue AndAlso semesterId.Value > 0 Then
                sql &= " AND a.semester_id = @semId "
                parameters.Add(DatabaseConnection.CreateParam("@semId", semesterId.Value, SqlDbType.Int))
            End If

            If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                sql &= " AND h.hostel_id = @hostelId "
                parameters.Add(DatabaseConnection.CreateParam("@hostelId", hostelId.Value, SqlDbType.Int))
            End If

            If Not String.IsNullOrEmpty(status) AndAlso status <> "ALL" Then
                sql &= " AND a.allocation_status = @status "
                parameters.Add(DatabaseConnection.CreateParam("@status", status, SqlDbType.NVarChar))
            End If

            If Not String.IsNullOrEmpty(search) Then
                sql &= " AND (s.registration_number LIKE @search OR s.first_name LIKE @search OR s.last_name LIKE @search OR b.bed_number LIKE @search OR r.room_number LIKE @search OR h.hostel_name LIKE @search) "
                parameters.Add(DatabaseConnection.CreateParam("@search", "%" & search.Trim() & "%", SqlDbType.NVarChar))
            End If

            sql &= " ORDER BY a.allocation_date DESC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, parameters.ToArray())
            For Each row As DataRow In dt.Rows
                list.Add(MapAllocationFromRow(row))
            Next

            Return list
        End Function

        Public Function GetAllocationById(allocationId As Integer) As BedAllocation
            Dim sql As String =
                "SELECT a.allocation_id, a.student_id, a.bed_id, a.semester_id, a.semester_fee, " &
                "       a.allocation_date, a.check_in_date, a.check_out_date, a.allocation_status, " &
                "       a.notes, a.created_by, a.created_at, a.updated_at, " &
                "       s.registration_number, " &
                "       LTRIM(RTRIM(CONCAT(s.first_name, ' ', ISNULL(s.middle_name, ''), ' ', s.last_name))) AS student_name, " &
                "       s.course, s.department, s.phone_number, " &
                "       b.bed_number, b.bed_label, b.bed_type, " &
                "       r.room_number, r.floor_number, " &
                "       h.hostel_id, h.hostel_name, h.hostel_code, " &
                "       sem.semester_name, sem.academic_year, " &
                "       ISNULL(p.total_paid, 0.00) AS total_paid, " &
                "       (a.semester_fee - ISNULL(p.total_paid, 0.00)) AS balance_owed " &
                "FROM dbo.bed_allocations a " &
                "INNER JOIN dbo.students s ON a.student_id = s.student_id " &
                "INNER JOIN dbo.beds b ON a.bed_id = b.bed_id " &
                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "INNER JOIN dbo.semesters sem ON a.semester_id = sem.semester_id " &
                "LEFT JOIN ( " &
                "    SELECT allocation_id, SUM(amount_paid) AS total_paid " &
                "    FROM dbo.payments " &
                "    WHERE payment_status = 'CONFIRMED' " &
                "    GROUP BY allocation_id " &
                ") p ON a.allocation_id = p.allocation_id " &
                "WHERE a.allocation_id = @id;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@id", allocationId, SqlDbType.Int)})
            If dt.Rows.Count > 0 Then
                Return MapAllocationFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Creates an individual bed allocation with rate snapshotting and transactional bed occupation.
        ''' </summary>
        Public Function CreateAllocation(studentId As Integer,
                                         bedId As Integer,
                                         semesterId As Integer,
                                         userId As Integer,
                                         notes As String) As (Success As Boolean, Message As String, AllocationId As Integer)
            Using conn = DatabaseConnection.GetOpenConnection()
                Using trans = conn.BeginTransaction()
                    Try
                        ' 1. Validate Semester
                        Dim semCmd As New SqlCommand("SELECT status FROM dbo.semesters WHERE semester_id = @semId;", conn, trans)
                        semCmd.Parameters.Add(DatabaseConnection.CreateParam("@semId", semesterId, SqlDbType.Int))
                        Dim semStatus = Convert.ToString(semCmd.ExecuteScalar())
                        If String.IsNullOrEmpty(semStatus) Then
                            trans.Rollback()
                            Return (False, "Target academic semester does not exist.", 0)
                        End If
                        If semStatus <> "ACTIVE" AndAlso semStatus <> "UPCOMING" Then
                            trans.Rollback()
                            Return (False, $"Cannot allocate bed: Semester is marked as '{semStatus}'.", 0)
                        End If

                        ' 2. Prevent Multiple Active Allocations for the Same Student in the Semester
                        Dim dupStudCmd As New SqlCommand("SELECT COUNT(*) FROM dbo.bed_allocations WHERE student_id = @sId AND semester_id = @semId AND allocation_status = 'ACTIVE';", conn, trans)
                        dupStudCmd.Parameters.Add(DatabaseConnection.CreateParam("@sId", studentId, SqlDbType.Int))
                        dupStudCmd.Parameters.Add(DatabaseConnection.CreateParam("@semId", semesterId, SqlDbType.Int))
                        Dim studAllocCount = Convert.ToInt32(dupStudCmd.ExecuteScalar())
                        If studAllocCount > 0 Then
                            trans.Rollback()
                            Return (False, "This student already has an active bed allocation in this semester.", 0)
                        End If

                        ' 3. Prevent Allocating a Bed that is Not Available
                        Dim bedCmd As New SqlCommand("SELECT status, semester_rate FROM dbo.beds WHERE bed_id = @bId;", conn, trans)
                        bedCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", bedId, SqlDbType.Int))
                        Using rdr = bedCmd.ExecuteReader()
                            If Not rdr.Read() Then
                                rdr.Close()
                                trans.Rollback()
                                Return (False, "Selected bed space does not exist.", 0)
                            End If

                            Dim bedStatus = rdr("status").ToString()
                            Dim currentRate = Convert.ToDecimal(rdr("semester_rate"))
                            rdr.Close()

                            If bedStatus <> "AVAILABLE" Then
                                trans.Rollback()
                                Return (False, $"Cannot allocate bed: Bed status is currently '{bedStatus}'. Only AVAILABLE beds can be allocated.", 0)
                            End If

                            ' 4. Insert Allocation with Snapshotted Rate
                            Dim insertSql As String =
                                "INSERT INTO dbo.bed_allocations (student_id, bed_id, semester_id, semester_fee, allocation_date, allocation_status, notes, created_by, created_at, updated_at) " &
                                "VALUES (@sId, @bId, @semId, @fee, SYSUTCDATETIME(), 'ACTIVE', @notes, @userId, SYSUTCDATETIME(), SYSUTCDATETIME()); " &
                                "SELECT CAST(SCOPE_IDENTITY() AS INT);"

                            Dim insertCmd As New SqlCommand(insertSql, conn, trans)
                            insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@sId", studentId, SqlDbType.Int))
                            insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", bedId, SqlDbType.Int))
                            insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@semId", semesterId, SqlDbType.Int))
                            insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@fee", currentRate, SqlDbType.Decimal))
                            insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@notes", If(String.IsNullOrEmpty(notes), DBNull.Value, notes), SqlDbType.NVarChar))
                            insertCmd.Parameters.Add(DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int))

                            Dim newAllocId = Convert.ToInt32(insertCmd.ExecuteScalar())

                            ' 5. Mark Bed as OCCUPIED
                            Dim updateBedCmd As New SqlCommand("UPDATE dbo.beds SET status = 'OCCUPIED', updated_at = SYSUTCDATETIME() WHERE bed_id = @bId;", conn, trans)
                            updateBedCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", bedId, SqlDbType.Int))
                            updateBedCmd.ExecuteNonQuery()

                            ' 6. Write Audit Activity Log
                            Dim logSql As String =
                                "INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description, action_timestamp, ip_address) " &
                                "VALUES (@userId, 'ALLOCATE_BED', 'BED_ALLOCATION', @allocId, @desc, SYSUTCDATETIME(), '127.0.0.1');"
                            Dim logCmd As New SqlCommand(logSql, conn, trans)
                            logCmd.Parameters.Add(DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int))
                            logCmd.Parameters.Add(DatabaseConnection.CreateParam("@allocId", newAllocId, SqlDbType.Int))
                            logCmd.Parameters.Add(DatabaseConnection.CreateParam("@desc", $"Allocated Bed ID {bedId} to Student ID {studentId} for Semester ID {semesterId} at snapshotted rate KES {currentRate:N2}.", SqlDbType.NVarChar))
                            logCmd.ExecuteNonQuery()

                            trans.Commit()
                            Return (True, "Bed allocated successfully.", newAllocId)
                        End Using
                    Catch ex As Exception
                        trans.Rollback()
                        Return (False, $"Allocation failed: {ex.Message}", 0)
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Records student check-in with keys and condition notes.
        ''' </summary>
        Public Function CheckInResident(allocationId As Integer, checkInDate As DateTime, notes As String, userId As Integer) As (Success As Boolean, Message As String)
            Dim sql As String =
                "UPDATE dbo.bed_allocations " &
                "SET check_in_date = @checkIn, " &
                "    notes = CASE WHEN notes IS NULL OR notes = '' THEN @notes ELSE CONCAT(notes, CHAR(13), CHAR(10), @notes) END, " &
                "    updated_at = SYSUTCDATETIME() " &
                "WHERE allocation_id = @id AND allocation_status = 'ACTIVE';"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@id", allocationId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@checkIn", checkInDate, SqlDbType.DateTime2),
                DatabaseConnection.CreateParam("@notes", $"Check-in on {checkInDate:yyyy-MM-dd}: {notes.Trim()}", SqlDbType.NVarChar)
            }

            Dim rows = DatabaseConnection.ExecuteNonQuery(sql, parameters)
            If rows > 0 Then
                ' Log Activity
                AuditLogger(userId, "CHECK_IN", "BED_ALLOCATION", allocationId, $"Checked in resident for Allocation ID {allocationId} on {checkInDate:yyyy-MM-dd}.")
                Return (True, "Resident successfully checked in.")
            Else
                Return (False, "Allocation record not found or not in active status.")
            End If
        End Function

        ''' <summary>
        ''' Processes student check-out, releasing the bed back to AVAILABLE (or MAINTENANCE if damages reported).
        ''' </summary>
        Public Function CheckOutResident(allocationId As Integer,
                                         checkOutDate As DateTime,
                                         flagBedForMaintenance As Boolean,
                                         maintenanceReason As String,
                                         userId As Integer) As (Success As Boolean, Message As String)
            Using conn = DatabaseConnection.GetOpenConnection()
                Using trans = conn.BeginTransaction()
                    Try
                        ' 1. Fetch Allocation and Bed ID
                        Dim fetchCmd As New SqlCommand("SELECT bed_id, allocation_status FROM dbo.bed_allocations WHERE allocation_id = @id;", conn, trans)
                        fetchCmd.Parameters.Add(DatabaseConnection.CreateParam("@id", allocationId, SqlDbType.Int))
                        Dim bedId As Integer = 0
                        Dim currentStatus As String = String.Empty
                        Using rdr = fetchCmd.ExecuteReader()
                            If Not rdr.Read() Then
                                rdr.Close()
                                trans.Rollback()
                                Return (False, "Allocation record could not be found.")
                            End If
                            bedId = Convert.ToInt32(rdr("bed_id"))
                            currentStatus = rdr("allocation_status").ToString()
                        End Using

                        If currentStatus <> "ACTIVE" Then
                            trans.Rollback()
                            Return (False, $"Cannot checkout: Allocation status is '{currentStatus}'.")
                        End If

                        ' 2. Mark Allocation as CHECKED_OUT
                        Dim checkoutSql As String =
                            "UPDATE dbo.bed_allocations " &
                            "SET allocation_status = 'CHECKED_OUT', " &
                            "    check_out_date = @outDate, " &
                            "    updated_at = SYSUTCDATETIME() " &
                            "WHERE allocation_id = @id;"
                        Dim checkoutCmd As New SqlCommand(checkoutSql, conn, trans)
                        checkoutCmd.Parameters.Add(DatabaseConnection.CreateParam("@id", allocationId, SqlDbType.Int))
                        checkoutCmd.Parameters.Add(DatabaseConnection.CreateParam("@outDate", checkOutDate, SqlDbType.DateTime2))
                        checkoutCmd.ExecuteNonQuery()

                        ' 3. Update Bed Status
                        Dim newBedStatus As String = If(flagBedForMaintenance, "MAINTENANCE", "AVAILABLE")
                        Dim bedUpdateCmd As New SqlCommand("UPDATE dbo.beds SET status = @status, updated_at = SYSUTCDATETIME() WHERE bed_id = @bId;", conn, trans)
                        bedUpdateCmd.Parameters.Add(DatabaseConnection.CreateParam("@status", newBedStatus, SqlDbType.NVarChar))
                        bedUpdateCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", bedId, SqlDbType.Int))
                        bedUpdateCmd.ExecuteNonQuery()

                        ' 4. Optional Maintenance Record
                        If flagBedForMaintenance Then
                            Dim maintSql As String =
                                "INSERT INTO dbo.maintenance_records (bed_id, issue_title, issue_description, maintenance_status, reported_by, reported_date) " &
                                "VALUES (@bId, 'Bed Clearance Damage Assessment', @desc, 'REPORTED', @userId, SYSUTCDATETIME());"
                            Dim maintCmd As New SqlCommand(maintSql, conn, trans)
                            maintCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", bedId, SqlDbType.Int))
                            maintCmd.Parameters.Add(DatabaseConnection.CreateParam("@desc", maintenanceReason.Trim(), SqlDbType.NVarChar))
                            maintCmd.Parameters.Add(DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int))
                            maintCmd.ExecuteNonQuery()
                        End If

                        ' 5. Audit Log
                        Dim logSql As String =
                            "INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description, action_timestamp, ip_address) " &
                            "VALUES (@userId, 'CHECK_OUT', 'BED_ALLOCATION', @allocId, @desc, SYSUTCDATETIME(), '127.0.0.1');"
                        Dim logCmd As New SqlCommand(logSql, conn, trans)
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int))
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@allocId", allocationId, SqlDbType.Int))
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@desc", $"Checked out resident from Allocation ID {allocationId}. Bed ID {bedId} set to {newBedStatus}.", SqlDbType.NVarChar))
                        logCmd.ExecuteNonQuery()

                        trans.Commit()
                        Return (True, $"Resident checked out successfully. Bed released to {newBedStatus}.")
                    Catch ex As Exception
                        trans.Rollback()
                        Return (False, $"Check-out failed: {ex.Message}")
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Executes a transactional bed transfer within the same academic semester.
        ''' </summary>
        Public Function TransferBed(allocationId As Integer,
                                    newBedId As Integer,
                                    reason As String,
                                    userId As Integer) As (Success As Boolean, Message As String, NewAllocationId As Integer)
            Using conn = DatabaseConnection.GetOpenConnection()
                Using trans = conn.BeginTransaction()
                    Try
                        ' 1. Fetch Old Allocation
                        Dim fetchSql As String = "SELECT student_id, bed_id, semester_id, allocation_status FROM dbo.bed_allocations WHERE allocation_id = @id;"
                        Dim fetchCmd As New SqlCommand(fetchSql, conn, trans)
                        fetchCmd.Parameters.Add(DatabaseConnection.CreateParam("@id", allocationId, SqlDbType.Int))

                        Dim studentId As Integer = 0
                        Dim oldBedId As Integer = 0
                        Dim semesterId As Integer = 0
                        Dim oldStatus As String = String.Empty

                        Using rdr = fetchCmd.ExecuteReader()
                            If Not rdr.Read() Then
                                rdr.Close()
                                trans.Rollback()
                                Return (False, "Original allocation record not found.", 0)
                            End If
                            studentId = Convert.ToInt32(rdr("student_id"))
                            oldBedId = Convert.ToInt32(rdr("bed_id"))
                            semesterId = Convert.ToInt32(rdr("semester_id"))
                            oldStatus = rdr("allocation_status").ToString()
                        End Using

                        If oldStatus <> "ACTIVE" Then
                            trans.Rollback()
                            Return (False, $"Cannot transfer: Allocation status is '{oldStatus}'.", 0)
                        End If

                        ' 2. Verify New Bed
                        Dim newBedCmd As New SqlCommand("SELECT status, semester_rate FROM dbo.beds WHERE bed_id = @bId;", conn, trans)
                        newBedCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", newBedId, SqlDbType.Int))
                        Dim newBedStatus As String = String.Empty
                        Dim newBedRate As Decimal = 0.0D
                        Using rdr = newBedCmd.ExecuteReader()
                            If Not rdr.Read() Then
                                rdr.Close()
                                trans.Rollback()
                                Return (False, "Target destination bed does not exist.", 0)
                            End If
                            newBedStatus = rdr("status").ToString()
                            newBedRate = Convert.ToDecimal(rdr("semester_rate"))
                        End Using

                        If newBedStatus <> "AVAILABLE" Then
                            trans.Rollback()
                            Return (False, $"Target bed status is '{newBedStatus}'. Only AVAILABLE beds can receive a transfer.", 0)
                        End If

                        ' 3. Mark Old Allocation as TRANSFERRED
                        Dim transferOldSql As String =
                            "UPDATE dbo.bed_allocations " &
                            "SET allocation_status = 'TRANSFERRED', " &
                            "    notes = CASE WHEN notes IS NULL OR notes = '' THEN @notes ELSE CONCAT(notes, CHAR(13), CHAR(10), @notes) END, " &
                            "    updated_at = SYSUTCDATETIME() " &
                            "WHERE allocation_id = @id;"
                        Dim transOldCmd As New SqlCommand(transferOldSql, conn, trans)
                        transOldCmd.Parameters.Add(DatabaseConnection.CreateParam("@id", allocationId, SqlDbType.Int))
                        transOldCmd.Parameters.Add(DatabaseConnection.CreateParam("@notes", $"Transferred to Bed ID {newBedId} on {DateTime.UtcNow:yyyy-MM-dd}. Reason: {reason.Trim()}", SqlDbType.NVarChar))
                        transOldCmd.ExecuteNonQuery()

                        ' 4. Release Old Bed back to AVAILABLE
                        Dim releaseOldBedCmd As New SqlCommand("UPDATE dbo.beds SET status = 'AVAILABLE', updated_at = SYSUTCDATETIME() WHERE bed_id = @bId;", conn, trans)
                        releaseOldBedCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", oldBedId, SqlDbType.Int))
                        releaseOldBedCmd.ExecuteNonQuery()

                        ' 5. Create New Allocation for Destination Bed
                        Dim insertNewSql As String =
                            "INSERT INTO dbo.bed_allocations (student_id, bed_id, semester_id, semester_fee, allocation_date, allocation_status, notes, created_by, created_at, updated_at) " &
                            "VALUES (@sId, @bId, @semId, @fee, SYSUTCDATETIME(), 'ACTIVE', @notes, @userId, SYSUTCDATETIME(), SYSUTCDATETIME()); " &
                            "SELECT CAST(SCOPE_IDENTITY() AS INT);"
                        Dim insertNewCmd As New SqlCommand(insertNewSql, conn, trans)
                        insertNewCmd.Parameters.Add(DatabaseConnection.CreateParam("@sId", studentId, SqlDbType.Int))
                        insertNewCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", newBedId, SqlDbType.Int))
                        insertNewCmd.Parameters.Add(DatabaseConnection.CreateParam("@semId", semesterId, SqlDbType.Int))
                        insertNewCmd.Parameters.Add(DatabaseConnection.CreateParam("@fee", newBedRate, SqlDbType.Decimal))
                        insertNewCmd.Parameters.Add(DatabaseConnection.CreateParam("@notes", $"Transferred from Allocation ID {allocationId} (Old Bed ID {oldBedId}). Reason: {reason.Trim()}", SqlDbType.NVarChar))
                        insertNewCmd.Parameters.Add(DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int))

                        Dim newAllocId = Convert.ToInt32(insertNewCmd.ExecuteScalar())

                        ' 6. Occupy New Bed
                        Dim occupyNewBedCmd As New SqlCommand("UPDATE dbo.beds SET status = 'OCCUPIED', updated_at = SYSUTCDATETIME() WHERE bed_id = @bId;", conn, trans)
                        occupyNewBedCmd.Parameters.Add(DatabaseConnection.CreateParam("@bId", newBedId, SqlDbType.Int))
                        occupyNewBedCmd.ExecuteNonQuery()

                        ' 7. Audit Activity Log
                        Dim logSql As String =
                            "INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description, action_timestamp, ip_address) " &
                            "VALUES (@userId, 'TRANSFER_BED', 'BED_ALLOCATION', @newId, @desc, SYSUTCDATETIME(), '127.0.0.1');"
                        Dim logCmd As New SqlCommand(logSql, conn, trans)
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int))
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@newId", newAllocId, SqlDbType.Int))
                        logCmd.Parameters.Add(DatabaseConnection.CreateParam("@desc", $"Transferred Student ID {studentId} from Bed ID {oldBedId} to Bed ID {newBedId}. New Allocation ID {newAllocId} created.", SqlDbType.NVarChar))
                        logCmd.ExecuteNonQuery()

                        trans.Commit()
                        Return (True, "Bed transfer completed successfully.", newAllocId)
                    Catch ex As Exception
                        trans.Rollback()
                        Return (False, $"Transfer failed: {ex.Message}", 0)
                    End Try
                End Using
            End Using
        End Function

        Private Sub AuditLogger(userId As Integer, actionType As String, entityType As String, entityId As Integer, description As String)
            Try
                Dim logSql As String =
                    "INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description, action_timestamp, ip_address) " &
                    "VALUES (@userId, @action, @entity, @entId, @desc, SYSUTCDATETIME(), '127.0.0.1');"
                Dim parameters As SqlParameter() = {
                    DatabaseConnection.CreateParam("@userId", userId, SqlDbType.Int),
                    DatabaseConnection.CreateParam("@action", actionType, SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@entity", entityType, SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@entId", entityId, SqlDbType.Int),
                    DatabaseConnection.CreateParam("@desc", description, SqlDbType.NVarChar)
                }
                DatabaseConnection.ExecuteNonQuery(logSql, parameters)
            Catch
            End Try
        End Sub

        Private Function MapAllocationFromRow(row As DataRow) As BedAllocation
            Return New BedAllocation With {
                .AllocationId = Convert.ToInt32(row("allocation_id")),
                .StudentId = Convert.ToInt32(row("student_id")),
                .BedId = Convert.ToInt32(row("bed_id")),
                .SemesterId = Convert.ToInt32(row("semester_id")),
                .SemesterFee = Convert.ToDecimal(row("semester_fee")),
                .AllocationDate = Convert.ToDateTime(row("allocation_date")),
                .CheckInDate = If(IsDBNull(row("check_in_date")), CType(Nothing, DateTime?), Convert.ToDateTime(row("check_in_date"))),
                .CheckOutDate = If(IsDBNull(row("check_out_date")), CType(Nothing, DateTime?), Convert.ToDateTime(row("check_out_date"))),
                .AllocationStatus = row("allocation_status").ToString(),
                .Notes = If(IsDBNull(row("notes")), String.Empty, row("notes").ToString()),
                .CreatedBy = Convert.ToInt32(row("created_by")),
                .CreatedAt = Convert.ToDateTime(row("created_at")),
                .UpdatedAt = Convert.ToDateTime(row("updated_at")),
                .StudentName = row("student_name").ToString(),
                .RegistrationNumber = row("registration_number").ToString(),
                .Course = row("course").ToString(),
                .Department = row("department").ToString(),
                .PhoneNumber = row("phone_number").ToString(),
                .BedNumber = row("bed_number").ToString(),
                .BedLabel = row("bed_label").ToString(),
                .BedType = row("bed_type").ToString(),
                .RoomNumber = row("room_number").ToString(),
                .FloorNumber = Convert.ToInt32(row("floor_number")),
                .HostelId = Convert.ToInt32(row("hostel_id")),
                .HostelName = row("hostel_name").ToString(),
                .HostelCode = row("hostel_code").ToString(),
                .SemesterName = row("semester_name").ToString(),
                .AcademicYear = row("academic_year").ToString(),
                .TotalPaid = Convert.ToDecimal(row("total_paid")),
                .BalanceOwed = Convert.ToDecimal(row("balance_owed"))
            }
        End Function
    End Class
End Namespace
