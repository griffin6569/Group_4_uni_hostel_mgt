Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET Data Access Repository for room photographs and visual gallery metadata.
    ''' </summary>
    Public Class RoomImageRepository
        ''' <summary>
        ''' Retrieves all registered images for a specific room, ordered by primary flag and display order.
        ''' </summary>
        Public Function GetImagesByRoom(roomId As Integer) As List(Of RoomImage)
            Dim list As New List(Of RoomImage)()
            Const sql As String = "SELECT image_id, room_id, image_path, image_caption, image_category, display_order, is_primary, uploaded_at " &
                                  "FROM dbo.room_images " &
                                  "WHERE room_id = @room_id " &
                                  "ORDER BY is_primary DESC, display_order ASC, uploaded_at DESC"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@room_id", roomId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapRoomImage(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ''' <summary>
        ''' Gets the single primary image for a room, or the first available image if no primary is explicitly set.
        ''' </summary>
        Public Function GetPrimaryImage(roomId As Integer) As RoomImage
            Const sql As String = "SELECT TOP 1 image_id, room_id, image_path, image_caption, image_category, display_order, is_primary, uploaded_at " &
                                  "FROM dbo.room_images " &
                                  "WHERE room_id = @room_id " &
                                  "ORDER BY is_primary DESC, display_order ASC, uploaded_at DESC"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@room_id", roomId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapRoomImage(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Retrieves an image record by its primary key.
        ''' </summary>
        Public Function GetImageById(imageId As Integer) As RoomImage
            Const sql As String = "SELECT image_id, room_id, image_path, image_caption, image_category, display_order, is_primary, uploaded_at " &
                                  "FROM dbo.room_images WHERE image_id = @image_id"

            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@image_id", imageId)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapRoomImage(reader)
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        ''' <summary>
        ''' Inserts a new room image record. If marked as primary, unsets other primary photos for this room.
        ''' </summary>
        Public Function AddImage(img As RoomImage) As Integer
            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using tx As SqlTransaction = conn.BeginTransaction()
                    Try
                        If img.IsPrimary Then
                            Using cmdReset As New SqlCommand("UPDATE dbo.room_images SET is_primary = 0 WHERE room_id = @room_id", conn, tx)
                                cmdReset.Parameters.AddWithValue("@room_id", img.RoomId)
                                cmdReset.ExecuteNonQuery()
                            End Using
                        End If

                        Const sqlInsert As String = "INSERT INTO dbo.room_images (room_id, image_path, image_caption, image_category, display_order, is_primary) " &
                                                    "VALUES (@room_id, @image_path, @image_caption, @image_category, @display_order, @is_primary); " &
                                                    "SELECT CAST(SCOPE_IDENTITY() AS INT);"

                        Dim newId As Integer
                        Using cmd As New SqlCommand(sqlInsert, conn, tx)
                            cmd.Parameters.AddWithValue("@room_id", img.RoomId)
                            cmd.Parameters.AddWithValue("@image_path", img.ImagePath)
                            cmd.Parameters.AddWithValue("@image_caption", If(String.IsNullOrWhiteSpace(img.ImageCaption), DBNull.Value, CObj(img.ImageCaption)))
                            cmd.Parameters.AddWithValue("@image_category", If(String.IsNullOrWhiteSpace(img.ImageCategory), "GENERAL", img.ImageCategory))
                            cmd.Parameters.AddWithValue("@display_order", img.DisplayOrder)
                            cmd.Parameters.AddWithValue("@is_primary", img.IsPrimary)

                            newId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        tx.Commit()
                        img.ImageId = newId
                        Return newId
                    Catch ex As Exception
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Designates a specific photo as the primary image for its room, clearing previous primary designations.
        ''' </summary>
        Public Function SetPrimaryImage(imageId As Integer, roomId As Integer) As Boolean
            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using tx As SqlTransaction = conn.BeginTransaction()
                    Try
                        Using cmdReset As New SqlCommand("UPDATE dbo.room_images SET is_primary = 0 WHERE room_id = @room_id", conn, tx)
                            cmdReset.Parameters.AddWithValue("@room_id", roomId)
                            cmdReset.ExecuteNonQuery()
                        End Using

                        Using cmdSet As New SqlCommand("UPDATE dbo.room_images SET is_primary = 1 WHERE image_id = @image_id AND room_id = @room_id", conn, tx)
                            cmdSet.Parameters.AddWithValue("@image_id", imageId)
                            cmdSet.Parameters.AddWithValue("@room_id", roomId)
                            Dim rows = cmdSet.ExecuteNonQuery()
                            tx.Commit()
                            Return rows > 0
                        End Using
                    Catch ex As Exception
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Deletes an image record from the database.
        ''' </summary>
        Public Function DeleteImage(imageId As Integer) As Boolean
            Const sql As String = "DELETE FROM dbo.room_images WHERE image_id = @image_id"
            Using conn As SqlConnection = DatabaseConnection.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@image_id", imageId)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        End Function

        Private Shared Function MapRoomImage(reader As SqlDataReader) As RoomImage
            Return New RoomImage With {
                .ImageId = Convert.ToInt32(reader("image_id")),
                .RoomId = Convert.ToInt32(reader("room_id")),
                .ImagePath = reader("image_path").ToString(),
                .ImageCaption = If(reader.IsDBNull(reader.GetOrdinal("image_caption")), String.Empty, reader("image_caption").ToString()),
                .ImageCategory = If(reader.IsDBNull(reader.GetOrdinal("image_category")), "GENERAL", reader("image_category").ToString()),
                .DisplayOrder = Convert.ToInt32(reader("display_order")),
                .IsPrimary = Convert.ToBoolean(reader("is_primary")),
                .UploadedAt = Convert.ToDateTime(reader("uploaded_at"))
            }
        End Function
    End Class
End Namespace
