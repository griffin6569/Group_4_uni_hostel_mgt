Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Students
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Resident student directory module with instant search, department filtering, and CRUD operations.
    ''' </summary>
    Public Class StudentsControl
        Inherits UserControl

        Private ReadOnly _studentRepo As New StudentRepository()
        Private _studentsList As New List(Of Student)()

        Private pnlFilterBar As Panel
        Private lblTitle As Label
        Private txtSearch As TextBox
        Private cboDeptFilter As ComboBox
        Private cboStatusFilter As ComboBox
        Private btnNewStudent As Button
        Private btnEditStudent As Button
        Private btnRefresh As Button
        Private dgvStudents As DataGridView

        Public Sub New()
            InitializeComponents()
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(dgvStudents)
        End Sub

        Private Sub InitializeComponents()
            Me.Dock = DockStyle.Fill
            Me.BackColor = ThemeManager.CanvasDark
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)

            ' =========================================================================
            ' FILTER / ACTION BAR
            ' =========================================================================
            pnlFilterBar = New PrestigeGlassCard With {
                .Dock = DockStyle.Top,
                .Height = 58,
                .Margin = New Padding(16, 12, 16, 6),
                .Padding = New Padding(16, 10, 16, 10),
                .AccentColor = ThemeManager.GlassBorderFocus,
                .AccentHeight = 2
            }

            Dim txtSearchControl As New GlassSearchBox With {
                .Location = New Point(16, 11),
                .Size = New Size(240, 34),
                .Placeholder = "Search Reg No, Name, Email..."
            }
            AddHandler txtSearchControl.SearchTextChanged, Sub()
                                                               txtSearch.Text = txtSearchControl.QueryText
                                                               ApplyFilters()
                                                           End Sub

            txtSearch = New TextBox With {
                .Visible = False
            }

            Dim lblDept As New Label With {
                .Text = "Dept:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(268, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboDeptFilter = New ComboBox With {
                .Location = New Point(310, 15),
                .Size = New Size(170, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboDeptFilter.Items.AddRange(New Object() {"ALL DEPARTMENTS", "School of Computing", "School of Business", "School of Engineering", "School of Health Sciences", "School of Humanities", "School of Law", "School of Education"})
            cboDeptFilter.SelectedIndex = 0
            AddHandler cboDeptFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblStatus As New Label With {
                .Text = "Status:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(495, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboStatusFilter = New ComboBox With {
                .Location = New Point(542, 15),
                .Size = New Size(130, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboStatusFilter.Items.AddRange(New Object() {"ALL STATUSES", "ACTIVE", "SUSPENDED", "GRADUATED", "DEFERRED"})
            cboStatusFilter.SelectedIndex = 0
            AddHandler cboStatusFilter.SelectedIndexChanged, Sub() ApplyFilters()

            btnNewStudent = New Button With {
                .Text = "＋ Register Student",
                .Size = New Size(150, 32),
                .Location = New Point(pnlFilterBar.Width - 350, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 58, 138),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnNewStudent.FlatAppearance.BorderColor = Color.FromArgb(56, 189, 248)
            btnNewStudent.FlatAppearance.BorderSize = 1
            AddHandler btnNewStudent.Click, AddressOf BtnNewStudent_Click

            btnEditStudent = New Button With {
                .Text = "✎ Edit Selected",
                .Size = New Size(120, 32),
                .Location = New Point(pnlFilterBar.Width - 190, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = ThemeManager.TextPlatinum,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnEditStudent.FlatAppearance.BorderColor = ThemeManager.GlassBorder
            AddHandler btnEditStudent.Click, AddressOf BtnEditStudent_Click

            btnRefresh = New Button With {
                .Text = "↻",
                .Size = New Size(34, 32),
                .Location = New Point(pnlFilterBar.Width - 60, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnRefresh.FlatAppearance.BorderColor = ThemeManager.GlassBorder
            AddHandler btnRefresh.Click, Sub() LoadStudentsData()

            pnlFilterBar.Controls.AddRange({txtSearchControl, txtSearch, lblDept, cboDeptFilter, lblStatus, cboStatusFilter, btnNewStudent, btnEditStudent, btnRefresh})

            ' =========================================================================
            ' DATA GRID VIEW
            ' =========================================================================
            dgvStudents = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvStudents)
            SetupGridColumns()

            AddHandler dgvStudents.CellDoubleClick, Sub(s, e)
                                                        If e.RowIndex >= 0 Then
                                                            BtnEditStudent_Click(s, EventArgs.Empty)
                                                        End If
                                                    End Sub

            Dim pnlGridContainer As New Panel With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16)
            }
            pnlGridContainer.Controls.Add(dgvStudents)

            Me.Controls.Add(pnlGridContainer)
            Me.Controls.Add(pnlFilterBar)
        End Sub

        Private Sub SetupGridColumns()
            dgvStudents.Columns.Clear()
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "StudentId", .HeaderText = "ID", .Visible = False})
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RegNo", .HeaderText = "Reg. Number", .Width = 130})
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "FullName", .HeaderText = "Student Resident Name", .Width = 180, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Gender", .HeaderText = "Gender", .Width = 75})
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Department", .HeaderText = "Faculty / Department", .Width = 160})
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Course", .HeaderText = "Degree Course", .Width = 160})
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BedAlloc", .HeaderText = "Active Bed Space", .Width = 160})
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Balance", .HeaderText = "Balance (KES)", .Width = 110, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvStudents.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Status", .HeaderText = "Status", .Width = 90})
        End Sub

        Public Sub LoadStudentsData()
            Try
                _studentsList = _studentRepo.GetStudents()
                ApplyFilters()
            Catch ex As Exception
                MessageBox.Show($"Failed to load students: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ApplyFilters()
            Dim search As String = txtSearch.Text.Trim().ToLowerInvariant()
            Dim dept As String = If(cboDeptFilter.SelectedIndex > 0, cboDeptFilter.SelectedItem.ToString(), "ALL")
            Dim status As String = If(cboStatusFilter.SelectedIndex > 0, cboStatusFilter.SelectedItem.ToString(), "ALL")

            dgvStudents.Rows.Clear()
            For Each s In _studentsList
                If dept <> "ALL" AndAlso Not String.Equals(s.Department, dept, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If status <> "ALL" AndAlso Not String.Equals(s.StudentStatus, status, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If Not String.IsNullOrEmpty(search) Then
                    Dim matches = s.RegistrationNumber.ToLowerInvariant().Contains(search) OrElse
                                  s.FullName.ToLowerInvariant().Contains(search) OrElse
                                  s.Email.ToLowerInvariant().Contains(search) OrElse
                                  s.Course.ToLowerInvariant().Contains(search)
                    If Not matches Then Continue For
                End If

                Dim bedText As String = If(s.HasActiveAllocation, s.CurrentBedIdentifier, "— Not Allocated —")
                Dim balanceText As String = If(s.TotalBalanceOwed > 0, $"KES {s.TotalBalanceOwed:N2}", "KES 0.00")

                Dim rowIdx = dgvStudents.Rows.Add(
                    s.StudentId,
                    s.RegistrationNumber,
                    s.FullName,
                    s.Gender,
                    s.Department,
                    s.Course,
                    bedText,
                    balanceText,
                    s.StudentStatus
                )

                ' Visual indicator for balances or suspended students
                If s.TotalBalanceOwed > 0 Then
                    dgvStudents.Rows(rowIdx).Cells("Balance").Style.ForeColor = ThemeManager.GlassStatusWarning
                Else
                    dgvStudents.Rows(rowIdx).Cells("Balance").Style.ForeColor = ThemeManager.GlassStatusSuccess
                End If

                If s.StudentStatus = "SUSPENDED" Then
                    dgvStudents.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.GlassStatusDanger
                ElseIf s.StudentStatus = "ACTIVE" Then
                    dgvStudents.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.GlassStatusSuccess
                End If
            Next
        End Sub

        Public Sub OpenNewStudentModal()
            Using modal As New frmStudentModal()
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadStudentsData()
                End If
            End Using
        End Sub

        Private Sub BtnNewStudent_Click(sender As Object, e As EventArgs)
            OpenNewStudentModal()
        End Sub

        Private Sub BtnEditStudent_Click(sender As Object, e As EventArgs)
            If dgvStudents.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a student from the table to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim studentId As Integer = Convert.ToInt32(dgvStudents.SelectedRows(0).Cells("StudentId").Value)
            Using modal As New frmStudentModal(studentId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadStudentsData()
                End If
            End Using
        End Sub
    End Class
End Namespace

