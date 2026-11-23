Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Beds
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Core individual bed space matrix module with per-semester rate tracking, amenity verification, and occupancy inspection.
    ''' Accommodation is managed and billed strictly at the individual bed level.
    ''' </summary>
    Public Class BedsControl
        Inherits UserControl

        Private ReadOnly _bedRepo As New BedRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private _bedsList As New List(Of Bed)()

        Private pnlFilterBar As Panel
        Private txtSearch As TextBox
        Private cboHostelFilter As ComboBox
        Private cboStatusFilter As ComboBox
        Private cboBedTypeFilter As ComboBox
        Private btnNewBed As Button
        Private btnEditBed As Button
        Private btnRefresh As Button
        Private dgvBeds As DataGridView

        Private Structure HostelFilterItem
            Public Property Id As Integer
            Public Property Name As String

            Public Overrides Function ToString() As String
                Return Name
            End Function
        End Structure

        Public Sub New()
            InitializeComponents()
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(dgvBeds)
        End Sub

        Private Sub InitializeComponents()
            Me.Dock = DockStyle.Fill
            Me.BackColor = ThemeManager.CanvasDark
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)

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
                .Size = New Size(210, 34),
                .Placeholder = "Search Bed / Occupant..."
            }
            AddHandler txtSearchControl.SearchTextChanged, Sub()
                                                               txtSearch.Text = txtSearchControl.QueryText
                                                               ApplyFilters()
                                                           End Sub

            txtSearch = New TextBox With {
                .Visible = False
            }

            Dim lblHostel As New Label With {
                .Text = "Hostel:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(236, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboHostelFilter = New ComboBox With {
                .Location = New Point(285, 15),
                .Size = New Size(155, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            AddHandler cboHostelFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblStatus As New Label With {
                .Text = "Status:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(450, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboStatusFilter = New ComboBox With {
                .Location = New Point(496, 15),
                .Size = New Size(130, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboStatusFilter.Items.AddRange(New Object() {"ALL STATUSES", "AVAILABLE", "OCCUPIED", "MAINTENANCE", "RESERVED", "INACTIVE"})
            cboStatusFilter.SelectedIndex = 0
            AddHandler cboStatusFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblType As New Label With {
                .Text = "Type:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(634, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboBedTypeFilter = New ComboBox With {
                .Location = New Point(674, 15),
                .Size = New Size(120, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboBedTypeFilter.Items.AddRange(New Object() {"ALL TYPES", "STANDARD", "BUNK_TOP", "BUNK_BOTTOM", "DELUXE"})
            cboBedTypeFilter.SelectedIndex = 0
            AddHandler cboBedTypeFilter.SelectedIndexChanged, Sub() ApplyFilters()

            btnNewBed = New Button With {
                .Text = "＋ Add Bed Space",
                .Size = New Size(140, 32),
                .Location = New Point(pnlFilterBar.Width - 340, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 58, 138),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnNewBed.FlatAppearance.BorderColor = Color.FromArgb(56, 189, 248)
            btnNewBed.FlatAppearance.BorderSize = 1
            AddHandler btnNewBed.Click, AddressOf BtnNewBed_Click

            btnEditBed = New Button With {
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
            btnEditBed.FlatAppearance.BorderColor = ThemeManager.GlassBorder
            AddHandler btnEditBed.Click, AddressOf BtnEditBed_Click

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
            AddHandler btnRefresh.Click, Sub() LoadBedsData()

            pnlFilterBar.Controls.AddRange({txtSearchControl, txtSearch, lblHostel, cboHostelFilter, lblStatus, cboStatusFilter, lblType, cboBedTypeFilter, btnNewBed, btnEditBed, btnRefresh})

            dgvBeds = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvBeds)
            SetupGridColumns()
            SetupGridColumns()

            AddHandler dgvBeds.CellDoubleClick, Sub(s, e)
                                                    If e.RowIndex >= 0 Then
                                                        BtnEditBed_Click(s, EventArgs.Empty)
                                                    End If
                                                End Sub

            Dim pnlGridContainer As New Panel With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16)
            }
            pnlGridContainer.Controls.Add(dgvBeds)

            Me.Controls.Add(pnlGridContainer)
            Me.Controls.Add(pnlFilterBar)
        End Sub

        Private Sub SetupGridColumns()
            dgvBeds.Columns.Clear()
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BedId", .HeaderText = "ID", .Visible = False})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "HostelName", .HeaderText = "Hostel Facility", .Width = 150})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RoomNumber", .HeaderText = "Room #", .Width = 80})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BedNumber", .HeaderText = "Bed #", .Width = 70})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BedLabel", .HeaderText = "Bed Space Label", .Width = 160, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "BedType", .HeaderText = "Type", .Width = 100})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "SemesterRate", .HeaderText = "Semester Rate", .Width = 120, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Desk", .HeaderText = "Desk", .Width = 55, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Locker", .HeaderText = "Lock", .Width = 55, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Window", .HeaderText = "Wind", .Width = 55, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Vent", .HeaderText = "Vent", .Width = 55, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Status", .HeaderText = "Status", .Width = 95})
            dgvBeds.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Occupant", .HeaderText = "Current Resident Occupant", .Width = 180})
        End Sub

        Public Sub LoadBedsData()
            Try
                ' Load Hostels into Filter Combo if empty
                If cboHostelFilter.Items.Count = 0 Then
                    cboHostelFilter.Items.Add(New HostelFilterItem With {.Id = 0, .Name = "ALL HOSTELS"})
                    Dim hostels = _hostelRepo.GetAllHostels()
                    For Each h In hostels
                        cboHostelFilter.Items.Add(New HostelFilterItem With {.Id = h.HostelId, .Name = $"{h.HostelName} ({h.HostelCode})"})
                    Next
                    cboHostelFilter.SelectedIndex = 0
                End If

                _bedsList = _bedRepo.GetBeds()
                ApplyFilters()
            Catch ex As Exception
                MessageBox.Show($"Failed to load bed spaces: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ApplyFilters()
            Dim search As String = txtSearch.Text.Trim().ToLowerInvariant()
            Dim selectedHostelId As Integer = 0
            If cboHostelFilter.SelectedItem IsNot Nothing Then
                selectedHostelId = DirectCast(cboHostelFilter.SelectedItem, HostelFilterItem).Id
            End If

            Dim status As String = If(cboStatusFilter.SelectedIndex > 0, cboStatusFilter.SelectedItem.ToString(), "ALL")
            Dim bedType As String = If(cboBedTypeFilter.SelectedIndex > 0, cboBedTypeFilter.SelectedItem.ToString(), "ALL")

            dgvBeds.Rows.Clear()
            For Each b In _bedsList
                If selectedHostelId > 0 AndAlso b.HostelId <> selectedHostelId Then
                    Continue For
                End If

                If status <> "ALL" AndAlso Not String.Equals(b.Status, status, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If bedType <> "ALL" AndAlso Not String.Equals(b.BedType, bedType, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If Not String.IsNullOrEmpty(search) Then
                    Dim matches = b.BedNumber.ToLowerInvariant().Contains(search) OrElse
                                  b.BedLabel.ToLowerInvariant().Contains(search) OrElse
                                  b.RoomNumber.ToLowerInvariant().Contains(search) OrElse
                                  b.HostelName.ToLowerInvariant().Contains(search) OrElse
                                  (b.CurrentOccupantName IsNot Nothing AndAlso b.CurrentOccupantName.ToLowerInvariant().Contains(search)) OrElse
                                  (b.CurrentOccupantRegNo IsNot Nothing AndAlso b.CurrentOccupantRegNo.ToLowerInvariant().Contains(search))
                    If Not matches Then Continue For
                End If

                Dim occupantText As String = "— Vacant —"
                If b.Status = "OCCUPIED" AndAlso Not String.IsNullOrEmpty(b.CurrentOccupantName) Then
                    occupantText = $"{b.CurrentOccupantName} ({b.CurrentOccupantRegNo})"
                End If

                Dim rowIdx = dgvBeds.Rows.Add(
                    b.BedId,
                    b.HostelName,
                    b.RoomNumber,
                    b.BedNumber,
                    b.BedLabel,
                    b.BedType,
                    $"KES {b.SemesterRate:N2}",
                    If(b.HasDesk, "✓", "—"),
                    If(b.HasLocker, "✓", "—"),
                    If(b.HasWindow, "✓", "—"),
                    If(b.HasVentilation, "✓", "—"),
                    b.Status,
                    occupantText
                )

                If b.Status = "AVAILABLE" Then
                    dgvBeds.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.GlassStatusSuccess
                ElseIf b.Status = "OCCUPIED" Then
                    dgvBeds.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.GlassStatusDanger
                ElseIf b.Status = "MAINTENANCE" Then
                    dgvBeds.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.GlassStatusWarning
                ElseIf b.Status = "RESERVED" Then
                    dgvBeds.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.GlassStatusInfo
                End If
            Next
        End Sub

        Public Sub OpenNewBedModal()
            Using modal As New frmBedModal()
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadBedsData()
                End If
            End Using
        End Sub

        Private Sub BtnNewBed_Click(sender As Object, e As EventArgs)
            OpenNewBedModal()
        End Sub

        Private Sub BtnEditBed_Click(sender As Object, e As EventArgs)
            If dgvBeds.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a bed space from the table to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim bedId As Integer = Convert.ToInt32(dgvBeds.SelectedRows(0).Cells("BedId").Value)
            Using modal As New frmBedModal(bedId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadBedsData()
                End If
            End Using
        End Sub
    End Class
End Namespace

