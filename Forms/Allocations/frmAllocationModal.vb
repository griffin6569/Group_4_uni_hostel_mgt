Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Allocations
    ''' <summary>
    ''' Modal form for allocating an individual bed to a student resident with rate freezing.
    ''' </summary>
    Public Class frmAllocationModal
        Private ReadOnly _allocRepo As New AllocationRepository()
        Private ReadOnly _semesterRepo As New SemesterRepository()
        Private ReadOnly _studentRepo As New StudentRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private ReadOnly _roomRepo As New RoomRepository()
        Private ReadOnly _bedRepo As New BedRepository()

        Private _userId As Integer = 1
        Private _isLoading As Boolean = False
        Private _selectedBedRate As Decimal = 0.0D

        Public Property CreatedAllocationId As Integer = 0

        Private Structure ComboItem
            Public Property Id As Integer
            Public Property DisplayText As String
            Public Property ExtraValue As Decimal

            Public Overrides Function ToString() As String
                Return DisplayText
            End Function
        End Structure

        Public Sub New(Optional currentUserId As Integer = 1, Optional preselectedBedId As Integer? = Nothing)
            InitializeComponent()
            _userId = currentUserId

            LoadSemesters()
            LoadStudents()
            LoadHostels()

            If preselectedBedId.HasValue Then
                SelectBedDirectly(preselectedBedId.Value)
            End If
        End Sub

        Private Sub LoadSemesters()
            _isLoading = True
            cboSemester.Items.Clear()
            Dim semesters = _semesterRepo.GetSemesters()
            Dim selectedIdx As Integer = 0

            For i As Integer = 0 To semesters.Count - 1
                Dim s = semesters(i)
                cboSemester.Items.Add(New ComboItem With {
                    .Id = s.SemesterId,
                    .DisplayText = If(s.IsCurrent, $"★ {s.SemesterName} ({s.AcademicYear}) - ACTIVE", $"{s.SemesterName} ({s.AcademicYear})")
                })
                If s.IsCurrent Then selectedIdx = i
            Next

            If cboSemester.Items.Count > 0 Then
                cboSemester.SelectedIndex = selectedIdx
            End If
            _isLoading = False
        End Sub

        Private Sub LoadStudents()
            cboStudent.Items.Clear()
            Dim students = _studentRepo.GetStudents(status:="ACTIVE")
            For Each s In students
                cboStudent.Items.Add(New ComboItem With {
                    .Id = s.StudentId,
                    .DisplayText = $"{s.RegistrationNumber} — {s.FullName} ({s.Gender}, {s.Course})"
                })
            Next

            If cboStudent.Items.Count > 0 Then
                cboStudent.SelectedIndex = 0
            Else
                cboStudent.Items.Add(New ComboItem With {.Id = 0, .DisplayText = "No active students found in system"})
                cboStudent.SelectedIndex = 0
                cboStudent.Enabled = False
            End If
        End Sub

        Private Sub LoadHostels()
            _isLoading = True
            cboHostel.Items.Clear()
            Dim hostels = _hostelRepo.GetAllHostels(filterStatus:="ACTIVE")
            For Each h In hostels
                cboHostel.Items.Add(New ComboItem With {
                    .Id = h.HostelId,
                    .DisplayText = $"{h.HostelName} ({h.HostelCode}) - {h.GenderCategory}"
                })
            Next

            If cboHostel.Items.Count > 0 Then
                cboHostel.SelectedIndex = 0
                Dim firstHostel = DirectCast(cboHostel.Items(0), ComboItem)
                LoadRoomsForHostel(firstHostel.Id)
            Else
                cboRoom.Items.Clear()
                cboBed.Items.Clear()
            End If
            _isLoading = False
        End Sub

        Private Sub LoadRoomsForHostel(hostelId As Integer)
            cboRoom.Items.Clear()
            Dim rooms = _roomRepo.GetRooms(hostelId:=hostelId, status:="AVAILABLE")
            For Each r In rooms
                cboRoom.Items.Add(New ComboItem With {
                    .Id = r.RoomId,
                    .DisplayText = $"Room {r.RoomNumber} (Floor {r.FloorNumber}, {r.RoomType} - {r.AvailableBedCount} Available)"
                })
            Next

            If cboRoom.Items.Count > 0 Then
                cboRoom.SelectedIndex = 0
                cboRoom.Enabled = True
                Dim firstRoom = DirectCast(cboRoom.Items(0), ComboItem)
                LoadBedsForRoom(firstRoom.Id)
            Else
                cboRoom.Items.Add(New ComboItem With {.Id = 0, .DisplayText = "No available rooms in this hostel"})
                cboRoom.SelectedIndex = 0
                cboRoom.Enabled = False
                cboBed.Items.Clear()
                lblRateAmount.Text = "KES 0.00"
                _selectedBedRate = 0.0D
            End If
        End Sub

        Private Sub LoadBedsForRoom(roomId As Integer)
            cboBed.Items.Clear()
            Dim beds = _bedRepo.GetBeds(roomId:=roomId, status:="AVAILABLE")
            For Each b In beds
                cboBed.Items.Add(New ComboItem With {
                    .Id = b.BedId,
                    .DisplayText = $"{b.BedLabel} (Bed #{b.BedNumber}, {b.BedType}, {b.BedPosition}) — KES {b.SemesterRate:N2}",
                    .ExtraValue = b.SemesterRate
                })
            Next

            If cboBed.Items.Count > 0 Then
                cboBed.SelectedIndex = 0
                cboBed.Enabled = True
                Dim firstBed = DirectCast(cboBed.Items(0), ComboItem)
                _selectedBedRate = firstBed.ExtraValue
                lblRateAmount.Text = $"KES {_selectedBedRate:N2}"
            Else
                cboBed.Items.Add(New ComboItem With {.Id = 0, .DisplayText = "No available beds in this room"})
                cboBed.SelectedIndex = 0
                cboBed.Enabled = False
                lblRateAmount.Text = "KES 0.00"
                _selectedBedRate = 0.0D
            End If
        End Sub

        Private Sub SelectBedDirectly(bedId As Integer)
            Dim bed = _bedRepo.GetBedById(bedId)
            If bed Is Nothing Then Return

            ' Preselect hostel
            For i As Integer = 0 To cboHostel.Items.Count - 1
                Dim h = DirectCast(cboHostel.Items(i), ComboItem)
                If h.Id = bed.HostelId Then
                    cboHostel.SelectedIndex = i
                    Exit For
                End If
            Next

            ' Preselect room
            For i As Integer = 0 To cboRoom.Items.Count - 1
                Dim r = DirectCast(cboRoom.Items(i), ComboItem)
                If r.Id = bed.RoomId Then
                    cboRoom.SelectedIndex = i
                    Exit For
                End If
            Next

            ' Preselect bed
            For i As Integer = 0 To cboBed.Items.Count - 1
                Dim b = DirectCast(cboBed.Items(i), ComboItem)
                If b.Id = bed.BedId Then
                    cboBed.SelectedIndex = i
                    Exit For
                End If
            Next
        End Sub

        Private Sub cboHostel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboHostel.SelectedIndexChanged
            If _isLoading Then Return
            If cboHostel.SelectedItem IsNot Nothing Then
                Dim h = DirectCast(cboHostel.SelectedItem, ComboItem)
                LoadRoomsForHostel(h.Id)
            End If
        End Sub

        Private Sub cboRoom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRoom.SelectedIndexChanged
            If _isLoading Then Return
            If cboRoom.SelectedItem IsNot Nothing Then
                Dim r = DirectCast(cboRoom.SelectedItem, ComboItem)
                If r.Id > 0 Then
                    LoadBedsForRoom(r.Id)
                End If
            End If
        End Sub

        Private Sub cboBed_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBed.SelectedIndexChanged
            If cboBed.SelectedItem IsNot Nothing Then
                Dim b = DirectCast(cboBed.SelectedItem, ComboItem)
                _selectedBedRate = b.ExtraValue
                lblRateAmount.Text = $"KES {_selectedBedRate:N2}"
            End If
        End Sub

        Private Sub btnAllocate_Click(sender As Object, e As EventArgs) Handles btnAllocate.Click
            If cboSemester.SelectedItem Is Nothing Then
                MessageBox.Show("Please select an academic semester.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If cboStudent.SelectedItem Is Nothing Then
                MessageBox.Show("Please select a student resident.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sem = DirectCast(cboSemester.SelectedItem, ComboItem)
            Dim stud = DirectCast(cboStudent.SelectedItem, ComboItem)
            If stud.Id <= 0 Then
                MessageBox.Show("Invalid student selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If cboBed.SelectedItem Is Nothing Then
                MessageBox.Show("Please select an available bed space.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim targetBed = DirectCast(cboBed.SelectedItem, ComboItem)
            If targetBed.Id <= 0 Then
                MessageBox.Show("No available bed selected. Please select a room with free bed capacity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Confirmation Prompt Highlighting Frozen Fee
            Dim confirmMsg As String =
                $"Please confirm the individual bed space allocation:{Environment.NewLine}{Environment.NewLine}" &
                $"Student: {stud.DisplayText}{Environment.NewLine}" &
                $"Semester: {sem.DisplayText}{Environment.NewLine}" &
                $"Bed Space: {targetBed.DisplayText}{Environment.NewLine}" &
                $"Snapshotted Fee: KES {_selectedBedRate:N2}{Environment.NewLine}{Environment.NewLine}" &
                $"Note: The semester fee will be permanently snapshotted into this allocation."

            Dim res = MessageBox.Show(confirmMsg, "Confirm Bed Allocation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If res <> DialogResult.Yes Then Return

            Dim result = _allocRepo.CreateAllocation(stud.Id, targetBed.Id, sem.Id, _userId, txtNotes.Text.Trim())
            If result.Success Then
                CreatedAllocationId = result.AllocationId
                MessageBox.Show(
                    $"Bed allocated successfully!{Environment.NewLine}{Environment.NewLine}" &
                    $"Allocation ID: {result.AllocationId}{Environment.NewLine}" &
                    $"Semester Fee Frozen: KES {_selectedBedRate:N2}{Environment.NewLine}" &
                    $"Bed space status has transitioned to OCCUPIED.",
                    "Allocation Confirmed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show(result.Message, "Allocation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace
