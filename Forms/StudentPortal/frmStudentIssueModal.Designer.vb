Namespace Forms.StudentPortal
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmStudentIssueModal
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
            Me.btnClose = New System.Windows.Forms.Button()
            Me.lblHeaderTitle = New System.Windows.Forms.Label()
            Me.pnlRoot = New University_hostel_mgt.Controls.GlassPanel()
            Me.btnCancel = New University_hostel_mgt.Controls.GlassButton()
            Me.btnSubmitTicket = New University_hostel_mgt.Controls.GlassButton()
            Me.lblStatusMessage = New System.Windows.Forms.Label()
            Me.lblEvidencePath = New System.Windows.Forms.Label()
            Me.btnBrowseImage = New University_hostel_mgt.Controls.GlassButton()
            Me.picEvidence = New System.Windows.Forms.PictureBox()
            Me.lblEvidence = New System.Windows.Forms.Label()
            Me.txtDescription = New System.Windows.Forms.TextBox()
            Me.lblDescription = New System.Windows.Forms.Label()
            Me.txtTitle = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblIssueTitle = New System.Windows.Forms.Label()
            Me.cboCategory = New System.Windows.Forms.ComboBox()
            Me.lblCategory = New System.Windows.Forms.Label()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlRoot.SuspendLayout()
            CType(Me.picEvidence, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(540, 3)
            Me.pnlTopAccent.TabIndex = 0
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.btnClose)
            Me.pnlHeader.Controls.Add(Me.lblHeaderTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 3)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(540, 38)
            Me.pnlHeader.TabIndex = 1
            '
            'btnClose
            '
            Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnClose.FlatAppearance.BorderSize = 0
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnClose.Location = New System.Drawing.Point(500, 4)
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
            Me.lblHeaderTitle.Size = New System.Drawing.Size(268, 15)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "Room Maintenance & Repairs • DeKUT Helpdesk"
            '
            'pnlRoot
            '
            Me.pnlRoot.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlRoot.BorderWidth = 1.0!
            Me.pnlRoot.Controls.Add(Me.btnCancel)
            Me.pnlRoot.Controls.Add(Me.btnSubmitTicket)
            Me.pnlRoot.Controls.Add(Me.lblStatusMessage)
            Me.pnlRoot.Controls.Add(Me.lblEvidencePath)
            Me.pnlRoot.Controls.Add(Me.btnBrowseImage)
            Me.pnlRoot.Controls.Add(Me.picEvidence)
            Me.pnlRoot.Controls.Add(Me.lblEvidence)
            Me.pnlRoot.Controls.Add(Me.txtDescription)
            Me.pnlRoot.Controls.Add(Me.lblDescription)
            Me.pnlRoot.Controls.Add(Me.txtTitle)
            Me.pnlRoot.Controls.Add(Me.lblIssueTitle)
            Me.pnlRoot.Controls.Add(Me.cboCategory)
            Me.pnlRoot.Controls.Add(Me.lblCategory)
            Me.pnlRoot.Controls.Add(Me.lblSubtitle)
            Me.pnlRoot.Controls.Add(Me.lblTitle)
            Me.pnlRoot.CornerRadius = 18
            Me.pnlRoot.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlRoot.FillColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(31, Byte), Integer))
            Me.pnlRoot.GlowColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlRoot.Location = New System.Drawing.Point(0, 41)
            Me.pnlRoot.Name = "pnlRoot"
            Me.pnlRoot.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlRoot.Size = New System.Drawing.Size(540, 529)
            Me.pnlRoot.TabIndex = 2
            '
            'btnCancel
            '
            Me.btnCancel.CornerRadius = 12
            Me.btnCancel.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.HoverColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnCancel.Location = New System.Drawing.Point(24, 474)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnCancel.Size = New System.Drawing.Size(492, 36)
            Me.btnCancel.TabIndex = 14
            Me.btnCancel.Text = "Cancel"
            '
            'btnSubmitTicket
            '
            Me.btnSubmitTicket.CornerRadius = 12
            Me.btnSubmitTicket.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnSubmitTicket.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSubmitTicket.HoverColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
            Me.btnSubmitTicket.Location = New System.Drawing.Point(24, 424)
            Me.btnSubmitTicket.Name = "btnSubmitTicket"
            Me.btnSubmitTicket.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnSubmitTicket.Size = New System.Drawing.Size(492, 42)
            Me.btnSubmitTicket.TabIndex = 13
            Me.btnSubmitTicket.Text = "Submit Maintenance Ticket"
            '
            'lblStatusMessage
            '
            Me.lblStatusMessage.BackColor = System.Drawing.Color.Transparent
            Me.lblStatusMessage.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.lblStatusMessage.Location = New System.Drawing.Point(24, 394)
            Me.lblStatusMessage.Name = "lblStatusMessage"
            Me.lblStatusMessage.Size = New System.Drawing.Size(492, 24)
            Me.lblStatusMessage.TabIndex = 12
            Me.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblEvidencePath
            '
            Me.lblEvidencePath.BackColor = System.Drawing.Color.Transparent
            Me.lblEvidencePath.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblEvidencePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblEvidencePath.Location = New System.Drawing.Point(216, 350)
            Me.lblEvidencePath.Name = "lblEvidencePath"
            Me.lblEvidencePath.Size = New System.Drawing.Size(300, 28)
            Me.lblEvidencePath.TabIndex = 11
            Me.lblEvidencePath.Text = "No photo selected (optional)"
            Me.lblEvidencePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'btnBrowseImage
            '
            Me.btnBrowseImage.CornerRadius = 8
            Me.btnBrowseImage.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.btnBrowseImage.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnBrowseImage.HoverColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnBrowseImage.Location = New System.Drawing.Point(86, 350)
            Me.btnBrowseImage.Name = "btnBrowseImage"
            Me.btnBrowseImage.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnBrowseImage.Size = New System.Drawing.Size(120, 28)
            Me.btnBrowseImage.TabIndex = 10
            Me.btnBrowseImage.Text = "Browse Photo..."
            '
            'picEvidence
            '
            Me.picEvidence.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(35, Byte), Integer))
            Me.picEvidence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.picEvidence.Location = New System.Drawing.Point(24, 340)
            Me.picEvidence.Name = "picEvidence"
            Me.picEvidence.Size = New System.Drawing.Size(52, 48)
            Me.picEvidence.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picEvidence.TabIndex = 9
            Me.picEvidence.TabStop = False
            '
            'lblEvidence
            '
            Me.lblEvidence.AutoSize = True
            Me.lblEvidence.BackColor = System.Drawing.Color.Transparent
            Me.lblEvidence.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblEvidence.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblEvidence.Location = New System.Drawing.Point(24, 320)
            Me.lblEvidence.Name = "lblEvidence"
            Me.lblEvidence.Size = New System.Drawing.Size(161, 15)
            Me.lblEvidence.TabIndex = 8
            Me.lblEvidence.Text = "Attach Evidence Photograph"
            '
            'txtDescription
            '
            Me.txtDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(35, Byte), Integer))
            Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.txtDescription.Location = New System.Drawing.Point(24, 218)
            Me.txtDescription.Multiline = True
            Me.txtDescription.Name = "txtDescription"
            Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtDescription.Size = New System.Drawing.Size(492, 90)
            Me.txtDescription.TabIndex = 7
            '
            'lblDescription
            '
            Me.lblDescription.AutoSize = True
            Me.lblDescription.BackColor = System.Drawing.Color.Transparent
            Me.lblDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblDescription.Location = New System.Drawing.Point(24, 198)
            Me.lblDescription.Name = "lblDescription"
            Me.lblDescription.Size = New System.Drawing.Size(111, 15)
            Me.lblDescription.TabIndex = 6
            Me.lblDescription.Text = "Defect Description *"
            '
            'txtTitle
            '
            Me.txtTitle.Location = New System.Drawing.Point(24, 152)
            Me.txtTitle.Name = "txtTitle"
            Me.txtTitle.PlaceholderText = "e.g. Leaking sink faucet or broken locker latch"
            Me.txtTitle.Size = New System.Drawing.Size(492, 36)
            Me.txtTitle.TabIndex = 5
            '
            'lblIssueTitle
            '
            Me.lblIssueTitle.AutoSize = True
            Me.lblIssueTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblIssueTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblIssueTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblIssueTitle.Location = New System.Drawing.Point(24, 132)
            Me.lblIssueTitle.Name = "lblIssueTitle"
            Me.lblIssueTitle.Size = New System.Drawing.Size(107, 15)
            Me.lblIssueTitle.TabIndex = 4
            Me.lblIssueTitle.Text = "Brief Defect Title *"
            '
            'cboCategory
            '
            Me.cboCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(35, Byte), Integer))
            Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cboCategory.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.cboCategory.FormattingEnabled = True
            Me.cboCategory.Items.AddRange(New Object() {"Plumbing (Water / Drainage)", "Electrical (Sockets / Lights)", "Furniture (Bed / Desk / Chair)", "Carpentry & Locks (Doors / Keys)", "Wi-Fi & Network Access", "General Maintenance"})
            Me.cboCategory.Location = New System.Drawing.Point(24, 98)
            Me.cboCategory.Name = "cboCategory"
            Me.cboCategory.Size = New System.Drawing.Size(492, 25)
            Me.cboCategory.TabIndex = 3
            '
            'lblCategory
            '
            Me.lblCategory.AutoSize = True
            Me.lblCategory.BackColor = System.Drawing.Color.Transparent
            Me.lblCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblCategory.Location = New System.Drawing.Point(24, 78)
            Me.lblCategory.Name = "lblCategory"
            Me.lblCategory.Size = New System.Drawing.Size(98, 15)
            Me.lblCategory.TabIndex = 2
            Me.lblCategory.Text = "Issue Category *"
            '
            'lblSubtitle
            '
            Me.lblSubtitle.AutoSize = True
            Me.lblSubtitle.BackColor = System.Drawing.Color.Transparent
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(24, 46)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(342, 15)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Submit repair request directly to university hostel estate officers"
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblTitle.Location = New System.Drawing.Point(24, 20)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(262, 25)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "Report Room Defect or Issue"
            '
            'frmStudentIssueModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(540, 570)
            Me.Controls.Add(Me.pnlRoot)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmStudentIssueModal"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Report Maintenance Issue"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlRoot.ResumeLayout(False)
            Me.pnlRoot.PerformLayout()
            CType(Me.picEvidence, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlRoot As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents lblCategory As System.Windows.Forms.Label
        Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
        Friend WithEvents lblIssueTitle As System.Windows.Forms.Label
        Friend WithEvents txtTitle As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblDescription As System.Windows.Forms.Label
        Friend WithEvents txtDescription As System.Windows.Forms.TextBox
        Friend WithEvents lblEvidence As System.Windows.Forms.Label
        Friend WithEvents picEvidence As System.Windows.Forms.PictureBox
        Friend WithEvents btnBrowseImage As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents lblEvidencePath As System.Windows.Forms.Label
        Friend WithEvents lblStatusMessage As System.Windows.Forms.Label
        Friend WithEvents btnSubmitTicket As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnCancel As University_hostel_mgt.Controls.GlassButton
    End Class
End Namespace

