Namespace Forms.Students
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmStudentModal
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
            Me.pnlTopGold = New System.Windows.Forms.Panel()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblModalTitle = New System.Windows.Forms.Label()
            Me.lblModalSub = New System.Windows.Forms.Label()
            Me.pnlBody = New System.Windows.Forms.Panel()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.txtEmergencyPhone = New System.Windows.Forms.TextBox()
            Me.lblEmergencyPhone = New System.Windows.Forms.Label()
            Me.txtEmergencyName = New System.Windows.Forms.TextBox()
            Me.lblEmergencyName = New System.Windows.Forms.Label()
            Me.cboAcademicYear = New System.Windows.Forms.ComboBox()
            Me.lblAcademicYear = New System.Windows.Forms.Label()
            Me.cboDepartment = New System.Windows.Forms.ComboBox()
            Me.lblDepartment = New System.Windows.Forms.Label()
            Me.txtCourse = New System.Windows.Forms.TextBox()
            Me.lblCourse = New System.Windows.Forms.Label()
            Me.txtPhone = New System.Windows.Forms.TextBox()
            Me.lblPhone = New System.Windows.Forms.Label()
            Me.txtEmail = New System.Windows.Forms.TextBox()
            Me.lblEmail = New System.Windows.Forms.Label()
            Me.cboGender = New System.Windows.Forms.ComboBox()
            Me.lblGender = New System.Windows.Forms.Label()
            Me.txtLastName = New System.Windows.Forms.TextBox()
            Me.lblLastName = New System.Windows.Forms.Label()
            Me.txtMiddleName = New System.Windows.Forms.TextBox()
            Me.lblMiddleName = New System.Windows.Forms.Label()
            Me.txtFirstName = New System.Windows.Forms.TextBox()
            Me.lblFirstName = New System.Windows.Forms.Label()
            Me.txtRegNo = New System.Windows.Forms.TextBox()
            Me.lblRegNo = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            Me.pnlFooter.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopGold
            '
            Me.pnlTopGold.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.pnlTopGold.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopGold.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopGold.Name = "pnlTopGold"
            Me.pnlTopGold.Size = New System.Drawing.Size(580, 3)
            Me.pnlTopGold.TabIndex = 0
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.lblModalTitle)
            Me.pnlHeader.Controls.Add(Me.lblModalSub)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 3)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Padding = New System.Windows.Forms.Padding(24, 16, 24, 16)
            Me.pnlHeader.Size = New System.Drawing.Size(580, 68)
            Me.pnlHeader.TabIndex = 1
            '
            'lblModalTitle
            '
            Me.lblModalTitle.AutoSize = True
            Me.lblModalTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblModalTitle.ForeColor = System.Drawing.Color.White
            Me.lblModalTitle.Location = New System.Drawing.Point(22, 12)
            Me.lblModalTitle.Name = "lblModalTitle"
            Me.lblModalTitle.Size = New System.Drawing.Size(206, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Register Student Resident"
            '
            'lblModalSub
            '
            Me.lblModalSub.AutoSize = True
            Me.lblModalSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblModalSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblModalSub.Location = New System.Drawing.Point(23, 36)
            Me.lblModalSub.Name = "lblModalSub"
            Me.lblModalSub.Size = New System.Drawing.Size(431, 15)
            Me.lblModalSub.TabIndex = 1
            Me.lblModalSub.Text = "Register student biographical, academic, and emergency contact specifications."
            '
            'pnlBody
            '
            Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlBody.Controls.Add(Me.cboStatus)
            Me.pnlBody.Controls.Add(Me.lblStatus)
            Me.pnlBody.Controls.Add(Me.txtEmergencyPhone)
            Me.pnlBody.Controls.Add(Me.lblEmergencyPhone)
            Me.pnlBody.Controls.Add(Me.txtEmergencyName)
            Me.pnlBody.Controls.Add(Me.lblEmergencyName)
            Me.pnlBody.Controls.Add(Me.cboAcademicYear)
            Me.pnlBody.Controls.Add(Me.lblAcademicYear)
            Me.pnlBody.Controls.Add(Me.cboDepartment)
            Me.pnlBody.Controls.Add(Me.lblDepartment)
            Me.pnlBody.Controls.Add(Me.txtCourse)
            Me.pnlBody.Controls.Add(Me.lblCourse)
            Me.pnlBody.Controls.Add(Me.txtPhone)
            Me.pnlBody.Controls.Add(Me.lblPhone)
            Me.pnlBody.Controls.Add(Me.txtEmail)
            Me.pnlBody.Controls.Add(Me.lblEmail)
            Me.pnlBody.Controls.Add(Me.cboGender)
            Me.pnlBody.Controls.Add(Me.lblGender)
            Me.pnlBody.Controls.Add(Me.txtLastName)
            Me.pnlBody.Controls.Add(Me.lblLastName)
            Me.pnlBody.Controls.Add(Me.txtMiddleName)
            Me.pnlBody.Controls.Add(Me.lblMiddleName)
            Me.pnlBody.Controls.Add(Me.txtFirstName)
            Me.pnlBody.Controls.Add(Me.lblFirstName)
            Me.pnlBody.Controls.Add(Me.txtRegNo)
            Me.pnlBody.Controls.Add(Me.lblRegNo)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 71)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlBody.Size = New System.Drawing.Size(580, 520)
            Me.pnlBody.TabIndex = 2
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"ACTIVE", "SUSPENDED", "GRADUATED", "DEFERRED"})
            Me.cboStatus.Location = New System.Drawing.Point(298, 460)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(256, 25)
            Me.cboStatus.TabIndex = 25
            '
            'lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStatus.Location = New System.Drawing.Point(296, 440)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(95, 15)
            Me.lblStatus.TabIndex = 24
            Me.lblStatus.Text = "Student Status *"
            '
            'txtEmergencyPhone
            '
            Me.txtEmergencyPhone.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtEmergencyPhone.Location = New System.Drawing.Point(26, 460)
            Me.txtEmergencyPhone.MaxLength = 20
            Me.txtEmergencyPhone.Name = "txtEmergencyPhone"
            Me.txtEmergencyPhone.Size = New System.Drawing.Size(254, 24)
            Me.txtEmergencyPhone.TabIndex = 23
            '
            'lblEmergencyPhone
            '
            Me.lblEmergencyPhone.AutoSize = True
            Me.lblEmergencyPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblEmergencyPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblEmergencyPhone.Location = New System.Drawing.Point(24, 440)
            Me.lblEmergencyPhone.Name = "lblEmergencyPhone"
            Me.lblEmergencyPhone.Size = New System.Drawing.Size(152, 15)
            Me.lblEmergencyPhone.TabIndex = 22
            Me.lblEmergencyPhone.Text = "Emergency Contact Phone *"
            '
            'txtEmergencyName
            '
            Me.txtEmergencyName.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtEmergencyName.Location = New System.Drawing.Point(26, 396)
            Me.txtEmergencyName.MaxLength = 100
            Me.txtEmergencyName.Name = "txtEmergencyName"
            Me.txtEmergencyName.PlaceholderText = "e.g. Dr. Jane Doe (Parent/Guardian)"
            Me.txtEmergencyName.Size = New System.Drawing.Size(528, 24)
            Me.txtEmergencyName.TabIndex = 21
            '
            'lblEmergencyName
            '
            Me.lblEmergencyName.AutoSize = True
            Me.lblEmergencyName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblEmergencyName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblEmergencyName.Location = New System.Drawing.Point(24, 376)
            Me.lblEmergencyName.Name = "lblEmergencyName"
            Me.lblEmergencyName.Size = New System.Drawing.Size(193, 15)
            Me.lblEmergencyName.TabIndex = 20
            Me.lblEmergencyName.Text = "Emergency Contact / Next of Kin *"
            '
            'cboAcademicYear
            '
            Me.cboAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboAcademicYear.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboAcademicYear.FormattingEnabled = True
            Me.cboAcademicYear.Items.AddRange(New Object() {"1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year", "Postgraduate"})
            Me.cboAcademicYear.Location = New System.Drawing.Point(298, 332)
            Me.cboAcademicYear.Name = "cboAcademicYear"
            Me.cboAcademicYear.Size = New System.Drawing.Size(256, 25)
            Me.cboAcademicYear.TabIndex = 19
            '
            'lblAcademicYear
            '
            Me.lblAcademicYear.AutoSize = True
            Me.lblAcademicYear.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblAcademicYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblAcademicYear.Location = New System.Drawing.Point(296, 312)
            Me.lblAcademicYear.Name = "lblAcademicYear"
            Me.lblAcademicYear.Size = New System.Drawing.Size(95, 15)
            Me.lblAcademicYear.TabIndex = 18
            Me.lblAcademicYear.Text = "Academic Year *"
            '
            'cboDepartment
            '
            Me.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboDepartment.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboDepartment.FormattingEnabled = True
            Me.cboDepartment.Items.AddRange(New Object() {"School of Computing", "School of Business", "School of Engineering", "School of Health Sciences", "School of Humanities", "School of Law", "School of Education"})
            Me.cboDepartment.Location = New System.Drawing.Point(26, 332)
            Me.cboDepartment.Name = "cboDepartment"
            Me.cboDepartment.Size = New System.Drawing.Size(254, 25)
            Me.cboDepartment.TabIndex = 17
            '
            'lblDepartment
            '
            Me.lblDepartment.AutoSize = True
            Me.lblDepartment.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblDepartment.Location = New System.Drawing.Point(24, 312)
            Me.lblDepartment.Name = "lblDepartment"
            Me.lblDepartment.Size = New System.Drawing.Size(124, 15)
            Me.lblDepartment.TabIndex = 16
            Me.lblDepartment.Text = "Faculty / Department *"
            '
            'txtCourse
            '
            Me.txtCourse.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtCourse.Location = New System.Drawing.Point(26, 268)
            Me.txtCourse.MaxLength = 100
            Me.txtCourse.Name = "txtCourse"
            Me.txtCourse.PlaceholderText = "e.g. BSc. Computer Science"
            Me.txtCourse.Size = New System.Drawing.Size(528, 24)
            Me.txtCourse.TabIndex = 15
            '
            'lblCourse
            '
            Me.lblCourse.AutoSize = True
            Me.lblCourse.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblCourse.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblCourse.Location = New System.Drawing.Point(24, 248)
            Me.lblCourse.Name = "lblCourse"
            Me.lblCourse.Size = New System.Drawing.Size(94, 15)
            Me.lblCourse.TabIndex = 14
            Me.lblCourse.Text = "Degree Course *"
            '
            'txtPhone
            '
            Me.txtPhone.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtPhone.Location = New System.Drawing.Point(298, 204)
            Me.txtPhone.MaxLength = 20
            Me.txtPhone.Name = "txtPhone"
            Me.txtPhone.Size = New System.Drawing.Size(256, 24)
            Me.txtPhone.TabIndex = 13
            '
            'lblPhone
            '
            Me.lblPhone.AutoSize = True
            Me.lblPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblPhone.Location = New System.Drawing.Point(296, 184)
            Me.lblPhone.Name = "lblPhone"
            Me.lblPhone.Size = New System.Drawing.Size(97, 15)
            Me.lblPhone.TabIndex = 12
            Me.lblPhone.Text = "Phone Number *"
            '
            'txtEmail
            '
            Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtEmail.Location = New System.Drawing.Point(26, 204)
            Me.txtEmail.MaxLength = 100
            Me.txtEmail.Name = "txtEmail"
            Me.txtEmail.Size = New System.Drawing.Size(254, 24)
            Me.txtEmail.TabIndex = 11
            '
            'lblEmail
            '
            Me.lblEmail.AutoSize = True
            Me.lblEmail.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblEmail.Location = New System.Drawing.Point(24, 184)
            Me.lblEmail.Name = "lblEmail"
            Me.lblEmail.Size = New System.Drawing.Size(91, 15)
            Me.lblEmail.TabIndex = 10
            Me.lblEmail.Text = "Email Address *"
            '
            'cboGender
            '
            Me.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboGender.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboGender.FormattingEnabled = True
            Me.cboGender.Items.AddRange(New Object() {"MALE", "FEMALE", "OTHER"})
            Me.cboGender.Location = New System.Drawing.Point(298, 140)
            Me.cboGender.Name = "cboGender"
            Me.cboGender.Size = New System.Drawing.Size(256, 25)
            Me.cboGender.TabIndex = 9
            '
            'lblGender
            '
            Me.lblGender.AutoSize = True
            Me.lblGender.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblGender.Location = New System.Drawing.Point(296, 120)
            Me.lblGender.Name = "lblGender"
            Me.lblGender.Size = New System.Drawing.Size(55, 15)
            Me.lblGender.TabIndex = 8
            Me.lblGender.Text = "Gender *"
            '
            'txtLastName
            '
            Me.txtLastName.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtLastName.Location = New System.Drawing.Point(26, 140)
            Me.txtLastName.MaxLength = 50
            Me.txtLastName.Name = "txtLastName"
            Me.txtLastName.Size = New System.Drawing.Size(254, 24)
            Me.txtLastName.TabIndex = 7
            '
            'lblLastName
            '
            Me.lblLastName.AutoSize = True
            Me.lblLastName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblLastName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblLastName.Location = New System.Drawing.Point(24, 120)
            Me.lblLastName.Name = "lblLastName"
            Me.lblLastName.Size = New System.Drawing.Size(73, 15)
            Me.lblLastName.TabIndex = 6
            Me.lblLastName.Text = "Last Name *"
            '
            'txtMiddleName
            '
            Me.txtMiddleName.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtMiddleName.Location = New System.Drawing.Point(384, 76)
            Me.txtMiddleName.MaxLength = 50
            Me.txtMiddleName.Name = "txtMiddleName"
            Me.txtMiddleName.Size = New System.Drawing.Size(170, 24)
            Me.txtMiddleName.TabIndex = 5
            '
            'lblMiddleName
            '
            Me.lblMiddleName.AutoSize = True
            Me.lblMiddleName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblMiddleName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblMiddleName.Location = New System.Drawing.Point(382, 56)
            Me.lblMiddleName.Name = "lblMiddleName"
            Me.lblMiddleName.Size = New System.Drawing.Size(79, 15)
            Me.lblMiddleName.TabIndex = 4
            Me.lblMiddleName.Text = "Middle Name"
            '
            'txtFirstName
            '
            Me.txtFirstName.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtFirstName.Location = New System.Drawing.Point(204, 76)
            Me.txtFirstName.MaxLength = 50
            Me.txtFirstName.Name = "txtFirstName"
            Me.txtFirstName.Size = New System.Drawing.Size(166, 24)
            Me.txtFirstName.TabIndex = 3
            '
            'lblFirstName
            '
            Me.lblFirstName.AutoSize = True
            Me.lblFirstName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblFirstName.Location = New System.Drawing.Point(202, 56)
            Me.lblFirstName.Name = "lblFirstName"
            Me.lblFirstName.Size = New System.Drawing.Size(74, 15)
            Me.lblFirstName.TabIndex = 2
            Me.lblFirstName.Text = "First Name *"
            '
            'txtRegNo
            '
            Me.txtRegNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.txtRegNo.Location = New System.Drawing.Point(26, 76)
            Me.txtRegNo.MaxLength = 30
            Me.txtRegNo.Name = "txtRegNo"
            Me.txtRegNo.PlaceholderText = "e.g. CCS/00101/021"
            Me.txtRegNo.Size = New System.Drawing.Size(162, 24)
            Me.txtRegNo.TabIndex = 1
            '
            'lblRegNo
            '
            Me.lblRegNo.AutoSize = True
            Me.lblRegNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRegNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblRegNo.Location = New System.Drawing.Point(24, 56)
            Me.lblRegNo.Name = "lblRegNo"
            Me.lblRegNo.Size = New System.Drawing.Size(102, 15)
            Me.lblRegNo.TabIndex = 0
            Me.lblRegNo.Text = "Registration No. *"
            '
            'pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Controls.Add(Me.btnSave)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 591)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(24, 12, 24, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(580, 60)
            Me.pnlFooter.TabIndex = 3
            '
            'btnCancel
            '
            Me.btnCancel.BackColor = System.Drawing.Color.White
            Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnCancel.Location = New System.Drawing.Point(314, 13)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(110, 34)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'btnSave
            '
            Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.btnSave.FlatAppearance.BorderSize = 0
            Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSave.ForeColor = System.Drawing.Color.White
            Me.btnSave.Location = New System.Drawing.Point(432, 13)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(122, 34)
            Me.btnSave.TabIndex = 0
            Me.btnSave.Text = "Save Student"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            'frmStudentModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(580, 651)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopGold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmStudentModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Student Registration"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents txtEmergencyPhone As System.Windows.Forms.TextBox
        Friend WithEvents lblEmergencyPhone As System.Windows.Forms.Label
        Friend WithEvents txtEmergencyName As System.Windows.Forms.TextBox
        Friend WithEvents lblEmergencyName As System.Windows.Forms.Label
        Friend WithEvents cboAcademicYear As System.Windows.Forms.ComboBox
        Friend WithEvents lblAcademicYear As System.Windows.Forms.Label
        Friend WithEvents cboDepartment As System.Windows.Forms.ComboBox
        Friend WithEvents lblDepartment As System.Windows.Forms.Label
        Friend WithEvents txtCourse As System.Windows.Forms.TextBox
        Friend WithEvents lblCourse As System.Windows.Forms.Label
        Friend WithEvents txtPhone As System.Windows.Forms.TextBox
        Friend WithEvents lblPhone As System.Windows.Forms.Label
        Friend WithEvents txtEmail As System.Windows.Forms.TextBox
        Friend WithEvents lblEmail As System.Windows.Forms.Label
        Friend WithEvents cboGender As System.Windows.Forms.ComboBox
        Friend WithEvents lblGender As System.Windows.Forms.Label
        Friend WithEvents txtLastName As System.Windows.Forms.TextBox
        Friend WithEvents lblLastName As System.Windows.Forms.Label
        Friend WithEvents txtMiddleName As System.Windows.Forms.TextBox
        Friend WithEvents lblMiddleName As System.Windows.Forms.Label
        Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
        Friend WithEvents lblFirstName As System.Windows.Forms.Label
        Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
        Friend WithEvents lblRegNo As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnSave As System.Windows.Forms.Button
    End Class
End Namespace

