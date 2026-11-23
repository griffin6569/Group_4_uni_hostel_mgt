Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET repository for managing individual beds, per-semester rates, amenities, and allocation state.
    ''' </summary>
    Public Class BedRepository
        Public Function GetBeds(Optional roomId As Integer? = Nothing,
                               Optional hostelId As Integer? = Nothing,
                               Optional status As String = Nothing,
                               Optional search As String = Nothing) As List(Of Bed)
            Dim list As New List(Of Bed)()
            Dim sql As String =
                "SELECT b.bed_id, b.room_id, b.bed_number, b.bed_label, b.bed_type, " &
                "       b.semester_rate, b.bed_position, b.has_desk, b.has_locker, " &
                "       b.has_window, b.has_ventilation, b.status, b.notes, b.created_at, b.updated_at, " &
                "       r.room_number, r.floor_number, h.hostel_id, h.hostel_name, h.hostel_code, " &
                "       alloc.allocation_id, s.student_id, " &
                "       LTRIM(RTRIM(CONCAT(s.first_name, ' ', ISNULL(s.middle_name, ''), ' ', s.last_name))) AS occupant_name, " &
                "       s.registration_number AS occupant_reg_no " &
                "FROM dbo.beds b " &
                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "LEFT JOIN dbo.bed_allocations alloc ON b.bed_id = alloc.bed_id AND alloc.allocation_status = 'ACTIVE' " &
                "LEFT JOIN dbo.students s ON alloc.student_id = s.student_id " &
                "WHERE 1=1 "

            Dim parameters As New List(Of SqlParameter)()

            If roomId.HasValue AndAlso roomId.Value > 0 Then
                sql &= " AND b.room_id = @roomId "
                parameters.Add(DatabaseConnection.CreateParam("@roomId", roomId.Value, SqlDbType.Int))
            End If

            If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                sql &= " AND h.hostel_id = @hostelId "
                parameters.Add(DatabaseConnection.CreateParam("@hostelId", hostelId.Value, SqlDbType.Int))
            End If

            If Not String.IsNullOrEmpty(status) AndAlso status <> "ALL" Then
                sql &= " AND b.status = @status "
                parameters.Add(DatabaseConnection.CreateParam("@status", status, SqlDbType.NVarChar))
            End If

            If Not String.IsNullOrEmpty(search) Then
                sql &= " AND (b.bed_number LIKE @search OR b.bed_label LIKE @search OR r.room_number LIKE @search OR h.hostel_name LIKE @search OR s.first_name LIKE @search OR s.last_name LIKE @search OR s.registration_number LIKE @search) "
                parameters.Add(DatabaseConnection.CreateParam("@search", "%" & search.Trim() & "%", SqlDbType.NVarChar))
            End If

            sql &= " ORDER BY h.hostel_name ASC, r.room_number ASC, b.bed_number ASC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, parameters.ToArray())
            For Each row As DataRow In dt.Rows
                list.Add(MapBedFromRow(row))
            Next

            Return list
        End Function

        Public Function GetBedById(bedId As Integer) As Bed
            Dim sql As String =
                "SELECT b.bed_id, b.room_id, b.bed_number, b.bed_label, b.bed_type, " &
                "       b.semester_rate, b.bed_position, b.has_desk, b.has_locker, " &
                "       b.has_window, b.has_ventilation, b.status, b.notes, b.created_at, b.updated_at, " &
                "       r.room_number, r.floor_number, h.hostel_id, h.hostel_name, h.hostel_code, " &
                "       alloc.allocation_id, s.student_id, " &
                "       LTRIM(RTRIM(CONCAT(s.first_name, ' ', ISNULL(s.middle_name, ''), ' ', s.last_name))) AS occupant_name, " &
                "       s.registration_number AS occupant_reg_no " &
                "FROM dbo.beds b " &
                "INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "LEFT JOIN dbo.bed_allocations alloc ON b.bed_id = alloc.bed_id AND alloc.allocation_status = 'ACTIVE' " &
                "LEFT JOIN dbo.students s ON alloc.student_id = s.student_id " &
                "WHERE b.bed_id = @id;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@id", bedId, SqlDbType.Int)})
            If dt.Rows.Count > 0 Then
                Return MapBedFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        Public Function CreateBed(bed As Bed) As (Success As Boolean, Message As String, BedId As Integer)
            ' Check duplicate bed number in room
            Dim checkDupSql As String = "SELECT COUNT(*) FROM dbo.beds WHERE room_id = @rId AND bed_number = @bNum;"
            Dim dupCount As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkDupSql, {
                DatabaseConnection.CreateParam("@rId", bed.RoomId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@bNum", bed.BedNumber.Trim(), SqlDbType.NVarChar)
            }))
            If dupCount > 0 Then
                Return (False, $"Bed '{bed.BedNumber}' already exists in this room.", 0)
            End If

            ' Check room capacity ceiling
            Dim checkCapSql As String =
                "SELECT r.capacity, (SELECT COUNT(*) FROM dbo.beds WHERE room_id = r.room_id) AS current_beds " &
                "FROM dbo.rooms r WHERE r.room_id = @rId;"
            Dim capDt As DataTable = DatabaseConnection.ExecuteDataTable(checkCapSql, {DatabaseConnection.CreateParam("@rId", bed.RoomId, SqlDbType.Int)})
            If capDt.Rows.Count > 0 Then
                Dim cap As Integer = Convert.ToInt32(capDt.Rows(0)("capacity"))
                Dim current As Integer = Convert.ToInt32(capDt.Rows(0)("current_beds"))
                If current >= cap Then
                    Return (False, $"Cannot add bed: Room capacity ({cap}) has already been reached ({current} beds registered). Increase room capacity first.", 0)
                End If
            End If

            Dim sql As String =
                "INSERT INTO dbo.beds (room_id, bed_number, bed_label, bed_type, semester_rate, bed_position, " &
                "                      has_desk, has_locker, has_window, has_ventilation, status, notes, created_at, updated_at) " &
                "VALUES (@rId, @bNum, @label, @type, @rate, @pos, @desk, @locker, @win, @vent, @status, @notes, SYSUTCDATETIME(), SYSUTCDATETIME()); " &
                "SELECT CAST(SCOPE_IDENTITY() AS INT);"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@rId", bed.RoomId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@bNum", bed.BedNumber.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@label", bed.BedLabel.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@type", bed.BedType, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@rate", bed.SemesterRate, SqlDbType.Decimal),
                DatabaseConnection.CreateParam("@pos", If(String.IsNullOrEmpty(bed.BedPosition), DBNull.Value, bed.BedPosition), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@desk", bed.HasDesk, SqlDbType.Bit),
                DatabaseConnection.CreateParam("@locker", bed.HasLocker, SqlDbType.Bit),
                DatabaseConnection.CreateParam("@win", bed.HasWindow, SqlDbType.Bit),
                DatabaseConnection.CreateParam("@vent", bed.HasVentilation, SqlDbType.Bit),
                DatabaseConnection.CreateParam("@status", bed.Status, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@notes", If(String.IsNullOrEmpty(bed.Notes), DBNull.Value, bed.Notes), SqlDbType.NVarChar)
            }

            Dim result = DatabaseConnection.ExecuteScalar(sql, parameters)
            If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                bed.BedId = Convert.ToInt32(result)
                Return (True, "Bed registered successfully.", bed.BedId)
            End If
            Return (False, "Failed to register bed.", 0)
        End Function

        Public Function UpdateBed(bed As Bed) As (Success As Boolean, Message As String)
            Dim sql As String =
                "UPDATE dbo.beds " &
                "SET bed_number = @bNum, " &
                "    bed_label = @label, " &
                "    bed_type = @type, " &
                "    semester_rate = @rate, " &
                "    bed_position = @pos, " &
                "    has_desk = @desk, " &
                "    has_locker = @locker, " &
                "    has_window = @win, " &
                "    has_ventilation = @vent, " &
                "    status = @status, " &
                "    notes = @notes, " &
                "    updated_at = SYSUTCDATETIME() " &
                "WHERE bed_id = @id;"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@id", bed.BedId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@bNum", bed.BedNumber.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@label", bed.BedLabel.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@type", bed.BedType, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@rate", bed.SemesterRate, SqlDbType.Decimal),
                DatabaseConnection.CreateParam("@pos", If(String.IsNullOrEmpty(bed.BedPosition), DBNull.Value, bed.BedPosition), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@desk", bed.HasDesk, SqlDbType.Bit),
                DatabaseConnection.CreateParam("@locker", bed.HasLocker, SqlDbType.Bit),
                DatabaseConnection.CreateParam("@win", bed.HasWindow, SqlDbType.Bit),
                DatabaseConnection.CreateParam("@vent", bed.HasVentilation, SqlDbType.Bit),
                DatabaseConnection.CreateParam("@status", bed.Status, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@notes", If(String.IsNullOrEmpty(bed.Notes), DBNull.Value, bed.Notes), SqlDbType.NVarChar)
            }

            Dim rows = DatabaseConnection.ExecuteNonQuery(sql, parameters)
            If rows > 0 Then
                Return (True, "Bed updated successfully.")
            Else
                Return (False, "Bed not found.")
            End If
        End Function

        Public Function DeleteBed(bedId As Integer) As (Success As Boolean, Message As String)
            ' Business rule: Cannot delete bed if active or historical allocations exist
            Dim checkAllocSql As String = "SELECT COUNT(*) FROM dbo.bed_allocations WHERE bed_id = @id;"
            Dim allocCount As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkAllocSql, {DatabaseConnection.CreateParam("@id", bedId, SqlDbType.Int)}))
            If allocCount > 0 Then
                Return (False, $"Cannot delete bed: It has {allocCount} allocation record(s) attached. Set bed status to INACTIVE to retire it while preserving historical records.")
            End If

            Dim sql As String = "DELETE FROM dbo.beds WHERE bed_id = @id;"
            Dim rows = DatabaseConnection.ExecuteNonQuery(sql, {DatabaseConnection.CreateParam("@id", bedId, SqlDbType.Int)})
            If rows > 0 Then
                Return (True, "Bed deleted successfully.")
            Else
                Return (False, "Bed not found.")
            End If
        End Function

        Public Function UpdateBedStatus(bedId As Integer, newStatus As String) As Boolean
            Dim sql As String = "UPDATE dbo.beds SET status = @status, updated_at = SYSUTCDATETIME() WHERE bed_id = @id;"
            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@id", bedId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@status", newStatus, SqlDbType.NVarChar)
            }
            Return DatabaseConnection.ExecuteNonQuery(sql, parameters) > 0
        End Function

        Private Function MapBedFromRow(row As DataRow) As Bed
            Return New Bed() With {
                .BedId = Convert.ToInt32(row("bed_id")),
                .RoomId = Convert.ToInt32(row("room_id")),
                .BedNumber = row("bed_number").ToString(),
                .BedLabel = row("bed_label").ToString(),
                .BedType = row("bed_type").ToString(),
                .SemesterRate = Convert.ToDecimal(row("semester_rate")),
                .BedPosition = If(IsDBNull(row("bed_position")), String.Empty, row("bed_position").ToString()),
                .HasDesk = Convert.ToBoolean(row("has_desk")),
                .HasLocker = Convert.ToBoolean(row("has_locker")),
                .HasWindow = Convert.ToBoolean(row("has_window")),
                .HasVentilation = Convert.ToBoolean(row("has_ventilation")),
                .Status = row("status").ToString(),
                .Notes = If(IsDBNull(row("notes")), String.Empty, row("notes").ToString()),
                .CreatedAt = Convert.ToDateTime(row("created_at")),
                .UpdatedAt = Convert.ToDateTime(row("updated_at")),
                .RoomNumber = row("room_number").ToString(),
                .FloorNumber = Convert.ToInt32(row("floor_number")),
                .HostelId = Convert.ToInt32(row("hostel_id")),
                .HostelName = row("hostel_name").ToString(),
                .HostelCode = row("hostel_code").ToString(),
                .CurrentOccupantName = If(IsDBNull(row("occupant_name")), String.Empty, row("occupant_name").ToString()),
                .CurrentOccupantRegNo = If(IsDBNull(row("occupant_reg_no")), String.Empty, row("occupant_reg_no").ToString()),
                .CurrentAllocationId = If(IsDBNull(row("allocation_id")), CType(Nothing, Integer?), Convert.ToInt32(row("allocation_id")))
            }
        End Function
    End Class
End Namespace

