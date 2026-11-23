Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Forms.Maintenance
    ''' <summary>
    ''' Code-behind for reporting and updating institutional maintenance incidents.
    ''' Coordinates facility cascading lookups, photographic evidence, and bed locking.
    ''' </summary>
    Public Class frmMaintenanceModal
        Private ReadOnly _currentUserId As Integer
        Private ReadOnly _record As MaintenanceRecord = Nothing
        Private ReadOnly _isEditMode As Boolean = False

        Private ReadOnly _maintenanceRepo As New MaintenanceRepository()
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private ReadOnly _roomRepo As New RoomRepository()
        Private ReadOnly _bedRepo As New BedRepository()

        Private _selectedEvidenceLocalPath As String = String.Empty

        Public Sub New(currentUserId As Integer, Optional existingRecord As MaintenanceRecord = Nothing)
            InitializeComponent()
            _currentUserId = currentUserId
            _record = existingRecord
            _isEditMode = (existingRecord IsNot Nothing)

            If _isEditMode Then
                lblTitle.Text = $"EDIT MAINTENANCE INCIDENT #{_record.MaintenanceId}"
                btnSave.Text = "💾 Update Incident"
            End If
        End Sub

        Private Sub frmMaintenanceModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadHostels()

            If _isEditMode Then
                PopulateExistingRecord()
            Else
                cboStatus.SelectedIndex = 0 ' REPORTED
                picEvidence.Image = ImageManager.CreateProceduralPlaceholder(200, 105, "No Photo Attached")
            End If
        End Sub

        Private Sub LoadHostels()
            cboHostel.Items.Clear()
            For Each h In _hostelRepo.GetAllHostels()
                cboHostel.Items.Add(New ComboItem(h.HostelId, $"{h.HostelName} ({h.HostelCode})"))
            Next
            If cboHostel.Items.Count > 0 Then cboHostel.SelectedIndex = 0
        End Sub

        Private Sub cboHostel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboHostel.SelectedIndexChanged
            cboRoom.Items.Clear()
            cboBed.Items.Clear()

            If cboHostel.SelectedItem Is Nothing Then Return
            Dim hItem = DirectCast(cboHostel.SelectedItem, ComboItem)

            Dim rooms = _roomRepo.GetRooms(hItem.Id)
            For Each r In rooms
                cboRoom.Items.Add(New ComboItem(r.RoomId, $"Room {r.RoomNumber} (Flr {r.FloorNumber})"))
            Next
            If cboRoom.Items.Count > 0 Then cboRoom.SelectedIndex = 0
        End Sub

        Private Sub cboRoom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRoom.SelectedIndexChanged
            cboBed.Items.Clear()
            cboBed.Items.Add(New ComboItem(0, "— Room-Wide Issue (No Specific Bed) —"))

            If cboRoom.SelectedItem Is Nothing Then Return
            Dim rItem = DirectCast(cboRoom.SelectedItem, ComboItem)

            Dim beds = _bedRepo.GetBeds(rItem.Id)
            For Each b In beds
                cboBed.Items.Add(New ComboItem(b.BedId, $"{b.BedLabel} [{b.Status}]"))
            Next
            cboBed.SelectedIndex = 0
        End Sub

        Private Sub PopulateExistingRecord()
            txtIssueTitle.Text = _record.IssueTitle
            txtDescription.Text = _record.IssueDescription
            txtAssignedTo.Text = _record.AssignedTo

            ' Select Hostel
            For i As Integer = 0 To cboHostel.Items.Count - 1
                Dim it = DirectCast(cboHostel.Items(i), ComboItem)
                If it.Id = _record.HostelId Then
                    cboHostel.SelectedIndex = i
                    Exit For
                End If
            Next

            ' Select Room
            For i As Integer = 0 To cboRoom.Items.Count - 1
                Dim it = DirectCast(cboRoom.Items(i), ComboItem)
                If it.Id = _record.RoomId Then
                    cboRoom.SelectedIndex = i
                    Exit For
                End If
            Next

            ' Select Bed
            If _record.BedId.HasValue Then
                For i As Integer = 0 To cboBed.Items.Count - 1
                    Dim it = DirectCast(cboBed.Items(i), ComboItem)
                    If it.Id = _record.BedId.Value Then
                        cboBed.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If

            ' Status
            Dim statusIdx = cboStatus.FindStringExact(_record.MaintenanceStatus)
            If statusIdx >= 0 Then cboStatus.SelectedIndex = statusIdx

            ' Lock facility selectors in edit mode
            cboHostel.Enabled = False
            cboRoom.Enabled = False
            cboBed.Enabled = False

            ' Evidence image
            If Not String.IsNullOrWhiteSpace(_record.EvidenceImagePath) Then
                picEvidence.Image = ImageManager.LoadImageSafely(_record.EvidenceImagePath, 200, 105, "Evidence Photo")
                lblEvidencePath.Text = Path.GetFileName(_record.EvidenceImagePath)
            Else
                picEvidence.Image = ImageManager.CreateProceduralPlaceholder(200, 105, "No Photo Attached")
            End If
        End Sub

        Private Sub btnAttachEvidence_Click(sender As Object, e As EventArgs) Handles btnAttachEvidence.Click
            Using ofd As New OpenFileDialog()
                ofd.Title = "Select Maintenance Defect Evidence Photograph"
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.webp)|*.jpg;*.jpeg;*.png;*.webp|All Files (*.*)|*.*"
                If ofd.ShowDialog(Me) = DialogResult.OK Then
                    _selectedEvidenceLocalPath = ofd.FileName
                    lblEvidencePath.Text = Path.GetFileName(ofd.FileName)
                    picEvidence.Image = ImageManager.LoadImageSafely(ofd.FileName, 200, 105, "Selected Photo")
                End If
            End Using
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            If String.IsNullOrWhiteSpace(txtIssueTitle.Text) Then
                MessageBox.Show("Please enter an issue summary title.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtIssueTitle.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(txtDescription.Text) Then
                MessageBox.Show("Please enter a detailed description of the maintenance issue.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtDescription.Focus()
                Return
            End If

            If cboRoom.SelectedItem Is Nothing Then
                MessageBox.Show("Please select a room.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim roomId As Integer = DirectCast(cboRoom.SelectedItem, ComboItem).Id
            Dim bedId As Integer? = Nothing
            If cboBed.SelectedItem IsNot Nothing Then
                Dim bItem = DirectCast(cboBed.SelectedItem, ComboItem)
                If bItem.Id > 0 Then bedId = bItem.Id
            End If

            Dim status As String = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "REPORTED")

            Try
                Dim evidenceRelativePath As String = If(_isEditMode, _record.EvidenceImagePath, String.Empty)

                ' If new evidence photo was attached, copy it to assets
                If Not String.IsNullOrWhiteSpace(_selectedEvidenceLocalPath) Then
                    Dim tempId As Integer = If(_isEditMode, _record.MaintenanceId, New Random().Next(1000, 9999))
                    evidenceRelativePath = ImageManager.SaveMaintenanceEvidence(_selectedEvidenceLocalPath, tempId)
                End If

                If _isEditMode Then
                    _record.IssueTitle = txtIssueTitle.Text.Trim()
                    _record.IssueDescription = txtDescription.Text.Trim()
                    _record.AssignedTo = txtAssignedTo.Text.Trim()
                    _record.MaintenanceStatus = status
                    _record.EvidenceImagePath = evidenceRelativePath

                    _maintenanceRepo.UpdateIncident(_record)
                    MessageBox.Show("Maintenance incident record updated successfully.", "Incident Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim newRecord As New MaintenanceRecord With {
                        .RoomId = roomId,
                        .BedId = bedId,
                        .IssueTitle = txtIssueTitle.Text.Trim(),
                        .IssueDescription = txtDescription.Text.Trim(),
                        .AssignedTo = txtAssignedTo.Text.Trim(),
                        .MaintenanceStatus = status,
                        .EvidenceImagePath = evidenceRelativePath
                    }

                    Dim newId = _maintenanceRepo.CreateIncident(newRecord, _currentUserId)
                    MessageBox.Show($"Maintenance incident #{newId} logged successfully!" & vbCrLf &
                                    If(bedId.HasValue, "The targeted bed has been locked to MAINTENANCE status.", "Room-wide issue recorded."),
                                    "Incident Recorded", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Catch ex As Exception
                MessageBox.Show($"Failed to save maintenance incident: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Class ComboItem
            Public Property Id As Integer
            Public Property Text As String

            Public Sub New(id As Integer, text As String)
                Me.Id = id
                Me.Text = text
            End Sub

            Public Overrides Function ToString() As String
                Return Text
            End Function
        End Class
    End Class
End Namespace
