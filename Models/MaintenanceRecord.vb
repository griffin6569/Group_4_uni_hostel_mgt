Imports System.Drawing
Imports System.IO

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing an institutional hostel maintenance incident.
    ''' Maintains bed locking rules and evidence photo attachments.
    ''' </summary>
    Public Class MaintenanceRecord
        Public Property MaintenanceId As Integer
        Public Property BedId As Integer?
        Public Property RoomId As Integer

        ' Joined display properties
        Public Property HostelId As Integer
        Public Property HostelName As String = String.Empty
        Public Property HostelCode As String = String.Empty
        Public Property RoomNumber As String = String.Empty
        Public Property BedLabel As String = String.Empty

        Public Property IssueTitle As String = String.Empty
        Public Property IssueDescription As String = String.Empty
        Public Property MaintenanceStatus As String = "REPORTED" ' REPORTED, IN_PROGRESS, RESOLVED, CANCELLED
        Public Property ReportedBy As Integer
        Public Property ReporterName As String = String.Empty
        Public Property AssignedTo As String = String.Empty
        Public Property ReportedDate As DateTime = DateTime.UtcNow
        Public Property ResolvedDate As DateTime?
        Public Property ResolutionNotes As String = String.Empty
        Public Property EvidenceImagePath As String = String.Empty
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        Public Property UpdatedAt As DateTime = DateTime.UtcNow

        ''' <summary>
        ''' Target description (Bed level or Room wide).
        ''' </summary>
        Public ReadOnly Property TargetDescription As String
            Get
                If BedId.HasValue AndAlso Not String.IsNullOrWhiteSpace(BedLabel) Then
                    Return $"{HostelCode} - Rm {RoomNumber} ({BedLabel})"
                Else
                    Return $"{HostelCode} - Rm {RoomNumber} (Room-Wide)"
                End If
            End Get
        End Property

        ''' <summary>
        ''' True if the maintenance incident targets a specific bed.
        ''' </summary>
        Public ReadOnly Property IsBedSpecific As Boolean
            Get
                Return BedId.HasValue AndAlso BedId.Value > 0
            End Get
        End Property

        ''' <summary>
        ''' Resolves relative evidence image path to an absolute disk path.
        ''' </summary>
        Public ReadOnly Property FullEvidencePath As String
            Get
                If String.IsNullOrWhiteSpace(EvidenceImagePath) Then Return String.Empty
                If Path.IsPathRooted(EvidenceImagePath) Then Return EvidenceImagePath

                Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
                Dim combined As String = Path.Combine(baseDir, EvidenceImagePath)
                If File.Exists(combined) Then Return combined

                Dim cwdCombined As String = Path.Combine(Directory.GetCurrentDirectory(), EvidenceImagePath)
                If File.Exists(cwdCombined) Then Return cwdCombined

                Return combined
            End Get
        End Property

        ''' <summary>
        ''' Semantic status badge color.
        ''' </summary>
        Public ReadOnly Property StatusColor As Color
            Get
                Select Case MaintenanceStatus.ToUpperInvariant()
                    Case "REPORTED"
                        Return Color.FromArgb(220, 38, 38)     ' Crimson Red
                    Case "IN_PROGRESS"
                        Return Color.FromArgb(217, 119, 6)     ' Amber
                    Case "RESOLVED"
                        Return Color.FromArgb(5, 150, 105)     ' Emerald Green
                    Case "CANCELLED"
                        Return Color.FromArgb(100, 116, 139)   ' Slate Gray
                    Case Else
                        Return Color.FromArgb(71, 85, 105)
                End Select
            End Get
        End Property
    End Class
End Namespace
