Namespace Forms.Rooms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmRoomModal
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
            Me.lblExistingBedsNotice = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.txtDescription = New System.Windows.Forms.TextBox()
            Me.lblDescription = New System.Windows.Forms.Label()
            Me.numCapacity = New System.Windows.Forms.NumericUpDown()
            Me.lblCapacity = New System.Windows.Forms.Label()
            Me.cboRoomType = New System.Windows.Forms.ComboBox()
            Me.lblRoomType = New System.Windows.Forms.Label()
            Me.numFloor = New System.Windows.Forms.NumericUpDown()
            Me.lblFloor = New System.Windows.Forms.Label()
            Me.txtRoomNumber = New System.Windows.Forms.TextBox()
            Me.lblRoomNumber = New System.Windows.Forms.Label()
            Me.cboHostel = New System.Windows.Forms.ComboBox()
            Me.lblHostel = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            CType(Me.numCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.numFloor, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlFooter.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopGold
            '
            Me.pnlTopGold.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.pnlTopGold.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopGold.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopGold.Name = "pnlTopGold"
            Me.pnlTopGold.Size = New System.Drawing.Size(520, 3)
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
            Me.pnlHeader.Size = New System.Drawing.Size(520, 68)
            Me.pnlHeader.TabIndex = 1
            '
            'lblModalTitle
            '
            Me.lblModalTitle.AutoSize = True
            Me.lblModalTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblModalTitle.ForeColor = System.Drawing.Color.White
            Me.lblModalTitle.Location = New System.Drawing.Point(22, 12)
            Me.lblModalTitle.Name = "lblModalTitle"
            Me.lblModalTitle.Size = New System.Drawing.Size(183, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Register Hostel Room"
            '
            'lblModalSub
            '
            Me.lblModalSub.AutoSize = True
            Me.lblModalSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblModalSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblModalSub.Location = New System.Drawing.Point(23, 36)
            Me.lblModalSub.Name = "lblModalSub"
            Me.lblModalSub.Size = New System.Drawing.Size(378, 15)
            Me.lblModalSub.TabIndex = 1
            Me.lblModalSub.Text = "Define room number, floor elevation, room type, and bed capacity limit."
            '
            'pnlBody
            '
            Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlBody.Controls.Add(Me.lblExistingBedsNotice)
            Me.pnlBody.Controls.Add(Me.cboStatus)
            Me.pnlBody.Controls.Add(Me.lblStatus)
            Me.pnlBody.Controls.Add(Me.txtDescription)
            Me.pnlBody.Controls.Add(Me.lblDescription)
            Me.pnlBody.Controls.Add(Me.numCapacity)
            Me.pnlBody.Controls.Add(Me.lblCapacity)
            Me.pnlBody.Controls.Add(Me.cboRoomType)
            Me.pnlBody.Controls.Add(Me.lblRoomType)
            Me.pnlBody.Controls.Add(Me.numFloor)
            Me.pnlBody.Controls.Add(Me.lblFloor)
            Me.pnlBody.Controls.Add(Me.txtRoomNumber)
            Me.pnlBody.Controls.Add(Me.lblRoomNumber)
            Me.pnlBody.Controls.Add(Me.cboHostel)
            Me.pnlBody.Controls.Add(Me.lblHostel)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 71)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlBody.Size = New System.Drawing.Size(520, 360)
            Me.pnlBody.TabIndex = 2
            '
            'lblExistingBedsNotice
            '
            Me.lblExistingBedsNotice.AutoSize = True
            Me.lblExistingBedsNotice.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblExistingBedsNotice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblExistingBedsNotice.Location = New System.Drawing.Point(26, 320)
            Me.lblExistingBedsNotice.Name = "lblExistingBedsNotice"
            Me.lblExistingBedsNotice.Size = New System.Drawing.Size(395, 13)
            Me.lblExistingBedsNotice.TabIndex = 14
            Me.lblExistingBedsNotice.Text = "Room capacity controls the maximum number of individual beds in this room."
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"AVAILABLE", "OCCUPIED", "MAINTENANCE", "INACTIVE"})
            Me.cboStatus.Location = New System.Drawing.Point(28, 290)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(464, 25)
            Me.cboStatus.TabIndex = 13
            '
            'lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStatus.Location = New System.Drawing.Point(26, 270)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(92, 15)
            Me.lblStatus.TabIndex = 12
            Me.lblStatus.Text = "Room Status *"
            '
            'txtDescription
            '
            Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtDescription.Location = New System.Drawing.Point(28, 202)
            Me.txtDescription.Multiline = True
            Me.txtDescription.Name = "txtDescription"
            Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtDescription.Size = New System.Drawing.Size(464, 56)
            Me.txtDescription.TabIndex = 11
            '
            'lblDescription
            '
            Me.lblDescription.AutoSize = True
            Me.lblDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblDescription.Location = New System.Drawing.Point(26, 182)
            Me.lblDescription.Name = "lblDescription"
            Me.lblDescription.Size = New System.Drawing.Size(127, 15)
            Me.lblDescription.TabIndex = 10
            Me.lblDescription.Text = "Description & Features"
            '
            'numCapacity
            '
            Me.numCapacity.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.numCapacity.Location = New System.Drawing.Point(272, 144)
            Me.numCapacity.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
            Me.numCapacity.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numCapacity.Name = "numCapacity"
            Me.numCapacity.Size = New System.Drawing.Size(220, 24)
            Me.numCapacity.TabIndex = 9
            Me.numCapacity.Value = New Decimal(New Integer() {2, 0, 0, 0})
            '
            'lblCapacity
            '
            Me.lblCapacity.AutoSize = True
            Me.lblCapacity.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblCapacity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblCapacity.Location = New System.Drawing.Point(270, 124)
            Me.lblCapacity.Name = "lblCapacity"
            Me.lblCapacity.Size = New System.Drawing.Size(130, 15)
            Me.lblCapacity.TabIndex = 8
            Me.lblCapacity.Text = "Bed Capacity (Limit) *"
            '
            'cboRoomType
            '
            Me.cboRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRoomType.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboRoomType.FormattingEnabled = True
            Me.cboRoomType.Items.AddRange(New Object() {"STANDARD", "DELUXE", "STUDIO"})
            Me.cboRoomType.Location = New System.Drawing.Point(28, 144)
            Me.cboRoomType.Name = "cboRoomType"
            Me.cboRoomType.Size = New System.Drawing.Size(226, 25)
            Me.cboRoomType.TabIndex = 7
            '
            'lblRoomType
            '
            Me.lblRoomType.AutoSize = True
            Me.lblRoomType.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRoomType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblRoomType.Location = New System.Drawing.Point(26, 124)
            Me.lblRoomType.Name = "lblRoomType"
            Me.lblRoomType.Size = New System.Drawing.Size(76, 15)
            Me.lblRoomType.TabIndex = 6
            Me.lblRoomType.Text = "Room Type *"
            '
            'numFloor
            '
            Me.numFloor.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.numFloor.Location = New System.Drawing.Point(272, 88)
            Me.numFloor.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
            Me.numFloor.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
            Me.numFloor.Name = "numFloor"
            Me.numFloor.Size = New System.Drawing.Size(220, 24)
            Me.numFloor.TabIndex = 5
            Me.numFloor.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'lblFloor
            '
            Me.lblFloor.AutoSize = True
            Me.lblFloor.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblFloor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblFloor.Location = New System.Drawing.Point(270, 68)
            Me.lblFloor.Name = "lblFloor"
            Me.lblFloor.Size = New System.Drawing.Size(91, 15)
            Me.lblFloor.TabIndex = 4
            Me.lblFloor.Text = "Floor Elevation *"
            '
            'txtRoomNumber
            '
            Me.txtRoomNumber.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.txtRoomNumber.Location = New System.Drawing.Point(28, 88)
            Me.txtRoomNumber.MaxLength = 20
            Me.txtRoomNumber.Name = "txtRoomNumber"
            Me.txtRoomNumber.Size = New System.Drawing.Size(226, 24)
            Me.txtRoomNumber.TabIndex = 3
            '
            'lblRoomNumber
            '
            Me.lblRoomNumber.AutoSize = True
            Me.lblRoomNumber.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRoomNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblRoomNumber.Location = New System.Drawing.Point(26, 68)
            Me.lblRoomNumber.Name = "lblRoomNumber"
            Me.lblRoomNumber.Size = New System.Drawing.Size(161, 15)
            Me.lblRoomNumber.TabIndex = 2
            Me.lblRoomNumber.Text = "Room Number (e.g. 101, A2) *"
            '
            'cboHostel
            '
            Me.cboHostel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboHostel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboHostel.FormattingEnabled = True
            Me.cboHostel.Location = New System.Drawing.Point(28, 34)
            Me.cboHostel.Name = "cboHostel"
            Me.cboHostel.Size = New System.Drawing.Size(464, 25)
            Me.cboHostel.TabIndex = 1
            '
            'lblHostel
            '
            Me.lblHostel.AutoSize = True
            Me.lblHostel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHostel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblHostel.Location = New System.Drawing.Point(26, 14)
            Me.lblHostel.Name = "lblHostel"
            Me.lblHostel.Size = New System.Drawing.Size(123, 15)
            Me.lblHostel.TabIndex = 0
            Me.lblHostel.Text = "Hostel Residential Block *"
            '
            'pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Controls.Add(Me.btnSave)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 431)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(24, 12, 24, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(520, 54)
            Me.pnlFooter.TabIndex = 3
            '
            'btnCancel
            '
            Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnCancel.BackColor = System.Drawing.Color.White
            Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnCancel.Location = New System.Drawing.Point(318, 11)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(84, 32)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'btnSave
            '
            Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnSave.FlatAppearance.BorderSize = 0
            Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSave.ForeColor = System.Drawing.Color.White
            Me.btnSave.Location = New System.Drawing.Point(408, 11)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(98, 32)
            Me.btnSave.TabIndex = 0
            Me.btnSave.Text = "Save Room"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            'frmRoomModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(520, 485)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopGold)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmRoomModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Hostel Room Configuration"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            CType(Me.numCapacity, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.numFloor, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents lblHostel As System.Windows.Forms.Label
        Friend WithEvents cboHostel As System.Windows.Forms.ComboBox
        Friend WithEvents lblRoomNumber As System.Windows.Forms.Label
        Friend WithEvents txtRoomNumber As System.Windows.Forms.TextBox
        Friend WithEvents lblFloor As System.Windows.Forms.Label
        Friend WithEvents numFloor As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblRoomType As System.Windows.Forms.Label
        Friend WithEvents cboRoomType As System.Windows.Forms.ComboBox
        Friend WithEvents lblCapacity As System.Windows.Forms.Label
        Friend WithEvents numCapacity As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblDescription As System.Windows.Forms.Label
        Friend WithEvents txtDescription As System.Windows.Forms.TextBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblExistingBedsNotice As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnSave As System.Windows.Forms.Button
    End Class
End Namespace

