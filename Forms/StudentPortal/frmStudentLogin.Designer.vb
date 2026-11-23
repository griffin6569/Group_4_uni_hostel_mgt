Namespace Forms.StudentPortal
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmStudentLogin
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
            Me.pnlRootAmbient = New System.Windows.Forms.Panel()
            Me.pnlLeftBrand = New University_hostel_mgt.Controls.GlassPanel()
            Me.pnlDbStatusBadge = New University_hostel_mgt.Controls.GlassPanel()
            Me.lblDbStatusDot = New System.Windows.Forms.Label()
            Me.lblDbStatusText = New System.Windows.Forms.Label()
            Me.lblBrandMission = New System.Windows.Forms.Label()
            Me.lblBrandDirectorate = New System.Windows.Forms.Label()
            Me.lblBrandUniv = New System.Windows.Forms.Label()
            Me.picBrandLogo = New System.Windows.Forms.PictureBox()
            Me.pnlLoginCard = New University_hostel_mgt.Controls.GlassPanel()
            Me.btnBackToChooser = New University_hostel_mgt.Controls.GlassButton()
            Me.btnRegister = New University_hostel_mgt.Controls.GlassButton()
            Me.btnLogin = New University_hostel_mgt.Controls.GlassButton()
            Me.lblStatusMessage = New System.Windows.Forms.Label()
            Me.txtPassword = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.txtIdentifier = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblIdentifier = New System.Windows.Forms.Label()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlRootAmbient.SuspendLayout()
            Me.pnlLeftBrand.SuspendLayout()
            Me.pnlDbStatusBadge.SuspendLayout()
            CType(Me.picBrandLogo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlLoginCard.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(860, 3)
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
            Me.pnlHeader.Size = New System.Drawing.Size(860, 32)
            Me.pnlHeader.TabIndex = 1
            '
            'btnMinimize
            '
            Me.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnMinimize.FlatAppearance.BorderSize = 0
            Me.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnMinimize.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnMinimize.Location = New System.Drawing.Point(780, 0)
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
            Me.btnClose.Location = New System.Drawing.Point(820, 0)
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
            Me.lblHeaderTitle.Size = New System.Drawing.Size(370, 15)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "Dedan Kimathi University of Technology • Student Residential Portal"
            '
            'pnlRootAmbient
            '
            Me.pnlRootAmbient.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.pnlRootAmbient.Controls.Add(Me.pnlLeftBrand)
            Me.pnlRootAmbient.Controls.Add(Me.pnlLoginCard)
            Me.pnlRootAmbient.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlRootAmbient.Location = New System.Drawing.Point(0, 35)
            Me.pnlRootAmbient.Name = "pnlRootAmbient"
            Me.pnlRootAmbient.Padding = New System.Windows.Forms.Padding(18)
            Me.pnlRootAmbient.Size = New System.Drawing.Size(860, 485)
            Me.pnlRootAmbient.TabIndex = 2
            '
            'pnlLeftBrand
            '
            Me.pnlLeftBrand.BorderColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlLeftBrand.BorderWidth = 1.0!
            Me.pnlLeftBrand.Controls.Add(Me.pnlDbStatusBadge)
            Me.pnlLeftBrand.Controls.Add(Me.lblBrandMission)
            Me.pnlLeftBrand.Controls.Add(Me.lblBrandDirectorate)
            Me.pnlLeftBrand.Controls.Add(Me.lblBrandUniv)
            Me.pnlLeftBrand.Controls.Add(Me.picBrandLogo)
            Me.pnlLeftBrand.CornerRadius = 14
            Me.pnlLeftBrand.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlLeftBrand.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(32, Byte), Integer))
            Me.pnlLeftBrand.GlowColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.pnlLeftBrand.Location = New System.Drawing.Point(18, 18)
            Me.pnlLeftBrand.Name = "pnlLeftBrand"
            Me.pnlLeftBrand.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlLeftBrand.Size = New System.Drawing.Size(340, 449)
            Me.pnlLeftBrand.TabIndex = 0
            '
            'pnlDbStatusBadge
            '
            Me.pnlDbStatusBadge.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlDbStatusBadge.BorderWidth = 1.0!
            Me.pnlDbStatusBadge.Controls.Add(Me.lblDbStatusDot)
            Me.pnlDbStatusBadge.Controls.Add(Me.lblDbStatusText)
            Me.pnlDbStatusBadge.CornerRadius = 8
            Me.pnlDbStatusBadge.FillColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(11, Byte), Integer), CType(CType(18, Byte), Integer), CType(CType(28, Byte), Integer))
            Me.pnlDbStatusBadge.Location = New System.Drawing.Point(24, 396)
            Me.pnlDbStatusBadge.Name = "pnlDbStatusBadge"
            Me.pnlDbStatusBadge.Size = New System.Drawing.Size(292, 30)
            Me.pnlDbStatusBadge.TabIndex = 4
            '
            'lblDbStatusDot
            '
            Me.lblDbStatusDot.BackColor = System.Drawing.Color.Transparent
            Me.lblDbStatusDot.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDbStatusDot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblDbStatusDot.Location = New System.Drawing.Point(10, 6)
            Me.lblDbStatusDot.Name = "lblDbStatusDot"
            Me.lblDbStatusDot.Size = New System.Drawing.Size(16, 18)
            Me.lblDbStatusDot.TabIndex = 0
            Me.lblDbStatusDot.Text = "●"
            '
            'lblDbStatusText
            '
            Me.lblDbStatusText.AutoSize = True
            Me.lblDbStatusText.BackColor = System.Drawing.Color.Transparent
            Me.lblDbStatusText.Font = New System.Drawing.Font("Consolas", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblDbStatusText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblDbStatusText.Location = New System.Drawing.Point(28, 8)
            Me.lblDbStatusText.Name = "lblDbStatusText"
            Me.lblDbStatusText.Size = New System.Drawing.Size(203, 14)
            Me.lblDbStatusText.TabIndex = 1
            Me.lblDbStatusText.Text = "Database: Connected [SQL Server]"
            '
            'lblBrandMission
            '
            Me.lblBrandMission.BackColor = System.Drawing.Color.Transparent
            Me.lblBrandMission.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandMission.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblBrandMission.Location = New System.Drawing.Point(24, 230)
            Me.lblBrandMission.Name = "lblBrandMission"
            Me.lblBrandMission.Size = New System.Drawing.Size(292, 130)
            Me.lblBrandMission.TabIndex = 3
            Me.lblBrandMission.Text = "Welcome to the DeKUT Student Residential Portal. Manage your semester accommodati" &
    "on, reserve bed spaces in verified hostels, settle fees via M-Pesa with instant " &
    "receipts, discover assigned roommates, and log room maintenance requests."
            '
            'lblBrandDirectorate
            '
            Me.lblBrandDirectorate.BackColor = System.Drawing.Color.Transparent
            Me.lblBrandDirectorate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandDirectorate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.lblBrandDirectorate.Location = New System.Drawing.Point(24, 186)
            Me.lblBrandDirectorate.Name = "lblBrandDirectorate"
            Me.lblBrandDirectorate.Size = New System.Drawing.Size(292, 22)
            Me.lblBrandDirectorate.TabIndex = 2
            Me.lblBrandDirectorate.Text = "Directorate of Student Accommodation"
            Me.lblBrandDirectorate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblBrandUniv
            '
            Me.lblBrandUniv.BackColor = System.Drawing.Color.Transparent
            Me.lblBrandUniv.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandUniv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.lblBrandUniv.Location = New System.Drawing.Point(24, 138)
            Me.lblBrandUniv.Name = "lblBrandUniv"
            Me.lblBrandUniv.Size = New System.Drawing.Size(292, 44)
            Me.lblBrandUniv.TabIndex = 1
            Me.lblBrandUniv.Text = "DEDAN KIMATHI UNIVERSITY OF TECHNOLOGY"
            Me.lblBrandUniv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'picBrandLogo
            '
            Me.picBrandLogo.BackColor = System.Drawing.Color.Transparent
            Me.picBrandLogo.Location = New System.Drawing.Point(125, 34)
            Me.picBrandLogo.Name = "picBrandLogo"
            Me.picBrandLogo.Size = New System.Drawing.Size(90, 90)
            Me.picBrandLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picBrandLogo.TabIndex = 0
            Me.picBrandLogo.TabStop = False
            '
            'pnlLoginCard
            '
            Me.pnlLoginCard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlLoginCard.BorderWidth = 1.0!
            Me.pnlLoginCard.Controls.Add(Me.btnBackToChooser)
            Me.pnlLoginCard.Controls.Add(Me.btnRegister)
            Me.pnlLoginCard.Controls.Add(Me.btnLogin)
            Me.pnlLoginCard.Controls.Add(Me.lblStatusMessage)
            Me.pnlLoginCard.Controls.Add(Me.txtPassword)
            Me.pnlLoginCard.Controls.Add(Me.lblPassword)
            Me.pnlLoginCard.Controls.Add(Me.txtIdentifier)
            Me.pnlLoginCard.Controls.Add(Me.lblIdentifier)
            Me.pnlLoginCard.Controls.Add(Me.lblSubtitle)
            Me.pnlLoginCard.Controls.Add(Me.lblTitle)
            Me.pnlLoginCard.CornerRadius = 14
            Me.pnlLoginCard.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlLoginCard.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.pnlLoginCard.GlowColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.pnlLoginCard.Location = New System.Drawing.Point(376, 18)
            Me.pnlLoginCard.Name = "pnlLoginCard"
            Me.pnlLoginCard.Padding = New System.Windows.Forms.Padding(32)
            Me.pnlLoginCard.Size = New System.Drawing.Size(466, 449)
            Me.pnlLoginCard.TabIndex = 1
            '
            'btnBackToChooser
            '
            Me.btnBackToChooser.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnBackToChooser.CornerRadius = 9
            Me.btnBackToChooser.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnBackToChooser.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnBackToChooser.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnBackToChooser.HoverColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnBackToChooser.Location = New System.Drawing.Point(32, 388)
            Me.btnBackToChooser.Name = "btnBackToChooser"
            Me.btnBackToChooser.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnBackToChooser.Size = New System.Drawing.Size(402, 34)
            Me.btnBackToChooser.TabIndex = 9
            Me.btnBackToChooser.Text = "← Back to Portal Selection"
            '
            'btnRegister
            '
            Me.btnRegister.BorderColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.btnRegister.CornerRadius = 9
            Me.btnRegister.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnRegister.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnRegister.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.btnRegister.HoverColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.btnRegister.Location = New System.Drawing.Point(32, 342)
            Me.btnRegister.Name = "btnRegister"
            Me.btnRegister.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnRegister.Size = New System.Drawing.Size(402, 38)
            Me.btnRegister.TabIndex = 8
            Me.btnRegister.Text = "🎓  New Student? Create Account with DeKUT Email"
            '
            'btnLogin
            '
            Me.btnLogin.CornerRadius = 9
            Me.btnLogin.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnLogin.HoverColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.btnLogin.Location = New System.Drawing.Point(32, 290)
            Me.btnLogin.Name = "btnLogin"
            Me.btnLogin.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.btnLogin.Size = New System.Drawing.Size(402, 42)
            Me.btnLogin.TabIndex = 7
            Me.btnLogin.Text = "Sign In to Student Portal"
            '
            'lblStatusMessage
            '
            Me.lblStatusMessage.BackColor = System.Drawing.Color.Transparent
            Me.lblStatusMessage.Font = New System.Drawing.Font("Segoe UI", 8.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.lblStatusMessage.Location = New System.Drawing.Point(32, 248)
            Me.lblStatusMessage.Name = "lblStatusMessage"
            Me.lblStatusMessage.Size = New System.Drawing.Size(402, 34)
            Me.lblStatusMessage.TabIndex = 6
            Me.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'txtPassword
            '
            Me.txtPassword.BackColor = System.Drawing.Color.Transparent
            Me.txtPassword.CornerRadius = 10
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtPassword.IsPassword = True
            Me.txtPassword.LeadingIconText = "🔑"
            Me.txtPassword.Location = New System.Drawing.Point(32, 198)
            Me.txtPassword.MinimumSize = New System.Drawing.Size(120, 38)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PlaceholderText = "Enter your portal password"
            Me.txtPassword.Size = New System.Drawing.Size(402, 44)
            Me.txtPassword.TabIndex = 5
            Me.txtPassword.TextValue = ""
            '
            'lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.BackColor = System.Drawing.Color.Transparent
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblPassword.Location = New System.Drawing.Point(32, 176)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(57, 15)
            Me.lblPassword.TabIndex = 4
            Me.lblPassword.Text = "Password"
            '
            'txtIdentifier
            '
            Me.txtIdentifier.BackColor = System.Drawing.Color.Transparent
            Me.txtIdentifier.CornerRadius = 10
            Me.txtIdentifier.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtIdentifier.IsPassword = False
            Me.txtIdentifier.LeadingIconText = "👤"
            Me.txtIdentifier.Location = New System.Drawing.Point(32, 124)
            Me.txtIdentifier.MinimumSize = New System.Drawing.Size(120, 38)
            Me.txtIdentifier.Name = "txtIdentifier"
            Me.txtIdentifier.PlaceholderText = "Student email (@students.dkut.ac.ke) or Reg No"
            Me.txtIdentifier.Size = New System.Drawing.Size(402, 44)
            Me.txtIdentifier.TabIndex = 3
            Me.txtIdentifier.TextValue = ""
            '
            'lblIdentifier
            '
            Me.lblIdentifier.AutoSize = True
            Me.lblIdentifier.BackColor = System.Drawing.Color.Transparent
            Me.lblIdentifier.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblIdentifier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblIdentifier.Location = New System.Drawing.Point(32, 102)
            Me.lblIdentifier.Name = "lblIdentifier"
            Me.lblIdentifier.Size = New System.Drawing.Size(225, 15)
            Me.lblIdentifier.TabIndex = 2
            Me.lblIdentifier.Text = "Student Email or Registration Number"
            '
            'lblSubtitle
            '
            Me.lblSubtitle.BackColor = System.Drawing.Color.Transparent
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(32, 58)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(402, 34)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Sign in using your verified @students.dkut.ac.ke email or student registration nu" &
    "mber."
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(30, 24)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(223, 30)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "Student Portal Login"
            '
            'frmStudentLogin
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(860, 520)
            Me.Controls.Add(Me.pnlRootAmbient)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmStudentLogin"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Dedan Kimathi University of Technology - Student Portal Login"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlRootAmbient.ResumeLayout(False)
            Me.pnlLeftBrand.ResumeLayout(False)
            Me.pnlLeftBrand.PerformLayout()
            Me.pnlDbStatusBadge.ResumeLayout(False)
            Me.pnlDbStatusBadge.PerformLayout()
            CType(Me.picBrandLogo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlLoginCard.ResumeLayout(False)
            Me.pnlLoginCard.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents btnMinimize As System.Windows.Forms.Button
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlRootAmbient As System.Windows.Forms.Panel
        Friend WithEvents pnlLeftBrand As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents picBrandLogo As System.Windows.Forms.PictureBox
        Friend WithEvents lblBrandUniv As System.Windows.Forms.Label
        Friend WithEvents lblBrandDirectorate As System.Windows.Forms.Label
        Friend WithEvents lblBrandMission As System.Windows.Forms.Label
        Friend WithEvents pnlDbStatusBadge As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblDbStatusDot As System.Windows.Forms.Label
        Friend WithEvents lblDbStatusText As System.Windows.Forms.Label
        Friend WithEvents pnlLoginCard As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents lblIdentifier As System.Windows.Forms.Label
        Friend WithEvents txtIdentifier As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblPassword As System.Windows.Forms.Label
        Friend WithEvents txtPassword As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblStatusMessage As System.Windows.Forms.Label
        Friend WithEvents btnLogin As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnRegister As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnBackToChooser As University_hostel_mgt.Controls.GlassButton
    End Class
End Namespace
