Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports University_hostel_mgt.Controls
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Reports
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Controls.Views
    ''' <summary>
    ''' Executive Institutional Reporting & Analytics Control.
    ''' Provides report selection via interactive frosted glass cards, dynamic scope filtering,
    ''' high-contrast data grid previews, and integration with the printing and web document export engines.
    ''' Built strictly with Option B: Frosted Glassmorphism & Acrylic design system.
    ''' </summary>
    Public Class ReportsControl
        Inherits UserControl

        Private ReadOnly _reportRepo As New ReportRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private ReadOnly _semesterRepo As New SemesterRepository()

        Private cardFilter As PrestigeGlassCard
        Private cboSemester As ComboBox
        Private cboHostel As ComboBox
        Private chkDefaultersOnly As CheckBox
        Private cboMaintenanceStatus As ComboBox

        Private pnlCardsRow As TableLayoutPanel
        Private cardOccupancy As PrestigeGlassCard
        Private cardAllocations As PrestigeGlassCard
        Private cardFinancial As PrestigeGlassCard
        Private cardMaintenance As PrestigeGlassCard

        Private cardSummaryStrip As PrestigeGlassCard
        Private lblMetric1 As Label
        Private lblMetric2 As Label
        Private lblMetric3 As Label

        Private dgvPreview As DataGridView
        Private cardBottomActions As PrestigeGlassCard
        Private btnPreviewReport As Button
        Private btnExportHtml As Button
        Private btnRefreshData As Button

        Private _selectedKind As InstitutionalReportGenerator.ReportKind = InstitutionalReportGenerator.ReportKind.Occupancy
        Private _cachedData As Object = Nothing

        Public Sub New()
            InitializeUI()
            Dock = DockStyle.Fill
            BackColor = ThemeManager.CanvasDark
        End Sub

        Private Sub InitializeUI()
            Me.SuspendLayout()

            ' 1. Top Filter Frosted Card
            cardFilter = New PrestigeGlassCard With {
                .Dock = DockStyle.Top,
                .Height = 58,
                .Margin = New Padding(16, 12, 16, 6),
                .Padding = New Padding(16, 10, 16, 10),
                .AccentColor = ThemeManager.GlassBorderFocus,
                .AccentHeight = 2
            }

            Dim lblSem As New Label With {
                .Text = "Semester:",
                .AutoSize = True,
                .Location = New Point(16, 18),
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .BackColor = Color.Transparent
            }

            cboSemester = New ComboBox With {
                .Location = New Point(85, 14),
                .Width = 190,
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White
            }

            Dim lblHostel As New Label With {
                .Text = "Hostel:",
                .AutoSize = True,
                .Location = New Point(290, 18),
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .BackColor = Color.Transparent
            }

            cboHostel = New ComboBox With {
                .Location = New Point(340, 14),
                .Width = 170,
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White
            }

            chkDefaultersOnly = New CheckBox With {
                .Text = "⚠️ Defaulters Only (Bal > 0)",
                .AutoSize = True,
                .Location = New Point(525, 16),
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.GlassStatusDanger,
                .Visible = False
            }

            cboMaintenanceStatus = New ComboBox With {
                .Location = New Point(525, 14),
                .Width = 150,
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Visible = False
            }
            cboMaintenanceStatus.Items.AddRange({"ALL", "REPORTED", "IN_PROGRESS", "RESOLVED"})
            cboMaintenanceStatus.SelectedIndex = 0

            cardFilter.Controls.AddRange({lblSem, cboSemester, lblHostel, cboHostel, chkDefaultersOnly, cboMaintenanceStatus})

            ' 2. Report Category Cards Row (Responsive 4-Card Grid)
            pnlCardsRow = New TableLayoutPanel With {
                .Dock = DockStyle.Top,
                .Height = 88,
                .ColumnCount = 4,
                .RowCount = 1,
                .BackColor = ThemeManager.CanvasDark,
                .Padding = New Padding(16, 4, 16, 4)
            }
            pnlCardsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlCardsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlCardsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlCardsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))

            cardOccupancy = CreateReportCard("🏢 Occupancy Matrix", "Room Capacities & Vacancies", InstitutionalReportGenerator.ReportKind.Occupancy)
            cardAllocations = CreateReportCard("📋 Active Allocations", "Resident Semester Schedule", InstitutionalReportGenerator.ReportKind.Allocations)
            cardFinancial = CreateReportCard("💳 Bursar Defaulters", "Fee Arrears & Balances Due", InstitutionalReportGenerator.ReportKind.FinancialDefaulters)
            cardMaintenance = CreateReportCard("🔧 Maintenance Logs", "Repair Logs & Defect Downtime", InstitutionalReportGenerator.ReportKind.Maintenance)

            pnlCardsRow.Controls.Add(cardOccupancy, 0, 0)
            pnlCardsRow.Controls.Add(cardAllocations, 1, 0)
            pnlCardsRow.Controls.Add(cardFinancial, 2, 0)
            pnlCardsRow.Controls.Add(cardMaintenance, 3, 0)

            ' 3. Metric Summary Strip Frosted Card
            cardSummaryStrip = New PrestigeGlassCard With {
                .Dock = DockStyle.Top,
                .Height = 44,
                .Margin = New Padding(16, 2, 16, 6),
                .Padding = New Padding(16, 6, 16, 6),
                .AccentColor = Color.Transparent,
                .AccentHeight = 0
            }

            lblMetric1 = New Label With {
                .AutoSize = True,
                .Location = New Point(18, 12),
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextChalkWhite,
                .BackColor = Color.Transparent
            }

            lblMetric2 = New Label With {
                .AutoSize = True,
                .Location = New Point(320, 12),
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.GlassStatusInfo,
                .BackColor = Color.Transparent
            }

            lblMetric3 = New Label With {
                .AutoSize = True,
                .Location = New Point(640, 12),
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.GlassStatusWarning,
                .BackColor = Color.Transparent
            }

            cardSummaryStrip.Controls.AddRange({lblMetric1, lblMetric2, lblMetric3})

            ' 4. Preview DataGridView
            dgvPreview = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvPreview)

            Dim pnlGridContainer As New Panel With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16, 0, 16, 8),
                .BackColor = Color.Transparent
            }
            pnlGridContainer.Controls.Add(dgvPreview)

            ' 5. Bottom Action Frosted Card
            cardBottomActions = New PrestigeGlassCard With {
                .Dock = DockStyle.Bottom,
                .Height = 56,
                .Margin = New Padding(16, 6, 16, 12),
                .Padding = New Padding(16, 8, 16, 8),
                .AccentColor = ThemeManager.GlassBorderGold,
                .AccentHeight = 2
            }

            btnPreviewReport = New Button With {
                .Text = "🖨️  Preview & Print Official Document",
                .BackColor = Color.FromArgb(30, 58, 138),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Size = New Size(240, 32),
                .Location = New Point(16, 11),
                .Cursor = Cursors.Hand
            }
            btnPreviewReport.FlatAppearance.BorderColor = Color.FromArgb(56, 189, 248)
            btnPreviewReport.FlatAppearance.BorderSize = 1

            btnExportHtml = New Button With {
                .Text = "🌐  Export Web / PDF Report",
                .BackColor = Color.FromArgb(16, 185, 129),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Size = New Size(210, 32),
                .Location = New Point(266, 11),
                .Cursor = Cursors.Hand
            }
            btnExportHtml.FlatAppearance.BorderSize = 0

            btnRefreshData = New Button With {
                .Text = "🔄  Refresh Data",
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = ThemeManager.TextPlatinum,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 8.5F),
                .Size = New Size(130, 32),
                .Location = New Point(486, 11),
                .Cursor = Cursors.Hand
            }
            btnRefreshData.FlatAppearance.BorderColor = ThemeManager.GlassBorder

            cardBottomActions.Controls.AddRange({btnPreviewReport, btnExportHtml, btnRefreshData})

            ' Add in correct layout hierarchy
            Controls.Add(pnlGridContainer)
            Controls.Add(cardSummaryStrip)
            Controls.Add(pnlCardsRow)
            Controls.Add(cardFilter)
            Controls.Add(cardBottomActions)

            Me.ResumeLayout(False)

            ' Events
            AddHandler btnPreviewReport.Click, AddressOf btnPreviewReport_Click
            AddHandler btnExportHtml.Click, AddressOf btnExportHtml_Click
            AddHandler btnRefreshData.Click, Sub() LoadActiveReportData()
            AddHandler cboSemester.SelectedIndexChanged, Sub() LoadActiveReportData()
            AddHandler cboHostel.SelectedIndexChanged, Sub() LoadActiveReportData()
            AddHandler chkDefaultersOnly.CheckedChanged, Sub() LoadActiveReportData()
            AddHandler cboMaintenanceStatus.SelectedIndexChanged, Sub() LoadActiveReportData()

            SelectReportKind(InstitutionalReportGenerator.ReportKind.Occupancy)
        End Sub

        Private Function CreateReportCard(title As String, subTitle As String, kind As InstitutionalReportGenerator.ReportKind) As PrestigeGlassCard
            Dim pnl As New PrestigeGlassCard With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4),
                .Cursor = Cursors.Hand,
                .Tag = kind,
                .AccentHeight = 3,
                .AccentColor = Color.Transparent
            }

            Dim lblT As New Label With {
                .Text = title,
                .Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextChalkWhite,
                .Location = New Point(12, 10),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            Dim lblS As New Label With {
                .Text = subTitle,
                .Font = New Font("Segoe UI", 8.0F, FontStyle.Regular),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(12, 34),
                .Size = New Size(210, 28),
                .BackColor = Color.Transparent
            }

            pnl.Controls.AddRange({lblT, lblS})

            Dim clickHandler As EventHandler = Sub(s, e) SelectReportKind(kind)
            AddHandler pnl.Click, clickHandler
            AddHandler lblT.Click, clickHandler
            AddHandler lblS.Click, clickHandler

            Return pnl
        End Function

        Private Sub SelectReportKind(kind As InstitutionalReportGenerator.ReportKind)
            _selectedKind = kind

            ' Highlight selected frosted tile
            Dim cards = {cardOccupancy, cardAllocations, cardFinancial, cardMaintenance}
            For Each c In cards
                Dim cKind = DirectCast(c.Tag, InstitutionalReportGenerator.ReportKind)
                If cKind = kind Then
                    c.FillColor = ThemeManager.GlassCardElevated
                    c.BorderColor = ThemeManager.GlassBorderFocus
                    c.AccentColor = ThemeManager.GlassStatusInfo
                Else
                    c.FillColor = ThemeManager.GlassCardFill
                    c.BorderColor = ThemeManager.GlassBorder
                    c.AccentColor = Color.Transparent
                End If
                c.Invalidate()
            Next

            ' Toggle filter controls
            chkDefaultersOnly.Visible = (kind = InstitutionalReportGenerator.ReportKind.FinancialDefaulters)
            cboMaintenanceStatus.Visible = (kind = InstitutionalReportGenerator.ReportKind.Maintenance)

            LoadActiveReportData()
        End Sub

        Public Sub LoadFilters()
            ' Load Semesters
            cboSemester.Items.Clear()
            cboSemester.Items.Add(New FilterItem(0, "All Semesters"))
            Dim semesters = _semesterRepo.GetSemesters()
            Dim selectedSemIndex As Integer = 0
            For i As Integer = 0 To semesters.Count - 1
                Dim s = semesters(i)
                Dim item As New FilterItem(s.SemesterId, s.SemesterName)
                cboSemester.Items.Add(item)
                If s.IsCurrent Then
                    selectedSemIndex = i + 1
                End If
            Next
            cboSemester.SelectedIndex = selectedSemIndex

            ' Load Hostels
            cboHostel.Items.Clear()
            cboHostel.Items.Add(New FilterItem(0, "All Hostels"))
            For Each h In _hostelRepo.GetAllHostels()
                cboHostel.Items.Add(New FilterItem(h.HostelId, $"{h.HostelName} ({h.HostelCode})"))
            Next
            cboHostel.SelectedIndex = 0
        End Sub

        Public Sub LoadActiveReportData()
            If cboSemester.Items.Count = 0 Then
                LoadFilters()
            End If

            Dim semId As Integer? = Nothing
            If cboSemester.SelectedItem IsNot Nothing Then
                Dim it = DirectCast(cboSemester.SelectedItem, FilterItem)
                If it.Id > 0 Then semId = it.Id
            End If

            Dim hostelId As Integer? = Nothing
            If cboHostel.SelectedItem IsNot Nothing Then
                Dim it = DirectCast(cboHostel.SelectedItem, FilterItem)
                If it.Id > 0 Then hostelId = it.Id
            End If

            dgvPreview.Columns.Clear()

            Select Case _selectedKind
                Case InstitutionalReportGenerator.ReportKind.Occupancy
                    Dim data = _reportRepo.GetOccupancyReportData(hostelId)
                    _cachedData = data
                    PopulateOccupancyGrid(data)

                    Dim totalCap = data.Sum(Function(x) x.Capacity)
                    Dim totalOcc = data.Sum(Function(x) x.OccupiedBeds)
                    Dim totalAvail = data.Sum(Function(x) x.AvailableBeds)
                    Dim occPct As Double = If(totalCap > 0, Math.Round((totalOcc / CDbl(totalCap)) * 100.0, 1), 0.0)

                    lblMetric1.Text = $"Total Rooms: {data.Count} | Bed Capacity: {totalCap:N0}"
                    lblMetric2.Text = $"Occupied Beds: {totalOcc:N0} ({occPct:F1}%)"
                    lblMetric3.Text = $"Available Vacancies: {totalAvail:N0}"

                Case InstitutionalReportGenerator.ReportKind.Allocations
                    Dim data = _reportRepo.GetAllocationReportData(semId, hostelId)
                    _cachedData = data
                    PopulateAllocationsGrid(data)

                    Dim totalBilled = data.Sum(Function(x) x.SemesterFee)
                    Dim totalPaid = data.Sum(Function(x) x.AmountPaid)
                    Dim totalBal = data.Sum(Function(x) x.BalanceDue)

                    lblMetric1.Text = $"Active Resident Allocations: {data.Count:N0}"
                    lblMetric2.Text = $"Billed Fees: KES {totalBilled:N0}"
                    lblMetric3.Text = $"Outstanding Arrears: KES {totalBal:N0}"

                Case InstitutionalReportGenerator.ReportKind.FinancialDefaulters
                    Dim defaultersOnly As Boolean = chkDefaultersOnly.Checked
                    Dim data = _reportRepo.GetFinancialDefaultersReportData(semId, defaultersOnly)
                    _cachedData = data
                    PopulateFinancialGrid(data)

                    Dim totalBilled = data.Sum(Function(x) x.SemesterFee)
                    Dim totalPaid = data.Sum(Function(x) x.TotalPaid)
                    Dim totalBal = data.Sum(Function(x) x.BalanceDue)
                    Dim colPct As Double = If(totalBilled > 0, Math.Round((totalPaid / CDbl(totalBilled)) * 100.0, 1), 0.0)

                    lblMetric1.Text = $"Total Invoiced: KES {totalBilled:N0}"
                    lblMetric2.Text = $"Total Collected: KES {totalPaid:N0} ({colPct:F1}%)"
                    lblMetric3.Text = $"Total Balances Due: KES {totalBal:N0}"

                Case InstitutionalReportGenerator.ReportKind.Maintenance
                    Dim statusFilter As String = If(cboMaintenanceStatus.SelectedItem IsNot Nothing, cboMaintenanceStatus.SelectedItem.ToString(), "ALL")
                    Dim data = _reportRepo.GetMaintenanceReportData(statusFilter)
                    _cachedData = data
                    PopulateMaintenanceGrid(data)

                    Dim reportedCount = data.Where(Function(x) x.MaintenanceStatus = "REPORTED").Count()
                    Dim inProgCount = data.Where(Function(x) x.MaintenanceStatus = "IN_PROGRESS").Count()
                    Dim resolvedCount = data.Where(Function(x) x.MaintenanceStatus = "RESOLVED").Count()

                    lblMetric1.Text = $"Total Incidents: {data.Count:N0}"
                    lblMetric2.Text = $"Pending: {reportedCount} • In Repair: {inProgCount}"
                    lblMetric3.Text = $"Resolved Issues: {resolvedCount}"
            End Select
        End Sub

        Private Sub PopulateOccupancyGrid(data As List(Of OccupancyReportItem))
            dgvPreview.Columns.Add("Hostel", "Hostel Facility")
            dgvPreview.Columns.Add("Room", "Room")
            dgvPreview.Columns.Add("Floor", "Floor")
            dgvPreview.Columns.Add("RoomType", "Room Type")
            dgvPreview.Columns.Add("Capacity", "Capacity")
            dgvPreview.Columns.Add("Occupied", "Occupied")
            dgvPreview.Columns.Add("Available", "Available")
            dgvPreview.Columns.Add("Status", "Status")
            dgvPreview.Columns.Add("Rate", "Occupancy %")

            For Each it In data
                dgvPreview.Rows.Add(
                    $"{it.HostelName} ({it.HostelCode})",
                    it.RoomNumber,
                    $"Floor {it.FloorNumber}",
                    it.RoomType,
                    it.Capacity,
                    it.OccupiedBeds,
                    it.AvailableBeds,
                    it.StatusText,
                    $"{it.OccupancyPercentage:F1}%"
                )
            Next
        End Sub

        Private Sub PopulateAllocationsGrid(data As List(Of AllocationReportItem))
            dgvPreview.Columns.Add("RegNo", "Registration No")
            dgvPreview.Columns.Add("Name", "Student Name")
            dgvPreview.Columns.Add("Course", "Course")
            dgvPreview.Columns.Add("Bed", "Bed Space")
            dgvPreview.Columns.Add("Fee", "Fee (KES)")
            dgvPreview.Columns.Add("Paid", "Paid (KES)")
            dgvPreview.Columns.Add("Balance", "Balance (KES)")
            dgvPreview.Columns.Add("CheckIn", "Check-In Date")
            dgvPreview.Columns.Add("Status", "Status")

            For Each it In data
                dgvPreview.Rows.Add(
                    it.RegistrationNumber,
                    it.StudentName,
                    it.Course,
                    $"{it.HostelCode}-{it.RoomNumber} ({it.BedLabel})",
                    it.SemesterFee.ToString("N0"),
                    it.AmountPaid.ToString("N0"),
                    it.BalanceDue.ToString("N0"),
                    If(it.CheckInDate.HasValue, it.CheckInDate.Value.ToString("yyyy-MM-dd"), "Pending"),
                    it.AllocationStatus
                )
            Next
        End Sub

        Private Sub PopulateFinancialGrid(data As List(Of FinancialReportItem))
            dgvPreview.Columns.Add("RegNo", "Registration No")
            dgvPreview.Columns.Add("Name", "Student Name")
            dgvPreview.Columns.Add("Phone", "Phone Number")
            dgvPreview.Columns.Add("Bed", "Bed Unit")
            dgvPreview.Columns.Add("Fee", "Invoiced (KES)")
            dgvPreview.Columns.Add("Paid", "Paid (KES)")
            dgvPreview.Columns.Add("Balance", "Balance Due (KES)")
            dgvPreview.Columns.Add("Status", "Clearance Status")
            dgvPreview.Columns.Add("CollectionRate", "Paid %")

            For Each it In data
                dgvPreview.Rows.Add(
                    it.RegistrationNumber,
                    it.StudentName,
                    it.PhoneNumber,
                    $"{it.HostelCode}-{it.RoomNumber} ({it.BedLabel})",
                    it.SemesterFee.ToString("N0"),
                    it.TotalPaid.ToString("N0"),
                    it.BalanceDue.ToString("N0"),
                    it.PaymentStatus,
                    $"{it.CollectionPercentage:F1}%"
                )
            Next
        End Sub

        Private Sub PopulateMaintenanceGrid(data As List(Of MaintenanceReportItem))
            dgvPreview.Columns.Add("Id", "Incident ID")
            dgvPreview.Columns.Add("Target", "Facility")
            dgvPreview.Columns.Add("Title", "Issue Title")
            dgvPreview.Columns.Add("Status", "Status")
            dgvPreview.Columns.Add("Technician", "Technician")
            dgvPreview.Columns.Add("Reported", "Reported Date")
            dgvPreview.Columns.Add("Resolved", "Resolved Date")
            dgvPreview.Columns.Add("Downtime", "Downtime")

            For Each it In data
                dgvPreview.Rows.Add(
                    $"#{it.MaintenanceId}",
                    it.TargetDescription,
                    it.IssueTitle,
                    it.MaintenanceStatus,
                    If(String.IsNullOrWhiteSpace(it.AssignedTo), "Unassigned", it.AssignedTo),
                    it.ReportedDate.ToString("yyyy-MM-dd"),
                    If(it.ResolvedDate.HasValue, it.ResolvedDate.Value.ToString("yyyy-MM-dd"), "-"),
                    $"{it.DowntimeDays} days"
                )
            Next
        End Sub

        Private Function BuildReportMetadata() As ReportMetadata
            Dim semText As String = If(cboSemester.SelectedItem IsNot Nothing, cboSemester.SelectedItem.ToString(), "Current Semester")
            Dim hostelText As String = If(cboHostel.SelectedItem IsNot Nothing, cboHostel.SelectedItem.ToString(), "All Hostels")

            Dim title As String = GetReportTitle(_selectedKind)
            Dim subTitle As String = GetReportSubtitle(_selectedKind)

            Dim totalCount As Integer = 0
            If _cachedData IsNot Nothing Then
                Dim col = TryCast(_cachedData, System.Collections.ICollection)
                If col IsNot Nothing Then totalCount = col.Count
            End If

            Return New ReportMetadata With {
                .ReportTitle = title,
                .Subtitle = subTitle,
                .GeneratedBy = "Directorate of Accommodation",
                .GeneratedAt = DateTime.Now,
                .SemesterName = semText,
                .FilterDescription = $"Scope: {hostelText}",
                .TotalRecords = totalCount
            }
        End Function

        Private Sub btnPreviewReport_Click(sender As Object, e As EventArgs)
            If _cachedData Is Nothing Then
                MessageBox.Show("Please load or refresh report data before previewing.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim meta = BuildReportMetadata()
            Dim generator As New InstitutionalReportGenerator(meta, _selectedKind, _cachedData)
            Using viewer As New frmReportViewerModal(generator, meta.ReportTitle)
                viewer.ShowDialog(Me.FindForm())
            End Using
        End Sub

        Private Sub btnExportHtml_Click(sender As Object, e As EventArgs)
            If _cachedData Is Nothing Then
                MessageBox.Show("Please load or refresh report data before exporting.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim meta = BuildReportMetadata()
            Dim generator As New InstitutionalReportGenerator(meta, _selectedKind, _cachedData)
            Dim html = generator.GenerateHtmlReport()

            Try
                Dim tempDir = Path.Combine(Path.GetTempPath(), "UniversityHostelReports")
                If Not Directory.Exists(tempDir) Then Directory.CreateDirectory(tempDir)

                Dim fileName = $"{meta.ReportTitle.Replace(" "c, "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.html"
                Dim filePath = Path.Combine(tempDir, fileName)
                File.WriteAllText(filePath, html)

                Dim psi As New ProcessStartInfo With {
                    .FileName = filePath,
                    .UseShellExecute = True
                }
                Process.Start(psi)
            Catch ex As Exception
                MessageBox.Show($"Export error: {ex.Message}", "Error Exporting Report", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function GetReportTitle(kind As InstitutionalReportGenerator.ReportKind) As String
            Select Case kind
                Case InstitutionalReportGenerator.ReportKind.Occupancy
                    Return "Hostel Facility Space Inventory & Occupancy Matrix"
                Case InstitutionalReportGenerator.ReportKind.Allocations
                    Return "Resident Bed Allocations & Tenancy Schedule"
                Case InstitutionalReportGenerator.ReportKind.FinancialDefaulters
                    Return "Bursar Student Accommodation Fee Collection & Arrears Schedule"
                Case InstitutionalReportGenerator.ReportKind.Maintenance
                    Return "Hostel Maintenance Incidents & Facility Downtime Register"
                Case Else
                    Return "Accommodation Directorate Institutional Report"
            End Select
        End Function

        Private Function GetReportSubtitle(kind As InstitutionalReportGenerator.ReportKind) As String
            Select Case kind
                Case InstitutionalReportGenerator.ReportKind.Occupancy
                    Return "Campus-Wide Bed Space Capacity, Resident Allocation Distribution & Vacancy Rates"
                Case InstitutionalReportGenerator.ReportKind.Allocations
                    Return "Official Semester Resident Allocations, Frozen Rates & Check-In Clearance Verification"
                Case InstitutionalReportGenerator.ReportKind.FinancialDefaulters
                    Return "Comprehensive Bursar Audit of Fee Obligations, Multi-Channel Collections & Student Arrears"
                Case InstitutionalReportGenerator.ReportKind.Maintenance
                    Return "Audit Log of Reported Property Defects, Technician Repairs & Bed-Space Maintenance Locks"
                Case Else
                    Return "Official Academic Record"
            End Select
        End Function

        Private Class FilterItem
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
