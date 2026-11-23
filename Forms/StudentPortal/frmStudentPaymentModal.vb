Imports System
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Forms.StudentPortal
    Public Class frmStudentPaymentModal
        Private ReadOnly _allocationId As Integer
        Private ReadOnly _balanceOwed As Decimal
        Private ReadOnly _studentUser As User
        Private ReadOnly _portalService As New StudentPortalService()
        Private _isDragging As Boolean = False
        Private _dragStartPoint As Point

        Public Property GeneratedReceiptNumber As String = String.Empty

        Public Sub New(allocationId As Integer, balanceOwed As Decimal, studentUser As User)
            InitializeComponent()
            _allocationId = allocationId
            _balanceOwed = balanceOwed
            _studentUser = studentUser
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlRoot)
            ThemeManager.EnableDoubleBuffering(pnlBalanceCard)

            lblBalanceAmount.Text = $"KES {_balanceOwed:N2}"
            txtAmount.TextValue = If(_balanceOwed > 0, _balanceOwed.ToString("F2"), "1000.00")
            If cboPaymentMethod.Items.Count > 0 Then cboPaymentMethod.SelectedIndex = 0
            txtTxRef.TextValue = $"MPESA-{DateTime.Now:HHmmss}-{New Random().Next(100, 999)}"

            txtAmount.Select()
        End Sub

        Private Sub pnlHeader_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseDown, lblHeaderTitle.MouseDown
            If e.Button = MouseButtons.Left Then
                _isDragging = True
                _dragStartPoint = New Point(e.X, e.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseMove, lblHeaderTitle.MouseMove
            If _isDragging Then
                Dim screenPos = PointToScreen(e.Location)
                Me.Location = New Point(screenPos.X - _dragStartPoint.X, screenPos.Y - _dragStartPoint.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseUp, lblHeaderTitle.MouseUp
            _isDragging = False
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Async Sub btnSubmitPayment_Click(sender As Object, e As EventArgs) Handles btnSubmitPayment.Click
            Dim amountText = txtAmount.TextValue.Trim()
            Dim txRef = txtTxRef.TextValue.Trim()
            Dim method = If(cboPaymentMethod.SelectedItem IsNot Nothing, cboPaymentMethod.SelectedItem.ToString(), "MPESA")
            Dim amount As Decimal = 0

            If Not Decimal.TryParse(amountText, amount) OrElse amount <= 0 Then
                lblStatusMessage.ForeColor = ThemeManager.Coral
                lblStatusMessage.Text = "Please enter a valid payment amount greater than zero."
                txtAmount.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(txRef) Then
                lblStatusMessage.ForeColor = ThemeManager.Coral
                lblStatusMessage.Text = "Please enter the M-Pesa transaction reference or bank slip code."
                txtTxRef.Focus()
                Return
            End If

            btnSubmitPayment.Enabled = False
            btnSubmitPayment.Text = "Processing Payment..."
            lblStatusMessage.ForeColor = ThemeManager.TextSecondary
            lblStatusMessage.Text = "Verifying transaction and generating receipt..."

            Dim studentId = _studentUser.StudentId.GetValueOrDefault(0)
            Dim result = Await _portalService.ProcessPaymentAsync(_allocationId, studentId, amount, method, txRef, _studentUser.UserId)

            btnSubmitPayment.Enabled = True
            btnSubmitPayment.Text = "Process Payment & Generate Receipt"

            If Not result.Success Then
                lblStatusMessage.ForeColor = ThemeManager.Coral
                lblStatusMessage.Text = result.Message
                Return
            End If

            GeneratedReceiptNumber = result.ReceiptNumber
            lblStatusMessage.ForeColor = ThemeManager.EmeraldPrimary
            lblStatusMessage.Text = result.Message
            Await Task.Delay(400)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub
    End Class
End Namespace

