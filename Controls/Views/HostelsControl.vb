Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Hostels
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Hostel facilities management module for residential buildings, capacity tracking, and structural configurations.
    ''' </summary>
    Public Class HostelsControl
        Inherits UserControl

        Private ReadOnly _hostelRepo As New HostelRepository()
        Private _hostelsList As New List(Of Hostel)()

        Private pnlFilterBar As Panel
        Private txtSearch As TextBox
        Private cboGenderFilter As ComboBox
        Private cboStatusFilter As ComboBox
        Private btnNewHostel As Button
        Private btnEditHostel As Button
        Private btnRefresh As Button
        Private dgvHostels As DataGridView

        Public Sub New()
            InitializeComponents()
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(dgvHostels)
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
                .Size = New Size(240, 26),
                .PlaceholderText = "🔍  Search by Name, Code, Location...",
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
            }
            AddHandler txtSearch.TextChanged, Sub() ApplyFilters()

            Dim lblGender As New Label With {
                .Text = "Gender:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(270, 18),
                .AutoSize = True
            }

            cboGenderFilter = New ComboBox With {
                .Location = New Point(325, 15),
                .Size = New Size(140, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboGenderFilter.Items.AddRange(New Object() {"ALL GENDERS", "MALE", "FEMALE", "MIXED"})
            cboGenderFilter.SelectedIndex = 0
            AddHandler cboGenderFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblStatus As New Label With {
                .Text = "Status:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(480, 18),
                .AutoSize = True
            }

            cboStatusFilter = New ComboBox With {
                .Location = New Point(528, 15),
                .Size = New Size(130, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboStatusFilter.Items.AddRange(New Object() {"ALL STATUSES", "ACTIVE", "MAINTENANCE", "INACTIVE"})
            cboStatusFilter.SelectedIndex = 0
            AddHandler cboStatusFilter.SelectedIndexChanged, Sub() ApplyFilters()

            btnNewHostel = New Button With {
                .Text = "＋ Add Hostel Building",
                .Size = New Size(170, 30),
                .Location = New Point(pnlFilterBar.Width - 380, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = ThemeManager.NavyDark,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnNewHostel.FlatAppearance.BorderSize = 0
            AddHandler btnNewHostel.Click, AddressOf BtnNewHostel_Click

            btnEditHostel = New Button With {
                .Text = "✎ Edit Selected",
                .Size = New Size(120, 30),
                .Location = New Point(pnlFilterBar.Width - 200, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.TextDeepSlate,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnEditHostel.FlatAppearance.BorderColor = ThemeManager.BorderSubtle
            AddHandler btnEditHostel.Click, AddressOf BtnEditHostel_Click

            btnRefresh = New Button With {
                .Text = "↻",
                .Size = New Size(34, 30),
                .Location = New Point(pnlFilterBar.Width - 70, 13),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.TextDeepSlate,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnRefresh.FlatAppearance.BorderColor = ThemeManager.BorderSubtle
            AddHandler btnRefresh.Click, Sub() LoadHostelsData()

            pnlFilterBar.Controls.AddRange({txtSearch, lblGender, cboGenderFilter, lblStatus, cboStatusFilter, btnNewHostel, btnEditHostel, btnRefresh})

            dgvHostels = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyPrestigeGridStyle(dgvHostels)
            SetupGridColumns()

            AddHandler dgvHostels.CellDoubleClick, Sub(s, e)
                                                       If e.RowIndex >= 0 Then
                                                           BtnEditHostel_Click(s, EventArgs.Empty)
                                                       End If
                                                   End Sub

            Dim pnlGridContainer As New Panel With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16)
            }
            pnlGridContainer.Controls.Add(dgvHostels)

            Me.Controls.Add(pnlGridContainer)
            Me.Controls.Add(pnlFilterBar)
        End Sub

        Private Sub SetupGridColumns()
            dgvHostels.Columns.Clear()
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "HostelId", .HeaderText = "ID", .Visible = False})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Code", .HeaderText = "Code", .Width = 80})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Name", .HeaderText = "Hostel Building Name", .Width = 200, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Location", .HeaderText = "Campus Zone / Location", .Width = 180})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Gender", .HeaderText = "Gender", .Width = 90})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Floors", .HeaderText = "Floors", .Width = 75, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Rooms", .HeaderText = "Rooms", .Width = 80, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "TotalBeds", .HeaderText = "Total Beds", .Width = 90, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Occupancy", .HeaderText = "Occupancy %", .Width = 110, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvHostels.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Status", .HeaderText = "Status", .Width = 90})
        End Sub

        Public Sub LoadHostelsData()
            Try
                _hostelsList = _hostelRepo.GetAllHostels()
                ApplyFilters()
            Catch ex As Exception
                MessageBox.Show($"Failed to load hostels: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ApplyFilters()
            Dim search As String = txtSearch.Text.Trim().ToLowerInvariant()
            Dim gender As String = If(cboGenderFilter.SelectedIndex > 0, cboGenderFilter.SelectedItem.ToString(), "ALL")
            Dim status As String = If(cboStatusFilter.SelectedIndex > 0, cboStatusFilter.SelectedItem.ToString(), "ALL")

            dgvHostels.Rows.Clear()
            For Each h In _hostelsList
                If gender <> "ALL" AndAlso Not String.Equals(h.GenderCategory, gender, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If status <> "ALL" AndAlso Not String.Equals(h.Status, status, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If Not String.IsNullOrEmpty(search) Then
                    Dim matches = h.HostelName.ToLowerInvariant().Contains(search) OrElse
                                  h.HostelCode.ToLowerInvariant().Contains(search) OrElse
                                  h.Location.ToLowerInvariant().Contains(search)
                    If Not matches Then Continue For
                End If

                Dim rowIdx = dgvHostels.Rows.Add(
                    h.HostelId,
                    h.HostelCode,
                    h.HostelName,
                    h.Location,
                    h.GenderCategory,
                    h.TotalFloors,
                    h.RoomCount,
                    h.TotalBedCount,
                    $"{h.OccupancyPercentage:F1}%",
                    h.Status
                )

                If h.Status = "ACTIVE" Then
                    dgvHostels.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.StatusSuccess
                ElseIf h.Status = "MAINTENANCE" Then
                    dgvHostels.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.StatusWarning
                End If
            Next
        End Sub

        Public Sub OpenNewHostelModal()
            Using modal As New frmHostelModal()
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadHostelsData()
                End If
            End Using
        End Sub

        Private Sub BtnNewHostel_Click(sender As Object, e As EventArgs)
            OpenNewHostelModal()
        End Sub

        Private Sub BtnEditHostel_Click(sender As Object, e As EventArgs)
            If dgvHostels.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a hostel facility from the table to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim hostelId As Integer = Convert.ToInt32(dgvHostels.SelectedRows(0).Cells("HostelId").Value)
            Using modal As New frmHostelModal(hostelId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadHostelsData()
                End If
            End Using
        End Sub
    End Class
End Namespace

