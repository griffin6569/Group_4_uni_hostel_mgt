Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed user model for authentication and role-based authorization.
    ''' </summary>
    Public Class User
        Public Property UserId As Integer
        Public Property FullName As String = String.Empty
        Public Property Username As String = String.Empty
        Public Property Email As String = String.Empty
        Public Property PasswordHash As String = String.Empty
        Public Property RoleId As Integer
        Public Property RoleName As String = String.Empty
        Public Property RoleDescription As String = String.Empty
        Public Property StudentId As Integer? = Nothing
        Public Property RegistrationNumber As String = String.Empty
        Public Property AccountStatus As String = "ACTIVE"
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        Public Property UpdatedAt As DateTime = DateTime.UtcNow
        Public Property LastLogin As DateTime?

        Public ReadOnly Property IsActive As Boolean
            Get
                Return String.Equals(AccountStatus, "ACTIVE", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property IsSuspended As Boolean
            Get
                Return String.Equals(AccountStatus, "SUSPENDED", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property IsStudent As Boolean
            Get
                Return String.Equals(RoleName, Role.Student, StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property IsSuperAdmin As Boolean
            Get
                Return String.Equals(RoleName, Role.SuperAdmin, StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property IsAdmin As Boolean
            Get
                Return IsSuperAdmin OrElse String.Equals(RoleName, Role.Admin, StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property IsAccommodationOfficer As Boolean
            Get
                Return IsAdmin OrElse String.Equals(RoleName, Role.AccommodationOfficer, StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property IsFinanceOfficer As Boolean
            Get
                Return IsAdmin OrElse String.Equals(RoleName, Role.FinanceOfficer, StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property CanManageUsers As Boolean
            Get
                Return IsSuperAdmin OrElse IsAdmin
            End Get
        End Property

        Public ReadOnly Property CanAllocateBeds As Boolean
            Get
                Return IsAccommodationOfficer
            End Get
        End Property

        Public ReadOnly Property CanManagePayments As Boolean
            Get
                Return IsFinanceOfficer
            End Get
        End Property

        Public ReadOnly Property CanViewReports As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return $"{FullName} ({Username}) - {RoleName}"
        End Function
    End Class
End Namespace

