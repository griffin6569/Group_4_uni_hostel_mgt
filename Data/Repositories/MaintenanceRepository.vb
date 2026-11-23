Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET Data Access Repository for maintenance incident reports,
    ''' physical evidence tracking, and automatic bed-space maintenance locking.
    ''' </summary>
    Public Class MaintenanceRepository
        ''' <summary>
        ''' Retrieves filtered maintenance incident records with joined hostel, room, and bed details.
        ''' </summary>
        Public Function GetMaintenanceRecords(Optional statusFilter As String = Nothing,
                                              Optional hostelId As Integer? = Nothing,
                                              Optional searchTerm As String = Nothing) As List(Of MaintenanceRecord)
            Dim list As New List(Of MaintenanceRecord)()
            Dim sql As String = "SELECT m.maintenance_id, m.bed_id, m.room_id, m.issue_title, m.issue_description, " &
                                "       m.maintenance_status, m.reported_by, ISNULL(u.full_name, 'System') AS reporter_name, " &
                                "       m.assigned_to, m.reported_date, m.resolved_date, m.resolution_notes, " &
                                "       m.evidence_image_path, m.created_at, m.updated_at, " &
                                "       r.room_number, h.hostel_id, h.hostel_name, h.hostel_code, " &
                                "       ISNULL(b.bed_label, '') AS bed_label " &
                                "FROM dbo.maintenance_records m " &
                                "INNER JOIN dbo.rooms r ON m.room_id = r.room_id " &
                                "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                                "LEFT JOIN dbo.beds b ON m.bed_id = b.bed_id " &
                                "LEFT JOIN dbo.users u ON m.reported_by = u.user_id " &
                                "WHERE 1=1 "

            If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso statusFilter.ToUpperInvariant() <> "ALL" Then
                sql &= " AND m.maintenance_status = @status "
            End If

            If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                sql &= " AND h.hostel_id = @hostel_id "
            End If

            If Not String.IsNullOrWhiteSpace(searchTerm) Then
                sql &= " AND (m.issue_title LIKE @search OR m.issue_description LIKE @search OR " &
                       "      r.room_number LIKE @search OR b.bed_label LIKE @search OR " &
                       "      m.assigned_to LIKE @search OR h.hostel_name LIKE @search) "
            End If

            sql &= " ORDER BY m.reported_date DESC"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(statusFilter) AndAlso statusFilter.ToUpperInvariant() <> "ALL" Then
                        cmd.Parameters.AddWithValue("@status", statusFilter.Trim())
                    End If
                    If hostelId.HasValue AndAlso hostelId.Value > 0 Then
                        cmd.Parameters.AddWithValue("@hostel_id", hostelId.Value)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchTerm) Then
                        cmd.Parameters.AddWithValue("@search", $"%{searchTerm.Trim()}%")
                    End If

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapMaintenanceRecord(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Gets an individual incident record by its ID.
        ''' </summary>
        Public Function GetRecordById(maintenanceId As Integer) As MaintenanceRecord
            Const sql As String = "SELECT m.maintenance_id, m.bed_id, m.room_id, m.issue_title, m.issue_description, " &
                                  "       m.maintenance_status, m.reported_by, ISNULL(u.full_name, 'System') AS reporter_name, " &
                                  "       m.assigned_to, m.reported_date, m.resolved_date, m.resolution_notes, " &
                                  "       m.evidence_image_path, m.created_at, m.updated_at, " &
                                  "       r.room_number, h.hostel_id, h.hostel_name, h.hostel_code, " &
                                  "       ISNULL(b.bed_label, '') AS bed_label " &
                                  "FROM dbo.maintenance_records m " &
                                  "INNER JOIN dbo.rooms r ON m.room_id = r.room_id " &
                                  "INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                                  "LEFT JOIN dbo.beds b ON m.bed_id = b.bed_id " &
                                  "LEFT JOIN dbo.users u ON m.reported_by = u.user_id " &
                                  "WHERE m.maintenance_id = @id"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", maintenanceId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapMaintenanceRecord(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Creates a new maintenance record and automatically locks the affected bed to MAINTENANCE status.
        ''' </summary>
        Public Function CreateIncident(record As MaintenanceRecord, currentUserId As Integer) As Integer
            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using tx As SqlTransaction = conn.BeginTransaction()
                    Try
                        Const sqlInsert As String = "INSERT INTO dbo.maintenance_records " &
                                                    "(bed_id, room_id, issue_title, issue_description, maintenance_status, reported_by, assigned_to, evidence_image_path) " &
                                                    "VALUES (@bed_id, @room_id, @issue_title, @issue_description, @maintenance_status, @reported_by, @assigned_to, @evidence_image_path); " &
                                                    "SELECT CAST(SCOPE_IDENTITY() AS INT);"

                        Dim newId As Integer
                        Using cmd As New SqlCommand(sqlInsert, conn, tx)
                            cmd.Parameters.AddWithValue("@bed_id", If(record.BedId.HasValue AndAlso record.BedId.Value > 0, CObj(record.BedId.Value), DBNull.Value))
                            cmd.Parameters.AddWithValue("@room_id", record.RoomId)
                            cmd.Parameters.AddWithValue("@issue_title", record.IssueTitle.Trim())
                            cmd.Parameters.AddWithValue("@issue_description", record.IssueDescription.Trim())
                            cmd.Parameters.AddWithValue("@maintenance_status", If(String.IsNullOrWhiteSpace(record.MaintenanceStatus), "REPORTED", record.MaintenanceStatus))
                            cmd.Parameters.AddWithValue("@reported_by", currentUserId)
                            cmd.Parameters.AddWithValue("@assigned_to", If(String.IsNullOrWhiteSpace(record.AssignedTo), DBNull.Value, CObj(record.AssignedTo.Trim())))
                            cmd.Parameters.AddWithValue("@evidence_image_path", If(String.IsNullOrWhiteSpace(record.EvidenceImagePath), DBNull.Value, CObj(record.EvidenceImagePath.Trim())))

                            newId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' Business Rule: If a bed is under maintenance, lock its status to 'MAINTENANCE' in dbo.beds
                        If record.BedId.HasValue AndAlso record.BedId.Value > 0 Then
                            Const sqlLockBed As String = "UPDATE dbo.beds SET status = 'MAINTENANCE', updated_at = SYSUTCDATETIME() WHERE bed_id = @bed_id"
                            Using cmdBed As New SqlCommand(sqlLockBed, conn, tx)
                                cmdBed.Parameters.AddWithValue("@bed_id", record.BedId.Value)
                                cmdBed.ExecuteNonQuery()
                            End Using
                        End If

                        ' Audit log
                        Const sqlAudit As String = "INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description) " &
                                                   "VALUES (@user_id, 'MAINTENANCE_LOGGED', 'MAINTENANCE_RECORDS', @entity_id, @desc)"
                        Using cmdAudit As New SqlCommand(sqlAudit, conn, tx)
                            cmdAudit.Parameters.AddWithValue("@user_id", currentUserId)
                            cmdAudit.Parameters.AddWithValue("@entity_id", newId.ToString())
                            cmdAudit.Parameters.AddWithValue("@desc", $"Reported maintenance #{newId}: '{record.IssueTitle}' for Room ID {record.RoomId}.")
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        tx.Commit()
                        record.MaintenanceId = newId
                        Return newId
                    Catch ex As Exception
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Updates an existing maintenance record's assignment, description, or evidence photograph.
        ''' </summary>
        Public Function UpdateIncident(record As MaintenanceRecord) As Boolean
            Const sql As String = "UPDATE dbo.maintenance_records SET " &
                                  "issue_title = @issue_title, " &
                                  "issue_description = @issue_description, " &
                                  "assigned_to = @assigned_to, " &
                                  "maintenance_status = @maintenance_status, " &
                                  "evidence_image_path = @evidence_image_path, " &
                                  "updated_at = SYSUTCDATETIME() " &
                                  "WHERE maintenance_id = @id"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", record.MaintenanceId)
                    cmd.Parameters.AddWithValue("@issue_title", record.IssueTitle.Trim())
                    cmd.Parameters.AddWithValue("@issue_description", record.IssueDescription.Trim())
                    cmd.Parameters.AddWithValue("@assigned_to", If(String.IsNullOrWhiteSpace(record.AssignedTo), DBNull.Value, CObj(record.AssignedTo.Trim())))
                    cmd.Parameters.AddWithValue("@maintenance_status", record.MaintenanceStatus)
                    cmd.Parameters.AddWithValue("@evidence_image_path", If(String.IsNullOrWhiteSpace(record.EvidenceImagePath), DBNull.Value, CObj(record.EvidenceImagePath.Trim())))

                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Resolves a maintenance incident and optionally restores the affected bed space back to AVAILABLE or OCCUPIED.
        ''' </summary>
        Public Function ResolveIncident(maintenanceId As Integer, resolutionNotes As String, restoreBed As Boolean, currentUserId As Integer) As Boolean
            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using tx As SqlTransaction = conn.BeginTransaction()
                    Try
                        ' 1. Fetch incident record
                        Dim bedId As Integer? = Nothing
                        Const sqlGet As String = "SELECT bed_id FROM dbo.maintenance_records WHERE maintenance_id = @id"
                        Using cmdGet As New SqlCommand(sqlGet, conn, tx)
                            cmdGet.Parameters.AddWithValue("@id", maintenanceId)
                            Dim val = cmdGet.ExecuteScalar()
                            If val IsNot Nothing AndAlso Not Convert.IsDBNull(val) Then
                                bedId = Convert.ToInt32(val)
                            End If
                        End Using

                        ' 2. Mark incident as RESOLVED
                        Const sqlResolve As String = "UPDATE dbo.maintenance_records SET " &
                                                     "maintenance_status = 'RESOLVED', " &
                                                     "resolved_date = SYSUTCDATETIME(), " &
                                                     "resolution_notes = @notes, " &
                                                     "updated_at = SYSUTCDATETIME() " &
                                                     "WHERE maintenance_id = @id"

                        Using cmdResolve As New SqlCommand(sqlResolve, conn, tx)
                            cmdResolve.Parameters.AddWithValue("@id", maintenanceId)
                            cmdResolve.Parameters.AddWithValue("@notes", If(String.IsNullOrWhiteSpace(resolutionNotes), "Resolved and verified operational.", resolutionNotes.Trim()))
                            cmdResolve.ExecuteNonQuery()
                        End Using

                        ' 3. Restore bed if requested and bedId is set
                        If restoreBed AndAlso bedId.HasValue Then
                            ' Check if bed has an active allocation
                            Const sqlCheckActive As String = "SELECT COUNT(1) FROM dbo.bed_allocations WHERE bed_id = @bed_id AND allocation_status = 'ACTIVE'"
                            Dim hasActiveAlloc As Boolean
                            Using cmdActive As New SqlCommand(sqlCheckActive, conn, tx)
                                cmdActive.Parameters.AddWithValue("@bed_id", bedId.Value)
                                hasActiveAlloc = Convert.ToInt32(cmdActive.ExecuteScalar()) > 0
                            End Using

                            Dim restoredStatus As String = If(hasActiveAlloc, "OCCUPIED", "AVAILABLE")
                            Const sqlRestoreBed As String = "UPDATE dbo.beds SET status = @status, updated_at = SYSUTCDATETIME() WHERE bed_id = @bed_id"
                            Using cmdBed As New SqlCommand(sqlRestoreBed, conn, tx)
                                cmdBed.Parameters.AddWithValue("@status", restoredStatus)
                                cmdBed.Parameters.AddWithValue("@bed_id", bedId.Value)
                                cmdBed.ExecuteNonQuery()
                            End Using
                        End If

                        ' 4. Audit Log
                        Const sqlAudit As String = "INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description) " &
                                                   "VALUES (@user_id, 'MAINTENANCE_RESOLVED', 'MAINTENANCE_RECORDS', @entity_id, @desc)"
                        Using cmdAudit As New SqlCommand(sqlAudit, conn, tx)
                            cmdAudit.Parameters.AddWithValue("@user_id", currentUserId)
                            cmdAudit.Parameters.AddWithValue("@entity_id", maintenanceId.ToString())
                            cmdAudit.Parameters.AddWithValue("@desc", $"Resolved maintenance #{maintenanceId}. Notes: {resolutionNotes}")
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        tx.Commit()
                        Return True
                    Catch ex As Exception
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Gets statistical summary counts for maintenance incidents.
        ''' </summary>
        Public Function GetMaintenanceSummaryCounts() As Dictionary(Of String, Integer)
            Dim stats As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase) From {
                {"TOTAL", 0},
                {"REPORTED", 0},
                {"IN_PROGRESS", 0},
                {"RESOLVED", 0}
            }

            Const sql As String = "SELECT maintenance_status, COUNT(1) AS cnt " &
                                  "FROM dbo.maintenance_records " &
                                  "GROUP BY maintenance_status"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Dim total As Integer = 0
                        While reader.Read()
                            Dim st As String = reader("maintenance_status").ToString().ToUpperInvariant()
                            Dim count As Integer = Convert.ToInt32(reader("cnt"))
                            total += count
                            If stats.ContainsKey(st) Then
                                stats(st) = count
                            Else
                                stats.Add(st, count)
                            End If
                        End While
                        stats("TOTAL") = total
                    End Using
                End Using
            End Using
            Return stats
        End Function

        Private Shared Function MapMaintenanceRecord(reader As SqlDataReader) As MaintenanceRecord
            Dim r As New MaintenanceRecord With {
                .MaintenanceId = Convert.ToInt32(reader("maintenance_id")),
                .RoomId = Convert.ToInt32(reader("room_id")),
                .IssueTitle = reader("issue_title").ToString(),
                .IssueDescription = reader("issue_description").ToString(),
                .MaintenanceStatus = reader("maintenance_status").ToString(),
                .ReportedBy = Convert.ToInt32(reader("reported_by")),
                .ReporterName = reader("reporter_name").ToString(),
                .AssignedTo = If(reader.IsDBNull(reader.GetOrdinal("assigned_to")), String.Empty, reader("assigned_to").ToString()),
                .ReportedDate = Convert.ToDateTime(reader("reported_date")),
                .ResolutionNotes = If(reader.IsDBNull(reader.GetOrdinal("resolution_notes")), String.Empty, reader("resolution_notes").ToString()),
                .EvidenceImagePath = If(reader.IsDBNull(reader.GetOrdinal("evidence_image_path")), String.Empty, reader("evidence_image_path").ToString()),
                .CreatedAt = Convert.ToDateTime(reader("created_at")),
                .UpdatedAt = Convert.ToDateTime(reader("updated_at")),
                .RoomNumber = reader("room_number").ToString(),
                .HostelId = Convert.ToInt32(reader("hostel_id")),
                .HostelName = reader("hostel_name").ToString(),
                .HostelCode = reader("hostel_code").ToString(),
                .BedLabel = If(reader.IsDBNull(reader.GetOrdinal("bed_label")), String.Empty, reader("bed_label").ToString())
            }

            If Not reader.IsDBNull(reader.GetOrdinal("bed_id")) Then
                r.BedId = Convert.ToInt32(reader("bed_id"))
            End If

            If Not reader.IsDBNull(reader.GetOrdinal("resolved_date")) Then
                r.ResolvedDate = Convert.ToDateTime(reader("resolved_date"))
            End If

            Return r
        End Function
    End Class
End Namespace
