Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmLogin
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlTopAccent = New System.Windows.Forms.Panel()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.btnMinimize = New System.Windows.Forms.Button()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.lblHeaderTitle = New System.Windows.Forms.Label()
            Me.pnlRootAmbient = New University_hostel_mgt.Controls.AmbientBackgroundPanel()
            Me.pnlLeftBrand = New University_hostel_mgt.Controls.GlassPanel()
            Me.picBrandLogo = New System.Windows.Forms.PictureBox()
            Me.pnlDbStatusBadge = New University_hostel_mgt.Controls.GlassPanel()
            Me.lblDbStatusText = New System.Windows.Forms.Label()
            Me.lblDbStatusDot = New System.Windows.Forms.Label()
            Me.lblBrandDesc = New System.Windows.Forms.Label()
            Me.lblBrandSub = New System.Windows.Forms.Label()
            Me.lblBrandTitle = New System.Windows.Forms.Label()
            Me.pnlLoginCard = New University_hostel_mgt.Controls.GlassPanel()
            Me.btnStudentRegister = New University_hostel_mgt.Controls.GlassButton()
            Me.btnExitApp = New University_hostel_mgt.Controls.GlassButton()
            Me.btnLogin = New University_hostel_mgt.Controls.GlassButton()
            Me.lblStatusMessage = New System.Windows.Forms.Label()
            Me.txtPassword = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblPasswordTitle = New System.Windows.Forms.Label()
            Me.txtUsername = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblUsernameTitle = New System.Windows.Forms.Label()
            Me.lblSignInSub = New System.Windows.Forms.Label()
            Me.lblSignInHeader = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlRootAmbient.SuspendLayout()
            Me.pnlLeftBrand.SuspendLayout()
            Me.pnlDbStatusBadge.SuspendLayout()
            Me.pnlLoginCard.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(920, 3)
            Me.pnlTopAccent.TabIndex = 0
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.btnMinimize)
            Me.pnlHeader.Controls.Add(Me.btnClose)
            Me.pnlHeader.Controls.Add(Me.lblHeaderTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 3)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(920, 38)
            Me.pnlHeader.TabIndex = 1
            '
            'btnMinimize
            '
            Me.btnMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnMinimize.FlatAppearance.BorderSize = 0
            Me.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnMinimize.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnMinimize.Location = New System.Drawing.Point(840, 4)
            Me.btnMinimize.Name = "btnMinimize"
            Me.btnMinimize.Size = New System.Drawing.Size(35, 30)
            Me.btnMinimize.TabIndex = 2
            Me.btnMinimize.Text = "—"
            Me.btnMinimize.UseVisualStyleBackColor = True
            '
            'btnClose
            '
            Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnClose.FlatAppearance.BorderSize = 0
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnClose.Location = New System.Drawing.Point(880, 4)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(35, 30)
            Me.btnClose.TabIndex = 1
            Me.btnClose.Text = "✕"
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'lblHeaderTitle
            '
            Me.lblHeaderTitle.AutoSize = True
            Me.lblHeaderTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblHeaderTitle.Location = New System.Drawing.Point(16, 11)
            Me.lblHeaderTitle.Name = "lblHeaderTitle"
            Me.lblHeaderTitle.Size = New System.Drawing.Size(315, 15)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "University Hostel Management System • Staff Authentication"
            '
            'pnlRootAmbient
            '
            Me.pnlRootAmbient.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.pnlRootAmbient.Controls.Add(Me.pnlLoginCard)
            Me.pnlRootAmbient.Controls.Add(Me.pnlLeftBrand)
            Me.pnlRootAmbient.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlRootAmbient.Location = New System.Drawing.Point(0, 41)
            Me.pnlRootAmbient.Name = "pnlRootAmbient"
            Me.pnlRootAmbient.Padding = New System.Windows.Forms.Padding(20)
            Me.pnlRootAmbient.Size = New System.Drawing.Size(920, 549)
            Me.pnlRootAmbient.TabIndex = 2
            '
            'pnlLeftBrand
            '
            Me.pnlLeftBrand.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlLeftBrand.BorderWidth = 1.0!
            Me.pnlLeftBrand.Controls.Add(Me.pnlDbStatusBadge)
            Me.pnlLeftBrand.Controls.Add(Me.lblBrandDesc)
            Me.pnlLeftBrand.Controls.Add(Me.lblBrandSub)
            Me.pnlLeftBrand.Controls.Add(Me.lblBrandTitle)
            Me.pnlLeftBrand.Controls.Add(Me.picBrandLogo)
            Me.pnlLeftBrand.CornerRadius = 18
            Me.pnlLeftBrand.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlLeftBrand.FillColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(33, Byte), Integer))
            Me.pnlLeftBrand.GlowColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
            Me.pnlLeftBrand.Location = New System.Drawing.Point(20, 20)
            Me.pnlLeftBrand.Name = "pnlLeftBrand"
            Me.pnlLeftBrand.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlLeftBrand.Size = New System.Drawing.Size(370, 509)
            Me.pnlLeftBrand.TabIndex = 0
            '
            'picBrandLogo
            '
            Me.picBrandLogo.BackColor = System.Drawing.Color.Transparent
            Me.picBrandLogo.Location = New System.Drawing.Point(130, 26)
            Me.picBrandLogo.Name = "picBrandLogo"
            Me.picBrandLogo.Size = New System.Drawing.Size(110, 110)
            Me.picBrandLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picBrandLogo.TabIndex = 0
            Me.picBrandLogo.TabStop = False
            '
            'lblBrandTitle
            '
            Me.lblBrandTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblBrandTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblBrandTitle.Location = New System.Drawing.Point(20, 146)
            Me.lblBrandTitle.Name = "lblBrandTitle"
            Me.lblBrandTitle.Size = New System.Drawing.Size(330, 48)
            Me.lblBrandTitle.TabIndex = 1
            Me.lblBrandTitle.Text = "DEDAN KIMATHI UNIVERSITY OF TECHNOLOGY"
            Me.lblBrandTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblBrandSub
            '
            Me.lblBrandSub.BackColor = System.Drawing.Color.Transparent
            Me.lblBrandSub.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
            Me.lblBrandSub.Location = New System.Drawing.Point(20, 198)
            Me.lblBrandSub.Name = "lblBrandSub"
            Me.lblBrandSub.Size = New System.Drawing.Size(330, 24)
            Me.lblBrandSub.TabIndex = 2
            Me.lblBrandSub.Text = "Directorate of Student Accommodation"
            Me.lblBrandSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblBrandSub.UseMnemonic = False
            '
            'lblBrandDesc
            '
            Me.lblBrandDesc.BackColor = System.Drawing.Color.Transparent
            Me.lblBrandDesc.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblBrandDesc.Location = New System.Drawing.Point(24, 234)
            Me.lblBrandDesc.Name = "lblBrandDesc"
            Me.lblBrandDesc.Size = New System.Drawing.Size(322, 110)
            Me.lblBrandDesc.TabIndex = 3
            Me.lblBrandDesc.Text = "Official staff security gateway for student residence allocations, individual bed " &
    "matrices, semester billing schedules, room diagnostics, and auditable enterprise " &
    "operations."
            Me.lblBrandDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'pnlDbStatusBadge
            '
            Me.pnlDbStatusBadge.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
            Me.pnlDbStatusBadge.BorderWidth = 1.0!
            Me.pnlDbStatusBadge.Controls.Add(Me.lblDbStatusText)
            Me.pnlDbStatusBadge.Controls.Add(Me.lblDbStatusDot)
            Me.pnlDbStatusBadge.CornerRadius = 10
            Me.pnlDbStatusBadge.FillColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.pnlDbStatusBadge.GlowColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
            Me.pnlDbStatusBadge.Location = New System.Drawing.Point(24, 445)
            Me.pnlDbStatusBadge.Name = "pnlDbStatusBadge"
            Me.pnlDbStatusBadge.Padding = New System.Windows.Forms.Padding(8)
            Me.pnlDbStatusBadge.Size = New System.Drawing.Size(322, 38)
            Me.pnlDbStatusBadge.TabIndex = 4
            '
            'lblDbStatusText
            '
            Me.lblDbStatusText.AutoSize = True
            Me.lblDbStatusText.BackColor = System.Drawing.Color.Transparent
            Me.lblDbStatusText.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblDbStatusText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblDbStatusText.Location = New System.Drawing.Point(30, 11)
            Me.lblDbStatusText.Name = "lblDbStatusText"
            Me.lblDbStatusText.Size = New System.Drawing.Size(217, 15)
            Me.lblDbStatusText.TabIndex = 1
            Me.lblDbStatusText.Text = "Database: Connected [SQL Server 2025]"
            '
            'lblDbStatusDot
            '
            Me.lblDbStatusDot.BackColor = System.Drawing.Color.Transparent
            Me.lblDbStatusDot.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDbStatusDot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
            Me.lblDbStatusDot.Location = New System.Drawing.Point(8, 7)
            Me.lblDbStatusDot.Name = "lblDbStatusDot"
            Me.lblDbStatusDot.Size = New System.Drawing.Size(18, 20)
            Me.lblDbStatusDot.TabIndex = 0
            Me.lblDbStatusDot.Text = "●"
            '
            'pnlLoginCard
            '
            Me.pnlLoginCard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlLoginCard.BorderWidth = 1.0!
            Me.pnlLoginCard.Controls.Add(Me.btnStudentRegister)
            Me.pnlLoginCard.Controls.Add(Me.btnExitApp)
            Me.pnlLoginCard.Controls.Add(Me.btnLogin)
            Me.pnlLoginCard.Controls.Add(Me.lblStatusMessage)
            Me.pnlLoginCard.Controls.Add(Me.txtPassword)
            Me.pnlLoginCard.Controls.Add(Me.lblPasswordTitle)
            Me.pnlLoginCard.Controls.Add(Me.txtUsername)
            Me.pnlLoginCard.Controls.Add(Me.lblUsernameTitle)
            Me.pnlLoginCard.Controls.Add(Me.lblSignInSub)
            Me.pnlLoginCard.Controls.Add(Me.lblSignInHeader)
            Me.pnlLoginCard.CornerRadius = 18
            Me.pnlLoginCard.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlLoginCard.FillColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(33, Byte), Integer))
            Me.pnlLoginCard.GlowColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
            Me.pnlLoginCard.Location = New System.Drawing.Point(390, 20)
            Me.pnlLoginCard.Name = "pnlLoginCard"
            Me.pnlLoginCard.Padding = New System.Windows.Forms.Padding(36)
            Me.pnlLoginCard.Size = New System.Drawing.Size(510, 509)
            Me.pnlLoginCard.TabIndex = 1
            '
            'btnStudentRegister
            '
            Me.btnStudentRegister.BackColor = System.Drawing.Color.Transparent
            Me.btnStudentRegister.CornerRadius = 12
            Me.btnStudentRegister.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnStudentRegister.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnStudentRegister.HoverColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnStudentRegister.Location = New System.Drawing.Point(36, 412)
            Me.btnStudentRegister.Name = "btnStudentRegister"
            Me.btnStudentRegister.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnStudentRegister.Size = New System.Drawing.Size(438, 38)
            Me.btnStudentRegister.TabIndex = 9
            Me.btnStudentRegister.Text = "🎓 New Student? Create Account with DeKUT Email"
            '
            'btnExitApp
            '
            Me.btnExitApp.BackColor = System.Drawing.Color.Transparent
            Me.btnExitApp.CornerRadius = 12
            Me.btnExitApp.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnExitApp.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnExitApp.HoverColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnExitApp.Location = New System.Drawing.Point(36, 458)
            Me.btnExitApp.Name = "btnExitApp"
            Me.btnExitApp.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnExitApp.Size = New System.Drawing.Size(438, 36)
            Me.btnExitApp.TabIndex = 10
            Me.btnExitApp.Text = "Exit Application"
            '
            'btnLogin
            '
            Me.btnLogin.BackColor = System.Drawing.Color.Transparent
            Me.btnLogin.CornerRadius = 12
            Me.btnLogin.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(20, Byte), Integer))
            Me.btnLogin.HoverColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(94, Byte), Integer))
            Me.btnLogin.Location = New System.Drawing.Point(36, 362)
            Me.btnLogin.Name = "btnLogin"
            Me.btnLogin.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(118, Byte), Integer))
            Me.btnLogin.Size = New System.Drawing.Size(438, 42)
            Me.btnLogin.TabIndex = 8
            Me.btnLogin.Text = "Sign In"
            '
            'lblStatusMessage
            '
            Me.lblStatusMessage.BackColor = System.Drawing.Color.Transparent
            Me.lblStatusMessage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.lblStatusMessage.Location = New System.Drawing.Point(36, 318)
            Me.lblStatusMessage.Name = "lblStatusMessage"
            Me.lblStatusMessage.Size = New System.Drawing.Size(438, 38)
            Me.lblStatusMessage.TabIndex = 7
            Me.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'txtPassword
            '
            Me.txtPassword.BackColor = System.Drawing.Color.Transparent
            Me.txtPassword.CornerRadius = 12
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtPassword.IsPassword = True
            Me.txtPassword.LeadingIconText = "🔑"
            Me.txtPassword.Location = New System.Drawing.Point(36, 260)
            Me.txtPassword.MinimumSize = New System.Drawing.Size(120, 38)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PlaceholderText = "Enter secure password"
            Me.txtPassword.Size = New System.Drawing.Size(438, 44)
            Me.txtPassword.TabIndex = 5
            Me.txtPassword.TextValue = ""
            '
            'lblPasswordTitle
            '
            Me.lblPasswordTitle.AutoSize = True
            Me.lblPasswordTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblPasswordTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPasswordTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblPasswordTitle.Location = New System.Drawing.Point(36, 236)
            Me.lblPasswordTitle.Name = "lblPasswordTitle"
            Me.lblPasswordTitle.Size = New System.Drawing.Size(57, 15)
            Me.lblPasswordTitle.TabIndex = 4
            Me.lblPasswordTitle.Text = "Password"
            '
            'txtUsername
            '
            Me.txtUsername.BackColor = System.Drawing.Color.Transparent
            Me.txtUsername.CornerRadius = 12
            Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtUsername.IsPassword = False
            Me.txtUsername.LeadingIconText = "👤"
            Me.txtUsername.Location = New System.Drawing.Point(36, 175)
            Me.txtUsername.MinimumSize = New System.Drawing.Size(120, 38)
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.PlaceholderText = "Staff username or Student email (@students.dkut.ac.ke)"
            Me.txtUsername.Size = New System.Drawing.Size(438, 44)
            Me.txtUsername.TabIndex = 3
            Me.txtUsername.TextValue = ""
            '
            'lblUsernameTitle
            '
            Me.lblUsernameTitle.AutoSize = True
            Me.lblUsernameTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblUsernameTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUsernameTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblUsernameTitle.Location = New System.Drawing.Point(36, 150)
            Me.lblUsernameTitle.Name = "lblUsernameTitle"
            Me.lblUsernameTitle.Size = New System.Drawing.Size(155, 15)
            Me.lblUsernameTitle.TabIndex = 2
            Me.lblUsernameTitle.Text = "Username or Student Email"
            '
            'lblSignInSub
            '
            Me.lblSignInSub.BackColor = System.Drawing.Color.Transparent
            Me.lblSignInSub.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSignInSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblSignInSub.Location = New System.Drawing.Point(36, 75)
            Me.lblSignInSub.Name = "lblSignInSub"
            Me.lblSignInSub.Size = New System.Drawing.Size(438, 45)
            Me.lblSignInSub.TabIndex = 1
            Me.lblSignInSub.Text = "Please enter your administrative credentials to access accommodation and billing " &
    "records."
            '
            'lblSignInHeader
            '
            Me.lblSignInHeader.AutoSize = True
            Me.lblSignInHeader.BackColor = System.Drawing.Color.Transparent
            Me.lblSignInHeader.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblSignInHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblSignInHeader.Location = New System.Drawing.Point(36, 35)
            Me.lblSignInHeader.Name = "lblSignInHeader"
            Me.lblSignInHeader.Size = New System.Drawing.Size(262, 32)
            Me.lblSignInHeader.TabIndex = 0
            Me.lblSignInHeader.Text = "Staff Authentication"
            '
            'frmLogin
            '
            Me.AcceptButton = Me.btnLogin
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.CancelButton = Me.btnExitApp
            Me.ClientSize = New System.Drawing.Size(920, 590)
            Me.Controls.Add(Me.pnlRootAmbient)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.KeyPreview = True
            Me.Name = "frmLogin"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "University Hostel Management - Sign In"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlRootAmbient.ResumeLayout(False)
            Me.pnlLeftBrand.ResumeLayout(False)
            Me.pnlLeftBrand.PerformLayout()
            Me.pnlDbStatusBadge.ResumeLayout(False)
            Me.pnlDbStatusBadge.PerformLayout()
            Me.pnlLoginCard.ResumeLayout(False)
            Me.pnlLoginCard.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents btnMinimize As System.Windows.Forms.Button
        Friend WithEvents pnlRootAmbient As University_hostel_mgt.Controls.AmbientBackgroundPanel
        Friend WithEvents pnlLeftBrand As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents picBrandLogo As System.Windows.Forms.PictureBox
        Friend WithEvents lblBrandTitle As System.Windows.Forms.Label
        Friend WithEvents lblBrandSub As System.Windows.Forms.Label
        Friend WithEvents lblBrandDesc As System.Windows.Forms.Label
        Friend WithEvents pnlDbStatusBadge As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblDbStatusDot As System.Windows.Forms.Label
        Friend WithEvents lblDbStatusText As System.Windows.Forms.Label
        Friend WithEvents pnlLoginCard As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblSignInHeader As System.Windows.Forms.Label
        Friend WithEvents lblSignInSub As System.Windows.Forms.Label
        Friend WithEvents lblUsernameTitle As System.Windows.Forms.Label
        Friend WithEvents txtUsername As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblPasswordTitle As System.Windows.Forms.Label
        Friend WithEvents txtPassword As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblStatusMessage As System.Windows.Forms.Label
        Friend WithEvents btnLogin As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnStudentRegister As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnExitApp As University_hostel_mgt.Controls.GlassButton
    End Class
End Namespace
