Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Rooms
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' Hostel rooms management module for managing room numbers, floor elevations, bed capacities, and types.
    ''' </summary>
    Public Class RoomsControl
        Inherits UserControl

        Private ReadOnly _roomRepo As New RoomRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private _roomsList As New List(Of Room)()

        Private pnlFilterBar As Panel
        Private txtSearch As TextBox
        Private cboHostelFilter As ComboBox
        Private cboRoomTypeFilter As ComboBox
        Private cboStatusFilter As ComboBox
        Private btnNewRoom As Button
        Private btnEditRoom As Button
        Private btnGallery As Button
        Private btnRefresh As Button
        Private dgvRooms As DataGridView

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
            ThemeManager.EnableDoubleBuffering(dgvRooms)
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
                .Placeholder = "Search Room / Hostel..."
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
                .Size = New Size(160, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            AddHandler cboHostelFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblType As New Label With {
                .Text = "Type:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(455, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboRoomTypeFilter = New ComboBox With {
                .Location = New Point(495, 15),
                .Size = New Size(115, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboRoomTypeFilter.Items.AddRange(New Object() {"ALL TYPES", "STANDARD", "DELUXE", "STUDIO"})
            cboRoomTypeFilter.SelectedIndex = 0
            AddHandler cboRoomTypeFilter.SelectedIndexChanged, Sub() ApplyFilters()

            Dim lblStatus As New Label With {
                .Text = "Status:",
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextSilverSlate,
                .Location = New Point(620, 19),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }

            cboStatusFilter = New ComboBox With {
                .Location = New Point(668, 15),
                .Size = New Size(120, 24),
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = Color.FromArgb(16, 26, 42),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            }
            cboStatusFilter.Items.AddRange(New Object() {"ALL STATUSES", "AVAILABLE", "OCCUPIED", "MAINTENANCE", "INACTIVE"})
            cboStatusFilter.SelectedIndex = 0
            AddHandler cboStatusFilter.SelectedIndexChanged, Sub() ApplyFilters()

            btnNewRoom = New Button With {
                .Text = "＋ Add Room",
                .Size = New Size(125, 32),
                .Location = New Point(pnlFilterBar.Width - 365, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 58, 138),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnNewRoom.FlatAppearance.BorderColor = Color.FromArgb(56, 189, 248)
            btnNewRoom.FlatAppearance.BorderSize = 1
            AddHandler btnNewRoom.Click, AddressOf BtnNewRoom_Click

            btnEditRoom = New Button With {
                .Text = "✎ Edit Selected",
                .Size = New Size(115, 32),
                .Location = New Point(pnlFilterBar.Width - 235, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = ThemeManager.TextPlatinum,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnEditRoom.FlatAppearance.BorderColor = ThemeManager.GlassBorder
            AddHandler btnEditRoom.Click, AddressOf BtnEditRoom_Click

            btnGallery = New Button With {
                .Text = "📷 Photos",
                .Size = New Size(95, 32),
                .Location = New Point(pnlFilterBar.Width - 115, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = ThemeManager.GlassStatusInfo,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnGallery.FlatAppearance.BorderColor = ThemeManager.GlassBorder
            AddHandler btnGallery.Click, AddressOf BtnGallery_Click

            btnRefresh = New Button With {
                .Text = "↻",
                .Size = New Size(34, 32),
                .Location = New Point(pnlFilterBar.Width - 50, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .BackColor = Color.FromArgb(30, 42, 60),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
                .Cursor = Cursors.Hand
            }
            btnRefresh.FlatAppearance.BorderColor = ThemeManager.GlassBorder
            AddHandler btnRefresh.Click, Sub() LoadRoomsData()

            pnlFilterBar.Controls.AddRange({txtSearchControl, txtSearch, lblHostel, cboHostelFilter, lblType, cboRoomTypeFilter, lblStatus, cboStatusFilter, btnNewRoom, btnEditRoom, btnGallery, btnRefresh})

            dgvRooms = New DataGridView With {
                .Dock = DockStyle.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvRooms)
            SetupGridColumns()

            AddHandler dgvRooms.CellDoubleClick, Sub(s, e)
                                                     If e.RowIndex >= 0 Then
                                                         BtnEditRoom_Click(s, EventArgs.Empty)
                                                     End If
                                                 End Sub

            Dim pnlGridContainer As New Panel With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(16)
            }
            pnlGridContainer.Controls.Add(dgvRooms)

            Me.Controls.Add(pnlGridContainer)
            Me.Controls.Add(pnlFilterBar)
        End Sub

        Private Sub SetupGridColumns()
            dgvRooms.Columns.Clear()
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RoomId", .HeaderText = "ID", .Visible = False})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "HostelName", .HeaderText = "Hostel Facility", .Width = 180, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RoomNumber", .HeaderText = "Room #", .Width = 90})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "FloorNumber", .HeaderText = "Floor", .Width = 70, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RoomType", .HeaderText = "Room Type", .Width = 110})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Capacity", .HeaderText = "Capacity Limit", .Width = 110, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "RegisteredBeds", .HeaderText = "Registered Beds", .Width = 120, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "OccupiedBeds", .HeaderText = "Occupied", .Width = 85, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "AvailableBeds", .HeaderText = "Available", .Width = 85, .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleRight}})
            dgvRooms.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Status", .HeaderText = "Status", .Width = 100})
        End Sub

        Public Sub LoadRoomsData()
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

                _roomsList = _roomRepo.GetRooms()
                ApplyFilters()
            Catch ex As Exception
                MessageBox.Show($"Failed to load rooms: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ApplyFilters()
            Dim search As String = txtSearch.Text.Trim().ToLowerInvariant()
            Dim selectedHostelId As Integer = 0
            If cboHostelFilter.SelectedItem IsNot Nothing Then
                selectedHostelId = DirectCast(cboHostelFilter.SelectedItem, HostelFilterItem).Id
            End If

            Dim roomType As String = If(cboRoomTypeFilter.SelectedIndex > 0, cboRoomTypeFilter.SelectedItem.ToString(), "ALL")
            Dim status As String = If(cboStatusFilter.SelectedIndex > 0, cboStatusFilter.SelectedItem.ToString(), "ALL")

            dgvRooms.Rows.Clear()
            For Each r In _roomsList
                If selectedHostelId > 0 AndAlso r.HostelId <> selectedHostelId Then
                    Continue For
                End If

                If roomType <> "ALL" AndAlso Not String.Equals(r.RoomType, roomType, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If status <> "ALL" AndAlso Not String.Equals(r.Status, status, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If Not String.IsNullOrEmpty(search) Then
                    Dim matches = r.RoomNumber.ToLowerInvariant().Contains(search) OrElse
                                  r.HostelName.ToLowerInvariant().Contains(search) OrElse
                                  r.RoomType.ToLowerInvariant().Contains(search)
                    If Not matches Then Continue For
                End If

                Dim rowIdx = dgvRooms.Rows.Add(
                    r.RoomId,
                    r.HostelName,
                    r.RoomNumber,
                    r.FloorNumber,
                    r.RoomType,
                    $"{r.Capacity} Beds",
                    $"{r.ActualBedCount} Beds",
                    r.OccupiedBedCount,
                    r.AvailableBedCount,
                    r.Status
                )

                If r.Status = "AVAILABLE" Then
                    dgvRooms.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.StatusSuccess
                ElseIf r.Status = "OCCUPIED" Then
                    dgvRooms.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.StatusDanger
                ElseIf r.Status = "MAINTENANCE" Then
                    dgvRooms.Rows(rowIdx).Cells("Status").Style.ForeColor = ThemeManager.StatusWarning
                End If
            Next
        End Sub

        Public Sub OpenNewRoomModal()
            Dim selectedHostelId As Integer? = Nothing
            If cboHostelFilter.SelectedItem IsNot Nothing Then
                Dim h = DirectCast(cboHostelFilter.SelectedItem, HostelFilterItem)
                If h.Id > 0 Then selectedHostelId = h.Id
            End If

            Using modal As New frmRoomModal(selectedHostelId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadRoomsData()
                End If
            End Using
        End Sub

        Private Sub BtnNewRoom_Click(sender As Object, e As EventArgs)
            OpenNewRoomModal()
        End Sub

        Private Sub BtnEditRoom_Click(sender As Object, e As EventArgs)
            If dgvRooms.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a room from the table to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim roomId As Integer = Convert.ToInt32(dgvRooms.SelectedRows(0).Cells("RoomId").Value)
            Using modal As New frmRoomModal(roomId)
                If modal.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadRoomsData()
                End If
            End Using
        End Sub

        Private Sub BtnGallery_Click(sender As Object, e As EventArgs)
            If dgvRooms.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a room from the table to view its photo gallery.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim roomId As Integer = Convert.ToInt32(dgvRooms.SelectedRows(0).Cells("RoomId").Value)
            Dim hostelName As String = dgvRooms.SelectedRows(0).Cells("HostelName").Value.ToString()
            Dim roomNum As String = dgvRooms.SelectedRows(0).Cells("RoomNumber").Value.ToString()

            Using modal As New Forms.Rooms.frmRoomGalleryModal(roomId, hostelName, roomNum)
                modal.ShowDialog(Me.FindForm())
            End Using
        End Sub
    End Class
End Namespace

