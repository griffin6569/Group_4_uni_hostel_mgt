Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Maintenance
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Institutional Hostel Maintenance & Defect Tracking Desk Control.
    ''' Coordinates incident reporting, staff assignment, evidence photographs,
    ''' and automatic bed-space maintenance locking.
    ''' </summary>
    Public Class MaintenanceControl
        Inherits UserControl

        Private ReadOnly _currentUserId As Integer
        Private ReadOnly _maintenanceRepo As New MaintenanceRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()

        Private pnlKpi As Panel
        Private pnlFilter As Panel
        Private cboStatus As ComboBox
        Private cboHostel As ComboBox
        Private txtSearch As TextBox
        Private btnReportNew As Button
        Private btnEdit As Button
        Private btnResolve As Button
        Private btnRefresh As Button

        Private splitBody As SplitContainer
        Private dgvMaintenance As DataGridView

        ' Detail / Evidence Sidebar
        Private pnlDetail As Panel
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
            BackColor = Color.FromArgb(248, 250, 252)
        End Sub

        Private Sub InitializeUI()
            Me.SuspendLayout()

            ' 1. KPI Panel
            pnlKpi = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 84,
                .BackColor = Color.FromArgb(248, 250, 252),
                .Padding = New Padding(16, 8, 16, 8)
            }

            ' 2. Filter & Action Ribbon
            pnlFilter = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 52,
                .BackColor = Color.White,
                .Padding = New Padding(16, 10, 16, 10)
            }

            Dim lblSt As New Label With {.Text = "Status:", .AutoSize = True, .Location = New Point(16, 17), .Font = New Font("Segoe UI Semibold", 8.5F)}
            cboStatus = New ComboBox With {.Location = New Point(65, 14), .Width = 130, .DropDownStyle = ComboBoxStyle.DropDownList}
            cboStatus.Items.AddRange({"ALL", "REPORTED", "IN_PROGRESS", "RESOLVED", "CANCELLED"})
            cboStatus.SelectedIndex = 0

            Dim lblH As New Label With {.Text = "Hostel:", .AutoSize = True, .Location = New Point(205, 17), .Font = New Font("Segoe UI Semibold", 8.5F)}
            cboHostel = New ComboBox With {.Location = New Point(255, 14), .Width = 150, .DropDownStyle = ComboBoxStyle.DropDownList}

            txtSearch = New TextBox With {.Location = New Point(420, 14), .Width = 180, .PlaceholderText = "🔍 Search issue, room, staff..."}

            btnReportNew = New Button With {
                .Text = "➕ Report Incident",
                .BackColor = ThemeManager.NavyPrimary,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .Size = New Size(140, 28),
                .Location = New Point(615, 12)
            }
            btnReportNew.FlatAppearance.BorderSize = 0

            btnResolve = New Button With {
                .Text = "✅ Resolve & Restore",
                .BackColor = Color.FromArgb(5, 150, 105),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .Size = New Size(145, 28),
                .Location = New Point(765, 12),
                .Enabled = False
            }
            btnResolve.FlatAppearance.BorderSize = 0

            btnEdit = New Button With {
                .Text = "✏️ Edit / Assign",
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.NavyPrimary,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 8.5F),
                .Size = New Size(115, 28),
                .Location = New Point(920, 12),
                .Enabled = False
            }
            btnEdit.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)

            btnRefresh = New Button With {
                .Text = "🔄",
                .BackColor = Color.FromArgb(241, 245, 249),
                .FlatStyle = FlatStyle.Flat,
                .Size = New Size(32, 28),
                .Location = New Point(1045, 12)
            }
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)

            pnlFilter.Controls.AddRange({lblSt, cboStatus, lblH, cboHostel, txtSearch, btnReportNew, btnResolve, btnEdit, btnRefresh})

            ' 3. Split Container (Data Grid + Detail/Evidence Pane)
            splitBody = New SplitContainer With {
                .Dock = DockStyle.Fill,
                .Orientation = Orientation.Vertical,
                .SplitterDistance = 720,
                .SplitterWidth = 6,
                .BackColor = Color.FromArgb(226, 232, 240)
            }

            ' DataGridView
            dgvMaintenance = New DataGridView With {
                .Dock = DockStyle.Fill,
                .BackgroundColor = Color.White,
                .BorderStyle = BorderStyle.None,
                .AllowUserToAddRows = False,
                .AllowUserToDeleteRows = False,
                .ReadOnly = True,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .MultiSelect = False,
                .RowHeadersVisible = False,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            }
            ThemeManager.ApplyPrestigeGridStyle(dgvMaintenance)
            splitBody.Panel1.Controls.Add(dgvMaintenance)

            ' Detail & Evidence Panel
            pnlDetail = New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White,
                .Padding = New Padding(16)
            }

            Dim lblDetailHead As New Label With {
                .Text = "DEFECT EVIDENCE & RESOLUTION",
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.NavyPrimary,
                .Location = New Point(16, 12),
                .AutoSize = True
            }

            lblDetailTitle = New Label With {
                .Text = "Select an incident to view details",
                .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(15, 23, 42),
                .Location = New Point(16, 36),
                .Size = New Size(300, 24)
            }

            lblDetailTarget = New Label With {
                .Text = "-",
                .Font = New Font("Segoe UI", 8.5F),
                .ForeColor = Color.FromArgb(100, 116, 139),
                .Location = New Point(16, 62),
                .Size = New Size(300, 18)
            }

            lblDetailStatus = New Label With {
                .Text = "STATUS",
                .Font = New Font("Segoe UI", 7.5F, FontStyle.Bold),
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = Color.FromArgb(71, 85, 105),
                .Location = New Point(16, 84),
                .Size = New Size(110, 22),
                .TextAlign = ContentAlignment.MiddleCenter
            }

            txtDetailDesc = New TextBox With {
                .Location = New Point(16, 114),
                .Size = New Size(300, 60),
                .Multiline = True,
                .ReadOnly = True,
                .BackColor = Color.FromArgb(248, 250, 252),
                .BorderStyle = BorderStyle.FixedSingle,
                .Font = New Font("Segoe UI", 8.5F),
                .ScrollBars = ScrollBars.Vertical
            }

            picDetailEvidence = New PictureBox With {
                .Location = New Point(16, 184),
                .Size = New Size(300, 150),
                .SizeMode = PictureBoxSizeMode.Zoom,
                .BackColor = Color.FromArgb(241, 245, 249),
                .BorderStyle = BorderStyle.FixedSingle
            }

            lblDetailAssigned = New Label With {
                .Text = "Assigned: -",
                .Font = New Font("Segoe UI Semibold", 8.5F),
                .ForeColor = ThemeManager.NavyPrimary,
                .Location = New Point(16, 342),
                .Size = New Size(300, 20)
            }

            txtDetailResolution = New TextBox With {
                .Location = New Point(16, 368),
                .Size = New Size(300, 60),
                .Multiline = True,
                .ReadOnly = True,
                .BackColor = Color.FromArgb(248, 250, 252),
                .BorderStyle = BorderStyle.FixedSingle,
                .Font = New Font("Segoe UI", 8.5F),
                .PlaceholderText = "Resolution notes will appear here once certified."
            }

            pnlDetail.Controls.AddRange({lblDetailHead, lblDetailTitle, lblDetailTarget, lblDetailStatus, txtDetailDesc, picDetailEvidence, lblDetailAssigned, txtDetailResolution})
            splitBody.Panel2.Controls.Add(pnlDetail)

            ' Assemble Controls
            Controls.Add(splitBody)
            Controls.Add(pnlFilter)
            Controls.Add(pnlKpi)

            Me.ResumeLayout(False)

            ' Event Handlers
            AddHandler btnReportNew.Click, AddressOf btnReportNew_Click
            AddHandler btnEdit.Click, AddressOf btnEdit_Click
            AddHandler btnResolve.Click, AddressOf btnResolve_Click
            AddHandler btnRefresh.Click, Sub() LoadMaintenanceData()
            AddHandler cboStatus.SelectedIndexChanged, Sub() LoadMaintenanceData()
            AddHandler cboHostel.SelectedIndexChanged, Sub() LoadMaintenanceData()
            AddHandler txtSearch.TextChanged, Sub() LoadMaintenanceData()
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
            Dim searchTerm = txtSearch.Text.Trim()

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
            pnlKpi.Controls.Clear()
            Dim stats = _maintenanceRepo.GetMaintenanceSummaryCounts()

            Dim total = stats("TOTAL")
            Dim reported = If(stats.ContainsKey("REPORTED"), stats("REPORTED"), 0)
            Dim inProgress = If(stats.ContainsKey("IN_PROGRESS"), stats("IN_PROGRESS"), 0)
            Dim resolved = If(stats.ContainsKey("RESOLVED"), stats("RESOLVED"), 0)

            Dim c1 = CreateKpiCard("TOTAL INCIDENTS", total.ToString(), "Campus-Wide Issues Logged", ThemeManager.NavyPrimary, 16)
            Dim c2 = CreateKpiCard("PENDING / REPORTED", reported.ToString(), "Beds Locked to Maintenance", Color.FromArgb(220, 38, 38), 246)
            Dim c3 = CreateKpiCard("IN PROGRESS", inProgress.ToString(), "Under Active Technician Repair", Color.FromArgb(217, 119, 6), 476)
            Dim c4 = CreateKpiCard("RESOLVED ISSUES", resolved.ToString(), "Restored to Occupancy", Color.FromArgb(5, 150, 105), 706)

            pnlKpi.Controls.AddRange({c1, c2, c3, c4})
        End Sub

        Private Function CreateKpiCard(title As String, value As String, subText As String, accentColor As Color, xPos As Integer) As Panel
            Dim pnl As New Panel With {
                .Size = New Size(215, 68),
                .Location = New Point(xPos, 8),
                .BackColor = Color.White
            }

            Dim pnlStripe As New Panel With {
                .Dock = DockStyle.Left,
                .Width = 4,
                .BackColor = accentColor
            }

            Dim lblT As New Label With {
                .Text = title,
                .Font = New Font("Segoe UI", 7.5F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(100, 116, 139),
                .Location = New Point(12, 8),
                .AutoSize = True
            }

            Dim lblV As New Label With {
                .Text = value,
                .Font = New Font("Segoe UI", 14.0F, FontStyle.Bold),
                .ForeColor = accentColor,
                .Location = New Point(12, 24),
                .AutoSize = True
            }

            Dim lblS As New Label With {
                .Text = subText,
                .Font = New Font("Segoe UI", 7.0F),
                .ForeColor = Color.FromArgb(148, 163, 184),
                .Location = New Point(12, 48),
                .AutoSize = True
            }

            pnl.Controls.AddRange({pnlStripe, lblT, lblV, lblS})
            Return pnl
        End Function

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
            lblDetailStatus.BackColor = Color.FromArgb(241, 245, 249)
            lblDetailStatus.ForeColor = Color.FromArgb(71, 85, 105)
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
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38)
                        e.CellStyle.Font = New Font(dgvMaintenance.Font, FontStyle.Bold)
                    Case "IN_PROGRESS"
                        e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6)
                        e.CellStyle.Font = New Font(dgvMaintenance.Font, FontStyle.Bold)
                    Case "RESOLVED"
                        e.CellStyle.ForeColor = Color.FromArgb(5, 150, 105)
                        e.CellStyle.Font = New Font(dgvMaintenance.Font, FontStyle.Bold)
                    Case "CANCELLED"
                        e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139)
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
