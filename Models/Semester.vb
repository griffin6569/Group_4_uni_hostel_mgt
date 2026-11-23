Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing an academic semester.
    ''' Accommodation allocations and financial billings are strictly scoped per semester.
    ''' </summary>
    Public Class Semester
        Public Property SemesterId As Integer
        Public Property SemesterName As String = String.Empty
        Public Property SemesterCode As String = String.Empty
        Public Property AcademicYear As String = String.Empty
        Public Property StartDate As DateTime = DateTime.Today
        Public Property EndDate As DateTime = DateTime.Today.AddMonths(4)
        Public Property AllocationOpenDate As DateTime = DateTime.Today.AddDays(-14)
        Public Property AllocationCloseDate As DateTime = DateTime.Today.AddDays(30)
        Public Property Status As String = "UPCOMING"        ' UPCOMING, ACTIVE, COMPLETED, CLOSED
        Public Property CreatedAt As DateTime = DateTime.UtcNow

        Public ReadOnly Property DisplayName As String
            Get
                Return $"{AcademicYear} - {SemesterName} [{Status}]"
            End Get
        End Property

        Public ReadOnly Property IsCurrent As Boolean
            Get
                Return String.Equals(Status, "ACTIVE", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property IsAllocationOpen As Boolean
            Get
                Dim today As DateTime = DateTime.Today
                Return Status = "ACTIVE" AndAlso today >= AllocationOpenDate AndAlso today <= AllocationCloseDate
            End Get
        End Property
    End Class
End Namespace
