Namespace Forms.Allocations
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmBedTransferModal
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
            Me.pnlDiffBanner = New System.Windows.Forms.Panel()
            Me.lblDiffSummary = New System.Windows.Forms.Label()
            Me.lblDiffAmount = New System.Windows.Forms.Label()
            Me.lblDiffTitle = New System.Windows.Forms.Label()
            Me.txtReason = New System.Windows.Forms.TextBox()
            Me.lblReason = New System.Windows.Forms.Label()
            Me.cboNewBed = New System.Windows.Forms.ComboBox()
            Me.lblNewBed = New System.Windows.Forms.Label()
            Me.cboNewRoom = New System.Windows.Forms.ComboBox()
            Me.lblNewRoom = New System.Windows.Forms.Label()
            Me.cboNewHostel = New System.Windows.Forms.ComboBox()
            Me.lblNewHostel = New System.Windows.Forms.Label()
            Me.pnlCurrentCard = New System.Windows.Forms.Panel()
            Me.lblCurrentRate = New System.Windows.Forms.Label()
            Me.lblCurrentBed = New System.Windows.Forms.Label()
            Me.lblStudentName = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnExecuteTransfer = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            Me.pnlDiffBanner.SuspendLayout()
            Me.pnlCurrentCard.SuspendLayout()
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
            Me.lblModalTitle.Size = New System.Drawing.Size(201, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Resident Bed Space Transfer"
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
            Me.lblModalSub.Text = "Transfer resident to a different bed space within the semester with rate reconciliation."
            '
            'pnlBody
            '
            Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlBody.Controls.Add(Me.pnlDiffBanner)
            Me.pnlBody.Controls.Add(Me.txtReason)
            Me.pnlBody.Controls.Add(Me.lblReason)
            Me.pnlBody.Controls.Add(Me.cboNewBed)
            Me.pnlBody.Controls.Add(Me.lblNewBed)
            Me.pnlBody.Controls.Add(Me.cboNewRoom)
            Me.pnlBody.Controls.Add(Me.lblNewRoom)
            Me.pnlBody.Controls.Add(Me.cboNewHostel)
            Me.pnlBody.Controls.Add(Me.lblNewHostel)
            Me.pnlBody.Controls.Add(Me.pnlCurrentCard)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 71)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlBody.Size = New System.Drawing.Size(560, 500)
            Me.pnlBody.TabIndex = 2
            '
            'pnlDiffBanner
            '
            Me.pnlDiffBanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(199, Byte), Integer))
            Me.pnlDiffBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlDiffBanner.Controls.Add(Me.lblDiffSummary)
            Me.pnlDiffBanner.Controls.Add(Me.lblDiffAmount)
            Me.pnlDiffBanner.Controls.Add(Me.lblDiffTitle)
            Me.pnlDiffBanner.Location = New System.Drawing.Point(26, 298)
            Me.pnlDiffBanner.Name = "pnlDiffBanner"
            Me.pnlDiffBanner.Padding = New System.Windows.Forms.Padding(16, 10, 16, 10)
            Me.pnlDiffBanner.Size = New System.Drawing.Size(508, 62)
            Me.pnlDiffBanner.TabIndex = 8
            '
            'lblDiffSummary
            '
            Me.lblDiffSummary.AutoSize = True
            Me.lblDiffSummary.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblDiffSummary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.lblDiffSummary.Location = New System.Drawing.Point(14, 36)
            Me.lblDiffSummary.Name = "lblDiffSummary"
            Me.lblDiffSummary.Size = New System.Drawing.Size(326, 13)
            Me.lblDiffSummary.TabIndex = 2
            Me.lblDiffSummary.Text = "Current KES 20,000.00 ➔ Destination KES 25,000.00 (+KES 5,000.00)"
            '
            'lblDiffAmount
            '
            Me.lblDiffAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblDiffAmount.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDiffAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(9, Byte), Integer))
            Me.lblDiffAmount.Location = New System.Drawing.Point(310, 10)
            Me.lblDiffAmount.Name = "lblDiffAmount"
            Me.lblDiffAmount.Size = New System.Drawing.Size(180, 22)
            Me.lblDiffAmount.TabIndex = 1
            Me.lblDiffAmount.Text = "KES 0.00"
            Me.lblDiffAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblDiffTitle
            '
            Me.lblDiffTitle.AutoSize = True
            Me.lblDiffTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDiffTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.lblDiffTitle.Location = New System.Drawing.Point(14, 12)
            Me.lblDiffTitle.Name = "lblDiffTitle"
            Me.lblDiffTitle.Size = New System.Drawing.Size(130, 15)
            Me.lblDiffTitle.TabIndex = 0
            Me.lblDiffTitle.Text = "Fee Rate Reconciliation:"
            '
            'txtReason
            '
            Me.txtReason.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtReason.Location = New System.Drawing.Point(26, 394)
            Me.txtReason.Multiline = True
            Me.txtReason.Name = "txtReason"
            Me.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtReason.Size = New System.Drawing.Size(508, 68)
            Me.txtReason.TabIndex = 10
            '
            'lblReason
            '
            Me.lblReason.AutoSize = True
            Me.lblReason.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblReason.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblReason.Location = New System.Drawing.Point(24, 374)
            Me.lblReason.Name = "lblReason"
            Me.lblReason.Size = New System.Drawing.Size(186, 15)
            Me.lblReason.TabIndex = 9
            Me.lblReason.Text = "Reason for Bed Space Transfer *"
            '
            'cboNewBed
            '
            Me.cboNewBed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboNewBed.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboNewBed.FormattingEnabled = True
            Me.cboNewBed.Location = New System.Drawing.Point(26, 254)
            Me.cboNewBed.Name = "cboNewBed"
            Me.cboNewBed.Size = New System.Drawing.Size(508, 25)
            Me.cboNewBed.TabIndex = 7
            '
            'lblNewBed
            '
            Me.lblNewBed.AutoSize = True
            Me.lblNewBed.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNewBed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblNewBed.Location = New System.Drawing.Point(24, 234)
            Me.lblNewBed.Name = "lblNewBed"
            Me.lblNewBed.Size = New System.Drawing.Size(183, 15)
            Me.lblNewBed.TabIndex = 6
            Me.lblNewBed.Text = "Select Destination Free Bed *"
            '
            'cboNewRoom
            '
            Me.cboNewRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboNewRoom.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboNewRoom.FormattingEnabled = True
            Me.cboNewRoom.Location = New System.Drawing.Point(286, 194)
            Me.cboNewRoom.Name = "cboNewRoom"
            Me.cboNewRoom.Size = New System.Drawing.Size(248, 25)
            Me.cboNewRoom.TabIndex = 5
            '
            'lblNewRoom
            '
            Me.lblNewRoom.AutoSize = True
            Me.lblNewRoom.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNewRoom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblNewRoom.Location = New System.Drawing.Point(284, 174)
            Me.lblNewRoom.Name = "lblNewRoom"
            Me.lblNewRoom.Size = New System.Drawing.Size(117, 15)
            Me.lblNewRoom.TabIndex = 4
            Me.lblNewRoom.Text = "Destination Room *"
            '
            'cboNewHostel
            '
            Me.cboNewHostel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboNewHostel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboNewHostel.FormattingEnabled = True
            Me.cboNewHostel.Location = New System.Drawing.Point(26, 194)
            Me.cboNewHostel.Name = "cboNewHostel"
            Me.cboNewHostel.Size = New System.Drawing.Size(248, 25)
            Me.cboNewHostel.TabIndex = 3
            '
            'lblNewHostel
            '
            Me.lblNewHostel.AutoSize = True
            Me.lblNewHostel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNewHostel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblNewHostel.Location = New System.Drawing.Point(24, 174)
            Me.lblNewHostel.Name = "lblNewHostel"
            Me.lblNewHostel.Size = New System.Drawing.Size(120, 15)
            Me.lblNewHostel.TabIndex = 2
            Me.lblNewHostel.Text = "Destination Hostel *"
            '
            'pnlCurrentCard
            '
            Me.pnlCurrentCard.BackColor = System.Drawing.Color.White
            Me.pnlCurrentCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCurrentCard.Controls.Add(Me.lblCurrentRate)
            Me.pnlCurrentCard.Controls.Add(Me.lblCurrentBed)
            Me.pnlCurrentCard.Controls.Add(Me.lblStudentName)
            Me.pnlCurrentCard.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCurrentCard.Location = New System.Drawing.Point(24, 24)
            Me.pnlCurrentCard.Name = "pnlCurrentCard"
            Me.pnlCurrentCard.Padding = New System.Windows.Forms.Padding(16, 12, 16, 12)
            Me.pnlCurrentCard.Size = New System.Drawing.Size(512, 80)
            Me.pnlCurrentCard.TabIndex = 1
            '
            'lblCurrentRate
            '
            Me.lblCurrentRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblCurrentRate.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblCurrentRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblCurrentRate.Location = New System.Drawing.Point(318, 12)
            Me.lblCurrentRate.Name = "lblCurrentRate"
            Me.lblCurrentRate.Size = New System.Drawing.Size(175, 20)
            Me.lblCurrentRate.TabIndex = 2
            Me.lblCurrentRate.Text = "Rate: KES 20,000.00"
            Me.lblCurrentRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblCurrentBed
            '
            Me.lblCurrentBed.AutoSize = True
            Me.lblCurrentBed.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblCurrentBed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.lblCurrentBed.Location = New System.Drawing.Point(14, 38)
            Me.lblCurrentBed.Name = "lblCurrentBed"
            Me.lblCurrentBed.Size = New System.Drawing.Size(268, 15)
            Me.lblCurrentBed.TabIndex = 1
            Me.lblCurrentBed.Text = "Current: Mount Kenya Hall • Rm 101 • Bed A"
            '
            'lblStudentName
            '
            Me.lblStudentName.AutoSize = True
            Me.lblStudentName.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStudentName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStudentName.Location = New System.Drawing.Point(14, 12)
            Me.lblStudentName.Name = "lblStudentName"
            Me.lblStudentName.Size = New System.Drawing.Size(267, 19)
            Me.lblStudentName.TabIndex = 0
            Me.lblStudentName.Text = "Arthur Pendelton (CCS/00101/021)"
            '
            'pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Controls.Add(Me.btnExecuteTransfer)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 571)
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
            Me.btnCancel.Location = New System.Drawing.Point(264, 13)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(110, 34)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'btnExecuteTransfer
            '
            Me.btnExecuteTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.btnExecuteTransfer.FlatAppearance.BorderSize = 0
            Me.btnExecuteTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnExecuteTransfer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnExecuteTransfer.ForeColor = System.Drawing.Color.White
            Me.btnExecuteTransfer.Location = New System.Drawing.Point(382, 13)
            Me.btnExecuteTransfer.Name = "btnExecuteTransfer"
            Me.btnExecuteTransfer.Size = New System.Drawing.Size(152, 34)
            Me.btnExecuteTransfer.TabIndex = 0
            Me.btnExecuteTransfer.Text = "Execute Transfer"
            Me.btnExecuteTransfer.UseVisualStyleBackColor = False
            '
            'frmBedTransferModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(560, 631)
            Me.Controls.Add(Me.pnlBody)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopGold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmBedTransferModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Bed Space Transfer"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            Me.pnlDiffBanner.ResumeLayout(False)
            Me.pnlDiffBanner.PerformLayout()
            Me.pnlCurrentCard.ResumeLayout(False)
            Me.pnlCurrentCard.PerformLayout()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents pnlCurrentCard As System.Windows.Forms.Panel
        Friend WithEvents lblStudentName As System.Windows.Forms.Label
        Friend WithEvents lblCurrentBed As System.Windows.Forms.Label
        Friend WithEvents lblCurrentRate As System.Windows.Forms.Label
        Friend WithEvents cboNewHostel As System.Windows.Forms.ComboBox
        Friend WithEvents lblNewHostel As System.Windows.Forms.Label
        Friend WithEvents cboNewRoom As System.Windows.Forms.ComboBox
        Friend WithEvents lblNewRoom As System.Windows.Forms.Label
        Friend WithEvents cboNewBed As System.Windows.Forms.ComboBox
        Friend WithEvents lblNewBed As System.Windows.Forms.Label
        Friend WithEvents pnlDiffBanner As System.Windows.Forms.Panel
        Friend WithEvents lblDiffTitle As System.Windows.Forms.Label
        Friend WithEvents lblDiffAmount As System.Windows.Forms.Label
        Friend WithEvents lblDiffSummary As System.Windows.Forms.Label
        Friend WithEvents txtReason As System.Windows.Forms.TextBox
        Friend WithEvents lblReason As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnExecuteTransfer As System.Windows.Forms.Button
    End Class
End Namespace

