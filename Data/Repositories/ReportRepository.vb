Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET Data Access Repository executing optimized reporting queries across
    ''' occupancy, active allocations, bursar fee collection/defaulters, and maintenance records.
    ''' </summary>
    Public Class ReportRepository
        ''' <summary>
        ''' Extracts hostel room occupancy data, bed capacities, and vacancy rates.
        ''' </summary>
        Public Function GetOccupancyReportData(Optional hostelId As Integer? = Nothing) As List(Of OccupancyReportItem)
            Dim list As New List(Of OccupancyReportItem)()
            Dim sql As String = "SELECT h.hostel_name, h.hostel_code, h.gender_category, r.floor_number, r.room_number, r.room_type, " &
                                "       r.capacity, " &
                                "       ISNULL(bCounts.OccupiedCount, 0) AS OccupiedBeds, " &
                                "       ISNULL(bCounts.AvailableCount, 0) AS AvailableBeds, " &
                                "       ISNULL(bCounts.MaintenanceCount, 0) AS MaintenanceBeds " &
                                "FROM dbo.rooms r " &
                                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                                "OUTER APPLY ( " &
                                "    SELECT " &
                                "        SUM(CASE WHEN b.status = 'OCCUPIED' THEN 1 ELSE 0 END) AS OccupiedCount, " &
                                "        SUM(CASE WHEN b.status = 'AVAILABLE' THEN 1 ELSE 0 END) AS AvailableCount, " &
                                "        SUM(CASE WHEN b.status = 'MAINTENANCE' THEN 1 ELSE 0 END) AS MaintenanceCount " &
                                "    FROM dbo.beds b " &
                                "    WHERE b.room_id = r.room_id " &
                                ") bCounts " &
                                "WHERE 1=1 "

            If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                sql &= " AND h.hostel_id = @hostel_id "
            End If

            sql &= " ORDER BY h.hostel_name, r.floor_number, r.room_number"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                        cmd.Parameters.AddWithValue("@hostel_id", hostelId.Value)
                    End If

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(New OccupancyReportItem With {
                                .HostelName = reader("hostel_name").ToString(),
                                .HostelCode = reader("hostel_code").ToString(),
                                .GenderCategory = reader("gender_category").ToString(),
                                .FloorNumber = Convert.ToInt32(reader("floor_number")),
                                .RoomNumber = reader("room_number").ToString(),
                                .RoomType = reader("room_type").ToString(),
                                .Capacity = Convert.ToInt32(reader("capacity")),
                                .OccupiedBeds = Convert.ToInt32(reader("OccupiedBeds")),
                                .AvailableBeds = Convert.ToInt32(reader("AvailableBeds")),
                                .MaintenanceBeds = Convert.ToInt32(reader("MaintenanceBeds"))
                            })
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Extracts active resident allocation schedules for university administration.
        ''' </summary>
        Public Function GetAllocationReportData(Optional semesterId As Integer? = Nothing,
                                               Optional hostelId As Integer? = Nothing) As List(Of AllocationReportItem)
            Dim list As New List(Of AllocationReportItem)()
            Dim sql As String = "SELECT a.allocation_id, s.registration_number, " &
                                "       CONCAT(s.first_name, ' ', ISNULL(s.middle_name + ' ', ''), s.last_name) AS student_name, " &
                                "       s.course, s.gender, h.hostel_code, r.room_number, b.bed_label, " &
                                "       sem.semester_code, a.semester_fee, " &
                                "       ISNULL(pay.TotalPaid, 0.00) AS AmountPaid, " &
                                "       (a.semester_fee - ISNULL(pay.TotalPaid, 0.00)) AS BalanceDue, " &
                                "       a.allocation_date, a.check_in_date, a.allocation_status " &
                                "FROM dbo.bed_allocations a " &
                                "INNER JOIN dbo.students s ON a.student_id = s.student_id " &
                                "INNER JOIN dbo.beds b ON a.bed_id = b.bed_id " &
                                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                                "INNER JOIN dbo.semesters sem ON a.semester_id = sem.semester_id " &
                                "OUTER APPLY ( " &
                                "    SELECT SUM(p.amount_paid) AS TotalPaid " &
                                "    FROM dbo.payments p " &
                                "    WHERE p.allocation_id = a.allocation_id AND p.payment_status = 'CONFIRMED' " &
                                ") pay " &
                                "WHERE a.allocation_status = 'ACTIVE' "

            If semesterId.HasValue AndAlso semesterId.Value > 0 Then
                sql &= " AND a.semester_id = @semester_id "
            End If

            If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                sql &= " AND h.hostel_id = @hostel_id "
            End If

            sql &= " ORDER BY h.hostel_code, r.room_number, b.bed_label"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    If semesterId.HasValue AndAlso semesterId.Value > 0 Then
                        cmd.Parameters.AddWithValue("@semester_id", semesterId.Value)
                    End If
                    If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                        cmd.Parameters.AddWithValue("@hostel_id", hostelId.Value)
                    End If

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New AllocationReportItem With {
                                .AllocationId = Convert.ToInt32(reader("allocation_id")),
                                .RegistrationNumber = reader("registration_number").ToString(),
                                .StudentName = reader("student_name").ToString(),
                                .Course = reader("course").ToString(),
                                .Gender = reader("gender").ToString(),
                                .HostelCode = reader("hostel_code").ToString(),
                                .RoomNumber = reader("room_number").ToString(),
                                .BedLabel = reader("bed_label").ToString(),
                                .SemesterCode = reader("semester_code").ToString(),
                                .SemesterFee = Convert.ToDecimal(reader("semester_fee")),
                                .AmountPaid = Convert.ToDecimal(reader("AmountPaid")),
                                .BalanceDue = Convert.ToDecimal(reader("BalanceDue")),
                                .AllocationDate = Convert.ToDateTime(reader("allocation_date")),
                                .AllocationStatus = reader("allocation_status").ToString()
                            }
                            If Not reader.IsDBNull(reader.GetOrdinal("check_in_date")) Then
                                item.CheckInDate = Convert.ToDateTime(reader("check_in_date"))
                            End If
                            list.Add(item)
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Extracts bursar financial data, collections, and fee defaulters / outstanding balances.
        ''' </summary>
        Public Function GetFinancialDefaultersReportData(Optional semesterId As Integer? = Nothing,
                                                        Optional defaultersOnly As Boolean = False) As List(Of FinancialReportItem)
            Dim list As New List(Of FinancialReportItem)()
            Dim sql As String = "SELECT s.registration_number, " &
                                "       CONCAT(s.first_name, ' ', ISNULL(s.middle_name + ' ', ''), s.last_name) AS student_name, " &
                                "       s.phone_number, s.course, b.bed_label, h.hostel_code, r.room_number, " &
                                "       sem.semester_code, a.semester_fee, " &
                                "       ISNULL(pay.TotalPaid, 0.00) AS TotalPaid, " &
                                "       (a.semester_fee - ISNULL(pay.TotalPaid, 0.00)) AS BalanceDue, " &
                                "       pay.LastPaymentDate " &
                                "FROM dbo.bed_allocations a " &
                                "INNER JOIN dbo.students s ON a.student_id = s.student_id " &
                                "INNER JOIN dbo.beds b ON a.bed_id = b.bed_id " &
                                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                                "INNER JOIN dbo.semesters sem ON a.semester_id = sem.semester_id " &
                                "OUTER APPLY ( " &
                                "    SELECT SUM(p.amount_paid) AS TotalPaid, MAX(p.payment_date) AS LastPaymentDate " &
                                "    FROM dbo.payments p " &
                                "    WHERE p.allocation_id = a.allocation_id AND p.payment_status = 'CONFIRMED' " &
                                ") pay " &
                                "WHERE a.allocation_status = 'ACTIVE' "

            If semesterId.HasValue AndAlso semesterId.Value > 0 Then
                sql &= " AND a.semester_id = @semester_id "
            End If

            If defaultersOnly Then
                sql &= " AND (a.semester_fee - ISNULL(pay.TotalPaid, 0.00)) > 0 "
            End If

            sql &= " ORDER BY BalanceDue DESC, s.registration_number ASC"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    If semesterId.HasValue AndAlso semesterId.Value > 0 Then
                        cmd.Parameters.AddWithValue("@semester_id", semesterId.Value)
                    End If

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim fee As Decimal = Convert.ToDecimal(reader("semester_fee"))
                            Dim paid As Decimal = Convert.ToDecimal(reader("TotalPaid"))
                            Dim bal As Decimal = Convert.ToDecimal(reader("BalanceDue"))

                            Dim pStatus As String = "UNPAID"
                            If bal <= 0D Then
                                pStatus = "CLEARED"
                            ElseIf paid > 0D Then
                                pStatus = "PARTIAL"
                            End If

                            Dim item As New FinancialReportItem With {
                                .RegistrationNumber = reader("registration_number").ToString(),
                                .StudentName = reader("student_name").ToString(),
                                .PhoneNumber = reader("phone_number").ToString(),
                                .Course = reader("course").ToString(),
                                .BedLabel = reader("bed_label").ToString(),
                                .HostelCode = reader("hostel_code").ToString(),
                                .RoomNumber = reader("room_number").ToString(),
                                .SemesterCode = reader("semester_code").ToString(),
                                .SemesterFee = fee,
                                .TotalPaid = paid,
                                .BalanceDue = Math.Max(0D, bal),
                                .PaymentStatus = pStatus
                            }

                            If Not reader.IsDBNull(reader.GetOrdinal("LastPaymentDate")) Then
                                item.LastPaymentDate = Convert.ToDateTime(reader("LastPaymentDate"))
                            End If

                            list.Add(item)
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Extracts maintenance issues, assigned personnel, status, and downtime metrics.
        ''' </summary>
        Public Function GetMaintenanceReportData(Optional statusFilter As String = Nothing) As List(Of MaintenanceReportItem)
            Dim list As New List(Of MaintenanceReportItem)()
            Dim sql As String = "SELECT m.maintenance_id, m.issue_title, m.maintenance_status, " &
                                "       ISNULL(u.full_name, 'Staff') AS reported_by, " &
                                "       ISNULL(m.assigned_to, 'Unassigned') AS assigned_to, " &
                                "       m.reported_date, m.resolved_date, m.resolution_notes, " &
                                "       CONCAT(h.hostel_code, ' - Rm ', r.room_number, " &
                                "              CASE WHEN b.bed_label IS NOT NULL THEN ' (' + b.bed_label + ')' ELSE ' (Room-Wide)' END) AS target_desc " &
                                "FROM dbo.maintenance_records m " &
                                "INNER JOIN dbo.rooms r ON m.room_id = r.room_id " &
                                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                                "LEFT JOIN dbo.beds b ON m.bed_id = b.bed_id " &
                                "LEFT JOIN dbo.users u ON m.reported_by = u.user_id " &
                                "WHERE 1=1 "

            If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso statusFilter.ToUpperInvariant() <> "ALL" Then
                sql &= " AND m.maintenance_status = @status "
            End If

            sql &= " ORDER BY m.reported_date DESC"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso statusFilter.ToUpperInvariant() <> "ALL" Then
                        cmd.Parameters.AddWithValue("@status", statusFilter.Trim())
                    End If

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New MaintenanceReportItem With {
                                .MaintenanceId = Convert.ToInt32(reader("maintenance_id")),
                                .TargetDescription = reader("target_desc").ToString(),
                                .IssueTitle = reader("issue_title").ToString(),
                                .MaintenanceStatus = reader("maintenance_status").ToString(),
                                .ReportedBy = reader("reported_by").ToString(),
                                .AssignedTo = reader("assigned_to").ToString(),
                                .ReportedDate = Convert.ToDateTime(reader("reported_date")),
                                .ResolutionNotes = If(reader.IsDBNull(reader.GetOrdinal("resolution_notes")), String.Empty, reader("resolution_notes").ToString())
                            }
                            If Not reader.IsDBNull(reader.GetOrdinal("resolved_date")) Then
                                item.ResolvedDate = Convert.ToDateTime(reader("resolved_date"))
                            End If
                            list.Add(item)
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function
    End Class
End Namespace
