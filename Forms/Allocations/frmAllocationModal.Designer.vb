Namespace Forms.Allocations
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmAllocationModal
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
            Me.pnlRateBanner = New System.Windows.Forms.Panel()
            Me.lblRateSub = New System.Windows.Forms.Label()
            Me.lblRateAmount = New System.Windows.Forms.Label()
            Me.lblRateTitle = New System.Windows.Forms.Label()
            Me.txtNotes = New System.Windows.Forms.TextBox()
            Me.lblNotes = New System.Windows.Forms.Label()
            Me.cboBed = New System.Windows.Forms.ComboBox()
            Me.lblBed = New System.Windows.Forms.Label()
            Me.cboRoom = New System.Windows.Forms.ComboBox()
            Me.lblRoom = New System.Windows.Forms.Label()
            Me.cboHostel = New System.Windows.Forms.ComboBox()
            Me.lblHostel = New System.Windows.Forms.Label()
            Me.cboStudent = New System.Windows.Forms.ComboBox()
            Me.lblStudent = New System.Windows.Forms.Label()
            Me.cboSemester = New System.Windows.Forms.ComboBox()
            Me.lblSemester = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnAllocate = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            Me.pnlRateBanner.SuspendLayout()
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
            Me.lblModalTitle.Size = New System.Drawing.Size(211, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Allocate Individual Bed Space"
            '
            'lblModalSub
            '
            Me.lblModalSub.AutoSize = True
            Me.lblModalSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblModalSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblModalSub.Location = New System.Drawing.Point(23, 36)
            Me.lblModalSub.Name = "lblModalSub"
            Me.lblModalSub.Size = New System.Drawing.Size(437, 15)
            Me.lblModalSub.TabIndex = 1
            Me.lblModalSub.Text = "Allocate a verified available bed to a student resident for the active academic semester."
            '
            'pnlBody
            '
            Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlBody.Controls.Add(Me.pnlRateBanner)
            Me.pnlBody.Controls.Add(Me.txtNotes)
            Me.pnlBody.Controls.Add(Me.lblNotes)
            Me.pnlBody.Controls.Add(Me.cboBed)
            Me.pnlBody.Controls.Add(Me.lblBed)
            Me.pnlBody.Controls.Add(Me.cboRoom)
            Me.pnlBody.Controls.Add(Me.lblRoom)
            Me.pnlBody.Controls.Add(Me.cboHostel)
            Me.pnlBody.Controls.Add(Me.lblHostel)
            Me.pnlBody.Controls.Add(Me.cboStudent)
            Me.pnlBody.Controls.Add(Me.lblStudent)
            Me.pnlBody.Controls.Add(Me.cboSemester)
            Me.pnlBody.Controls.Add(Me.lblSemester)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 71)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlBody.Size = New System.Drawing.Size(560, 480)
            Me.pnlBody.TabIndex = 2
            '
            'pnlRateBanner
            '
            Me.pnlRateBanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(199, Byte), Integer))
            Me.pnlRateBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlRateBanner.Controls.Add(Me.lblRateSub)
            Me.pnlRateBanner.Controls.Add(Me.lblRateAmount)
            Me.pnlRateBanner.Controls.Add(Me.lblRateTitle)
            Me.pnlRateBanner.Location = New System.Drawing.Point(26, 290)
            Me.pnlRateBanner.Name = "pnlRateBanner"
            Me.pnlRateBanner.Padding = New System.Windows.Forms.Padding(16, 10, 16, 10)
            Me.pnlRateBanner.Size = New System.Drawing.Size(508, 64)
            Me.pnlRateBanner.TabIndex = 10
            '
            'lblRateSub
            '
            Me.lblRateSub.AutoSize = True
            Me.lblRateSub.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblRateSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.lblRateSub.Location = New System.Drawing.Point(14, 38)
            Me.lblRateSub.Name = "lblRateSub"
            Me.lblRateSub.Size = New System.Drawing.Size(374, 13)
            Me.lblRateSub.TabIndex = 2
            Me.lblRateSub.Text = "This rate will be frozen into the allocation record and cannot be changed later."
            '
            'lblRateAmount
            '
            Me.lblRateAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblRateAmount.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRateAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(9, Byte), Integer))
            Me.lblRateAmount.Location = New System.Drawing.Point(280, 12)
            Me.lblRateAmount.Name = "lblRateAmount"
            Me.lblRateAmount.Size = New System.Drawing.Size(210, 24)
            Me.lblRateAmount.TabIndex = 1
            Me.lblRateAmount.Text = "KES 0.00"
            Me.lblRateAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblRateTitle
            '
            Me.lblRateTitle.AutoSize = True
            Me.lblRateTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRateTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.lblRateTitle.Location = New System.Drawing.Point(14, 14)
            Me.lblRateTitle.Name = "lblRateTitle"
            Me.lblRateTitle.Size = New System.Drawing.Size(189, 15)
            Me.lblRateTitle.TabIndex = 0
            Me.lblRateTitle.Text = "Semester Accommodation Rate:"
            '
            'txtNotes
            '
            Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtNotes.Location = New System.Drawing.Point(26, 386)
            Me.txtNotes.Multiline = True
            Me.txtNotes.Name = "txtNotes"
            Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtNotes.Size = New System.Drawing.Size(508, 60)
            Me.txtNotes.TabIndex = 12
            '
            'lblNotes
            '
            Me.lblNotes.AutoSize = True
            Me.lblNotes.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblNotes.Location = New System.Drawing.Point(24, 366)
            Me.lblNotes.Name = "lblNotes"
            Me.lblNotes.Size = New System.Drawing.Size(147, 15)
            Me.lblNotes.TabIndex = 11
            Me.lblNotes.Text = "Allocation Remarks / Notes"
            '
            'cboBed
            '
            Me.cboBed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboBed.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboBed.FormattingEnabled = True
            Me.cboBed.Location = New System.Drawing.Point(26, 246)
            Me.cboBed.Name = "cboBed"
            Me.cboBed.Size = New System.Drawing.Size(508, 25)
            Me.cboBed.TabIndex = 9
            '
            'lblBed
            '
            Me.lblBed.AutoSize = True
            Me.lblBed.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblBed.Location = New System.Drawing.Point(24, 226)
            Me.lblBed.Name = "lblBed"
            Me.lblBed.Size = New System.Drawing.Size(147, 15)
            Me.lblBed.TabIndex = 8
            Me.lblBed.Text = "Target Bed Space (Free) *"
            '
            'cboRoom
            '
            Me.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRoom.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboRoom.FormattingEnabled = True
            Me.cboRoom.Location = New System.Drawing.Point(286, 186)
            Me.cboRoom.Name = "cboRoom"
            Me.cboRoom.Size = New System.Drawing.Size(248, 25)
            Me.cboRoom.TabIndex = 7
            '
            'lblRoom
            '
            Me.lblRoom.AutoSize = True
            Me.lblRoom.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblRoom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblRoom.Location = New System.Drawing.Point(284, 166)
            Me.lblRoom.Name = "lblRoom"
            Me.lblRoom.Size = New System.Drawing.Size(89, 15)
            Me.lblRoom.TabIndex = 6
            Me.lblRoom.Text = "Hostel Room *"
            '
            'cboHostel
            '
            Me.cboHostel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboHostel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboHostel.FormattingEnabled = True
            Me.cboHostel.Location = New System.Drawing.Point(26, 186)
            Me.cboHostel.Name = "cboHostel"
            Me.cboHostel.Size = New System.Drawing.Size(248, 25)
            Me.cboHostel.TabIndex = 5
            '
            'lblHostel
            '
            Me.lblHostel.AutoSize = True
            Me.lblHostel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblHostel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblHostel.Location = New System.Drawing.Point(24, 166)
            Me.lblHostel.Name = "lblHostel"
            Me.lblHostel.Size = New System.Drawing.Size(92, 15)
            Me.lblHostel.TabIndex = 4
            Me.lblHostel.Text = "Hostel Facility *"
            '
            'cboStudent
            '
            Me.cboStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboStudent.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboStudent.FormattingEnabled = True
            Me.cboStudent.Location = New System.Drawing.Point(26, 124)
            Me.cboStudent.Name = "cboStudent"
            Me.cboStudent.Size = New System.Drawing.Size(508, 25)
            Me.cboStudent.TabIndex = 3
            '
            'lblStudent
            '
            Me.lblStudent.AutoSize = True
            Me.lblStudent.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStudent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStudent.Location = New System.Drawing.Point(24, 104)
            Me.lblStudent.Name = "lblStudent"
            Me.lblStudent.Size = New System.Drawing.Size(158, 15)
            Me.lblStudent.TabIndex = 2
            Me.lblStudent.Text = "Student Resident to Allocate *"
            '
            'cboSemester
            '
            Me.cboSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSemester.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboSemester.FormattingEnabled = True
            Me.cboSemester.Location = New System.Drawing.Point(26, 62)
            Me.cboSemester.Name = "cboSemester"
            Me.cboSemester.Size = New System.Drawing.Size(508, 25)
            Me.cboSemester.TabIndex = 1
            '
            'lblSemester
            '
            Me.lblSemester.AutoSize = True
            Me.lblSemester.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblSemester.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblSemester.Location = New System.Drawing.Point(24, 42)
            Me.lblSemester.Name = "lblSemester"
            Me.lblSemester.Size = New System.Drawing.Size(117, 15)
            Me.lblSemester.TabIndex = 0
            Me.lblSemester.Text = "Academic Semester *"
            '
            'pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Controls.Add(Me.btnAllocate)
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
            Me.btnCancel.Location = New System.Drawing.Point(274, 13)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(110, 34)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'btnAllocate
            '
            Me.btnAllocate.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.btnAllocate.FlatAppearance.BorderSize = 0
            Me.btnAllocate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnAllocate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnAllocate.ForeColor = System.Drawing.Color.White
            Me.btnAllocate.Location = New System.Drawing.Point(392, 13)
            Me.btnAllocate.Name = "btnAllocate"
            Me.btnAllocate.Size = New System.Drawing.Size(142, 34)
            Me.btnAllocate.TabIndex = 0
            Me.btnAllocate.Text = "Allocate Bed Space"
            Me.btnAllocate.UseVisualStyleBackColor = False
            '
            'frmAllocationModal
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
            Me.Name = "frmAllocationModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Bed Space Allocation"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            Me.pnlRateBanner.ResumeLayout(False)
            Me.pnlRateBanner.PerformLayout()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents pnlRateBanner As System.Windows.Forms.Panel
        Friend WithEvents lblRateSub As System.Windows.Forms.Label
        Friend WithEvents lblRateAmount As System.Windows.Forms.Label
        Friend WithEvents lblRateTitle As System.Windows.Forms.Label
        Friend WithEvents txtNotes As System.Windows.Forms.TextBox
        Friend WithEvents lblNotes As System.Windows.Forms.Label
        Friend WithEvents cboBed As System.Windows.Forms.ComboBox
        Friend WithEvents lblBed As System.Windows.Forms.Label
        Friend WithEvents cboRoom As System.Windows.Forms.ComboBox
        Friend WithEvents lblRoom As System.Windows.Forms.Label
        Friend WithEvents cboHostel As System.Windows.Forms.ComboBox
        Friend WithEvents lblHostel As System.Windows.Forms.Label
        Friend WithEvents cboStudent As System.Windows.Forms.ComboBox
        Friend WithEvents lblStudent As System.Windows.Forms.Label
        Friend WithEvents cboSemester As System.Windows.Forms.ComboBox
        Friend WithEvents lblSemester As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnAllocate As System.Windows.Forms.Button
    End Class
End Namespace
