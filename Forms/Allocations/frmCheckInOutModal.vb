Imports System
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Allocations
    ''' <summary>
    ''' Modal form for processing resident arrival check-in or clearance check-out.
    ''' </summary>
    Public Class frmCheckInOutModal
        Private ReadOnly _allocRepo As New AllocationRepository()
        Private ReadOnly _allocationId As Integer
        Private ReadOnly _mode As String        ' "CHECK_IN" or "CHECK_OUT"
        Private ReadOnly _userId As Integer

        Public Sub New(allocationId As Integer, mode As String, Optional currentUserId As Integer = 1)
            InitializeComponent()
            _allocationId = allocationId
            _mode = mode.ToUpperInvariant()
            _userId = currentUserId

            ConfigureMode()
            LoadAllocationData()
        End Sub

        Private Sub ConfigureMode()
            If _mode = "CHECK_OUT" Then
                lblModalTitle.Text = "Resident Departure & Clearance Check-Out"
                lblModalSub.Text = "Verify room keys return, conduct clearance inspection, and release bed back to inventory."
                lblDate.Text = "Check-Out Timestamp *"
                chkKeys.Text = "Room key & wardrobe padlock returned to Accommodation Office"
                chkInventory.Text = "Room inspection conducted; inventory signed off"
                chkMaintenance.Visible = True
                btnConfirm.Text = "Process Clearance Check-Out"
                btnConfirm.BackColor = System.Drawing.Color.FromArgb(180, 83, 9)
            Else
                lblModalTitle.Text = "Resident Arrival Check-In Desk"
                lblModalSub.Text = "Verify room inventory, issue room keys, and log formal arrival timestamp."
                lblDate.Text = "Check-In Timestamp *"
                chkKeys.Text = "Room key & wardrobe padlock issued to resident"
                chkInventory.Text = "Bed frame, mattress, study desk, and lighting inspected with resident"
                chkMaintenance.Visible = False
                btnConfirm.Text = "Confirm Resident Check-In"
                btnConfirm.BackColor = System.Drawing.Color.FromArgb(11, 25, 44)
            End If
            dtpActionDate.Value = DateTime.Now
        End Sub

        Private Sub LoadAllocationData()
            Dim alloc = _allocRepo.GetAllocationById(_allocationId)
            If alloc Is Nothing Then
                MessageBox.Show("Allocation record could not be found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            lblStudentInfo.Text = $"{alloc.StudentName} ({alloc.RegistrationNumber})"
            lblBedInfo.Text = $"{alloc.HostelName} ({alloc.HostelCode}) • Rm {alloc.RoomNumber} • Bed {alloc.BedNumber} ({alloc.BedType})"
            lblAllocInfo.Text = $"Allocation #{alloc.AllocationId} • {alloc.SemesterName} ({alloc.AcademicYear}) • Status: {alloc.AllocationStatus}"
        End Sub

        Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
            If Not chkKeys.Checked Then
                Dim keyPrompt = If(_mode = "CHECK_OUT", "Has the resident returned the room key?", "Have you issued the room key to the resident?")
                Dim r = MessageBox.Show(keyPrompt, "Key Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If r <> DialogResult.Yes Then Return
            End If

            If _mode = "CHECK_OUT" Then
                Dim flagMaint = chkMaintenance.Checked
                Dim maintNotes = If(flagMaint, txtNotes.Text.Trim(), String.Empty)
                If flagMaint AndAlso String.IsNullOrWhiteSpace(maintNotes) Then
                    MessageBox.Show("Please describe the maintenance or repair issue in the notes field before releasing bed to maintenance.", "Issue Notes Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtNotes.Focus()
                    Return
                End If

                Dim result = _allocRepo.CheckOutResident(_allocationId, dtpActionDate.Value, flagMaint, maintNotes, _userId)
                If result.Success Then
                    MessageBox.Show(result.Message, "Check-Out Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(result.Message, "Check-Out Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                Dim result = _allocRepo.CheckInResident(_allocationId, dtpActionDate.Value, txtNotes.Text.Trim(), _userId)
                If result.Success Then
                    MessageBox.Show(result.Message, "Check-In Verified", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(result.Message, "Check-In Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace

