Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing a student bed allocation for a specific semester.
    ''' Crucial Business Rule: semester_fee MUST snapshot the bed's semester_rate at allocation time.
    ''' </summary>
    Public Class BedAllocation
        Public Property AllocationId As Integer
        Public Property StudentId As Integer
        Public Property BedId As Integer
        Public Property SemesterId As Integer
        Public Property SemesterFee As Decimal
        Public Property AllocationDate As DateTime = DateTime.UtcNow
        Public Property CheckInDate As DateTime?
        Public Property CheckOutDate As DateTime?
        Public Property AllocationStatus As String = "ACTIVE"   ' ACTIVE, CHECKED_OUT, CANCELLED, TRANSFERRED
        Public Property Notes As String = String.Empty
        Public Property CreatedBy As Integer
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        Public Property UpdatedAt As DateTime = DateTime.UtcNow

        ' Joined student details
        Public Property StudentName As String = String.Empty
        Public Property RegistrationNumber As String = String.Empty
        Public Property Course As String = String.Empty
        Public Property Department As String = String.Empty
        Public Property PhoneNumber As String = String.Empty

        ' Joined bed & accommodation details
        Public Property BedNumber As String = String.Empty
        Public Property BedLabel As String = String.Empty
        Public Property BedType As String = String.Empty
        Public Property RoomNumber As String = String.Empty
        Public Property FloorNumber As Integer
        Public Property HostelId As Integer
        Public Property HostelName As String = String.Empty
        Public Property HostelCode As String = String.Empty

        ' Joined semester details
        Public Property SemesterName As String = String.Empty
        Public Property AcademicYear As String = String.Empty

        ' Financial reconciliations
        Public Property TotalPaid As Decimal = 0.0D
        Public Property BalanceOwed As Decimal = 0.0D

        Public ReadOnly Property BedIdentifier As String
            Get
                Return $"{HostelCode} • Rm {RoomNumber} • Bed {BedNumber}"
            End Get
        End Property

        Public ReadOnly Property IsCheckedIn As Boolean
            Get
                Return CheckInDate.HasValue
            End Get
        End Property

        Public ReadOnly Property IsCheckedOut As Boolean
            Get
                Return CheckOutDate.HasValue
            End Get
        End Property

        Public ReadOnly Property IsFullyPaid As Boolean
            Get
                Return BalanceOwed <= 0.0D
            End Get
        End Property
    End Class
End Namespace
