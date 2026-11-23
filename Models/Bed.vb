Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing an individual bed inside a hostel room.
    ''' Accommodation is managed and charged at the individual BED level.
    ''' </summary>
    Public Class Bed
        Public Property BedId As Integer
        Public Property RoomId As Integer
        Public Property BedNumber As String = String.Empty
        Public Property BedLabel As String = String.Empty
        Public Property BedType As String = "STANDARD"     ' STANDARD, BUNK_TOP, BUNK_BOTTOM, DELUXE
        Public Property SemesterRate As Decimal = 0.0D
        Public Property BedPosition As String = String.Empty ' NEAR_WINDOW, NEAR_DOOR, CORNER, CENTER
        Public Property HasDesk As Boolean = True
        Public Property HasLocker As Boolean = True
        Public Property HasWindow As Boolean = False
        Public Property HasVentilation As Boolean = True
        Public Property Status As String = "AVAILABLE"       ' AVAILABLE, OCCUPIED, MAINTENANCE, RESERVED, INACTIVE
        Public Property Notes As String = String.Empty
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        Public Property UpdatedAt As DateTime = DateTime.UtcNow

        ' Navigation / Join Properties
        Public Property RoomNumber As String = String.Empty
        Public Property FloorNumber As Integer = 1
        Public Property HostelId As Integer = 0
        Public Property HostelName As String = String.Empty
        Public Property HostelCode As String = String.Empty
        Public Property CurrentOccupantName As String = String.Empty
        Public Property CurrentOccupantRegNo As String = String.Empty
        Public Property CurrentAllocationId As Integer? = Nothing

        Public ReadOnly Property FullIdentifier As String
            Get
                Return $"{HostelCode} - Rm {RoomNumber} - Bed {BedNumber} ({BedLabel})"
            End Get
        End Property

        Public ReadOnly Property FormattedRate As String
            Get
                Return $"KES {SemesterRate:N2}"
            End Get
        End Property

        Public ReadOnly Property AmenitiesSummary As String
            Get
                Dim amenities As New System.Collections.Generic.List(Of String)()
                If HasDesk Then amenities.Add("Desk")
                If HasLocker Then amenities.Add("Locker")
                If HasWindow Then amenities.Add("Window")
                If HasVentilation Then amenities.Add("Ventilated")
                Return String.Join(", ", amenities)
            End Get
        End Property
    End Class
End Namespace

