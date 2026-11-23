Imports System
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Forms.StudentPortal
    Public Class frmBookBedModal
        Private ReadOnly _roomCard As AvailableRoomCard
        Private ReadOnly _studentUser As User
        Private ReadOnly _portalService As New StudentPortalService()
        Private _isDragging As Boolean = False
        Private _dragStartPoint As Point

        Public Property CreatedAllocationId As Integer = 0

        Public Sub New(roomCard As AvailableRoomCard, studentUser As User)
            InitializeComponent()
            _roomCard = roomCard
            _studentUser = studentUser
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlRoot)
            ThemeManager.EnableDoubleBuffering(pnlSummary)

            ' Populate summary
            lblHostel.Text = _roomCard.HostelName
            lblRoom.Text = $"Room {_roomCard.RoomNumber}"
            lblFloor.Text = $"Floor {_roomCard.FloorNumber}"
            lblType.Text = _roomCard.RoomType
            lblRate.Text = $"KES {_roomCard.SemesterRate:N2} / Semester"

            ' Populate beds
            cboBeds.Items.Clear()
            For Each bed In _roomCard.AvailableBeds
                cboBeds.Items.Add($"Bed {bed.BedNumber} ({bed.BedType}) - KES {bed.SemesterRate:N2}")
            Next
            If cboBeds.Items.Count > 0 Then cboBeds.SelectedIndex = 0
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

        Private Async Sub btnConfirmBook_Click(sender As Object, e As EventArgs) Handles btnConfirmBook.Click
            If cboBeds.SelectedIndex < 0 OrElse cboBeds.SelectedIndex >= _roomCard.AvailableBeds.Count Then
                lblStatusMessage.Text = "Please select a bed space."
                Return
            End If

            Dim selectedBed = _roomCard.AvailableBeds(cboBeds.SelectedIndex)
            Dim studentId = _studentUser.StudentId.GetValueOrDefault(0)

            If studentId = 0 Then
                lblStatusMessage.Text = "Error: Student profile ID not found."
                Return
            End If

            btnConfirmBook.Enabled = False
            btnConfirmBook.Text = "Booking..."
            lblStatusMessage.ForeColor = ThemeManager.TextSecondary
            lblStatusMessage.Text = "Reserving bed and creating allocation..."

            Dim result = Await _portalService.BookBedAsync(studentId, selectedBed.BedId, _studentUser.UserId)

            btnConfirmBook.Enabled = True
            btnConfirmBook.Text = "Confirm Booking & Reserve Bed"

            If Not result.Success Then
                lblStatusMessage.ForeColor = ThemeManager.Coral
                lblStatusMessage.Text = result.Message
                Return
            End If

            CreatedAllocationId = result.AllocationId
            lblStatusMessage.ForeColor = ThemeManager.EmeraldPrimary
            lblStatusMessage.Text = result.Message
            Await Task.Delay(400)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub
    End Class
End Namespace

