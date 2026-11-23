Namespace Forms.Billing
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmReceiptPreviewModal
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
            Me.pnlReceiptPaper = New System.Windows.Forms.Panel()
            Me.picCrest = New System.Windows.Forms.PictureBox()
            Me.lblUnivTitle = New System.Windows.Forms.Label()
            Me.lblUnivSub = New System.Windows.Forms.Label()
            Me.pnlLine1 = New System.Windows.Forms.Panel()
            Me.lblReceiptNo = New System.Windows.Forms.Label()
            Me.lblReceiptDate = New System.Windows.Forms.Label()
            Me.pnlStudentCard = New System.Windows.Forms.Panel()
            Me.lblStudentInfo = New System.Windows.Forms.Label()
            Me.lblBedInfo = New System.Windows.Forms.Label()
            Me.lblSemesterInfo = New System.Windows.Forms.Label()
            Me.pnlTable = New System.Windows.Forms.Panel()
            Me.lblMethodRow = New System.Windows.Forms.Label()
            Me.lblTxRefRow = New System.Windows.Forms.Label()
            Me.lblFeeRow = New System.Windows.Forms.Label()
            Me.pnlPaidBanner = New System.Windows.Forms.Panel()
            Me.lblPaidTitle = New System.Windows.Forms.Label()
            Me.lblPaidAmount = New System.Windows.Forms.Label()
            Me.lblBalanceRemaining = New System.Windows.Forms.Label()
            Me.lblCashierSign = New System.Windows.Forms.Label()
            Me.lblOfficialSeal = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.btnPrint = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlReceiptPaper.SuspendLayout()
            CType(Me.picCrest, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlStudentCard.SuspendLayout()
            Me.pnlTable.SuspendLayout()
            Me.pnlPaidBanner.SuspendLayout()
            Me.pnlFooter.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopGold
            '
            Me.pnlTopGold.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.pnlTopGold.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopGold.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopGold.Name = "pnlTopGold"
            Me.pnlTopGold.Size = New System.Drawing.Size(600, 3)
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
            Me.pnlHeader.Size = New System.Drawing.Size(600, 68)
            Me.pnlHeader.TabIndex = 1
            '
            'lblModalTitle
            '
            Me.lblModalTitle.AutoSize = True
            Me.lblModalTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblModalTitle.ForeColor = System.Drawing.Color.White
            Me.lblModalTitle.Location = New System.Drawing.Point(22, 12)
            Me.lblModalTitle.Name = "lblModalTitle"
            Me.lblModalTitle.Size = New System.Drawing.Size(262, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Official Bursar Electronic Receipt"
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
            Me.lblModalSub.Text = "Standard official student payment receipt with institutional authentication seal."
            '
            'pnlReceiptPaper
            '
            Me.pnlReceiptPaper.BackColor = System.Drawing.Color.White
            Me.pnlReceiptPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlReceiptPaper.Controls.Add(Me.lblOfficialSeal)
            Me.pnlReceiptPaper.Controls.Add(Me.lblCashierSign)
            Me.pnlReceiptPaper.Controls.Add(Me.lblBalanceRemaining)
            Me.pnlReceiptPaper.Controls.Add(Me.pnlPaidBanner)
            Me.pnlReceiptPaper.Controls.Add(Me.pnlTable)
            Me.pnlReceiptPaper.Controls.Add(Me.pnlStudentCard)
            Me.pnlReceiptPaper.Controls.Add(Me.lblReceiptDate)
            Me.pnlReceiptPaper.Controls.Add(Me.lblReceiptNo)
            Me.pnlReceiptPaper.Controls.Add(Me.pnlLine1)
            Me.pnlReceiptPaper.Controls.Add(Me.lblUnivSub)
            Me.pnlReceiptPaper.Controls.Add(Me.lblUnivTitle)
            Me.pnlReceiptPaper.Controls.Add(Me.picCrest)
            Me.pnlReceiptPaper.Location = New System.Drawing.Point(30, 85)
            Me.pnlReceiptPaper.Name = "pnlReceiptPaper"
            Me.pnlReceiptPaper.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlReceiptPaper.Size = New System.Drawing.Size(540, 500)
            Me.pnlReceiptPaper.TabIndex = 2
            '
            'picCrest
            '
            Me.picCrest.Location = New System.Drawing.Point(24, 18)
            Me.picCrest.Name = "picCrest"
            Me.picCrest.Size = New System.Drawing.Size(50, 52)
            Me.picCrest.TabIndex = 0
            Me.picCrest.TabStop = False
            '
            'lblUnivTitle
            '
            Me.lblUnivTitle.AutoSize = True
            Me.lblUnivTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUnivTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.lblUnivTitle.Location = New System.Drawing.Point(82, 20)
            Me.lblUnivTitle.Name = "lblUnivTitle"
            Me.lblUnivTitle.Size = New System.Drawing.Size(360, 21)
            Me.lblUnivTitle.TabIndex = 1
            Me.lblUnivTitle.Text = "UNIVERSITY ACCOMMODATION DIRECTORATE"
            Me.lblUnivTitle.UseMnemonic = False
            '
            'lblUnivSub
            '
            Me.lblUnivSub.AutoSize = True
            Me.lblUnivSub.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUnivSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.lblUnivSub.Location = New System.Drawing.Point(84, 44)
            Me.lblUnivSub.Name = "lblUnivSub"
            Me.lblUnivSub.Size = New System.Drawing.Size(262, 15)
            Me.lblUnivSub.TabIndex = 2
            Me.lblUnivSub.Text = "OFFICIAL STUDENT BURSARY PAYMENT RECEIPT"
            Me.lblUnivSub.UseMnemonic = False
            '
            'pnlLine1
            '
            Me.pnlLine1.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
            Me.pnlLine1.Location = New System.Drawing.Point(24, 78)
            Me.pnlLine1.Name = "pnlLine1"
            Me.pnlLine1.Size = New System.Drawing.Size(490, 1)
            Me.pnlLine1.TabIndex = 3
            '
            'lblReceiptNo
            '
            Me.lblReceiptNo.AutoSize = True
            Me.lblReceiptNo.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblReceiptNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblReceiptNo.Location = New System.Drawing.Point(24, 88)
            Me.lblReceiptNo.Name = "lblReceiptNo"
            Me.lblReceiptNo.Size = New System.Drawing.Size(168, 17)
            Me.lblReceiptNo.TabIndex = 4
            Me.lblReceiptNo.Text = "RCP-2026-01004"
            '
            'lblReceiptDate
            '
            Me.lblReceiptDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblReceiptDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblReceiptDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblReceiptDate.Location = New System.Drawing.Point(294, 88)
            Me.lblReceiptDate.Name = "lblReceiptDate"
            Me.lblReceiptDate.Size = New System.Drawing.Size(220, 18)
            Me.lblReceiptDate.TabIndex = 5
            Me.lblReceiptDate.Text = "Date: 2026-11-23 16:15 UTC"
            Me.lblReceiptDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'pnlStudentCard
            '
            Me.pnlStudentCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlStudentCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlStudentCard.Controls.Add(Me.lblSemesterInfo)
            Me.pnlStudentCard.Controls.Add(Me.lblBedInfo)
            Me.pnlStudentCard.Controls.Add(Me.lblStudentInfo)
            Me.pnlStudentCard.Location = New System.Drawing.Point(24, 114)
            Me.pnlStudentCard.Name = "pnlStudentCard"
            Me.pnlStudentCard.Padding = New System.Windows.Forms.Padding(12)
            Me.pnlStudentCard.Size = New System.Drawing.Size(490, 72)
            Me.pnlStudentCard.TabIndex = 6
            '
            'lblStudentInfo
            '
            Me.lblStudentInfo.AutoSize = True
            Me.lblStudentInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStudentInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStudentInfo.Location = New System.Drawing.Point(10, 8)
            Me.lblStudentInfo.Name = "lblStudentInfo"
            Me.lblStudentInfo.Size = New System.Drawing.Size(283, 17)
            Me.lblStudentInfo.TabIndex = 0
            Me.lblStudentInfo.Text = "Student: Arthur Pendelton (Reg: CCS/00101/021)"
            '
            'lblBedInfo
            '
            Me.lblBedInfo.AutoSize = True
            Me.lblBedInfo.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblBedInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblBedInfo.Location = New System.Drawing.Point(10, 28)
            Me.lblBedInfo.Name = "lblBedInfo"
            Me.lblBedInfo.Size = New System.Drawing.Size(306, 15)
            Me.lblBedInfo.TabIndex = 1
            Me.lblBedInfo.Text = "Allocated Space: Mount Kenya Hall • Room 101 • Bed A"
            '
            'lblSemesterInfo
            '
            Me.lblSemesterInfo.AutoSize = True
            Me.lblSemesterInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSemesterInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblSemesterInfo.Location = New System.Drawing.Point(10, 48)
            Me.lblSemesterInfo.Name = "lblSemesterInfo"
            Me.lblSemesterInfo.Size = New System.Drawing.Size(262, 13)
            Me.lblSemesterInfo.TabIndex = 2
            Me.lblSemesterInfo.Text = "Academic Period: Semester 1 2026/2027 (ACTIVE)"
            '
            'pnlTable
            '
            Me.pnlTable.Controls.Add(Me.lblFeeRow)
            Me.pnlTable.Controls.Add(Me.lblTxRefRow)
            Me.pnlTable.Controls.Add(Me.lblMethodRow)
            Me.pnlTable.Location = New System.Drawing.Point(24, 196)
            Me.pnlTable.Name = "pnlTable"
            Me.pnlTable.Size = New System.Drawing.Size(490, 80)
            Me.pnlTable.TabIndex = 7
            '
            'lblFeeRow
            '
            Me.lblFeeRow.AutoSize = True
            Me.lblFeeRow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblFeeRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblFeeRow.Location = New System.Drawing.Point(10, 54)
            Me.lblFeeRow.Name = "lblFeeRow"
            Me.lblFeeRow.Size = New System.Drawing.Size(211, 15)
            Me.lblFeeRow.TabIndex = 2
            Me.lblFeeRow.Text = "Semester Allocated Fee: KES 20,000.00"
            '
            'lblTxRefRow
            '
            Me.lblTxRefRow.AutoSize = True
            Me.lblTxRefRow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblTxRefRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblTxRefRow.Location = New System.Drawing.Point(10, 30)
            Me.lblTxRefRow.Name = "lblTxRefRow"
            Me.lblTxRefRow.Size = New System.Drawing.Size(186, 15)
            Me.lblTxRefRow.TabIndex = 1
            Me.lblTxRefRow.Text = "Transaction Reference: QKJ882190A"
            '
            'lblMethodRow
            '
            Me.lblMethodRow.AutoSize = True
            Me.lblMethodRow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblMethodRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblMethodRow.Location = New System.Drawing.Point(10, 6)
            Me.lblMethodRow.Name = "lblMethodRow"
            Me.lblMethodRow.Size = New System.Drawing.Size(199, 15)
            Me.lblMethodRow.TabIndex = 0
            Me.lblMethodRow.Text = "Payment Channel: MPESA Verified"
            '
            'pnlPaidBanner
            '
            Me.pnlPaidBanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(231, Byte), Integer))
            Me.pnlPaidBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlPaidBanner.Controls.Add(Me.lblPaidAmount)
            Me.pnlPaidBanner.Controls.Add(Me.lblPaidTitle)
            Me.pnlPaidBanner.Location = New System.Drawing.Point(24, 286)
            Me.pnlPaidBanner.Name = "pnlPaidBanner"
            Me.pnlPaidBanner.Padding = New System.Windows.Forms.Padding(16, 10, 16, 10)
            Me.pnlPaidBanner.Size = New System.Drawing.Size(490, 56)
            Me.pnlPaidBanner.TabIndex = 8
            '
            'lblPaidAmount
            '
            Me.lblPaidAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblPaidAmount.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPaidAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(52, Byte), Integer))
            Me.lblPaidAmount.Location = New System.Drawing.Point(240, 12)
            Me.lblPaidAmount.Name = "lblPaidAmount"
            Me.lblPaidAmount.Size = New System.Drawing.Size(235, 28)
            Me.lblPaidAmount.TabIndex = 1
            Me.lblPaidAmount.Text = "KES 15,000.00"
            Me.lblPaidAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblPaidTitle
            '
            Me.lblPaidTitle.AutoSize = True
            Me.lblPaidTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPaidTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(52, Byte), Integer))
            Me.lblPaidTitle.Location = New System.Drawing.Point(14, 16)
            Me.lblPaidTitle.Name = "lblPaidTitle"
            Me.lblPaidTitle.Size = New System.Drawing.Size(183, 19)
            Me.lblPaidTitle.TabIndex = 0
            Me.lblPaidTitle.Text = "AMOUNT RECEIVED (PAID):"
            '
            'lblBalanceRemaining
            '
            Me.lblBalanceRemaining.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBalanceRemaining.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.lblBalanceRemaining.Location = New System.Drawing.Point(24, 354)
            Me.lblBalanceRemaining.Name = "lblBalanceRemaining"
            Me.lblBalanceRemaining.Size = New System.Drawing.Size(490, 20)
            Me.lblBalanceRemaining.TabIndex = 9
            Me.lblBalanceRemaining.Text = "Remaining Semester Balance Due: KES 5,000.00"
            Me.lblBalanceRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblCashierSign
            '
            Me.lblCashierSign.AutoSize = True
            Me.lblCashierSign.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblCashierSign.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblCashierSign.Location = New System.Drawing.Point(24, 430)
            Me.lblCashierSign.Name = "lblCashierSign"
            Me.lblCashierSign.Size = New System.Drawing.Size(262, 13)
            Me.lblCashierSign.TabIndex = 10
            Me.lblCashierSign.Text = "Cashier: Directorate Bursar • Verified ADO.NET Record"
            '
            'lblOfficialSeal
            '
            Me.lblOfficialSeal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblOfficialSeal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblOfficialSeal.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblOfficialSeal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.lblOfficialSeal.Location = New System.Drawing.Point(364, 410)
            Me.lblOfficialSeal.Name = "lblOfficialSeal"
            Me.lblOfficialSeal.Size = New System.Drawing.Size(150, 52)
            Me.lblOfficialSeal.TabIndex = 11
            Me.lblOfficialSeal.Text = "UNIVERSITY HOSTEL" & vbCrLf & "AUTHENTICATED" & vbCrLf & "ACCOMMODATION SEAL"
            Me.lblOfficialSeal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlFooter.Controls.Add(Me.btnPrint)
            Me.pnlFooter.Controls.Add(Me.btnClose)
            Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlFooter.Location = New System.Drawing.Point(0, 608)
            Me.pnlFooter.Name = "pnlFooter"
            Me.pnlFooter.Padding = New System.Windows.Forms.Padding(24, 12, 24, 12)
            Me.pnlFooter.Size = New System.Drawing.Size(600, 60)
            Me.pnlFooter.TabIndex = 3
            '
            'btnClose
            '
            Me.btnClose.BackColor = System.Drawing.Color.White
            Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnClose.Location = New System.Drawing.Point(344, 13)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(100, 34)
            Me.btnClose.TabIndex = 1
            Me.btnClose.Text = "Close"
            Me.btnClose.UseVisualStyleBackColor = False
            '
            'btnPrint
            '
            Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.btnPrint.FlatAppearance.BorderSize = 0
            Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnPrint.ForeColor = System.Drawing.Color.White
            Me.btnPrint.Location = New System.Drawing.Point(452, 13)
            Me.btnPrint.Name = "btnPrint"
            Me.btnPrint.Size = New System.Drawing.Size(120, 34)
            Me.btnPrint.TabIndex = 0
            Me.btnPrint.Text = "🖨️  Print Receipt"
            Me.btnPrint.UseVisualStyleBackColor = False
            '
            'frmReceiptPreviewModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(600, 668)
            Me.Controls.Add(Me.pnlFooter)
            Me.Controls.Add(Me.pnlReceiptPaper)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopGold)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmReceiptPreviewModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Electronic Receipt Preview"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlReceiptPaper.ResumeLayout(False)
            Me.pnlReceiptPaper.PerformLayout()
            CType(Me.picCrest, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlStudentCard.ResumeLayout(False)
            Me.pnlStudentCard.PerformLayout()
            Me.pnlTable.ResumeLayout(False)
            Me.pnlTable.PerformLayout()
            Me.pnlPaidBanner.ResumeLayout(False)
            Me.pnlPaidBanner.PerformLayout()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlReceiptPaper As System.Windows.Forms.Panel
        Friend WithEvents picCrest As System.Windows.Forms.PictureBox
        Friend WithEvents lblUnivTitle As System.Windows.Forms.Label
        Friend WithEvents lblUnivSub As System.Windows.Forms.Label
        Friend WithEvents pnlLine1 As System.Windows.Forms.Panel
        Friend WithEvents lblReceiptNo As System.Windows.Forms.Label
        Friend WithEvents lblReceiptDate As System.Windows.Forms.Label
        Friend WithEvents pnlStudentCard As System.Windows.Forms.Panel
        Friend WithEvents lblStudentInfo As System.Windows.Forms.Label
        Friend WithEvents lblBedInfo As System.Windows.Forms.Label
        Friend WithEvents lblSemesterInfo As System.Windows.Forms.Label
        Friend WithEvents pnlTable As System.Windows.Forms.Panel
        Friend WithEvents lblMethodRow As System.Windows.Forms.Label
        Friend WithEvents lblTxRefRow As System.Windows.Forms.Label
        Friend WithEvents lblFeeRow As System.Windows.Forms.Label
        Friend WithEvents pnlPaidBanner As System.Windows.Forms.Panel
        Friend WithEvents lblPaidTitle As System.Windows.Forms.Label
        Friend WithEvents lblPaidAmount As System.Windows.Forms.Label
        Friend WithEvents lblBalanceRemaining As System.Windows.Forms.Label
        Friend WithEvents lblCashierSign As System.Windows.Forms.Label
        Friend WithEvents lblOfficialSeal As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents btnPrint As System.Windows.Forms.Button
    End Class
End Namespace
