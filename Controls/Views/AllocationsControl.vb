Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Allocations
Imports University_hostel_mgt.Forms.Billing
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Core bed allocations module managing semester bed assignments, check-ins, transfers, and check-outs.
    ''' Crucial Rule: Rates are snapshotted permanently into allocation records.
    ''' </summary>
    Public Class AllocationsControl
        Inherits UserControl

        Private ReadOnly _allocRepo As New AllocationRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private ReadOnly _semesterRepo As New SemesterRepository()

        Private _allocList As New List(Of BedAllocation)()
        Private _currentUserId As Integer = 1

        ' Filter Controls
        Private pnlFilterBar As Panel
        Private txtSearch As TextBox
        Private cboSemesterFilter As ComboBox
        Private cboHostelFilter As ComboBox
        Private cboStatusFilter As ComboBox

        ' Action Buttons
        Private btnNewAllocation As Button
        Private btnCheckIn As Button
        Private btnTransfer As Button
        Private btnCheckOut As Button
        Private btnPay As Button
        Private btnRefresh As Button

        Private dgvAllocations As DataGridView

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
            ThemeManager.EnableDoubleBuffering(dgvAllocations)
        End Sub

        Private Sub InitializeComponents()
            Me.Dock = DockStyle.Fill
            Me.BackColor = ThemeManager.SurfaceCanvas
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)

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
                .Size = New Size(180, 26),
                .PlaceholderText = "🔍  Search Student / Bed...",
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
            }
            AddHandler txtSearch.TextChanged, Sub() ApplyFilters()

            Dim lblSem As New Label With {
                .Text = "Term:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(202, 18),
                .AutoSize = True
            }

            cboSemesterFilter = New ComboBox With {
                .Location = New Point(242, 15),
                .Size = New Size(150, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            AddHandler cboSemesterFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblHostel As New Label With {
                .Text = "Hostel:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(398, 18),
                .AutoSize = True
            }

            cboHostelFilter = New ComboBox With {
                .Location = New Point(444, 15),
                .Size = New Size(140, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            AddHandler cboHostelFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblStatus As New Label With {
                .Text = "Status:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(590, 18),
                .AutoSize = True
            }

            cboStatusFilter = New ComboBox With {
                .Location = New Point(634, 15),
                .Size = New Size(115, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboStatusFilter.Items.AddRange(New Object() {"ALL STATUSES", "ACTIVE", "CHECKED_OUT", "TRANSFERRED", "CANCELLED"})
            cboStatusFilter.SelectedIndex = 0
            AddHandler cboStatusFilter.SelectedIndexChanged, Sub() ApplyFilters()

            ' Action buttons
            btnNewAllocation = New Button With {
                .Text = "＋ Allocate Bed",
                .Size = New Size(125, 30),
                .Location = New Point(pnlFilterBar.Width - 530, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = ThemeManager.NavyDark,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnNewAllocation.FlatAppearance.BorderSize = 0
            AddHandler btnNewAllocation.Click, AddressOf BtnNewAllocation_Click

            btnCheckIn = New Button With {
                .Text = "✓ Check-In",
                .Size = New Size(95, 30),
                .Location = New Point(pnlFilterBar.Width - 400, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.TextDeepSlate,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnCheckIn.FlatAppearance.BorderColor = ThemeManager.BorderSubtle
            AddHandler btnCheckIn.Click, AddressOf BtnCheckIn_Click

            btnTransfer = New Button With {
                .Text = "↗ Transfer",
                .Size = New Size(90, 30),
                .Location = New Point(pnlFilterBar.Width - 300, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.TextDeepSlate,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnTransfer.FlatAppearance.BorderColor = ThemeManager.BorderSubtle
            AddHandler btnTransfer.Click, AddressOf BtnTransfer_Click

            btnCheckOut = New Button With {
                .Text = "🚪 Check-Out",
                .Size = New Size(100, 30),
                .Location = New Point(pnlFilterBar.Width - 205, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.TextDeepSlate,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnCheckOut.FlatAppearance.BorderColor = ThemeManager.BorderSubtle
            AddHandler btnCheckOut.Click, AddressOf BtnCheckOut_Click

            btnPay = New Button With {
                .Text = "💳 Pay",
                .Size = New Size(60, 30),
                .Location = New Point(pnlFilterBar.Width - 100, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = ThemeManager.AcademicGold,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnPay.FlatAppearance.BorderSize = 0
            AddHandler btnPay.Click, AddressOf BtnPay_Click

            btnRefresh = New Button With {
                .Text = "↻",
                .Size = New Size(32, 30),
                .Location = New Point(pnlFilterBar.Width - 35, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.TextDeepSlate,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnRefresh.FlatAppearance.BorderColor = ThemeManager.BorderSubtle
            AddHandler btnRefresh.Click, Sub() LoadAllocationsData()

            pnlFilterBar.Controls.AddRange({txtSearch, lblSem, cboSemesterFilter, lblHostel, cboHostelFilter, lblStatus, cboStatusFilter, btnNewAllocation, btnCheckIn, btnTransfer, btnCheckOut, btnPay, btnRefresh})

            dgvAllocations = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyPrestigeGridStyle(dgvAllocations)
            SetupGridColumns()

            Dim pnlGridContainer As New Panel With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16)
            }
            pnlGridContainer.Controls.Add(dgvAllocations)

            Me.Controls.Add(pnlGridContainer)
            Me.Controls.Add(pnlFilterBar)
        End Sub

        Private Sub SetupGridColumns()
            dgvAllocations.Columns.Clear()
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "AllocationId", .HeaderText = "ID", .Visible = False})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RegNo", .HeaderText = "Reg. No", .Width = 120})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "StudentName", .HeaderText = "Student Resident", .Width = 170, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BedSpace", .HeaderText = "Allocated Bed Space", .Width = 190})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Semester", .HeaderText = "Academic Term", .Width = 140})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Rate", .HeaderText = "Fee (Frozen)", .Width = 110, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Paid", .HeaderText = "Paid", .Width = 100, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Balance", .HeaderText = "Balance Due", .Width = 105, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "CheckIn", .HeaderText = "Arrival", .Width = 95})
            dgvAllocations.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Status", .HeaderText = "Status", .Width = 100})
        End Sub

        Public Sub LoadAllocationsData()
            Try
                ' Load Semester Filter
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

                ' Load Hostel Filter
                If cboHostelFilter.Items.Count = 0 Then
                    cboHostelFilter.Items.Add(New ComboItem With {.Id = 0, .Name = "ALL HOSTELS"})
                    Dim hostels = _hostelRepo.GetAllHostels()
                    For Each h In hostels
                        cboHostelFilter.Items.Add(New ComboItem With {.Id = h.HostelId, .Name = h.HostelCode})
                    Next
                    cboHostelFilter.SelectedIndex = 0
                End If

                _allocList = _allocRepo.GetAllocations()
                ApplyFilters()
            Catch ex As Exception
                MessageBox.Show($"Failed to load allocations: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ApplyFilters()
            Dim search = txtSearch.Text.Trim().ToLowerInvariant()
            Dim semId = If(cboSemesterFilter.SelectedItem IsNot Nothing, DirectCast(cboSemesterFilter.SelectedItem, ComboItem).Id, 0)
            Dim hostelId = If(cboHostelFilter.SelectedItem IsNot Nothing, DirectCast(cboHostelFilter.SelectedItem, ComboItem).Id, 0)
            Dim status = If(cboStatusFilter.SelectedIndex > 0, cboStatusFilter.SelectedItem.ToString(), "ALL")

            dgvAllocations.Rows.Clear()
            For Each a In _allocList
                If semId > 0 AndAlso a.SemesterId <> semId Then Continue For
                If hostelId > 0 AndAlso a.HostelId <> hostelId Then Continue For
                If status <> "ALL" AndAlso Not String.Equals(a.AllocationStatus, status, StringComparison.OrdinalIgnoreCase) Then Continue For

                If Not String.IsNullOrEmpty(search) Then
                    Dim matches = a.RegistrationNumber.ToLowerInvariant().Contains(search) OrElse
                                  a.StudentName.ToLowerInvariant().Contains(search) OrElse
                                  a.BedIdentifier.ToLowerInvariant().Contains(search)
                    If Not matches Then Continue For
                End If

                Dim checkInText = If(a.IsCheckedIn, "✓ Arrived", "Pending")
                Dim rowIdx = dgvAllocations.Rows.Add(
                    a.AllocationId,
                    a.RegistrationNumber,
                    a.StudentName,
                    a.BedIdentifier,
                    a.SemesterName,
                    $"KES {a.SemesterFee:N2}",
                    $"KES {a.TotalPaid:N2}",
                    $"KES {a.BalanceOwed:N2}",
                    checkInText,
                    a.AllocationStatus
                )

                If a.BalanceOwed <= 0 Then
                    dgvAllocations.Rows(rowIdx).Cells("Balance").Style.ForeColor = ThemeManager.StatusSuccess
                Else
                    dgvAllocations.Rows(rowIdx).Cells("Balance").Style.ForeColor = ThemeManager.StatusWarning
                End If

                If a.AllocationStatus = "ACTIVE" Then
                    dgvAllocations.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.NavyPrimary
                ElseIf a.AllocationStatus = "CHECKED_OUT" Then
                    dgvAllocations.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.TextSlateMuted
                ElseIf a.AllocationStatus = "TRANSFERRED" Then
                    dgvAllocations.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.StatusInfo
                End If
            Next
        End Sub

        Public Sub OpenNewAllocationModal(Optional preselectedBedId As Integer? = Nothing)
            Using modal As New frmAllocationModal(_currentUserId, preselectedBedId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadAllocationsData()
                End If
            End Using
        End Sub

        Private Sub BtnNewAllocation_Click(sender As Object, e As EventArgs)
            OpenNewAllocationModal()
        End Sub

        Private Sub BtnCheckIn_Click(sender As Object, e As EventArgs)
            If dgvAllocations.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an allocation record from the table to check in.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim allocId = Convert.ToInt32(dgvAllocations.SelectedRows(0).Cells("AllocationId").Value)
            Using modal As New frmCheckInOutModal(allocId, "CHECK_IN", _currentUserId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadAllocationsData()
                End If
            End Using
        End Sub

        Private Sub BtnCheckOut_Click(sender As Object, e As EventArgs)
            If dgvAllocations.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an allocation record from the table to check out.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim allocId = Convert.ToInt32(dgvAllocations.SelectedRows(0).Cells("AllocationId").Value)
            Using modal As New frmCheckInOutModal(allocId, "CHECK_OUT", _currentUserId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadAllocationsData()
                End If
            End Using
        End Sub

        Private Sub BtnTransfer_Click(sender As Object, e As EventArgs)
            If dgvAllocations.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an active allocation record to transfer.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim allocId = Convert.ToInt32(dgvAllocations.SelectedRows(0).Cells("AllocationId").Value)
            Using modal As New frmBedTransferModal(allocId, _currentUserId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadAllocationsData()
                End If
            End Using
        End Sub

        Private Sub BtnPay_Click(sender As Object, e As EventArgs)
            If dgvAllocations.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an allocation to record payment.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim allocId = Convert.ToInt32(dgvAllocations.SelectedRows(0).Cells("AllocationId").Value)
            Using modal As New frmPaymentModal(allocId, _currentUserId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadAllocationsData()
                End If
            End Using
        End Sub
    End Class
End Namespace

