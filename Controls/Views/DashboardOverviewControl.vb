Imports System
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports University_hostel_mgt.Controls
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' High-level Executive Dashboard overview featuring 4 live Frosted Glass KPI widgets,
    ''' hostel occupancy matrix, quick accommodation action buttons, and recent immutable audit activity.
    ''' Adheres strictly to Option B: Frosted Glassmorphism & Acrylic Design System.
    ''' </summary>
    Public Class DashboardOverviewControl
        Inherits UserControl

        Private ReadOnly _dashboardRepo As New DashboardRepository()
        Private _currentSemesterId As Integer? = Nothing

        ' Events for quick navigation
        Public Event RequestNavigation(targetView As String)
        Public Event RequestNewStudent()
        Public Event RequestNewBed()
        Public Event RequestNewRoom()
        Public Event RequestNewHostel()

        ' Top KPI Glass Widgets
        Private pnlMetricsRow As TableLayoutPanel
        Private kpiCapacity As GlassKpiWidget
        Private kpiAvailable As GlassKpiWidget
        Private kpiBilled As GlassKpiWidget
        Private kpiCollected As GlassKpiWidget

        ' Main Body Split Row
        Private pnlSplitRow As TableLayoutPanel
        Private cardHostels As PrestigeGlassCard
        Private lblHostelTitle As Label
        Private dgvHostels As DataGridView

        Private pnlRightCol As Panel
        Private cardQuickActions As PrestigeGlassCard
        Private lblQuickTitle As Label
        Private btnQNewBed As Button
        Private btnQNewStudent As Button
        Private btnQNewRoom As Button
        Private btnQNewHostel As Button

        Private cardAudit As PrestigeGlassCard
        Private lblAuditTitle As Label
        Private dgvAudit As DataGridView

        Public Sub New()
            InitializeComponents()
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(dgvHostels)
            ThemeManager.EnableDoubleBuffering(dgvAudit)
        End Sub

        Private Sub InitializeComponents()
            Me.Dock = DockStyle.Fill
            Me.BackColor = ThemeManager.CanvasDark
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            Me.AutoScroll = True
            Me.Padding = New Padding(20, 16, 20, 20)

            ' =========================================================================
            ' 1. TOP KPI METRICS ROW (4 Responsive Glass Widgets)
            ' =========================================================================
            pnlMetricsRow = New TableLayoutPanel With {
                .Dock = DockStyle.Top,
                .Height = 115,
                .ColumnCount = 4,
                .RowCount = 1,
                .BackColor = ThemeManager.CanvasDark,
                .Padding = New Padding(0, 0, 0, 14)
            }
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))

            ' Widget 1: Total Capacity & Occupancy Meter
            kpiCapacity = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(0, 0, 8, 0),
                .CategoryTitle = "TOTAL BED CAPACITY",
                .PrimaryValue = "0 Beds",
                .Subtitle = "0.0% Occupancy (0 occupied)",
                .AccentColor = ThemeManager.GlassStatusInfo,
                .ProgressPercentage = 0,
                .IconSymbol = "🛏️"
            }

            ' Widget 2: Available Beds Ready for Allocation
            kpiAvailable = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4, 0, 6, 0),
                .CategoryTitle = "AVAILABLE BED SPACES",
                .PrimaryValue = "0 Free",
                .Subtitle = "Ready for immediate semester allocation",
                .AccentColor = ThemeManager.GlassStatusSuccess,
                .ValueColor = ThemeManager.GlassStatusSuccess,
                .IconSymbol = "✨"
            }

            ' Widget 3: Active Semester Billed Fees
            kpiBilled = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(6, 0, 4, 0),
                .CategoryTitle = "ACTIVE SEMESTER BILLED",
                .PrimaryValue = "KES 0.00",
                .Subtitle = "0 active student allocations",
                .AccentColor = ThemeManager.GlassStatusWarning,
                .ValueColor = ThemeManager.GlassStatusWarning,
                .IconSymbol = "💳"
            }

            ' Widget 4: Fee Collections & Arrears
            kpiCollected = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(8, 0, 0, 0),
                .CategoryTitle = "FEE COLLECTIONS & PENDING",
                .PrimaryValue = "KES 0.00",
                .Subtitle = "Outstanding Arrears: KES 0.00",
                .AccentColor = ThemeManager.GlassStatusInfo,
                .ValueColor = ThemeManager.TextChalkWhite,
                .IconSymbol = "💰"
            }

            pnlMetricsRow.Controls.Add(kpiCapacity, 0, 0)
            pnlMetricsRow.Controls.Add(kpiAvailable, 1, 0)
            pnlMetricsRow.Controls.Add(kpiBilled, 2, 0)
            pnlMetricsRow.Controls.Add(kpiCollected, 3, 0)

            ' =========================================================================
            ' 2. MAIN SPLIT ROW: HOSTEL OCCUPANCY (LEFT) + QUICK ACTIONS & AUDIT (RIGHT)
            ' =========================================================================
            pnlSplitRow = New TableLayoutPanel With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 2,
                .RowCount = 1,
                .BackColor = ThemeManager.CanvasDark,
                .Padding = New Padding(0)
            }
            pnlSplitRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 58.0F))
            pnlSplitRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 42.0F))

            ' --- Left Card: Hostels Occupancy Table ---
            cardHostels = New PrestigeGlassCard With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(0, 0, 8, 0),
                .Padding = New Padding(16, 14, 16, 14),
                .AccentColor = ThemeManager.GlassStatusInfo,
                .AccentHeight = 3
            }

            lblHostelTitle = New Label With {
                .Text = "🏢  Hostel Facility Occupancy & Bed Distribution",
                .Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextPlatinum,
                .Dock = DockStyle.Top,
                .Height = 30,
                .UseMnemonic = False
            }

            dgvHostels = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvHostels)
            SetupHostelsGridColumns()

            cardHostels.Controls.Add(dgvHostels)
            cardHostels.Controls.Add(lblHostelTitle)

            ' --- Right Column: Quick Actions + Audit Activity ---
            pnlRightCol = New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = ThemeManager.CanvasDark,
                .Margin = New Padding(8, 0, 0, 0)
            }

            ' Quick Actions Frosted Card
            cardQuickActions = New PrestigeGlassCard With {
                .Dock = DockStyle.Top,
                .Height = 165,
                .Margin = New Padding(0, 0, 0, 14),
                .Padding = New Padding(16, 14, 16, 14),
                .AccentColor = ThemeManager.GlassBorderGold,
                .AccentHeight = 3
            }

            lblQuickTitle = New Label With {
                .Text = "⚡  Quick Accommodation Operations",
                .Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextPlatinum,
                .Dock = DockStyle.Top,
                .Height = 30
            }

            Dim pnlQuickButtons As New TableLayoutPanel With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 2,
                .RowCount = 2
            }
            pnlQuickButtons.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
            pnlQuickButtons.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
            pnlQuickButtons.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
            pnlQuickButtons.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))

            btnQNewBed = CreateActionButton("🛏️  Add Bed Space", Color.FromArgb(30, 58, 138))
            btnQNewStudent = CreateActionButton("🎓  Register Student", Color.FromArgb(16, 185, 129))
            btnQNewRoom = CreateActionButton("🚪  Add Hostel Room", Color.FromArgb(30, 62, 98))
            btnQNewHostel = CreateActionButton("🏢  Add Hostel Building", Color.FromArgb(71, 85, 105))

            AddHandler btnQNewBed.Click, Sub() RaiseEvent RequestNewBed()
            AddHandler btnQNewStudent.Click, Sub() RaiseEvent RequestNewStudent()
            AddHandler btnQNewRoom.Click, Sub() RaiseEvent RequestNewRoom()
            AddHandler btnQNewHostel.Click, Sub() RaiseEvent RequestNewHostel()

            pnlQuickButtons.Controls.Add(btnQNewBed, 0, 0)
            pnlQuickButtons.Controls.Add(btnQNewStudent, 1, 0)
            pnlQuickButtons.Controls.Add(btnQNewRoom, 0, 1)
            pnlQuickButtons.Controls.Add(btnQNewHostel, 1, 1)

            cardQuickActions.Controls.Add(pnlQuickButtons)
            cardQuickActions.Controls.Add(lblQuickTitle)

            ' Audit Log Frosted Card
            cardAudit = New PrestigeGlassCard With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16, 14, 16, 14),
                .AccentColor = Color.FromArgb(120, 56, 189, 248),
                .AccentHeight = 3
            }

            lblAuditTitle = New Label With {
                .Text = "📜  Recent System Activity Audit Trail",
                .Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextPlatinum,
                .Dock = DockStyle.Top,
                .Height = 30
            }

            dgvAudit = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvAudit)
            SetupAuditGridColumns()

            cardAudit.Controls.Add(dgvAudit)
            cardAudit.Controls.Add(lblAuditTitle)

            pnlRightCol.Controls.Add(cardAudit)
            pnlRightCol.Controls.Add(cardQuickActions)

            pnlSplitRow.Controls.Add(cardHostels, 0, 0)
            pnlSplitRow.Controls.Add(pnlRightCol, 1, 0)

            Me.Controls.Add(pnlSplitRow)
            Me.Controls.Add(pnlMetricsRow)
        End Sub

        Private Function CreateActionButton(text As String, bgColor As Color) As Button
            Dim darkR As Integer = CInt(14 * 0.7F + bgColor.R * 0.3F)
            Dim darkG As Integer = CInt(22 * 0.7F + bgColor.G * 0.3F)
            Dim darkB As Integer = CInt(36 * 0.7F + bgColor.B * 0.3F)

            Dim btn As New Button With {
                .Text = text,
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4),
                .BackColor = Color.FromArgb(darkR, darkG, darkB),
                .ForeColor = ThemeManager.TextChalkWhite,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .Cursor = Cursors.Hand,
                .UseMnemonic = False
            }
            btn.FlatAppearance.BorderColor = Color.FromArgb(Math.Min(255, darkR + 40), Math.Min(255, darkG + 40), Math.Min(255, darkB + 50))
            btn.FlatAppearance.BorderSize = 1
            Return btn
        End Function

        Private Sub SetupHostelsGridColumns()
            dgvHostels.Columns.Clear()
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "HostelCode", .HeaderText = "Code", .Width = 70})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "HostelName", .HeaderText = "Hostel Facility", .Width = 140, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Gender", .HeaderText = "Gender", .Width = 80})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TotalBeds", .HeaderText = "Total Beds", .Width = 80, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Occupied", .HeaderText = "Occupied", .Width = 80, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Available", .HeaderText = "Available", .Width = 80, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "OccupancyRate", .HeaderText = "Occupancy %", .Width = 100, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
        End Sub

        Private Sub SetupAuditGridColumns()
            dgvAudit.Columns.Clear()
            dgvAudit.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ActionType", .HeaderText = "Action", .Width = 95})
            dgvAudit.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Description", .HeaderText = "Activity Detail", .Width = 180, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            dgvAudit.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Timestamp", .HeaderText = "Timestamp", .Width = 135})
        End Sub

        ''' <summary>
        ''' Refreshes all metrics, tables and audit activities using real SQL Server data.
        ''' </summary>
        Public Sub LoadDashboardData(Optional semesterId As Integer? = Nothing)
            _currentSemesterId = semesterId

            Try
                ' 1. Load Aggregated KPIs
                Dim stats = _dashboardRepo.GetDashboardStatistics(_currentSemesterId)

                kpiCapacity.PrimaryValue = $"{stats.TotalBeds} Beds"
                kpiCapacity.Subtitle = $"{stats.OccupancyPercentage:F1}% Occupancy ({stats.OccupiedBeds} occupied)"
                kpiCapacity.ProgressPercentage = CSng(stats.OccupancyPercentage)

                kpiAvailable.PrimaryValue = $"{stats.AvailableBeds} Free"
                kpiAvailable.Subtitle = $"{stats.MaintenanceBeds} maintenance • {stats.ReservedBeds} reserved"

                kpiBilled.PrimaryValue = $"KES {stats.TotalSemesterBilled:N2}"
                kpiBilled.Subtitle = $"{stats.ActiveAllocationsCount} active student allocations"

                kpiCollected.PrimaryValue = $"KES {stats.TotalPaymentsCollected:N2}"
                kpiCollected.Subtitle = $"Arrears Due: KES {stats.TotalOutstandingBalance:N2}"

                ' 2. Load Hostel Occupancy List
                Dim dtHostels = _dashboardRepo.GetHostelOccupancyList()
                dgvHostels.Rows.Clear()
                For Each row As DataRow In dtHostels.Rows
                    Dim occRate As Decimal = Convert.ToDecimal(row("occupancy_rate"))
                    dgvHostels.Rows.Add(
                        row("hostel_code").ToString(),
                        row("hostel_name").ToString(),
                        row("gender_category").ToString(),
                        row("total_beds").ToString(),
                        row("occupied_beds").ToString(),
                        row("available_beds").ToString(),
                        $"{occRate:F1}%"
                    )
                Next

                ' 3. Load Recent Audit Activity
                Dim dtAudit = _dashboardRepo.GetRecentActivities(10)
                dgvAudit.Rows.Clear()
                For Each row As DataRow In dtAudit.Rows
                    Dim ts As DateTime = Convert.ToDateTime(row("action_timestamp"))
                    dgvAudit.Rows.Add(
                        row("action_type").ToString(),
                        row("description").ToString(),
                        ts.ToString("yyyy-MM-dd HH:mm:ss")
                    )
                Next
            Catch ex As Exception
                MessageBox.Show($"Failed to refresh dashboard data: {ex.Message}", "Database Query Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
    End Class
End Namespace
