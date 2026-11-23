Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Allocations
    ''' <summary>
    ''' Modal form for transferring a student resident from one bed space to another within the same semester.
    ''' </summary>
    Public Class frmBedTransferModal
        Private ReadOnly _allocRepo As New AllocationRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private ReadOnly _roomRepo As New RoomRepository()
        Private ReadOnly _bedRepo As New BedRepository()

        Private ReadOnly _allocationId As Integer
        Private ReadOnly _userId As Integer
        Private _currentAlloc As BedAllocation
        Private _newBedRate As Decimal = 0.0D
        Private _isLoading As Boolean = False

        Public Property NewAllocationId As Integer = 0

        Private Structure ComboItem
            Public Property Id As Integer
            Public Property DisplayText As String
            Public Property ExtraValue As Decimal

            Public Overrides Function ToString() As String
                Return DisplayText
            End Function
        End Structure

        Public Sub New(allocationId As Integer, Optional currentUserId As Integer = 1)
            InitializeComponent()
            _allocationId = allocationId
            _userId = currentUserId

            LoadCurrentAllocation()
            LoadHostels()
        End Sub

        Private Sub LoadCurrentAllocation()
            _currentAlloc = _allocRepo.GetAllocationById(_allocationId)
            If _currentAlloc Is Nothing Then
                MessageBox.Show("Allocation record could not be found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            lblStudentName.Text = $"{_currentAlloc.StudentName} ({_currentAlloc.RegistrationNumber})"
            lblCurrentBed.Text = $"Current: {_currentAlloc.HostelName} • Rm {_currentAlloc.RoomNumber} • Bed {_currentAlloc.BedNumber}"
            lblCurrentRate.Text = $"Rate: KES {_currentAlloc.SemesterFee:N2}"
        End Sub

        Private Sub LoadHostels()
            _isLoading = True
            cboNewHostel.Items.Clear()
            Dim hostels = _hostelRepo.GetAllHostels(filterStatus:="ACTIVE")
            For Each h In hostels
                cboNewHostel.Items.Add(New ComboItem With {
                    .Id = h.HostelId,
                    .DisplayText = $"{h.HostelName} ({h.HostelCode}) - {h.GenderCategory}"
                })
            Next

            If cboNewHostel.Items.Count > 0 Then
                cboNewHostel.SelectedIndex = 0
                Dim firstHostel = DirectCast(cboNewHostel.Items(0), ComboItem)
                LoadRooms(firstHostel.Id)
            Else
                cboNewRoom.Items.Clear()
                cboNewBed.Items.Clear()
            End If
            _isLoading = False
        End Sub

        Private Sub LoadRooms(hostelId As Integer)
            cboNewRoom.Items.Clear()
            Dim rooms = _roomRepo.GetRooms(hostelId:=hostelId, status:="AVAILABLE")
            For Each r In rooms
                cboNewRoom.Items.Add(New ComboItem With {
                    .Id = r.RoomId,
                    .DisplayText = $"Room {r.RoomNumber} (Floor {r.FloorNumber}, {r.RoomType} - {r.AvailableBedCount} Free)"
                })
            Next

            If cboNewRoom.Items.Count > 0 Then
                cboNewRoom.SelectedIndex = 0
                cboNewRoom.Enabled = True
                Dim firstRoom = DirectCast(cboNewRoom.Items(0), ComboItem)
                LoadBeds(firstRoom.Id)
            Else
                cboNewRoom.Items.Add(New ComboItem With {.Id = 0, .DisplayText = "No available rooms in this hostel"})
                cboNewRoom.SelectedIndex = 0
                cboNewRoom.Enabled = False
                cboNewBed.Items.Clear()
                UpdateReconciliation(0.0D)
            End If
        End Sub

        Private Sub LoadBeds(roomId As Integer)
            cboNewBed.Items.Clear()
            Dim beds = _bedRepo.GetBeds(roomId:=roomId, status:="AVAILABLE")
            For Each b In beds
                ' Exclude current bed if in same room
                If b.BedId = _currentAlloc.BedId Then Continue For
                cboNewBed.Items.Add(New ComboItem With {
                    .Id = b.BedId,
                    .DisplayText = $"{b.BedLabel} (Bed #{b.BedNumber}, {b.BedType}, {b.BedPosition}) — KES {b.SemesterRate:N2}",
                    .ExtraValue = b.SemesterRate
                })
            Next

            If cboNewBed.Items.Count > 0 Then
                cboNewBed.SelectedIndex = 0
                cboNewBed.Enabled = True
                Dim firstBed = DirectCast(cboNewBed.Items(0), ComboItem)
                UpdateReconciliation(firstBed.ExtraValue)
            Else
                cboNewBed.Items.Add(New ComboItem With {.Id = 0, .DisplayText = "No available beds in this room"})
                cboNewBed.SelectedIndex = 0
                cboNewBed.Enabled = False
                UpdateReconciliation(0.0D)
            End If
        End Sub

        Private Sub UpdateReconciliation(newRate As Decimal)
            _newBedRate = newRate
            Dim oldRate = _currentAlloc.SemesterFee
            Dim diff = newRate - oldRate

            If newRate <= 0 Then
                lblDiffAmount.Text = "KES 0.00"
                lblDiffSummary.Text = "Select a valid destination bed space to calculate differential."
                lblDiffAmount.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139)
                Return
            End If

            If diff > 0 Then
                lblDiffAmount.Text = $"+KES {diff:N2}"
                lblDiffAmount.ForeColor = System.Drawing.Color.FromArgb(180, 83, 9)
                lblDiffSummary.Text = $"Upgrade: Current KES {oldRate:N2} ➔ New KES {newRate:N2} (Additional +KES {diff:N2} balance will apply)"
            ElseIf diff < 0 Then
                lblDiffAmount.Text = $"-KES {Math.Abs(diff):N2}"
                lblDiffAmount.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74)
                lblDiffSummary.Text = $"Downgrade: Current KES {oldRate:N2} ➔ New KES {newRate:N2} (Credit of KES {Math.Abs(diff):N2} applies)"
            Else
                lblDiffAmount.Text = "KES 0.00"
                lblDiffAmount.ForeColor = System.Drawing.Color.FromArgb(11, 25, 44)
                lblDiffSummary.Text = $"Parity Transfer: Both bed spaces carry the identical rate of KES {oldRate:N2}."
            End If
        End Sub

        Private Sub cboNewHostel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNewHostel.SelectedIndexChanged
            If _isLoading Then Return
            If cboNewHostel.SelectedItem IsNot Nothing Then
                Dim h = DirectCast(cboNewHostel.SelectedItem, ComboItem)
                LoadRooms(h.Id)
            End If
        End Sub

        Private Sub cboNewRoom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNewRoom.SelectedIndexChanged
            If _isLoading Then Return
            If cboNewRoom.SelectedItem IsNot Nothing Then
                Dim r = DirectCast(cboNewRoom.SelectedItem, ComboItem)
                If r.Id > 0 Then
                    LoadBeds(r.Id)
                End If
            End If
        End Sub

        Private Sub cboNewBed_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNewBed.SelectedIndexChanged
            If cboNewBed.SelectedItem IsNot Nothing Then
                Dim b = DirectCast(cboNewBed.SelectedItem, ComboItem)
                UpdateReconciliation(b.ExtraValue)
            End If
        End Sub

        Private Sub btnExecuteTransfer_Click(sender As Object, e As EventArgs) Handles btnExecuteTransfer.Click
            If cboNewBed.SelectedItem Is Nothing Then
                MessageBox.Show("Please select a valid destination bed space.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim targetBed = DirectCast(cboNewBed.SelectedItem, ComboItem)
            If targetBed.Id <= 0 OrElse targetBed.Id = _currentAlloc.BedId Then
                MessageBox.Show("Please select a different available bed space for transfer.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim reason = txtReason.Text.Trim()
            If String.IsNullOrWhiteSpace(reason) Then
                MessageBox.Show("Please provide a justification or reason for this transfer.", "Reason Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtReason.Focus()
                Return
            End If

            Dim confirmMsg =
                $"Execute Bed Transfer for {_currentAlloc.StudentName}?{Environment.NewLine}{Environment.NewLine}" &
                $"From: {_currentAlloc.BedIdentifier} (KES {_currentAlloc.SemesterFee:N2}){Environment.NewLine}" &
                $"To: {targetBed.DisplayText}{Environment.NewLine}" &
                $"Rate Difference: {lblDiffAmount.Text}{Environment.NewLine}{Environment.NewLine}" &
                $"The original allocation will be marked as TRANSFERRED and the old bed space will be released to AVAILABLE."

            Dim r = MessageBox.Show(confirmMsg, "Confirm Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r <> DialogResult.Yes Then Return

            Dim result = _allocRepo.TransferBed(_allocationId, targetBed.Id, reason, _userId)
            If result.Success Then
                NewAllocationId = result.NewAllocationId
                MessageBox.Show(
                    $"Bed transfer executed successfully!{Environment.NewLine}{Environment.NewLine}" &
                    $"New Allocation ID: {result.NewAllocationId}{Environment.NewLine}" &
                    $"Original Bed ID {_currentAlloc.BedId} released back to inventory.",
                    "Transfer Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show(result.Message, "Transfer Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace
