Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET repository providing real-time aggregated dashboard KPIs and analytics directly from SQL Server.
    ''' Strict Rule: All statistics and balances must calculate from actual database records without simulation.
    ''' </summary>
    Public Class DashboardRepository
        Private ReadOnly _semesterRepo As New SemesterRepository()

        Public Function GetDashboardStatistics(Optional semesterId As Integer? = Nothing) As DashboardStatistics
            Dim stats As New DashboardStatistics()

            ' 1. Resolve Active Semester Context
            Dim currentSemester As Semester = Nothing
            If semesterId.HasValue AndAlso semesterId.Value > 0 Then
                currentSemester = _semesterRepo.GetSemesterById(semesterId.Value)
            Else
                currentSemester = _semesterRepo.GetCurrentSemester()
            End If

            If currentSemester IsNot Nothing Then
                stats.ActiveSemesterId = currentSemester.SemesterId
                stats.ActiveSemesterName = currentSemester.SemesterName
                stats.ActiveAcademicYear = currentSemester.AcademicYear
            End If

            ' 2. Capacity & Bed Aggregations
            Dim bedSql As String =
                "SELECT COUNT(DISTINCT h.hostel_id) AS total_hostels, " &
                "       COUNT(DISTINCT r.room_id) AS total_rooms, " &
                "       COUNT(b.bed_id) AS total_beds, " &
                "       SUM(CASE WHEN b.status = 'OCCUPIED' THEN 1 ELSE 0 END) AS occupied_beds, " &
                "       SUM(CASE WHEN b.status = 'AVAILABLE' THEN 1 ELSE 0 END) AS available_beds, " &
                "       SUM(CASE WHEN b.status = 'MAINTENANCE' THEN 1 ELSE 0 END) AS maintenance_beds, " &
                "       SUM(CASE WHEN b.status = 'RESERVED' THEN 1 ELSE 0 END) AS reserved_beds " &
                "FROM dbo.beds b " &
                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "WHERE b.status <> 'INACTIVE';"

            Dim dtBeds As DataTable = DatabaseConnection.ExecuteDataTable(bedSql)
            If dtBeds.Rows.Count > 0 Then
                Dim row = dtBeds.Rows(0)
                stats.TotalHostels = Convert.ToInt32(row("total_hostels"))
                stats.TotalRooms = Convert.ToInt32(row("total_rooms"))
                stats.TotalBeds = Convert.ToInt32(row("total_beds"))
                stats.OccupiedBeds = Convert.ToInt32(row("occupied_beds"))
                stats.AvailableBeds = Convert.ToInt32(row("available_beds"))
                stats.MaintenanceBeds = Convert.ToInt32(row("maintenance_beds"))
                stats.ReservedBeds = Convert.ToInt32(row("reserved_beds"))
            End If

            ' 3. Student Count
            Dim studentSql As String = "SELECT COUNT(*) FROM dbo.students WHERE student_status = 'ACTIVE';"
            stats.TotalStudents = Convert.ToInt32(DatabaseConnection.ExecuteScalar(studentSql))

            ' 4. Financial Calculations for the Target Semester
            Dim semParamId As Integer = stats.ActiveSemesterId
            Dim finSql As String =
                "SELECT " &
                "    ISNULL(COUNT(a.allocation_id), 0) AS active_allocations, " &
                "    ISNULL(SUM(a.semester_fee), 0.00) AS total_billed, " &
                "    ISNULL(( " &
                "        SELECT SUM(p.amount_paid) " &
                "        FROM dbo.payments p " &
                "        INNER JOIN dbo.bed_allocations ba ON p.allocation_id = ba.allocation_id " &
                "        WHERE ba.semester_id = @semId AND p.payment_status = 'CONFIRMED' " &
                "    ), 0.00) AS total_collected " &
                "FROM dbo.bed_allocations a " &
                "WHERE a.semester_id = @semId AND a.allocation_status IN ('ACTIVE', 'CHECKED_OUT');"

            Dim dtFin As DataTable = DatabaseConnection.ExecuteDataTable(finSql, {DatabaseConnection.CreateParam("@semId", semParamId, SqlDbType.Int)})
            If dtFin.Rows.Count > 0 Then
                Dim row = dtFin.Rows(0)
                stats.ActiveAllocationsCount = Convert.ToInt32(row("active_allocations"))
                stats.TotalSemesterBilled = Convert.ToDecimal(row("total_billed"))
                stats.TotalPaymentsCollected = Convert.ToDecimal(row("total_collected"))
                stats.TotalOutstandingBalance = Math.Max(0.0D, stats.TotalSemesterBilled - stats.TotalPaymentsCollected)
            End If

            Return stats
        End Function

        Public Function GetHostelOccupancyList() As DataTable
            Dim sql As String =
                "SELECT h.hostel_name, h.hostel_code, h.gender_category, " &
                "       COUNT(b.bed_id) AS total_beds, " &
                "       SUM(CASE WHEN b.status = 'OCCUPIED' THEN 1 ELSE 0 END) AS occupied_beds, " &
                "       SUM(CASE WHEN b.status = 'AVAILABLE' THEN 1 ELSE 0 END) AS available_beds, " &
                "       CAST(ROUND(CASE WHEN COUNT(b.bed_id) = 0 THEN 0 ELSE (CAST(SUM(CASE WHEN b.status = 'OCCUPIED' THEN 1 ELSE 0 END) AS FLOAT) / COUNT(b.bed_id)) * 100 END, 1) AS DECIMAL(5,1)) AS occupancy_rate " &
                "FROM dbo.hostels h " &
                "LEFT JOIN dbo.rooms r ON h.hostel_id = r.hostel_id " &
                "LEFT JOIN dbo.beds b ON r.room_id = b.room_id AND b.status <> 'INACTIVE' " &
                "GROUP BY h.hostel_id, h.hostel_name, h.hostel_code, h.gender_category " &
                "ORDER BY h.hostel_name ASC;"

            Return DatabaseConnection.ExecuteDataTable(sql)
        End Function

        Public Function GetRecentActivities(Optional limit As Integer = 8) As DataTable
            Dim sql As String =
                "SELECT TOP (@limit) log_id, action_type, entity_type, entity_id, description, action_timestamp, ip_address " &
                "FROM dbo.activity_logs " &
                "ORDER BY action_timestamp DESC;"

            Return DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@limit", limit, SqlDbType.Int)})
        End Function
    End Class
End Namespace

