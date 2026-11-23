Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Rooms
    ''' <summary>
    ''' Modal form for adding and editing hostel rooms with capacity ceiling enforcement.
    ''' </summary>
    Public Class frmRoomModal
        Private ReadOnly _roomRepo As New RoomRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private _roomId As Integer = 0
        Private _isEditMode As Boolean = False
        Private _hostels As List(Of Hostel)

        Public Property CreatedOrUpdatedRoomId As Integer = 0

        Public Sub New(Optional preselectedHostelId As Integer? = Nothing)
            InitializeComponent()
            _isEditMode = False
            _roomId = 0
            lblModalTitle.Text = "Register New Hostel Room"
            btnSave.Text = "Save Room"

            LoadHostelsCombo(preselectedHostelId)
            cboRoomType.SelectedIndex = 0
            cboStatus.SelectedIndex = 0
            numFloor.Value = 1
            numCapacity.Value = 2
            lblExistingBedsNotice.Visible = False
        End Sub

        Public Sub New(roomId As Integer)
            InitializeComponent()
            _isEditMode = True
            _roomId = roomId
            lblModalTitle.Text = "Edit Room Details"
            btnSave.Text = "Update Room"

            LoadHostelsCombo()
            LoadRoomData(roomId)
        End Sub

        Private Structure HostelComboItem
            Public Property HostelId As Integer
            Public Property DisplayText As String

            Public Overrides Function ToString() As String
                Return DisplayText
            End Function
        End Structure

        Private Sub LoadHostelsCombo(Optional selectedId As Integer? = Nothing)
            cboHostel.Items.Clear()
            _hostels = _hostelRepo.GetAllHostels(filterStatus:="ACTIVE")

            Dim selectedIndex As Integer = 0
            For i As Integer = 0 To _hostels.Count - 1
                Dim h = _hostels(i)
                cboHostel.Items.Add(New HostelComboItem With {
                    .HostelId = h.HostelId,
                    .DisplayText = $"{h.HostelName} ({h.HostelCode}) - {h.GenderCategory}"
                })
                If selectedId.HasValue AndAlso h.HostelId = selectedId.Value Then
                    selectedIndex = i
                End If
            Next

            If cboHostel.Items.Count > 0 Then
                cboHostel.SelectedIndex = selectedIndex
            End If
        End Sub

        Private Sub LoadRoomData(id As Integer)
            Dim room = _roomRepo.GetRoomById(id)
            If room Is Nothing Then
                MessageBox.Show("Room record could not be found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            ' Hostel cannot be changed in edit mode to preserve bed and allocation relations
            cboHostel.Enabled = False
            For i As Integer = 0 To cboHostel.Items.Count - 1
                Dim item = DirectCast(cboHostel.Items(i), HostelComboItem)
                If item.HostelId = room.HostelId Then
                    cboHostel.SelectedIndex = i
                    Exit For
                End If
            Next

            txtRoomNumber.Text = room.RoomNumber
            numFloor.Value = Math.Max(numFloor.Minimum, Math.Min(numFloor.Maximum, room.FloorNumber))
            cboRoomType.SelectedItem = room.RoomType
            numCapacity.Value = Math.Max(numCapacity.Minimum, Math.Min(numCapacity.Maximum, room.Capacity))
            txtDescription.Text = room.Description
            cboStatus.SelectedItem = room.Status

            If room.ActualBedCount > 0 Then
                lblExistingBedsNotice.Text = $"Notice: Room currently has {room.ActualBedCount} registered bed(s) ({room.OccupiedBedCount} occupied). Capacity cannot be set below {room.ActualBedCount}."
                lblExistingBedsNotice.ForeColor = System.Drawing.Color.FromArgb(180, 83, 9)
                lblExistingBedsNotice.Visible = True
            Else
                lblExistingBedsNotice.Text = "No beds currently registered in this room. You can allocate up to the capacity specified."
                lblExistingBedsNotice.Visible = True
            End If
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Dim roomNum As String = txtRoomNumber.Text.Trim()
            If String.IsNullOrWhiteSpace(roomNum) Then
                MessageBox.Show("Please enter the room number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtRoomNumber.Focus()
                Return
            End If

            If cboHostel.SelectedIndex < 0 Then
                MessageBox.Show("Please select a hostel for this room.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cboHostel.Focus()
                Return
            End If

            Dim selectedHostel = DirectCast(cboHostel.SelectedItem, HostelComboItem)

            Dim room As New Room With {
                .RoomId = _roomId,
                .HostelId = selectedHostel.HostelId,
                .RoomNumber = roomNum,
                .FloorNumber = CInt(numFloor.Value),
                .RoomType = If(cboRoomType.SelectedItem IsNot Nothing, cboRoomType.SelectedItem.ToString(), "STANDARD"),
                .Capacity = CInt(numCapacity.Value),
                .Description = txtDescription.Text.Trim(),
                .Status = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "AVAILABLE")
            }

            If _isEditMode Then
                Dim result = _roomRepo.UpdateRoom(room)
                If result.Success Then
                    CreatedOrUpdatedRoomId = room.RoomId
                    MessageBox.Show(result.Message, "Room Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(result.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                Dim result = _roomRepo.CreateRoom(room)
                If result.Success Then
                    CreatedOrUpdatedRoomId = result.RoomId
                    MessageBox.Show("Room registered successfully.", "Room Created", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(result.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace
