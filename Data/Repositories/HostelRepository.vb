Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET repository for university residential hostel buildings.
    ''' </summary>
    Public Class HostelRepository
        Public Function GetAllHostels(Optional filterStatus As String = Nothing) As List(Of Hostel)
            Dim list As New List(Of Hostel)()
            Dim sql As String =
                "SELECT h.hostel_id, h.hostel_name, h.hostel_code, h.location, h.description, " &
                "       h.gender_category, h.total_floors, h.status, h.created_at, h.updated_at, " &
                "       ISNULL(rc.room_count, 0) AS room_count, " &
                "       ISNULL(bc.total_beds, 0) AS total_beds, " &
                "       ISNULL(bc.occupied_beds, 0) AS occupied_beds, " &
                "       ISNULL(bc.available_beds, 0) AS available_beds " &
                "FROM dbo.hostels h " &
                "LEFT JOIN ( " &
                "    SELECT hostel_id, COUNT(*) AS room_count " &
                "    FROM dbo.rooms " &
                "    GROUP BY hostel_id " &
                ") rc ON h.hostel_id = rc.hostel_id " &
                "LEFT JOIN ( " &
                "    SELECT r.hostel_id, " &
                "           COUNT(b.bed_id) AS total_beds, " &
                "           SUM(CASE WHEN b.status = 'OCCUPIED' THEN 1 ELSE 0 END) AS occupied_beds, " &
                "           SUM(CASE WHEN b.status = 'AVAILABLE' THEN 1 ELSE 0 END) AS available_beds " &
                "    FROM dbo.rooms r " &
                "    INNER JOIN dbo.beds b ON r.room_id = b.room_id " &
                "    GROUP BY r.hostel_id " &
                ") bc ON h.hostel_id = bc.hostel_id "

            Dim parameters As New List(Of SqlParameter)()
            If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "ALL" Then
                sql &= " WHERE h.status = @status "
                parameters.Add(DatabaseConnection.CreateParam("@status", filterStatus, SqlDbType.NVarChar))
            End If

            sql &= " ORDER BY h.hostel_name ASC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, parameters.ToArray())
            For Each row As DataRow In dt.Rows
                list.Add(MapHostelFromRow(row))
            Next

            Return list
        End Function

        Public Function GetHostelById(hostelId As Integer) As Hostel
            Dim sql As String =
                "SELECT h.hostel_id, h.hostel_name, h.hostel_code, h.location, h.description, " &
                "       h.gender_category, h.total_floors, h.status, h.created_at, h.updated_at, " &
                "       ISNULL((SELECT COUNT(*) FROM dbo.rooms WHERE hostel_id = h.hostel_id), 0) AS room_count, " &
                "       ISNULL((SELECT COUNT(b.bed_id) FROM dbo.beds b INNER JOIN dbo.rooms r ON b.room_id = r.room_id WHERE r.hostel_id = h.hostel_id), 0) AS total_beds, " &
                "       ISNULL((SELECT COUNT(b.bed_id) FROM dbo.beds b INNER JOIN dbo.rooms r ON b.room_id = r.room_id WHERE r.hostel_id = h.hostel_id AND b.status = 'OCCUPIED'), 0) AS occupied_beds, " &
                "       ISNULL((SELECT COUNT(b.bed_id) FROM dbo.beds b INNER JOIN dbo.rooms r ON b.room_id = r.room_id WHERE r.hostel_id = h.hostel_id AND b.status = 'AVAILABLE'), 0) AS available_beds " &
                "FROM dbo.hostels h " &
                "WHERE h.hostel_id = @id;"

            Dim param = DatabaseConnection.CreateParam("@id", hostelId, SqlDbType.Int)
            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {param})
            If dt.Rows.Count > 0 Then
                Return MapHostelFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        Public Function CreateHostel(hostel As Hostel) As Integer
            Dim sql As String =
                "INSERT INTO dbo.hostels (hostel_name, hostel_code, location, description, gender_category, total_floors, status, created_at, updated_at) " &
                "VALUES (@name, @code, @location, @desc, @gender, @floors, @status, SYSUTCDATETIME(), SYSUTCDATETIME()); " &
                "SELECT CAST(SCOPE_IDENTITY() AS INT);"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@name", hostel.HostelName, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@code", hostel.HostelCode.ToUpper().Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@location", hostel.Location, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@desc", If(String.IsNullOrEmpty(hostel.Description), DBNull.Value, hostel.Description), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@gender", hostel.GenderCategory, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@floors", hostel.TotalFloors, SqlDbType.Int),
                DatabaseConnection.CreateParam("@status", hostel.Status, SqlDbType.NVarChar)
            }

            Dim result = DatabaseConnection.ExecuteScalar(sql, parameters)
            If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                hostel.HostelId = Convert.ToInt32(result)
                Return hostel.HostelId
            End If
            Return 0
        End Function

        Public Function UpdateHostel(hostel As Hostel) As Boolean
            Dim sql As String =
                "UPDATE dbo.hostels " &
                "SET hostel_name = @name, " &
                "    hostel_code = @code, " &
                "    location = @location, " &
                "    description = @desc, " &
                "    gender_category = @gender, " &
                "    total_floors = @floors, " &
                "    status = @status, " &
                "    updated_at = SYSUTCDATETIME() " &
                "WHERE hostel_id = @id;"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@id", hostel.HostelId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@name", hostel.HostelName, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@code", hostel.HostelCode.ToUpper().Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@location", hostel.Location, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@desc", If(String.IsNullOrEmpty(hostel.Description), DBNull.Value, hostel.Description), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@gender", hostel.GenderCategory, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@floors", hostel.TotalFloors, SqlDbType.Int),
                DatabaseConnection.CreateParam("@status", hostel.Status, SqlDbType.NVarChar)
            }

            Return DatabaseConnection.ExecuteNonQuery(sql, parameters) > 0
        End Function

        Public Function DeleteHostel(hostelId As Integer) As (Success As Boolean, Message As String)
            ' Check foreign key references
            Dim checkRoomsSql As String = "SELECT COUNT(*) FROM dbo.rooms WHERE hostel_id = @id;"
            Dim roomCount As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkRoomsSql, {DatabaseConnection.CreateParam("@id", hostelId, SqlDbType.Int)}))
            If roomCount > 0 Then
                Return (False, $"Cannot delete hostel: It contains {roomCount} registered room(s). Delete or reassign the rooms first.")
            End If

            Dim sql As String = "DELETE FROM dbo.hostels WHERE hostel_id = @id;"
            Dim rows As Integer = DatabaseConnection.ExecuteNonQuery(sql, {DatabaseConnection.CreateParam("@id", hostelId, SqlDbType.Int)})
            If rows > 0 Then
                Return (True, "Hostel deleted successfully.")
            Else
                Return (False, "Hostel not found or already deleted.")
            End If
        End Function

        Private Function MapHostelFromRow(row As DataRow) As Hostel
            Return New Hostel() With {
                .HostelId = Convert.ToInt32(row("hostel_id")),
                .HostelName = row("hostel_name").ToString(),
                .HostelCode = row("hostel_code").ToString(),
                .Location = row("location").ToString(),
                .Description = If(IsDBNull(row("description")), String.Empty, row("description").ToString()),
                .GenderCategory = row("gender_category").ToString(),
                .TotalFloors = Convert.ToInt32(row("total_floors")),
                .Status = row("status").ToString(),
                .CreatedAt = Convert.ToDateTime(row("created_at")),
                .UpdatedAt = Convert.ToDateTime(row("updated_at")),
                .RoomCount = Convert.ToInt32(row("room_count")),
                .TotalBedCount = Convert.ToInt32(row("total_beds")),
                .OccupiedBedCount = Convert.ToInt32(row("occupied_beds")),
                .AvailableBedCount = Convert.ToInt32(row("available_beds"))
            }
        End Function
    End Class
End Namespace

