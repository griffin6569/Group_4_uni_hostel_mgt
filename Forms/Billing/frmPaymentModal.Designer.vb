Namespace Forms.Billing
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmPaymentModal
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
            Me.txtNotes = New System.Windows.Forms.TextBox()
            Me.lblNotes = New System.Windows.Forms.Label()
            Me.dtpPaymentDate = New System.Windows.Forms.DateTimePicker()
            Me.lblDate = New System.Windows.Forms.Label()
            Me.txtReceiptNo = New System.Windows.Forms.TextBox()
            Me.lblReceiptNo = New System.Windows.Forms.Label()
            Me.btnAutoRef = New System.Windows.Forms.Button()
            Me.txtTxRef = New System.Windows.Forms.TextBox()
            Me.lblTxRef = New System.Windows.Forms.Label()
            Me.cboPaymentMethod = New System.Windows.Forms.ComboBox()
            Me.lblMethod = New System.Windows.Forms.Label()
            Me.btnFullBalance = New System.Windows.Forms.Button()
            Me.numAmount = New System.Windows.Forms.NumericUpDown()
            Me.lblAmount = New System.Windows.Forms.Label()
            Me.pnlStatusBanner = New System.Windows.Forms.Panel()
            Me.lblBalDue = New System.Windows.Forms.Label()
            Me.lblTotalPaid = New System.Windows.Forms.Label()
            Me.lblTotalFee = New System.Windows.Forms.Label()
            Me.cboAllocation = New System.Windows.Forms.ComboBox()
            Me.lblAllocation = New System.Windows.Forms.Label()
            Me.pnlFooter = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnSaveAndPrint = New System.Windows.Forms.Button()
            Me.btnSave = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlBody.SuspendLayout()
            CType(Me.numAmount, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlStatusBanner.SuspendLayout()
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
            Me.lblModalTitle.Size = New System.Drawing.Size(252, 21)
            Me.lblModalTitle.TabIndex = 0
            Me.lblModalTitle.Text = "Record Student Resident Payment"
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
            Me.lblModalSub.Text = "Issue university bursary receipt, verify MPESA/Bank reference, and adjust arrears."
            '
            'pnlBody
            '
            Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlBody.Controls.Add(Me.txtNotes)
            Me.pnlBody.Controls.Add(Me.lblNotes)
            Me.pnlBody.Controls.Add(Me.dtpPaymentDate)
            Me.pnlBody.Controls.Add(Me.lblDate)
            Me.pnlBody.Controls.Add(Me.txtReceiptNo)
            Me.pnlBody.Controls.Add(Me.lblReceiptNo)
            Me.pnlBody.Controls.Add(Me.btnAutoRef)
            Me.pnlBody.Controls.Add(Me.txtTxRef)
            Me.pnlBody.Controls.Add(Me.lblTxRef)
            Me.pnlBody.Controls.Add(Me.cboPaymentMethod)
            Me.pnlBody.Controls.Add(Me.lblMethod)
            Me.pnlBody.Controls.Add(Me.btnFullBalance)
            Me.pnlBody.Controls.Add(Me.numAmount)
            Me.pnlBody.Controls.Add(Me.lblAmount)
            Me.pnlBody.Controls.Add(Me.pnlStatusBanner)
            Me.pnlBody.Controls.Add(Me.cboAllocation)
            Me.pnlBody.Controls.Add(Me.lblAllocation)
            Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBody.Location = New System.Drawing.Point(0, 71)
            Me.pnlBody.Name = "pnlBody"
            Me.pnlBody.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlBody.Size = New System.Drawing.Size(560, 500)
            Me.pnlBody.TabIndex = 2
            '
            'txtNotes
            '
            Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtNotes.Location = New System.Drawing.Point(26, 404)
            Me.txtNotes.Multiline = True
            Me.txtNotes.Name = "txtNotes"
            Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtNotes.Size = New System.Drawing.Size(508, 60)
            Me.txtNotes.TabIndex = 16
            '
            'lblNotes
            '
            Me.lblNotes.AutoSize = True
            Me.lblNotes.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblNotes.Location = New System.Drawing.Point(24, 384)
            Me.lblNotes.Name = "lblNotes"
            Me.lblNotes.Size = New System.Drawing.Size(148, 15)
            Me.lblNotes.TabIndex = 15
            Me.lblNotes.Text = "Receipt Remarks / Drawer"
            '
            'dtpPaymentDate
            '
            Me.dtpPaymentDate.CustomFormat = "yyyy-MM-dd HH:mm"
            Me.dtpPaymentDate.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.dtpPaymentDate.Location = New System.Drawing.Point(286, 342)
            Me.dtpPaymentDate.Name = "dtpPaymentDate"
            Me.dtpPaymentDate.Size = New System.Drawing.Size(248, 24)
            Me.dtpPaymentDate.TabIndex = 14
            '
            'lblDate
            '
            Me.lblDate.AutoSize = True
            Me.lblDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblDate.Location = New System.Drawing.Point(284, 322)
            Me.lblDate.Name = "lblDate"
            Me.lblDate.Size = New System.Drawing.Size(89, 15)
            Me.lblDate.TabIndex = 13
            Me.lblDate.Text = "Payment Date *"
            '
            'txtReceiptNo
            '
            Me.txtReceiptNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.txtReceiptNo.Location = New System.Drawing.Point(26, 342)
            Me.txtReceiptNo.MaxLength = 50
            Me.txtReceiptNo.Name = "txtReceiptNo"
            Me.txtReceiptNo.Size = New System.Drawing.Size(248, 24)
            Me.txtReceiptNo.TabIndex = 12
            '
            'lblReceiptNo
            '
            Me.lblReceiptNo.AutoSize = True
            Me.lblReceiptNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblReceiptNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblReceiptNo.Location = New System.Drawing.Point(24, 322)
            Me.lblReceiptNo.Name = "lblReceiptNo"
            Me.lblReceiptNo.Size = New System.Drawing.Size(100, 15)
            Me.lblReceiptNo.TabIndex = 11
            Me.lblReceiptNo.Text = "Receipt Number *"
            '
            'btnAutoRef
            '
            Me.btnAutoRef.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.btnAutoRef.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnAutoRef.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnAutoRef.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnAutoRef.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnAutoRef.Location = New System.Drawing.Point(462, 280)
            Me.btnAutoRef.Name = "btnAutoRef"
            Me.btnAutoRef.Size = New System.Drawing.Size(72, 25)
            Me.btnAutoRef.TabIndex = 10
            Me.btnAutoRef.Text = "Auto Gen"
            Me.btnAutoRef.UseVisualStyleBackColor = False
            '
            'txtTxRef
            '
            Me.txtTxRef.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.txtTxRef.Location = New System.Drawing.Point(286, 280)
            Me.txtTxRef.MaxLength = 100
            Me.txtTxRef.Name = "txtTxRef"
            Me.txtTxRef.PlaceholderText = "e.g. QKJ882190A"
            Me.txtTxRef.Size = New System.Drawing.Size(170, 24)
            Me.txtTxRef.TabIndex = 9
            '
            'lblTxRef
            '
            Me.lblTxRef.AutoSize = True
            Me.lblTxRef.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTxRef.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblTxRef.Location = New System.Drawing.Point(284, 260)
            Me.lblTxRef.Name = "lblTxRef"
            Me.lblTxRef.Size = New System.Drawing.Size(130, 15)
            Me.lblTxRef.TabIndex = 8
            Me.lblTxRef.Text = "Transaction Ref / Code *"
            '
            'cboPaymentMethod
            '
            Me.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboPaymentMethod.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboPaymentMethod.FormattingEnabled = True
            Me.cboPaymentMethod.Items.AddRange(New Object() {"MPESA", "BANK", "CASH", "OTHER"})
            Me.cboPaymentMethod.Location = New System.Drawing.Point(26, 280)
            Me.cboPaymentMethod.Name = "cboPaymentMethod"
            Me.cboPaymentMethod.Size = New System.Drawing.Size(248, 25)
            Me.cboPaymentMethod.TabIndex = 7
            '
            'lblMethod
            '
            Me.lblMethod.AutoSize = True
            Me.lblMethod.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblMethod.Location = New System.Drawing.Point(24, 260)
            Me.lblMethod.Name = "lblMethod"
            Me.lblMethod.Size = New System.Drawing.Size(107, 15)
            Me.lblMethod.TabIndex = 6
            Me.lblMethod.Text = "Payment Method *"
            '
            'btnFullBalance
            '
            Me.btnFullBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.btnFullBalance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnFullBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnFullBalance.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnFullBalance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnFullBalance.Location = New System.Drawing.Point(432, 218)
            Me.btnFullBalance.Name = "btnFullBalance"
            Me.btnFullBalance.Size = New System.Drawing.Size(102, 25)
            Me.btnFullBalance.TabIndex = 5
            Me.btnFullBalance.Text = "Pay Full Due"
            Me.btnFullBalance.UseVisualStyleBackColor = False
            '
            'numAmount
            '
            Me.numAmount.DecimalPlaces = 2
            Me.numAmount.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.numAmount.Increment = New Decimal(New Integer() {500, 0, 0, 0})
            Me.numAmount.Location = New System.Drawing.Point(26, 218)
            Me.numAmount.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
            Me.numAmount.Name = "numAmount"
            Me.numAmount.Size = New System.Drawing.Size(398, 27)
            Me.numAmount.TabIndex = 4
            Me.numAmount.ThousandsSeparator = True
            '
            'lblAmount
            '
            Me.lblAmount.AutoSize = True
            Me.lblAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblAmount.Location = New System.Drawing.Point(24, 198)
            Me.lblAmount.Name = "lblAmount"
            Me.lblAmount.Size = New System.Drawing.Size(155, 15)
            Me.lblAmount.TabIndex = 3
            Me.lblAmount.Text = "Amount Paid (KES / USD) *"
            '
            'pnlStatusBanner
            '
            Me.pnlStatusBanner.BackColor = System.Drawing.Color.White
            Me.pnlStatusBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlStatusBanner.Controls.Add(Me.lblBalDue)
            Me.pnlStatusBanner.Controls.Add(Me.lblTotalPaid)
            Me.pnlStatusBanner.Controls.Add(Me.lblTotalFee)
            Me.pnlStatusBanner.Location = New System.Drawing.Point(26, 126)
            Me.pnlStatusBanner.Name = "pnlStatusBanner"
            Me.pnlStatusBanner.Padding = New System.Windows.Forms.Padding(16, 10, 16, 10)
            Me.pnlStatusBanner.Size = New System.Drawing.Size(508, 56)
            Me.pnlStatusBanner.TabIndex = 2
            '
            'lblBalDue
            '
            Me.lblBalDue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblBalDue.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBalDue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.lblBalDue.Location = New System.Drawing.Point(320, 18)
            Me.lblBalDue.Name = "lblBalDue"
            Me.lblBalDue.Size = New System.Drawing.Size(175, 20)
            Me.lblBalDue.TabIndex = 2
            Me.lblBalDue.Text = "Due: KES 0.00"
            Me.lblBalDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblTotalPaid
            '
            Me.lblTotalPaid.AutoSize = True
            Me.lblTotalPaid.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTotalPaid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(74, Byte), Integer))
            Me.lblTotalPaid.Location = New System.Drawing.Point(165, 18)
            Me.lblTotalPaid.Name = "lblTotalPaid"
            Me.lblTotalPaid.Size = New System.Drawing.Size(107, 15)
            Me.lblTotalPaid.TabIndex = 1
            Me.lblTotalPaid.Text = "Paid: KES 0.00"
            '
            'lblTotalFee
            '
            Me.lblTotalFee.AutoSize = True
            Me.lblTotalFee.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTotalFee.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblTotalFee.Location = New System.Drawing.Point(14, 18)
            Me.lblTotalFee.Name = "lblTotalFee"
            Me.lblTotalFee.Size = New System.Drawing.Size(103, 15)
            Me.lblTotalFee.TabIndex = 0
            Me.lblTotalFee.Text = "Fee: KES 0.00"
            '
            'cboAllocation
            '
            Me.cboAllocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboAllocation.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboAllocation.FormattingEnabled = True
            Me.cboAllocation.Location = New System.Drawing.Point(26, 86)
            Me.cboAllocation.Name = "cboAllocation"
            Me.cboAllocation.Size = New System.Drawing.Size(508, 25)
            Me.cboAllocation.TabIndex = 1
            '
            'lblAllocation
            '
            Me.lblAllocation.AutoSize = True
            Me.lblAllocation.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblAllocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblAllocation.Location = New System.Drawing.Point(24, 66)
            Me.lblAllocation.Name = "lblAllocation"
            Me.lblAllocation.Size = New System.Drawing.Size(186, 15)
            Me.lblAllocation.TabIndex = 0
            Me.lblAllocation.Text = "Target Bed Space Allocation *"
            '
            'pnlFooter
            '
            Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlFooter.Controls.Add(Me.btnCancel)
            Me.pnlFooter.Controls.Add(Me.btnSaveAndPrint)
            Me.pnlFooter.Controls.Add(Me.btnSave)
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
            Me.btnCancel.Location = New System.Drawing.Point(146, 13)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(95, 34)
            Me.btnCancel.TabIndex = 2
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'btnSaveAndPrint
            '
            Me.btnSaveAndPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.btnSaveAndPrint.FlatAppearance.BorderSize = 0
            Me.btnSaveAndPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSaveAndPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSaveAndPrint.ForeColor = System.Drawing.Color.White
            Me.btnSaveAndPrint.Location = New System.Drawing.Point(247, 13)
            Me.btnSaveAndPrint.Name = "btnSaveAndPrint"
            Me.btnSaveAndPrint.Size = New System.Drawing.Size(160, 34)
            Me.btnSaveAndPrint.TabIndex = 1
            Me.btnSaveAndPrint.Text = "🖨️  Save & Preview"
            Me.btnSaveAndPrint.UseVisualStyleBackColor = False
            '
            'btnSave
            '
            Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.btnSave.FlatAppearance.BorderSize = 0
            Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSave.ForeColor = System.Drawing.Color.White
            Me.btnSave.Location = New System.Drawing.Point(413, 13)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(121, 34)
            Me.btnSave.TabIndex = 0
            Me.btnSave.Text = "Save Payment"
            Me.btnSave.UseVisualStyleBackColor = False
            '
            'frmPaymentModal
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
            Me.Name = "frmPaymentModal"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Record Student Payment"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlBody.ResumeLayout(False)
            Me.pnlBody.PerformLayout()
            CType(Me.numAmount, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlStatusBanner.ResumeLayout(False)
            Me.pnlStatusBanner.PerformLayout()
            Me.pnlFooter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGold As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblModalTitle As System.Windows.Forms.Label
        Friend WithEvents lblModalSub As System.Windows.Forms.Label
        Friend WithEvents pnlBody As System.Windows.Forms.Panel
        Friend WithEvents cboAllocation As System.Windows.Forms.ComboBox
        Friend WithEvents lblAllocation As System.Windows.Forms.Label
        Friend WithEvents pnlStatusBanner As System.Windows.Forms.Panel
        Friend WithEvents lblTotalFee As System.Windows.Forms.Label
        Friend WithEvents lblTotalPaid As System.Windows.Forms.Label
        Friend WithEvents lblBalDue As System.Windows.Forms.Label
        Friend WithEvents numAmount As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblAmount As System.Windows.Forms.Label
        Friend WithEvents btnFullBalance As System.Windows.Forms.Button
        Friend WithEvents cboPaymentMethod As System.Windows.Forms.ComboBox
        Friend WithEvents lblMethod As System.Windows.Forms.Label
        Friend WithEvents txtTxRef As System.Windows.Forms.TextBox
        Friend WithEvents lblTxRef As System.Windows.Forms.Label
        Friend WithEvents btnAutoRef As System.Windows.Forms.Button
        Friend WithEvents txtReceiptNo As System.Windows.Forms.TextBox
        Friend WithEvents lblReceiptNo As System.Windows.Forms.Label
        Friend WithEvents dtpPaymentDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblDate As System.Windows.Forms.Label
        Friend WithEvents txtNotes As System.Windows.Forms.TextBox
        Friend WithEvents lblNotes As System.Windows.Forms.Label
        Friend WithEvents pnlFooter As System.Windows.Forms.Panel
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnSaveAndPrint As System.Windows.Forms.Button
        Friend WithEvents btnSave As System.Windows.Forms.Button
    End Class
End Namespace

