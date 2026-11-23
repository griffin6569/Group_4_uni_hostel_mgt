Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Data
Imports University_hostel_mgt.Helpers

Namespace Forms
    ''' <summary>
    ''' Ultra-modern institutional portal selection gateway for Dedan Kimathi University of Technology.
    ''' Allows users to choose between the Student Residential Portal and the Staff Administration Gateway.
    ''' </summary>
    Public Class frmPortalChooser
        Private _isDragging As Boolean = False
        Private _dragStartPoint As Point

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlHeader)
            ThemeManager.EnableDoubleBuffering(pnlStudentCard)
            ThemeManager.EnableDoubleBuffering(pnlStaffCard)
            ThemeManager.EnableDoubleBuffering(pnlDbBadge)
            ThemeManager.EnableDoubleBuffering(picLogo)

            picLogo.Image = ThemeManager.GetInstitutionalLogo()
            lblDbText.Text = $"Database: Connected [{DbConfig.Database}]"
            ThemeManager.ApplyTypography(Me)
        End Sub

        ' 1. Header Dragging
        Private Sub pnlHeader_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseDown, lblHeaderTitle.MouseDown
            If e.Button = MouseButtons.Left Then
                _isDragging = True
                _dragStartPoint = New Point(e.X, e.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseMove, lblHeaderTitle.MouseMove
            If _isDragging Then
                Dim currentScreenPos = PointToScreen(e.Location)
                Me.Location = New Point(currentScreenPos.X - _dragStartPoint.X, currentScreenPos.Y - _dragStartPoint.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseUp, lblHeaderTitle.MouseUp
            _isDragging = False
        End Sub

        ' 2. Window Controls
        Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
            Me.WindowState = FormWindowState.Minimized
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Application.Exit()
        End Sub

        Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
            Application.Exit()
        End Sub

        ' 3. Portal Navigation
        Private Sub btnStudentPortal_Click(sender As Object, e As EventArgs) Handles btnStudentPortal.Click
            Me.Hide()
            Using studentLogin As New Forms.StudentPortal.frmStudentLogin()
                studentLogin.ShowDialog(Me)
            End Using
            Me.Show()
        End Sub

        Private Sub btnStaffPortal_Click(sender As Object, e As EventArgs) Handles btnStaffPortal.Click
            Me.Hide()
            Using staffLogin As New Forms.frmLogin()
                staffLogin.ShowDialog(Me)
            End Using
            Me.Show()
        End Sub

        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
            If keyData = Keys.Escape Then
                Application.Exit()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function
    End Class
End Namespace

