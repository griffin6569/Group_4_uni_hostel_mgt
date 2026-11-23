Namespace Forms.Allocations
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmCheckInOutModal
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
            Me.pnlResidentCard = New System.Windows.Forms.Panel()
            Me.lblAllocInfo = New System.Windows.Forms.Label()
            Me.lblBedInfo = New System.Windows.Forms.Label()
            Me.lblStudentInfo = New System.Windows.Forms.Label()
            Me.dtpActionDate = New System.Windows.Forms.DateTimePicker()
            Me.lblDate = New System.Windows.Forms.Label()
            Me.chkKeys = New System.Windows.Forms.CheckBox()
            Me.chkInventory = New System.Windows.Forms.CheckBox()
            Me.chkMaintenance = New System.Windows.Forms.CheckBox()
            Me.txtNotes = New System.Windows.Forms.TextBox()
            Me.lblNotes = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnConfirm = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            Me.pnlResidentCard.SuspendLayout()
            Me.pnlFooter.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopGold
            '
            Me.pnlTopGold.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.pnlTopGold.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopGold.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopGold.Name = "pnlTopGold"
            Me.pnlTopGold.Size = New System.Drawing.Size(540, 3)
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
            Me.pnlHeader.Size = New System.Drawing.Size(540, 68)
            Me.pnlHeader.TabIndex = 1
            '
            'lblModalTitle
            '
            Me.lblModalTitle.AutoSize = True
            Me.lblModalTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblModalTitle.ForeColor = System.Drawing.Color.White
            Me.lblModalTitle.Location = New System.Drawing.Point(22, 12)
            Me.lblModalTitle.Name = "lblModalTitle"
            Me.lblModalTitle.Size = New System.Drawing.Size(184, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Resident Check-In Desk"
            '
            'lblModalSub
            '
            Me.lblModalSub.AutoSize = True
            Me.lblModalSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblModalSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblModalSub.Location = New System.Drawing.Point(23, 36)
            Me.lblModalSub.Name = "lblModalSub"
            Me.lblModalSub.Size = New System.Drawing.Size(377, 15)
            Me.lblModalSub.TabIndex = 1
            Me.lblModalSub.Text = "Verify room inventory, issue room keys, and log formal arrival timestamp."
            '
            'pnlBody
            '
            Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlBody.Controls.Add(Me.txtNotes)
            Me.pnlBody.Controls.Add(Me.lblNotes)
            Me.pnlBody.Controls.Add(Me.chkMaintenance)
            Me.pnlBody.Controls.Add(Me.chkInventory)
            Me.pnlBody.Controls.Add(Me.chkKeys)
            Me.pnlBody.Controls.Add(Me.dtpActionDate)
            Me.pnlBody.Controls.Add(Me.lblDate)
            Me.pnlBody.Controls.Add(Me.pnlResidentCard)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 71)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlBody.Size = New System.Drawing.Size(540, 399)
            Me.pnlBody.TabIndex = 2
            '
            'pnlResidentCard
            '
            Me.pnlResidentCard.BackColor = System.Drawing.Color.White
            Me.pnlResidentCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlResidentCard.Controls.Add(Me.lblAllocInfo)
            Me.pnlResidentCard.Controls.Add(Me.lblBedInfo)
            Me.pnlResidentCard.Controls.Add(Me.lblStudentInfo)
            Me.pnlResidentCard.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlResidentCard.Location = New System.Drawing.Point(24, 24)
            Me.pnlResidentCard.Name = "pnlResidentCard"
            Me.pnlResidentCard.Padding = New System.Windows.Forms.Padding(16, 12, 16, 12)
            Me.pnlResidentCard.Size = New System.Drawing.Size(492, 80)
            Me.pnlResidentCard.TabIndex = 0
            '
            'lblAllocInfo
            '
            Me.lblAllocInfo.AutoSize = True
            Me.lblAllocInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblAllocInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblAllocInfo.Location = New System.Drawing.Point(14, 54)
            Me.lblAllocInfo.Name = "lblAllocInfo"
            Me.lblAllocInfo.Size = New System.Drawing.Size(264, 13)
            Me.lblAllocInfo.TabIndex = 2
            Me.lblAllocInfo.Text = "Allocation #101 • Semester 1 2026/2027 • ACTIVE"
            '
            'lblBedInfo
            '
            Me.lblBedInfo.AutoSize = True
            Me.lblBedInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBedInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.lblBedInfo.Location = New System.Drawing.Point(14, 34)
            Me.lblBedInfo.Name = "lblBedInfo"
            Me.lblBedInfo.Size = New System.Drawing.Size(262, 15)
            Me.lblBedInfo.TabIndex = 1
            Me.lblBedInfo.Text = "Mount Kenya Hall • Rm 101 • Bed A (Standard)"
            '
            'lblStudentInfo
            '
            Me.lblStudentInfo.AutoSize = True
            Me.lblStudentInfo.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStudentInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStudentInfo.Location = New System.Drawing.Point(14, 12)
            Me.lblStudentInfo.Name = "lblStudentInfo"
            Me.lblStudentInfo.Size = New System.Drawing.Size(267, 19)
            Me.lblStudentInfo.TabIndex = 0
            Me.lblStudentInfo.Text = "Arthur Pendelton (CCS/00101/021)"
            '
            'dtpActionDate
            '
            Me.dtpActionDate.CustomFormat = "yyyy-MM-dd HH:mm"
            Me.dtpActionDate.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.dtpActionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.dtpActionDate.Location = New System.Drawing.Point(26, 134)
            Me.dtpActionDate.Name = "dtpActionDate"
            Me.dtpActionDate.Size = New System.Drawing.Size(488, 24)
            Me.dtpActionDate.TabIndex = 2
            '
            'lblDate
            '
            Me.lblDate.AutoSize = True
            Me.lblDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblDate.Location = New System.Drawing.Point(24, 114)
            Me.lblDate.Name = "lblDate"
            Me.lblDate.Size = New System.Drawing.Size(127, 15)
            Me.lblDate.TabIndex = 1
            Me.lblDate.Text = "Check-In Timestamp *"
            '
            'chkKeys
            '
            Me.chkKeys.AutoSize = True
            Me.chkKeys.Checked = True
            Me.chkKeys.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkKeys.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.chkKeys.Location = New System.Drawing.Point(28, 172)
            Me.chkKeys.Name = "chkKeys"
            Me.chkKeys.Size = New System.Drawing.Size(292, 19)
            Me.chkKeys.TabIndex = 3
            Me.chkKeys.Text = "Room key & wardrobe padlock issued to resident"
            Me.chkKeys.UseVisualStyleBackColor = True
            '
            'chkInventory
            '
            Me.chkInventory.AutoSize = True
            Me.chkInventory.Checked = True
            Me.chkInventory.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkInventory.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.chkInventory.Location = New System.Drawing.Point(28, 200)
            Me.chkInventory.Name = "chkInventory"
            Me.chkInventory.Size = New System.Drawing.Size(325, 19)
            Me.chkInventory.TabIndex = 4
            Me.chkInventory.Text = "Bed frame, mattress, study desk, and light inspected"
            Me.chkInventory.UseVisualStyleBackColor = True
            '
            'chkMaintenance
            '
            Me.chkMaintenance.AutoSize = True
            Me.chkMaintenance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.chkMaintenance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(9, Byte), Integer))
            Me.chkMaintenance.Location = New System.Drawing.Point(28, 228)
            Me.chkMaintenance.Name = "chkMaintenance"
            Me.chkMaintenance.Size = New System.Drawing.Size(346, 19)
            Me.chkMaintenance.TabIndex = 5
            Me.chkMaintenance.Text = "Flag Bed Space for Maintenance Assessment on Release"
            Me.chkMaintenance.UseVisualStyleBackColor = True
            Me.chkMaintenance.Visible = False
            '
            'txtNotes
            '
            Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtNotes.Location = New System.Drawing.Point(26, 276)
            Me.txtNotes.Multiline = True
            Me.txtNotes.Name = "txtNotes"
            Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtNotes.Size = New System.Drawing.Size(488, 90)
            Me.txtNotes.TabIndex = 7
            '
            'lblNotes
            '
            Me.lblNotes.AutoSize = True
            Me.lblNotes.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblNotes.Location = New System.Drawing.Point(24, 256)
            Me.lblNotes.Name = "lblNotes"
            Me.lblNotes.Size = New System.Drawing.Size(147, 15)
            Me.lblNotes.TabIndex = 6
            Me.lblNotes.Text = "Remarks / Damage Notes"
            '
            'pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Controls.Add(Me.btnConfirm)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 470)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(24, 12, 24, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(540, 60)
            Me.pnlFooter.TabIndex = 3
            '
            'btnCancel
            '
            Me.btnCancel.BackColor = System.Drawing.Color.White
            Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnCancel.Location = New System.Drawing.Point(254, 13)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(110, 34)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'btnConfirm
            '
            Me.btnConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.btnConfirm.FlatAppearance.BorderSize = 0
            Me.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnConfirm.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnConfirm.ForeColor = System.Drawing.Color.White
            Me.btnConfirm.Location = New System.Drawing.Point(372, 13)
            Me.btnConfirm.Name = "btnConfirm"
            Me.btnConfirm.Size = New System.Drawing.Size(142, 34)
            Me.btnConfirm.TabIndex = 0
            Me.btnConfirm.Text = "Confirm Check-In"
            Me.btnConfirm.UseVisualStyleBackColor = False
            '
            'frmCheckInOutModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(540, 530)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopGold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmCheckInOutModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Check-In / Check-Out Desk"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            Me.pnlResidentCard.ResumeLayout(False)
            Me.pnlResidentCard.PerformLayout()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents pnlResidentCard As System.Windows.Forms.Panel
        Friend WithEvents lblStudentInfo As System.Windows.Forms.Label
        Friend WithEvents lblBedInfo As System.Windows.Forms.Label
        Friend WithEvents lblAllocInfo As System.Windows.Forms.Label
        Friend WithEvents dtpActionDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblDate As System.Windows.Forms.Label
        Friend WithEvents chkKeys As System.Windows.Forms.CheckBox
        Friend WithEvents chkInventory As System.Windows.Forms.CheckBox
        Friend WithEvents chkMaintenance As System.Windows.Forms.CheckBox
        Friend WithEvents txtNotes As System.Windows.Forms.TextBox
        Friend WithEvents lblNotes As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnConfirm As System.Windows.Forms.Button
    End Class
End Namespace

