Imports System.IO

Namespace Models
    ''' <summary>
    ''' Strongly typed model representing a room photograph asset stored locally and linked via relative DB path.
    ''' </summary>
    Public Class RoomImage
        Public Property ImageId As Integer
        Public Property RoomId As Integer
        Public Property ImagePath As String = String.Empty
        Public Property ImageCaption As String = String.Empty
        Public Property ImageCategory As String = "GENERAL" ' GENERAL, INTERIOR, EXTERIOR, AMENITY, WASHROOM
        Public Property DisplayOrder As Integer = 0
        Public Property IsPrimary As Boolean = False
        Public Property UploadedAt As DateTime = DateTime.UtcNow

        ''' <summary>
        ''' Resolves the relative path to an absolute file path on disk.
        ''' </summary>
        Public ReadOnly Property FullFilePath As String
            Get
                If String.IsNullOrWhiteSpace(ImagePath) Then Return String.Empty
                If Path.IsPathRooted(ImagePath) Then Return ImagePath

                ' Resolve relative to application base directory or current directory
                Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
                Dim combined As String = Path.Combine(baseDir, ImagePath)
                If File.Exists(combined) Then Return combined

                ' Fallback to working directory
                Dim cwdCombined As String = Path.Combine(Directory.GetCurrentDirectory(), ImagePath)
                If File.Exists(cwdCombined) Then Return cwdCombined

                Return combined
            End Get
        End Property

        ''' <summary>
        ''' Returns true if the image file physically exists on disk.
        ''' </summary>
        Public ReadOnly Property FileExists As Boolean
            Get
                Return Not String.IsNullOrWhiteSpace(FullFilePath) AndAlso File.Exists(FullFilePath)
            End Get
        End Property
    End Class
End Namespace
