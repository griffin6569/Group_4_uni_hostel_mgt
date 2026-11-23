Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Helpers
    ''' <summary>
    ''' DotEnv configuration loader for the University Hostel Management System.
    ''' Loads environment variables from a .env file into the system process environment
    ''' and provides strongly typed accessors.
    ''' </summary>
    Public NotInheritable Class DotEnv
        Private Shared ReadOnly _values As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Private Shared _isLoaded As Boolean = False
        Private Shared _loadedFilePath As String = String.Empty

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property IsLoaded As Boolean
            Get
                Return _isLoaded
            End Get
        End Property

        Public Shared ReadOnly Property LoadedFilePath As String
            Get
                Return _loadedFilePath
            End Get
        End Property

        ''' <summary>
        ''' Discovers and loads the nearest .env file.
        ''' </summary>
        Public Shared Sub Load(Optional filePath As String = Nothing)
            Dim resolvedPath As String = ResolveEnvPath(filePath)

            If String.IsNullOrWhiteSpace(resolvedPath) OrElse Not File.Exists(resolvedPath) Then
                ' If no .env file found, keep process environment variables intact
                Return
            End If

            _values.Clear()
            Dim lines As String() = File.ReadAllLines(resolvedPath)

            For Each rawLine As String In lines
                Dim line As String = rawLine.Trim()

                ' Ignore comments and blank lines
                If String.IsNullOrWhiteSpace(line) OrElse line.StartsWith("#"c) OrElse line.StartsWith(";"c) Then
                    Continue For
                End If

                Dim separatorIndex As Integer = line.IndexOf("="c)
                If separatorIndex <= 0 Then
                    Continue For
                End If

                Dim key As String = line.Substring(0, separatorIndex).Trim()
                Dim value As String = line.Substring(separatorIndex + 1).Trim()

                ' Strip inline comments if not quoted
                If Not (value.StartsWith(""""c) AndAlso value.EndsWith(""""c)) AndAlso
                   Not (value.StartsWith("'"c) AndAlso value.EndsWith("'"c)) Then
                    Dim commentIndex As Integer = value.IndexOf("#"c)
                    If commentIndex > 0 Then
                        value = value.Substring(0, commentIndex).Trim()
                    End If
                End If

                ' Strip surrounding quotes
                If (value.StartsWith(""""c) AndAlso value.EndsWith(""""c) AndAlso value.Length >= 2) OrElse
                   (value.StartsWith("'"c) AndAlso value.EndsWith("'"c) AndAlso value.Length >= 2) Then
                    value = value.Substring(1, value.Length - 2)
                End If

                ' Replace common escape sequences
                value = value.Replace("\n", vbLf).Replace("\r", vbCr).Replace("\t", vbTab)

                _values(key) = value
                Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.Process)
            Next

            _isLoaded = True
            _loadedFilePath = Path.GetFullPath(resolvedPath)
        End Sub

        ''' <summary>
        ''' Gets a string configuration value.
        ''' </summary>
        Public Shared Function [Get](key As String, Optional defaultValue As String = "") As String
            If Not _isLoaded Then
                Load()
            End If

            ' 1. Check loaded dictionary
            If _values.ContainsKey(key) Then
                Return _values(key)
            End If

            ' 2. Check process environment variable
            Dim envVal As String = Environment.GetEnvironmentVariable(key)
            If Not String.IsNullOrEmpty(envVal) Then
                Return envVal
            End If

            Return defaultValue
        End Function

        ''' <summary>
        ''' Gets an integer configuration value.
        ''' </summary>
        Public Shared Function GetInt(key As String, Optional defaultValue As Integer = 0) As Integer
            Dim strVal As String = [Get](key, Nothing)
            Dim result As Integer
            If Integer.TryParse(strVal, result) Then
                Return result
            End If
            Return defaultValue
        End Function

        ''' <summary>
        ''' Gets a boolean configuration value.
        ''' </summary>
        Public Shared Function GetBool(key As String, Optional defaultValue As Boolean = False) As Boolean
            Dim strVal As String = [Get](key, Nothing)
            If String.IsNullOrWhiteSpace(strVal) Then
                Return defaultValue
            End If

            Dim normalized As String = strVal.Trim().ToLowerInvariant()
            Return (normalized = "true" OrElse normalized = "1" OrElse normalized = "yes" OrElse normalized = "on")
        End Function

        ''' <summary>
        ''' Searches for the .env file in the current directory and its ancestors.
        ''' </summary>
        Private Shared Function ResolveEnvPath(customPath As String) As String
            If Not String.IsNullOrWhiteSpace(customPath) AndAlso File.Exists(customPath) Then
                Return customPath
            End If

            Dim candidates As New List(Of String)()

            ' 1. Application base directory
            Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
            candidates.Add(Path.Combine(baseDir, ".env"))

            ' 2. Current working directory
            Dim currentDir As String = Directory.GetCurrentDirectory()
            candidates.Add(Path.Combine(currentDir, ".env"))

            ' 3. Walk up directories from baseDir (up to 5 levels)
            Dim dirInfo As DirectoryInfo = New DirectoryInfo(baseDir)
            For i As Integer = 0 To 5
                If dirInfo Is Nothing Then Exit For
                candidates.Add(Path.Combine(dirInfo.FullName, ".env"))
                dirInfo = dirInfo.Parent
            Next

            For Each candidate In candidates
                If File.Exists(candidate) Then
                    Return candidate
                End If
            Next

            Return String.Empty
        End Function
    End Class
End Namespace
