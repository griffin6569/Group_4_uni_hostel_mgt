Imports System

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing a bursar payment record linked to an allocation.
    ''' </summary>
    Public Class Payment
        Public Property PaymentId As Integer
        Public Property AllocationId As Integer
        Public Property ReceiptNumber As String = String.Empty
        Public Property AmountPaid As Decimal
        Public Property PaymentMethod As String = "MPESA"       ' MPESA, BANK, CASH, OTHER
        Public Property TransactionReference As String = String.Empty
        Public Property PaymentDate As DateTime = DateTime.UtcNow
        Public Property RecordedBy As Integer
        Public Property PaymentStatus As String = "CONFIRMED"   ' CONFIRMED, PENDING, REVERSED
        Public Property Notes As String = String.Empty
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        Public Property UpdatedAt As DateTime = DateTime.UtcNow

        ' Joined information
        Public Property StudentName As String = String.Empty
        Public Property RegistrationNumber As String = String.Empty
        Public Property BedIdentifier As String = String.Empty
        Public Property SemesterName As String = String.Empty
        Public Property SemesterFee As Decimal = 0.0D
        Public Property TotalBalanceRemaining As Decimal = 0.0D
        Public Property RecordedByName As String = String.Empty
    End Class
End Namespace
