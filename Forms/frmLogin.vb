Imports System
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports University_hostel_mgt.Data
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Forms
    ''' <summary>
    ''' Ultra-modern staff authentication gateway featuring authentic glassmorphism panels,
    ''' smooth focus illumination, and role-based staff administration validation.
    ''' </summary>
    Public Class frmLogin
        Private ReadOnly _authService As New AuthenticationService()
        Private _isDragging As Boolean = False
        Private _dragStartPoint As Point

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlRootAmbient)
            ThemeManager.EnableDoubleBuffering(pnlLoginCard)
            ThemeManager.EnableDoubleBuffering(pnlLeftBrand)
            ThemeManager.EnableDoubleBuffering(picBrandLogo)
            ThemeManager.EnableDoubleBuffering(pnlDbStatusBadge)

            ' Load official Dedan Kimathi University of Technology logo
            picBrandLogo.Image = ThemeManager.GetInstitutionalLogo()

            ' Display active database status
            lblDbStatusText.Text = $"Database: Connected [{DbConfig.Database}]"

            ' Focus username input
            txtUsername.Select()
        End Sub

        ' 1. Borderless Form Dragging Support
        Private Sub pnlHeader_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseDown, lblHeaderTitle.MouseDown
            If e.Button = MouseButtons.Left Then
                _isDragging = True
                _dragStartPoint = New Point(e.X, e.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseMove, lblHeaderTitle.MouseMove
            If _isDragging Then
                Dim currentScreenPos As Point = PointToScreen(e.Location)
                Me.Location = New Point(currentScreenPos.X - _dragStartPoint.X, currentScreenPos.Y - _dragStartPoint.Y)
            End If
        End Sub

        Private Sub pnlHeader_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlHeader.MouseUp, lblHeaderTitle.MouseUp
            _isDragging = False
        End Sub

        ' 2. Window Controls
        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

        Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
            Me.WindowState = FormWindowState.Minimized
        End Sub

        Private Sub btnBackToChooser_Click(sender As Object, e As EventArgs) Handles btnBackToChooser.Click
            Me.Close()
        End Sub

        Private Sub btnExitApp_Click(sender As Object, e As EventArgs) Handles btnExitApp.Click
            Application.Exit()
        End Sub

        ' 3. Authentication Action
        Private Async Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
            Await ExecuteLoginAsync()
        End Sub

        Public Async Function ExecuteLoginAsync() As Task(Of Boolean)
            Dim username As String = txtUsername.TextValue.Trim()
            Dim password As String = txtPassword.TextValue

            lblStatusMessage.Text = String.Empty

            If String.IsNullOrWhiteSpace(username) Then
                ShowStatus("Please enter your staff username.", ThemeManager.Coral)
                txtUsername.Focus()
                Return False
            End If

            If String.IsNullOrEmpty(password) Then
                ShowStatus("Please enter your administrative password.", ThemeManager.Coral)
                txtPassword.Focus()
                Return False
            End If

            ' Animated UI Loading State
            SetLoadingState(True)
            ShowStatus("Verifying administrative credentials...", ThemeManager.TextSecondary)

            ' Call Business Logic Layer
            Dim result As AuthenticationResult = Await _authService.AuthenticateAsync(username, password, "127.0.0.1")

            SetLoadingState(False)

            If Not result.Success Then
                Dim errorColor As Color = If(result.Status = AuthStatus.LockedOut, ThemeManager.Amber, ThemeManager.Coral)
                ShowStatus(result.ErrorMessage, errorColor)
                txtPassword.TextValue = String.Empty
                txtPassword.Focus()
                Return False
            End If

            ' Staff role check: If student enters staff gateway, provide guidance
            If result.User.IsStudent Then
                ShowStatus("This console is for university staff. Launching Student Portal...", ThemeManager.Amber)
                Await Task.Delay(500)

                Me.Hide()
                Using studentPortal As New Forms.StudentPortal.frmStudentDashboard(result.User)
                    studentPortal.ShowDialog(Me)
                End Using

                txtPassword.TextValue = String.Empty
                txtPassword.Focus()
                ShowStatus("Signed out from student portal.", ThemeManager.TextSecondary)
                Me.Show()
                Return True
            End If

            ' Staff Dashboard Route
            ShowStatus($"✓ Authenticated! Launching Administrative Console...", ThemeManager.EmeraldPrimary)
            Await Task.Delay(350)

            Me.Hide()
            Using dashboard As New frmMainDashboard(result.User)
                dashboard.ShowDialog(Me)
            End Using

            ' If dashboard closes (sign out), reset password and show login again
            txtPassword.TextValue = String.Empty
            txtPassword.Focus()
            ShowStatus("Signed out from administrative console.", ThemeManager.TextSecondary)
            Me.Show()

            Return True
        End Function

        Private Sub SetLoadingState(isLoading As Boolean)
            btnLogin.Enabled = Not isLoading
            btnBackToChooser.Enabled = Not isLoading
            btnExitApp.Enabled = Not isLoading
            txtUsername.Enabled = Not isLoading
            txtPassword.Enabled = Not isLoading

            If isLoading Then
                btnLogin.Text = "Verifying..."
                btnLogin.PrimaryColor = Color.FromArgb(40, 255, 255, 255)
                btnLogin.CustomTextColor = ThemeManager.TextSecondary
            Else
                btnLogin.Text = "Sign In to Admin Console"
                btnLogin.PrimaryColor = Color.FromArgb(245, 158, 11)
                btnLogin.CustomTextColor = Color.FromArgb(7, 10, 14)
            End If
        End Sub

        Private Sub ShowStatus(message As String, color As Color)
            lblStatusMessage.ForeColor = color
            lblStatusMessage.Text = message
        End Sub

        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
            If keyData = Keys.Escape Then
                Me.Close()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function
    End Class
End Namespace
