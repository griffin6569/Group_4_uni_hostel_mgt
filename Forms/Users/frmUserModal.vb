Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Users
    ''' <summary>
    ''' Code-behind for staff user registration, credential management, and role-based access assignment.
    ''' </summary>
    Public Class frmUserModal
        Private ReadOnly _currentUserId As Integer
        Private ReadOnly _existingUser As User = Nothing
        Private ReadOnly _isEditMode As Boolean = False
        Private ReadOnly _userRepo As New UserRepository()

        Public Sub New(currentUserId As Integer, Optional existingUser As User = Nothing)
            InitializeComponent()
            _currentUserId = currentUserId
            _existingUser = existingUser
            _isEditMode = (existingUser IsNot Nothing)

            If _isEditMode Then
                lblTitle.Text = $"EDIT STAFF USER: {_existingUser.Username.ToUpperInvariant()}"
                btnSave.Text = "💾 Update User"
            End If
        End Sub

        Private Sub frmUserModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadRoles()

            If _isEditMode Then
                txtFullName.Text = _existingUser.FullName
                txtUsername.Text = _existingUser.Username
                txtUsername.ReadOnly = True ' Username cannot be altered once created
                txtEmail.Text = _existingUser.Email

                Dim statusIdx = cboStatus.FindStringExact(_existingUser.AccountStatus)
                If statusIdx >= 0 Then cboStatus.SelectedIndex = statusIdx

                chkChangePassword.Visible = True
                chkChangePassword.Checked = False
                pnlPasswordSection.Visible = False
            Else
                cboStatus.SelectedIndex = 0 ' ACTIVE
                chkChangePassword.Visible = False
                pnlPasswordSection.Visible = True
            End If
        End Sub

        Private Sub LoadRoles()
            cboRole.Items.Clear()
            For Each r In _userRepo.GetAllRoles()
                cboRole.Items.Add(New RoleComboItem(r.RoleId, $"{r.RoleName} ({r.Description})"))
            Next

            If _isEditMode AndAlso _existingUser IsNot Nothing Then
                For i As Integer = 0 To cboRole.Items.Count - 1
                    Dim it = DirectCast(cboRole.Items(i), RoleComboItem)
                    If it.Id = _existingUser.RoleId Then
                        cboRole.SelectedIndex = i
                        Exit For
                    End If
                Next
            ElseIf cboRole.Items.Count > 0 Then
                cboRole.SelectedIndex = 0
            End If
        End Sub

        Private Sub chkChangePassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkChangePassword.CheckedChanged
            pnlPasswordSection.Visible = chkChangePassword.Checked
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            If String.IsNullOrWhiteSpace(txtFullName.Text) Then
                MessageBox.Show("Please enter the staff member's full name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtFullName.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(txtUsername.Text) Then
                MessageBox.Show("Please enter a username.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtUsername.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(txtEmail.Text) OrElse Not txtEmail.Text.Contains("@") Then
                MessageBox.Show("Please enter a valid official email address.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtEmail.Focus()
                Return
            End If

            If cboRole.SelectedItem Is Nothing Then
                MessageBox.Show("Please assign a security role.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim roleId = DirectCast(cboRole.SelectedItem, RoleComboItem).Id
            Dim status = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "ACTIVE")

            ' Password validation
            Dim needPasswordCheck As Boolean = (Not _isEditMode) OrElse chkChangePassword.Checked
            If needPasswordCheck Then
                If String.IsNullOrWhiteSpace(txtPassword.Text) OrElse txtPassword.Text.Length < 6 Then
                    MessageBox.Show("Password must be at least 6 characters in length.", "Security Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtPassword.Focus()
                    Return
                End If

                If txtPassword.Text <> txtConfirm.Text Then
                    MessageBox.Show("Password and Confirmation do not match. Please re-enter.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtConfirm.Focus()
                    Return
                End If
            End If

            Try
                If _isEditMode Then
                    _existingUser.FullName = txtFullName.Text.Trim()
                    _existingUser.Email = txtEmail.Text.Trim()
                    _existingUser.RoleId = roleId
                    _existingUser.AccountStatus = status

                    _userRepo.UpdateUser(_existingUser, _currentUserId)

                    If chkChangePassword.Checked Then
                        _userRepo.ResetPassword(_existingUser.UserId, txtPassword.Text, _currentUserId)
                    End If

                    MessageBox.Show("Staff account updated successfully.", "User Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim newUser As New User With {
                        .FullName = txtFullName.Text.Trim(),
                        .Username = txtUsername.Text.Trim().ToLowerInvariant(),
                        .Email = txtEmail.Text.Trim().ToLowerInvariant(),
                        .RoleId = roleId,
                        .AccountStatus = status
                    }

                    Dim newId = _userRepo.CreateUser(newUser, txtPassword.Text, _currentUserId)
                    MessageBox.Show($"Staff user '{newUser.Username}' created successfully with User ID #{newId}.", "User Created", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Catch ex As Exception
                MessageBox.Show($"Failed to save user account: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Class RoleComboItem
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

