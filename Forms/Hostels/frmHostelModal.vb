Imports System
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Models

Namespace Forms.Hostels
    ''' <summary>
    ''' Modal form for adding and editing hostel residential buildings.
    ''' </summary>
    Public Class frmHostelModal
        Private ReadOnly _hostelRepo As New HostelRepository()
        Private _hostelId As Integer = 0
        Private _isEditMode As Boolean = False

        Public Property CreatedOrUpdatedHostelId As Integer = 0

        Public Sub New()
            InitializeComponent()
            _isEditMode = False
            _hostelId = 0
            lblModalTitle.Text = "Register New Hostel Facility"
            btnSave.Text = "Save Hostel"
            cboGender.SelectedIndex = 0
            cboStatus.SelectedIndex = 0
            numFloors.Value = 1
        End Sub

        Public Sub New(hostelId As Integer)
            InitializeComponent()
            _isEditMode = True
            _hostelId = hostelId
            lblModalTitle.Text = "Edit Hostel Facility"
            btnSave.Text = "Update Hostel"
            LoadHostelData(hostelId)
        End Sub

        Private Sub LoadHostelData(id As Integer)
            Dim hostel = _hostelRepo.GetHostelById(id)
            If hostel Is Nothing Then
                MessageBox.Show("Hostel record could not be found.", "Record Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            txtHostelName.Text = hostel.HostelName
            txtHostelCode.Text = hostel.HostelCode
            txtLocation.Text = hostel.Location
            txtDescription.Text = hostel.Description
            cboGender.SelectedItem = hostel.GenderCategory
            numFloors.Value = Math.Max(1, hostel.TotalFloors)
            cboStatus.SelectedItem = hostel.Status
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            ' Validation
            Dim name As String = txtHostelName.Text.Trim()
            Dim code As String = txtHostelCode.Text.Trim().ToUpperInvariant()
            Dim location As String = txtLocation.Location.ToString()
            location = txtLocation.Text.Trim()

            If String.IsNullOrWhiteSpace(name) Then
                MessageBox.Show("Please enter the hostel name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtHostelName.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(code) Then
                MessageBox.Show("Please enter a unique hostel code (e.g. H-A, HALL-1).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtHostelCode.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(location) Then
                MessageBox.Show("Please specify the campus location / zone.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtLocation.Focus()
                Return
            End If

            Dim hostel As New Hostel() With {
                .HostelId = _hostelId,
                .HostelName = name,
                .HostelCode = code,
                .Location = location,
                .Description = txtDescription.Text.Trim(),
                .GenderCategory = If(cboGender.SelectedItem IsNot Nothing, cboGender.SelectedItem.ToString(), "MIXED"),
                .TotalFloors = CInt(numFloors.Value),
                .Status = If(cboStatus.SelectedItem IsNot Nothing, cboStatus.SelectedItem.ToString(), "ACTIVE")
            }

            If _isEditMode Then
                Dim success = _hostelRepo.UpdateHostel(hostel)
                If success Then
                    CreatedOrUpdatedHostelId = hostel.HostelId
                    MessageBox.Show("Hostel facility updated successfully.", "Hostel Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Failed to update hostel facility.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                Dim newId = _hostelRepo.CreateHostel(hostel)
                If newId > 0 Then
                    CreatedOrUpdatedHostelId = newId
                    MessageBox.Show($"Hostel facility '{name}' registered successfully.", "Hostel Created", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Failed to create hostel facility. Ensure the hostel code is unique.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End Sub
    End Class
End Namespace

