Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Maintenance
    ''' <summary>
    ''' Modal for certifying the resolution of a maintenance incident and restoring affected bed spaces.
    ''' </summary>
    Public Class frmResolveMaintenanceModal
        Private ReadOnly _record As MaintenanceRecord
        Private ReadOnly _currentUserId As Integer
        Private ReadOnly _maintenanceRepo As New MaintenanceRepository()

        Public Sub New(record As MaintenanceRecord, currentUserId As Integer)
            InitializeComponent()
            _record = record
            _currentUserId = currentUserId

            lblIncidentInfo.Text = $"Incident #{_record.MaintenanceId} — {_record.IssueTitle}" & vbCrLf &
                                   $"Facility: {_record.TargetDescription} | Reported: {_record.ReportedDate:yyyy-MM-dd}"

            If Not _record.IsBedSpecific Then
                chkRestoreBed.Text = "No specific bed affected (Room-wide issue)"
                chkRestoreBed.Enabled = False
                chkRestoreBed.Checked = False
            End If
        End Sub

        Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
            If String.IsNullOrWhiteSpace(txtNotes.Text) Then
                MessageBox.Show("Please enter resolution notes describing the completed repair work.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNotes.Focus()
                Return
            End If

            Try
                Dim restoreBed As Boolean = chkRestoreBed.Checked
                _maintenanceRepo.ResolveIncident(_record.MaintenanceId, txtNotes.Text.Trim(), restoreBed, _currentUserId)

                MessageBox.Show($"Incident #{_record.MaintenanceId} marked as RESOLVED!" & vbCrLf &
                                If(restoreBed AndAlso _record.IsBedSpecific, "Bed status restored to AVAILABLE.", "Maintenance record closed."),
                                "Incident Resolved", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Catch ex As Exception
                MessageBox.Show($"Failed to resolve incident: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace

