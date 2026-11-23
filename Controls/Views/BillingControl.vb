Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Billing
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Financial ledger and bursar fee collection module with receipt preview and payment verification.
    ''' </summary>
    Public Class BillingControl
        Inherits UserControl

        Private ReadOnly _paymentRepo As New PaymentRepository()
        Private ReadOnly _semesterRepo As New SemesterRepository()

        Private _paymentList As New List(Of Payment)()
        Private _currentUserId As Integer = 1

        ' KPI Metrics
        Private pnlMetricsRow As TableLayoutPanel
        Private lblBilledVal As Label
        Private lblPaidVal As Label
        Private lblArrearsVal As Label
        Private lblCountVal As Label

        ' Filter Controls
        Private pnlFilterBar As Panel
        Private txtSearch As TextBox
        Private cboSemesterFilter As ComboBox
        Private cboMethodFilter As ComboBox
        Private btnNewPayment As Button
        Private btnViewReceipt As Button
        Private btnRefresh As Button

        Private dgvPayments As DataGridView

        Private Structure ComboItem
            Public Property Id As Integer
            Public Property Name As String

            Public Overrides Function ToString() As String
                Return Name
            End Function
        End Structure

        Public Sub New(Optional currentUserId As Integer = 1)
            InitializeComponents()
            _currentUserId = currentUserId
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(dgvPayments)
        End Sub

        Private Sub InitializeComponents()
            Me.Dock = DockStyle.Fill
            Me.BackColor = ThemeManager.SurfaceCanvas
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)

            ' =========================================================================
            ' TOP KPI SUMMARY ROW
            ' =========================================================================
            pnlMetricsRow = New TableLayoutPanel With {
                .Dock = DockStyle.Top,
                .Height = 88,
                .ColumnCount = 4,
                .RowCount = 1,
                .Padding = New Padding(16, 12, 16, 8)
            }
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))

            Dim cardBilled = CreateMiniCard("SEMESTER REVENUE BILLED", ThemeManager.NavyPrimary)
            lblBilledVal = CreateMiniValLabel("KES 0.00", ThemeManager.TextDeepSlate)
            cardBilled.Controls.Add(lblBilledVal)

            Dim cardPaid = CreateMiniCard("PAYMENTS COLLECTED", ThemeManager.StatusSuccess)
            lblPaidVal = CreateMiniValLabel("KES 0.00", ThemeManager.StatusSuccess)
            cardPaid.Controls.Add(lblPaidVal)

            Dim cardArrears = CreateMiniCard("OUTSTANDING ARREARS", ThemeManager.StatusDanger)
            lblArrearsVal = CreateMiniValLabel("KES 0.00", ThemeManager.StatusDanger)
            cardArrears.Controls.Add(lblArrearsVal)

            Dim cardCount = CreateMiniCard("RECEIPTS ISSUED", ThemeManager.AcademicGold)
            lblCountVal = CreateMiniValLabel("0 Confirmed", ThemeManager.AcademicGold)
            cardCount.Controls.Add(lblCountVal)

            pnlMetricsRow.Controls.Add(cardBilled, 0, 0)
            pnlMetricsRow.Controls.Add(cardPaid, 1, 0)
            pnlMetricsRow.Controls.Add(cardArrears, 2, 0)
            pnlMetricsRow.Controls.Add(cardCount, 3, 0)

            ' =========================================================================
            ' FILTER / ACTION BAR
            ' =========================================================================
            pnlFilterBar = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 56,
                .BackColor = Color.White,
                .Padding = New Padding(16, 12, 16, 12)
            }
            AddHandler pnlFilterBar.Paint, Sub(s, e)
                                               Using pen As New Pen(ThemeManager.BorderSubtle, 1.0F)
                                                   e.Graphics.DrawLine(pen, 0, pnlFilterBar.Height - 1, pnlFilterBar.Width, pnlFilterBar.Height - 1)
                                               End Using
                                           End Sub

            txtSearch = New TextBox With {
                .Location = New Point(16, 14),
                .Size = New Size(220, 26),
                .PlaceholderText = "🔍  Search Receipt, Student, Ref...",
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
            }
            AddHandler txtSearch.TextChanged, Sub() ApplyFilters()

            Dim lblSem As New Label With {
                .Text = "Term:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(245, 18),
                .AutoSize = True
            }

            cboSemesterFilter = New ComboBox With {
                .Location = New Point(285, 15),
                .Size = New Size(160, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            AddHandler cboSemesterFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblMethod As New Label With {
                .Text = "Method:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(455, 18),
                .AutoSize = True
            }

            cboMethodFilter = New ComboBox With {
                .Location = New Point(510, 15),
                .Size = New Size(130, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboMethodFilter.Items.AddRange(New Object() {"ALL METHODS", "MPESA", "BANK", "CASH", "OTHER"})
            cboMethodFilter.SelectedIndex = 0
            AddHandler cboMethodFilter.SelectedIndexChanged, Sub() ApplyFilters()

            btnNewPayment = New Button With {
                .Text = "＋ Record Payment",
                .Size = New Size(150, 30),
                .Location = New Point(pnlFilterBar.Width - 340, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = ThemeManager.NavyDark,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnNewPayment.FlatAppearance.BorderSize = 0
            AddHandler btnNewPayment.Click, AddressOf BtnNewPayment_Click

            btnViewReceipt = New Button With {
                .Text = "🖨️  View Receipt",
                .Size = New Size(130, 30),
                .Location = New Point(pnlFilterBar.Width - 180, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.TextDeepSlate,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnViewReceipt.FlatAppearance.BorderColor = ThemeManager.BorderSubtle
            AddHandler btnViewReceipt.Click, AddressOf BtnViewReceipt_Click

            btnRefresh = New Button With {
                .Text = "↻",
                .Size = New Size(34, 30),
                .Location = New Point(pnlFilterBar.Width - 40, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.TextDeepSlate,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnRefresh.FlatAppearance.BorderColor = ThemeManager.BorderSubtle
            AddHandler btnRefresh.Click, Sub() LoadBillingData()

            pnlFilterBar.Controls.AddRange({txtSearch, lblSem, cboSemesterFilter, lblMethod, cboMethodFilter, btnNewPayment, btnViewReceipt, btnRefresh})

            ' =========================================================================
            ' PAYMENTS DATA GRID
            ' =========================================================================
            dgvPayments = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyPrestigeGridStyle(dgvPayments)
            SetupGridColumns()

            AddHandler dgvPayments.CellDoubleClick, Sub(s, e)
                                                        If e.RowIndex >= 0 Then
                                                            BtnViewReceipt_Click(s, EventArgs.Empty)
                                                        End If
                                                    End Sub

            Dim pnlGridContainer As New Panel With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16)
            }
            pnlGridContainer.Controls.Add(dgvPayments)

            Me.Controls.Add(pnlGridContainer)
            Me.Controls.Add(pnlFilterBar)
            Me.Controls.Add(pnlMetricsRow)
        End Sub

        Private Function CreateMiniCard(title As String, accentColor As Color) As Panel
            Dim pnl As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White,
                .Margin = New Padding(4, 0, 4, 0),
                .Padding = New Padding(12, 8, 12, 8)
            }
            Dim topBar As New Panel With {.Dock = DockStyle.Top, .Height = 3, .BackColor = accentColor}
            Dim lblTitle As New Label With {
                .Text = title,
                .Font = New Font("Segoe UI Semibold", 7.5!, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateMuted,
                .Dock = DockStyle.Top,
                .Height = 16,
                .UseMnemonic = False
            }
            pnl.Controls.Add(lblTitle)
            pnl.Controls.Add(topBar)
            AddHandler pnl.Paint, Sub(s, e)
                                      Using pen As New Pen(ThemeManager.BorderSubtle, 1.0F)
                                          e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1)
                                      End Using
                                  End Sub
            Return pnl
        End Function

        Private Function CreateMiniValLabel(text As String, foreColor As Color) As Label
            Return New Label With {
                .Text = text,
                .Font = New Font("Segoe UI", 12.5!, FontStyle.Bold),
                .ForeColor = foreColor,
                .Location = New Point(10, 26),
                .Size = New Size(200, 24),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right,
                .UseMnemonic = False
            }
        End Function

        Private Sub SetupGridColumns()
            dgvPayments.Columns.Clear()
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "PaymentId", .HeaderText = "ID", .Visible = False})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "ReceiptNo", .HeaderText = "Receipt #", .Width = 140})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Date", .HeaderText = "Date & Time", .Width = 135})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "StudentName", .HeaderText = "Student Resident", .Width = 170, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RegNo", .HeaderText = "Reg. Number", .Width = 120})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BedSpace", .HeaderText = "Allocated Space", .Width = 170})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Method", .HeaderText = "Method", .Width = 85})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TxRef", .HeaderText = "Transaction Reference", .Width = 140})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Amount", .HeaderText = "Amount Paid", .Width = 120, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvPayments.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Status", .HeaderText = "Status", .Width = 95})
        End Sub

        Public Sub LoadBillingData()
            Try
                ' Load Semesters
                If cboSemesterFilter.Items.Count = 0 Then
                    cboSemesterFilter.Items.Add(New ComboItem With {.Id = 0, .Name = "ALL TERMS"})
                    Dim semesters = _semesterRepo.GetSemesters()
                    Dim activeIdx = 0
                    For i As Integer = 0 To semesters.Count - 1
                        Dim s = semesters(i)
                        cboSemesterFilter.Items.Add(New ComboItem With {.Id = s.SemesterId, .Name = s.SemesterName})
                        If s.IsCurrent Then activeIdx = i + 1
                    Next
                    cboSemesterFilter.SelectedIndex = activeIdx
                End If

                Dim selectedSemId As Integer? = Nothing
                If cboSemesterFilter.SelectedItem IsNot Nothing Then
                    Dim item = DirectCast(cboSemesterFilter.SelectedItem, ComboItem)
                    If item.Id > 0 Then selectedSemId = item.Id
                End If

                ' Update KPI Summary
                Dim fin = _paymentRepo.GetFinancialSummary(selectedSemId)
                lblBilledVal.Text = $"KES {fin.TotalBilled:N2}"
                lblPaidVal.Text = $"KES {fin.TotalPaid:N2}"
                lblArrearsVal.Text = $"KES {fin.TotalArrears:N2}"
                lblCountVal.Text = $"{fin.ConfirmedCount} Receipts"

                _paymentList = _paymentRepo.GetPayments(semesterId:=selectedSemId)
                ApplyFilters()
            Catch ex As Exception
                MessageBox.Show($"Failed to load financial records: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ApplyFilters()
            Dim search = txtSearch.Text.Trim().ToLowerInvariant()
            Dim method = If(cboMethodFilter.SelectedIndex > 0, cboMethodFilter.SelectedItem.ToString(), "ALL")

            dgvPayments.Rows.Clear()
            For Each p In _paymentList
                If method <> "ALL" AndAlso Not String.Equals(p.PaymentMethod, method, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If Not String.IsNullOrEmpty(search) Then
                    Dim matches = p.ReceiptNumber.ToLowerInvariant().Contains(search) OrElse
                                  p.TransactionReference.ToLowerInvariant().Contains(search) OrElse
                                  p.StudentName.ToLowerInvariant().Contains(search) OrElse
                                  p.RegistrationNumber.ToLowerInvariant().Contains(search)
                    If Not matches Then Continue For
                End If

                Dim rowIdx = dgvPayments.Rows.Add(
                    p.PaymentId,
                    p.ReceiptNumber,
                    p.PaymentDate.ToString("yyyy-MM-dd HH:mm"),
                    p.StudentName,
                    p.RegistrationNumber,
                    p.BedIdentifier,
                    p.PaymentMethod,
                    p.TransactionReference,
                    $"KES {p.AmountPaid:N2}",
                    p.PaymentStatus
                )

                dgvPayments.Rows(rowIdx).Cells("Amount").Style.ForeColor = ThemeManager.StatusSuccess
                If p.PaymentStatus = "CONFIRMED" Then
                    dgvPayments.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.NavyPrimary
                ElseIf p.PaymentStatus = "REVERSED" Then
                    dgvPayments.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.StatusDanger
                End If
            Next
        End Sub

        Public Sub OpenNewPaymentModal(Optional preselectedAllocId As Integer? = Nothing)
            Using modal As New frmPaymentModal(preselectedAllocId, _currentUserId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadBillingData()
                End If
            End Using
        End Sub

        Private Sub BtnNewPayment_Click(sender As Object, e As EventArgs)
            OpenNewPaymentModal()
        End Sub

        Private Sub BtnViewReceipt_Click(sender As Object, e As EventArgs)
            If dgvPayments.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a payment record from the table to preview its electronic receipt.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim paymentId = Convert.ToInt32(dgvPayments.SelectedRows(0).Cells("PaymentId").Value)
            Using modal As New frmReceiptPreviewModal(paymentId)
                modal.ShowDialog(Me.FindForm())
            End Using
        End Sub
    End Class
End Namespace

