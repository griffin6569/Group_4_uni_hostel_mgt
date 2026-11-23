Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Beds
    ''' <summary>
    ''' Modal form for adding and editing individual beds with per-semester rate, amenities, and room capacity enforcement.
    ''' </summary>
    Public Class frmBedModal
        Private ReadOnly _bedRepo As New BedRepository()
        Private ReadOnly _roomRepo As New RoomRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private _bedId As Integer = 0
        Private _isEditMode As Boolean = False
        Private _isLoadingCombos As Boolean = False

        Public Property CreatedOrUpdatedBedId As Integer = 0

        Private Structure ComboItem
            Public Property Id As Integer
            Public Property DisplayText As String

            Public Overrides Function ToString() As String
                Return DisplayText
            End Function
        End Structure

        Public Sub New(Optional preselectedRoomId As Integer? = Nothing, Optional preselectedHostelId As Integer? = Nothing)
            InitializeComponent()
            _isEditMode = False
            _bedId = 0
            lblModalTitle.Text = "Register Individual Bed"
            btnSave.Text = "Save Bed"

            InitDefaults(preselectedRoomId, preselectedHostelId)
        End Sub

        Public Sub New(bedId As Integer)
            InitializeComponent()
            _isEditMode = True
            _bedId = bedId
            lblModalTitle.Text = "Edit Bed Details"
            btnSave.Text = "Update Bed"

            LoadBedData(bedId)
        End Sub

        Private Sub InitDefaults(preselectedRoomId As Integer?, preselectedHostelId As Integer?)
            cboBedType.SelectedIndex = 0
            cboPosition.SelectedIndex = 0
            cboStatus.SelectedIndex = 0
            numSemesterRate.Value = 18000.0D

            chkDesk.Checked = True
            chkLocker.Checked = True
            chkWindow.Checked = False
            chkVentilation.Checked = True

            LoadHostels(preselectedHostelId)

            If preselectedHostelId.HasValue AndAlso preselectedRoomId.HasValue Then
                LoadRoomsForHostel(preselectedHostelId.Value, preselectedRoomId.Value)
            ElseIf cboHostel.Items.Count > 0 Then
                Dim firstHostel = DirectCast(cboHostel.Items(0), ComboItem)
                LoadRoomsForHostel(firstHostel.Id, preselectedRoomId)
            End If
        End Sub

        Private Sub LoadHostels(Optional selectedHostelId As Integer? = Nothing)
            _isLoadingCombos = True
            cboHostel.Items.Clear()
            Dim hostels = _hostelRepo.GetAllHostels(filterStatus:="ACTIVE")

            Dim selectedIndex As Integer = 0
            For i As Integer = 0 To hostels.Count - 1
                Dim h = hostels(i)
                cboHostel.Items.Add(New ComboItem With {
                    .Id = h.HostelId,
                    .DisplayText = $"{h.HostelName} ({h.HostelCode})"
                })
                If selectedHostelId.HasValue AndAlso h.HostelId = selectedHostelId.Value Then
                    selectedIndex = i
                End If
            Next

            If cboHostel.Items.Count > 0 Then
                cboHostel.SelectedIndex = selectedIndex
            End If
            _isLoadingCombos = False
        End Sub

        Private Sub LoadRoomsForHostel(hostelId As Integer, Optional selectedRoomId As Integer? = Nothing)
            cboRoom.Items.Clear()
            Dim rooms = _roomRepo.GetRooms(hostelId:=hostelId, status:="AVAILABLE")

            Dim selectedIndex As Integer = 0
            For i As Integer = 0 To rooms.Count - 1
                Dim r = rooms(i)
                cboRoom.Items.Add(New ComboItem With {
                    .Id = r.RoomId,
                    .DisplayText = $"Room {r.RoomNumber} (Floor {r.FloorNumber}, {r.RoomType} - {r.ActualBedCount}/{r.Capacity} Beds)"
                })
                If selectedRoomId.HasValue AndAlso r.RoomId = selectedRoomId.Value Then
                    selectedIndex = i
                End If
            Next

            If cboRoom.Items.Count > 0 Then
                cboRoom.SelectedIndex = selectedIndex
                cboRoom.Enabled = True
            Else
                cboRoom.Items.Add(New ComboItem With {.Id = 0, .DisplayText = "No active rooms available in this hostel"})
                cboRoom.SelectedIndex = 0
                cboRoom.Enabled = False
            End If
        End Sub

        Private Sub cboHostel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboHostel.SelectedIndexChanged
            If _isLoadingCombos OrElse _isEditMode Then Return
            If cboHostel.SelectedItem IsNot Nothing Then
                Dim item = DirectCast(cboHostel.SelectedItem, ComboItem)
                LoadRoomsForHostel(item.Id)
            End If
        End Sub

        Private Sub LoadBedData(id As Integer)
            Dim bed = _bedRepo.GetBedById(id)
            If bed Is Nothing Then
                MessageBox.Show("Bed record could not be found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            _isLoadingCombos = True

            ' In edit mode, hostel and room cannot be altered to preserve transactional allocation continuity
            cboHostel.Items.Add(New ComboItem With {.Id = bed.HostelId, .DisplayText = $"{bed.HostelName} ({bed.HostelCode})"})
            cboHostel.SelectedIndex = 0
            cboHostel.Enabled = False

            cboRoom.Items.Add(New ComboItem With {.Id = bed.RoomId, .DisplayText = $"Room {bed.RoomNumber} (Floor {bed.FloorNumber})"})
            cboRoom.SelectedIndex = 0
            cboRoom.Enabled = False

            txtBedNumber.Text = bed.BedNumber
            txtBedLabel.Text = bed.BedLabel
            cboBedType.SelectedItem = bed.BedType
            numSemesterRate.Value = Math.Max(numSemesterRate.Minimum, Math.Min(numSemesterRate.Maximum, bed.SemesterRate))
            cboPosition.SelectedItem = bed.BedPosition
            cboStatus.SelectedItem = bed.Status

            chkDesk.Checked = bed.HasDesk
            chkLocker.Checked = bed.HasLocker
            chkWindow.Checked = bed.HasWindow
            chkVentilation.Checked = bed.HasVentilation
            txtNotes.Text = bed.Notes

            ' If bed is occupied, prevent changing status manually to avoid orphan allocation records
            If bed.Status = "OCCUPIED" Then
                cboStatus.Enabled = False
                lblStatus.Text = $"Bed Status * (Occupied by {bed.CurrentOccupantName})"
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(180, 83, 9)
            End If

            _isLoadingCombos = False
        End Sub

        Private Sub txtBedNumber_TextChanged(sender As Object, e As EventArgs) Handles txtBedNumber.TextChanged
            If Not _isEditMode AndAlso String.IsNullOrWhiteSpace(txtBedLabel.Text) Then
                Dim num = txtBedNumber.Text.Trim()
                If Not String.IsNullOrEmpty(num) Then
                    txtBedLabel.Text = $"Bed {num}"
                End If
            End If
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Dim bedNum As String = txtBedNumber.Text.Trim()
            If String.IsNullOrWhiteSpace(bedNum) Then
                MessageBox.Show("Please specify the bed number or code (e.g. A, B, 1).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtBedNumber.Focus()
                Return
            End If

            Dim bedLabel As String = txtBedLabel.Text.Trim()
            If String.IsNullOrWhiteSpace(bedLabel) Then
                bedLabel = $"Bed {bedNum}"
            End If

            If cboRoom.SelectedItem Is Nothing Then
                MessageBox.Show("Please select a target room for this bed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cboRoom.Focus()
                Return
            End If

            Dim selectedRoom = DirectCast(cboRoom.SelectedItem, ComboItem)
            If selectedRoom.Id <= 0 Then
                MessageBox.Show("Selected room is not valid. Please select an active room.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If numSemesterRate.Value <= 0 Then
                MessageBox.Show("Semester rate must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                numSemesterRate.Focus()
                Return
            End If

            Dim bed As New Bed With {
                .BedId = _bedId,
                .RoomId = selectedRoom.Id,
                .BedNumber = bedNum,
                .BedLabel = bedLabel,
                .BedType = If(cboBedType.SelectedItem IsNot Nothing, cboBedType.SelectedItem.ToString(), "STANDARD"),
                .SemesterRate = numSemesterRate.Value,
                .BedPosition = If(cboPosition.SelectedItem IsNot Nothing, cboPosition.SelectedItem.ToString(), "CENTER"),
                .HasDesk = chkDesk.Checked,
                .HasLocker = chkLocker.Checked,
                .HasWindow = chkWindow.Checked,
                .HasVentilation = chkVentilation.Checked,
                .Status = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "AVAILABLE"),
                .Notes = txtNotes.Text.Trim()
            }

            If _isEditMode Then
                Dim result = _bedRepo.UpdateBed(bed)
                If result.Success Then
                    CreatedOrUpdatedBedId = bed.BedId
                    MessageBox.Show(result.Message, "Bed Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(result.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                Dim result = _bedRepo.CreateBed(bed)
                If result.Success Then
                    CreatedOrUpdatedBedId = result.BedId
                    MessageBox.Show("Bed registered successfully.", "Bed Created", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(result.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace
