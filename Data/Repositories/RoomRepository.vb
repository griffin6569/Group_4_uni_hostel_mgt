Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET repository for hostel rooms with capacity enforcement and bed counters.
    ''' </summary>
    Public Class RoomRepository
        Public Function GetRooms(Optional hostelId As Integer? = Nothing,
                                 Optional status As String = Nothing,
                                 Optional search As String = Nothing) As List(Of Room)
            Dim list As New List(Of Room)()
            Dim sql As String =
                "SELECT r.room_id, r.hostel_id, r.room_number, r.floor_number, r.room_type, " &
                "       r.capacity, r.description, r.status, r.created_at, r.updated_at, " &
                "       h.hostel_name, h.hostel_code, " &
                "       ISNULL(bc.total_beds, 0) AS actual_beds, " &
                "       ISNULL(bc.occupied_beds, 0) AS occupied_beds, " &
                "       ISNULL(bc.available_beds, 0) AS available_beds, " &
                "       img.image_path AS primary_image_path " &
                "FROM dbo.rooms r " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "LEFT JOIN ( " &
                "    SELECT room_id, " &
                "           COUNT(*) AS total_beds, " &
                "           SUM(CASE WHEN status = 'OCCUPIED' THEN 1 ELSE 0 END) AS occupied_beds, " &
                "           SUM(CASE WHEN status = 'AVAILABLE' THEN 1 ELSE 0 END) AS available_beds " &
                "    FROM dbo.beds " &
                "    GROUP BY room_id " &
                ") bc ON r.room_id = bc.room_id " &
                "LEFT JOIN ( " &
                "    SELECT room_id, image_path " &
                "    FROM dbo.room_images " &
                "    WHERE is_primary = 1 " &
                ") img ON r.room_id = img.room_id " &
                "WHERE 1=1 "

            Dim parameters As New List(Of SqlParameter)()

            If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                sql &= " AND r.hostel_id = @hostelId "
                parameters.Add(DatabaseConnection.CreateParam("@hostelId", hostelId.Value, SqlDbType.Int))
            End If

            If Not String.IsNullOrEmpty(status) AndAlso status <> "ALL" Then
                sql &= " AND r.status = @status "
                parameters.Add(DatabaseConnection.CreateParam("@status", status, SqlDbType.NVarChar))
            End If

            If Not String.IsNullOrEmpty(search) Then
                sql &= " AND (r.room_number LIKE @search OR h.hostel_name LIKE @search OR r.room_type LIKE @search) "
                parameters.Add(DatabaseConnection.CreateParam("@search", "%" & search.Trim() & "%", SqlDbType.NVarChar))
            End If

            sql &= " ORDER BY h.hostel_name ASC, r.floor_number ASC, r.room_number ASC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, parameters.ToArray())
            For Each row As DataRow In dt.Rows
                list.Add(MapRoomFromRow(row))
            Next

            Return list
        End Function

        Public Function GetRoomById(roomId As Integer) As Room
            Dim sql As String =
                "SELECT r.room_id, r.hostel_id, r.room_number, r.floor_number, r.room_type, " &
                "       r.capacity, r.description, r.status, r.created_at, r.updated_at, " &
                "       h.hostel_name, h.hostel_code, " &
                "       ISNULL((SELECT COUNT(*) FROM dbo.beds WHERE room_id = r.room_id), 0) AS actual_beds, " &
                "       ISNULL((SELECT COUNT(*) FROM dbo.beds WHERE room_id = r.room_id AND status = 'OCCUPIED'), 0) AS occupied_beds, " &
                "       ISNULL((SELECT COUNT(*) FROM dbo.beds WHERE room_id = r.room_id AND status = 'AVAILABLE'), 0) AS available_beds, " &
                "       (SELECT TOP 1 image_path FROM dbo.room_images WHERE room_id = r.room_id AND is_primary = 1) AS primary_image_path " &
                "FROM dbo.rooms r " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "WHERE r.room_id = @id;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@id", roomId, SqlDbType.Int)})
            If dt.Rows.Count > 0 Then
                Return MapRoomFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        Public Function CreateRoom(room As Room) As (Success As Boolean, Message As String, RoomId As Integer)
            ' Check duplicate room number in same hostel
            Dim checkDupSql As String = "SELECT COUNT(*) FROM dbo.rooms WHERE hostel_id = @hId AND room_number = @rNum;"
            Dim dupParams As SqlParameter() = {
                DatabaseConnection.CreateParam("@hId", room.HostelId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@rNum", room.RoomNumber.Trim(), SqlDbType.NVarChar)
            }
            Dim count As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkDupSql, dupParams))
            If count > 0 Then
                Return (False, $"Room '{room.RoomNumber}' already exists in this hostel.", 0)
            End If

            Dim sql As String =
                "INSERT INTO dbo.rooms (hostel_id, room_number, floor_number, room_type, capacity, description, status, created_at, updated_at) " &
                "VALUES (@hId, @rNum, @floor, @type, @cap, @desc, @status, SYSUTCDATETIME(), SYSUTCDATETIME()); " &
                "SELECT CAST(SCOPE_IDENTITY() AS INT);"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@hId", room.HostelId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@rNum", room.RoomNumber.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@floor", room.FloorNumber, SqlDbType.Int),
                DatabaseConnection.CreateParam("@type", room.RoomType, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@cap", room.Capacity, SqlDbType.Int),
                DatabaseConnection.CreateParam("@desc", If(String.IsNullOrEmpty(room.Description), DBNull.Value, room.Description), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@status", room.Status, SqlDbType.NVarChar)
            }

            Dim result = DatabaseConnection.ExecuteScalar(sql, parameters)
            If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                room.RoomId = Convert.ToInt32(result)
                Return (True, "Room created successfully.", room.RoomId)
            End If
            Return (False, "Failed to create room.", 0)
        End Function

        Public Function UpdateRoom(room As Room) As (Success As Boolean, Message As String)
            ' Capacity rule: cannot reduce room capacity below existing bed count
            Dim checkBedsSql As String = "SELECT COUNT(*) FROM dbo.beds WHERE room_id = @id;"
            Dim actualBeds As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkBedsSql, {DatabaseConnection.CreateParam("@id", room.RoomId, SqlDbType.Int)}))
            If room.Capacity < actualBeds Then
                Return (False, $"Cannot reduce room capacity to {room.Capacity}: There are already {actualBeds} registered bed(s) in this room. Remove excess beds first.")
            End If

            Dim sql As String =
                "UPDATE dbo.rooms " &
                "SET room_number = @rNum, " &
                "    floor_number = @floor, " &
                "    room_type = @type, " &
                "    capacity = @cap, " &
                "    description = @desc, " &
                "    status = @status, " &
                "    updated_at = SYSUTCDATETIME() " &
                "WHERE room_id = @id;"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@id", room.RoomId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@rNum", room.RoomNumber.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@floor", room.FloorNumber, SqlDbType.Int),
                DatabaseConnection.CreateParam("@type", room.RoomType, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@cap", room.Capacity, SqlDbType.Int),
                DatabaseConnection.CreateParam("@desc", If(String.IsNullOrEmpty(room.Description), DBNull.Value, room.Description), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@status", room.Status, SqlDbType.NVarChar)
            }

            Dim rows = DatabaseConnection.ExecuteNonQuery(sql, parameters)
            If rows > 0 Then
                Return (True, "Room updated successfully.")
            Else
                Return (False, "Room not found or no changes made.")
            End If
        End Function

        Public Function DeleteRoom(roomId As Integer) As (Success As Boolean, Message As String)
            Dim checkBedsSql As String = "SELECT COUNT(*) FROM dbo.beds WHERE room_id = @id;"
            Dim bedCount As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkBedsSql, {DatabaseConnection.CreateParam("@id", roomId, SqlDbType.Int)}))
            If bedCount > 0 Then
                Return (False, $"Cannot delete room: It contains {bedCount} registered bed(s). Delete the beds first.")
            End If

            Dim sql As String = "DELETE FROM dbo.rooms WHERE room_id = @id;"
            Dim rows As Integer = DatabaseConnection.ExecuteNonQuery(sql, {DatabaseConnection.CreateParam("@id", roomId, SqlDbType.Int)})
            If rows > 0 Then
                Return (True, "Room deleted successfully.")
            Else
                Return (False, "Room not found.")
            End If
        End Function

        Private Function MapRoomFromRow(row As DataRow) As Room
            Return New Room() With {
                .RoomId = Convert.ToInt32(row("room_id")),
                .HostelId = Convert.ToInt32(row("hostel_id")),
                .RoomNumber = row("room_number").ToString(),
                .FloorNumber = Convert.ToInt32(row("floor_number")),
                .RoomType = row("room_type").ToString(),
                .Capacity = Convert.ToInt32(row("capacity")),
                .Description = If(IsDBNull(row("description")), String.Empty, row("description").ToString()),
                .Status = row("status").ToString(),
                .CreatedAt = Convert.ToDateTime(row("created_at")),
                .UpdatedAt = Convert.ToDateTime(row("updated_at")),
                .HostelName = row("hostel_name").ToString(),
                .HostelCode = row("hostel_code").ToString(),
                .ActualBedCount = Convert.ToInt32(row("actual_beds")),
                .OccupiedBedCount = Convert.ToInt32(row("occupied_beds")),
                .AvailableBedCount = Convert.ToInt32(row("available_beds")),
                .PrimaryImagePath = If(IsDBNull(row("primary_image_path")), String.Empty, row("primary_image_path").ToString())
            }
        End Function
    End Class
End Namespace

