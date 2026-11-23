Namespace Forms.Beds
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmBedModal
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
            Me.pnlAmenities = New System.Windows.Forms.Panel()
            Me.chkVentilation = New System.Windows.Forms.CheckBox()
            Me.chkWindow = New System.Windows.Forms.CheckBox()
            Me.chkLocker = New System.Windows.Forms.CheckBox()
            Me.chkDesk = New System.Windows.Forms.CheckBox()
            Me.lblAmenities = New System.Windows.Forms.Label()
            Me.txtNotes = New System.Windows.Forms.TextBox()
            Me.lblNotes = New System.Windows.Forms.Label()
            Me.cboStatus = New System.Windows.Forms.ComboBox()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.cboPosition = New System.Windows.Forms.ComboBox()
            Me.lblPosition = New System.Windows.Forms.Label()
            Me.numSemesterRate = New System.Windows.Forms.NumericUpDown()
            Me.lblRate = New System.Windows.Forms.Label()
            Me.cboBedType = New System.Windows.Forms.ComboBox()
            Me.lblBedType = New System.Windows.Forms.Label()
            Me.txtBedLabel = New System.Windows.Forms.TextBox()
            Me.lblBedLabel = New System.Windows.Forms.Label()
            Me.txtBedNumber = New System.Windows.Forms.TextBox()
            Me.lblBedNumber = New System.Windows.Forms.Label()
            Me.cboRoom = New System.Windows.Forms.ComboBox()
            Me.lblRoom = New System.Windows.Forms.Label()
            Me.cboHostel = New System.Windows.Forms.ComboBox()
            Me.lblHostel = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            Me.pnlAmenities.SuspendLayout()
            CType(Me.numSemesterRate, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlFooter.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopGold
            '
            Me.pnlTopGold.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.pnlTopGold.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopGold.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopGold.Name = "pnlTopGold"
            Me.pnlTopGold.Size = New System.Drawing.Size(560, 3)
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
            Me.pnlHeader.Size = New System.Drawing.Size(560, 68)
            Me.pnlHeader.TabIndex = 1
            '
            'lblModalTitle
            '
            Me.lblModalTitle.AutoSize = True
            Me.lblModalTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblModalTitle.ForeColor = System.Drawing.Color.White
            Me.lblModalTitle.Location = New System.Drawing.Point(22, 12)
            Me.lblModalTitle.Name = "lblModalTitle"
            Me.lblModalTitle.Size = New System.Drawing.Size(185, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Register Individual Bed"
            '
            'lblModalSub
            '
            Me.lblModalSub.AutoSize = True
            Me.lblModalSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblModalSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblModalSub.Location = New System.Drawing.Point(23, 36)
            Me.lblModalSub.Name = "lblModalSub"
            Me.lblModalSub.Size = New System.Drawing.Size(434, 15)
            Me.lblModalSub.TabIndex = 1
            Me.lblModalSub.Text = "Beds are the fundamental allocable unit. Configure rates, position and amenities."
            '
            'pnlBody
            '
            Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlBody.Controls.Add(Me.pnlAmenities)
            Me.pnlBody.Controls.Add(Me.lblAmenities)
            Me.pnlBody.Controls.Add(Me.txtNotes)
            Me.pnlBody.Controls.Add(Me.lblNotes)
            Me.pnlBody.Controls.Add(Me.cboStatus)
            Me.pnlBody.Controls.Add(Me.lblStatus)
            Me.pnlBody.Controls.Add(Me.cboPosition)
            Me.pnlBody.Controls.Add(Me.lblPosition)
            Me.pnlBody.Controls.Add(Me.numSemesterRate)
            Me.pnlBody.Controls.Add(Me.lblRate)
            Me.pnlBody.Controls.Add(Me.cboBedType)
            Me.pnlBody.Controls.Add(Me.lblBedType)
            Me.pnlBody.Controls.Add(Me.txtBedLabel)
            Me.pnlBody.Controls.Add(Me.lblBedLabel)
            Me.pnlBody.Controls.Add(Me.txtBedNumber)
            Me.pnlBody.Controls.Add(Me.lblBedNumber)
            Me.pnlBody.Controls.Add(Me.cboRoom)
            Me.pnlBody.Controls.Add(Me.lblRoom)
            Me.pnlBody.Controls.Add(Me.cboHostel)
            Me.pnlBody.Controls.Add(Me.lblHostel)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 71)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlBody.Size = New System.Drawing.Size(560, 480)
            Me.pnlBody.TabIndex = 2
            '
            'pnlAmenities
            '
            Me.pnlAmenities.BackColor = System.Drawing.Color.White
            Me.pnlAmenities.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlAmenities.Controls.Add(Me.chkVentilation)
            Me.pnlAmenities.Controls.Add(Me.chkWindow)
            Me.pnlAmenities.Controls.Add(Me.chkLocker)
            Me.pnlAmenities.Controls.Add(Me.chkDesk)
            Me.pnlAmenities.Location = New System.Drawing.Point(28, 280)
            Me.pnlAmenities.Name = "pnlAmenities"
            Me.pnlAmenities.Padding = New System.Windows.Forms.Padding(8)
            Me.pnlAmenities.Size = New System.Drawing.Size(504, 38)
            Me.pnlAmenities.TabIndex = 15
            '
            'chkVentilation
            '
            Me.chkVentilation.AutoSize = True
            Me.chkVentilation.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.chkVentilation.Location = New System.Drawing.Point(375, 9)
            Me.chkVentilation.Name = "chkVentilation"
            Me.chkVentilation.Size = New System.Drawing.Size(82, 19)
            Me.chkVentilation.TabIndex = 3
            Me.chkVentilation.Text = "Ventilation"
            Me.chkVentilation.UseVisualStyleBackColor = True
            '
            'chkWindow
            '
            Me.chkWindow.AutoSize = True
            Me.chkWindow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.chkWindow.Location = New System.Drawing.Point(245, 9)
            Me.chkWindow.Name = "chkWindow"
            Me.chkWindow.Size = New System.Drawing.Size(107, 19)
            Me.chkWindow.TabIndex = 2
            Me.chkWindow.Text = "Window Access"
            Me.chkWindow.UseVisualStyleBackColor = True
            '
            'chkLocker
            '
            Me.chkLocker.AutoSize = True
            Me.chkLocker.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.chkLocker.Location = New System.Drawing.Point(125, 9)
            Me.chkLocker.Name = "chkLocker"
            Me.chkLocker.Size = New System.Drawing.Size(100, 19)
            Me.chkLocker.TabIndex = 1
            Me.chkLocker.Text = "Lockable Box"
            Me.chkLocker.UseVisualStyleBackColor = True
            '
            'chkDesk
            '
            Me.chkDesk.AutoSize = True
            Me.chkDesk.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.chkDesk.Location = New System.Drawing.Point(12, 9)
            Me.chkDesk.Name = "chkDesk"
            Me.chkDesk.Size = New System.Drawing.Size(87, 19)
            Me.chkDesk.TabIndex = 0
            Me.chkDesk.Text = "Study Desk"
            Me.chkDesk.UseVisualStyleBackColor = True
            '
            'lblAmenities
            '
            Me.lblAmenities.AutoSize = True
            Me.lblAmenities.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblAmenities.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblAmenities.Location = New System.Drawing.Point(26, 260)
            Me.lblAmenities.Name = "lblAmenities"
            Me.lblAmenities.Size = New System.Drawing.Size(142, 15)
            Me.lblAmenities.TabIndex = 14
            Me.lblAmenities.Text = "Bed Amenities & Features"
            '
            'txtNotes
            '
            Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtNotes.Location = New System.Drawing.Point(28, 412)
            Me.txtNotes.Multiline = True
            Me.txtNotes.Name = "txtNotes"
            Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtNotes.Size = New System.Drawing.Size(504, 48)
            Me.txtNotes.TabIndex = 19
            '
            'lblNotes
            '
            Me.lblNotes.AutoSize = True
            Me.lblNotes.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblNotes.Location = New System.Drawing.Point(26, 392)
            Me.lblNotes.Name = "lblNotes"
            Me.lblNotes.Size = New System.Drawing.Size(130, 15)
            Me.lblNotes.TabIndex = 18
            Me.lblNotes.Text = "Internal Notes / Remarks"
            '
            'cboStatus
            '
            Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboStatus.FormattingEnabled = True
            Me.cboStatus.Items.AddRange(New Object() {"AVAILABLE", "OCCUPIED", "MAINTENANCE", "RESERVED", "INACTIVE"})
            Me.cboStatus.Location = New System.Drawing.Point(285, 350)
            Me.cboStatus.Name = "cboStatus"
            Me.cboStatus.Size = New System.Drawing.Size(247, 25)
            Me.cboStatus.TabIndex = 17
            '
            'lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStatus.Location = New System.Drawing.Point(283, 330)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(78, 15)
            Me.lblStatus.TabIndex = 16
            Me.lblStatus.Text = "Bed Status *"
            '
            'cboPosition
            '
            Me.cboPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboPosition.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboPosition.FormattingEnabled = True
            Me.cboPosition.Items.AddRange(New Object() {"NEAR_WINDOW", "NEAR_DOOR", "CORNER", "CENTER"})
            Me.cboPosition.Location = New System.Drawing.Point(28, 350)
            Me.cboPosition.Name = "cboPosition"
            Me.cboPosition.Size = New System.Drawing.Size(242, 25)
            Me.cboPosition.TabIndex = 15
            '
            'lblPosition
            '
            Me.lblPosition.AutoSize = True
            Me.lblPosition.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPosition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblPosition.Location = New System.Drawing.Point(26, 330)
            Me.lblPosition.Name = "lblPosition"
            Me.lblPosition.Size = New System.Drawing.Size(126, 15)
            Me.lblPosition.TabIndex = 14
            Me.lblPosition.Text = "Bed Position in Room *"
            '
            'numSemesterRate
            '
            Me.numSemesterRate.DecimalPlaces = 2
            Me.numSemesterRate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.numSemesterRate.Increment = New Decimal(New Integer() {500, 0, 0, 0})
            Me.numSemesterRate.Location = New System.Drawing.Point(285, 218)
            Me.numSemesterRate.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
            Me.numSemesterRate.Name = "numSemesterRate"
            Me.numSemesterRate.Size = New System.Drawing.Size(247, 24)
            Me.numSemesterRate.TabIndex = 13
            Me.numSemesterRate.ThousandsSeparator = True
            Me.numSemesterRate.Value = New Decimal(New Integer() {18000, 0, 0, 0})
            '
            'lblRate
            '
            Me.lblRate.AutoSize = True
            Me.lblRate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblRate.Location = New System.Drawing.Point(283, 198)
            Me.lblRate.Name = "lblRate"
            Me.lblRate.Size = New System.Drawing.Size(147, 15)
            Me.lblRate.TabIndex = 12
            Me.lblRate.Text = "Semester Rate (KES / $) *"
            '
            'cboBedType
            '
            Me.cboBedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboBedType.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboBedType.FormattingEnabled = True
            Me.cboBedType.Items.AddRange(New Object() {"STANDARD", "BUNK_TOP", "BUNK_BOTTOM", "DELUXE"})
            Me.cboBedType.Location = New System.Drawing.Point(28, 218)
            Me.cboBedType.Name = "cboBedType"
            Me.cboBedType.Size = New System.Drawing.Size(242, 25)
            Me.cboBedType.TabIndex = 11
            '
            'lblBedType
            '
            Me.lblBedType.AutoSize = True
            Me.lblBedType.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBedType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblBedType.Location = New System.Drawing.Point(26, 198)
            Me.lblBedType.Name = "lblBedType"
            Me.lblBedType.Size = New System.Drawing.Size(65, 15)
            Me.lblBedType.TabIndex = 10
            Me.lblBedType.Text = "Bed Type *"
            '
            'txtBedLabel
            '
            Me.txtBedLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtBedLabel.Location = New System.Drawing.Point(285, 155)
            Me.txtBedLabel.MaxLength = 50
            Me.txtBedLabel.Name = "txtBedLabel"
            Me.txtBedLabel.PlaceholderText = "e.g. Bed A - Window Side"
            Me.txtBedLabel.Size = New System.Drawing.Size(247, 24)
            Me.txtBedLabel.TabIndex = 9
            '
            'lblBedLabel
            '
            Me.lblBedLabel.AutoSize = True
            Me.lblBedLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBedLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblBedLabel.Location = New System.Drawing.Point(283, 135)
            Me.lblBedLabel.Name = "lblBedLabel"
            Me.lblBedLabel.Size = New System.Drawing.Size(126, 15)
            Me.lblBedLabel.TabIndex = 8
            Me.lblBedLabel.Text = "Bed Descriptive Label *"
            '
            'txtBedNumber
            '
            Me.txtBedNumber.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.txtBedNumber.Location = New System.Drawing.Point(28, 155)
            Me.txtBedNumber.MaxLength = 20
            Me.txtBedNumber.Name = "txtBedNumber"
            Me.txtBedNumber.PlaceholderText = "e.g. A, B, 1, 2"
            Me.txtBedNumber.Size = New System.Drawing.Size(242, 24)
            Me.txtBedNumber.TabIndex = 7
            '
            'lblBedNumber
            '
            Me.lblBedNumber.AutoSize = True
            Me.lblBedNumber.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBedNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblBedNumber.Location = New System.Drawing.Point(26, 135)
            Me.lblBedNumber.Name = "lblBedNumber"
            Me.lblBedNumber.Size = New System.Drawing.Size(127, 15)
            Me.lblBedNumber.TabIndex = 6
            Me.lblBedNumber.Text = "Bed Identifier / Code *"
            '
            'cboRoom
            '
            Me.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRoom.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboRoom.FormattingEnabled = True
            Me.cboRoom.Location = New System.Drawing.Point(285, 92)
            Me.cboRoom.Name = "cboRoom"
            Me.cboRoom.Size = New System.Drawing.Size(247, 25)
            Me.cboRoom.TabIndex = 5
            '
            'lblRoom
            '
            Me.lblRoom.AutoSize = True
            Me.lblRoom.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRoom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblRoom.Location = New System.Drawing.Point(283, 72)
            Me.lblRoom.Name = "lblRoom"
            Me.lblRoom.Size = New System.Drawing.Size(89, 15)
            Me.lblRoom.TabIndex = 4
            Me.lblRoom.Text = "Target Room *"
            '
            'cboHostel
            '
            Me.cboHostel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboHostel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboHostel.FormattingEnabled = True
            Me.cboHostel.Location = New System.Drawing.Point(28, 92)
            Me.cboHostel.Name = "cboHostel"
            Me.cboHostel.Size = New System.Drawing.Size(242, 25)
            Me.cboHostel.TabIndex = 3
            '
            'lblHostel
            '
            Me.lblHostel.AutoSize = True
            Me.lblHostel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHostel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblHostel.Location = New System.Drawing.Point(26, 72)
            Me.lblHostel.Name = "lblHostel"
            Me.lblHostel.Size = New System.Drawing.Size(92, 15)
            Me.lblHostel.TabIndex = 2
            Me.lblHostel.Text = "Hostel Facility *"
            '
            'pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Controls.Add(Me.btnSave)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 551)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(24, 12, 24, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(560, 60)
            Me.pnlFooter.TabIndex = 3
            '
            'btnCancel
            '
            Me.btnCancel.BackColor = System.Drawing.Color.White
            Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnCancel.Location = New System.Drawing.Point(294, 13)
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
            Me.btnSave.Location = New System.Drawing.Point(412, 13)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(120, 34)
            Me.btnSave.TabIndex = 0
            Me.btnSave.Text = "Save Bed"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            'frmBedModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(560, 611)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopGold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmBedModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Bed Registration"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            Me.pnlAmenities.ResumeLayout(False)
            Me.pnlAmenities.PerformLayout()
            CType(Me.numSemesterRate, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents pnlAmenities As System.Windows.Forms.Panel
        Friend WithEvents chkVentilation As System.Windows.Forms.CheckBox
        Friend WithEvents chkWindow As System.Windows.Forms.CheckBox
        Friend WithEvents chkLocker As System.Windows.Forms.CheckBox
        Friend WithEvents chkDesk As System.Windows.Forms.CheckBox
        Friend WithEvents lblAmenities As System.Windows.Forms.Label
        Friend WithEvents txtNotes As System.Windows.Forms.TextBox
        Friend WithEvents lblNotes As System.Windows.Forms.Label
        Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents cboPosition As System.Windows.Forms.ComboBox
        Friend WithEvents lblPosition As System.Windows.Forms.Label
        Friend WithEvents numSemesterRate As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblRate As System.Windows.Forms.Label
        Friend WithEvents cboBedType As System.Windows.Forms.ComboBox
        Friend WithEvents lblBedType As System.Windows.Forms.Label
        Friend WithEvents txtBedLabel As System.Windows.Forms.TextBox
        Friend WithEvents lblBedLabel As System.Windows.Forms.Label
        Friend WithEvents txtBedNumber As System.Windows.Forms.TextBox
        Friend WithEvents lblBedNumber As System.Windows.Forms.Label
        Friend WithEvents cboRoom As System.Windows.Forms.ComboBox
        Friend WithEvents lblRoom As System.Windows.Forms.Label
        Friend WithEvents cboHostel As System.Windows.Forms.ComboBox
        Friend WithEvents lblHostel As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnSave As System.Windows.Forms.Button
    End Class
End Namespace

