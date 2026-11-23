Namespace Forms.Hostels
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmHostelModal
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
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblModalTitle = New System.Windows.Forms.Label()
            Me.lblModalSub = New System.Windows.Forms.Label()
            Me.pnlTopGold = New System.Windows.Forms.Panel()
            Me.pnlBody = New System.Windows.Forms.Panel()
            Me.lblStatusDesc = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.numFloors = New System.Windows.Forms.NumericUpDown()
            Me.lblFloors = New System.Windows.Forms.Label()
            Me.cboGender = New System.Windows.Forms.ComboBox()
            Me.lblGender = New System.Windows.Forms.Label()
            Me.txtDescription = New System.Windows.Forms.TextBox()
            Me.lblDescription = New System.Windows.Forms.Label()
            Me.txtLocation = New System.Windows.Forms.TextBox()
            Me.lblLocation = New System.Windows.Forms.Label()
            Me.txtHostelCode = New System.Windows.Forms.TextBox()
            Me.lblHostelCode = New System.Windows.Forms.Label()
            Me.txtHostelName = New System.Windows.Forms.TextBox()
            Me.lblHostelName = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            CType(Me.numFloors, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlFooter.SuspendLayout()
            Me.SuspendLayout()
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
            Me.pnlHeader.TabIndex = 0
            '
            'lblModalTitle
            '
            Me.lblModalTitle.AutoSize = True
            Me.lblModalTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblModalTitle.ForeColor = System.Drawing.Color.White
            Me.lblModalTitle.Location = New System.Drawing.Point(22, 12)
            Me.lblModalTitle.Name = "lblModalTitle"
            Me.lblModalTitle.Size = New System.Drawing.Size(188, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Register Hostel Facility"
            '
            'lblModalSub
            '
            Me.lblModalSub.AutoSize = True
            Me.lblModalSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblModalSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblModalSub.Location = New System.Drawing.Point(23, 36)
            Me.lblModalSub.Name = "lblModalSub"
            Me.lblModalSub.Size = New System.Drawing.Size(360, 15)
            Me.lblModalSub.TabIndex = 1
            Me.lblModalSub.Text = "Provide residential building specifications, capacity, and demographics."
            '
            'pnlTopGold
            '
            Me.pnlTopGold.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.pnlTopGold.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopGold.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopGold.Name = "pnlTopGold"
            Me.pnlTopGold.Size = New System.Drawing.Size(520, 3)
            Me.pnlTopGold.TabIndex = 1
            '
            'pnlBody
            '
            Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlBody.Controls.Add(Me.lblStatusDesc)
            Me.pnlBody.Controls.Add(Me.cboStatus)
            Me.pnlBody.Controls.Add(Me.lblStatus)
            Me.pnlBody.Controls.Add(Me.numFloors)
            Me.pnlBody.Controls.Add(Me.lblFloors)
            Me.pnlBody.Controls.Add(Me.cboGender)
            Me.pnlBody.Controls.Add(Me.lblGender)
            Me.pnlBody.Controls.Add(Me.txtDescription)
            Me.pnlBody.Controls.Add(Me.lblDescription)
            Me.pnlBody.Controls.Add(Me.txtLocation)
            Me.pnlBody.Controls.Add(Me.lblLocation)
            Me.pnlBody.Controls.Add(Me.txtHostelCode)
            Me.pnlBody.Controls.Add(Me.lblHostelCode)
            Me.pnlBody.Controls.Add(Me.txtHostelName)
            Me.pnlBody.Controls.Add(Me.lblHostelName)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 71)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlBody.Size = New System.Drawing.Size(520, 360)
            Me.pnlBody.TabIndex = 2
            '
            'lblStatusDesc
            '
            Me.lblStatusDesc.AutoSize = True
            Me.lblStatusDesc.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblStatusDesc.Location = New System.Drawing.Point(26, 318)
            Me.lblStatusDesc.Name = "lblStatusDesc"
            Me.lblStatusDesc.Size = New System.Drawing.Size(342, 13)
            Me.lblStatusDesc.TabIndex = 14
            Me.lblStatusDesc.Text = "Only hostels with ACTIVE status are eligible for room bed allocations."
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"ACTIVE", "INACTIVE", "MAINTENANCE"})
            Me.cboStatus.Location = New System.Drawing.Point(28, 288)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(464, 25)
            Me.cboStatus.TabIndex = 13
            '
            'lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStatus.Location = New System.Drawing.Point(26, 268)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(107, 15)
            Me.lblStatus.TabIndex = 12
            Me.lblStatus.Text = "Operational Status"
            '
            'numFloors
            '
            Me.numFloors.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.numFloors.Location = New System.Drawing.Point(272, 142)
            Me.numFloors.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
            Me.numFloors.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numFloors.Name = "numFloors"
            Me.numFloors.Size = New System.Drawing.Size(220, 24)
            Me.numFloors.TabIndex = 9
            Me.numFloors.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'lblFloors
            '
            Me.lblFloors.AutoSize = True
            Me.lblFloors.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblFloors.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblFloors.Location = New System.Drawing.Point(270, 122)
            Me.lblFloors.Name = "lblFloors"
            Me.lblFloors.Size = New System.Drawing.Size(68, 15)
            Me.lblFloors.TabIndex = 8
            Me.lblFloors.Text = "Total Floors"
            '
            'cboGender
            '
            Me.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboGender.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboGender.FormattingEnabled = True
            Me.cboGender.Items.AddRange(New Object() {"MALE", "FEMALE", "MIXED"})
            Me.cboGender.Location = New System.Drawing.Point(28, 142)
            Me.cboGender.Name = "cboGender"
            Me.cboGender.Size = New System.Drawing.Size(226, 25)
            Me.cboGender.TabIndex = 7
            '
            'lblGender
            '
            Me.lblGender.AutoSize = True
            Me.lblGender.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblGender.Location = New System.Drawing.Point(26, 122)
            Me.lblGender.Name = "lblGender"
            Me.lblGender.Size = New System.Drawing.Size(95, 15)
            Me.lblGender.TabIndex = 6
            Me.lblGender.Text = "Gender Category"
            '
            'txtDescription
            '
            Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtDescription.Location = New System.Drawing.Point(28, 198)
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
            Me.lblDescription.Location = New System.Drawing.Point(26, 178)
            Me.lblDescription.Name = "lblDescription"
            Me.lblDescription.Size = New System.Drawing.Size(129, 15)
            Me.lblDescription.TabIndex = 10
            Me.lblDescription.Text = "Description & Amenities"
            '
            'txtLocation
            '
            Me.txtLocation.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtLocation.Location = New System.Drawing.Point(28, 88)
            Me.txtLocation.Name = "txtLocation"
            Me.txtLocation.Size = New System.Drawing.Size(464, 24)
            Me.txtLocation.TabIndex = 5
            '
            'lblLocation
            '
            Me.lblLocation.AutoSize = True
            Me.lblLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblLocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblLocation.Location = New System.Drawing.Point(26, 68)
            Me.lblLocation.Name = "lblLocation"
            Me.lblLocation.Size = New System.Drawing.Size(149, 15)
            Me.lblLocation.TabIndex = 4
            Me.lblLocation.Text = "Campus Location / Zone *"
            '
            'txtHostelCode
            '
            Me.txtHostelCode.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.txtHostelCode.Location = New System.Drawing.Point(344, 34)
            Me.txtHostelCode.MaxLength = 20
            Me.txtHostelCode.Name = "txtHostelCode"
            Me.txtHostelCode.Size = New System.Drawing.Size(148, 24)
            Me.txtHostelCode.TabIndex = 3
            '
            'lblHostelCode
            '
            Me.lblHostelCode.AutoSize = True
            Me.lblHostelCode.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHostelCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblHostelCode.Location = New System.Drawing.Point(342, 14)
            Me.lblHostelCode.Name = "lblHostelCode"
            Me.lblHostelCode.Size = New System.Drawing.Size(81, 15)
            Me.lblHostelCode.TabIndex = 2
            Me.lblHostelCode.Text = "Hostel Code *"
            '
            'txtHostelName
            '
            Me.txtHostelName.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtHostelName.Location = New System.Drawing.Point(28, 34)
            Me.txtHostelName.MaxLength = 100
            Me.txtHostelName.Name = "txtHostelName"
            Me.txtHostelName.Size = New System.Drawing.Size(298, 24)
            Me.txtHostelName.TabIndex = 1
            '
            'lblHostelName
            '
            Me.lblHostelName.AutoSize = True
            Me.lblHostelName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHostelName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblHostelName.Location = New System.Drawing.Point(26, 14)
            Me.lblHostelName.Name = "lblHostelName"
            Me.lblHostelName.Size = New System.Drawing.Size(85, 15)
            Me.lblHostelName.TabIndex = 0
            Me.lblHostelName.Text = "Hostel Name *"
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
            Me.btnSave.Text = "Save Hostel"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            'frmHostelModal
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
            Me.Name = "frmHostelModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Hostel Facility Management"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            CType(Me.numFloors, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents lblHostelName As System.Windows.Forms.Label
        Friend WithEvents txtHostelName As System.Windows.Forms.TextBox
        Friend WithEvents txtHostelCode As System.Windows.Forms.TextBox
        Friend WithEvents lblHostelCode As System.Windows.Forms.Label
        Friend WithEvents txtLocation As System.Windows.Forms.TextBox
        Friend WithEvents lblLocation As System.Windows.Forms.Label
        Friend WithEvents cboGender As System.Windows.Forms.ComboBox
        Friend WithEvents lblGender As System.Windows.Forms.Label
        Friend WithEvents numFloors As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblFloors As System.Windows.Forms.Label
        Friend WithEvents txtDescription As System.Windows.Forms.TextBox
        Friend WithEvents lblDescription As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents lblStatusDesc As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnSave As System.Windows.Forms.Button
    End Class
End Namespace

