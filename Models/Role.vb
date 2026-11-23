Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed representation of a system user role.
    ''' </summary>
    Public Class Role
        Public Property RoleId As Integer
        Public Property RoleName As String = String.Empty
        Public Property Description As String = String.Empty
        Public Property CreatedAt As DateTime = DateTime.UtcNow

        Public Const SuperAdmin As String = "SUPER_ADMIN"
        Public Const Admin As String = "ADMIN"
        Public Const AccommodationOfficer As String = "ACCOMMODATION_OFFICER"
        Public Const FinanceOfficer As String = "FINANCE_OFFICER"
        Public Const Viewer As String = "VIEWER"
        Public Const Student As String = "STUDENT"

        Public Sub New()
        End Sub

        Public Sub New(id As Integer, name As String, Optional desc As String = "")
            RoleId = id
            RoleName = name
            Description = desc
        End Sub

        Public Overrides Function ToString() As String
            Return RoleName
        End Function
    End Class
End Namespace

