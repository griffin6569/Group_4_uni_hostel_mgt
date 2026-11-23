Namespace Forms.Maintenance
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmMaintenanceModal
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
        Friend WithEvents lblHostel As System.Windows.Forms.Label
        Friend WithEvents cboHostel As System.Windows.Forms.ComboBox
        Friend WithEvents lblRoom As System.Windows.Forms.Label
        Friend WithEvents cboRoom As System.Windows.Forms.ComboBox
        Friend WithEvents lblBed As System.Windows.Forms.Label
        Friend WithEvents cboBed As System.Windows.Forms.ComboBox
        Friend WithEvents pnlNotice As System.Windows.Forms.Panel
        Friend WithEvents lblNotice As System.Windows.Forms.Label
        Friend WithEvents lblIssueTitle As System.Windows.Forms.Label
        Friend WithEvents txtIssueTitle As System.Windows.Forms.TextBox
        Friend WithEvents lblDescription As System.Windows.Forms.Label
        Friend WithEvents txtDescription As System.Windows.Forms.TextBox
        Friend WithEvents lblAssigned As System.Windows.Forms.Label
        Friend WithEvents txtAssignedTo As System.Windows.Forms.TextBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblEvidence As System.Windows.Forms.Label
        Friend WithEvents picEvidence As System.Windows.Forms.PictureBox
        Friend WithEvents btnAttachEvidence As System.Windows.Forms.Button
        Friend WithEvents lblEvidencePath As System.Windows.Forms.Label
        Friend WithEvents btnSave As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.lblHostel = New System.Windows.Forms.Label()
            Me.cboHostel = New System.Windows.Forms.ComboBox()
            Me.lblRoom = New System.Windows.Forms.Label()
            Me.cboRoom = New System.Windows.Forms.ComboBox()
            Me.lblBed = New System.Windows.Forms.Label()
            Me.cboBed = New System.Windows.Forms.ComboBox()
            Me.pnlNotice = New System.Windows.Forms.Panel()
            Me.lblNotice = New System.Windows.Forms.Label()
            Me.lblIssueTitle = New System.Windows.Forms.Label()
            Me.txtIssueTitle = New System.Windows.Forms.TextBox()
            Me.lblDescription = New System.Windows.Forms.Label()
            Me.txtDescription = New System.Windows.Forms.TextBox()
            Me.lblAssigned = New System.Windows.Forms.Label()
            Me.txtAssignedTo = New System.Windows.Forms.TextBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblEvidence = New System.Windows.Forms.Label()
            Me.picEvidence = New System.Windows.Forms.PictureBox()
            Me.btnAttachEvidence = New System.Windows.Forms.Button()
            Me.lblEvidencePath = New System.Windows.Forms.Label()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlNotice.SuspendLayout()
            CType(Me.picEvidence, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.pnlHeader.Size = New System.Drawing.Size(680, 64)
            Me.pnlHeader.TabIndex = 0
            '
            'lblSubtitle
            '
            Me.lblSubtitle.AutoSize = True
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(20, 34)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(370, 15)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Report facility issues, assign maintenance staff, and lock bed allocation"
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(20, 10)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(287, 21)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "MAINTENANCE INCIDENT LOGGING"
            '
            'lblHostel
            '
            Me.lblHostel.AutoSize = True
            Me.lblHostel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHostel.Location = New System.Drawing.Point(24, 78)
            Me.lblHostel.Name = "lblHostel"
            Me.lblHostel.Size = New System.Drawing.Size(89, 15)
            Me.lblHostel.TabIndex = 1
            Me.lblHostel.Text = "Hostel Facility *"
            '
            'cboHostel
            '
            Me.cboHostel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboHostel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboHostel.FormattingEnabled = True
            Me.cboHostel.Location = New System.Drawing.Point(24, 96)
            Me.cboHostel.Name = "cboHostel"
            Me.cboHostel.Size = New System.Drawing.Size(200, 23)
            Me.cboHostel.TabIndex = 2
            '
            'lblRoom
            '
            Me.lblRoom.AutoSize = True
            Me.lblRoom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRoom.Location = New System.Drawing.Point(240, 78)
            Me.lblRoom.Name = "lblRoom"
            Me.lblRoom.Size = New System.Drawing.Size(46, 15)
            Me.lblRoom.TabIndex = 3
            Me.lblRoom.Text = "Room *"
            '
            'cboRoom
            '
            Me.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRoom.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboRoom.FormattingEnabled = True
            Me.cboRoom.Location = New System.Drawing.Point(240, 96)
            Me.cboRoom.Name = "cboRoom"
            Me.cboRoom.Size = New System.Drawing.Size(190, 23)
            Me.cboRoom.TabIndex = 4
            '
            'lblBed
            '
            Me.lblBed.AutoSize = True
            Me.lblBed.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBed.Location = New System.Drawing.Point(446, 78)
            Me.lblBed.Name = "lblBed"
            Me.lblBed.Size = New System.Drawing.Size(68, 15)
            Me.lblBed.TabIndex = 5
            Me.lblBed.Text = "Target Bed *"
            '
            'cboBed
            '
            Me.cboBed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboBed.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboBed.FormattingEnabled = True
            Me.cboBed.Location = New System.Drawing.Point(446, 96)
            Me.cboBed.Name = "cboBed"
            Me.cboBed.Size = New System.Drawing.Size(210, 23)
            Me.cboBed.TabIndex = 6
            '
            'pnlNotice
            '
            Me.pnlNotice.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(199, Byte), Integer))
            Me.pnlNotice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlNotice.Controls.Add(Me.lblNotice)
            Me.pnlNotice.Location = New System.Drawing.Point(24, 132)
            Me.pnlNotice.Name = "pnlNotice"
            Me.pnlNotice.Padding = New System.Windows.Forms.Padding(10, 6, 10, 6)
            Me.pnlNotice.Size = New System.Drawing.Size(632, 38)
            Me.pnlNotice.TabIndex = 7
            '
            'lblNotice
            '
            Me.lblNotice.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblNotice.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNotice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.lblNotice.Location = New System.Drawing.Point(10, 6)
            Me.lblNotice.Name = "lblNotice"
            Me.lblNotice.Size = New System.Drawing.Size(610, 24)
            Me.lblNotice.TabIndex = 0
            Me.lblNotice.Text = "⚠️ Business Rule: Selecting an individual bed will automatically transition its s" &
    "tatus to MAINTENANCE and block new allocations."
            Me.lblNotice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'lblIssueTitle
            '
            Me.lblIssueTitle.AutoSize = True
            Me.lblIssueTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblIssueTitle.Location = New System.Drawing.Point(24, 182)
            Me.lblIssueTitle.Name = "lblIssueTitle"
            Me.lblIssueTitle.Size = New System.Drawing.Size(125, 15)
            Me.lblIssueTitle.TabIndex = 8
            Me.lblIssueTitle.Text = "Incident Summary Title *"
            '
            'txtIssueTitle
            '
            Me.txtIssueTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtIssueTitle.Location = New System.Drawing.Point(24, 200)
            Me.txtIssueTitle.Name = "txtIssueTitle"
            Me.txtIssueTitle.Size = New System.Drawing.Size(632, 23)
            Me.txtIssueTitle.TabIndex = 9
            '
            'lblDescription
            '
            Me.lblDescription.AutoSize = True
            Me.lblDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDescription.Location = New System.Drawing.Point(24, 234)
            Me.lblDescription.Name = "lblDescription"
            Me.lblDescription.Size = New System.Drawing.Size(155, 15)
            Me.lblDescription.TabIndex = 10
            Me.lblDescription.Text = "Detailed Issue Description *"
            '
            'txtDescription
            '
            Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtDescription.Location = New System.Drawing.Point(24, 252)
            Me.txtDescription.Multiline = True
            Me.txtDescription.Name = "txtDescription"
            Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtDescription.Size = New System.Drawing.Size(410, 84)
            Me.txtDescription.TabIndex = 11
            '
            'lblAssigned
            '
            Me.lblAssigned.AutoSize = True
            Me.lblAssigned.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblAssigned.Location = New System.Drawing.Point(24, 348)
            Me.lblAssigned.Name = "lblAssigned"
            Me.lblAssigned.Size = New System.Drawing.Size(170, 15)
            Me.lblAssigned.TabIndex = 12
            Me.lblAssigned.Text = "Assigned Technician / Contractor"
            '
            'txtAssignedTo
            '
            Me.txtAssignedTo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtAssignedTo.Location = New System.Drawing.Point(24, 366)
            Me.txtAssignedTo.Name = "txtAssignedTo"
            Me.txtAssignedTo.Size = New System.Drawing.Size(260, 23)
            Me.txtAssignedTo.TabIndex = 13
            '
            'lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStatus.Location = New System.Drawing.Point(300, 348)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(117, 15)
            Me.lblStatus.TabIndex = 14
            Me.lblStatus.Text = "Maintenance Status *"
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"REPORTED", "IN_PROGRESS", "RESOLVED", "CANCELLED"})
            Me.cboStatus.Location = New System.Drawing.Point(300, 366)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(134, 23)
            Me.cboStatus.TabIndex = 15
            '
            'lblEvidence
            '
            Me.lblEvidence.AutoSize = True
            Me.lblEvidence.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblEvidence.Location = New System.Drawing.Point(456, 234)
            Me.lblEvidence.Name = "lblEvidence"
            Me.lblEvidence.Size = New System.Drawing.Size(170, 15)
            Me.lblEvidence.TabIndex = 16
            Me.lblEvidence.Text = "Photographic Defect Evidence"
            '
            'picEvidence
            '
            Me.picEvidence.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.picEvidence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.picEvidence.Location = New System.Drawing.Point(456, 252)
            Me.picEvidence.Name = "picEvidence"
            Me.picEvidence.Size = New System.Drawing.Size(200, 105)
            Me.picEvidence.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picEvidence.TabIndex = 17
            Me.picEvidence.TabStop = False
            '
            'btnAttachEvidence
            '
            Me.btnAttachEvidence.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.btnAttachEvidence.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnAttachEvidence.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnAttachEvidence.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnAttachEvidence.Location = New System.Drawing.Point(456, 363)
            Me.btnAttachEvidence.Name = "btnAttachEvidence"
            Me.btnAttachEvidence.Size = New System.Drawing.Size(200, 26)
            Me.btnAttachEvidence.TabIndex = 18
            Me.btnAttachEvidence.Text = "📷 Attach Photograph..."
            Me.btnAttachEvidence.UseVisualStyleBackColor = False
            '
            'lblEvidencePath
            '
            Me.lblEvidencePath.AutoEllipsis = True
            Me.lblEvidencePath.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
            Me.lblEvidencePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblEvidencePath.Location = New System.Drawing.Point(456, 392)
            Me.lblEvidencePath.Name = "lblEvidencePath"
            Me.lblEvidencePath.Size = New System.Drawing.Size(200, 18)
            Me.lblEvidencePath.TabIndex = 19
            Me.lblEvidencePath.Text = "No photo attached"
            '
            'btnSave
            '
            Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(47, Byte), Integer))
            Me.btnSave.FlatAppearance.BorderSize = 0
            Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSave.ForeColor = System.Drawing.Color.White
            Me.btnSave.Location = New System.Drawing.Point(416, 425)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(140, 36)
            Me.btnSave.TabIndex = 20
            Me.btnSave.Text = "💾 Save Incident"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            'btnCancel
            '
            Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.Location = New System.Drawing.Point(566, 425)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(90, 36)
            Me.btnCancel.TabIndex = 21
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'frmMaintenanceModal
            '
            Me.AcceptButton = Me.btnSave
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(680, 480)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.lblEvidencePath)
            Me.Controls.Add(Me.btnAttachEvidence)
            Me.Controls.Add(Me.picEvidence)
            Me.Controls.Add(Me.lblEvidence)
            Me.Controls.Add(Me.cboStatus)
            Me.Controls.Add(Me.lblStatus)
            Me.Controls.Add(Me.txtAssignedTo)
            Me.Controls.Add(Me.lblAssigned)
            Me.Controls.Add(Me.txtDescription)
            Me.Controls.Add(Me.lblDescription)
            Me.Controls.Add(Me.txtIssueTitle)
            Me.Controls.Add(Me.lblIssueTitle)
            Me.Controls.Add(Me.pnlNotice)
            Me.Controls.Add(Me.cboBed)
            Me.Controls.Add(Me.lblBed)
            Me.Controls.Add(Me.cboRoom)
            Me.Controls.Add(Me.lblRoom)
            Me.Controls.Add(Me.cboHostel)
            Me.Controls.Add(Me.lblHostel)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmMaintenanceModal"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Hostel Maintenance Incident Desk"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlNotice.ResumeLayout(False)
            CType(Me.picEvidence, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub
    End Class
End Namespace
