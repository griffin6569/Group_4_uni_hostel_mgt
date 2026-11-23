Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Controls
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Billing
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Bursar Financial Ledger & Student Billing Control.
    ''' Manages accommodation fee collections, payment methods, transaction references,
    ''' receipt issuance, and student arrears tracking.
    ''' Built strictly with Option B: Frosted Glassmorphism & Acrylic design system.
    ''' </summary>
    Public Class BillingControl
        Inherits UserControl

        Private ReadOnly _paymentRepo As New PaymentRepository()
        Private ReadOnly _semesterRepo As New SemesterRepository()
        Private _currentUserId As Integer = 1
        Private _paymentList As New List(Of Payment)()

        ' UI Components
        Private pnlMetricsRow As TableLayoutPanel
        Private kpiBilled As GlassKpiWidget
        Private kpiPaid As GlassKpiWidget
        Private kpiArrears As GlassKpiWidget
        Private kpiReceipts As GlassKpiWidget

        Private pnlFilterBar As PrestigeGlassCard
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
            Me.BackColor = ThemeManager.CanvasDark
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)

            ' =========================================================================
            ' TOP KPI SUMMARY ROW (4 Frosted Glass Widgets)
            ' =========================================================================
            pnlMetricsRow = New TableLayoutPanel With {
                .Dock = DockStyle.Top,
                .Height = 100,
                .ColumnCount = 4,
                .RowCount = 1,
                .BackColor = ThemeManager.CanvasDark,
                .Padding = New Padding(16, 12, 16, 8)
            }
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))

            kpiBilled = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(0, 0, 6, 0),
                .CategoryTitle = "SEMESTER REVENUE BILLED",
                .PrimaryValue = "KES 0.00",
                .Subtitle = "Invoiced resident fees",
                .AccentColor = ThemeManager.GlassStatusInfo,
                .IconSymbol = "📋"
            }

            kpiPaid = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4, 0, 4, 0),
                .CategoryTitle = "PAYMENTS COLLECTED",
                .PrimaryValue = "KES 0.00",
                .Subtitle = "Cleared student revenue",
                .AccentColor = ThemeManager.GlassStatusSuccess,
                .ValueColor = ThemeManager.GlassStatusSuccess,
                .IconSymbol = "💰"
            }

            kpiArrears = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4, 0, 4, 0),
                .CategoryTitle = "OUTSTANDING ARREARS",
                .PrimaryValue = "KES 0.00",
                .Subtitle = "Unpaid resident balances",
                .AccentColor = ThemeManager.GlassStatusDanger,
                .ValueColor = ThemeManager.GlassStatusDanger,
                .IconSymbol = "⚠️"
            }

            kpiReceipts = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(6, 0, 0, 0),
                .CategoryTitle = "RECEIPTS ISSUED",
                .PrimaryValue = "0",
                .Subtitle = "Cleared audit transactions",
                .AccentColor = ThemeManager.GlassStatusWarning,
                .ValueColor = ThemeManager.GlassStatusWarning,
                .IconSymbol = "🧾"
            }

            pnlMetricsRow.Controls.Add(kpiBilled, 0, 0)
            pnlMetricsRow.Controls.Add(kpiPaid, 1, 0)
            pnlMetricsRow.Controls.Add(kpiArrears, 2, 0)
            pnlMetricsRow.Controls.Add(kpiReceipts, 3, 0)

            ' =========================================================================
            ' FILTER / ACTION BAR (Frosted Glass Card)
            ' =========================================================================
            pnlFilterBar = New PrestigeGlassCard With {
                .Dock = DockStyle.Top,
                .Height = 58,
                .Margin = New Padding(16, 0, 16, 6),
                .Padding = New Padding(16, 10, 16, 10),
                .AccentColor = ThemeManager.GlassBorderFocus,
                .AccentHeight = 2
            }

            Dim txtSearchControl As New GlassSearchBox With {
                .Location = New Point(16, 11),
                .Size = New Size(220, 34),
                .Placeholder = "Search Receipt, Resident, Ref..."
            }
            AddHandler txtSearchControl.SearchTextChanged, Sub()
                                                               txtSearch.Text = txtSearchControl.QueryText
                                                               ApplyFilters()
                                                           End Sub

            txtSearch = New TextBox With {
                .Visible = False
            }

            Dim lblSem As New Label With {
                .Text = "Term:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(248, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboSemesterFilter = New ComboBox With {
                .Location = New Point(288, 15),
                .Size = New Size(150, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            AddHandler cboSemesterFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblMethod As New Label With {
                .Text = "Method:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(448, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboMethodFilter = New ComboBox With {
                .Location = New Point(505, 15),
                .Size = New Size(130, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboMethodFilter.Items.AddRange(New Object() {"ALL METHODS", "MPESA", "BANK", "CASH", "OTHER"})
            cboMethodFilter.SelectedIndex = 0
            AddHandler cboMethodFilter.SelectedIndexChanged, Sub() ApplyFilters()

            btnNewPayment = New Button With {
                .Text = "＋ Record Payment",
                .Size = New Size(150, 32),
                .Location = New Point(pnlFilterBar.Width - 340, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(16, 185, 129),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnNewPayment.FlatAppearance.BorderSize = 0
            AddHandler btnNewPayment.Click, AddressOf BtnNewPayment_Click

            btnViewReceipt = New Button With {
                .Text = "🖨️  View Receipt",
                .Size = New Size(130, 32),
                .Location = New Point(pnlFilterBar.Width - 180, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 58, 138),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnViewReceipt.FlatAppearance.BorderColor = Color.FromArgb(56, 189, 248)
            btnViewReceipt.FlatAppearance.BorderSize = 1
            AddHandler btnViewReceipt.Click, AddressOf BtnViewReceipt_Click

            btnRefresh = New Button With {
                .Text = "↻",
                .Size = New Size(34, 32),
                .Location = New Point(pnlFilterBar.Width - 40, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnRefresh.FlatAppearance.BorderColor = ThemeManager.GlassBorder
            AddHandler btnRefresh.Click, Sub() LoadBillingData()

            pnlFilterBar.Controls.AddRange({txtSearchControl, txtSearch, lblSem, cboSemesterFilter, lblMethod, cboMethodFilter, btnNewPayment, btnViewReceipt, btnRefresh})

            ' =========================================================================
            ' PAYMENTS DATA GRID
            ' =========================================================================
            dgvPayments = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvPayments)
            SetupGridColumns()

            AddHandler dgvPayments.CellDoubleClick, Sub(s, e)
                                                        If e.RowIndex >= 0 Then
                                                            BtnViewReceipt_Click(s, EventArgs.Empty)
                                                        End If
                                                    End Sub

            Dim pnlGridContainer As New Panel With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16, 0, 16, 16),
                .BackColor = ThemeManager.CanvasDark
            }
            pnlGridContainer.Controls.Add(dgvPayments)

            Me.Controls.Add(pnlGridContainer)
            Me.Controls.Add(pnlFilterBar)
            Me.Controls.Add(pnlMetricsRow)
        End Sub

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
                kpiBilled.PrimaryValue = $"KES {fin.TotalBilled:N2}"
                kpiPaid.PrimaryValue = $"KES {fin.TotalPaid:N2}"
                kpiArrears.PrimaryValue = $"KES {fin.TotalArrears:N2}"
                kpiReceipts.PrimaryValue = $"{fin.ConfirmedCount:N0}"

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

                dgvPayments.Rows(rowIdx).Cells("Amount").Style.ForeColor = ThemeManager.GlassStatusSuccess
                If p.PaymentStatus = "CONFIRMED" Then
                    dgvPayments.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.GlassStatusSuccess
                ElseIf p.PaymentStatus = "REVERSED" Then
                    dgvPayments.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.GlassStatusDanger
                End If
            Next
        End Sub

        Private Sub BtnNewPayment_Click(sender As Object, e As EventArgs)
            Using modal As New frmPaymentModal(_currentUserId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadBillingData()
                End If
            End Using
        End Sub

        Private Sub BtnViewReceipt_Click(sender As Object, e As EventArgs)
            If dgvPayments.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a payment transaction record to preview receipt.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim paymentId = Convert.ToInt32(dgvPayments.SelectedRows(0).Cells("PaymentId").Value)
            Using modal As New frmReceiptPreviewModal(paymentId)
                modal.ShowDialog(Me.FindForm())
            End Using
        End Sub
    End Class
End Namespace
