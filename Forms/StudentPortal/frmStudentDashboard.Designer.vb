Namespace Forms.StudentPortal
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmStudentDashboard
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
            Me.btnMaximize = New System.Windows.Forms.Button()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.btnSignOut = New University_hostel_mgt.Controls.GlassButton()
            Me.lblStudentPill = New System.Windows.Forms.Label()
            Me.lblSemesterPill = New System.Windows.Forms.Label()
            Me.lblSubBrand = New System.Windows.Forms.Label()
            Me.lblBrandTitle = New System.Windows.Forms.Label()
            Me.picLogo = New System.Windows.Forms.PictureBox()
            Me.pnlNavBar = New System.Windows.Forms.Panel()
            Me.btnNavMaintenance = New University_hostel_mgt.Controls.GlassButton()
            Me.btnNavBilling = New University_hostel_mgt.Controls.GlassButton()
            Me.btnNavBrowse = New University_hostel_mgt.Controls.GlassButton()
            Me.btnNavMyRoom = New University_hostel_mgt.Controls.GlassButton()
            Me.pnlContent = New System.Windows.Forms.Panel()
            Me.pnlTabMyRoom = New System.Windows.Forms.Panel()
            Me.pnlTabBrowse = New System.Windows.Forms.Panel()
            Me.pnlTabBilling = New System.Windows.Forms.Panel()
            Me.pnlTabMaintenance = New System.Windows.Forms.Panel()
            Me.pnlHeader.SuspendLayout()
            CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlNavBar.SuspendLayout()
            Me.pnlContent.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(1120, 3)
            Me.pnlTopAccent.TabIndex = 0
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.btnMinimize)
            Me.pnlHeader.Controls.Add(Me.btnMaximize)
            Me.pnlHeader.Controls.Add(Me.btnClose)
            Me.pnlHeader.Controls.Add(Me.btnSignOut)
            Me.pnlHeader.Controls.Add(Me.lblStudentPill)
            Me.pnlHeader.Controls.Add(Me.lblSemesterPill)
            Me.pnlHeader.Controls.Add(Me.lblSubBrand)
            Me.pnlHeader.Controls.Add(Me.lblBrandTitle)
            Me.pnlHeader.Controls.Add(Me.picLogo)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 3)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(1120, 56)
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
            Me.btnMinimize.Location = New System.Drawing.Point(1005, 12)
            Me.btnMinimize.Name = "btnMinimize"
            Me.btnMinimize.Size = New System.Drawing.Size(35, 30)
            Me.btnMinimize.TabIndex = 8
            Me.btnMinimize.Text = "—"
            Me.btnMinimize.UseVisualStyleBackColor = True
            '
            'btnMaximize
            '
            Me.btnMaximize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnMaximize.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnMaximize.FlatAppearance.BorderSize = 0
            Me.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnMaximize.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnMaximize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnMaximize.Location = New System.Drawing.Point(1042, 12)
            Me.btnMaximize.Name = "btnMaximize"
            Me.btnMaximize.Size = New System.Drawing.Size(35, 30)
            Me.btnMaximize.TabIndex = 7
            Me.btnMaximize.Text = "🗖"
            Me.btnMaximize.UseVisualStyleBackColor = True
            '
            'btnClose
            '
            Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnClose.FlatAppearance.BorderSize = 0
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnClose.Location = New System.Drawing.Point(1079, 12)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(35, 30)
            Me.btnClose.TabIndex = 6
            Me.btnClose.Text = "✕"
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'btnSignOut
            '
            Me.btnSignOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnSignOut.CornerRadius = 8
            Me.btnSignOut.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnSignOut.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSignOut.HoverColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.btnSignOut.Location = New System.Drawing.Point(915, 12)
            Me.btnSignOut.Name = "btnSignOut"
            Me.btnSignOut.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnSignOut.Size = New System.Drawing.Size(80, 32)
            Me.btnSignOut.TabIndex = 5
            Me.btnSignOut.Text = "Sign Out"
            '
            'lblStudentPill
            '
            Me.lblStudentPill.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblStudentPill.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(48, Byte), Integer))
            Me.lblStudentPill.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStudentPill.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblStudentPill.Location = New System.Drawing.Point(670, 12)
            Me.lblStudentPill.Name = "lblStudentPill"
            Me.lblStudentPill.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
            Me.lblStudentPill.Size = New System.Drawing.Size(265, 32)
            Me.lblStudentPill.TabIndex = 4
            Me.lblStudentPill.Text = "👤 Student Portal"
            Me.lblStudentPill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblSemesterPill
            '
            Me.lblSemesterPill.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblSemesterPill.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(48, Byte), Integer))
            Me.lblSemesterPill.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblSemesterPill.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.lblSemesterPill.Location = New System.Drawing.Point(450, 12)
            Me.lblSemesterPill.Name = "lblSemesterPill"
            Me.lblSemesterPill.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
            Me.lblSemesterPill.Size = New System.Drawing.Size(210, 32)
            Me.lblSemesterPill.TabIndex = 3
            Me.lblSemesterPill.Text = "📅 2025/2026 • Semester 2"
            Me.lblSemesterPill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblSubBrand
            '
            Me.lblSubBrand.AutoSize = True
            Me.lblSubBrand.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubBrand.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblSubBrand.Location = New System.Drawing.Point(62, 31)
            Me.lblSubBrand.Name = "lblSubBrand"
            Me.lblSubBrand.Size = New System.Drawing.Size(232, 13)
            Me.lblSubBrand.TabIndex = 2
            Me.lblSubBrand.Text = "Student Self-Service Accommodation Portal"
            '
            'lblBrandTitle
            '
            Me.lblBrandTitle.AutoSize = True
            Me.lblBrandTitle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBrandTitle.ForeColor = System.Drawing.Color.White
            Me.lblBrandTitle.Location = New System.Drawing.Point(62, 10)
            Me.lblBrandTitle.Name = "lblBrandTitle"
            Me.lblBrandTitle.Size = New System.Drawing.Size(329, 19)
            Me.lblBrandTitle.TabIndex = 1
            Me.lblBrandTitle.Text = "DEDAN KIMATHI UNIVERSITY OF TECHNOLOGY"
            '
            'picLogo
            '
            Me.picLogo.Location = New System.Drawing.Point(14, 8)
            Me.picLogo.Name = "picLogo"
            Me.picLogo.Size = New System.Drawing.Size(40, 40)
            Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picLogo.TabIndex = 0
            Me.picLogo.TabStop = False
            '
            'pnlNavBar
            '
            Me.pnlNavBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(18, Byte), Integer), CType(CType(28, Byte), Integer))
            Me.pnlNavBar.Controls.Add(Me.btnNavMaintenance)
            Me.pnlNavBar.Controls.Add(Me.btnNavBilling)
            Me.pnlNavBar.Controls.Add(Me.btnNavBrowse)
            Me.pnlNavBar.Controls.Add(Me.btnNavMyRoom)
            Me.pnlNavBar.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlNavBar.Location = New System.Drawing.Point(0, 59)
            Me.pnlNavBar.Name = "pnlNavBar"
            Me.pnlNavBar.Padding = New System.Windows.Forms.Padding(14, 6, 14, 6)
            Me.pnlNavBar.Size = New System.Drawing.Size(1120, 48)
            Me.pnlNavBar.TabIndex = 2
            '
            'btnNavMaintenance
            '
            Me.btnNavMaintenance.CornerRadius = 10
            Me.btnNavMaintenance.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnNavMaintenance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnNavMaintenance.HoverColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnNavMaintenance.Location = New System.Drawing.Point(674, 6)
            Me.btnNavMaintenance.Name = "btnNavMaintenance"
            Me.btnNavMaintenance.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnNavMaintenance.Size = New System.Drawing.Size(210, 36)
            Me.btnNavMaintenance.TabIndex = 3
            Me.btnNavMaintenance.Text = "🔧 Room Maintenance Desk"
            '
            'btnNavBilling
            '
            Me.btnNavBilling.CornerRadius = 10
            Me.btnNavBilling.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnNavBilling.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnNavBilling.HoverColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnNavBilling.Location = New System.Drawing.Point(454, 6)
            Me.btnNavBilling.Name = "btnNavBilling"
            Me.btnNavBilling.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnNavBilling.Size = New System.Drawing.Size(210, 36)
            Me.btnNavBilling.TabIndex = 2
            Me.btnNavBilling.Text = "💳 Fees & Electronic Receipts"
            '
            'btnNavBrowse
            '
            Me.btnNavBrowse.CornerRadius = 10
            Me.btnNavBrowse.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnNavBrowse.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnNavBrowse.HoverColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnNavBrowse.Location = New System.Drawing.Point(234, 6)
            Me.btnNavBrowse.Name = "btnNavBrowse"
            Me.btnNavBrowse.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnNavBrowse.Size = New System.Drawing.Size(210, 36)
            Me.btnNavBrowse.TabIndex = 1
            Me.btnNavBrowse.Text = "🛏️ Browse Available Units"
            '
            'btnNavMyRoom
            '
            Me.btnNavMyRoom.CornerRadius = 10
            Me.btnNavMyRoom.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnNavMyRoom.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnNavMyRoom.HoverColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
            Me.btnNavMyRoom.Location = New System.Drawing.Point(14, 6)
            Me.btnNavMyRoom.Name = "btnNavMyRoom"
            Me.btnNavMyRoom.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnNavMyRoom.Size = New System.Drawing.Size(210, 36)
            Me.btnNavMyRoom.TabIndex = 0
            Me.btnNavMyRoom.Text = "🏠 My Room & Residency"
            '
            'pnlContent
            '
            Me.pnlContent.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.pnlContent.Controls.Add(Me.pnlTabMyRoom)
            Me.pnlContent.Controls.Add(Me.pnlTabBrowse)
            Me.pnlContent.Controls.Add(Me.pnlTabBilling)
            Me.pnlContent.Controls.Add(Me.pnlTabMaintenance)
            Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContent.Location = New System.Drawing.Point(0, 107)
            Me.pnlContent.Name = "pnlContent"
            Me.pnlContent.Padding = New System.Windows.Forms.Padding(20)
            Me.pnlContent.Size = New System.Drawing.Size(1120, 633)
            Me.pnlContent.TabIndex = 3
            '
            'pnlTabMyRoom
            '
            Me.pnlTabMyRoom.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlTabMyRoom.Location = New System.Drawing.Point(20, 20)
            Me.pnlTabMyRoom.Name = "pnlTabMyRoom"
            Me.pnlTabMyRoom.Size = New System.Drawing.Size(1080, 593)
            Me.pnlTabMyRoom.TabIndex = 0
            '
            'pnlTabBrowse
            '
            Me.pnlTabBrowse.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlTabBrowse.Location = New System.Drawing.Point(20, 20)
            Me.pnlTabBrowse.Name = "pnlTabBrowse"
            Me.pnlTabBrowse.Size = New System.Drawing.Size(1080, 593)
            Me.pnlTabBrowse.TabIndex = 1
            Me.pnlTabBrowse.Visible = False
            '
            'pnlTabBilling
            '
            Me.pnlTabBilling.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlTabBilling.Location = New System.Drawing.Point(20, 20)
            Me.pnlTabBilling.Name = "pnlTabBilling"
            Me.pnlTabBilling.Size = New System.Drawing.Size(1080, 593)
            Me.pnlTabBilling.TabIndex = 2
            Me.pnlTabBilling.Visible = False
            '
            'pnlTabMaintenance
            '
            Me.pnlTabMaintenance.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlTabMaintenance.Location = New System.Drawing.Point(20, 20)
            Me.pnlTabMaintenance.Name = "pnlTabMaintenance"
            Me.pnlTabMaintenance.Size = New System.Drawing.Size(1080, 593)
            Me.pnlTabMaintenance.TabIndex = 3
            Me.pnlTabMaintenance.Visible = False
            '
            'frmStudentDashboard
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(1120, 740)
            Me.Controls.Add(Me.pnlContent)
            Me.Controls.Add(Me.pnlNavBar)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmStudentDashboard"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Student Accommodation Portal - Dedan Kimathi University of Technology"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlNavBar.ResumeLayout(False)
            Me.pnlContent.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents picLogo As System.Windows.Forms.PictureBox
        Friend WithEvents lblBrandTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubBrand As System.Windows.Forms.Label
        Friend WithEvents lblSemesterPill As System.Windows.Forms.Label
        Friend WithEvents lblStudentPill As System.Windows.Forms.Label
        Friend WithEvents btnSignOut As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents btnMaximize As System.Windows.Forms.Button
        Friend WithEvents btnMinimize As System.Windows.Forms.Button
        Friend WithEvents pnlNavBar As System.Windows.Forms.Panel
        Friend WithEvents btnNavMyRoom As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnNavBrowse As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnNavBilling As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnNavMaintenance As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents pnlContent As System.Windows.Forms.Panel
        Friend WithEvents pnlTabMyRoom As System.Windows.Forms.Panel
        Friend WithEvents pnlTabBrowse As System.Windows.Forms.Panel
        Friend WithEvents pnlTabBilling As System.Windows.Forms.Panel
        Friend WithEvents pnlTabMaintenance As System.Windows.Forms.Panel
    End Class
End Namespace

