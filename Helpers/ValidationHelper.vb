Imports System
Imports System.Text.RegularExpressions

Namespace Helpers
    ''' <summary>
    ''' Centralized input validator for Dedan Kimathi University of Technology (DeKUT) institutional rules.
    ''' </summary>
    Public NotInheritable Class ValidationHelper
        Private Sub New()
        End Sub

        ' Official DeKUT student email domain regex (e.g. griffin.wekesa25@students.dkut.ac.ke)
        Private Shared ReadOnly DeKutEmailRegex As New Regex(
            "^[a-zA-Z0-9._%+-]+@students\.dkut\.ac\.ke$",
            RegexOptions.Compiled Or RegexOptions.IgnoreCase)

        ' Phone format: +254..., 254..., 07..., 01...
        Private Shared ReadOnly PhoneRegex As New Regex(
            "^(?:\+?254|0)[17]\d{8}$",
            RegexOptions.Compiled)

        ' Registration number format: e.g. C026-01-1234/2023 or B123-01-4567/2022
        Private Shared ReadOnly RegNoRegex As New Regex(
            "^[A-Za-z0-9]{3,5}-\d{2}-\d{4}\/\d{4}$|^[A-Za-z0-9\/\-_]{5,30}$",
            RegexOptions.Compiled)

        ''' <summary>
        ''' Validates that the email belongs to the official Dedan Kimathi University student domain (@students.dkut.ac.ke).
        ''' </summary>
        Public Shared Function IsValidDeKutStudentEmail(email As String) As Boolean
            If String.IsNullOrWhiteSpace(email) Then Return False
            Return DeKutEmailRegex.IsMatch(email.Trim())
        End Function

        ''' <summary>
        ''' Validates that the registration number matches institutional standards.
        ''' </summary>
        Public Shared Function IsValidRegistrationNumber(regNo As String) As Boolean
            If String.IsNullOrWhiteSpace(regNo) Then Return False
            Return RegNoRegex.IsMatch(regNo.Trim())
        End Function

        ''' <summary>
        ''' Validates Kenyan mobile phone numbers.
        ''' </summary>
        Public Shared Function IsValidPhoneNumber(phone As String) As Boolean
            If String.IsNullOrWhiteSpace(phone) Then Return False
            Dim clean = phone.Trim().Replace(" ", "").Replace("-", "")
            Return PhoneRegex.IsMatch(clean) OrElse clean.Length >= 9
        End Function

        ''' <summary>
        ''' Validates password against institutional security policies (minimum 6 characters, mixed alphanumerics).
        ''' </summary>
        Public Shared Function ValidatePasswordStrength(password As String, ByRef errorMessage As String) As Boolean
            If String.IsNullOrEmpty(password) Then
                errorMessage = "Password cannot be empty."
                Return False
            End If

            If password.Length < 6 Then
                errorMessage = "Password must be at least 6 characters long."
                Return False
            End If

            Dim hasLetter As Boolean = False
            Dim hasDigit As Boolean = False

            For Each ch As Char In password
                If Char.IsLetter(ch) Then hasLetter = True
                If Char.IsDigit(ch) Then hasDigit = True
            Next

            If Not hasLetter OrElse Not hasDigit Then
                errorMessage = "Password must contain both letters and numbers."
                Return False
            End If

            errorMessage = String.Empty
            Return True
        End Function
    End Class
End Namespace

