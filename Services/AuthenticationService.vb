Imports System
Imports System.Collections.Concurrent
Imports System.Threading.Tasks
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Services
    Public Enum AuthStatus
        Success
        InvalidCredentials
        AccountInactive
        AccountSuspended
        LockedOut
        DatabaseError
    End Enum

    Public Class AuthenticationResult
        Public Property Success As Boolean
        Public Property Status As AuthStatus
        Public Property ErrorMessage As String = String.Empty
        Public Property User As User

        Public Shared Function Succeeded(user As User) As AuthenticationResult
            Return New AuthenticationResult() With {
                .Success = True,
                .Status = AuthStatus.Success,
                .User = user
            }
        End Function

        Public Shared Function Failed(status As AuthStatus, message As String) As AuthenticationResult
            Return New AuthenticationResult() With {
                .Success = False,
                .Status = status,
                .ErrorMessage = message
            }
        End Function
    End Class

    ''' <summary>
    ''' Business Logic Service handling login credential verification, brute-force mitigation,
    ''' account status validation, session population, and security event logging.
    ''' </summary>
    Public Class AuthenticationService
        Private ReadOnly _userRepo As UserRepository
        Private Shared ReadOnly _failedAttempts As New ConcurrentDictionary(Of String, (Count As Integer, LastAttempt As DateTime))()
        Private Const MaxFailedAttempts As Integer = 5
        Private Const LockoutMinutes As Integer = 5

        Public Sub New(Optional userRepo As UserRepository = Nothing)
            _userRepo = If(userRepo, New UserRepository())
        End Sub

        ''' <summary>
        ''' Authenticates a user with username and plaintext password.
        ''' </summary>
        Public Function Authenticate(username As String, password As String, Optional ipAddress As String = "127.0.0.1") As AuthenticationResult
            ' 1. Input sanitization and pre-validation
            If String.IsNullOrWhiteSpace(username) Then
                Return AuthenticationResult.Failed(AuthStatus.InvalidCredentials, "Please enter your username.")
            End If

            If String.IsNullOrEmpty(password) Then
                Return AuthenticationResult.Failed(AuthStatus.InvalidCredentials, "Please enter your password.")
            End If

            Dim cleanUsername As String = username.Trim()

            ' 2. Check brute-force lockout protection
            If IsLockedOut(cleanUsername) Then
                _userRepo.LogActivity(Nothing, "LOGIN_LOCKOUT_BLOCKED", "USER", cleanUsername,
                                      $"Login attempt blocked due to excessive failed attempts from IP {ipAddress}.", ipAddress)
                Return AuthenticationResult.Failed(AuthStatus.LockedOut,
                    $"Too many failed login attempts. Please wait {LockoutMinutes} minutes before trying again.")
            End If

            ' 3. Retrieve user from repository
            Dim user As User = Nothing
            Try
                user = _userRepo.GetUserByUsername(cleanUsername)
            Catch ex As Exception
                Return AuthenticationResult.Failed(AuthStatus.DatabaseError, $"Database communication error: {ex.Message}")
            End Try

            If user Is Nothing Then
                RecordFailedAttempt(cleanUsername)
                _userRepo.LogActivity(Nothing, "LOGIN_FAILED_UNKNOWN_USER", "USER", cleanUsername,
                                      $"Failed login attempt for non-existent username '{cleanUsername}' from IP {ipAddress}.", ipAddress)
                Return AuthenticationResult.Failed(AuthStatus.InvalidCredentials, "Invalid username or password.")
            End If

            ' 4. Verify password hash using PBKDF2
            Dim isPasswordValid As Boolean = False
            Try
                isPasswordValid = PasswordHasher.VerifyPassword(password, user.PasswordHash)
            Catch ex As Exception
                Return AuthenticationResult.Failed(AuthStatus.DatabaseError, "Cryptographic verification error.")
            End Try

            If Not isPasswordValid Then
                RecordFailedAttempt(cleanUsername)
                _userRepo.LogActivity(user.UserId, "LOGIN_FAILED_BAD_PASSWORD", "USER", user.Username,
                                      $"Invalid password supplied for user '{cleanUsername}' from IP {ipAddress}.", ipAddress)
                Return AuthenticationResult.Failed(AuthStatus.InvalidCredentials, "Invalid username or password.")
            End If

            ' 5. Check account status
            If user.IsSuspended Then
                _userRepo.LogActivity(user.UserId, "LOGIN_REJECTED_SUSPENDED", "USER", user.Username,
                                      $"Login rejected: Account for '{cleanUsername}' is suspended.", ipAddress)
                Return AuthenticationResult.Failed(AuthStatus.AccountSuspended,
                    "Your account has been suspended by the administration. Please contact the Accommodation Office.")
            End If

            If Not user.IsActive Then
                _userRepo.LogActivity(user.UserId, "LOGIN_REJECTED_INACTIVE", "USER", user.Username,
                                      $"Login rejected: Account for '{cleanUsername}' is inactive.", ipAddress)
                Return AuthenticationResult.Failed(AuthStatus.AccountInactive,
                    "Your account is inactive. Please contact the system administrator.")
            End If

            ' 6. Authentication successful: reset failed attempt counter and update login state
            ResetFailedAttempts(cleanUsername)
            _userRepo.UpdateLastLogin(user.UserId)
            UserSession.StartSession(user, ipAddress)

            _userRepo.LogActivity(user.UserId, "LOGIN_SUCCESS", "USER", user.Username,
                                  $"User '{user.Username}' ({user.RoleName}) logged in successfully from IP {ipAddress}.", ipAddress)

            Return AuthenticationResult.Succeeded(user)
        End Function

        ''' <summary>
        ''' Asynchronously authenticates a user.
        ''' </summary>
        Public Async Function AuthenticateAsync(username As String, password As String, Optional ipAddress As String = "127.0.0.1") As Task(Of AuthenticationResult)
            Return Await Task.Run(Function() Authenticate(username, password, ipAddress)).ConfigureAwait(False)
        End Function

        Private Function IsLockedOut(username As String) As Boolean
            Dim record As (Count As Integer, LastAttempt As DateTime)
            If _failedAttempts.TryGetValue(username.ToLowerInvariant(), record) Then
                If record.Count >= MaxFailedAttempts Then
                    Dim elapsedMinutes As Double = (DateTime.UtcNow - record.LastAttempt).TotalMinutes
                    If elapsedMinutes < LockoutMinutes Then
                        Return True
                    Else
                        ' Lockout expired
                        _failedAttempts.TryRemove(username.ToLowerInvariant(), record)
                    End If
                End If
            End If
            Return False
        End Function

        Private Sub RecordFailedAttempt(username As String)
            Dim key As String = username.ToLowerInvariant()
            _failedAttempts.AddOrUpdate(key,
                (Count:=1, LastAttempt:=DateTime.UtcNow),
                Function(k, existing) (Count:=existing.Count + 1, LastAttempt:=DateTime.UtcNow))
        End Sub

        Private Sub ResetFailedAttempts(username As String)
            Dim dummy As (Count As Integer, LastAttempt As DateTime)
            _failedAttempts.TryRemove(username.ToLowerInvariant(), dummy)
        End Sub
    End Class
End Namespace

