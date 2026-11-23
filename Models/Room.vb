Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing an individual hostel room.
    ''' Room capacity must be validated against actual registered beds.
    ''' </summary>
    Public Class Room
        Public Property RoomId As Integer
        Public Property HostelId As Integer
        Public Property RoomNumber As String = String.Empty
        Public Property FloorNumber As Integer = 1
        Public Property RoomType As String = "STANDARD"     ' STANDARD, DELUXE, STUDIO
        Public Property Capacity As Integer = 1
        Public Property Description As String = String.Empty
        Public Property Status As String = "AVAILABLE"       ' AVAILABLE, OCCUPIED, MAINTENANCE, INACTIVE
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        Public Property UpdatedAt As DateTime = DateTime.UtcNow

        ' Navigation / Join Properties
        Public Property HostelName As String = String.Empty
        Public Property HostelCode As String = String.Empty
        Public Property ActualBedCount As Integer = 0
        Public Property OccupiedBedCount As Integer = 0
        Public Property AvailableBedCount As Integer = 0
        Public Property PrimaryImagePath As String = String.Empty

        Public ReadOnly Property DisplayName As String
            Get
                Return $"{HostelCode} - Room {RoomNumber} (Floor {FloorNumber})"
            End Get
        End Property

        Public ReadOnly Property CapacityStatus As String
            Get
                Return $"{ActualBedCount}/{Capacity} Beds"
            End Get
        End Property

        Public ReadOnly Property IsAtCapacity As Boolean
            Get
                Return ActualBedCount >= Capacity
            End Get
        End Property
    End Class
End Namespace

