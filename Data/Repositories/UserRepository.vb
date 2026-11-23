Imports System
Imports System.Data
Imports System.Threading.Tasks
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' Repository for user queries, credential retrieval, last-login tracking, and audit logging.
    ''' </summary>
    Public Class UserRepository
        ''' <summary>
        ''' Retrieves an active or inactive user record joined with their role details.
        ''' Uses parameterized SQL to prevent SQL injection.
        ''' </summary>
        Public Function GetUserByUsername(username As String) As User
            If String.IsNullOrWhiteSpace(username) Then
                Return Nothing
            End If

            Const query As String = "
                SELECT 
                    u.user_id,
                    u.full_name,
                    u.username,
                    u.email,
                    u.password_hash,
                    u.role_id,
                    u.account_status,
                    u.created_at,
                    u.updated_at,
                    u.last_login,
                    r.role_name,
                    r.description AS role_description
                FROM dbo.users u
                INNER JOIN dbo.roles r ON u.role_id = r.role_id
                WHERE u.username = @Username;"

            Dim params As SqlParameter() = {
                DatabaseConnection.CreateParam("@Username", username.Trim(), SqlDbType.NVarChar)
            }

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(query, params)
            If dt.Rows.Count = 0 Then
                Return Nothing
            End If

            Return MapUserFromRow(dt.Rows(0))
        End Function

        ''' <summary>
        ''' Asynchronously retrieves a user by username.
        ''' </summary>
        Public Async Function GetUserByUsernameAsync(username As String) As Task(Of User)
            Return Await Task.Run(Function() GetUserByUsername(username)).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Updates the last login timestamp for the given user.
        ''' </summary>
        Public Function UpdateLastLogin(userId As Integer) As Boolean
            Const query As String = "
                UPDATE dbo.users 
                SET last_login = SYSUTCDATETIME(), 
                    updated_at = SYSUTCDATETIME()
                WHERE user_id = @UserId;"

            Dim params As SqlParameter() = {
                DatabaseConnection.CreateParam("@UserId", userId, SqlDbType.Int)
            }

            Dim rows As Integer = DatabaseConnection.ExecuteNonQuery(query, params)
            Return rows > 0
        End Function

        ''' <summary>
        ''' Records an immutable activity log entry for audit and compliance.
        ''' </summary>
        Public Sub LogActivity(userId As Integer?, actionType As String, entityType As String, entityId As String, description As String, Optional ipAddress As String = "127.0.0.1")
            Try
                Const query As String = "
                    INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description, ip_address, action_timestamp)
                    VALUES (@UserId, @ActionType, @EntityType, @EntityId, @Description, @IpAddress, SYSUTCDATETIME());"

                Dim params As SqlParameter() = {
                    DatabaseConnection.CreateParam("@UserId", If(userId.HasValue, CObj(userId.Value), DBNull.Value), SqlDbType.Int),
                    DatabaseConnection.CreateParam("@ActionType", actionType, SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@EntityType", entityType, SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@EntityId", If(String.IsNullOrEmpty(entityId), DBNull.Value, CObj(entityId)), SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@Description", description, SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@IpAddress", If(String.IsNullOrEmpty(ipAddress), "127.0.0.1", ipAddress), SqlDbType.NVarChar)
                }

                DatabaseConnection.ExecuteNonQuery(query, params)
            Catch ex As Exception
                ' Logging failure must not crash the primary operational workflow,
                ' but can be recorded for diagnostics.
                System.Diagnostics.Debug.WriteLine($"Audit log error: {ex.Message}")
            End Try
        End Sub

        Private Function MapUserFromRow(row As DataRow) As User
            Dim u As New User()
            u.UserId = Convert.ToInt32(row("user_id"))
            u.FullName = row("full_name").ToString()
            u.Username = row("username").ToString()
            u.Email = row("email").ToString()
            u.PasswordHash = row("password_hash").ToString()
            u.RoleId = Convert.ToInt32(row("role_id"))
            u.AccountStatus = row("account_status").ToString()
            u.CreatedAt = Convert.ToDateTime(row("created_at"))
            u.UpdatedAt = Convert.ToDateTime(row("updated_at"))
            If Not IsDBNull(row("last_login")) Then
                u.LastLogin = Convert.ToDateTime(row("last_login"))
            End If
            u.RoleName = row("role_name").ToString()
            u.RoleDescription = If(IsDBNull(row("role_description")), String.Empty, row("role_description").ToString())
            Return u
        End Function
    End Class
End Namespace

