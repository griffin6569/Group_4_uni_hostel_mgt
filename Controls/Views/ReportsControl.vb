Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Reports
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Controls.Views
    ''' <summary>
    ''' Executive Institutional Reporting & Analytics Control.
    ''' Provides report selection, dynamic scope filtering, data grid previews,
    ''' and integration with the printing and web document export engines.
    ''' </summary>
    Public Class ReportsControl
        Inherits UserControl

        Private ReadOnly _reportRepo As New ReportRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private ReadOnly _semesterRepo As New SemesterRepository()

        Private pnlFilter As Panel
        Private cboSemester As ComboBox
        Private cboHostel As ComboBox
        Private chkDefaultersOnly As CheckBox
        Private cboMaintenanceStatus As ComboBox
        Private lblFilterInfo As Label

        Private pnlCards As Panel
        Private cardOccupancy As Panel
        Private cardAllocations As Panel
        Private cardFinancial As Panel
        Private cardMaintenance As Panel

        Private pnlSummaryStrip As Panel
        Private lblMetric1 As Label
        Private lblMetric2 As Label
        Private lblMetric3 As Label

        Private dgvPreview As DataGridView
        Private pnlBottomActions As Panel
        Private btnPreviewReport As Button
        Private btnExportHtml As Button
        Private btnRefreshData As Button

        Private _selectedKind As InstitutionalReportGenerator.ReportKind = InstitutionalReportGenerator.ReportKind.Occupancy
        Private _cachedData As Object = Nothing

        Public Sub New()
            InitializeUI()
            Dock = DockStyle.Fill
            BackColor = Color.FromArgb(248, 250, 252)
        End Sub

        Private Sub InitializeUI()
            Me.SuspendLayout()

            ' 1. Top Filter Panel
            pnlFilter = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 56,
                .BackColor = Color.White,
                .Padding = New Padding(16, 10, 16, 10)
            }

            Dim lblSem As New Label With {.Text = "Semester:", .AutoSize = True, .Location = New Point(16, 18), .Font = New Font("Segoe UI Semibold", 9.0F)}
            cboSemester = New ComboBox With {.Location = New Point(85, 15), .Width = 190, .DropDownStyle = ComboBoxStyle.DropDownList}

            Dim lblHostel As New Label With {.Text = "Hostel:", .AutoSize = True, .Location = New Point(295, 18), .Font = New Font("Segoe UI Semibold", 9.0F)}
            cboHostel = New ComboBox With {.Location = New Point(345, 15), .Width = 170, .DropDownStyle = ComboBoxStyle.DropDownList}

            chkDefaultersOnly = New CheckBox With {
                .Text = "⚠️ Defaulters Only (Bal > 0)",
                .AutoSize = True,
                .Location = New Point(535, 18),
                .Font = New Font("Segoe UI Semibold", 8.5F),
                .ForeColor = Color.FromArgb(185, 28, 28),
                .Visible = False
            }

            cboMaintenanceStatus = New ComboBox With {
                .Location = New Point(535, 15),
                .Width = 150,
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Visible = False
            }
            cboMaintenanceStatus.Items.AddRange({"ALL", "REPORTED", "IN_PROGRESS", "RESOLVED"})
            cboMaintenanceStatus.SelectedIndex = 0

            pnlFilter.Controls.AddRange({lblSem, cboSemester, lblHostel, cboHostel, chkDefaultersOnly, cboMaintenanceStatus})

            ' 2. Report Category Cards Strip
            pnlCards = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 100,
                .BackColor = Color.FromArgb(248, 250, 252),
                .Padding = New Padding(16, 8, 16, 8)
            }

            cardOccupancy = CreateReportCard("🏢 Occupancy Matrix", "Room Capacities & Vacancies", InstitutionalReportGenerator.ReportKind.Occupancy)
            cardAllocations = CreateReportCard("📋 Active Allocations", "Current Resident Schedule", InstitutionalReportGenerator.ReportKind.Allocations)
            cardFinancial = CreateReportCard("💳 Bursar Defaulters", "Fee Arrears & Balances", InstitutionalReportGenerator.ReportKind.FinancialDefaulters)
            cardMaintenance = CreateReportCard("🔧 Maintenance Logs", "Repair Logs & Downtime", InstitutionalReportGenerator.ReportKind.Maintenance)

            cardOccupancy.Location = New Point(16, 10)
            cardAllocations.Location = New Point(266, 10)
            cardFinancial.Location = New Point(516, 10)
            cardMaintenance.Location = New Point(766, 10)

            pnlCards.Controls.AddRange({cardOccupancy, cardAllocations, cardFinancial, cardMaintenance})

            ' 3. Metric Summary Strip
            pnlSummaryStrip = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 36,
                .BackColor = Color.FromArgb(241, 245, 249),
                .Padding = New Padding(16, 8, 16, 8)
            }
            lblMetric1 = New Label With {.AutoSize = True, .Location = New Point(20, 9), .Font = New Font("Segoe UI Semibold", 9.0F), .ForeColor = ThemeManager.NavyPrimary}
            lblMetric2 = New Label With {.AutoSize = True, .Location = New Point(300, 9), .Font = New Font("Segoe UI Semibold", 9.0F), .ForeColor = Color.FromArgb(15, 118, 110)}
            lblMetric3 = New Label With {.AutoSize = True, .Location = New Point(600, 9), .Font = New Font("Segoe UI Semibold", 9.0F), .ForeColor = Color.FromArgb(180, 83, 9)}
            pnlSummaryStrip.Controls.AddRange({lblMetric1, lblMetric2, lblMetric3})

            ' 4. Preview DataGridView
            dgvPreview = New DataGridView With {
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
            ThemeManager.ApplyPrestigeGridStyle(dgvPreview)

            ' 5. Bottom Action Bar
            pnlBottomActions = New Panel With {
                .Dock = DockStyle.Bottom,
                .Height = 54,
                .BackColor = Color.White,
                .Padding = New Padding(16, 10, 16, 10)
            }

            btnPreviewReport = New Button With {
                .Text = "🖨️ Preview & Print Document",
                .BackColor = ThemeManager.NavyPrimary,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Size = New Size(210, 34),
                .Location = New Point(16, 10)
            }
            btnPreviewReport.FlatAppearance.BorderSize = 0

            btnExportHtml = New Button With {
                .Text = "🌐 Export Web / PDF Report",
                .BackColor = Color.FromArgb(15, 118, 110),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Size = New Size(200, 34),
                .Location = New Point(236, 10)
            }
            btnExportHtml.FlatAppearance.BorderSize = 0

            btnRefreshData = New Button With {
                .Text = "🔄 Refresh Data",
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.NavyPrimary,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 9.0F),
                .Size = New Size(120, 34),
                .Location = New Point(446, 10)
            }
            btnRefreshData.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)

            pnlBottomActions.Controls.AddRange({btnPreviewReport, btnExportHtml, btnRefreshData})

            ' Add in correct z-order
            Controls.Add(dgvPreview)
            Controls.Add(pnlSummaryStrip)
            Controls.Add(pnlCards)
            Controls.Add(pnlFilter)
            Controls.Add(pnlBottomActions)

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

        Private Function CreateReportCard(title As String, subTitle As String, kind As InstitutionalReportGenerator.ReportKind) As Panel
            Dim pnl As New Panel With {
                .Size = New Size(240, 78),
                .BackColor = Color.White,
                .Cursor = Cursors.Hand,
                .Tag = kind
            }

            Dim lblT As New Label With {
                .Text = title,
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.NavyPrimary,
                .Location = New Point(12, 12),
                .AutoSize = True
            }

            Dim lblS As New Label With {
                .Text = subTitle,
                .Font = New Font("Segoe UI", 8.0F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(100, 116, 139),
                .Location = New Point(12, 36),
                .Size = New Size(215, 30)
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

            ' Highlight selected card
            Dim cards = {cardOccupancy, cardAllocations, cardFinancial, cardMaintenance}
            For Each c In cards
                Dim cKind = DirectCast(c.Tag, InstitutionalReportGenerator.ReportKind)
                If cKind = kind Then
                    c.BackColor = Color.FromArgb(238, 242, 255)
                    c.BorderStyle = BorderStyle.FixedSingle
                Else
                    c.BackColor = Color.White
                    c.BorderStyle = BorderStyle.None
                End If
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

                    lblMetric1.Text = $"Active Allocations: {data.Count:N0}"
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
                    lblMetric2.Text = $"Active Issues: {reportedCount + inProgCount} ({reportedCount} Reported, {inProgCount} In Progress)"
                    lblMetric3.Text = $"Resolved Repairs: {resolvedCount}"
            End Select
        End Sub

        Private Sub PopulateOccupancyGrid(data As List(Of OccupancyReportItem))
            dgvPreview.Columns.Add("Hostel", "Hostel Facility")
            dgvPreview.Columns.Add("Room", "Room Number")
            dgvPreview.Columns.Add("Floor", "Floor")
            dgvPreview.Columns.Add("Type", "Room Type")
            dgvPreview.Columns.Add("Capacity", "Capacity")
            dgvPreview.Columns.Add("Occupied", "Occupied")
            dgvPreview.Columns.Add("Available", "Available")
            dgvPreview.Columns.Add("Status", "Status")
            dgvPreview.Columns.Add("OccupancyRate", "Occupancy %")

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
            dgvPreview.Columns.Add("Status", "Status")
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
                    it.AssignedTo,
                    it.ReportedDate.ToString("yyyy-MM-dd"),
                    If(it.ResolvedDate.HasValue, it.ResolvedDate.Value.ToString("yyyy-MM-dd"), "-"),
                    $"{it.DowntimeDays} days"
                )
            Next
        End Sub

        Private Function BuildReportMetadata() As ReportMetadata
            Dim semText As String = If(cboSemester.SelectedItem IsNot Nothing, cboSemester.SelectedItem.ToString(), "Current Semester")
            Dim hostelText As String = If(cboHostel.SelectedItem IsNot Nothing, cboHostel.SelectedItem.ToString(), "All Hostels")

            Dim title As String = "INSTITUTIONAL ACCOMMODATION REPORT"
            Dim subTitle As String = "Hostel Operations Record"

            Select Case _selectedKind
                Case InstitutionalReportGenerator.ReportKind.Occupancy
                    title = "Hostel Room Occupancy & Capacity Matrix"
                    subTitle = "Floor-by-Floor Space Inventory & Vacancy Rates"
                Case InstitutionalReportGenerator.ReportKind.Allocations
                    title = "Active Semester Bed Allocations Schedule"
                    subTitle = "Certified Resident Allocations & Check-In Clearances"
                Case InstitutionalReportGenerator.ReportKind.FinancialDefaulters
                    title = "Bursar Revenue & Fee Defaulters Schedule"
                    subTitle = "Semester Billing, Collections & Outstanding Arrears"
                Case InstitutionalReportGenerator.ReportKind.Maintenance
                    title = "Hostel Maintenance & Facility Downtime Audit"
                    subTitle = "Incident Management, Equipment Repairs & Downtime Tracking"
            End Select

            Dim totalCount As Integer = 0
            If _cachedData IsNot Nothing Then
                Dim col = TryCast(_cachedData, System.Collections.ICollection)
                If col IsNot Nothing Then totalCount = col.Count
            End If

            Return New ReportMetadata With {
                .ReportTitle = title,
                .Subtitle = subTitle,
                .GeneratedBy = "Chief Accommodation Officer",
                .GeneratedAt = DateTime.Now,
                .SemesterName = semText,
                .FilterDescription = $"Scope: {hostelText}",
                .TotalRecords = totalCount
            }
        End Function

        Private Sub btnPreviewReport_Click(sender As Object, e As EventArgs)
            If _cachedData Is Nothing Then
                MessageBox.Show("No data available to preview.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim meta = BuildReportMetadata()
            Dim generator As New InstitutionalReportGenerator(meta, _selectedKind, _cachedData)
            Using frm As New frmReportViewerModal(generator, meta.ReportTitle)
                frm.ShowDialog(Me.FindForm())
            End Using
        End Sub

        Private Sub btnExportHtml_Click(sender As Object, e As EventArgs)
            If _cachedData Is Nothing Then
                MessageBox.Show("No data available to export.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
