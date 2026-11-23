Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports University_hostel_mgt.Controls
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Maintenance
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Institutional Hostel Maintenance & Defect Tracking Desk Control.
    ''' Coordinates incident reporting, staff assignment, evidence photographs,
    ''' and automatic bed-space maintenance locking.
    ''' Built strictly with Option B: Frosted Glassmorphism & Acrylic widgets.
    ''' </summary>
    Public Class MaintenanceControl
        Inherits UserControl

        Private ReadOnly _currentUserId As Integer
        Private ReadOnly _maintenanceRepo As New MaintenanceRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()

        Private pnlKpiRow As TableLayoutPanel
        Private kpiTotal As GlassKpiWidget
        Private kpiReported As GlassKpiWidget
        Private kpiInProgress As GlassKpiWidget
        Private kpiResolved As GlassKpiWidget

        Private cardFilter As PrestigeGlassCard
        Private cboStatus As ComboBox
        Private cboHostel As ComboBox
        Private txtSearch As GlassSearchBox
        Private btnReportNew As Button
        Private btnEdit As Button
        Private btnResolve As Button
        Private btnRefresh As Button

        Private splitBody As SplitContainer
        Private dgvMaintenance As DataGridView

        ' Detail / Evidence Sidebar
        Private cardDetail As PrestigeGlassCard
        Private lblDetailHead As Label
        Private lblDetailTitle As Label
        Private lblDetailTarget As Label
        Private lblDetailStatus As Label
        Private txtDetailDesc As TextBox
        Private picDetailEvidence As PictureBox
        Private lblDetailAssigned As Label
        Private txtDetailResolution As TextBox

        Private _records As List(Of MaintenanceRecord) = New List(Of MaintenanceRecord)()
        Private _selectedRecord As MaintenanceRecord = Nothing

        Public Sub New(currentUserId As Integer)
            _currentUserId = currentUserId
            InitializeUI()
            Dock = DockStyle.Fill
            BackColor = ThemeManager.CanvasDark
        End Sub

        Private Sub InitializeUI()
            Me.SuspendLayout()

            ' 1. KPI Panel (Responsive 4-Widget Grid)
            pnlKpiRow = New TableLayoutPanel With {
                .Dock = DockStyle.Top,
                .Height = 100,
                .ColumnCount = 4,
                .RowCount = 1,
                .BackColor = ThemeManager.CanvasDark,
                .Padding = New Padding(16, 12, 16, 8)
            }
            pnlKpiRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlKpiRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlKpiRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlKpiRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))

            kpiTotal = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(0, 0, 6, 0),
                .CategoryTitle = "TOTAL INCIDENTS",
                .PrimaryValue = "0",
                .Subtitle = "Campus-wide defects logged",
                .AccentColor = ThemeManager.GlassStatusInfo,
                .IconSymbol = "📋"
            }

            kpiReported = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4, 0, 4, 0),
                .CategoryTitle = "PENDING / LOCKED",
                .PrimaryValue = "0",
                .Subtitle = "Beds locked to maintenance",
                .AccentColor = ThemeManager.GlassStatusDanger,
                .ValueColor = ThemeManager.GlassStatusDanger,
                .IconSymbol = "⚠️"
            }

            kpiInProgress = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4, 0, 4, 0),
                .CategoryTitle = "IN PROGRESS",
                .PrimaryValue = "0",
                .Subtitle = "Under active technician repair",
                .AccentColor = ThemeManager.GlassStatusWarning,
                .ValueColor = ThemeManager.GlassStatusWarning,
                .IconSymbol = "🔧"
            }

            kpiResolved = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(6, 0, 0, 0),
                .CategoryTitle = "RESOLVED ISSUES",
                .PrimaryValue = "0",
                .Subtitle = "Restored to student occupancy",
                .AccentColor = ThemeManager.GlassStatusSuccess,
                .ValueColor = ThemeManager.GlassStatusSuccess,
                .IconSymbol = "✅"
            }

            pnlKpiRow.Controls.Add(kpiTotal, 0, 0)
            pnlKpiRow.Controls.Add(kpiReported, 1, 0)
            pnlKpiRow.Controls.Add(kpiInProgress, 2, 0)
            pnlKpiRow.Controls.Add(kpiResolved, 3, 0)

            ' 2. Filter & Action Ribbon (Frosted Glass Card)
            cardFilter = New PrestigeGlassCard With {
                .Dock = DockStyle.Top,
                .Height = 58,
                .Margin = New Padding(16, 0, 16, 8),
                .Padding = New Padding(16, 10, 16, 10),
                .AccentColor = ThemeManager.GlassBorderFocus,
                .AccentHeight = 2
            }

            Dim lblSt As New Label With {
                .Text = "Status:",
                .AutoSize = True,
                .Location = New Point(16, 19),
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .BackColor = Color.Transparent
            }

            cboStatus = New ComboBox With {
                .Location = New Point(68, 15),
                .Width = 120,
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White
            }
            cboStatus.Items.AddRange({"ALL", "REPORTED", "IN_PROGRESS", "RESOLVED", "CANCELLED"})
            cboStatus.SelectedIndex = 0

            Dim lblH As New Label With {
                .Text = "Hostel:",
                .AutoSize = True,
                .Location = New Point(200, 19),
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .BackColor = Color.Transparent
            }

            cboHostel = New ComboBox With {
                .Location = New Point(252, 15),
                .Width = 145,
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White
            }

            txtSearch = New GlassSearchBox With {
                .Location = New Point(410, 11),
                .Width = 200,
                .Placeholder = "Search defect, room..."
            }

            btnReportNew = New Button With {
                .Text = "➕ Report Incident",
                .BackColor = Color.FromArgb(30, 58, 138),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .Size = New Size(135, 30),
                .Location = New Point(625, 14),
                .Cursor = Cursors.Hand
            }
            btnReportNew.FlatAppearance.BorderColor = Color.FromArgb(56, 189, 248)
            btnReportNew.FlatAppearance.BorderSize = 1

            btnResolve = New Button With {
                .Text = "✅ Resolve & Restore",
                .BackColor = Color.FromArgb(16, 185, 129),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .Size = New Size(140, 30),
                .Location = New Point(768, 14),
                .Enabled = False,
                .Cursor = Cursors.Hand
            }
            btnResolve.FlatAppearance.BorderSize = 0

            btnEdit = New Button With {
                .Text = "✏️ Edit / Assign",
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = ThemeManager.TextPlatinum,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 8.5F),
                .Size = New Size(115, 30),
                .Location = New Point(916, 14),
                .Enabled = False,
                .Cursor = Cursors.Hand
            }
            btnEdit.FlatAppearance.BorderColor = ThemeManager.GlassBorder

            btnRefresh = New Button With {
                .Text = "🔄",
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Size = New Size(32, 30),
                .Location = New Point(1039, 14),
                .Cursor = Cursors.Hand
            }
            btnRefresh.FlatAppearance.BorderColor = ThemeManager.GlassBorder

            cardFilter.Controls.AddRange({lblSt, cboStatus, lblH, cboHostel, txtSearch, btnReportNew, btnResolve, btnEdit, btnRefresh})

            ' 3. Split Container (Data Grid + Detail/Evidence Pane)
            splitBody = New SplitContainer With {
                .Dock = DockStyle.Fill,
                .Orientation = Orientation.Vertical,
                .SplitterDistance = 730,
                .SplitterWidth = 8,
                .BackColor = ThemeManager.CanvasDark,
                .Padding = New Padding(16, 0, 16, 16)
            }

            ' DataGridView
            dgvMaintenance = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvMaintenance)
            splitBody.Panel1.Controls.Add(dgvMaintenance)

            ' Detail & Evidence Frosted Card
            cardDetail = New PrestigeGlassCard With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16, 14, 16, 14),
                .AccentColor = ThemeManager.GlassStatusWarning,
                .AccentHeight = 3
            }

            lblDetailHead = New Label With {
                .Text = "DEFECT EVIDENCE & RESOLUTION",
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.GlassStatusInfo,
                .Location = New Point(16, 14),
                .AutoSize = True
            }

            lblDetailTitle = New Label With {
                .Text = "Select an incident to view details",
                .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextChalkWhite,
                .Location = New Point(16, 38),
                .Size = New Size(330, 24)
            }

            lblDetailTarget = New Label With {
                .Text = "-",
                .Font = New Font("Segoe UI", 8.5F),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(16, 64),
                .Size = New Size(330, 18)
            }

            lblDetailStatus = New Label With {
                .Text = "STATUS",
                .Font = New Font("Segoe UI", 8.0F, FontStyle.Bold),
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = ThemeManager.TextChalkWhite,
                .Location = New Point(16, 86),
                .Size = New Size(110, 22),
                .TextAlign = ContentAlignment.MiddleCenter
            }

            txtDetailDesc = New TextBox With {
                .Location = New Point(16, 114),
                .Size = New Size(330, 60),
                .Multiline = True,
                .ReadOnly = True,
                .BackColor = Color.FromArgb(12, 19, 31),
                .ForeColor = ThemeManager.TextPlatinum,
                .BorderStyle = BorderStyle.None,
                .Font = New Font("Segoe UI", 8.5F),
                .ScrollBars = ScrollBars.Vertical
            }

            picDetailEvidence = New PictureBox With {
                .Location = New Point(16, 182),
                .Size = New Size(330, 150),
                .SizeMode = PictureBoxSizeMode.Zoom,
                .BackColor = Color.FromArgb(12, 19, 31),
                .BorderStyle = BorderStyle.None
            }

            lblDetailAssigned = New Label With {
                .Text = "Assigned: -",
                .Font = New Font("Segoe UI Semibold", 8.5F),
                .ForeColor = ThemeManager.GlassStatusInfo,
                .Location = New Point(16, 340),
                .Size = New Size(330, 20),
                .BackColor = Color.Transparent
            }

            txtDetailResolution = New TextBox With {
                .Location = New Point(16, 364),
                .Size = New Size(330, 60),
                .Multiline = True,
                .ReadOnly = True,
                .BackColor = Color.FromArgb(12, 19, 31),
                .ForeColor = ThemeManager.TextPlatinum,
                .BorderStyle = BorderStyle.None,
                .Font = New Font("Segoe UI", 8.5F),
                .PlaceholderText = "Resolution notes will appear here once certified."
            }

            cardDetail.Controls.AddRange({lblDetailHead, lblDetailTitle, lblDetailTarget, lblDetailStatus, txtDetailDesc, picDetailEvidence, lblDetailAssigned, txtDetailResolution})
            splitBody.Panel2.Controls.Add(cardDetail)

            ' Assemble Controls
            Controls.Add(splitBody)
            Controls.Add(cardFilter)
            Controls.Add(pnlKpiRow)

            Me.ResumeLayout(False)

            ' Event Handlers
            AddHandler btnReportNew.Click, AddressOf btnReportNew_Click
            AddHandler btnEdit.Click, AddressOf btnEdit_Click
            AddHandler btnResolve.Click, AddressOf btnResolve_Click
            AddHandler btnRefresh.Click, Sub() LoadMaintenanceData()
            AddHandler cboStatus.SelectedIndexChanged, Sub() LoadMaintenanceData()
            AddHandler cboHostel.SelectedIndexChanged, Sub() LoadMaintenanceData()
            AddHandler txtSearch.SearchTextChanged, Sub() LoadMaintenanceData()
            AddHandler dgvMaintenance.SelectionChanged, AddressOf dgvMaintenance_SelectionChanged
            AddHandler dgvMaintenance.CellFormatting, AddressOf dgvMaintenance_CellFormatting
        End Sub

        Public Sub LoadHostelFilters()
            cboHostel.Items.Clear()
            cboHostel.Items.Add(New ComboItem(0, "All Hostels"))
            For Each h In _hostelRepo.GetAllHostels()
                cboHostel.Items.Add(New ComboItem(h.HostelId, $"{h.HostelName} ({h.HostelCode})"))
            Next
            cboHostel.SelectedIndex = 0
        End Sub

        Public Sub LoadMaintenanceData()
            If cboHostel.Items.Count = 0 Then
                LoadHostelFilters()
            End If

            Dim statusFilter = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "ALL")
            Dim hostelId As Integer? = Nothing
            If cboHostel.SelectedItem IsNot Nothing Then
                Dim it = DirectCast(cboHostel.SelectedItem, ComboItem)
                If it.Id > 0 Then hostelId = it.Id
            End If
            Dim searchTerm = txtSearch.QueryText.Trim()

            _records = _maintenanceRepo.GetMaintenanceRecords(statusFilter, hostelId, searchTerm)
            PopulateGrid(_records)
            UpdateKpiCards()
        End Sub

        Private Sub PopulateGrid(records As List(Of MaintenanceRecord))
            dgvMaintenance.Columns.Clear()

            dgvMaintenance.Columns.Add("Id", "ID")
            dgvMaintenance.Columns("Id").Width = 45

            dgvMaintenance.Columns.Add("Target", "Target Facility")
            dgvMaintenance.Columns("Target").Width = 170

            dgvMaintenance.Columns.Add("Title", "Issue Title")
            dgvMaintenance.Columns("Title").Width = 220

            dgvMaintenance.Columns.Add("Status", "Status")
            dgvMaintenance.Columns("Status").Width = 100

            dgvMaintenance.Columns.Add("AssignedTo", "Assigned Technician")
            dgvMaintenance.Columns("AssignedTo").Width = 150

            dgvMaintenance.Columns.Add("ReportedDate", "Reported Date")
            dgvMaintenance.Columns("ReportedDate").Width = 95

            For Each r In records
                Dim rowIdx = dgvMaintenance.Rows.Add(
                    $"#{r.MaintenanceId}",
                    r.TargetDescription,
                    r.IssueTitle,
                    r.MaintenanceStatus,
                    If(String.IsNullOrWhiteSpace(r.AssignedTo), "Unassigned", r.AssignedTo),
                    r.ReportedDate.ToString("yyyy-MM-dd")
                )
                dgvMaintenance.Rows(rowIdx).Tag = r
            Next

            If dgvMaintenance.Rows.Count > 0 Then
                dgvMaintenance.Rows(0).Selected = True
            Else
                ClearDetailPane()
            End If
        End Sub

        Private Sub UpdateKpiCards()
            Dim stats = _maintenanceRepo.GetMaintenanceSummaryCounts()

            Dim total = stats("TOTAL")
            Dim reported = If(stats.ContainsKey("REPORTED"), stats("REPORTED"), 0)
            Dim inProgress = If(stats.ContainsKey("IN_PROGRESS"), stats("IN_PROGRESS"), 0)
            Dim resolved = If(stats.ContainsKey("RESOLVED"), stats("RESOLVED"), 0)

            kpiTotal.PrimaryValue = total.ToString()
            kpiReported.PrimaryValue = reported.ToString()
            kpiInProgress.PrimaryValue = inProgress.ToString()
            kpiResolved.PrimaryValue = resolved.ToString()
        End Sub

        Private Sub dgvMaintenance_SelectionChanged(sender As Object, e As EventArgs)
            If dgvMaintenance.SelectedRows.Count > 0 Then
                _selectedRecord = TryCast(dgvMaintenance.SelectedRows(0).Tag, MaintenanceRecord)
                If _selectedRecord IsNot Nothing Then
                    DisplayRecordDetails(_selectedRecord)
                    btnEdit.Enabled = True
                    btnResolve.Enabled = (_selectedRecord.MaintenanceStatus <> "RESOLVED" AndAlso _selectedRecord.MaintenanceStatus <> "CANCELLED")
                    Return
                End If
            End If

            _selectedRecord = Nothing
            btnEdit.Enabled = False
            btnResolve.Enabled = False
            ClearDetailPane()
        End Sub

        Private Sub DisplayRecordDetails(r As MaintenanceRecord)
            lblDetailTitle.Text = $"#{r.MaintenanceId} — {r.IssueTitle}"
            lblDetailTarget.Text = $"{r.TargetDescription} | Reported by {r.ReporterName}"
            lblDetailStatus.Text = r.MaintenanceStatus
            lblDetailStatus.BackColor = r.StatusColor
            lblDetailStatus.ForeColor = Color.White

            txtDetailDesc.Text = r.IssueDescription
            lblDetailAssigned.Text = $"Assigned: {If(String.IsNullOrWhiteSpace(r.AssignedTo), "Unassigned", r.AssignedTo)}"

            If Not String.IsNullOrWhiteSpace(r.ResolutionNotes) Then
                txtDetailResolution.Text = $"Resolved on {r.ResolvedDate:yyyy-MM-dd HH:mm}:" & vbCrLf & r.ResolutionNotes
            Else
                txtDetailResolution.Text = "Not yet resolved. Work in progress."
            End If

            If Not String.IsNullOrWhiteSpace(r.EvidenceImagePath) Then
                picDetailEvidence.Image = ImageManager.LoadImageSafely(r.EvidenceImagePath, 300, 150, "Defect Evidence")
            Else
                picDetailEvidence.Image = ImageManager.CreateProceduralPlaceholder(300, 150, "No Photographic Evidence")
            End If
        End Sub

        Private Sub ClearDetailPane()
            lblDetailTitle.Text = "No Incident Selected"
            lblDetailTarget.Text = "-"
            lblDetailStatus.Text = "STATUS"
            lblDetailStatus.BackColor = Color.FromArgb(30, 42, 60)
            lblDetailStatus.ForeColor = ThemeManager.TextSilverSlate
            txtDetailDesc.Clear()
            lblDetailAssigned.Text = "Assigned: -"
            txtDetailResolution.Clear()
            picDetailEvidence.Image = ImageManager.CreateProceduralPlaceholder(300, 150, "No Selection")
        End Sub

        Private Sub dgvMaintenance_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If dgvMaintenance.Columns(e.ColumnIndex).Name = "Status" AndAlso e.Value IsNot Nothing Then
                Dim st = e.Value.ToString().ToUpperInvariant()
                Select Case st
                    Case "REPORTED"
                        e.CellStyle.ForeColor = ThemeManager.GlassStatusDanger
                        e.CellStyle.Font = New Font(dgvMaintenance.Font, FontStyle.Bold)
                    Case "IN_PROGRESS"
                        e.CellStyle.ForeColor = ThemeManager.GlassStatusWarning
                        e.CellStyle.Font = New Font(dgvMaintenance.Font, FontStyle.Bold)
                    Case "RESOLVED"
                        e.CellStyle.ForeColor = ThemeManager.GlassStatusSuccess
                        e.CellStyle.Font = New Font(dgvMaintenance.Font, FontStyle.Bold)
                    Case "CANCELLED"
                        e.CellStyle.ForeColor = ThemeManager.TextMutedSlate
                End Select
            End If
        End Sub

        Private Sub btnReportNew_Click(sender As Object, e As EventArgs)
            Using frm As New frmMaintenanceModal(_currentUserId)
                If frm.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadMaintenanceData()
                End If
            End Using
        End Sub

        Private Sub btnEdit_Click(sender As Object, e As EventArgs)
            If _selectedRecord Is Nothing Then Return
            Using frm As New frmMaintenanceModal(_currentUserId, _selectedRecord)
                If frm.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadMaintenanceData()
                End If
            End Using
        End Sub

        Private Sub btnResolve_Click(sender As Object, e As EventArgs)
            If _selectedRecord Is Nothing Then Return
            Using frm As New frmResolveMaintenanceModal(_selectedRecord, _currentUserId)
                If frm.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadMaintenanceData()
                End If
            End Using
        End Sub

        Private Class ComboItem
            Public Property Id As Integer
            Public Property Text As String

            Public Sub New(id As Integer, text As String)
                Me.Id = id
                Me.Text = text
            End Sub

            Public Overrides Function ToString() As String
                Return Text
            End Function
        End Class
    End Class
End Namespace
