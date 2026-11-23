Namespace Forms.StudentPortal
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmStudentRegister
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
            Me.pnlRoot = New University_hostel_mgt.Controls.GlassPanel()
            Me.picLogo = New System.Windows.Forms.PictureBox()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblEmail = New System.Windows.Forms.Label()
            Me.txtEmail = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblRegNo = New System.Windows.Forms.Label()
            Me.txtRegNo = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblFirstName = New System.Windows.Forms.Label()
            Me.txtFirstName = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblLastName = New System.Windows.Forms.Label()
            Me.txtLastName = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblGender = New System.Windows.Forms.Label()
            Me.cboGender = New System.Windows.Forms.ComboBox()
            Me.lblCourse = New System.Windows.Forms.Label()
            Me.txtCourse = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblPhone = New System.Windows.Forms.Label()
            Me.txtPhone = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblDepartment = New System.Windows.Forms.Label()
            Me.txtDepartment = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.txtPassword = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblConfirmPassword = New System.Windows.Forms.Label()
            Me.txtConfirmPassword = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblStatusMessage = New System.Windows.Forms.Label()
            Me.btnRegister = New University_hostel_mgt.Controls.GlassButton()
            Me.btnBackToLogin = New University_hostel_mgt.Controls.GlassButton()
            Me.pnlHeader.SuspendLayout()
            Me.pnlRoot.SuspendLayout()
            CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(720, 3)
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
            Me.pnlHeader.Size = New System.Drawing.Size(720, 38)
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
            Me.btnMinimize.Location = New System.Drawing.Point(640, 4)
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
            Me.btnClose.Location = New System.Drawing.Point(680, 4)
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
            Me.lblHeaderTitle.Size = New System.Drawing.Size(356, 15)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "Dedan Kimathi University of Technology • Student Portal Registration"
            '
            'pnlRoot
            '
            Me.pnlRoot.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlRoot.BorderWidth = 1.0!
            Me.pnlRoot.Controls.Add(Me.btnBackToLogin)
            Me.pnlRoot.Controls.Add(Me.btnRegister)
            Me.pnlRoot.Controls.Add(Me.lblStatusMessage)
            Me.pnlRoot.Controls.Add(Me.txtConfirmPassword)
            Me.pnlRoot.Controls.Add(Me.lblConfirmPassword)
            Me.pnlRoot.Controls.Add(Me.txtPassword)
            Me.pnlRoot.Controls.Add(Me.lblPassword)
            Me.pnlRoot.Controls.Add(Me.txtDepartment)
            Me.pnlRoot.Controls.Add(Me.lblDepartment)
            Me.pnlRoot.Controls.Add(Me.txtPhone)
            Me.pnlRoot.Controls.Add(Me.lblPhone)
            Me.pnlRoot.Controls.Add(Me.txtCourse)
            Me.pnlRoot.Controls.Add(Me.lblCourse)
            Me.pnlRoot.Controls.Add(Me.cboGender)
            Me.pnlRoot.Controls.Add(Me.lblGender)
            Me.pnlRoot.Controls.Add(Me.txtLastName)
            Me.pnlRoot.Controls.Add(Me.lblLastName)
            Me.pnlRoot.Controls.Add(Me.txtFirstName)
            Me.pnlRoot.Controls.Add(Me.lblFirstName)
            Me.pnlRoot.Controls.Add(Me.txtRegNo)
            Me.pnlRoot.Controls.Add(Me.lblRegNo)
            Me.pnlRoot.Controls.Add(Me.txtEmail)
            Me.pnlRoot.Controls.Add(Me.lblEmail)
            Me.pnlRoot.Controls.Add(Me.lblSubtitle)
            Me.pnlRoot.Controls.Add(Me.lblTitle)
            Me.pnlRoot.Controls.Add(Me.picLogo)
            Me.pnlRoot.CornerRadius = 18
            Me.pnlRoot.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlRoot.FillColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(31, Byte), Integer))
            Me.pnlRoot.GlowColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlRoot.Location = New System.Drawing.Point(0, 41)
            Me.pnlRoot.Name = "pnlRoot"
            Me.pnlRoot.Padding = New System.Windows.Forms.Padding(28)
            Me.pnlRoot.Size = New System.Drawing.Size(720, 629)
            Me.pnlRoot.TabIndex = 2
            '
            'picLogo
            '
            Me.picLogo.BackColor = System.Drawing.Color.Transparent
            Me.picLogo.Location = New System.Drawing.Point(28, 20)
            Me.picLogo.Name = "picLogo"
            Me.picLogo.Size = New System.Drawing.Size(54, 54)
            Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picLogo.TabIndex = 0
            Me.picLogo.TabStop = False
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblTitle.Location = New System.Drawing.Point(90, 22)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(378, 25)
            Me.lblTitle.TabIndex = 1
            Me.lblTitle.Text = "Create Student Accommodation Account"
            '
            'lblSubtitle
            '
            Me.lblSubtitle.AutoSize = True
            Me.lblSubtitle.BackColor = System.Drawing.Color.Transparent
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(92, 49)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(395, 15)
            Me.lblSubtitle.TabIndex = 2
            Me.lblSubtitle.Text = "Use your official Dedan Kimathi University email (@students.dkut.ac.ke)"
            '
            'lblEmail
            '
            Me.lblEmail.AutoSize = True
            Me.lblEmail.BackColor = System.Drawing.Color.Transparent
            Me.lblEmail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblEmail.Location = New System.Drawing.Point(28, 88)
            Me.lblEmail.Name = "lblEmail"
            Me.lblEmail.Size = New System.Drawing.Size(269, 15)
            Me.lblEmail.TabIndex = 3
            Me.lblEmail.Text = "DeKUT Student Email (@students.dkut.ac.ke) *"
            '
            'txtEmail
            '
            Me.txtEmail.Location = New System.Drawing.Point(28, 107)
            Me.txtEmail.Name = "txtEmail"
            Me.txtEmail.PlaceholderText = "e.g. griffin.wekesa25@students.dkut.ac.ke"
            Me.txtEmail.Size = New System.Drawing.Size(320, 36)
            Me.txtEmail.TabIndex = 4
            '
            'lblRegNo
            '
            Me.lblRegNo.AutoSize = True
            Me.lblRegNo.BackColor = System.Drawing.Color.Transparent
            Me.lblRegNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRegNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblRegNo.Location = New System.Drawing.Point(372, 88)
            Me.lblRegNo.Name = "lblRegNo"
            Me.lblRegNo.Size = New System.Drawing.Size(183, 15)
            Me.lblRegNo.TabIndex = 5
            Me.lblRegNo.Text = "University Registration Number *"
            '
            'txtRegNo
            '
            Me.txtRegNo.Location = New System.Drawing.Point(372, 107)
            Me.txtRegNo.Name = "txtRegNo"
            Me.txtRegNo.PlaceholderText = "e.g. C026-01-1234/2023"
            Me.txtRegNo.Size = New System.Drawing.Size(320, 36)
            Me.txtRegNo.TabIndex = 6
            '
            'lblFirstName
            '
            Me.lblFirstName.AutoSize = True
            Me.lblFirstName.BackColor = System.Drawing.Color.Transparent
            Me.lblFirstName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblFirstName.Location = New System.Drawing.Point(28, 154)
            Me.lblFirstName.Name = "lblFirstName"
            Me.lblFirstName.Size = New System.Drawing.Size(73, 15)
            Me.lblFirstName.TabIndex = 7
            Me.lblFirstName.Text = "First Name *"
            '
            'txtFirstName
            '
            Me.txtFirstName.Location = New System.Drawing.Point(28, 173)
            Me.txtFirstName.Name = "txtFirstName"
            Me.txtFirstName.PlaceholderText = "First Name"
            Me.txtFirstName.Size = New System.Drawing.Size(320, 36)
            Me.txtFirstName.TabIndex = 8
            '
            'lblLastName
            '
            Me.lblLastName.AutoSize = True
            Me.lblLastName.BackColor = System.Drawing.Color.Transparent
            Me.lblLastName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblLastName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblLastName.Location = New System.Drawing.Point(372, 154)
            Me.lblLastName.Name = "lblLastName"
            Me.lblLastName.Size = New System.Drawing.Size(72, 15)
            Me.lblLastName.TabIndex = 9
            Me.lblLastName.Text = "Last Name *"
            '
            'txtLastName
            '
            Me.txtLastName.Location = New System.Drawing.Point(372, 173)
            Me.txtLastName.Name = "txtLastName"
            Me.txtLastName.PlaceholderText = "Last Name"
            Me.txtLastName.Size = New System.Drawing.Size(320, 36)
            Me.txtLastName.TabIndex = 10
            '
            'lblGender
            '
            Me.lblGender.AutoSize = True
            Me.lblGender.BackColor = System.Drawing.Color.Transparent
            Me.lblGender.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblGender.Location = New System.Drawing.Point(28, 220)
            Me.lblGender.Name = "lblGender"
            Me.lblGender.Size = New System.Drawing.Size(199, 15)
            Me.lblGender.TabIndex = 11
            Me.lblGender.Text = "Gender (for hostel allocation rule) *"
            '
            'cboGender
            '
            Me.cboGender.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(35, Byte), Integer))
            Me.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboGender.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cboGender.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.cboGender.FormattingEnabled = True
            Me.cboGender.Items.AddRange(New Object() {"Male", "Female"})
            Me.cboGender.Location = New System.Drawing.Point(28, 239)
            Me.cboGender.Name = "cboGender"
            Me.cboGender.Size = New System.Drawing.Size(320, 25)
            Me.cboGender.TabIndex = 12
            '
            'lblCourse
            '
            Me.lblCourse.AutoSize = True
            Me.lblCourse.BackColor = System.Drawing.Color.Transparent
            Me.lblCourse.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblCourse.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblCourse.Location = New System.Drawing.Point(372, 220)
            Me.lblCourse.Name = "lblCourse"
            Me.lblCourse.Size = New System.Drawing.Size(95, 15)
            Me.lblCourse.TabIndex = 13
            Me.lblCourse.Text = "Degree Course *"
            '
            'txtCourse
            '
            Me.txtCourse.Location = New System.Drawing.Point(372, 239)
            Me.txtCourse.Name = "txtCourse"
            Me.txtCourse.PlaceholderText = "e.g. BSc. Computer Science"
            Me.txtCourse.Size = New System.Drawing.Size(320, 36)
            Me.txtCourse.TabIndex = 14
            '
            'lblPhone
            '
            Me.lblPhone.AutoSize = True
            Me.lblPhone.BackColor = System.Drawing.Color.Transparent
            Me.lblPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblPhone.Location = New System.Drawing.Point(28, 286)
            Me.lblPhone.Name = "lblPhone"
            Me.lblPhone.Size = New System.Drawing.Size(97, 15)
            Me.lblPhone.TabIndex = 15
            Me.lblPhone.Text = "Mobile Phone *"
            '
            'txtPhone
            '
            Me.txtPhone.Location = New System.Drawing.Point(28, 305)
            Me.txtPhone.Name = "txtPhone"
            Me.txtPhone.PlaceholderText = "e.g. 0712345678"
            Me.txtPhone.Size = New System.Drawing.Size(320, 36)
            Me.txtPhone.TabIndex = 16
            '
            'lblDepartment
            '
            Me.lblDepartment.AutoSize = True
            Me.lblDepartment.BackColor = System.Drawing.Color.Transparent
            Me.lblDepartment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblDepartment.Location = New System.Drawing.Point(372, 286)
            Me.lblDepartment.Name = "lblDepartment"
            Me.lblDepartment.Size = New System.Drawing.Size(126, 15)
            Me.lblDepartment.TabIndex = 17
            Me.lblDepartment.Text = "Academic Department"
            '
            'txtDepartment
            '
            Me.txtDepartment.Location = New System.Drawing.Point(372, 305)
            Me.txtDepartment.Name = "txtDepartment"
            Me.txtDepartment.PlaceholderText = "e.g. Computer Science"
            Me.txtDepartment.Size = New System.Drawing.Size(320, 36)
            Me.txtDepartment.TabIndex = 18
            '
            'lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.BackColor = System.Drawing.Color.Transparent
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblPassword.Location = New System.Drawing.Point(28, 352)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(209, 15)
            Me.lblPassword.TabIndex = 19
            Me.lblPassword.Text = "Password (min 6 chars, alphanumeric) *"
            '
            'txtPassword
            '
            Me.txtPassword.IsPassword = True
            Me.txtPassword.Location = New System.Drawing.Point(28, 371)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PlaceholderText = "••••••••"
            Me.txtPassword.Size = New System.Drawing.Size(320, 36)
            Me.txtPassword.TabIndex = 20
            '
            'lblConfirmPassword
            '
            Me.lblConfirmPassword.AutoSize = True
            Me.lblConfirmPassword.BackColor = System.Drawing.Color.Transparent
            Me.lblConfirmPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblConfirmPassword.Location = New System.Drawing.Point(372, 352)
            Me.lblConfirmPassword.Name = "lblConfirmPassword"
            Me.lblConfirmPassword.Size = New System.Drawing.Size(112, 15)
            Me.lblConfirmPassword.TabIndex = 21
            Me.lblConfirmPassword.Text = "Confirm Password *"
            '
            'txtConfirmPassword
            '
            Me.txtConfirmPassword.IsPassword = True
            Me.txtConfirmPassword.Location = New System.Drawing.Point(372, 371)
            Me.txtConfirmPassword.Name = "txtConfirmPassword"
            Me.txtConfirmPassword.PlaceholderText = "••••••••"
            Me.txtConfirmPassword.Size = New System.Drawing.Size(320, 36)
            Me.txtConfirmPassword.TabIndex = 22
            '
            'lblStatusMessage
            '
            Me.lblStatusMessage.BackColor = System.Drawing.Color.Transparent
            Me.lblStatusMessage.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.lblStatusMessage.Location = New System.Drawing.Point(28, 420)
            Me.lblStatusMessage.Name = "lblStatusMessage"
            Me.lblStatusMessage.Size = New System.Drawing.Size(664, 28)
            Me.lblStatusMessage.TabIndex = 23
            Me.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'btnRegister
            '
            Me.btnRegister.CornerRadius = 12
            Me.btnRegister.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnRegister.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnRegister.HoverColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
            Me.btnRegister.Location = New System.Drawing.Point(28, 460)
            Me.btnRegister.Name = "btnRegister"
            Me.btnRegister.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnRegister.Size = New System.Drawing.Size(664, 42)
            Me.btnRegister.TabIndex = 24
            Me.btnRegister.Text = "Register Account & Proceed to Student Portal"
            '
            'btnBackToLogin
            '
            Me.btnBackToLogin.CornerRadius = 12
            Me.btnBackToLogin.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnBackToLogin.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnBackToLogin.HoverColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnBackToLogin.Location = New System.Drawing.Point(28, 514)
            Me.btnBackToLogin.Name = "btnBackToLogin"
            Me.btnBackToLogin.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnBackToLogin.Size = New System.Drawing.Size(664, 38)
            Me.btnBackToLogin.TabIndex = 25
            Me.btnBackToLogin.Text = "Already have an account? Back to Sign In"
            '
            'frmStudentRegister
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(720, 670)
            Me.Controls.Add(Me.pnlRoot)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmStudentRegister"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Student Self-Service Portal Registration"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlRoot.ResumeLayout(False)
            Me.pnlRoot.PerformLayout()
            CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents btnMinimize As System.Windows.Forms.Button
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlRoot As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents picLogo As System.Windows.Forms.PictureBox
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents lblEmail As System.Windows.Forms.Label
        Friend WithEvents txtEmail As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblRegNo As System.Windows.Forms.Label
        Friend WithEvents txtRegNo As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblFirstName As System.Windows.Forms.Label
        Friend WithEvents txtFirstName As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblLastName As System.Windows.Forms.Label
        Friend WithEvents txtLastName As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblGender As System.Windows.Forms.Label
        Friend WithEvents cboGender As System.Windows.Forms.ComboBox
        Friend WithEvents lblCourse As System.Windows.Forms.Label
        Friend WithEvents txtCourse As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblPhone As System.Windows.Forms.Label
        Friend WithEvents txtPhone As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblDepartment As System.Windows.Forms.Label
        Friend WithEvents txtDepartment As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblPassword As System.Windows.Forms.Label
        Friend WithEvents txtPassword As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblConfirmPassword As System.Windows.Forms.Label
        Friend WithEvents txtConfirmPassword As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblStatusMessage As System.Windows.Forms.Label
        Friend WithEvents btnRegister As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnBackToLogin As University_hostel_mgt.Controls.GlassButton
    End Class
End Namespace

