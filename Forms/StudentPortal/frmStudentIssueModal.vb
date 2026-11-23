Imports System
Imports System.Drawing
Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Forms.StudentPortal
    Public Class frmStudentIssueModal
        Private ReadOnly _roomId As Integer
        Private ReadOnly _bedId As Integer?
        Private ReadOnly _studentUser As User
        Private ReadOnly _portalService As New StudentPortalService()
        Private _selectedEvidencePath As String = String.Empty
        Private _isDragging As Boolean = False
        Private _dragStartPoint As Point

        Public Property CreatedMaintenanceId As Integer = 0

        Public Sub New(roomId As Integer, bedId As Integer?, studentUser As User)
            InitializeComponent()
            _roomId = roomId
            _bedId = bedId
            _studentUser = studentUser
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlRoot)
            ThemeManager.EnableDoubleBuffering(picEvidence)

            If cboCategory.Items.Count > 0 Then cboCategory.SelectedIndex = 0
            txtTitle.Select()
        End Sub

        Private Sub pnlHeader_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseDown, lblHeaderTitle.MouseDown
            If e.Button = MouseButtons.Left Then
                _isDragging = True
                _dragStartPoint = New Point(e.X, e.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseMove, lblHeaderTitle.MouseMove
            If _isDragging Then
                Dim screenPos = PointToScreen(e.Location)
                Me.Location = New Point(screenPos.X - _dragStartPoint.X, screenPos.Y - _dragStartPoint.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseUp, lblHeaderTitle.MouseUp
            _isDragging = False
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnBrowseImage_Click(sender As Object, e As EventArgs) Handles btnBrowseImage.Click
            Using ofd As New OpenFileDialog()
                ofd.Title = "Select Defect Evidence Photograph"
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.webp"
                If ofd.ShowDialog(Me) = DialogResult.OK Then
                    Try
                        _selectedEvidencePath = ofd.FileName
                        lblEvidencePath.Text = Path.GetFileName(_selectedEvidencePath)
                        picEvidence.Image = Image.FromFile(_selectedEvidencePath)
                    Catch ex As Exception
                        MessageBox.Show($"Could not load selected photo: {ex.Message}", "Image Load Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                End If
            End Using
        End Sub

        Private Async Sub btnSubmitTicket_Click(sender As Object, e As EventArgs) Handles btnSubmitTicket.Click
            Dim title = txtTitle.TextValue.Trim()
            Dim desc = txtDescription.Text.Trim()
            Dim category = If(cboCategory.SelectedItem IsNot Nothing, cboCategory.SelectedItem.ToString(), "General")

            If String.IsNullOrWhiteSpace(title) Then
                lblStatusMessage.ForeColor = ThemeManager.Coral
                lblStatusMessage.Text = "Please enter a defect title."
                txtTitle.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(desc) Then
                lblStatusMessage.ForeColor = ThemeManager.Coral
                lblStatusMessage.Text = "Please describe the maintenance issue."
                txtDescription.Focus()
                Return
            End If

            btnSubmitTicket.Enabled = False
            btnSubmitTicket.Text = "Submitting Ticket..."
            lblStatusMessage.ForeColor = ThemeManager.TextSecondary
            lblStatusMessage.Text = "Filing ticket with estate management..."

            Dim fullTitle = $"[{category}] {title}"
            Dim studentId = _studentUser.StudentId.GetValueOrDefault(0)

            Dim result = Await _portalService.ReportMaintenanceIssueAsync(
                studentId, _roomId, _bedId, fullTitle, desc, _selectedEvidencePath, _studentUser.UserId)

            btnSubmitTicket.Enabled = True
            btnSubmitTicket.Text = "Submit Maintenance Ticket"

            If Not result.Success Then
                lblStatusMessage.ForeColor = ThemeManager.Coral
                lblStatusMessage.Text = result.Message
                Return
            End If

            CreatedMaintenanceId = result.MaintenanceId
            lblStatusMessage.ForeColor = ThemeManager.EmeraldPrimary
            lblStatusMessage.Text = result.Message
            Await Task.Delay(400)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub
    End Class
End Namespace

