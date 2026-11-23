Imports System
Imports System.Security.Cryptography
Imports System.Text

Namespace Helpers
    ''' <summary>
    ''' Cryptographic password hashing utility using PBKDF2 (HMAC-SHA256)
    ''' with unique cryptographically random salts and constant-time verification.
    ''' </summary>
    Public NotInheritable Class PasswordHasher
        Private Const SaltByteSize As Integer = 16
        Private Const HashByteSize As Integer = 32
        Private Const DefaultIterations As Integer = 100000
        Private Const AlgorithmPrefix As String = "PBKDF2$SHA256"

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Computes a secure PBKDF2-SHA256 hash for the given password string.
        ''' Output format: PBKDF2$SHA256${iterations}${Base64Salt}${Base64Hash}
        ''' </summary>
        Public Shared Function HashPassword(password As String) As String
            If String.IsNullOrEmpty(password) Then
                Throw New ArgumentException("Password cannot be null or empty.", NameOf(password))
            End If

            Dim salt As Byte() = RandomNumberGenerator.GetBytes(SaltByteSize)
            Dim hash As Byte() = Rfc2898DeriveBytes.Pbkdf2(password, salt, DefaultIterations, HashAlgorithmName.SHA256, HashByteSize)

            Return $"{AlgorithmPrefix}${DefaultIterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}"
        End Function

        ''' <summary>
        ''' Verifies a plaintext password against a stored PBKDF2 hash.
        ''' </summary>
        Public Shared Function VerifyPassword(password As String, storedHash As String) As Boolean
            If String.IsNullOrEmpty(password) OrElse String.IsNullOrWhiteSpace(storedHash) Then
                Return False
            End If

            Dim parts As String() = storedHash.Split("$"c)
            If parts.Length <> 5 Then
                Return False
            End If

            Dim algorithm As String = $"{parts(0)}${parts(1)}"
            If Not String.Equals(algorithm, AlgorithmPrefix, StringComparison.OrdinalIgnoreCase) Then
                Return False
            End If

            Dim iterations As Integer
            If Not Integer.TryParse(parts(2), iterations) OrElse iterations <= 0 Then
                Return False
            End If

            Dim salt As Byte()
            Dim expectedHash As Byte()

            Try
                salt = Convert.FromBase64String(parts(3))
                expectedHash = Convert.FromBase64String(parts(4))
            Catch
                Return False
            End Try

            Dim computedHash As Byte() = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expectedHash.Length)
            Return CryptographicOperations.FixedTimeEquals(expectedHash, computedHash)
        End Function
    End Class
End Namespace
