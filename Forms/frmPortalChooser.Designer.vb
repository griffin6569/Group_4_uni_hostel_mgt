Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmPortalChooser
        Inherits System.Windows.Forms.Form

        <System.Diagnostics.DebuggerNonUserCode()>
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
            Me.pnlTopAccent = New System.Windows.Forms.Panel()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.btnMinimize = New System.Windows.Forms.Button()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.lblHeaderTitle = New System.Windows.Forms.Label()
            Me.picLogo = New System.Windows.Forms.PictureBox()
            Me.lblUnivName = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.pnlStudentCard = New University_hostel_mgt.Controls.GlassPanel()
            Me.badgeStudent = New System.Windows.Forms.Label()
            Me.lblStudentTitle = New System.Windows.Forms.Label()
            Me.lblStudentDesc = New System.Windows.Forms.Label()
            Me.lblStudentFeatures = New System.Windows.Forms.Label()
            Me.btnStudentPortal = New University_hostel_mgt.Controls.GlassButton()
            Me.pnlStaffCard = New University_hostel_mgt.Controls.GlassPanel()
            Me.badgeStaff = New System.Windows.Forms.Label()
            Me.lblStaffTitle = New System.Windows.Forms.Label()
            Me.lblStaffDesc = New System.Windows.Forms.Label()
            Me.lblStaffFeatures = New System.Windows.Forms.Label()
            Me.btnStaffPortal = New University_hostel_mgt.Controls.GlassButton()
            Me.pnlDbBadge = New University_hostel_mgt.Controls.GlassPanel()
            Me.lblDbDot = New System.Windows.Forms.Label()
            Me.lblDbText = New System.Windows.Forms.Label()
            Me.btnExit = New University_hostel_mgt.Controls.GlassButton()
            Me.pnlHeader.SuspendLayout()
            CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlStudentCard.SuspendLayout()
            Me.pnlStaffCard.SuspendLayout()
            Me.pnlDbBadge.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(880, 3)
            Me.pnlTopAccent.TabIndex = 0
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(26, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.btnMinimize)
            Me.pnlHeader.Controls.Add(Me.btnClose)
            Me.pnlHeader.Controls.Add(Me.lblHeaderTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 3)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(880, 32)
            Me.pnlHeader.TabIndex = 1
            '
            'btnMinimize
            '
            Me.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnMinimize.FlatAppearance.BorderSize = 0
            Me.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnMinimize.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnMinimize.Location = New System.Drawing.Point(800, 0)
            Me.btnMinimize.Name = "btnMinimize"
            Me.btnMinimize.Size = New System.Drawing.Size(40, 32)
            Me.btnMinimize.TabIndex = 2
            Me.btnMinimize.Text = "—"
            Me.btnMinimize.UseVisualStyleBackColor = True
            '
            'btnClose
            '
            Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnClose.FlatAppearance.BorderSize = 0
            Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnClose.Location = New System.Drawing.Point(840, 0)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(40, 32)
            Me.btnClose.TabIndex = 1
            Me.btnClose.Text = "✕"
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'lblHeaderTitle
            '
            Me.lblHeaderTitle.AutoSize = True
            Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblHeaderTitle.Location = New System.Drawing.Point(14, 8)
            Me.lblHeaderTitle.Name = "lblHeaderTitle"
            Me.lblHeaderTitle.Size = New System.Drawing.Size(420, 15)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "Dedan Kimathi University of Technology • Residential Accommodation Gateway"
            '
            'picLogo
            '
            Me.picLogo.BackColor = System.Drawing.Color.Transparent
            Me.picLogo.Location = New System.Drawing.Point(404, 46)
            Me.picLogo.Name = "picLogo"
            Me.picLogo.Size = New System.Drawing.Size(72, 72)
            Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picLogo.TabIndex = 2
            Me.picLogo.TabStop = False
            '
            'lblUnivName
            '
            Me.lblUnivName.BackColor = System.Drawing.Color.Transparent
            Me.lblUnivName.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUnivName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.lblUnivName.Location = New System.Drawing.Point(40, 122)
            Me.lblUnivName.Name = "lblUnivName"
            Me.lblUnivName.Size = New System.Drawing.Size(800, 22)
            Me.lblUnivName.TabIndex = 3
            Me.lblUnivName.Text = "DEDAN KIMATHI UNIVERSITY OF TECHNOLOGY"
            Me.lblUnivName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblTitle
            '
            Me.lblTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(40, 146)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(800, 32)
            Me.lblTitle.TabIndex = 4
            Me.lblTitle.Text = "HOSTEL MANAGEMENT SYSTEM"
            Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblSubtitle
            '
            Me.lblSubtitle.BackColor = System.Drawing.Color.Transparent
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(40, 180)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(800, 20)
            Me.lblSubtitle.TabIndex = 5
            Me.lblSubtitle.Text = "Please select your institutional portal to sign in or manage accommodation services"
            Me.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlStudentCard
            '
            Me.pnlStudentCard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlStudentCard.BorderWidth = 1.0!
            Me.pnlStudentCard.Controls.Add(Me.badgeStudent)
            Me.pnlStudentCard.Controls.Add(Me.lblStudentTitle)
            Me.pnlStudentCard.Controls.Add(Me.lblStudentDesc)
            Me.pnlStudentCard.Controls.Add(Me.lblStudentFeatures)
            Me.pnlStudentCard.Controls.Add(Me.btnStudentPortal)
            Me.pnlStudentCard.CornerRadius = 16
            Me.pnlStudentCard.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.pnlStudentCard.GlowColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.pnlStudentCard.Location = New System.Drawing.Point(50, 215)
            Me.pnlStudentCard.Name = "pnlStudentCard"
            Me.pnlStudentCard.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlStudentCard.Size = New System.Drawing.Size(375, 275)
            Me.pnlStudentCard.TabIndex = 6
            '
            'badgeStudent
            '
            Me.badgeStudent.AutoSize = True
            Me.badgeStudent.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.badgeStudent.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.badgeStudent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.badgeStudent.Location = New System.Drawing.Point(24, 20)
            Me.badgeStudent.Name = "badgeStudent"
            Me.badgeStudent.Padding = New System.Windows.Forms.Padding(8, 3, 8, 3)
            Me.badgeStudent.Size = New System.Drawing.Size(145, 19)
            Me.badgeStudent.TabIndex = 0
            Me.badgeStudent.Text = "🎓  STUDENT HOUSING"
            '
            'lblStudentTitle
            '
            Me.lblStudentTitle.AutoSize = True
            Me.lblStudentTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblStudentTitle.Font = New System.Drawing.Font("Segoe UI", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStudentTitle.ForeColor = System.Drawing.Color.White
            Me.lblStudentTitle.Location = New System.Drawing.Point(22, 48)
            Me.lblStudentTitle.Name = "lblStudentTitle"
            Me.lblStudentTitle.Size = New System.Drawing.Size(222, 25)
            Me.lblStudentTitle.TabIndex = 1
            Me.lblStudentTitle.Text = "Student Residential Portal"
            '
            'lblStudentDesc
            '
            Me.lblStudentDesc.BackColor = System.Drawing.Color.Transparent
            Me.lblStudentDesc.Font = New System.Drawing.Font("Segoe UI", 8.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStudentDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblStudentDesc.Location = New System.Drawing.Point(24, 78)
            Me.lblStudentDesc.Name = "lblStudentDesc"
            Me.lblStudentDesc.Size = New System.Drawing.Size(327, 62)
            Me.lblStudentDesc.TabIndex = 2
            Me.lblStudentDesc.Text = "Book available hostel rooms, view active residency, discover fellow roommates, se" &
    "ttle semester fees with M-Pesa, and report room maintenance issues."
            '
            'lblStudentFeatures
            '
            Me.lblStudentFeatures.BackColor = System.Drawing.Color.Transparent
            Me.lblStudentFeatures.Font = New System.Drawing.Font("Segoe UI Semibold", 8.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStudentFeatures.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblStudentFeatures.Location = New System.Drawing.Point(24, 144)
            Me.lblStudentFeatures.Name = "lblStudentFeatures"
            Me.lblStudentFeatures.Size = New System.Drawing.Size(327, 54)
            Me.lblStudentFeatures.TabIndex = 3
            Me.lblStudentFeatures.Text = "✓ Official @students.dkut.ac.ke email required" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "✓ Real-time bed reservation & sta" &
    "tus" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "✓ Instant electronic M-Pesa receipts"
            '
            'btnStudentPortal
            '
            Me.btnStudentPortal.CornerRadius = 10
            Me.btnStudentPortal.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnStudentPortal.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnStudentPortal.HoverColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.btnStudentPortal.Location = New System.Drawing.Point(24, 212)
            Me.btnStudentPortal.Name = "btnStudentPortal"
            Me.btnStudentPortal.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.btnStudentPortal.Size = New System.Drawing.Size(327, 42)
            Me.btnStudentPortal.TabIndex = 4
            Me.btnStudentPortal.Text = "Access Student Portal  ➔"
            '
            'pnlStaffCard
            '
            Me.pnlStaffCard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlStaffCard.BorderWidth = 1.0!
            Me.pnlStaffCard.Controls.Add(Me.badgeStaff)
            Me.pnlStaffCard.Controls.Add(Me.lblStaffTitle)
            Me.pnlStaffCard.Controls.Add(Me.lblStaffDesc)
            Me.pnlStaffCard.Controls.Add(Me.lblStaffFeatures)
            Me.pnlStaffCard.Controls.Add(Me.btnStaffPortal)
            Me.pnlStaffCard.CornerRadius = 16
            Me.pnlStaffCard.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.pnlStaffCard.GlowColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.pnlStaffCard.Location = New System.Drawing.Point(455, 215)
            Me.pnlStaffCard.Name = "pnlStaffCard"
            Me.pnlStaffCard.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlStaffCard.Size = New System.Drawing.Size(375, 275)
            Me.pnlStaffCard.TabIndex = 7
            '
            'badgeStaff
            '
            Me.badgeStaff.AutoSize = True
            Me.badgeStaff.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.badgeStaff.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.badgeStaff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.badgeStaff.Location = New System.Drawing.Point(24, 20)
            Me.badgeStaff.Name = "badgeStaff"
            Me.badgeStaff.Padding = New System.Windows.Forms.Padding(8, 3, 8, 3)
            Me.badgeStaff.Size = New System.Drawing.Size(126, 19)
            Me.badgeStaff.TabIndex = 0
            Me.badgeStaff.Text = "🏛️  STAFF GATEWAY"
            '
            'lblStaffTitle
            '
            Me.lblStaffTitle.AutoSize = True
            Me.lblStaffTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblStaffTitle.Font = New System.Drawing.Font("Segoe UI", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStaffTitle.ForeColor = System.Drawing.Color.White
            Me.lblStaffTitle.Location = New System.Drawing.Point(22, 48)
            Me.lblStaffTitle.Name = "lblStaffTitle"
            Me.lblStaffTitle.Size = New System.Drawing.Size(250, 25)
            Me.lblStaffTitle.TabIndex = 1
            Me.lblStaffTitle.Text = "Staff Administration Gateway"
            '
            'lblStaffDesc
            '
            Me.lblStaffDesc.BackColor = System.Drawing.Color.Transparent
            Me.lblStaffDesc.Font = New System.Drawing.Font("Segoe UI", 8.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStaffDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblStaffDesc.Location = New System.Drawing.Point(24, 78)
            Me.lblStaffDesc.Name = "lblStaffDesc"
            Me.lblStaffDesc.Size = New System.Drawing.Size(327, 62)
            Me.lblStaffDesc.TabIndex = 2
            Me.lblStaffDesc.Text = "Administrative console for Accommodation Officers, Hall Wardens, Bursar Finance C" &
    "lerks, and System Administrators to manage residence operations."
            '
            'lblStaffFeatures
            '
            Me.lblStaffFeatures.BackColor = System.Drawing.Color.Transparent
            Me.lblStaffFeatures.Font = New System.Drawing.Font("Segoe UI Semibold", 8.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStaffFeatures.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblStaffFeatures.Location = New System.Drawing.Point(24, 144)
            Me.lblStaffFeatures.Name = "lblStaffFeatures"
            Me.lblStaffFeatures.Size = New System.Drawing.Size(327, 54)
            Me.lblStaffFeatures.TabIndex = 3
            Me.lblStaffFeatures.Text = "✓ Campus-wide bed allocation matrix" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "✓ Bursar payment ledgers & audit trail" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "✓" &
    " Facility maintenance incident tracking"
            '
            'btnStaffPortal
            '
            Me.btnStaffPortal.CornerRadius = 10
            Me.btnStaffPortal.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnStaffPortal.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnStaffPortal.HoverColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(36, Byte), Integer))
            Me.btnStaffPortal.Location = New System.Drawing.Point(24, 212)
            Me.btnStaffPortal.Name = "btnStaffPortal"
            Me.btnStaffPortal.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.btnStaffPortal.Size = New System.Drawing.Size(327, 42)
            Me.btnStaffPortal.TabIndex = 4
            Me.btnStaffPortal.Text = "Access Staff Gateway  ➔"
            '
            'pnlDbBadge
            '
            Me.pnlDbBadge.BorderColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlDbBadge.BorderWidth = 1.0!
            Me.pnlDbBadge.Controls.Add(Me.lblDbDot)
            Me.pnlDbBadge.Controls.Add(Me.lblDbText)
            Me.pnlDbBadge.CornerRadius = 8
            Me.pnlDbBadge.FillColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.pnlDbBadge.Location = New System.Drawing.Point(50, 508)
            Me.pnlDbBadge.Name = "pnlDbBadge"
            Me.pnlDbBadge.Size = New System.Drawing.Size(260, 28)
            Me.pnlDbBadge.TabIndex = 8
            '
            'lblDbDot
            '
            Me.lblDbDot.BackColor = System.Drawing.Color.Transparent
            Me.lblDbDot.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDbDot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblDbDot.Location = New System.Drawing.Point(8, 4)
            Me.lblDbDot.Name = "lblDbDot"
            Me.lblDbDot.Size = New System.Drawing.Size(16, 20)
            Me.lblDbDot.TabIndex = 0
            Me.lblDbDot.Text = "●"
            '
            'lblDbText
            '
            Me.lblDbText.AutoSize = True
            Me.lblDbText.BackColor = System.Drawing.Color.Transparent
            Me.lblDbText.Font = New System.Drawing.Font("Consolas", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblDbText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblDbText.Location = New System.Drawing.Point(26, 7)
            Me.lblDbText.Name = "lblDbText"
            Me.lblDbText.Size = New System.Drawing.Size(210, 14)
            Me.lblDbText.TabIndex = 1
            Me.lblDbText.Text = "Database: Connected [SQL Server]"
            '
            'btnExit
            '
            Me.btnExit.CornerRadius = 8
            Me.btnExit.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnExit.HoverColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.btnExit.Location = New System.Drawing.Point(710, 508)
            Me.btnExit.Name = "btnExit"
            Me.btnExit.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnExit.Size = New System.Drawing.Size(120, 28)
            Me.btnExit.TabIndex = 9
            Me.btnExit.Text = "Exit Application"
            '
            'frmPortalChooser
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(880, 560)
            Me.Controls.Add(Me.btnExit)
            Me.Controls.Add(Me.pnlDbBadge)
            Me.Controls.Add(Me.pnlStaffCard)
            Me.Controls.Add(Me.pnlStudentCard)
            Me.Controls.Add(Me.lblSubtitle)
            Me.Controls.Add(Me.lblTitle)
            Me.Controls.Add(Me.lblUnivName)
            Me.Controls.Add(Me.picLogo)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmPortalChooser"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Dedan Kimathi University of Technology - Portal Selection"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlStudentCard.ResumeLayout(False)
            Me.pnlStudentCard.PerformLayout()
            Me.pnlStaffCard.ResumeLayout(False)
            Me.pnlStaffCard.PerformLayout()
            Me.pnlDbBadge.ResumeLayout(False)
            Me.pnlDbBadge.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents btnMinimize As System.Windows.Forms.Button
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents picLogo As System.Windows.Forms.PictureBox
        Friend WithEvents lblUnivName As System.Windows.Forms.Label
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents pnlStudentCard As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents badgeStudent As System.Windows.Forms.Label
        Friend WithEvents lblStudentTitle As System.Windows.Forms.Label
        Friend WithEvents lblStudentDesc As System.Windows.Forms.Label
        Friend WithEvents lblStudentFeatures As System.Windows.Forms.Label
        Friend WithEvents btnStudentPortal As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents pnlStaffCard As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents badgeStaff As System.Windows.Forms.Label
        Friend WithEvents lblStaffTitle As System.Windows.Forms.Label
        Friend WithEvents lblStaffDesc As System.Windows.Forms.Label
        Friend WithEvents lblStaffFeatures As System.Windows.Forms.Label
        Friend WithEvents btnStaffPortal As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents pnlDbBadge As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblDbDot As System.Windows.Forms.Label
        Friend WithEvents lblDbText As System.Windows.Forms.Label
        Friend WithEvents btnExit As University_hostel_mgt.Controls.GlassButton
    End Class
End Namespace

