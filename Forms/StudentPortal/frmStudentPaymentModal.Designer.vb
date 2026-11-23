Namespace Forms.StudentPortal
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmStudentPaymentModal
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
            Me.btnSubmitPayment = New University_hostel_mgt.Controls.GlassButton()
            Me.lblStatusMessage = New System.Windows.Forms.Label()
            Me.txtTxRef = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblTxRef = New System.Windows.Forms.Label()
            Me.txtAmount = New University_hostel_mgt.Controls.GlassTextBox()
            Me.lblAmount = New System.Windows.Forms.Label()
            Me.cboPaymentMethod = New System.Windows.Forms.ComboBox()
            Me.lblPaymentMethod = New System.Windows.Forms.Label()
            Me.pnlBalanceCard = New University_hostel_mgt.Controls.GlassPanel()
            Me.lblBalanceAmount = New System.Windows.Forms.Label()
            Me.lblBalanceTitle = New System.Windows.Forms.Label()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlRoot.SuspendLayout()
            Me.pnlBalanceCard.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(500, 3)
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
            Me.pnlHeader.Size = New System.Drawing.Size(500, 38)
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
            Me.btnClose.Location = New System.Drawing.Point(460, 4)
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
            Me.lblHeaderTitle.Size = New System.Drawing.Size(251, 15)
            Me.lblHeaderTitle.TabIndex = 0
            Me.lblHeaderTitle.Text = "Accommodation Fee Payment • Bursar Gateway"
            '
            'pnlRoot
            '
            Me.pnlRoot.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlRoot.BorderWidth = 1.0!
            Me.pnlRoot.Controls.Add(Me.btnCancel)
            Me.pnlRoot.Controls.Add(Me.btnSubmitPayment)
            Me.pnlRoot.Controls.Add(Me.lblStatusMessage)
            Me.pnlRoot.Controls.Add(Me.txtTxRef)
            Me.pnlRoot.Controls.Add(Me.lblTxRef)
            Me.pnlRoot.Controls.Add(Me.txtAmount)
            Me.pnlRoot.Controls.Add(Me.lblAmount)
            Me.pnlRoot.Controls.Add(Me.cboPaymentMethod)
            Me.pnlRoot.Controls.Add(Me.lblPaymentMethod)
            Me.pnlRoot.Controls.Add(Me.pnlBalanceCard)
            Me.pnlRoot.Controls.Add(Me.lblSubtitle)
            Me.pnlRoot.Controls.Add(Me.lblTitle)
            Me.pnlRoot.CornerRadius = 18
            Me.pnlRoot.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlRoot.FillColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(31, Byte), Integer))
            Me.pnlRoot.GlowColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlRoot.Location = New System.Drawing.Point(0, 41)
            Me.pnlRoot.Name = "pnlRoot"
            Me.pnlRoot.Padding = New System.Windows.Forms.Padding(24)
            Me.pnlRoot.Size = New System.Drawing.Size(500, 489)
            Me.pnlRoot.TabIndex = 2
            '
            'btnCancel
            '
            Me.btnCancel.CornerRadius = 12
            Me.btnCancel.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.HoverColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnCancel.Location = New System.Drawing.Point(24, 426)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnCancel.Size = New System.Drawing.Size(452, 36)
            Me.btnCancel.TabIndex = 11
            Me.btnCancel.Text = "Cancel"
            '
            'btnSubmitPayment
            '
            Me.btnSubmitPayment.CornerRadius = 12
            Me.btnSubmitPayment.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnSubmitPayment.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSubmitPayment.HoverColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
            Me.btnSubmitPayment.Location = New System.Drawing.Point(24, 376)
            Me.btnSubmitPayment.Name = "btnSubmitPayment"
            Me.btnSubmitPayment.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnSubmitPayment.Size = New System.Drawing.Size(452, 42)
            Me.btnSubmitPayment.TabIndex = 10
            Me.btnSubmitPayment.Text = "Process Payment & Generate Receipt"
            '
            'lblStatusMessage
            '
            Me.lblStatusMessage.BackColor = System.Drawing.Color.Transparent
            Me.lblStatusMessage.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.lblStatusMessage.Location = New System.Drawing.Point(24, 344)
            Me.lblStatusMessage.Name = "lblStatusMessage"
            Me.lblStatusMessage.Size = New System.Drawing.Size(452, 24)
            Me.lblStatusMessage.TabIndex = 9
            Me.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'txtTxRef
            '
            Me.txtTxRef.Location = New System.Drawing.Point(24, 298)
            Me.txtTxRef.Name = "txtTxRef"
            Me.txtTxRef.PlaceholderText = "e.g. QK839D83J or Bank Deposit Slip #"
            Me.txtTxRef.Size = New System.Drawing.Size(452, 36)
            Me.txtTxRef.TabIndex = 8
            '
            'lblTxRef
            '
            Me.lblTxRef.AutoSize = True
            Me.lblTxRef.BackColor = System.Drawing.Color.Transparent
            Me.lblTxRef.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTxRef.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblTxRef.Location = New System.Drawing.Point(24, 278)
            Me.lblTxRef.Name = "lblTxRef"
            Me.lblTxRef.Size = New System.Drawing.Size(200, 15)
            Me.lblTxRef.TabIndex = 7
            Me.lblTxRef.Text = "Transaction Reference / M-Pesa Code *"
            '
            'txtAmount
            '
            Me.txtAmount.Location = New System.Drawing.Point(24, 228)
            Me.txtAmount.Name = "txtAmount"
            Me.txtAmount.PlaceholderText = "0.00"
            Me.txtAmount.Size = New System.Drawing.Size(452, 36)
            Me.txtAmount.TabIndex = 6
            '
            'lblAmount
            '
            Me.lblAmount.AutoSize = True
            Me.lblAmount.BackColor = System.Drawing.Color.Transparent
            Me.lblAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblAmount.Location = New System.Drawing.Point(24, 208)
            Me.lblAmount.Name = "lblAmount"
            Me.lblAmount.Size = New System.Drawing.Size(130, 15)
            Me.lblAmount.TabIndex = 5
            Me.lblAmount.Text = "Amount to Pay (KES) *"
            '
            'cboPaymentMethod
            '
            Me.cboPaymentMethod.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(35, Byte), Integer))
            Me.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboPaymentMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cboPaymentMethod.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.cboPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.cboPaymentMethod.FormattingEnabled = True
            Me.cboPaymentMethod.Items.AddRange(New Object() {"MPESA", "BANK", "CASH"})
            Me.cboPaymentMethod.Location = New System.Drawing.Point(24, 168)
            Me.cboPaymentMethod.Name = "cboPaymentMethod"
            Me.cboPaymentMethod.Size = New System.Drawing.Size(452, 25)
            Me.cboPaymentMethod.TabIndex = 4
            '
            'lblPaymentMethod
            '
            Me.lblPaymentMethod.AutoSize = True
            Me.lblPaymentMethod.BackColor = System.Drawing.Color.Transparent
            Me.lblPaymentMethod.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblPaymentMethod.Location = New System.Drawing.Point(24, 148)
            Me.lblPaymentMethod.Name = "lblPaymentMethod"
            Me.lblPaymentMethod.Size = New System.Drawing.Size(107, 15)
            Me.lblPaymentMethod.TabIndex = 3
            Me.lblPaymentMethod.Text = "Payment Channel *"
            '
            'pnlBalanceCard
            '
            Me.pnlBalanceCard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlBalanceCard.BorderWidth = 1.0!
            Me.pnlBalanceCard.Controls.Add(Me.lblBalanceAmount)
            Me.pnlBalanceCard.Controls.Add(Me.lblBalanceTitle)
            Me.pnlBalanceCard.CornerRadius = 12
            Me.pnlBalanceCard.FillColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
            Me.pnlBalanceCard.GlowColor = System.Drawing.Color.Transparent
            Me.pnlBalanceCard.Location = New System.Drawing.Point(24, 76)
            Me.pnlBalanceCard.Name = "pnlBalanceCard"
            Me.pnlBalanceCard.Padding = New System.Windows.Forms.Padding(16, 12, 16, 12)
            Me.pnlBalanceCard.Size = New System.Drawing.Size(452, 58)
            Me.pnlBalanceCard.TabIndex = 2
            '
            'lblBalanceAmount
            '
            Me.lblBalanceAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblBalanceAmount.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblBalanceAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.lblBalanceAmount.Location = New System.Drawing.Point(232, 16)
            Me.lblBalanceAmount.Name = "lblBalanceAmount"
            Me.lblBalanceAmount.Size = New System.Drawing.Size(204, 26)
            Me.lblBalanceAmount.TabIndex = 1
            Me.lblBalanceAmount.Text = "KES 0.00"
            Me.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblBalanceTitle
            '
            Me.lblBalanceTitle.AutoSize = True
            Me.lblBalanceTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblBalanceTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblBalanceTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblBalanceTitle.Location = New System.Drawing.Point(16, 20)
            Me.lblBalanceTitle.Name = "lblBalanceTitle"
            Me.lblBalanceTitle.Size = New System.Drawing.Size(148, 15)
            Me.lblBalanceTitle.TabIndex = 0
            Me.lblBalanceTitle.Text = "Current Outstanding Balance:"
            '
            'lblSubtitle
            '
            Me.lblSubtitle.AutoSize = True
            Me.lblSubtitle.BackColor = System.Drawing.Color.Transparent
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(24, 46)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(288, 15)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Official accommodation bursar settlement gateway"
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblTitle.Location = New System.Drawing.Point(24, 20)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(271, 25)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "Pay Accommodation Fees"
            '
            'frmStudentPaymentModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(500, 530)
            Me.Controls.Add(Me.pnlRoot)
            Me.Controls.Add(Me.pnlHeader)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmStudentPaymentModal"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Pay Accommodation Fees"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlRoot.ResumeLayout(False)
            Me.pnlRoot.PerformLayout()
            Me.pnlBalanceCard.ResumeLayout(False)
            Me.pnlBalanceCard.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
        Friend WithEvents pnlRoot As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents pnlBalanceCard As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblBalanceTitle As System.Windows.Forms.Label
        Friend WithEvents lblBalanceAmount As System.Windows.Forms.Label
        Friend WithEvents lblPaymentMethod As System.Windows.Forms.Label
        Friend WithEvents cboPaymentMethod As System.Windows.Forms.ComboBox
        Friend WithEvents lblAmount As System.Windows.Forms.Label
        Friend WithEvents txtAmount As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblTxRef As System.Windows.Forms.Label
        Friend WithEvents txtTxRef As University_hostel_mgt.Controls.GlassTextBox
        Friend WithEvents lblStatusMessage As System.Windows.Forms.Label
        Friend WithEvents btnSubmitPayment As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnCancel As University_hostel_mgt.Controls.GlassButton
    End Class
End Namespace

