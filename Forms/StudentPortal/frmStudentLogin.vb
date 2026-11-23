Imports System
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports University_hostel_mgt.Data
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Forms.StudentPortal
    ''' <summary>
    ''' Ultra-modern, dedicated student residential login form.
    ''' Allows students to sign in using their official DeKUT email (@students.dkut.ac.ke)
    ''' or registration number, and provides quick access to student self-registration.
    ''' </summary>
    Public Class frmStudentLogin
        Private ReadOnly _authService As New AuthenticationService()
        Private _isDragging As Boolean = False
        Private _dragStartPoint As Point

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlRootAmbient)
            ThemeManager.EnableDoubleBuffering(pnlLeftBrand)
            ThemeManager.EnableDoubleBuffering(pnlLoginCard)
            ThemeManager.EnableDoubleBuffering(picBrandLogo)
            ThemeManager.EnableDoubleBuffering(pnlDbStatusBadge)

            ' Ensure form fits screen and centers
            Dim workArea = Screen.FromControl(Me).WorkingArea
            If Me.Height > workArea.Height - 40 Then
                Me.Height = workArea.Height - 40
            End If
            Me.CenterToScreen()

            picBrandLogo.Image = ThemeManager.GetInstitutionalLogo()
            lblDbStatusText.Text = $"Database: Connected [{DbConfig.Database}]"
            ThemeManager.ApplyTypography(Me)
            txtIdentifier.Select()
        End Sub

        ' 1. Dragging Header
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
            Me.Close()
        End Sub

        Private Sub btnBackToChooser_Click(sender As Object, e As EventArgs) Handles btnBackToChooser.Click
            Me.Close()
        End Sub

        ' 3. Authentication
        Private Async Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
            Await ExecuteLoginAsync()
        End Sub

        Public Async Function ExecuteLoginAsync() As Task(Of Boolean)
            Dim identifier As String = txtIdentifier.TextValue.Trim()
            Dim password As String = txtPassword.TextValue

            lblStatusMessage.Text = String.Empty

            If String.IsNullOrWhiteSpace(identifier) Then
                ShowStatus("Please enter your DeKUT student email or registration number.", ThemeManager.Coral)
                txtIdentifier.Focus()
                Return False
            End If

            If String.IsNullOrEmpty(password) Then
                ShowStatus("Please enter your account password.", ThemeManager.Coral)
                txtPassword.Focus()
                Return False
            End If

            SetLoadingState(True)
            ShowStatus("Verifying student credentials...", ThemeManager.TextSecondary)

            Dim result As AuthenticationResult = Await _authService.AuthenticateAsync(identifier, password, "127.0.0.1")

            SetLoadingState(False)

            If Not result.Success Then
                Dim errorColor = If(result.Status = AuthStatus.LockedOut, ThemeManager.Amber, ThemeManager.Coral)
                ShowStatus(result.ErrorMessage, errorColor)
                txtPassword.TextValue = String.Empty
                txtPassword.Focus()
                Return False
            End If

            ' If a staff user signs in here by mistake, route them or notify
            If Not result.User.IsStudent Then
                ShowStatus("This portal is designated for students. Launching administrative dashboard...", ThemeManager.Amber)
                Await Task.Delay(500)
                Me.Hide()
                Using dash As New frmMainDashboard(result.User)
                    dash.ShowDialog(Me)
                End Using
                txtPassword.TextValue = String.Empty
                txtPassword.Focus()
                Me.Show()
                Return True
            End If

            ' Student Dashboard Launch
            ShowStatus($"✓ Authenticated! Welcome, {result.User.FullName}...", ThemeManager.EmeraldPrimary)
            Await Task.Delay(350)

            Me.Hide()
            Using studentPortal As New frmStudentDashboard(result.User)
                studentPortal.ShowDialog(Me)
            End Using

            txtPassword.TextValue = String.Empty
            txtPassword.Focus()
            ShowStatus("Signed out from student portal.", ThemeManager.TextSecondary)
            Me.Show()
            Return True
        End Function

        Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
            Using reg As New frmStudentRegister()
                If reg.ShowDialog(Me) = DialogResult.OK AndAlso reg.RegisteredUser IsNot Nothing Then
                    txtIdentifier.TextValue = reg.RegisteredUser.Email
                    txtPassword.TextValue = String.Empty
                    txtPassword.Focus()
                    ShowStatus("✓ Account registered! Enter your password to sign in.", ThemeManager.EmeraldPrimary)
                End If
            End Using
        End Sub

        Private Sub SetLoadingState(isLoading As Boolean)
            btnLogin.Enabled = Not isLoading
            btnRegister.Enabled = Not isLoading
            btnBackToChooser.Enabled = Not isLoading
            txtIdentifier.Enabled = Not isLoading
            txtPassword.Enabled = Not isLoading

            If isLoading Then
                btnLogin.Text = "Verifying..."
                btnLogin.PrimaryColor = Color.FromArgb(40, 255, 255, 255)
                btnLogin.CustomTextColor = ThemeManager.TextSecondary
            Else
                btnLogin.Text = "Sign In to Student Portal"
                btnLogin.PrimaryColor = Color.FromArgb(56, 189, 248)
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

