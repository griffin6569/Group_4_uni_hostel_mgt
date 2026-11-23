Imports System
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Namespace Forms.StudentPortal
    ''' <summary>
    ''' Student self-service registration form enforcing official Dedan Kimathi University
    ''' student email (@students.dkut.ac.ke) validation and PBKDF2 credential generation.
    ''' </summary>
    Public Class frmStudentRegister
        Private ReadOnly _studentPortalService As New StudentPortalService()
        Private _isDragging As Boolean = False
        Private _dragStartPoint As Point

        Public Property RegisteredUser As User

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlRoot)
            ThemeManager.EnableDoubleBuffering(picLogo)

            picLogo.Image = ThemeManager.GetInstitutionalLogo()
            If cboGender.Items.Count > 0 Then cboGender.SelectedIndex = 0

            txtEmail.Select()
        End Sub

        ' 1. Window Dragging
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

        Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
            Me.WindowState = FormWindowState.Minimized
        End Sub

        Private Sub btnBackToLogin_Click(sender As Object, e As EventArgs) Handles btnBackToLogin.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        ' 2. Registration Action
        Private Async Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
            Await ExecuteRegisterAsync()
        End Sub

        Public Async Function ExecuteRegisterAsync() As Task(Of Boolean)
            Dim email = txtEmail.TextValue.Trim()
            Dim regNo = txtRegNo.TextValue.Trim()
            Dim firstName = txtFirstName.TextValue.Trim()
            Dim lastName = txtLastName.TextValue.Trim()
            Dim gender = If(cboGender.SelectedItem IsNot Nothing, cboGender.SelectedItem.ToString(), "Male")
            Dim course = txtCourse.TextValue.Trim()
            Dim dept = txtDepartment.TextValue.Trim()
            Dim phone = txtPhone.TextValue.Trim()
            Dim password = txtPassword.TextValue
            Dim confirmPass = txtConfirmPassword.TextValue

            lblStatusMessage.Text = String.Empty

            ' Client Validation
            If String.IsNullOrWhiteSpace(email) Then
                ShowStatus("Please enter your DeKUT student email address.", ThemeManager.Coral)
                txtEmail.Focus()
                Return False
            End If

            If Not ValidationHelper.IsValidDeKutStudentEmail(email) Then
                ShowStatus("Email must be an official DeKUT student address ending in @students.dkut.ac.ke.", ThemeManager.Coral)
                txtEmail.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(regNo) Then
                ShowStatus("Please enter your university registration number.", ThemeManager.Coral)
                txtRegNo.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(firstName) Then
                ShowStatus("Please enter your first name.", ThemeManager.Coral)
                txtFirstName.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(lastName) Then
                ShowStatus("Please enter your last name.", ThemeManager.Coral)
                txtLastName.Focus()
                Return False
            End If

            If String.IsNullOrEmpty(password) Then
                ShowStatus("Please create a password for your portal account.", ThemeManager.Coral)
                txtPassword.Focus()
                Return False
            End If

            If password <> confirmPass Then
                ShowStatus("Password and Confirm Password do not match.", ThemeManager.Coral)
                txtConfirmPassword.Focus()
                Return False
            End If

            Dim passError As String = String.Empty
            If Not ValidationHelper.ValidatePasswordStrength(password, passError) Then
                ShowStatus(passError, ThemeManager.Coral)
                txtPassword.Focus()
                Return False
            End If

            ' UI Loading State
            SetLoadingState(True)
            ShowStatus("Creating your student accommodation account...", ThemeManager.TextSecondary)

            Dim result = Await _studentPortalService.RegisterStudentAccountAsync(
                regNo, email, firstName, lastName, gender, course, dept, phone, password)

            SetLoadingState(False)

            If Not result.Success Then
                ShowStatus(result.Message, ThemeManager.Coral)
                Return False
            End If

            ShowStatus("✓ Account created! Redirecting to sign in...", ThemeManager.EmeraldPrimary)
            RegisteredUser = result.User
            Await Task.Delay(500)
            Me.DialogResult = DialogResult.OK
            Me.Close()
            Return True
        End Function

        Private Sub SetLoadingState(isLoading As Boolean)
            btnRegister.Enabled = Not isLoading
            btnBackToLogin.Enabled = Not isLoading
            txtEmail.Enabled = Not isLoading
            txtRegNo.Enabled = Not isLoading
            txtFirstName.Enabled = Not isLoading
            txtLastName.Enabled = Not isLoading
            txtPassword.Enabled = Not isLoading
            txtConfirmPassword.Enabled = Not isLoading

            If isLoading Then
                btnRegister.Text = "Registering..."
                btnRegister.PrimaryColor = Color.FromArgb(40, 255, 255, 255)
            Else
                btnRegister.Text = "Register Account & Proceed to Student Portal"
                btnRegister.PrimaryColor = ThemeManager.EmeraldPrimary
            End If
        End Sub

        Private Sub ShowStatus(message As String, color As Color)
            lblStatusMessage.ForeColor = color
            lblStatusMessage.Text = message
        End Sub

        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
            If keyData = Keys.Escape Then
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function
    End Class
End Namespace

