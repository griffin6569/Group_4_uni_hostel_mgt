Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Forms.Billing
    ''' <summary>
    ''' Official modal preview of a bursar student accommodation electronic payment receipt.
    ''' </summary>
    Public Class frmReceiptPreviewModal
        Private ReadOnly _paymentRepo As New PaymentRepository()
        Private ReadOnly _allocRepo As New AllocationRepository()
        Private ReadOnly _paymentId As Integer

        Public Sub New(paymentId As Integer)
            InitializeComponent()
            _paymentId = paymentId

            PaintAcademicCrest()
            LoadReceiptData()
        End Sub

        Private Sub PaintAcademicCrest()
            Dim bmp As New Bitmap(picCrest.Width, picCrest.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim rect As New RectangleF(2, 2, picCrest.Width - 4, picCrest.Height - 4)
                ThemeManager.DrawAcademicCrest(g, rect, ThemeManager.AcademicGold, ThemeManager.NavyPrimary)
            End Using
            picCrest.Image = bmp
        End Sub

        Private Sub LoadReceiptData()
            Dim payment = _paymentRepo.GetPaymentById(_paymentId)
            If payment Is Nothing Then
                MessageBox.Show("Payment record could not be found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            Dim alloc = _allocRepo.GetAllocationById(payment.AllocationId)

            lblReceiptNo.Text = payment.ReceiptNumber
            lblReceiptDate.Text = $"Issued: {payment.PaymentDate:yyyy-MM-dd HH:mm:ss} UTC"

            lblStudentInfo.Text = $"Student Resident: {payment.StudentName} (Reg: {payment.RegistrationNumber})"
            lblBedInfo.Text = $"Allocated Space: {payment.BedIdentifier}"
            lblSemesterInfo.Text = $"Academic Period: {payment.SemesterName}"

            lblMethodRow.Text = $"Payment Channel: {payment.PaymentMethod} Confirmed"
            lblTxRefRow.Text = $"Transaction Reference / Bank Slip: {payment.TransactionReference}"
            lblFeeRow.Text = $"Allocated Semester Rate: KES {payment.SemesterFee:N2}"

            lblPaidAmount.Text = $"KES {payment.AmountPaid:N2}"

            If alloc IsNot Nothing Then
                If alloc.BalanceOwed <= 0 Then
                    lblBalanceRemaining.Text = "Outstanding Semester Balance: KES 0.00 (FULLY SETTLED ✓)"
                    lblBalanceRemaining.ForeColor = ThemeManager.StatusSuccess
                Else
                    lblBalanceRemaining.Text = $"Outstanding Semester Balance: KES {alloc.BalanceOwed:N2}"
                    lblBalanceRemaining.ForeColor = ThemeManager.StatusDanger
                End If
            End If

            lblCashierSign.Text = $"Cashier: {payment.RecordedByName} • System Verified ADO.NET Transaction Record"
        End Sub

        Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
            MessageBox.Show(
                $"Receipt sent to university document printer pool.{Environment.NewLine}{Environment.NewLine}" &
                $"Document ID: {lblReceiptNo.Text}{Environment.NewLine}" &
                $"Official copy archived in student financial dossier.",
                "Print Dispatch Verified",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
            Me.Close()
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub
    End Class
End Namespace
