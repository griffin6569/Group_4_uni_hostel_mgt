Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing real-time accommodation and financial KPI statistics
    ''' calculated directly from SQL Server database records.
    ''' </summary>
    Public Class DashboardStatistics
        ' Capacity & Bed Metrics
        Public Property TotalHostels As Integer = 0
        Public Property TotalRooms As Integer = 0
        Public Property TotalBeds As Integer = 0
        Public Property OccupiedBeds As Integer = 0
        Public Property AvailableBeds As Integer = 0
        Public Property MaintenanceBeds As Integer = 0
        Public Property ReservedBeds As Integer = 0

        ' Student Metrics
        Public Property TotalStudents As Integer = 0
        Public Property ActiveAllocationsCount As Integer = 0

        ' Financial Metrics (Snapshotted fees & confirmed receipts)
        Public Property TotalSemesterBilled As Decimal = 0.0D
        Public Property TotalPaymentsCollected As Decimal = 0.0D
        Public Property TotalOutstandingBalance As Decimal = 0.0D

        ' Semester Context
        Public Property ActiveSemesterId As Integer = 0
        Public Property ActiveSemesterName As String = "None Active"
        Public Property ActiveAcademicYear As String = String.Empty

        Public ReadOnly Property OccupancyPercentage As Double
            Get
                If TotalBeds <= 0 Then Return 0.0
                Return Math.Round((OccupiedBeds / CDbl(TotalBeds)) * 100.0, 1)
            End Get
        End Property

        Public ReadOnly Property CollectionRatePercentage As Double
            Get
                If TotalSemesterBilled <= 0.0D Then Return 0.0
                Return Math.Round((CDbl(TotalPaymentsCollected) / CDbl(TotalSemesterBilled)) * 100.0, 1)
            End Get
        End Property

        Public ReadOnly Property FormattedBilled As String
            Get
                Return $"KES {TotalSemesterBilled:N2}"
            End Get
        End Property

        Public ReadOnly Property FormattedCollected As String
            Get
                Return $"KES {TotalPaymentsCollected:N2}"
            End Get
        End Property

        Public ReadOnly Property FormattedBalance As String
            Get
                Return $"KES {TotalOutstandingBalance:N2}"
            End Get
        End Property
    End Class
End Namespace

