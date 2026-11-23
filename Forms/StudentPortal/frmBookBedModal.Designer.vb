Namespace Forms.StudentPortal
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmBookBedModal
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
            Me.btnConfirmBook = New University_hostel_mgt.Controls.GlassButton()
            Me.lblStatusMessage = New System.Windows.Forms.Label()
            Me.cboBeds = New System.Windows.Forms.ComboBox()
            Me.lblSelectBed = New System.Windows.Forms.Label()
            Me.pnlSummary = New University_hostel_mgt.Controls.GlassPanel()
            Me.lblRate = New System.Windows.Forms.Label()
            Me.lblRateTitle = New System.Windows.Forms.Label()
            Me.lblType = New System.Windows.Forms.Label()
            Me.lblTypeTitle = New System.Windows.Forms.Label()
            Me.lblFloor = New System.Windows.Forms.Label()
            Me.lblFloorTitle = New System.Windows.Forms.Label()
            Me.lblRoom = New System.Windows.Forms.Label()
            Me.lblRoomTitle = New System.Windows.Forms.Label()
            Me.lblHostel = New System.Windows.Forms.Label()
            Me.lblHostelTitle = New System.Windows.Forms.Label()
            Me.lblNotice = New System.Windows.Forms.Label()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlRoot.SuspendLayout()
            Me.pnlSummary.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(520, 3)
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
            Me.pnlHeader.Size = New System.Drawing.Size(520, 38)
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
            Me.btnClose.Location = New System.Drawing.Point(480, 4)
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
            Me.lblHeaderTitle.Size = New System.Drawing.Size(262, 15)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "Bed Allocation Booking • Dedan Kimathi University"
            '
            'pnlRoot
            '
            Me.pnlRoot.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlRoot.BorderWidth = 1.0!
            Me.pnlRoot.Controls.Add(Me.btnCancel)
            Me.pnlRoot.Controls.Add(Me.btnConfirmBook)
            Me.pnlRoot.Controls.Add(Me.lblStatusMessage)
            Me.pnlRoot.Controls.Add(Me.cboBeds)
            Me.pnlRoot.Controls.Add(Me.lblSelectBed)
            Me.pnlRoot.Controls.Add(Me.pnlSummary)
            Me.pnlRoot.Controls.Add(Me.lblNotice)
            Me.pnlRoot.Controls.Add(Me.lblSubtitle)
            Me.pnlRoot.Controls.Add(Me.lblTitle)
            Me.pnlRoot.CornerRadius = 18
            Me.pnlRoot.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlRoot.FillColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(31, Byte), Integer))
            Me.pnlRoot.GlowColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlRoot.Location = New System.Drawing.Point(0, 41)
            Me.pnlRoot.Name = "pnlRoot"
            Me.pnlRoot.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlRoot.Size = New System.Drawing.Size(520, 459)
            Me.pnlRoot.TabIndex = 2
            '
            'btnCancel
            '
            Me.btnCancel.CornerRadius = 12
            Me.btnCancel.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.HoverColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnCancel.Location = New System.Drawing.Point(24, 396)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnCancel.Size = New System.Drawing.Size(472, 36)
            Me.btnCancel.TabIndex = 8
            Me.btnCancel.Text = "Cancel"
            '
            'btnConfirmBook
            '
            Me.btnConfirmBook.CornerRadius = 12
            Me.btnConfirmBook.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnConfirmBook.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnConfirmBook.HoverColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
            Me.btnConfirmBook.Location = New System.Drawing.Point(24, 348)
            Me.btnConfirmBook.Name = "btnConfirmBook"
            Me.btnConfirmBook.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnConfirmBook.Size = New System.Drawing.Size(472, 42)
            Me.btnConfirmBook.TabIndex = 7
            Me.btnConfirmBook.Text = "Confirm Booking & Reserve Bed"
            '
            'lblStatusMessage
            '
            Me.lblStatusMessage.BackColor = System.Drawing.Color.Transparent
            Me.lblStatusMessage.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.lblStatusMessage.Location = New System.Drawing.Point(24, 316)
            Me.lblStatusMessage.Name = "lblStatusMessage"
            Me.lblStatusMessage.Size = New System.Drawing.Size(472, 24)
            Me.lblStatusMessage.TabIndex = 6
            Me.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'cboBeds
            '
            Me.cboBeds.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(35, Byte), Integer))
            Me.cboBeds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboBeds.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cboBeds.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboBeds.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.cboBeds.FormattingEnabled = True
            Me.cboBeds.Location = New System.Drawing.Point(24, 252)
            Me.cboBeds.Name = "cboBeds"
            Me.cboBeds.Size = New System.Drawing.Size(472, 25)
            Me.cboBeds.TabIndex = 5
            '
            'lblSelectBed
            '
            Me.lblSelectBed.AutoSize = True
            Me.lblSelectBed.BackColor = System.Drawing.Color.Transparent
            Me.lblSelectBed.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblSelectBed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblSelectBed.Location = New System.Drawing.Point(24, 232)
            Me.lblSelectBed.Name = "lblSelectBed"
            Me.lblSelectBed.Size = New System.Drawing.Size(124, 15)
            Me.lblSelectBed.TabIndex = 4
            Me.lblSelectBed.Text = "Choose Available Bed *"
            '
            'pnlSummary
            '
            Me.pnlSummary.BorderColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlSummary.BorderWidth = 1.0!
            Me.pnlSummary.Controls.Add(Me.lblRate)
            Me.pnlSummary.Controls.Add(Me.lblRateTitle)
            Me.pnlSummary.Controls.Add(Me.lblType)
            Me.pnlSummary.Controls.Add(Me.lblTypeTitle)
            Me.pnlSummary.Controls.Add(Me.lblFloor)
            Me.pnlSummary.Controls.Add(Me.lblFloorTitle)
            Me.pnlSummary.Controls.Add(Me.lblRoom)
            Me.pnlSummary.Controls.Add(Me.lblRoomTitle)
            Me.pnlSummary.Controls.Add(Me.lblHostel)
            Me.pnlSummary.Controls.Add(Me.lblHostelTitle)
            Me.pnlSummary.CornerRadius = 12
            Me.pnlSummary.FillColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
            Me.pnlSummary.GlowColor = System.Drawing.Color.Transparent
            Me.pnlSummary.Location = New System.Drawing.Point(24, 78)
            Me.pnlSummary.Name = "pnlSummary"
            Me.pnlSummary.Padding = New System.Windows.Forms.Padding(16)
            Me.pnlSummary.Size = New System.Drawing.Size(472, 140)
            Me.pnlSummary.TabIndex = 3
            '
            'lblRate
            '
            Me.lblRate.AutoSize = True
            Me.lblRate.BackColor = System.Drawing.Color.Transparent
            Me.lblRate.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblRate.Location = New System.Drawing.Point(120, 102)
            Me.lblRate.Name = "lblRate"
            Me.lblRate.Size = New System.Drawing.Size(107, 20)
            Me.lblRate.TabIndex = 9
            Me.lblRate.Text = "KES 14,000.00"
            '
            'lblRateTitle
            '
            Me.lblRateTitle.AutoSize = True
            Me.lblRateTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblRateTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblRateTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblRateTitle.Location = New System.Drawing.Point(16, 106)
            Me.lblRateTitle.Name = "lblRateTitle"
            Me.lblRateTitle.Size = New System.Drawing.Size(83, 15)
            Me.lblRateTitle.TabIndex = 8
            Me.lblRateTitle.Text = "Semester Rate:"
            '
            'lblType
            '
            Me.lblType.AutoSize = True
            Me.lblType.BackColor = System.Drawing.Color.Transparent
            Me.lblType.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblType.Location = New System.Drawing.Point(120, 80)
            Me.lblType.Name = "lblType"
            Me.lblType.Size = New System.Drawing.Size(106, 15)
            Me.lblType.TabIndex = 7
            Me.lblType.Text = "Double Occupancy"
            '
            'lblTypeTitle
            '
            Me.lblTypeTitle.AutoSize = True
            Me.lblTypeTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblTypeTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblTypeTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblTypeTitle.Location = New System.Drawing.Point(16, 80)
            Me.lblTypeTitle.Name = "lblTypeTitle"
            Me.lblTypeTitle.Size = New System.Drawing.Size(70, 15)
            Me.lblTypeTitle.TabIndex = 6
            Me.lblTypeTitle.Text = "Room Type:"
            '
            'lblFloor
            '
            Me.lblFloor.AutoSize = True
            Me.lblFloor.BackColor = System.Drawing.Color.Transparent
            Me.lblFloor.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblFloor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblFloor.Location = New System.Drawing.Point(120, 56)
            Me.lblFloor.Name = "lblFloor"
            Me.lblFloor.Size = New System.Drawing.Size(65, 15)
            Me.lblFloor.TabIndex = 5
            Me.lblFloor.Text = "Floor 2 East"
            '
            'lblFloorTitle
            '
            Me.lblFloorTitle.AutoSize = True
            Me.lblFloorTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblFloorTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblFloorTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblFloorTitle.Location = New System.Drawing.Point(16, 56)
            Me.lblFloorTitle.Name = "lblFloorTitle"
            Me.lblFloorTitle.Size = New System.Drawing.Size(37, 15)
            Me.lblFloorTitle.TabIndex = 4
            Me.lblFloorTitle.Text = "Floor:"
            '
            'lblRoom
            '
            Me.lblRoom.AutoSize = True
            Me.lblRoom.BackColor = System.Drawing.Color.Transparent
            Me.lblRoom.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRoom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblRoom.Location = New System.Drawing.Point(120, 34)
            Me.lblRoom.Name = "lblRoom"
            Me.lblRoom.Size = New System.Drawing.Size(61, 15)
            Me.lblRoom.TabIndex = 3
            Me.lblRoom.Text = "Room 204"
            '
            'lblRoomTitle
            '
            Me.lblRoomTitle.AutoSize = True
            Me.lblRoomTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblRoomTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblRoomTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblRoomTitle.Location = New System.Drawing.Point(16, 34)
            Me.lblRoomTitle.Name = "lblRoomTitle"
            Me.lblRoomTitle.Size = New System.Drawing.Size(89, 15)
            Me.lblRoomTitle.TabIndex = 2
            Me.lblRoomTitle.Text = "Room Number:"
            '
            'lblHostel
            '
            Me.lblHostel.AutoSize = True
            Me.lblHostel.BackColor = System.Drawing.Color.Transparent
            Me.lblHostel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHostel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblHostel.Location = New System.Drawing.Point(120, 12)
            Me.lblHostel.Name = "lblHostel"
            Me.lblHostel.Size = New System.Drawing.Size(129, 15)
            Me.lblHostel.TabIndex = 1
            Me.lblHostel.Text = "Kimathi Premier Hostel"
            '
            'lblHostelTitle
            '
            Me.lblHostelTitle.AutoSize = True
            Me.lblHostelTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblHostelTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblHostelTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblHostelTitle.Location = New System.Drawing.Point(16, 12)
            Me.lblHostelTitle.Name = "lblHostelTitle"
            Me.lblHostelTitle.Size = New System.Drawing.Size(44, 15)
            Me.lblHostelTitle.TabIndex = 0
            Me.lblHostelTitle.Text = "Hostel:"
            '
            'lblNotice
            '
            Me.lblNotice.BackColor = System.Drawing.Color.Transparent
            Me.lblNotice.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblNotice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblNotice.Location = New System.Drawing.Point(24, 282)
            Me.lblNotice.Name = "lblNotice"
            Me.lblNotice.Size = New System.Drawing.Size(472, 32)
            Me.lblNotice.TabIndex = 2
            Me.lblNotice.Text = "By booking this bed space, you agree to the DeKUT residential terms and commit to" &
    " settling the semester fee upon accommodation."
            Me.lblNotice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'lblSubtitle
            '
            Me.lblSubtitle.AutoSize = True
            Me.lblSubtitle.BackColor = System.Drawing.Color.Transparent
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(24, 46)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(262, 15)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Select your preferred bed space and confirm below."
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblTitle.Location = New System.Drawing.Point(24, 20)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(251, 25)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "Book Bed Accommodation"
            '
            'frmBookBedModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(520, 500)
            Me.Controls.Add(Me.pnlRoot)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmBookBedModal"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Book Bed Space"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlRoot.ResumeLayout(False)
            Me.pnlRoot.PerformLayout()
            Me.pnlSummary.ResumeLayout(False)
            Me.pnlSummary.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlRoot As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents pnlSummary As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblHostelTitle As System.Windows.Forms.Label
        Friend WithEvents lblHostel As System.Windows.Forms.Label
        Friend WithEvents lblRoomTitle As System.Windows.Forms.Label
        Friend WithEvents lblRoom As System.Windows.Forms.Label
        Friend WithEvents lblFloorTitle As System.Windows.Forms.Label
        Friend WithEvents lblFloor As System.Windows.Forms.Label
        Friend WithEvents lblTypeTitle As System.Windows.Forms.Label
        Friend WithEvents lblType As System.Windows.Forms.Label
        Friend WithEvents lblRateTitle As System.Windows.Forms.Label
        Friend WithEvents lblRate As System.Windows.Forms.Label
        Friend WithEvents lblSelectBed As System.Windows.Forms.Label
        Friend WithEvents cboBeds As System.Windows.Forms.ComboBox
        Friend WithEvents lblNotice As System.Windows.Forms.Label
        Friend WithEvents lblStatusMessage As System.Windows.Forms.Label
        Friend WithEvents btnConfirmBook As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnCancel As University_hostel_mgt.Controls.GlassButton
    End Class
End Namespace

