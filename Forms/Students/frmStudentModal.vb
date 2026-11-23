Imports System
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Students
    ''' <summary>
    ''' Modal form for adding and editing university students with uniqueness and format validation.
    ''' </summary>
    Public Class frmStudentModal
        Private ReadOnly _studentRepo As New StudentRepository()
        Private _studentId As Integer = 0
        Private _isEditMode As Boolean = False

        Public Property CreatedOrUpdatedStudentId As Integer = 0

        Public Sub New()
            InitializeComponent()
            _isEditMode = False
            _studentId = 0
            lblModalTitle.Text = "Register Student Resident"
            btnSave.Text = "Save Student"

            cboGender.SelectedIndex = 0
            cboDepartment.SelectedIndex = 0
            cboAcademicYear.SelectedIndex = 0
            cboStatus.SelectedIndex = 0
        End Sub

        Public Sub New(studentId As Integer)
            InitializeComponent()
            _isEditMode = True
            _studentId = studentId
            lblModalTitle.Text = "Edit Student Profile"
            btnSave.Text = "Update Student"

            LoadStudentData(studentId)
        End Sub

        Private Sub LoadStudentData(id As Integer)
            Dim student = _studentRepo.GetStudentById(id)
            If student Is Nothing Then
                MessageBox.Show("Student record could not be found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            txtRegNo.Text = student.RegistrationNumber
            txtFirstName.Text = student.FirstName
            txtMiddleName.Text = student.MiddleName
            txtLastName.Text = student.LastName
            cboGender.SelectedItem = student.Gender
            txtEmail.Text = student.Email
            txtPhone.Text = student.PhoneNumber
            txtCourse.Text = student.Course
            cboDepartment.SelectedItem = student.Department
            cboAcademicYear.SelectedItem = student.AcademicYear
            txtEmergencyName.Text = student.EmergencyContactName
            txtEmergencyPhone.Text = student.EmergencyContactPhone
            cboStatus.SelectedItem = student.StudentStatus
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Dim regNo As String = txtRegNo.Text.Trim().ToUpperInvariant()
            Dim fName As String = txtFirstName.Text.Trim()
            Dim lName As String = txtLastName.Text.Trim()
            Dim email As String = txtEmail.Text.Trim()
            Dim phone As String = txtPhone.Text.Trim()
            Dim course As String = txtCourse.Text.Trim()
            Dim emergName As String = txtEmergencyName.Text.Trim()
            Dim emergPhone As String = txtEmergencyPhone.Text.Trim()

            If String.IsNullOrWhiteSpace(regNo) Then
                MessageBox.Show("Please enter the student registration number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtRegNo.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(fName) Then
                MessageBox.Show("Please enter the student's first name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtFirstName.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(lName) Then
                MessageBox.Show("Please enter the student's last name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtLastName.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(email) OrElse Not email.Contains("@") Then
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtEmail.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(phone) Then
                MessageBox.Show("Please enter the student's phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPhone.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(course) Then
                MessageBox.Show("Please specify the student's degree course.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCourse.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(emergName) Then
                MessageBox.Show("Please provide an emergency contact name (Parent/Guardian).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtEmergencyName.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(emergPhone) Then
                MessageBox.Show("Please provide an emergency contact phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtEmergencyPhone.Focus()
                Return
            End If

            Dim student As New Student With {
                .StudentId = _studentId,
                .RegistrationNumber = regNo,
                .FirstName = fName,
                .MiddleName = txtMiddleName.Text.Trim(),
                .LastName = lName,
                .Gender = If(cboGender.SelectedItem IsNot Nothing, cboGender.SelectedItem.ToString(), "OTHER"),
                .Email = email,
                .PhoneNumber = phone,
                .Course = course,
                .Department = If(cboDepartment.SelectedItem IsNot Nothing, cboDepartment.SelectedItem.ToString(), "General"),
                .AcademicYear = If(cboAcademicYear.SelectedItem IsNot Nothing, cboAcademicYear.SelectedItem.ToString(), "1st Year"),
                .EmergencyContactName = emergName,
                .EmergencyContactPhone = emergPhone,
                .StudentStatus = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "ACTIVE")
            }

            If _isEditMode Then
                Dim result = _studentRepo.UpdateStudent(student)
                If result.Success Then
                    CreatedOrUpdatedStudentId = student.StudentId
                    MessageBox.Show(result.Message, "Student Profile Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(result.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                Dim result = _studentRepo.CreateStudent(student)
                If result.Success Then
                    CreatedOrUpdatedStudentId = result.StudentId
                    MessageBox.Show("Student resident registered successfully.", "Student Registered", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show(result.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class
End Namespace

