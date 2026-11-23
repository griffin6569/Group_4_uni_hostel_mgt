Imports System.Drawing

Namespace Models
    ''' <summary>
    ''' Metadata accompanying all generated institutional reports for official university headers.
    ''' </summary>
    Public Class ReportMetadata
        Public Property ReportTitle As String = String.Empty
        Public Property Subtitle As String = String.Empty
        Public Property GeneratedBy As String = "System Administrator"
        Public Property GeneratedAt As DateTime = DateTime.Now
        Public Property SemesterName As String = "All Semesters"
        Public Property AcademicYear As String = String.Empty
        Public Property FilterDescription As String = "All Records"
        Public Property TotalRecords As Integer = 0
    End Class

    ''' <summary>
    ''' Data item for the Hostel Occupancy & Capacity Matrix report.
    ''' </summary>
    Public Class OccupancyReportItem
        Public Property HostelName As String = String.Empty
        Public Property HostelCode As String = String.Empty
        Public Property GenderCategory As String = String.Empty
        Public Property FloorNumber As Integer = 1
        Public Property RoomNumber As String = String.Empty
        Public Property RoomType As String = "STANDARD"
        Public Property Capacity As Integer = 0
        Public Property OccupiedBeds As Integer = 0
        Public Property AvailableBeds As Integer = 0
        Public Property MaintenanceBeds As Integer = 0

        Public ReadOnly Property OccupancyPercentage As Double
            Get
                If Capacity = 0 Then Return 0.0
                Return Math.Round((OccupiedBeds / CDbl(Capacity)) * 100.0, 1)
            End Get
        End Property

        Public ReadOnly Property StatusText As String
            Get
                If MaintenanceBeds > 0 AndAlso OccupiedBeds = 0 Then Return "MAINTENANCE"
                If OccupiedBeds >= Capacity Then Return "FULL"
                If OccupiedBeds = 0 Then Return "VACANT"
                Return $"{OccupiedBeds}/{Capacity} OCCUPIED"
            End Get
        End Property
    End Class

    ''' <summary>
    ''' Data item for Active Semester Bed Allocations Schedule.
    ''' </summary>
    Public Class AllocationReportItem
        Public Property AllocationId As Integer
        Public Property RegistrationNumber As String = String.Empty
        Public Property StudentName As String = String.Empty
        Public Property Course As String = String.Empty
        Public Property Gender As String = String.Empty
        Public Property HostelCode As String = String.Empty
        Public Property RoomNumber As String = String.Empty
        Public Property BedLabel As String = String.Empty
        Public Property SemesterCode As String = String.Empty
        Public Property SemesterFee As Decimal = 0D
        Public Property AmountPaid As Decimal = 0D
        Public Property BalanceDue As Decimal = 0D
        Public Property AllocationDate As DateTime
        Public Property CheckInDate As DateTime?
        Public Property AllocationStatus As String = "ACTIVE"
    End Class

    ''' <summary>
    ''' Data item for the Bursar Financial Ledger & Fee Defaulters Audit report.
    ''' </summary>
    Public Class FinancialReportItem
        Public Property RegistrationNumber As String = String.Empty
        Public Property StudentName As String = String.Empty
        Public Property PhoneNumber As String = String.Empty
        Public Property Course As String = String.Empty
        Public Property BedLabel As String = String.Empty
        Public Property HostelCode As String = String.Empty
        Public Property RoomNumber As String = String.Empty
        Public Property SemesterCode As String = String.Empty
        Public Property SemesterFee As Decimal = 0D
        Public Property TotalPaid As Decimal = 0D
        Public Property BalanceDue As Decimal = 0D
        Public Property PaymentStatus As String = "UNPAID" ' CLEARED, PARTIAL, UNPAID
        Public Property LastPaymentDate As DateTime?

        Public ReadOnly Property CollectionPercentage As Double
            Get
                If SemesterFee = 0D Then Return 100.0
                Return Math.Round(CDbl((TotalPaid / SemesterFee) * 100D), 1)
            End Get
        End Property
    End Class

    ''' <summary>
    ''' Data item for the Hostel Maintenance & Downtime Audit report.
    ''' </summary>
    Public Class MaintenanceReportItem
        Public Property MaintenanceId As Integer
        Public Property TargetDescription As String = String.Empty
        Public Property IssueTitle As String = String.Empty
        Public Property MaintenanceStatus As String = "REPORTED"
        Public Property ReportedBy As String = String.Empty
        Public Property AssignedTo As String = String.Empty
        Public Property ReportedDate As DateTime
        Public Property ResolvedDate As DateTime?
        Public Property ResolutionNotes As String = String.Empty

        Public ReadOnly Property DowntimeDays As Integer
            Get
                Dim endDate As DateTime = If(ResolvedDate.HasValue, ResolvedDate.Value, DateTime.Now)
                Return Math.Max(0, CInt(Math.Ceiling((endDate - ReportedDate).TotalDays)))
            End Get
        End Property
    End Class
End Namespace

