Imports System
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' High-level Executive Dashboard overview featuring 4 live KPI cards, hostel occupancy metrics, quick actions, and recent audit activity.
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

        ' UI Components
        Private pnlMetricsRow As TableLayoutPanel
        Private cardCapacity As Panel
        Private cardAvailable As Panel
        Private cardBilled As Panel
        Private cardCollected As Panel

        Private pnlSplitRow As TableLayoutPanel
        Private cardHostels As Panel
        Private lblHostelTitle As Label
        Private dgvHostels As DataGridView

        Private pnlRightCol As Panel
        Private cardQuickActions As Panel
        Private lblQuickTitle As Label
        Private btnQNewBed As Button
        Private btnQNewStudent As Button
        Private btnQNewRoom As Button
        Private btnQNewHostel As Button

        Private cardAudit As Panel
        Private lblAuditTitle As Label
        Private dgvAudit As DataGridView

        ' KPI Labels
        Private lblCapValue As Label
        Private lblCapSub As Label
        Private pgbOccupancy As ProgressBar

        Private lblAvailValue As Label
        Private lblAvailSub As Label

        Private lblBilledValue As Label
        Private lblBilledSub As Label

        Private lblCollectValue As Label
        Private lblCollectSub As Label

        Public Sub New()
            InitializeComponents()
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(dgvHostels)
            ThemeManager.EnableDoubleBuffering(dgvAudit)
        End Sub

        Private Sub InitializeComponents()
            Me.Dock = DockStyle.Fill
            Me.BackColor = ThemeManager.SurfaceCanvas
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            Me.AutoScroll = True

            ' =========================================================================
            ' 1. TOP KPI METRICS ROW (4 Cards)
            ' =========================================================================
            pnlMetricsRow = New TableLayoutPanel With {
                .Dock = DockStyle.Top,
                .Height = 120,
                .ColumnCount = 4,
                .RowCount = 1,
                .Padding = New Padding(0, 0, 0, 16)
            }
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))

            ' Card 1: Capacity & Occupancy
            cardCapacity = CreateKpiCard("TOTAL BED CAPACITY", ThemeManager.NavyPrimary)
            lblCapValue = CreateKpiValueLabel("0 Beds")
            lblCapSub = CreateKpiSubLabel("0% Occupancy (0 occupied)")
            pgbOccupancy = New ProgressBar With {
                .Location = New Point(16, 80),
                .Size = New Size(200, 8),
                .Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom,
                .Style = ProgressBarStyle.Continuous,
                .Value = 0
            }
            cardCapacity.Controls.AddRange({lblCapValue, lblCapSub, pgbOccupancy})

            ' Card 2: Available Beds
            cardAvailable = CreateKpiCard("AVAILABLE BED SPACES", ThemeManager.StatusSuccess)
            lblAvailValue = CreateKpiValueLabel("0 Free")
            lblAvailValue.ForeColor = ThemeManager.StatusSuccess
            lblAvailSub = CreateKpiSubLabel("Ready for immediate semester allocation")
            cardAvailable.Controls.AddRange({lblAvailValue, lblAvailSub})

            ' Card 3: Semester Billed
            cardBilled = CreateKpiCard("ACTIVE SEMESTER BILLED", ThemeManager.AcademicGold)
            lblBilledValue = CreateKpiValueLabel("KES 0.00")
            lblBilledValue.ForeColor = ThemeManager.AcademicGold
            lblBilledSub = CreateKpiSubLabel("0 active student allocations")
            cardBilled.Controls.AddRange({lblBilledValue, lblBilledSub})

            ' Card 4: Collections & Balance
            cardCollected = CreateKpiCard("FEE COLLECTIONS & PENDING", ThemeManager.StatusInfo)
            lblCollectValue = CreateKpiValueLabel("KES 0.00")
            lblCollectSub = CreateKpiSubLabel("Outstanding: KES 0.00")
            cardCollected.Controls.AddRange({lblCollectValue, lblCollectSub})

            pnlMetricsRow.Controls.Add(cardCapacity, 0, 0)
            pnlMetricsRow.Controls.Add(cardAvailable, 1, 0)
            pnlMetricsRow.Controls.Add(cardBilled, 2, 0)
            pnlMetricsRow.Controls.Add(cardCollected, 3, 0)

            ' =========================================================================
            ' 2. SPLIT ROW: HOSTELS (LEFT) + ACTIONS & AUDIT (RIGHT)
            ' =========================================================================
            pnlSplitRow = New TableLayoutPanel With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 2,
                .RowCount = 1,
                .Padding = New Padding(0)
            }
            pnlSplitRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 60.0F))
            pnlSplitRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 40.0F))

            ' --- Left Card: Hostels Table ---
            cardHostels = New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White,
                .Margin = New Padding(0, 0, 10, 0),
                .Padding = New Padding(16)
            }
            AddHandler cardHostels.Paint, Sub(s, e)
                                              Using pen As New Pen(ThemeManager.BorderSubtle, 1.0F)
                                                  e.Graphics.DrawRectangle(pen, 0, 0, cardHostels.Width - 1, cardHostels.Height - 1)
                                              End Using
                                          End Sub

            lblHostelTitle = New Label With {
                .Text = "Hostel Facility Occupancy & Bed Distribution",
                .Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextDeepSlate,
                .Dock = DockStyle.Top,
                .Height = 28,
                .UseMnemonic = False
            }

            dgvHostels = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyPrestigeGridStyle(dgvHostels)
            SetupHostelsGridColumns()

            cardHostels.Controls.Add(dgvHostels)
            cardHostels.Controls.Add(lblHostelTitle)

            ' --- Right Column: Quick Actions + Audit Log ---
            pnlRightCol = New Panel With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(10, 0, 0, 0)
            }

            ' Quick Actions Card
            cardQuickActions = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 160,
                .BackColor = Color.White,
                .Margin = New Padding(0, 0, 0, 16),
                .Padding = New Padding(16)
            }
            AddHandler cardQuickActions.Paint, Sub(s, e)
                                                   Using pen As New Pen(ThemeManager.BorderSubtle, 1.0F)
                                                       e.Graphics.DrawRectangle(pen, 0, 0, cardQuickActions.Width - 1, cardQuickActions.Height - 1)
                                                   End Using
                                               End Sub

            lblQuickTitle = New Label With {
                .Text = "Quick Accommodation Actions",
                .Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextDeepSlate,
                .Dock = DockStyle.Top,
                .Height = 28
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

            btnQNewBed = CreateActionButton("🛏️  Add Bed Space", ThemeManager.NavyDark)
            btnQNewStudent = CreateActionButton("🎓  Register Student", ThemeManager.NavyPrimary)
            btnQNewRoom = CreateActionButton("🚪  Add Hostel Room", Color.FromArgb(51, 65, 85))
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

            ' Audit Log Card
            cardAudit = New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White,
                .Padding = New Padding(16)
            }
            AddHandler cardAudit.Paint, Sub(s, e)
                                            Using pen As New Pen(ThemeManager.BorderSubtle, 1.0F)
                                                e.Graphics.DrawRectangle(pen, 0, 0, cardAudit.Width - 1, cardAudit.Height - 1)
                                            End Using
                                        End Sub

            lblAuditTitle = New Label With {
                .Text = "Recent Security & Operational Audit Log",
                .Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextDeepSlate,
                .Dock = DockStyle.Top,
                .Height = 28,
                .UseMnemonic = False
            }

            dgvAudit = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyPrestigeGridStyle(dgvAudit)
            SetupAuditGridColumns()

            cardAudit.Controls.Add(dgvAudit)
            cardAudit.Controls.Add(lblAuditTitle)

            ' Spacer panel between quick actions and audit
            Dim pnlSpacer As New Panel With {.Dock = DockStyle.Top, .Height = 14}

            pnlRightCol.Controls.Add(cardAudit)
            pnlRightCol.Controls.Add(pnlSpacer)
            pnlRightCol.Controls.Add(cardQuickActions)

            pnlSplitRow.Controls.Add(cardHostels, 0, 0)
            pnlSplitRow.Controls.Add(pnlRightCol, 1, 0)

            Me.Controls.Add(pnlSplitRow)
            Me.Controls.Add(pnlMetricsRow)
        End Sub

        Private Function CreateKpiCard(title As String, accentColor As Color) As Panel
            Dim pnl As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White,
                .Margin = New Padding(6, 0, 6, 0),
                .Padding = New Padding(16, 12, 16, 12)
            }

            ' Top 3px accent color bar
            Dim pnlTopAccent As New Panel With {
                .Dock = DockStyle.Top,
                .Height = 3,
                .BackColor = accentColor
            }

            Dim lblTitle As New Label With {
                .Text = title,
                .Font = New Font("Segoe UI Semibold", 8.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateMuted,
                .Dock = DockStyle.Top,
                .Height = 20,
                .UseMnemonic = False
            }

            pnl.Controls.Add(lblTitle)
            pnl.Controls.Add(pnlTopAccent)

            AddHandler pnl.Paint, Sub(s, e)
                                      Using pen As New Pen(ThemeManager.BorderSubtle, 1.0F)
                                          e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1)
                                      End Using
                                  End Sub
            Return pnl
        End Function

        Private Function CreateKpiValueLabel(defaultText As String) As Label
            Return New Label With {
                .Text = defaultText,
                .Font = New Font("Segoe UI", 16.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextDeepSlate,
                .Location = New Point(14, 34),
                .Size = New Size(220, 32),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right,
                .UseMnemonic = False
            }
        End Function

        Private Function CreateKpiSubLabel(defaultText As String) As Label
            Return New Label With {
                .Text = defaultText,
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Regular),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(16, 68),
                .Size = New Size(220, 18),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right,
                .UseMnemonic = False
            }
        End Function

        Private Function CreateActionButton(text As String, bgColor As Color) As Button
            Dim btn As New Button With {
                .Text = text,
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4),
                .BackColor = bgColor,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .Cursor = Cursors.Hand,
                .UseMnemonic = False
            }
            btn.FlatAppearance.BorderSize = 0
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
            dgvAudit.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Timestamp", .HeaderText = "Timestamp (UTC)", .Width = 135})
        End Sub

        ''' <summary>
        ''' Refreshes all metrics, tables and audit activities using real SQL Server data.
        ''' </summary>
        Public Sub LoadDashboardData(Optional semesterId As Integer? = Nothing)
            _currentSemesterId = semesterId

            Try
                ' 1. Load Aggregated KPIs
                Dim stats = _dashboardRepo.GetDashboardStatistics(_currentSemesterId)

                lblCapValue.Text = $"{stats.TotalBeds} Beds"
                lblCapSub.Text = $"{stats.OccupancyPercentage:F1}% Occupancy ({stats.OccupiedBeds} occupied)"
                pgbOccupancy.Value = Math.Max(0, Math.Min(100, CInt(Math.Round(stats.OccupancyPercentage))))

                lblAvailValue.Text = $"{stats.AvailableBeds} Free"
                lblAvailSub.Text = $"{stats.MaintenanceBeds} under maintenance • {stats.ReservedBeds} reserved"

                lblBilledValue.Text = $"KES {stats.TotalSemesterBilled:N2}"
                lblBilledSub.Text = $"{stats.ActiveAllocationsCount} active student allocations"

                lblCollectValue.Text = $"KES {stats.TotalPaymentsCollected:N2}"
                lblCollectSub.Text = $"Outstanding: KES {stats.TotalOutstandingBalance:N2}"

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

