Namespace Forms.Users
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmUserModal
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

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents lblFullName As System.Windows.Forms.Label
        Friend WithEvents txtFullName As System.Windows.Forms.TextBox
        Friend WithEvents lblUsername As System.Windows.Forms.Label
        Friend WithEvents txtUsername As System.Windows.Forms.TextBox
        Friend WithEvents lblEmail As System.Windows.Forms.Label
        Friend WithEvents txtEmail As System.Windows.Forms.TextBox
        Friend WithEvents lblRole As System.Windows.Forms.Label
        Friend WithEvents cboRole As System.Windows.Forms.ComboBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents pnlPasswordSection As System.Windows.Forms.Panel
        Friend WithEvents lblPassword As System.Windows.Forms.Label
        Friend WithEvents txtPassword As System.Windows.Forms.TextBox
        Friend WithEvents lblConfirm As System.Windows.Forms.Label
        Friend WithEvents txtConfirm As System.Windows.Forms.TextBox
        Friend WithEvents chkChangePassword As System.Windows.Forms.CheckBox
        Friend WithEvents btnSave As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.lblFullName = New System.Windows.Forms.Label()
            Me.txtFullName = New System.Windows.Forms.TextBox()
            Me.lblUsername = New System.Windows.Forms.Label()
            Me.txtUsername = New System.Windows.Forms.TextBox()
            Me.lblEmail = New System.Windows.Forms.Label()
            Me.txtEmail = New System.Windows.Forms.TextBox()
            Me.lblRole = New System.Windows.Forms.Label()
            Me.cboRole = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.pnlPasswordSection = New System.Windows.Forms.Panel()
            Me.txtConfirm = New System.Windows.Forms.TextBox()
            Me.lblConfirm = New System.Windows.Forms.Label()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.chkChangePassword = New System.Windows.Forms.CheckBox()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlPasswordSection.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(47, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.lblSubtitle)
            Me.pnlHeader.Controls.Add(Me.lblTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlHeader.Size = New System.Drawing.Size(520, 60)
            Me.pnlHeader.TabIndex = 0
            '
            'lblSubtitle
            '
            Me.lblSubtitle.AutoSize = True
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(20, 32)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(325, 15)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Configure staff credentials and role-based access permissions"
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(20, 10)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(262, 21)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "SYSTEM USER ADMINISTRATION"
            '
            'lblFullName
            '
            Me.lblFullName.AutoSize = True
            Me.lblFullName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblFullName.Location = New System.Drawing.Point(24, 76)
            Me.lblFullName.Name = "lblFullName"
            Me.lblFullName.Size = New System.Drawing.Size(95, 15)
            Me.lblFullName.TabIndex = 1
            Me.lblFullName.Text = "Staff Full Name *"
            '
            'txtFullName
            '
            Me.txtFullName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtFullName.Location = New System.Drawing.Point(24, 94)
            Me.txtFullName.Name = "txtFullName"
            Me.txtFullName.Size = New System.Drawing.Size(472, 23)
            Me.txtFullName.TabIndex = 2
            '
            'lblUsername
            '
            Me.lblUsername.AutoSize = True
            Me.lblUsername.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUsername.Location = New System.Drawing.Point(24, 130)
            Me.lblUsername.Name = "lblUsername"
            Me.lblUsername.Size = New System.Drawing.Size(68, 15)
            Me.lblUsername.TabIndex = 3
            Me.lblUsername.Text = "Username *"
            '
            'txtUsername
            '
            Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtUsername.Location = New System.Drawing.Point(24, 148)
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.Size = New System.Drawing.Size(220, 23)
            Me.txtUsername.TabIndex = 4
            '
            'lblEmail
            '
            Me.lblEmail.AutoSize = True
            Me.lblEmail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblEmail.Location = New System.Drawing.Point(260, 130)
            Me.lblEmail.Name = "lblEmail"
            Me.lblEmail.Size = New System.Drawing.Size(89, 15)
            Me.lblEmail.TabIndex = 5
            Me.lblEmail.Text = "Email Address *"
            '
            'txtEmail
            '
            Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtEmail.Location = New System.Drawing.Point(260, 148)
            Me.txtEmail.Name = "txtEmail"
            Me.txtEmail.Size = New System.Drawing.Size(236, 23)
            Me.txtEmail.TabIndex = 6
            '
            'lblRole
            '
            Me.lblRole.AutoSize = True
            Me.lblRole.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRole.Location = New System.Drawing.Point(24, 184)
            Me.lblRole.Name = "lblRole"
            Me.lblRole.Size = New System.Drawing.Size(85, 15)
            Me.lblRole.TabIndex = 7
            Me.lblRole.Text = "Assigned Role *"
            '
            'cboRole
            '
            Me.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRole.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboRole.FormattingEnabled = True
            Me.cboRole.Location = New System.Drawing.Point(24, 202)
            Me.cboRole.Name = "cboRole"
            Me.cboRole.Size = New System.Drawing.Size(220, 23)
            Me.cboRole.TabIndex = 8
            '
            'lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStatus.Location = New System.Drawing.Point(260, 184)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(97, 15)
            Me.lblStatus.TabIndex = 9
            Me.lblStatus.Text = "Account Status *"
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"ACTIVE", "INACTIVE", "SUSPENDED"})
            Me.cboStatus.Location = New System.Drawing.Point(260, 202)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(236, 23)
            Me.cboStatus.TabIndex = 10
            '
            'pnlPasswordSection
            '
            Me.pnlPasswordSection.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlPasswordSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlPasswordSection.Controls.Add(Me.txtConfirm)
            Me.pnlPasswordSection.Controls.Add(Me.lblConfirm)
            Me.pnlPasswordSection.Controls.Add(Me.txtPassword)
            Me.pnlPasswordSection.Controls.Add(Me.lblPassword)
            Me.pnlPasswordSection.Location = New System.Drawing.Point(24, 264)
            Me.pnlPasswordSection.Name = "pnlPasswordSection"
            Me.pnlPasswordSection.Padding = New System.Windows.Forms.Padding(12)
            Me.pnlPasswordSection.Size = New System.Drawing.Size(472, 80)
            Me.pnlPasswordSection.TabIndex = 12
            '
            'txtConfirm
            '
            Me.txtConfirm.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtConfirm.Location = New System.Drawing.Point(238, 38)
            Me.txtConfirm.Name = "txtConfirm"
            Me.txtConfirm.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
            Me.txtConfirm.Size = New System.Drawing.Size(218, 23)
            Me.txtConfirm.TabIndex = 3
            '
            'lblConfirm
            '
            Me.lblConfirm.AutoSize = True
            Me.lblConfirm.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblConfirm.Location = New System.Drawing.Point(238, 18)
            Me.lblConfirm.Name = "lblConfirm"
            Me.lblConfirm.Size = New System.Drawing.Size(107, 13)
            Me.lblConfirm.TabIndex = 2
            Me.lblConfirm.Text = "Confirm Password *"
            '
            'txtPassword
            '
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtPassword.Location = New System.Drawing.Point(14, 38)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
            Me.txtPassword.Size = New System.Drawing.Size(206, 23)
            Me.txtPassword.TabIndex = 1
            '
            'lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPassword.Location = New System.Drawing.Point(14, 18)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(130, 13)
            Me.lblPassword.TabIndex = 0
            Me.lblPassword.Text = "PBKDF2 Secure Password *"
            '
            'chkChangePassword
            '
            Me.chkChangePassword.AutoSize = True
            Me.chkChangePassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.chkChangePassword.Location = New System.Drawing.Point(24, 238)
            Me.chkChangePassword.Name = "chkChangePassword"
            Me.chkChangePassword.Size = New System.Drawing.Size(164, 19)
            Me.chkChangePassword.TabIndex = 11
            Me.chkChangePassword.Text = "Set / Change Password..."
            Me.chkChangePassword.UseVisualStyleBackColor = True
            '
            'btnSave
            '
            Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(47, Byte), Integer))
            Me.btnSave.FlatAppearance.BorderSize = 0
            Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSave.ForeColor = System.Drawing.Color.White
            Me.btnSave.Location = New System.Drawing.Point(276, 360)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(130, 36)
            Me.btnSave.TabIndex = 13
            Me.btnSave.Text = "💾 Save Staff User"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            'btnCancel
            '
            Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.Location = New System.Drawing.Point(416, 360)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(80, 36)
            Me.btnCancel.TabIndex = 14
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'frmUserModal
            '
            Me.AcceptButton = Me.btnSave
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(520, 412)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.chkChangePassword)
            Me.Controls.Add(Me.pnlPasswordSection)
            Me.Controls.Add(Me.cboStatus)
            Me.Controls.Add(Me.lblStatus)
            Me.Controls.Add(Me.cboRole)
            Me.Controls.Add(Me.lblRole)
            Me.Controls.Add(Me.txtEmail)
            Me.Controls.Add(Me.lblEmail)
            Me.Controls.Add(Me.txtUsername)
            Me.Controls.Add(Me.lblUsername)
            Me.Controls.Add(Me.txtFullName)
            Me.Controls.Add(Me.lblFullName)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmUserModal"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "System User Administration"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlPasswordSection.ResumeLayout(False)
            Me.pnlPasswordSection.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub
    End Class
End Namespace
