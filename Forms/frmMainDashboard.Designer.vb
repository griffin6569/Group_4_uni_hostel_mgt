Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmMainDashboard
        Inherits System.Windows.Forms.Form

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.pnlSidebar = New System.Windows.Forms.Panel()
            Me.pnlNavMenu = New System.Windows.Forms.Panel()
            Me.btnNavUsers = New System.Windows.Forms.Button()
            Me.btnNavReports = New System.Windows.Forms.Button()
            Me.btnNavMaintenance = New System.Windows.Forms.Button()
            Me.btnNavBilling = New System.Windows.Forms.Button()
            Me.btnNavAllocations = New System.Windows.Forms.Button()
            Me.btnNavBeds = New System.Windows.Forms.Button()
            Me.btnNavRooms = New System.Windows.Forms.Button()
            Me.btnNavHostels = New System.Windows.Forms.Button()
            Me.btnNavStudents = New System.Windows.Forms.Button()
            Me.btnNavDashboard = New System.Windows.Forms.Button()
            Me.pnlUserProfile = New System.Windows.Forms.Panel()
            Me.lblUserRole = New System.Windows.Forms.Label()
            Me.lblUserName = New System.Windows.Forms.Label()
            Me.lblUserInitials = New System.Windows.Forms.Label()
            Me.pnlBrand = New System.Windows.Forms.Panel()
            Me.lblBrandSub = New System.Windows.Forms.Label()
            Me.lblBrandTitle = New System.Windows.Forms.Label()
            Me.picCrest = New System.Windows.Forms.PictureBox()
            Me.pnlLogout = New System.Windows.Forms.Panel()
            Me.btnNavLogout = New System.Windows.Forms.Button()
            Me.pnlTopBar = New System.Windows.Forms.Panel()
            Me.btnRefreshData = New System.Windows.Forms.Button()
            Me.lblClock = New System.Windows.Forms.Label()
            Me.pnlDbStatus = New System.Windows.Forms.Panel()
            Me.lblDbStatus = New System.Windows.Forms.Label()
            Me.dotStatus = New System.Windows.Forms.Panel()
            Me.cboSemester = New System.Windows.Forms.ComboBox()
            Me.lblSemesterPrompt = New System.Windows.Forms.Label()
            Me.lblViewSub = New System.Windows.Forms.Label()
            Me.lblViewTitle = New System.Windows.Forms.Label()
            Me.pnlContentHost = New System.Windows.Forms.Panel()
            Me.pnlGoldAccent = New System.Windows.Forms.Panel()
            Me.tmrClock = New System.Windows.Forms.Timer(Me.components)
            Me.pnlSidebar.SuspendLayout()
            Me.pnlNavMenu.SuspendLayout()
            Me.pnlUserProfile.SuspendLayout()
            Me.pnlBrand.SuspendLayout()
            CType(Me.picCrest, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlLogout.SuspendLayout()
            Me.pnlTopBar.SuspendLayout()
            Me.pnlDbStatus.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlGoldAccent
            '
            Me.pnlGoldAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.pnlGoldAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlGoldAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlGoldAccent.Name = "pnlGoldAccent"
            Me.pnlGoldAccent.Size = New System.Drawing.Size(1300, 3)
            Me.pnlGoldAccent.TabIndex = 0
            '
            'pnlSidebar
            '
            Me.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.pnlSidebar.Controls.Add(Me.pnlNavMenu)
            Me.pnlSidebar.Controls.Add(Me.pnlUserProfile)
            Me.pnlSidebar.Controls.Add(Me.pnlBrand)
            Me.pnlSidebar.Controls.Add(Me.pnlLogout)
            Me.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlSidebar.Location = New System.Drawing.Point(0, 3)
            Me.pnlSidebar.Name = "pnlSidebar"
            Me.pnlSidebar.Size = New System.Drawing.Size(250, 797)
            Me.pnlSidebar.TabIndex = 1
            '
            'pnlBrand
            '
            Me.pnlBrand.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(18, Byte), Integer), CType(CType(32, Byte), Integer))
            Me.pnlBrand.Controls.Add(Me.lblBrandSub)
            Me.pnlBrand.Controls.Add(Me.lblBrandTitle)
            Me.pnlBrand.Controls.Add(Me.picCrest)
            Me.pnlBrand.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlBrand.Location = New System.Drawing.Point(0, 0)
            Me.pnlBrand.Name = "pnlBrand"
            Me.pnlBrand.Padding = New System.Windows.Forms.Padding(16, 12, 16, 12)
            Me.pnlBrand.Size = New System.Drawing.Size(250, 72)
            Me.pnlBrand.TabIndex = 0
            '
            'lblBrandSub
            '
            Me.lblBrandSub.AutoSize = True
            Me.lblBrandSub.Font = New System.Drawing.Font("Segoe UI Semibold", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.lblBrandSub.Location = New System.Drawing.Point(64, 40)
            Me.lblBrandSub.Name = "lblBrandSub"
            Me.lblBrandSub.Size = New System.Drawing.Size(160, 12)
            Me.lblBrandSub.TabIndex = 2
            Me.lblBrandSub.Text = "DEDAN KIMATHI UNIVERSITY"
            Me.lblBrandSub.UseMnemonic = False
            '
            'lblBrandTitle
            '
            Me.lblBrandTitle.AutoSize = True
            Me.lblBrandTitle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandTitle.ForeColor = System.Drawing.Color.White
            Me.lblBrandTitle.Location = New System.Drawing.Point(64, 18)
            Me.lblBrandTitle.Name = "lblBrandTitle"
            Me.lblBrandTitle.Size = New System.Drawing.Size(155, 19)
            Me.lblBrandTitle.TabIndex = 1
            Me.lblBrandTitle.Text = "DeKUT HOSTELS"
            Me.lblBrandTitle.UseMnemonic = False
            '
            'picCrest
            '
            Me.picCrest.Location = New System.Drawing.Point(12, 12)
            Me.picCrest.Name = "picCrest"
            Me.picCrest.Size = New System.Drawing.Size(46, 48)
            Me.picCrest.TabIndex = 0
            Me.picCrest.TabStop = False
            '
            'pnlUserProfile
            '
            Me.pnlUserProfile.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(54, Byte), Integer))
            Me.pnlUserProfile.Controls.Add(Me.lblUserRole)
            Me.pnlUserProfile.Controls.Add(Me.lblUserName)
            Me.pnlUserProfile.Controls.Add(Me.lblUserInitials)
            Me.pnlUserProfile.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlUserProfile.Location = New System.Drawing.Point(0, 72)
            Me.pnlUserProfile.Name = "pnlUserProfile"
            Me.pnlUserProfile.Padding = New System.Windows.Forms.Padding(16, 10, 16, 10)
            Me.pnlUserProfile.Size = New System.Drawing.Size(250, 64)
            Me.pnlUserProfile.TabIndex = 1
            '
            'lblUserRole
            '
            Me.lblUserRole.AutoSize = True
            Me.lblUserRole.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.lblUserRole.Location = New System.Drawing.Point(62, 34)
            Me.lblUserRole.Name = "lblUserRole"
            Me.lblUserRole.Size = New System.Drawing.Size(84, 12)
            Me.lblUserRole.TabIndex = 2
            Me.lblUserRole.Text = "SUPER_ADMIN"
            Me.lblUserRole.UseMnemonic = False
            '
            'lblUserName
            '
            Me.lblUserName.AutoEllipsis = True
            Me.lblUserName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUserName.ForeColor = System.Drawing.Color.White
            Me.lblUserName.Location = New System.Drawing.Point(62, 14)
            Me.lblUserName.Name = "lblUserName"
            Me.lblUserName.Size = New System.Drawing.Size(175, 18)
            Me.lblUserName.TabIndex = 1
            Me.lblUserName.Text = "Arthur Pendelton"
            Me.lblUserName.UseMnemonic = False
            '
            'lblUserInitials
            '
            Me.lblUserInitials.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.lblUserInitials.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUserInitials.ForeColor = System.Drawing.Color.White
            Me.lblUserInitials.Location = New System.Drawing.Point(14, 12)
            Me.lblUserInitials.Name = "lblUserInitials"
            Me.lblUserInitials.Size = New System.Drawing.Size(40, 40)
            Me.lblUserInitials.TabIndex = 0
            Me.lblUserInitials.Text = "AP"
            Me.lblUserInitials.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlNavMenu
            '
            Me.pnlNavMenu.AutoScroll = True
            Me.pnlNavMenu.Controls.Add(Me.btnNavUsers)
            Me.pnlNavMenu.Controls.Add(Me.btnNavReports)
            Me.pnlNavMenu.Controls.Add(Me.btnNavMaintenance)
            Me.pnlNavMenu.Controls.Add(Me.btnNavBilling)
            Me.pnlNavMenu.Controls.Add(Me.btnNavAllocations)
            Me.pnlNavMenu.Controls.Add(Me.btnNavBeds)
            Me.pnlNavMenu.Controls.Add(Me.btnNavRooms)
            Me.pnlNavMenu.Controls.Add(Me.btnNavHostels)
            Me.pnlNavMenu.Controls.Add(Me.btnNavStudents)
            Me.pnlNavMenu.Controls.Add(Me.btnNavDashboard)
            Me.pnlNavMenu.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlNavMenu.Location = New System.Drawing.Point(0, 136)
            Me.pnlNavMenu.Name = "pnlNavMenu"
            Me.pnlNavMenu.Padding = New System.Windows.Forms.Padding(0, 8, 0, 8)
            Me.pnlNavMenu.Size = New System.Drawing.Size(250, 609)
            Me.pnlNavMenu.TabIndex = 2
            '
            'btnNavDashboard
            '
            Me.btnNavDashboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.btnNavDashboard.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavDashboard.FlatAppearance.BorderSize = 0
            Me.btnNavDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavDashboard.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnNavDashboard.ForeColor = System.Drawing.Color.White
            Me.btnNavDashboard.Location = New System.Drawing.Point(0, 8)
            Me.btnNavDashboard.Name = "btnNavDashboard"
            Me.btnNavDashboard.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavDashboard.Size = New System.Drawing.Size(250, 44)
            Me.btnNavDashboard.TabIndex = 0
            Me.btnNavDashboard.Text = "📊  Executive Dashboard"
            Me.btnNavDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavDashboard.UseVisualStyleBackColor = False
            '
            'btnNavStudents
            '
            Me.btnNavStudents.BackColor = System.Drawing.Color.Transparent
            Me.btnNavStudents.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavStudents.FlatAppearance.BorderSize = 0
            Me.btnNavStudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavStudents.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavStudents.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavStudents.Location = New System.Drawing.Point(0, 52)
            Me.btnNavStudents.Name = "btnNavStudents"
            Me.btnNavStudents.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavStudents.Size = New System.Drawing.Size(250, 44)
            Me.btnNavStudents.TabIndex = 1
            Me.btnNavStudents.Text = "🎓  Student Residents"
            Me.btnNavStudents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavStudents.UseVisualStyleBackColor = False
            '
            'btnNavHostels
            '
            Me.btnNavHostels.BackColor = System.Drawing.Color.Transparent
            Me.btnNavHostels.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavHostels.FlatAppearance.BorderSize = 0
            Me.btnNavHostels.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavHostels.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavHostels.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavHostels.Location = New System.Drawing.Point(0, 96)
            Me.btnNavHostels.Name = "btnNavHostels"
            Me.btnNavHostels.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavHostels.Size = New System.Drawing.Size(250, 44)
            Me.btnNavHostels.TabIndex = 2
            Me.btnNavHostels.Text = "🏢  Hostel Facilities"
            Me.btnNavHostels.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavHostels.UseVisualStyleBackColor = False
            '
            'btnNavRooms
            '
            Me.btnNavRooms.BackColor = System.Drawing.Color.Transparent
            Me.btnNavRooms.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavRooms.FlatAppearance.BorderSize = 0
            Me.btnNavRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavRooms.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavRooms.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavRooms.Location = New System.Drawing.Point(0, 140)
            Me.btnNavRooms.Name = "btnNavRooms"
            Me.btnNavRooms.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavRooms.Size = New System.Drawing.Size(250, 44)
            Me.btnNavRooms.TabIndex = 3
            Me.btnNavRooms.Text = "🚪  Rooms & Capacity"
            Me.btnNavRooms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavRooms.UseVisualStyleBackColor = False
            Me.btnNavRooms.UseMnemonic = False
            '
            'btnNavBeds
            '
            Me.btnNavBeds.BackColor = System.Drawing.Color.Transparent
            Me.btnNavBeds.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavBeds.FlatAppearance.BorderSize = 0
            Me.btnNavBeds.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavBeds.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavBeds.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavBeds.Location = New System.Drawing.Point(0, 184)
            Me.btnNavBeds.Name = "btnNavBeds"
            Me.btnNavBeds.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavBeds.Size = New System.Drawing.Size(250, 44)
            Me.btnNavBeds.TabIndex = 4
            Me.btnNavBeds.Text = "🛏️  Bed Space Matrix"
            Me.btnNavBeds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavBeds.UseVisualStyleBackColor = False
            '
            'btnNavAllocations
            '
            Me.btnNavAllocations.BackColor = System.Drawing.Color.Transparent
            Me.btnNavAllocations.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavAllocations.FlatAppearance.BorderSize = 0
            Me.btnNavAllocations.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavAllocations.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavAllocations.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavAllocations.Location = New System.Drawing.Point(0, 228)
            Me.btnNavAllocations.Name = "btnNavAllocations"
            Me.btnNavAllocations.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavAllocations.Size = New System.Drawing.Size(250, 44)
            Me.btnNavAllocations.TabIndex = 5
            Me.btnNavAllocations.Text = "📋  Bed Allocations"
            Me.btnNavAllocations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavAllocations.UseVisualStyleBackColor = False
            '
            'btnNavBilling
            '
            Me.btnNavBilling.BackColor = System.Drawing.Color.Transparent
            Me.btnNavBilling.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavBilling.FlatAppearance.BorderSize = 0
            Me.btnNavBilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavBilling.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavBilling.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavBilling.Location = New System.Drawing.Point(0, 272)
            Me.btnNavBilling.Name = "btnNavBilling"
            Me.btnNavBilling.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavBilling.Size = New System.Drawing.Size(250, 44)
            Me.btnNavBilling.TabIndex = 6
            Me.btnNavBilling.Text = "💳  Billing & Payments"
            Me.btnNavBilling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavBilling.UseVisualStyleBackColor = False
            Me.btnNavBilling.UseMnemonic = False
            '
            'btnNavMaintenance
            '
            Me.btnNavMaintenance.BackColor = System.Drawing.Color.Transparent
            Me.btnNavMaintenance.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavMaintenance.FlatAppearance.BorderSize = 0
            Me.btnNavMaintenance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavMaintenance.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavMaintenance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavMaintenance.Location = New System.Drawing.Point(0, 316)
            Me.btnNavMaintenance.Name = "btnNavMaintenance"
            Me.btnNavMaintenance.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavMaintenance.Size = New System.Drawing.Size(250, 44)
            Me.btnNavMaintenance.TabIndex = 7
            Me.btnNavMaintenance.Text = "🔧  Maintenance Desk"
            Me.btnNavMaintenance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavMaintenance.UseVisualStyleBackColor = False
            Me.btnNavMaintenance.UseMnemonic = False
            '
            'btnNavReports
            '
            Me.btnNavReports.BackColor = System.Drawing.Color.Transparent
            Me.btnNavReports.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavReports.FlatAppearance.BorderSize = 0
            Me.btnNavReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavReports.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavReports.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavReports.Location = New System.Drawing.Point(0, 360)
            Me.btnNavReports.Name = "btnNavReports"
            Me.btnNavReports.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavReports.Size = New System.Drawing.Size(250, 44)
            Me.btnNavReports.TabIndex = 8
            Me.btnNavReports.Text = "📈  Reports & Analytics"
            Me.btnNavReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavReports.UseVisualStyleBackColor = False
            Me.btnNavReports.UseMnemonic = False
            '
            'btnNavUsers
            '
            Me.btnNavUsers.BackColor = System.Drawing.Color.Transparent
            Me.btnNavUsers.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnNavUsers.FlatAppearance.BorderSize = 0
            Me.btnNavUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavUsers.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnNavUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnNavUsers.Location = New System.Drawing.Point(0, 404)
            Me.btnNavUsers.Name = "btnNavUsers"
            Me.btnNavUsers.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
            Me.btnNavUsers.Size = New System.Drawing.Size(250, 44)
            Me.btnNavUsers.TabIndex = 9
            Me.btnNavUsers.Text = "⚙️  User Administration"
            Me.btnNavUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnNavUsers.UseVisualStyleBackColor = False
            '
            'pnlLogout
            '
            Me.pnlLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(18, Byte), Integer), CType(CType(32, Byte), Integer))
            Me.pnlLogout.Controls.Add(Me.btnNavLogout)
            Me.pnlLogout.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlLogout.Location = New System.Drawing.Point(0, 745)
            Me.pnlLogout.Name = "pnlLogout"
            Me.pnlLogout.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
            Me.pnlLogout.Size = New System.Drawing.Size(250, 52)
            Me.pnlLogout.TabIndex = 3
            '
            'btnNavLogout
            '
            Me.btnNavLogout.BackColor = System.Drawing.Color.Transparent
            Me.btnNavLogout.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnNavLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(130, Byte), Integer))
            Me.btnNavLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNavLogout.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnNavLogout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
            Me.btnNavLogout.Location = New System.Drawing.Point(12, 8)
            Me.btnNavLogout.Name = "btnNavLogout"
            Me.btnNavLogout.Size = New System.Drawing.Size(226, 36)
            Me.btnNavLogout.TabIndex = 0
            Me.btnNavLogout.Text = "🔒  Sign Out of Portal"
            Me.btnNavLogout.UseVisualStyleBackColor = False
            '
            'pnlTopBar
            '
            Me.pnlTopBar.BackColor = System.Drawing.Color.White
            Me.pnlTopBar.Controls.Add(Me.btnRefreshData)
            Me.pnlTopBar.Controls.Add(Me.lblClock)
            Me.pnlTopBar.Controls.Add(Me.pnlDbStatus)
            Me.pnlTopBar.Controls.Add(Me.cboSemester)
            Me.pnlTopBar.Controls.Add(Me.lblSemesterPrompt)
            Me.pnlTopBar.Controls.Add(Me.lblViewSub)
            Me.pnlTopBar.Controls.Add(Me.lblViewTitle)
            Me.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopBar.Location = New System.Drawing.Point(250, 3)
            Me.pnlTopBar.Name = "pnlTopBar"
            Me.pnlTopBar.Padding = New System.Windows.Forms.Padding(24, 12, 24, 12)
            Me.pnlTopBar.Size = New System.Drawing.Size(1050, 64)
            Me.pnlTopBar.TabIndex = 2
            '
            'btnRefreshData
            '
            Me.btnRefreshData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRefreshData.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.btnRefreshData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnRefreshData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRefreshData.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnRefreshData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.btnRefreshData.Location = New System.Drawing.Point(955, 17)
            Me.btnRefreshData.Name = "btnRefreshData"
            Me.btnRefreshData.Size = New System.Drawing.Size(75, 30)
            Me.btnRefreshData.TabIndex = 6
            Me.btnRefreshData.Text = "↻ Refresh"
            Me.btnRefreshData.UseVisualStyleBackColor = False
            '
            'lblClock
            '
            Me.lblClock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblClock.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblClock.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblClock.Location = New System.Drawing.Point(795, 23)
            Me.lblClock.Name = "lblClock"
            Me.lblClock.Size = New System.Drawing.Size(150, 18)
            Me.lblClock.TabIndex = 5
            Me.lblClock.Text = "2026-11-23 15:30"
            Me.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'pnlDbStatus
            '
            Me.pnlDbStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlDbStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(244, Byte), Integer))
            Me.pnlDbStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlDbStatus.Controls.Add(Me.lblDbStatus)
            Me.pnlDbStatus.Controls.Add(Me.dotStatus)
            Me.pnlDbStatus.Location = New System.Drawing.Point(670, 19)
            Me.pnlDbStatus.Name = "pnlDbStatus"
            Me.pnlDbStatus.Size = New System.Drawing.Size(115, 26)
            Me.pnlDbStatus.TabIndex = 4
            '
            'lblDbStatus
            '
            Me.lblDbStatus.AutoSize = True
            Me.lblDbStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDbStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(52, Byte), Integer))
            Me.lblDbStatus.Location = New System.Drawing.Point(18, 5)
            Me.lblDbStatus.Name = "lblDbStatus"
            Me.lblDbStatus.Size = New System.Drawing.Size(89, 13)
            Me.lblDbStatus.TabIndex = 1
            Me.lblDbStatus.Text = "SQL Server 2025"
            '
            'dotStatus
            '
            Me.dotStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(74, Byte), Integer))
            Me.dotStatus.Location = New System.Drawing.Point(6, 9)
            Me.dotStatus.Name = "dotStatus"
            Me.dotStatus.Size = New System.Drawing.Size(7, 7)
            Me.dotStatus.TabIndex = 0
            '
            'cboSemester
            '
            Me.cboSemester.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cboSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSemester.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboSemester.FormattingEnabled = True
            Me.cboSemester.Location = New System.Drawing.Point(440, 20)
            Me.cboSemester.Name = "cboSemester"
            Me.cboSemester.Size = New System.Drawing.Size(220, 23)
            Me.cboSemester.TabIndex = 3
            '
            'lblSemesterPrompt
            '
            Me.lblSemesterPrompt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblSemesterPrompt.AutoSize = True
            Me.lblSemesterPrompt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblSemesterPrompt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblSemesterPrompt.Location = New System.Drawing.Point(365, 24)
            Me.lblSemesterPrompt.Name = "lblSemesterPrompt"
            Me.lblSemesterPrompt.Size = New System.Drawing.Size(72, 15)
            Me.lblSemesterPrompt.TabIndex = 2
            Me.lblSemesterPrompt.Text = "Active Term:"
            '
            'lblViewSub
            '
            Me.lblViewSub.AutoSize = True
            Me.lblViewSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblViewSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblViewSub.Location = New System.Drawing.Point(22, 36)
            Me.lblViewSub.Name = "lblViewSub"
            Me.lblViewSub.Size = New System.Drawing.Size(268, 15)
            Me.lblViewSub.TabIndex = 1
            Me.lblViewSub.Text = "Real-Time Campus Accommodation Operations"
            Me.lblViewSub.UseMnemonic = False
            '
            'lblViewTitle
            '
            Me.lblViewTitle.AutoSize = True
            Me.lblViewTitle.Font = New System.Drawing.Font("Segoe UI", 12.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblViewTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblViewTitle.Location = New System.Drawing.Point(20, 11)
            Me.lblViewTitle.Name = "lblViewTitle"
            Me.lblViewTitle.Size = New System.Drawing.Size(244, 23)
            Me.lblViewTitle.TabIndex = 0
            Me.lblViewTitle.Text = "Executive Dashboard Overview"
            Me.lblViewTitle.UseMnemonic = False
            '
            'pnlContentHost
            '
            Me.pnlContentHost.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlContentHost.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContentHost.Location = New System.Drawing.Point(250, 67)
            Me.pnlContentHost.Name = "pnlContentHost"
            Me.pnlContentHost.Padding = New System.Windows.Forms.Padding(20)
            Me.pnlContentHost.Size = New System.Drawing.Size(1050, 733)
            Me.pnlContentHost.TabIndex = 3
            '
            'tmrClock
            '
            Me.tmrClock.Enabled = True
            Me.tmrClock.Interval = 1000
            '
            'frmMainDashboard
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(1300, 800)
            Me.Controls.Add(Me.pnlContentHost)
            Me.Controls.Add(Me.pnlTopBar)
            Me.Controls.Add(Me.pnlSidebar)
            Me.Controls.Add(Me.pnlGoldAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.MinimumSize = New System.Drawing.Size(1100, 700)
            Me.Name = "frmMainDashboard"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "University Hostel Management System — Prestige Academic Edition"
            Me.pnlSidebar.ResumeLayout(False)
            Me.pnlNavMenu.ResumeLayout(False)
            Me.pnlUserProfile.ResumeLayout(False)
            Me.pnlUserProfile.PerformLayout()
            Me.pnlBrand.ResumeLayout(False)
            Me.pnlBrand.PerformLayout()
            CType(Me.picCrest, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlLogout.ResumeLayout(False)
            Me.pnlTopBar.ResumeLayout(False)
            Me.pnlTopBar.PerformLayout()
            Me.pnlDbStatus.ResumeLayout(False)
            Me.pnlDbStatus.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlGoldAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlSidebar As System.Windows.Forms.Panel
        Friend WithEvents pnlBrand As System.Windows.Forms.Panel
        Friend WithEvents picCrest As System.Windows.Forms.PictureBox
        Friend WithEvents lblBrandTitle As System.Windows.Forms.Label
        Friend WithEvents lblBrandSub As System.Windows.Forms.Label
        Friend WithEvents pnlUserProfile As System.Windows.Forms.Panel
        Friend WithEvents lblUserInitials As System.Windows.Forms.Label
        Friend WithEvents lblUserName As System.Windows.Forms.Label
        Friend WithEvents lblUserRole As System.Windows.Forms.Label
        Friend WithEvents pnlNavMenu As System.Windows.Forms.Panel
        Friend WithEvents btnNavDashboard As System.Windows.Forms.Button
        Friend WithEvents btnNavStudents As System.Windows.Forms.Button
        Friend WithEvents btnNavHostels As System.Windows.Forms.Button
        Friend WithEvents btnNavRooms As System.Windows.Forms.Button
        Friend WithEvents btnNavBeds As System.Windows.Forms.Button
        Friend WithEvents btnNavAllocations As System.Windows.Forms.Button
        Friend WithEvents btnNavBilling As System.Windows.Forms.Button
        Friend WithEvents btnNavMaintenance As System.Windows.Forms.Button
        Friend WithEvents btnNavReports As System.Windows.Forms.Button
        Friend WithEvents btnNavUsers As System.Windows.Forms.Button
        Friend WithEvents pnlLogout As System.Windows.Forms.Panel
        Friend WithEvents btnNavLogout As System.Windows.Forms.Button
        Friend WithEvents pnlTopBar As System.Windows.Forms.Panel
        Friend WithEvents lblViewTitle As System.Windows.Forms.Label
        Friend WithEvents lblViewSub As System.Windows.Forms.Label
        Friend WithEvents lblSemesterPrompt As System.Windows.Forms.Label
        Friend WithEvents cboSemester As System.Windows.Forms.ComboBox
        Friend WithEvents pnlDbStatus As System.Windows.Forms.Panel
        Friend WithEvents dotStatus As System.Windows.Forms.Panel
        Friend WithEvents lblDbStatus As System.Windows.Forms.Label
        Friend WithEvents lblClock As System.Windows.Forms.Label
        Friend WithEvents btnRefreshData As System.Windows.Forms.Button
        Friend WithEvents pnlContentHost As System.Windows.Forms.Panel
        Friend WithEvents tmrClock As System.Windows.Forms.Timer
    End Class
End Namespace

