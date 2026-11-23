Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing a registered university student.
    ''' Registration number must be unique across the entire institution.
    ''' </summary>
    Public Class Student
        Public Property StudentId As Integer
        Public Property RegistrationNumber As String = String.Empty
        Public Property FirstName As String = String.Empty
        Public Property MiddleName As String = String.Empty
        Public Property LastName As String = String.Empty
        Public Property Gender As String = "OTHER"           ' MALE, FEMALE, OTHER
        Public Property Email As String = String.Empty
        Public Property PhoneNumber As String = String.Empty
        Public Property Course As String = String.Empty
        Public Property Department As String = String.Empty
        Public Property AcademicYear As String = String.Empty
        Public Property EmergencyContactName As String = String.Empty
        Public Property EmergencyContactPhone As String = String.Empty
        Public Property StudentStatus As String = "ACTIVE"   ' ACTIVE, SUSPENDED, GRADUATED, DEFERRED
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        Public Property UpdatedAt As DateTime = DateTime.UtcNow

        ' Aggregated / Allocation Properties
        Public Property HasActiveAllocation As Boolean = False
        Public Property CurrentBedIdentifier As String = String.Empty
        Public Property TotalBalanceOwed As Decimal = 0.0D

        Public ReadOnly Property FullName As String
            Get
                If String.IsNullOrWhiteSpace(MiddleName) Then
                    Return $"{FirstName} {LastName}".Trim()
                Else
                    Return $"{FirstName} {MiddleName} {LastName}".Trim()
                End If
            End Get
        End Property

        Public ReadOnly Property DisplayName As String
            Get
                Return $"{FullName} ({RegistrationNumber}) - {Course}"
            End Get
        End Property
    End Class
End Namespace

