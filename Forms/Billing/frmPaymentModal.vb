Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Billing
    ''' <summary>
    ''' Modal form for recording student accommodation payments with automated balance calculation and receipt issuance.
    ''' </summary>
    Public Class frmPaymentModal
        Private ReadOnly _paymentRepo As New PaymentRepository()
        Private ReadOnly _allocRepo As New AllocationRepository()
        Private ReadOnly _userId As Integer
        Private _selectedAlloc As BedAllocation
        Private _isLoading As Boolean = False

        Public Property CreatedPaymentId As Integer = 0
        Public Property CreatedReceiptNumber As String = String.Empty

        Private Structure AllocItem
            Public Property Id As Integer
            Public Property DisplayText As String
            Public Property Allocation As BedAllocation

            Public Overrides Function ToString() As String
                Return DisplayText
            End Function
        End Structure

        Public Sub New(Optional preselectedAllocId As Integer? = Nothing, Optional currentUserId As Integer = 1)
            InitializeComponent()
            _userId = currentUserId

            cboPaymentMethod.SelectedIndex = 0
            dtpPaymentDate.Value = DateTime.Now
            txtReceiptNo.Text = _paymentRepo.GenerateReceiptNumber()

            LoadAllocations(preselectedAllocId)
        End Sub

        Private Sub LoadAllocations(Optional preselectedId As Integer? = Nothing)
            _isLoading = True
            cboAllocation.Items.Clear()
            Dim allocs = _allocRepo.GetAllocations(status:="ACTIVE")

            Dim selectedIdx As Integer = 0
            For i As Integer = 0 To allocs.Count - 1
                Dim a = allocs(i)
                Dim item As New AllocItem With {
                    .Id = a.AllocationId,
                    .DisplayText = $"{a.RegistrationNumber} • {a.StudentName} — {a.BedIdentifier} [Due: KES {a.BalanceOwed:N2}]",
                    .Allocation = a
                }
                cboAllocation.Items.Add(item)
                If preselectedId.HasValue AndAlso a.AllocationId = preselectedId.Value Then
                    selectedIdx = i
                End If
            Next

            If cboAllocation.Items.Count > 0 Then
                cboAllocation.SelectedIndex = selectedIdx
                Dim firstItem = DirectCast(cboAllocation.Items(selectedIdx), AllocItem)
                SelectAllocation(firstItem.Allocation)
            Else
                cboAllocation.Items.Add(New AllocItem With {.Id = 0, .DisplayText = "No active bed space allocations found"})
                cboAllocation.SelectedIndex = 0
                cboAllocation.Enabled = False
            End If
            _isLoading = False
        End Sub

        Private Sub SelectAllocation(a As BedAllocation)
            _selectedAlloc = a
            If a Is Nothing Then Return

            lblTotalFee.Text = $"Fee: KES {a.SemesterFee:N2}"
            lblTotalPaid.Text = $"Paid: KES {a.TotalPaid:N2}"
            lblBalDue.Text = $"Due: KES {a.BalanceOwed:N2}"

            ' Default amount to remaining balance if > 0
            numAmount.Value = Math.Max(100.0D, a.BalanceOwed)
            GenerateDefaultTxRef()
        End Sub

        Private Sub GenerateDefaultTxRef()
            Dim method = If(cboPaymentMethod.SelectedItem IsNot Nothing, cboPaymentMethod.SelectedItem.ToString(), "MPESA")
            Dim rnd As New Random()
            Dim chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789"
            Dim code(7) As Char
            For i As Integer = 0 To 7
                code(i) = chars(rnd.Next(chars.Length))
            Next

            Select Case method
                Case "MPESA"
                    txtTxRef.Text = $"Q{New String(code)}"
                Case "BANK"
                    txtTxRef.Text = $"BNK-{DateTime.UtcNow:MMdd}-{New String(code).Substring(0, 4)}"
                Case "CASH"
                    txtTxRef.Text = $"CSH-{DateTime.UtcNow:yyMMddHHmmss}"
                Case Else
                    txtTxRef.Text = $"REF-{DateTime.UtcNow:MMddHHmm}"
            End Select
        End Sub

        Private Sub cboAllocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAllocation.SelectedIndexChanged
            If _isLoading Then Return
            If cboAllocation.SelectedItem IsNot Nothing Then
                Dim item = DirectCast(cboAllocation.SelectedItem, AllocItem)
                If item.Id > 0 Then
                    SelectAllocation(item.Allocation)
                End If
            End If
        End Sub

        Private Sub cboPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPaymentMethod.SelectedIndexChanged
            If String.IsNullOrWhiteSpace(txtTxRef.Text) OrElse txtTxRef.Text.StartsWith("Q") OrElse txtTxRef.Text.StartsWith("BNK") OrElse txtTxRef.Text.StartsWith("CSH") OrElse txtTxRef.Text.StartsWith("REF") Then
                GenerateDefaultTxRef()
            End If
        End Sub

        Private Sub btnAutoRef_Click(sender As Object, e As EventArgs) Handles btnAutoRef.Click
            GenerateDefaultTxRef()
        End Sub

        Private Sub btnFullBalance_Click(sender As Object, e As EventArgs) Handles btnFullBalance.Click
            If _selectedAlloc IsNot Nothing Then
                numAmount.Value = Math.Max(0.0D, _selectedAlloc.BalanceOwed)
            End If
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            ProcessPayment(previewReceipt:=False)
        End Sub

        Private Sub btnSaveAndPrint_Click(sender As Object, e As EventArgs) Handles btnSaveAndPrint.Click
            ProcessPayment(previewReceipt:=True)
        End Sub

        Private Sub ProcessPayment(previewReceipt As Boolean)
            If _selectedAlloc Is Nothing OrElse _selectedAlloc.AllocationId <= 0 Then
                MessageBox.Show("Please select an active allocation to record payment for.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim amount = numAmount.Value
            If amount <= 0 Then
                MessageBox.Show("Payment amount must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                numAmount.Focus()
                Return
            End If

            Dim txRef = txtTxRef.Text.Trim().ToUpperInvariant()
            If String.IsNullOrWhiteSpace(txRef) Then
                MessageBox.Show("Please enter a transaction reference or payment verification code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtTxRef.Focus()
                Return
            End If

            Dim method = If(cboPaymentMethod.SelectedItem IsNot Nothing, cboPaymentMethod.SelectedItem.ToString(), "MPESA")
            Dim receiptNo = txtReceiptNo.Text.Trim().ToUpperInvariant()

            Dim result = _paymentRepo.RecordPayment(_selectedAlloc.AllocationId, amount, method, txRef, txtNotes.Text.Trim(), _userId, receiptNo)
            If result.Success Then
                CreatedPaymentId = result.PaymentId
                CreatedReceiptNumber = result.ReceiptNumber

                If previewReceipt Then
                    Using preview As New frmReceiptPreviewModal(result.PaymentId)
                        preview.ShowDialog(Me.FindForm())
                    End Using
                Else
                    MessageBox.Show(
                        $"Payment recorded successfully!{Environment.NewLine}{Environment.NewLine}" &
                        $"Receipt Number: {result.ReceiptNumber}{Environment.NewLine}" &
                        $"Amount Paid: KES {amount:N2} via {method}{Environment.NewLine}" &
                        $"Transaction Ref: {txRef}",
                        "Payment Receipted",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                End If

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show(result.Message, "Payment Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace
