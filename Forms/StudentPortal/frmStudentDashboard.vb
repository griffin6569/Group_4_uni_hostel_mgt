Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports University_hostel_mgt.Controls
Imports University_hostel_mgt.Data
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Forms.StudentPortal
    ''' <summary>
    ''' Ultra-modern, responsive student self-service accommodation dashboard.
    ''' Allows university students to view residency details, browse campus rooms,
    ''' book available beds, settle fees with M-Pesa, and report maintenance issues.
    ''' </summary>
    Public Class frmStudentDashboard
        Private ReadOnly _currentUser As User
        Private ReadOnly _studentRepo As New Data.Repositories.StudentRepository()
        Private ReadOnly _portalService As New StudentPortalService()

        Private _studentProfile As Student
        Private _currentAllocation As StudentAllocationDetail
        Private _isDragging As Boolean = False
        Private _dragStartPoint As Point

        Private _tabPanels As List(Of Panel)
        Private _navButtons As List(Of GlassButton)

        Public Sub New(currentUser As User)
            InitializeComponent()
            _currentUser = currentUser
            _navButtons = New List(Of GlassButton) From {btnNavMyRoom, btnNavBrowse, btnNavBilling, btnNavMaintenance}
            _tabPanels = New List(Of Panel) From {pnlTabMyRoom, pnlTabBrowse, pnlTabBilling, pnlTabMaintenance}
        End Sub

        Protected Overrides Async Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlContent)
            ThemeManager.EnableDoubleBuffering(pnlHeader)
            ThemeManager.EnableDoubleBuffering(pnlNavBar)

            ' Make form responsive to user screen resolution
            Dim workArea = Screen.FromControl(Me).WorkingArea
            Dim targetW As Integer = Math.Min(1200, Math.Max(960, CInt(workArea.Width * 0.9F)))
            Dim targetH As Integer = Math.Min(780, Math.Max(580, CInt(workArea.Height * 0.88F)))
            Me.Size = New Size(targetW, targetH)
            Me.CenterToScreen()
            Me.MinimumSize = New Size(920, 560)

            picLogo.Image = ThemeManager.GetInstitutionalLogo()

            ' Fetch student profile
            Dim studentId = _currentUser.StudentId.GetValueOrDefault(0)
            If studentId > 0 Then
                _studentProfile = _studentRepo.GetStudentById(studentId)
            End If

            Dim displayName = If(_studentProfile IsNot Nothing, $"{_studentProfile.FullName} • {_studentProfile.RegistrationNumber}", _currentUser.FullName)
            lblStudentPill.Text = $"👤 {displayName}"

            ' Load data and build UI tabs
            Await RefreshAllStudentDataAsync()
            SwitchTab(0)
        End Sub

        Private Async Function RefreshAllStudentDataAsync() As Task
            Dim studentId = _currentUser.StudentId.GetValueOrDefault(0)
            _currentAllocation = Await _portalService.GetStudentCurrentAllocationAsync(studentId)

            BuildMyRoomTab()
            Await BuildBrowseUnitsTabAsync()
            Await BuildBillingTabAsync()
            Await BuildMaintenanceTabAsync()
        End Function

        ' =====================================================================
        ' TAB 1: MY ROOM & RESIDENCY STATUS
        ' =====================================================================
        Private Sub BuildMyRoomTab()
            pnlTabMyRoom.Controls.Clear()

            If _currentAllocation Is Nothing Then
                ' Responsive Not Allocated Container
                Dim pnlEmptyOuter As New Panel() With {
                    .Dock = DockStyle.Fill,
                    .AutoScroll = True,
                    .Padding = New Padding(4)
                }

                ' 1. Hero Card
                Dim pnlHero As New GlassPanel() With {
                    .Dock = DockStyle.Top,
                    .Height = 195,
                    .CornerRadius = 16,
                    .FillColor = Color.FromArgb(210, 15, 23, 38),
                    .BorderColor = Color.FromArgb(45, 255, 255, 255),
                    .Padding = New Padding(28),
                    .Margin = New Padding(0, 0, 0, 16)
                }

                Dim badgeUnallocated As New Label() With {
                    .Text = "● ACCOMMODATION STATUS: UNALLOCATED — 2025/2026 ACADEMIC YEAR (SEMESTER 2)",
                    .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                    .ForeColor = ThemeManager.AcademicGold,
                    .BackColor = Color.FromArgb(30, 245, 158, 11),
                    .Padding = New Padding(8, 4, 8, 4),
                    .AutoSize = True,
                    .Location = New Point(28, 20)
                }

                Dim lblHeroTitle As New Label() With {
                    .Text = "Ready to Reserve Your Hostel Bed Space?",
                    .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
                    .ForeColor = Color.White,
                    .AutoSize = True,
                    .Location = New Point(28, 54)
                }

                Dim lblHeroDesc As New Label() With {
                    .Text = "You currently do not hold an active room reservation. Browse verified university residences," & vbCrLf &
                            "select an eligible bed space with preferred amenities, and confirm your accommodation instantly.",
                    .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular),
                    .ForeColor = Color.FromArgb(148, 163, 184),
                    .Size = New Size(720, 38),
                    .Location = New Point(28, 92)
                }

                Dim btnGoBrowse As New GlassButton() With {
                    .Text = "🛏 Explore Available Hostels & Choose Bed Unit ➔",
                    .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                    .PrimaryColor = ThemeManager.EmeraldPrimary,
                    .CustomTextColor = Color.FromArgb(7, 10, 14),
                    .CornerRadius = 10,
                    .Size = New Size(390, 42),
                    .Location = New Point(28, 134)
                }
                AddHandler btnGoBrowse.Click, Sub() SwitchTab(1)

                pnlHero.Controls.AddRange({badgeUnallocated, lblHeroTitle, lblHeroDesc, btnGoBrowse})

                ' 2. Three-Step Guidance Matrix (Stretches across all screen sizes)
                Dim pnlStepsGrid As New TableLayoutPanel() With {
                    .Dock = DockStyle.Top,
                    .Height = 165,
                    .ColumnCount = 3,
                    .RowCount = 1,
                    .Margin = New Padding(0, 16, 0, 16)
                }
                pnlStepsGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
                pnlStepsGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
                pnlStepsGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.34F))

                pnlStepsGrid.Controls.Add(CreateStepCard("STEP 1", "🏢 Explore Residences", "View real-time room capacity, floor plans, and gender-segregated wings across campus."), 0, 0)
                pnlStepsGrid.Controls.Add(CreateStepCard("STEP 2", "🛏 Select Bed Unit", "Inspect bed availability (Bed A, Bed B), study desks, lockable wardrobes, and window ventilation."), 1, 0)
                pnlStepsGrid.Controls.Add(CreateStepCard("STEP 3", "💳 Instant Clearance", "Confirm reservation and settle fees via M-Pesa or Equity Bank with instant official receipt generation."), 2, 0)

                ' 3. Directorate Support Banner
                Dim pnlSupportBar As New GlassPanel() With {
                    .Dock = DockStyle.Top,
                    .Height = 72,
                    .CornerRadius = 12,
                    .FillColor = Color.FromArgb(160, 13, 20, 32),
                    .BorderColor = Color.FromArgb(30, 255, 255, 255),
                    .Padding = New Padding(20, 14, 20, 14),
                    .Margin = New Padding(0, 16, 0, 0)
                }

                Dim lblSupportInfo As New Label() With {
                    .Text = "ℹ Accommodation Directorate: Directorate Office Block, Ground Floor • Hours: Mon–Fri 8:00 AM – 5:00 PM • Ext. 2040 / 2041",
                    .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                    .ForeColor = Color.FromArgb(203, 213, 225),
                    .AutoSize = True,
                    .Location = New Point(20, 26)
                }

                Dim btnContactDesk As New GlassButton() With {
                    .Text = "🔧 Support Desk",
                    .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                    .PrimaryColor = Color.FromArgb(25, 255, 255, 255),
                    .CustomTextColor = Color.FromArgb(203, 213, 225),
                    .CornerRadius = 8,
                    .Size = New Size(140, 32),
                    .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                    .Location = New Point(Math.Max(500, pnlSupportBar.ClientSize.Width - 160), 20)
                }
                AddHandler btnContactDesk.Click, Sub() SwitchTab(3)
                AddHandler pnlSupportBar.Resize, Sub()
                    btnContactDesk.Location = New Point(Math.Max(500, pnlSupportBar.ClientSize.Width - 160), 20)
                End Sub

                pnlSupportBar.Controls.AddRange({lblSupportInfo, btnContactDesk})

                pnlEmptyOuter.Controls.AddRange({pnlSupportBar, pnlStepsGrid, pnlHero})
                pnlTabMyRoom.Controls.Add(pnlEmptyOuter)
                Return
            End If

            ' Active Allocation State (Two cards)
            Dim pnlSplit As New TableLayoutPanel() With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 2,
                .RowCount = 1
            }
            pnlSplit.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 55.0F))
            pnlSplit.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45.0F))

            ' Left Card: Room Details & Amenities
            Dim pnlLeft As New GlassPanel() With {
                .Dock = DockStyle.Fill,
                .CornerRadius = 16,
                .FillColor = Color.FromArgb(200, 15, 23, 38),
                .BorderColor = Color.FromArgb(40, 255, 255, 255),
                .Padding = New Padding(24),
                .Margin = New Padding(0, 0, 10, 0)
            }

            Dim lblCardTitle As New Label() With {
                .Text = "ASSIGNED RESIDENCE UNIT",
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.EmeraldPrimary,
                .AutoSize = True,
                .Location = New Point(24, 20)
            }

            Dim lblUnitHeader As New Label() With {
                .Text = $"{_currentAllocation.HostelName} • Room {_currentAllocation.RoomNumber}",
                .Font = New Font("Segoe UI", 15.0F, FontStyle.Bold),
                .ForeColor = Color.White,
                .AutoSize = True,
                .Location = New Point(24, 42)
            }

            Dim badgeStatus As New Label() With {
                .Text = $"STATUS: {_currentAllocation.AllocationStatus.ToUpperInvariant()}",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.EmeraldPrimary,
                .BackColor = Color.FromArgb(25, 16, 185, 129),
                .Padding = New Padding(8, 4, 8, 4),
                .AutoSize = True,
                .Location = New Point(24, 76)
            }

            Dim lblBedInfo As New Label() With {
                .Text = $"Assigned Bed: Bed {_currentAllocation.BedNumber}  •  Floor {_currentAllocation.FloorNumber}  •  {_currentAllocation.RoomType}",
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(203, 213, 225),
                .AutoSize = True,
                .Location = New Point(24, 114)
            }

            ' Amenities list
            Dim lblAmenitiesTitle As New Label() With {
                .Text = "ROOM AMENITIES & ASSETS:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(148, 163, 184),
                .AutoSize = True,
                .Location = New Point(24, 150)
            }

            Dim deskCheck = If(_currentAllocation.HasDesk, "✓ Study Desk Assigned", "— Study Desk N/A")
            Dim lockerCheck = If(_currentAllocation.HasLocker, "✓ Lockable Wardrobe", "— Wardrobe N/A")
            Dim windowCheck = If(_currentAllocation.HasWindow, "✓ Natural Window View", "— Window N/A")
            Dim ventCheck = If(_currentAllocation.HasVentilation, "✓ Fresh Air Ventilation", "— Ventilation N/A")

            Dim lblAmenitiesText As New Label() With {
                .Text = $"• {deskCheck}" & vbCrLf & $"• {lockerCheck}" & vbCrLf & $"• {windowCheck}" & vbCrLf & $"• {ventCheck}",
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(203, 213, 225),
                .Size = New Size(400, 80),
                .Location = New Point(24, 172)
            }

            ' Action buttons
            Dim btnReportIssue As New GlassButton() With {
                .Text = "🔧 Report Defect / Request Repair",
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .PrimaryColor = Color.FromArgb(25, 255, 255, 255),
                .CustomTextColor = Color.FromArgb(248, 250, 252),
                .CornerRadius = 10,
                .Size = New Size(240, 36),
                .Location = New Point(24, 270)
            }
            AddHandler btnReportIssue.Click, Sub() OpenStudentIssueModal()

            pnlLeft.Controls.AddRange({lblCardTitle, lblUnitHeader, badgeStatus, lblBedInfo, lblAmenitiesTitle, lblAmenitiesText, btnReportIssue})

            ' Right Card: Roommates & Quick Billing
            Dim pnlRight As New GlassPanel() With {
                .Dock = DockStyle.Fill,
                .CornerRadius = 16,
                .FillColor = Color.FromArgb(200, 15, 23, 38),
                .BorderColor = Color.FromArgb(40, 255, 255, 255),
                .Padding = New Padding(24),
                .Margin = New Padding(10, 0, 0, 0)
            }

            Dim lblRoommatesTitle As New Label() With {
                .Text = "FELLOW ROOMMATES IN THIS UNIT",
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.AccentSapphire,
                .AutoSize = True,
                .Location = New Point(24, 20)
            }

            Dim dgvRoommates As New DataGridView() With {
                .Location = New Point(24, 46),
                .Size = New Size(Math.Max(300, pnlRight.ClientSize.Width - 48), 155),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right,
                .BackgroundColor = Color.FromArgb(14, 22, 35),
                .BorderStyle = BorderStyle.None,
                .ReadOnly = True,
                .AllowUserToAddRows = False,
                .RowHeadersVisible = False,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvRoommates)

            dgvRoommates.Columns.Add("bed", "Bed #")
            dgvRoommates.Columns.Add("name", "Student Name")
            dgvRoommates.Columns.Add("course", "Degree Course")
            dgvRoommates.Columns("bed").Width = 60

            For Each rm In _currentAllocation.Roommates
                dgvRoommates.Rows.Add(rm.BedNumber, rm.FullName, rm.Course)
            Next
            If _currentAllocation.Roommates.Count = 0 Then
                dgvRoommates.Rows.Add("—", "No other bedmates currently assigned.", "—")
            End If

            AddHandler pnlRight.Resize, Sub()
                dgvRoommates.Width = Math.Max(300, pnlRight.ClientSize.Width - 48)
            End Sub

            ' Quick fee snapshot
            Dim lblFeeHeader As New Label() With {
                .Text = "SEMESTER FEE SNAPSHOT",
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.AcademicGold,
                .AutoSize = True,
                .Location = New Point(24, 220)
            }

            Dim lblFeeBalance As New Label() With {
                .Text = $"Balance Owed: KES {_currentAllocation.BalanceOwed:N2}  (Rate: KES {_currentAllocation.SemesterFee:N2})",
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular),
                .ForeColor = If(_currentAllocation.BalanceOwed > 0, ThemeManager.Coral, ThemeManager.EmeraldPrimary),
                .AutoSize = True,
                .Location = New Point(24, 244)
            }

            Dim btnPayNow As New GlassButton() With {
                .Text = "💳 Pay Fees (M-Pesa / Bank)",
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .PrimaryColor = ThemeManager.EmeraldPrimary,
                .CustomTextColor = Color.FromArgb(7, 10, 14),
                .CornerRadius = 10,
                .Size = New Size(220, 36),
                .Location = New Point(24, 276)
            }
            AddHandler btnPayNow.Click, Sub() OpenStudentPaymentModal()

            Dim btnViewBilling As New GlassButton() With {
                .Text = "📄 View Ledger & Receipts",
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .PrimaryColor = Color.FromArgb(25, 255, 255, 255),
                .CustomTextColor = Color.FromArgb(203, 213, 225),
                .CornerRadius = 10,
                .Size = New Size(190, 36),
                .Location = New Point(252, 276)
            }
            AddHandler btnViewBilling.Click, Sub() SwitchTab(2)

            pnlRight.Controls.AddRange({lblRoommatesTitle, dgvRoommates, lblFeeHeader, lblFeeBalance, btnPayNow, btnViewBilling})

            pnlSplit.Controls.Add(pnlLeft, 0, 0)
            pnlSplit.Controls.Add(pnlRight, 1, 0)
            pnlTabMyRoom.Controls.Add(pnlSplit)
        End Sub

        Private Function CreateStepCard(stepTag As String, stepTitle As String, stepDesc As String) As Control
            Dim pnl As New GlassPanel() With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(6),
                .CornerRadius = 14,
                .FillColor = Color.FromArgb(190, 14, 22, 35),
                .BorderColor = Color.FromArgb(35, 255, 255, 255),
                .Padding = New Padding(18)
            }

            Dim lblTag As New Label() With {
                .Text = stepTag,
                .Font = New Font("Segoe UI Semibold", 8.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.AccentSapphire,
                .AutoSize = True,
                .Location = New Point(18, 16)
            }

            Dim lblTitle As New Label() With {
                .Text = stepTitle,
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                .ForeColor = Color.White,
                .AutoSize = True,
                .Location = New Point(18, 38)
            }

            Dim lblDesc As New Label() With {
                .Text = stepDesc,
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(148, 163, 184),
                .Size = New Size(Math.Max(180, pnl.ClientSize.Width - 36), 65),
                .Location = New Point(18, 64)
            }
            AddHandler pnl.Resize, Sub()
                lblDesc.Width = Math.Max(180, pnl.ClientSize.Width - 36)
            End Sub

            pnl.Controls.AddRange({lblTag, lblTitle, lblDesc})
            Return pnl
        End Function

        ' =====================================================================
        ' TAB 2: BROWSE AVAILABLE UNITS
        ' =====================================================================
        Private Async Function BuildBrowseUnitsTabAsync() As Task
            pnlTabBrowse.Controls.Clear()

            Dim gender = If(_studentProfile IsNot Nothing, _studentProfile.Gender, "MALE")

            ' Top Filter Bar
            Dim pnlFilter As New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 50,
                .Padding = New Padding(10)
            }

            Dim lblGenderPolicy As New Label() With {
                .Text = $"Gender Policy: Displaying verified eligible accommodation for {gender.ToUpperInvariant()} students",
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.AccentSapphire,
                .AutoSize = True,
                .Location = New Point(10, 14)
            }

            Dim btnRefreshUnits As New GlassButton() With {
                .Text = "🔄 Refresh Units",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .PrimaryColor = Color.FromArgb(25, 255, 255, 255),
                .CustomTextColor = Color.FromArgb(203, 213, 225),
                .CornerRadius = 8,
                .Size = New Size(130, 30),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .Location = New Point(Math.Max(300, pnlTabBrowse.Width - 150), 8)
            }
            AddHandler btnRefreshUnits.Click, Async Sub() Await BuildBrowseUnitsTabAsync()
            AddHandler pnlFilter.Resize, Sub()
                btnRefreshUnits.Location = New Point(Math.Max(300, pnlFilter.ClientSize.Width - 145), 8)
            End Sub

            pnlFilter.Controls.AddRange({lblGenderPolicy, btnRefreshUnits})
            pnlTabBrowse.Controls.Add(pnlFilter)

            ' Flow layout for room cards
            Dim flpRooms As New FlowLayoutPanel() With {
                .Dock = DockStyle.Fill,
                .AutoScroll = True,
                .Padding = New Padding(10)
            }

            Dim availableRooms = Await _portalService.GetAvailableRoomsForStudentAsync(gender)

            If availableRooms.Count = 0 Then
                Dim lblNone As New Label() With {
                    .Text = "All bed spaces in your eligible hostels are currently occupied for this semester." & vbCrLf &
                            "Please contact the Accommodation Directorate for off-campus verified hostel listings.",
                    .Font = New Font("Segoe UI", 11.0F, FontStyle.Regular),
                    .ForeColor = Color.FromArgb(148, 163, 184),
                    .Size = New Size(700, 60),
                    .Margin = New Padding(20)
                }
                flpRooms.Controls.Add(lblNone)
            Else
                For Each card In availableRooms
                    Dim roomCard = CreateRoomCardControl(card)
                    flpRooms.Controls.Add(roomCard)
                Next
            End If

            pnlTabBrowse.Controls.Add(flpRooms)
            flpRooms.BringToFront()
        End Function

        Private Function CreateRoomCardControl(card As AvailableRoomCard) As Control
            Dim pnlCard As New GlassPanel() With {
                .Size = New Size(340, 240),
                .Margin = New Padding(12),
                .CornerRadius = 14,
                .FillColor = Color.FromArgb(210, 15, 23, 38),
                .BorderColor = Color.FromArgb(40, 255, 255, 255),
                .Padding = New Padding(16)
            }

            Dim lblHostel As New Label() With {
                .Text = card.HostelName,
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                .ForeColor = Color.White,
                .Size = New Size(300, 22),
                .Location = New Point(16, 14)
            }

            Dim lblRoomNum As New Label() With {
                .Text = $"Room {card.RoomNumber} • Floor {card.FloorNumber} ({card.RoomType})",
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(148, 163, 184),
                .Size = New Size(300, 20),
                .Location = New Point(16, 38)
            }

            Dim badgeBeds As New Label() With {
                .Text = $"Available: {card.AvailableBedsCount} Bed Spaces",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.EmeraldPrimary,
                .BackColor = Color.FromArgb(25, 16, 185, 129),
                .Padding = New Padding(8, 3, 8, 3),
                .AutoSize = True,
                .Location = New Point(16, 66)
            }

            Dim lblRate As New Label() With {
                .Text = $"KES {card.SemesterRate:N2} / Semester",
                .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.AcademicGold,
                .Size = New Size(300, 24),
                .Location = New Point(16, 102)
            }

            Dim deskTxt = If(card.HasDesk, "✓ Desk", "")
            Dim lockTxt = If(card.HasLocker, "✓ Locker", "")
            Dim winTxt = If(card.HasWindow, "✓ Window", "")
            Dim lblAmenities As New Label() With {
                .Text = $"{deskTxt}   {lockTxt}   {winTxt}".Trim(),
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(203, 213, 225),
                .Size = New Size(300, 20),
                .Location = New Point(16, 134)
            }

            Dim btnBook As New GlassButton() With {
                .Text = "Book Bed Space",
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .PrimaryColor = ThemeManager.EmeraldPrimary,
                .CustomTextColor = Color.FromArgb(7, 10, 14),
                .CornerRadius = 10,
                .Size = New Size(308, 36),
                .Location = New Point(16, 184)
            }
            AddHandler btnBook.Click, Sub() OpenBookBedModal(card)

            pnlCard.Controls.AddRange({lblHostel, lblRoomNum, badgeBeds, lblRate, lblAmenities, btnBook})
            Return pnlCard
        End Function

        ' =====================================================================
        ' TAB 3: BILLING & ELECTRONIC RECEIPTS
        ' =====================================================================
        Private Async Function BuildBillingTabAsync() As Task
            pnlTabBilling.Controls.Clear()

            Dim studentId = _currentUser.StudentId.GetValueOrDefault(0)
            Dim dtPayments = Await _portalService.GetStudentPaymentHistoryAsync(studentId)

            ' Top Metrics Header (Responsive TableLayoutPanel)
            Dim pnlMetricsGrid As New TableLayoutPanel() With {
                .Dock = DockStyle.Top,
                .Height = 105,
                .ColumnCount = 3,
                .RowCount = 1,
                .Padding = New Padding(0, 0, 0, 8)
            }
            pnlMetricsGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
            pnlMetricsGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
            pnlMetricsGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.34F))

            Dim fee = If(_currentAllocation IsNot Nothing, _currentAllocation.SemesterFee, 0.0D)
            Dim paid = If(_currentAllocation IsNot Nothing, _currentAllocation.TotalPaid, 0.0D)
            Dim bal = If(_currentAllocation IsNot Nothing, _currentAllocation.BalanceOwed, 0.0D)

            Dim cardFee = CreateMetricCard("Semester Accommodation Fee", $"KES {fee:N2}", ThemeManager.AccentSapphire)
            Dim cardPaid = CreateMetricCard("Total Amount Settled", $"KES {paid:N2}", ThemeManager.EmeraldPrimary)
            Dim cardBal = CreateMetricCard("Outstanding Balance Owed", $"KES {bal:N2}", If(bal > 0, ThemeManager.Coral, ThemeManager.EmeraldPrimary))

            pnlMetricsGrid.Controls.Add(cardFee, 0, 0)
            pnlMetricsGrid.Controls.Add(cardPaid, 1, 0)
            pnlMetricsGrid.Controls.Add(cardBal, 2, 0)
            pnlTabBilling.Controls.Add(pnlMetricsGrid)

            ' Action Bar
            Dim pnlActions As New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 48,
                .Padding = New Padding(10, 6, 10, 6)
            }

            Dim btnMakePay As New GlassButton() With {
                .Text = "💳 Pay Fees (M-Pesa / Bank)",
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .PrimaryColor = ThemeManager.EmeraldPrimary,
                .CustomTextColor = Color.FromArgb(7, 10, 14),
                .CornerRadius = 8,
                .Size = New Size(230, 34),
                .Location = New Point(10, 6)
            }
            AddHandler btnMakePay.Click, Sub() OpenStudentPaymentModal()

            pnlActions.Controls.Add(btnMakePay)
            pnlTabBilling.Controls.Add(pnlActions)

            ' Payments History Table
            Dim dgvPayments As New DataGridView() With {
                .Dock = DockStyle.Fill,
                .BackgroundColor = Color.FromArgb(14, 22, 35),
                .BorderStyle = BorderStyle.None,
                .ReadOnly = True,
                .AllowUserToAddRows = False,
                .RowHeadersVisible = False,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvPayments)

            dgvPayments.DataSource = dtPayments

            If dgvPayments.Columns.Contains("payment_id") Then dgvPayments.Columns("payment_id").Visible = False
            If dgvPayments.Columns.Contains("receipt_number") Then dgvPayments.Columns("receipt_number").HeaderText = "Receipt #"
            If dgvPayments.Columns.Contains("unit_name") Then dgvPayments.Columns("unit_name").HeaderText = "Hostel Unit"
            If dgvPayments.Columns.Contains("amount_paid") Then
                dgvPayments.Columns("amount_paid").HeaderText = "Amount (KES)"
                dgvPayments.Columns("amount_paid").DefaultCellStyle.Format = "N2"
            End If
            If dgvPayments.Columns.Contains("payment_method") Then dgvPayments.Columns("payment_method").HeaderText = "Channel"
            If dgvPayments.Columns.Contains("transaction_reference") Then dgvPayments.Columns("transaction_reference").HeaderText = "Reference Code"
            If dgvPayments.Columns.Contains("payment_date") Then
                dgvPayments.Columns("payment_date").HeaderText = "Date"
                dgvPayments.Columns("payment_date").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"
            End If
            If dgvPayments.Columns.Contains("payment_status") Then dgvPayments.Columns("payment_status").HeaderText = "Status"

            pnlTabBilling.Controls.Add(dgvPayments)
            dgvPayments.BringToFront()
        End Function

        Private Function CreateMetricCard(title As String, value As String, valueColor As Color) As Control
            Dim pnl As New GlassPanel() With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(6),
                .CornerRadius = 12,
                .FillColor = Color.FromArgb(200, 15, 23, 38),
                .BorderColor = Color.FromArgb(40, 255, 255, 255),
                .Padding = New Padding(14)
            }

            Dim lblT As New Label() With {
                .Text = title,
                .Font = New Font("Segoe UI", 8.5F, FontStyle.Regular),
                .ForeColor = Color.FromArgb(148, 163, 184),
                .AutoSize = True,
                .Location = New Point(14, 14)
            }

            Dim lblV As New Label() With {
                .Text = value,
                .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
                .ForeColor = valueColor,
                .AutoSize = True,
                .Location = New Point(14, 38)
            }

            pnl.Controls.AddRange({lblT, lblV})
            Return pnl
        End Function

        ' =====================================================================
        ' TAB 4: ROOM MAINTENANCE & HELPDESK
        ' =====================================================================
        Private Async Function BuildMaintenanceTabAsync() As Task
            pnlTabMaintenance.Controls.Clear()

            Dim roomId = If(_currentAllocation IsNot Nothing, _currentAllocation.RoomId, 0)
            Dim studentId = _currentUser.StudentId.GetValueOrDefault(0)

            ' Top Action Bar
            Dim pnlActions As New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 50,
                .Padding = New Padding(10)
            }

            Dim lblTitle As New Label() With {
                .Text = "Room Maintenance Tickets & Incident Helpdesk",
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                .ForeColor = Color.White,
                .AutoSize = True,
                .Location = New Point(10, 14)
            }

            Dim btnNewTicket As New GlassButton() With {
                .Text = "🔧 Report Defect in Room",
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .PrimaryColor = ThemeManager.EmeraldPrimary,
                .CustomTextColor = Color.FromArgb(7, 10, 14),
                .CornerRadius = 8,
                .Size = New Size(210, 32),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .Location = New Point(Math.Max(300, pnlActions.ClientSize.Width - 220), 9)
            }
            AddHandler btnNewTicket.Click, Sub() OpenStudentIssueModal()
            AddHandler pnlActions.Resize, Sub()
                btnNewTicket.Location = New Point(Math.Max(300, pnlActions.ClientSize.Width - 220), 9)
            End Sub

            pnlActions.Controls.AddRange({lblTitle, btnNewTicket})
            pnlTabMaintenance.Controls.Add(pnlActions)

            ' DataGridView
            Dim dgvMaint As New DataGridView() With {
                .Dock = DockStyle.Fill,
                .BackgroundColor = Color.FromArgb(14, 22, 35),
                .BorderStyle = BorderStyle.None,
                .ReadOnly = True,
                .AllowUserToAddRows = False,
                .RowHeadersVisible = False,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvMaint)

            Dim dtMaint = Await _portalService.GetStudentTicketsAsync(studentId, roomId)
            dgvMaint.DataSource = dtMaint

            If dgvMaint.Columns.Contains("maintenance_id") Then dgvMaint.Columns("maintenance_id").HeaderText = "Ticket #"
            If dgvMaint.Columns.Contains("room_name") Then dgvMaint.Columns("room_name").HeaderText = "Hostel Unit"
            If dgvMaint.Columns.Contains("issue_title") Then dgvMaint.Columns("issue_title").HeaderText = "Defect Description"
            If dgvMaint.Columns.Contains("maintenance_status") Then dgvMaint.Columns("maintenance_status").HeaderText = "Status"
            If dgvMaint.Columns.Contains("reported_date") Then
                dgvMaint.Columns("reported_date").HeaderText = "Reported"
                dgvMaint.Columns("reported_date").DefaultCellStyle.Format = "yyyy-MM-dd"
            End If
            If dgvMaint.Columns.Contains("resolution_notes") Then dgvMaint.Columns("resolution_notes").HeaderText = "Resolution Notes"
            If dgvMaint.Columns.Contains("issue_description") Then dgvMaint.Columns("issue_description").Visible = False

            pnlTabMaintenance.Controls.Add(dgvMaint)
            dgvMaint.BringToFront()
        End Function

        ' =====================================================================
        ' MODAL LAUNCHERS
        ' =====================================================================
        Private Async Sub OpenBookBedModal(roomCard As AvailableRoomCard)
            Using modal As New frmBookBedModal(roomCard, _currentUser)
                If modal.ShowDialog(Me) = DialogResult.OK Then
                    MessageBox.Show("Bed space booked successfully! Your residency status has been updated.", "Booking Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Await RefreshAllStudentDataAsync()
                    SwitchTab(0)
                End If
            End Using
        End Sub

        Private Async Sub OpenStudentPaymentModal()
            If _currentAllocation Is Nothing Then
                MessageBox.Show("You currently do not have an active bed allocation to make payments for.", "No Active Allocation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using modal As New frmStudentPaymentModal(_currentAllocation.AllocationId, _currentAllocation.BalanceOwed, _currentUser)
                If modal.ShowDialog(Me) = DialogResult.OK Then
                    Dim receiptNo = modal.GeneratedReceiptNumber
                    Dim askReceipt = MessageBox.Show($"Payment verified! Receipt #{receiptNo} generated." & vbCrLf & "Would you like to preview/print your official DeKUT receipt now?",
                                                     "Payment Confirmed", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If askReceipt = DialogResult.Yes Then
                        Dim dt = DatabaseConnection.ExecuteDataTable("SELECT TOP 1 payment_id FROM dbo.payments WHERE receipt_number = @rec;", {DatabaseConnection.CreateParam("@rec", receiptNo, SqlDbType.NVarChar)})
                        If dt.Rows.Count > 0 Then
                            Using receiptModal As New Forms.Billing.frmReceiptPreviewModal(Convert.ToInt32(dt.Rows(0)("payment_id")))
                                receiptModal.ShowDialog(Me)
                            End Using
                        End If
                    End If

                    Await RefreshAllStudentDataAsync()
                End If
            End Using
        End Sub

        Private Async Sub OpenStudentIssueModal()
            If _currentAllocation Is Nothing Then
                MessageBox.Show("Please book a bed space first before reporting room defects.", "No Active Unit", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using modal As New frmStudentIssueModal(_currentAllocation.RoomId, _currentAllocation.BedId, _currentUser)
                If modal.ShowDialog(Me) = DialogResult.OK Then
                    MessageBox.Show("Maintenance ticket submitted successfully. University technicians will inspect the defect.", "Ticket Lodged", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Await BuildMaintenanceTabAsync()
                    SwitchTab(3)
                End If
            End Using
        End Sub

        ' =====================================================================
        ' TAB SWITCHING & WINDOW EVENTS
        ' =====================================================================
        Private Sub SwitchTab(tabIndex As Integer)
            For i As Integer = 0 To _tabPanels.Count - 1
                Dim isSelected = (i = tabIndex)
                _tabPanels(i).Visible = isSelected

                If isSelected Then
                    _navButtons(i).PrimaryColor = ThemeManager.EmeraldPrimary
                    _navButtons(i).CustomTextColor = Color.FromArgb(7, 10, 14)
                    _navButtons(i).BorderColor = Color.FromArgb(52, 211, 153)
                    _navButtons(i).HoverBorderColor = Color.FromArgb(110, 231, 183)
                    _navButtons(i).Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
                Else
                    _navButtons(i).PrimaryColor = Color.FromArgb(18, 26, 40)
                    _navButtons(i).HoverColor = Color.FromArgb(28, 42, 64)
                    _navButtons(i).BorderColor = Color.FromArgb(50, 255, 255, 255)
                    _navButtons(i).HoverBorderColor = Color.FromArgb(120, 255, 255, 255)
                    _navButtons(i).CustomTextColor = Color.FromArgb(203, 213, 225)
                    _navButtons(i).Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
                End If
            Next
        End Sub

        Private Sub pnlNavBar_Resize(sender As Object, e As EventArgs) Handles pnlNavBar.Resize
            If btnNavMyRoom Is Nothing OrElse btnNavBrowse Is Nothing OrElse btnNavBilling Is Nothing OrElse btnNavMaintenance Is Nothing Then Return
            Dim padLeft As Integer = 14
            Dim totalW As Integer = pnlNavBar.ClientSize.Width - (padLeft * 2)
            Dim gap As Integer = 10
            Dim count As Integer = 4
            Dim btnWidth As Integer = Math.Max(140, (totalW - (gap * (count - 1))) \ count)

            btnNavMyRoom.Size = New Size(btnWidth, 36)
            btnNavMyRoom.Location = New Point(padLeft, 6)

            btnNavBrowse.Size = New Size(btnWidth, 36)
            btnNavBrowse.Location = New Point(padLeft + btnWidth + gap, 6)

            btnNavBilling.Size = New Size(btnWidth, 36)
            btnNavBilling.Location = New Point(padLeft + (btnWidth + gap) * 2, 6)

            btnNavMaintenance.Size = New Size(btnWidth, 36)
            btnNavMaintenance.Location = New Point(padLeft + (btnWidth + gap) * 3, 6)
        End Sub

        Private Sub pnlHeader_Resize(sender As Object, e As EventArgs) Handles pnlHeader.Resize
            If btnClose Is Nothing OrElse btnMaximize Is Nothing OrElse btnMinimize Is Nothing Then Return
            Dim rightMargin As Integer = pnlHeader.ClientSize.Width - 10
            btnClose.Location = New Point(rightMargin - 35, 12)
            btnMaximize.Location = New Point(rightMargin - 72, 12)
            btnMinimize.Location = New Point(rightMargin - 109, 12)
            btnSignOut.Location = New Point(rightMargin - 200, 12)

            Dim pillRight As Integer = rightMargin - 212
            If pillRight > 650 Then
                lblStudentPill.Visible = True
                lblStudentPill.Location = New Point(pillRight - lblStudentPill.Width, 12)
                Dim semRight As Integer = lblStudentPill.Left - 10
                If semRight > 450 Then
                    lblSemesterPill.Visible = True
                    lblSemesterPill.Location = New Point(semRight - lblSemesterPill.Width, 12)
                Else
                    lblSemesterPill.Visible = False
                End If
            Else
                lblSemesterPill.Visible = False
                lblStudentPill.Visible = (pillRight > 400)
                If lblStudentPill.Visible Then
                    lblStudentPill.Location = New Point(pillRight - lblStudentPill.Width, 12)
                End If
            End If
        End Sub

        Private Sub btnNavMyRoom_Click(sender As Object, e As EventArgs) Handles btnNavMyRoom.Click
            SwitchTab(0)
        End Sub

        Private Sub btnNavBrowse_Click(sender As Object, e As EventArgs) Handles btnNavBrowse.Click
            SwitchTab(1)
        End Sub

        Private Sub btnNavBilling_Click(sender As Object, e As EventArgs) Handles btnNavBilling.Click
            SwitchTab(2)
        End Sub

        Private Sub btnNavMaintenance_Click(sender As Object, e As EventArgs) Handles btnNavMaintenance.Click
            SwitchTab(3)
        End Sub

        Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
            Me.Close()
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

        Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
            Me.WindowState = FormWindowState.Minimized
        End Sub

        Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
            If Me.WindowState = FormWindowState.Maximized Then
                Me.WindowState = FormWindowState.Normal
                btnMaximize.Text = "🗖"
            Else
                Me.WindowState = FormWindowState.Maximized
                btnMaximize.Text = "🗗"
            End If
        End Sub

        Private Sub pnlHeader_DoubleClick(sender As Object, e As EventArgs) Handles pnlHeader.DoubleClick, lblBrandTitle.DoubleClick
            btnMaximize_Click(sender, e)
        End Sub

        ' =====================================================================
        ' WINDOW DRAGGING & EDGE RESIZE (BORDERLESS NATIVE RESIZE)
        ' =====================================================================
        Protected Overrides Sub WndProc(ByRef m As Message)
            Const WM_NCHITTEST As Integer = &H84
            Const HTCLIENT As Integer = 1
            Const HTLEFT As Integer = 10
            Const HTRIGHT As Integer = 11
            Const HTTOP As Integer = 12
            Const HTTOPLEFT As Integer = 13
            Const HTTOPRIGHT As Integer = 14
            Const HTBOTTOM As Integer = 15
            Const HTBOTTOMLEFT As Integer = 16
            Const HTBOTTOMRIGHT As Integer = 17

            If m.Msg = WM_NCHITTEST AndAlso Me.WindowState = FormWindowState.Normal Then
                MyBase.WndProc(m)
                If m.Result.ToInt32() = HTCLIENT Then
                    Dim screenPoint As New Point(m.LParam.ToInt32() And &HFFFF, (m.LParam.ToInt32() >> 16) And &HFFFF)
                    Dim pt As Point = Me.PointToClient(screenPoint)
                    Dim resizeArea As Integer = 8
                    Dim isLeft As Boolean = (pt.X <= resizeArea)
                    Dim isRight As Boolean = (pt.X >= Me.ClientSize.Width - resizeArea)
                    Dim isTop As Boolean = (pt.Y <= resizeArea)
                    Dim isBottom As Boolean = (pt.Y >= Me.ClientSize.Height - resizeArea)

                    If isTop AndAlso isLeft Then
                        m.Result = New IntPtr(HTTOPLEFT)
                        Return
                    ElseIf isTop AndAlso isRight Then
                        m.Result = New IntPtr(HTTOPRIGHT)
                        Return
                    ElseIf isBottom AndAlso isLeft Then
                        m.Result = New IntPtr(HTBOTTOMLEFT)
                        Return
                    ElseIf isBottom AndAlso isRight Then
                        m.Result = New IntPtr(HTBOTTOMRIGHT)
                        Return
                    ElseIf isLeft Then
                        m.Result = New IntPtr(HTLEFT)
                        Return
                    ElseIf isRight Then
                        m.Result = New IntPtr(HTRIGHT)
                        Return
                    ElseIf isTop Then
                        m.Result = New IntPtr(HTTOP)
                        Return
                    ElseIf isBottom Then
                        m.Result = New IntPtr(HTBOTTOM)
                        Return
                    End If
                End If
                Return
            End If

            MyBase.WndProc(m)
        End Sub

        Private Sub pnlHeader_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseDown, lblBrandTitle.MouseDown, lblSubBrand.MouseDown
            If e.Button = MouseButtons.Left Then
                _isDragging = True
                _dragStartPoint = New Point(e.X, e.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseMove, lblBrandTitle.MouseMove, lblSubBrand.MouseMove
            If _isDragging Then
                Dim screenPos = PointToScreen(e.Location)
                Me.Location = New Point(screenPos.X - _dragStartPoint.X, screenPos.Y - _dragStartPoint.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseUp, lblBrandTitle.MouseUp, lblSubBrand.MouseUp
            _isDragging = False
        End Sub
    End Class
End Namespace
