Imports System
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Helpers

Namespace Data
    ''' <summary>
    ''' Central configuration manager for database connectivity.
    ''' Reads parameters from .env via DotEnv and builds strongly typed connection strings.
    ''' </summary>
    Public NotInheritable Class DbConfig
        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Server As String
            Get
                Dim s As String = DotEnv.Get("DB_SERVER", Nothing)
                If String.IsNullOrWhiteSpace(s) Then
                    s = DotEnv.Get("DB_HOST", "localhost")
                End If
                Return s
            End Get
        End Property

        Public Shared ReadOnly Property Database As String
            Get
                Dim d As String = DotEnv.Get("DB_DATABASE", Nothing)
                If String.IsNullOrWhiteSpace(d) Then
                    d = DotEnv.Get("DB_NAME", "university_hostel_db")
                End If
                Return d
            End Get
        End Property

        Public Shared ReadOnly Property Port As Integer
            Get
                Return DotEnv.GetInt("DB_PORT", 1433)
            End Get
        End Property

        Public Shared ReadOnly Property User As String
            Get
                Return DotEnv.Get("DB_USER", "")
            End Get
        End Property

        Public Shared ReadOnly Property Password As String
            Get
                Return DotEnv.Get("DB_PASSWORD", "")
            End Get
        End Property

        Public Shared ReadOnly Property TrustedConnection As Boolean
            Get
                ' If DB_TRUSTED_CONNECTION is explicitly specified, use it.
                ' Otherwise, default to True if User is not provided.
                If Not String.IsNullOrEmpty(DotEnv.Get("DB_TRUSTED_CONNECTION", Nothing)) Then
                    Return DotEnv.GetBool("DB_TRUSTED_CONNECTION", True)
                End If
                Return String.IsNullOrWhiteSpace(User)
            End Get
        End Property

        Public Shared ReadOnly Property TrustServerCertificate As Boolean
            Get
                Return DotEnv.GetBool("DB_TRUST_SERVER_CERTIFICATE", True)
            End Get
        End Property

        Public Shared ReadOnly Property ConnectionTimeout As Integer
            Get
                Return DotEnv.GetInt("DB_CONNECTION_TIMEOUT", 30)
            End Get
        End Property

        Public Shared ReadOnly Property MaxPoolSize As Integer
            Get
                Return DotEnv.GetInt("DB_MAX_POOL_SIZE", 100)
            End Get
        End Property

        ''' <summary>
        ''' Builds a valid SqlConnectionStringBuilder based on current environment settings.
        ''' </summary>
        Public Shared Function BuildConnectionStringBuilder() As SqlConnectionStringBuilder
            Dim builder As New SqlConnectionStringBuilder()

            Dim srv As String = Server
            If Port > 0 AndAlso Port <> 1433 AndAlso Not srv.Contains(","c) AndAlso Not srv.Contains("\"c) Then
                builder.DataSource = $"{srv},{Port}"
            Else
                builder.DataSource = srv
            End If

            builder.InitialCatalog = Database
            builder.ConnectTimeout = ConnectionTimeout
            builder.MaxPoolSize = MaxPoolSize
            builder.TrustServerCertificate = TrustServerCertificate
            builder.MultipleActiveResultSets = True
            builder.ApplicationName = "UniversityHostelManagement"

            If TrustedConnection Then
                builder.IntegratedSecurity = True
            Else
                builder.IntegratedSecurity = False
                builder.UserID = User
                builder.Password = Password
            End If

            Return builder
        End Function

        ''' <summary>
        ''' Returns the complete active database connection string.
        ''' </summary>
        Public Shared Function GetConnectionString() As String
            Return BuildConnectionStringBuilder().ConnectionString
        End Function

        ''' <summary>
        ''' Returns the connection string with the password masked for secure diagnostic logging.
        ''' </summary>
        Public Shared Function GetSanitizedConnectionString() As String
            Dim builder As SqlConnectionStringBuilder = BuildConnectionStringBuilder()
            If Not builder.IntegratedSecurity AndAlso Not String.IsNullOrEmpty(builder.Password) Then
                builder.Password = "********"
            End If
            Return builder.ConnectionString
        End Function

        ''' <summary>
        ''' Validates that minimal required database configuration settings are present.
        ''' </summary>
        Public Shared Sub ValidateConfig()
            If String.IsNullOrWhiteSpace(Server) Then
                Throw New InvalidOperationException("Database server (DB_SERVER or DB_HOST) is not configured in .env.")
            End If

            If String.IsNullOrWhiteSpace(Database) Then
                Throw New InvalidOperationException("Database name (DB_DATABASE or DB_NAME) is not configured in .env.")
            End If

            If Not TrustedConnection AndAlso String.IsNullOrWhiteSpace(User) Then
                Throw New InvalidOperationException("Database user (DB_USER) must be specified when TrustedConnection is false.")
            End If
        End Sub
    End Class
End Namespace

