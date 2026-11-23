Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing a university residential hostel building.
    ''' </summary>
    Public Class Hostel
        Public Property HostelId As Integer
        Public Property HostelName As String = String.Empty
        Public Property HostelCode As String = String.Empty
        Public Property Location As String = String.Empty
        Public Property Description As String = String.Empty
        Public Property GenderCategory As String = "MIXED" ' MALE, FEMALE, MIXED
        Public Property TotalFloors As Integer = 1
        Public Property Status As String = "ACTIVE"         ' ACTIVE, INACTIVE, MAINTENANCE
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        Public Property UpdatedAt As DateTime = DateTime.UtcNow

        ' Aggregated / Navigation Properties
        Public Property RoomCount As Integer = 0
        Public Property TotalBedCount As Integer = 0
        Public Property OccupiedBedCount As Integer = 0
        Public Property AvailableBedCount As Integer = 0

        Public ReadOnly Property DisplayName As String
            Get
                Return $"{HostelName} ({HostelCode}) - {GenderCategory}"
            End Get
        End Property

        Public ReadOnly Property OccupancyPercentage As Double
            Get
                If TotalBedCount <= 0 Then Return 0.0
                Return Math.Round((OccupiedBedCount / CDbl(TotalBedCount)) * 100.0, 1)
            End Get
        End Property
    End Class
End Namespace

