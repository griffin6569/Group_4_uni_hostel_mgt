Imports System

Namespace Models
    ''' <summary>
    ''' Global application session manager tracking the active authenticated user and session metadata.
    ''' </summary>
    Public NotInheritable Class UserSession
        Private Shared _currentUser As User
        Private Shared _loginTime As DateTime?
        Private Shared _clientIp As String = "127.0.0.1"

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property CurrentUser As User
            Get
                Return _currentUser
            End Get
        End Property

        Public Shared ReadOnly Property IsAuthenticated As Boolean
            Get
                Return _currentUser IsNot Nothing AndAlso _currentUser.IsActive
            End Get
        End Property

        Public Shared ReadOnly Property LoginTime As DateTime?
            Get
                Return _loginTime
            End Get
        End Property

        Public Shared ReadOnly Property ClientIp As String
            Get
                Return _clientIp
            End Get
        End Property

        ''' <summary>
        ''' Initializes a user session upon successful authentication.
        ''' </summary>
        Public Shared Sub StartSession(user As User, Optional clientIp As String = "127.0.0.1")
            If user Is Nothing Then
                Throw New ArgumentNullException(NameOf(user))
            End If

            _currentUser = user
            _loginTime = DateTime.UtcNow
            _clientIp = If(String.IsNullOrWhiteSpace(clientIp), "127.0.0.1", clientIp)
        End Sub

        ''' <summary>
        ''' Terminates the current active session.
        ''' </summary>
        Public Shared Sub EndSession()
            _currentUser = Nothing
            _loginTime = Nothing
        End Sub
    End Class
End Namespace

