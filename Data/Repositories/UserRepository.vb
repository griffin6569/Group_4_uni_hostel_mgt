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
        ''' Retrieves all system staff users joined with role names.
        ''' </summary>
        Public Function GetAllUsers() As List(Of User)
            Dim list As New List(Of User)()
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
                ORDER BY u.user_id ASC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(query)
            For Each row As DataRow In dt.Rows
                list.Add(MapUserFromRow(row))
            Next
            Return list
        End Function

        ''' <summary>
        ''' Retrieves all available security roles.
        ''' </summary>
        Public Function GetAllRoles() As List(Of Role)
            Dim list As New List(Of Role)()
            Const query As String = "SELECT role_id, role_name, description FROM dbo.roles ORDER BY role_id ASC;"
            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(query)
            For Each row As DataRow In dt.Rows
                list.Add(New Role(Convert.ToInt32(row("role_id")), row("role_name").ToString(), row("description").ToString()))
            Next
            Return list
        End Function

        ''' <summary>
        ''' Registers a new staff user with secure PBKDF2 password hashing.
        ''' </summary>
        Public Function CreateUser(u As User, plaintextPassword As String, currentUserId As Integer) As Integer
            Dim hash As String = Helpers.PasswordHasher.HashPassword(plaintextPassword)
            Const query As String = "
                INSERT INTO dbo.users (full_name, username, email, password_hash, role_id, account_status)
                VALUES (@FullName, @Username, @Email, @Hash, @RoleId, @Status);
                SELECT CAST(SCOPE_IDENTITY() AS INT);"

            Dim params As SqlParameter() = {
                DatabaseConnection.CreateParam("@FullName", u.FullName.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@Username", u.Username.Trim().ToLowerInvariant(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@Email", u.Email.Trim().ToLowerInvariant(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@Hash", hash, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@RoleId", u.RoleId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@Status", If(String.IsNullOrWhiteSpace(u.AccountStatus), "ACTIVE", u.AccountStatus), SqlDbType.NVarChar)
            }

            Dim newId As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(query, params))
            LogActivity(currentUserId, "USER_CREATED", "USERS", newId.ToString(), $"Registered staff user: {u.Username} ({u.FullName}) with role ID {u.RoleId}.")
            Return newId
        End Function

        ''' <summary>
        ''' Updates user demographic, contact, role, and account status details.
        ''' </summary>
        Public Function UpdateUser(u As User, currentUserId As Integer) As Boolean
            Const query As String = "
                UPDATE dbo.users
                SET full_name = @FullName,
                    email = @Email,
                    role_id = @RoleId,
                    account_status = @Status,
                    updated_at = SYSUTCDATETIME()
                WHERE user_id = @UserId;"

            Dim params As SqlParameter() = {
                DatabaseConnection.CreateParam("@UserId", u.UserId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@FullName", u.FullName.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@Email", u.Email.Trim().ToLowerInvariant(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@RoleId", u.RoleId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@Status", u.AccountStatus, SqlDbType.NVarChar)
            }

            Dim rows As Integer = DatabaseConnection.ExecuteNonQuery(query, params)
            If rows > 0 Then
                LogActivity(currentUserId, "USER_UPDATED", "USERS", u.UserId.ToString(), $"Updated staff user: {u.Username} (Role: {u.RoleId}, Status: {u.AccountStatus}).")
            End If
            Return rows > 0
        End Function

        ''' <summary>
        ''' Resets a user's password with a new PBKDF2 hash.
        ''' </summary>
        Public Function ResetPassword(userId As Integer, newPlaintextPassword As String, currentUserId As Integer) As Boolean
            Dim hash As String = Helpers.PasswordHasher.HashPassword(newPlaintextPassword)
            Const query As String = "
                UPDATE dbo.users
                SET password_hash = @Hash,
                    updated_at = SYSUTCDATETIME()
                WHERE user_id = @UserId;"

            Dim params As SqlParameter() = {
                DatabaseConnection.CreateParam("@UserId", userId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@Hash", hash, SqlDbType.NVarChar)
            }

            Dim rows As Integer = DatabaseConnection.ExecuteNonQuery(query, params)
            If rows > 0 Then
                LogActivity(currentUserId, "PASSWORD_RESET", "USERS", userId.ToString(), $"Administrative password reset for user ID {userId}.")
            End If
            Return rows > 0
        End Function

        ''' <summary>
        ''' Retrieves the latest system activity audit log entries.
        ''' </summary>
        Public Function GetActivityLogs(Optional limit As Integer = 100) As DataTable
            Dim query As String = $"
                SELECT TOP ({limit})
                    l.log_id,
                    ISNULL(u.username, 'SYSTEM') AS staff_username,
                    ISNULL(u.full_name, 'System Process') AS staff_name,
                    l.action_type,
                    l.entity_type,
                    l.entity_id,
                    l.description,
                    l.action_timestamp,
                    l.ip_address
                FROM dbo.activity_logs l
                LEFT JOIN dbo.users u ON l.user_id = u.user_id
                ORDER BY l.action_timestamp DESC;"

            Return DatabaseConnection.ExecuteDataTable(query)
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

