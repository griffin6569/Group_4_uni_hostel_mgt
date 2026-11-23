Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports University_hostel_mgt.Controls.Views
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Forms
    ''' <summary>
    ''' Central Executive Dashboard and Navigation Hub for the University Hostel Management System.
    ''' Adheres strictly to the Prestige Academic & Executive Design System.
    ''' </summary>
    Public Class frmMainDashboard
        Private ReadOnly _currentUser As User
        Private ReadOnly _semesterRepo As New SemesterRepository()

        Private _activeNavButton As Button = Nothing
        Private _currentSemesterId As Integer = 0

        ' View Controls
        Private _viewDashboard As DashboardOverviewControl
        Private _viewStudents As StudentsControl
        Private _viewHostels As HostelsControl
        Private _viewRooms As RoomsControl
        Private _viewBeds As BedsControl
        Private _viewAllocations As AllocationsControl
        Private _viewBilling As BillingControl
        Private _viewMaintenance As MaintenanceControl
        Private _viewReports As ReportsControl
        Private _viewUsers As UsersControl

        Private Structure SemesterItem
            Public Property Id As Integer
            Public Property Name As String
            Public Property IsCurrent As Boolean

            Public Overrides Function ToString() As String
                Return If(IsCurrent, $"★ {Name} (ACTIVE)", Name)
            End Function
        End Structure

        Public Sub New(Optional user As User = Nothing)
            InitializeComponent()
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlContentHost)

            If user IsNot Nothing Then
                _currentUser = user
            Else
                _currentUser = New User With {
                    .UserId = 1,
                    .FullName = "Dr. Arthur Pendelton",
                    .Username = "admin",
                    .RoleName = Role.SuperAdmin,
                    .AccountStatus = "ACTIVE"
                }
            End If

            ConfigureUserProfile()
            PaintAcademicCrest()
            ApplyFrostedGlassShell()
            InitializeViews()
            LoadSemesters()

            ' Default to Dashboard View
            SwitchView("DASHBOARD", btnNavDashboard)
        End Sub

        Private Sub ConfigureUserProfile()
            lblUserName.Text = _currentUser.FullName
            lblUserRole.Text = _currentUser.RoleName.ToUpperInvariant()

            ' Monogram Initials
            Dim initials As String = "U"
            If Not String.IsNullOrWhiteSpace(_currentUser.FullName) Then
                Dim parts = _currentUser.FullName.Split(" "c, StringSplitOptions.RemoveEmptyEntries)
                If parts.Length >= 2 Then
                    initials = $"{parts(0)(0)}{parts(parts.Length - 1)(0)}".ToUpperInvariant()
                ElseIf parts.Length = 1 Then
                    initials = parts(0).Substring(0, Math.Min(2, parts(0).Length)).ToUpperInvariant()
                End If
            End If
            lblUserInitials.Text = initials

            ' Role-based permission gating
            If Not _currentUser.CanManageUsers Then
                btnNavUsers.Visible = False
            End If
        End Sub

        Private Sub PaintAcademicCrest()
            Dim bmp As New Bitmap(picCrest.Width, picCrest.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim crestRect As New RectangleF(2, 2, picCrest.Width - 4, picCrest.Height - 4)
                ThemeManager.DrawAcademicCrest(g, crestRect, ThemeManager.AcademicGold, ThemeManager.NavyPrimary)
            End Using
            picCrest.Image = bmp
        End Sub

        Private Sub InitializeViews()
            ' 1. Dashboard View
            _viewDashboard = New DashboardOverviewControl()
            AddHandler _viewDashboard.RequestNewBed, Sub() SwitchView("BEDS", btnNavBeds, Sub() _viewBeds.OpenNewBedModal())
            AddHandler _viewDashboard.RequestNewStudent, Sub() SwitchView("STUDENTS", btnNavStudents, Sub() _viewStudents.OpenNewStudentModal())
            AddHandler _viewDashboard.RequestNewRoom, Sub() SwitchView("ROOMS", btnNavRooms, Sub() _viewRooms.OpenNewRoomModal())
            AddHandler _viewDashboard.RequestNewHostel, Sub() SwitchView("HOSTELS", btnNavHostels, Sub() _viewHostels.OpenNewHostelModal())

            ' 2. Students View
            _viewStudents = New StudentsControl()

            ' 3. Hostels View
            _viewHostels = New HostelsControl()

            ' 4. Rooms View
            _viewRooms = New RoomsControl()

            ' 5. Beds View
            _viewBeds = New BedsControl()

            ' 6. Allocations View (Phase 4 Live Module)
            _viewAllocations = New AllocationsControl(_currentUser.UserId)

            ' 7. Billing View (Phase 4 Live Module)
            _viewBilling = New BillingControl(_currentUser.UserId)

            ' 8. Maintenance View (Phase 5 Live Module)
            _viewMaintenance = New MaintenanceControl(_currentUser.UserId)

            ' 9. Reports View (Phase 5 Live Module)
            _viewReports = New ReportsControl()

            ' 10. System Users & RBAC View (Phase 5 Live Module)
            _viewUsers = New UsersControl(_currentUser.UserId)

            ' Add all views to pnlContentHost
            pnlContentHost.Controls.AddRange({
                _viewDashboard,
                _viewStudents,
                _viewHostels,
                _viewRooms,
                _viewBeds,
                _viewAllocations,
                _viewBilling,
                _viewMaintenance,
                _viewReports,
                _viewUsers
            })
        End Sub

        Private Sub LoadSemesters()
            cboSemester.Items.Clear()
            Dim semesters = _semesterRepo.GetSemesters()

            Dim selectedIndex As Integer = 0
            For i As Integer = 0 To semesters.Count - 1
                Dim s = semesters(i)
                Dim item As New SemesterItem With {
                    .Id = s.SemesterId,
                    .Name = s.SemesterName,
                    .IsCurrent = s.IsCurrent
                }
                cboSemester.Items.Add(item)
                If s.IsCurrent Then
                    selectedIndex = i
                    _currentSemesterId = s.SemesterId
                End If
            Next

            If cboSemester.Items.Count > 0 Then
                cboSemester.SelectedIndex = selectedIndex
            End If
        End Sub

        Private Sub cboSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSemester.SelectedIndexChanged
            If cboSemester.SelectedItem IsNot Nothing Then
                Dim item = DirectCast(cboSemester.SelectedItem, SemesterItem)
                _currentSemesterId = item.Id
                RefreshActiveView()
            End If
        End Sub

        Private Sub ApplyFrostedGlassShell()
            ' 1. Top Bar Glass Styling
            pnlTopBar.BackColor = Color.FromArgb(10, 16, 27)
            lblViewTitle.ForeColor = ThemeManager.TextChalkWhite
            lblViewSub.ForeColor = ThemeManager.TextMutedSlate
            lblClock.ForeColor = ThemeManager.TextSilverSlate
            lblSemesterPrompt.ForeColor = ThemeManager.TextMutedSlate

            cboSemester.BackColor = Color.FromArgb(16, 26, 42)
            cboSemester.ForeColor = Color.White

            pnlDbStatus.BackColor = Color.FromArgb(12, 36, 41)
            lblDbStatus.ForeColor = ThemeManager.GlassStatusSuccess
            dotStatus.BackColor = ThemeManager.GlassStatusSuccess

            btnRefreshData.BackColor = Color.FromArgb(30, 42, 60)
            btnRefreshData.ForeColor = ThemeManager.TextChalkWhite
            btnRefreshData.FlatStyle = FlatStyle.Flat
            btnRefreshData.FlatAppearance.BorderColor = ThemeManager.GlassBorder
            btnRefreshData.FlatAppearance.BorderSize = 1

            ' 2. Sidebar Glass Refinement
            pnlSidebar.BackColor = Color.FromArgb(7, 12, 20)
            pnlBrand.BackColor = Color.FromArgb(5, 9, 16)
            pnlUserProfile.BackColor = Color.FromArgb(6, 11, 18)
            lblUserName.ForeColor = ThemeManager.TextPlatinum
            lblUserRole.ForeColor = ThemeManager.GlassStatusInfo
            pnlLogout.BackColor = Color.FromArgb(5, 9, 16)
            btnNavLogout.FlatAppearance.BorderColor = Color.FromArgb(40, 55, 80)
            btnNavLogout.ForeColor = ThemeManager.TextSilverSlate

            ' 3. Content Host Dark Canvas with Ambient Diffused Orbs
            pnlContentHost.BackColor = ThemeManager.CanvasDark
            AddHandler pnlContentHost.Paint, Sub(s, e)
                                                 Dim g As Graphics = e.Graphics
                                                 g.SmoothingMode = SmoothingMode.AntiAlias
                                                 ' Base dark gradient
                                                 Using baseBrush As New LinearGradientBrush(
                                                     New Point(0, 0),
                                                     New Point(pnlContentHost.Width, pnlContentHost.Height),
                                                     Color.FromArgb(7, 12, 21),
                                                     Color.FromArgb(12, 20, 34))
                                                     g.FillRectangle(baseBrush, pnlContentHost.ClientRectangle)
                                                 End Using

                                                 ' Diffused Sapphire Glow (Top-Right)
                                                 Dim r1 As Integer = CInt(Math.Min(pnlContentHost.Width, pnlContentHost.Height) * 0.7F)
                                                 If r1 > 10 Then
                                                     Dim c1 As New Point(CInt(pnlContentHost.Width * 0.85F), CInt(pnlContentHost.Height * 0.2F))
                                                     Using pth As New GraphicsPath()
                                                         pth.AddEllipse(c1.X - r1, c1.Y - r1, r1 * 2, r1 * 2)
                                                         Using pgb As New PathGradientBrush(pth)
                                                             pgb.CenterPoint = c1
                                                             pgb.CenterColor = Color.FromArgb(20, 30, 64, 175)
                                                             pgb.SurroundColors = New Color() {Color.FromArgb(0, 30, 64, 175)}
                                                             g.FillPath(pgb, pth)
                                                         End Using
                                                     End Using
                                                 End If

                                                 ' Diffused Gold Glow (Bottom-Left)
                                                 Dim r2 As Integer = CInt(Math.Min(pnlContentHost.Width, pnlContentHost.Height) * 0.55F)
                                                 If r2 > 10 Then
                                                     Dim c2 As New Point(CInt(pnlContentHost.Width * 0.15F), CInt(pnlContentHost.Height * 0.85F))
                                                     Using pth2 As New GraphicsPath()
                                                         pth2.AddEllipse(c2.X - r2, c2.Y - r2, r2 * 2, r2 * 2)
                                                         Using pgb2 As New PathGradientBrush(pth2)
                                                             pgb2.CenterPoint = c2
                                                             pgb2.CenterColor = Color.FromArgb(14, 217, 119, 6)
                                                             pgb2.SurroundColors = New Color() {Color.FromArgb(0, 217, 119, 6)}
                                                             g.FillPath(pgb2, pth2)
                                                         End Using
                                                     End Using
                                                 End If
                                             End Sub
        End Sub

        Private Sub SwitchView(viewTag As String, navButton As Button, Optional postNavAction As Action = Nothing)
            ' Highlight Nav Button
            If _activeNavButton IsNot Nothing Then
                _activeNavButton.BackColor = Color.FromArgb(7, 12, 20)
                _activeNavButton.ForeColor = Color.FromArgb(148, 163, 184)
            End If

            _activeNavButton = navButton
            If _activeNavButton IsNot Nothing Then
                _activeNavButton.BackColor = Color.FromArgb(30, 58, 138)
                _activeNavButton.ForeColor = Color.White
            End If

            ' Hide all views
            _viewDashboard.Visible = False
            _viewStudents.Visible = False
            _viewHostels.Visible = False
            _viewRooms.Visible = False
            _viewBeds.Visible = False
            _viewAllocations.Visible = False
            _viewBilling.Visible = False
            _viewMaintenance.Visible = False
            _viewReports.Visible = False
            _viewUsers.Visible = False

            Select Case viewTag.ToUpperInvariant()
                Case "DASHBOARD"
                    lblViewTitle.Text = "Executive Dashboard Overview"
                    lblViewSub.Text = "Real-Time Campus Accommodation Operations & Financial Health"
                    _viewDashboard.Visible = True
                    _viewDashboard.BringToFront()
                    _viewDashboard.LoadDashboardData(_currentSemesterId)

                Case "STUDENTS"
                    lblViewTitle.Text = "Student Residents Directory"
                    lblViewSub.Text = "Manage Student Registrations, Academic Profiles & Bed Occupancy"
                    _viewStudents.Visible = True
                    _viewStudents.BringToFront()
                    _viewStudents.LoadStudentsData()

                Case "HOSTELS"
                    lblViewTitle.Text = "Hostel Facilities & Buildings"
                    lblViewSub.Text = "Hostel Structural Configurations, Zones & Capacity Allocations"
                    _viewHostels.Visible = True
                    _viewHostels.BringToFront()
                    _viewHostels.LoadHostelsData()

                Case "ROOMS"
                    lblViewTitle.Text = "Hostel Rooms & Bed Capacities"
                    lblViewSub.Text = "Floor Elevations, Room Types & Capacity Ceiling Enforcement"
                    _viewRooms.Visible = True
                    _viewRooms.BringToFront()
                    _viewRooms.LoadRoomsData()

                Case "BEDS"
                    lblViewTitle.Text = "Bed Space Matrix & Semester Rates"
                    lblViewSub.Text = "Fundamental Individual Bed Units, Rates, Amenities & Status Tracking"
                    _viewBeds.Visible = True
                    _viewBeds.BringToFront()
                    _viewBeds.LoadBedsData()

                Case "ALLOCATIONS"
                    lblViewTitle.Text = "Bed Allocation Management"
                    lblViewSub.Text = "Active Semester Bed Assignment, Rate Freezing, Check-In & Transfers"
                    _viewAllocations.Visible = True
                    _viewAllocations.BringToFront()
                    _viewAllocations.LoadAllocationsData()

                Case "BILLING"
                    lblViewTitle.Text = "Bursar & Payment Ledger"
                    lblViewSub.Text = "Student Accommodation Fee Collection, Electronic Receipts & Arrears"
                    _viewBilling.Visible = True
                    _viewBilling.BringToFront()
                    _viewBilling.LoadBillingData()

                Case "MAINTENANCE"
                    lblViewTitle.Text = "Hostel Maintenance & Facility Desk"
                    lblViewSub.Text = "Incident Defect Tracking, Evidence Photographs & Bed Space Locking"
                    _viewMaintenance.Visible = True
                    _viewMaintenance.BringToFront()
                    _viewMaintenance.LoadMaintenanceData()

                Case "REPORTS"
                    lblViewTitle.Text = "Institutional Reports & Analytics"
                    lblViewSub.Text = "Official Room Occupancy Matrices, Allocation Schedules & Defaulters Audits"
                    _viewReports.Visible = True
                    _viewReports.BringToFront()
                    _viewReports.LoadActiveReportData()

                Case "USERS"
                    lblViewTitle.Text = "System User Administration & RBAC"
                    lblViewSub.Text = "Staff Accounts, PBKDF2 Password Vault & Activity Audit Trail"
                    _viewUsers.Visible = True
                    _viewUsers.BringToFront()
                    _viewUsers.LoadUsersData()
            End Select

            If postNavAction IsNot Nothing Then
                postNavAction.Invoke()
            End If
        End Sub

        Private Sub RefreshActiveView()
            If _viewDashboard.Visible Then
                _viewDashboard.LoadDashboardData(_currentSemesterId)
            ElseIf _viewStudents.Visible Then
                _viewStudents.LoadStudentsData()
            ElseIf _viewHostels.Visible Then
                _viewHostels.LoadHostelsData()
            ElseIf _viewRooms.Visible Then
                _viewRooms.LoadRoomsData()
            ElseIf _viewBeds.Visible Then
                _viewBeds.LoadBedsData()
            ElseIf _viewAllocations.Visible Then
                _viewAllocations.LoadAllocationsData()
            ElseIf _viewBilling.Visible Then
                _viewBilling.LoadBillingData()
            ElseIf _viewMaintenance.Visible Then
                _viewMaintenance.LoadMaintenanceData()
            ElseIf _viewReports.Visible Then
                _viewReports.LoadActiveReportData()
            ElseIf _viewUsers.Visible Then
                _viewUsers.LoadUsersData()
            End If
        End Sub

        ' =========================================================================
        ' NAVIGATION EVENT HANDLERS
        ' =========================================================================
        Private Sub btnNavDashboard_Click(sender As Object, e As EventArgs) Handles btnNavDashboard.Click
            SwitchView("DASHBOARD", btnNavDashboard)
        End Sub

        Private Sub btnNavStudents_Click(sender As Object, e As EventArgs) Handles btnNavStudents.Click
            SwitchView("STUDENTS", btnNavStudents)
        End Sub

        Private Sub btnNavHostels_Click(sender As Object, e As EventArgs) Handles btnNavHostels.Click
            SwitchView("HOSTELS", btnNavHostels)
        End Sub

        Private Sub btnNavRooms_Click(sender As Object, e As EventArgs) Handles btnNavRooms.Click
            SwitchView("ROOMS", btnNavRooms)
        End Sub

        Private Sub btnNavBeds_Click(sender As Object, e As EventArgs) Handles btnNavBeds.Click
            SwitchView("BEDS", btnNavBeds)
        End Sub

        Private Sub btnNavAllocations_Click(sender As Object, e As EventArgs) Handles btnNavAllocations.Click
            SwitchView("ALLOCATIONS", btnNavAllocations)
        End Sub

        Private Sub btnNavBilling_Click(sender As Object, e As EventArgs) Handles btnNavBilling.Click
            SwitchView("BILLING", btnNavBilling)
        End Sub

        Private Sub btnNavMaintenance_Click(sender As Object, e As EventArgs) Handles btnNavMaintenance.Click
            SwitchView("MAINTENANCE", btnNavMaintenance)
        End Sub

        Private Sub btnNavReports_Click(sender As Object, e As EventArgs) Handles btnNavReports.Click
            SwitchView("REPORTS", btnNavReports)
        End Sub

        Private Sub btnNavUsers_Click(sender As Object, e As EventArgs) Handles btnNavUsers.Click
            SwitchView("USERS", btnNavUsers)
        End Sub

        Private Sub btnRefreshData_Click(sender As Object, e As EventArgs) Handles btnRefreshData.Click
            RefreshActiveView()
        End Sub

        Private Sub tmrClock_Tick(sender As Object, e As EventArgs) Handles tmrClock.Tick
            lblClock.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        End Sub

        Private Sub btnNavLogout_Click(sender As Object, e As EventArgs) Handles btnNavLogout.Click
            Dim confirm = MessageBox.Show("Are you sure you want to sign out of the accommodation portal?", "Confirm Sign Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Me.Close()
            End If
        End Sub
    End Class
End Namespace
