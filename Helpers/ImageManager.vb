Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO

Namespace Helpers
    ''' <summary>
    ''' Manages physical application assets (room photographs, maintenance evidence, and placeholders)
    ''' with relative path resolution, safe non-locking image streaming, and procedural fallbacks.
    ''' </summary>
    Public Module ImageManager
        Private ReadOnly BaseAssetsDir As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets")
        Private ReadOnly RoomsDir As String = Path.Combine(BaseAssetsDir, "rooms")
        Private ReadOnly MaintenanceDir As String = Path.Combine(BaseAssetsDir, "maintenance")
        Private ReadOnly PlaceholdersDir As String = Path.Combine(BaseAssetsDir, "placeholders")

        ''' <summary>
        ''' Ensures all physical asset directories exist.
        ''' </summary>
        Public Sub EnsureAssetDirectoriesExist()
            Try
                If Not Directory.Exists(BaseAssetsDir) Then Directory.CreateDirectory(BaseAssetsDir)
                If Not Directory.Exists(RoomsDir) Then Directory.CreateDirectory(RoomsDir)
                If Not Directory.Exists(MaintenanceDir) Then Directory.CreateDirectory(MaintenanceDir)
                If Not Directory.Exists(PlaceholdersDir) Then Directory.CreateDirectory(PlaceholdersDir)
            Catch ex As Exception
                ' Best effort
            End Try
        End Sub

        ''' <summary>
        ''' Copies a source photograph to the Assets/rooms directory and returns the relative path for MySQL storage.
        ''' </summary>
        Public Function SaveRoomImage(sourceFilePath As String, roomId As Integer, category As String) As String
            EnsureAssetDirectoriesExist()

            If Not File.Exists(sourceFilePath) Then
                Throw New FileNotFoundException("Source photo not found", sourceFilePath)
            End If

            Dim ext As String = Path.GetExtension(sourceFilePath).ToLowerInvariant()
            If String.IsNullOrWhiteSpace(ext) Then ext = ".jpg"

            Dim safeCategory As String = category.Replace(" ", "_").ToLowerInvariant()
            Dim uniqueTag As String = Guid.NewGuid().ToString("N").Substring(0, 8)
            Dim fileName As String = $"room_{roomId}_{safeCategory}_{DateTime.Now:yyyyMMdd_HHmmss}_{uniqueTag}{ext}"
            Dim destinationPath As String = Path.Combine(RoomsDir, fileName)

            ' Copy source file
            File.Copy(sourceFilePath, destinationPath, True)

            ' Return relative path for database storage (normalized with forward slash or standard relative path)
            Return Path.Combine("Assets", "rooms", fileName).Replace("\"c, "/"c)
        End Function

        ''' <summary>
        ''' Copies a maintenance evidence photo to Assets/maintenance directory and returns the relative path.
        ''' </summary>
        Public Function SaveMaintenanceEvidence(sourceFilePath As String, maintenanceId As Integer) As String
            EnsureAssetDirectoriesExist()

            If Not File.Exists(sourceFilePath) Then
                Throw New FileNotFoundException("Source evidence photo not found", sourceFilePath)
            End If

            Dim ext As String = Path.GetExtension(sourceFilePath).ToLowerInvariant()
            If String.IsNullOrWhiteSpace(ext) Then ext = ".jpg"

            Dim uniqueTag As String = Guid.NewGuid().ToString("N").Substring(0, 8)
            Dim fileName As String = $"maint_{maintenanceId}_{DateTime.Now:yyyyMMdd_HHmmss}_{uniqueTag}{ext}"
            Dim destinationPath As String = Path.Combine(MaintenanceDir, fileName)

            File.Copy(sourceFilePath, destinationPath, True)

            Return Path.Combine("Assets", "maintenance", fileName).Replace("\"c, "/"c)
        End Function

        ''' <summary>
        ''' Safely loads an Image from disk into memory without holding an open OS file handle,
        ''' avoiding Windows Forms GDI+ file locking bugs. Returns a fallback placeholder if not found.
        ''' </summary>
        Public Function LoadImageSafely(relativePathOrAbsolute As String, Optional fallbackWidth As Integer = 320, Optional fallbackHeight As Integer = 200, Optional fallbackText As String = "No Image Available") As Image
            If String.IsNullOrWhiteSpace(relativePathOrAbsolute) Then
                Return CreateProceduralPlaceholder(fallbackWidth, fallbackHeight, fallbackText)
            End If

            ' Resolve path
            Dim resolvedPath As String = relativePathOrAbsolute
            If Not Path.IsPathRooted(resolvedPath) Then
                Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
                Dim candidate As String = Path.Combine(baseDir, relativePathOrAbsolute.Replace("/"c, "\"c))
                If File.Exists(candidate) Then
                    resolvedPath = candidate
                Else
                    Dim cwdCandidate As String = Path.Combine(Directory.GetCurrentDirectory(), relativePathOrAbsolute.Replace("/"c, "\"c))
                    If File.Exists(cwdCandidate) Then
                        resolvedPath = cwdCandidate
                    End If
                End If
            End If

            If Not File.Exists(resolvedPath) Then
                Return CreateProceduralPlaceholder(fallbackWidth, fallbackHeight, fallbackText)
            End If

            Try
                ' Load bytes into memory stream so the file lock is released immediately
                Dim fileBytes As Byte() = File.ReadAllBytes(resolvedPath)
                Using ms As New MemoryStream(fileBytes)
                    Using original As Image = Image.FromStream(ms)
                        ' Clone image so it is fully detached from MemoryStream
                        Return New Bitmap(original)
                    End Using
                End Using
            Catch ex As Exception
                Return CreateProceduralPlaceholder(fallbackWidth, fallbackHeight, "Error Loading Image")
            End Try
        End Function

        ''' <summary>
        ''' Safely deletes an asset file from disk if it exists.
        ''' </summary>
        Public Sub DeleteAssetFile(relativePathOrAbsolute As String)
            If String.IsNullOrWhiteSpace(relativePathOrAbsolute) Then Return

            Dim resolvedPath As String = relativePathOrAbsolute
            If Not Path.IsPathRooted(resolvedPath) Then
                resolvedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePathOrAbsolute.Replace("/"c, "\"c))
            End If

            Try
                If File.Exists(resolvedPath) Then
                    File.Delete(resolvedPath)
                End If
            Catch ex As Exception
                ' Best effort
            End Try
        End Sub

        ''' <summary>
        ''' Generates an elegant procedural academic fallback placeholder bitmap.
        ''' </summary>
        Public Function CreateProceduralPlaceholder(width As Integer, height As Integer, text As String) As Bitmap
            Dim bmp As New Bitmap(Math.Max(100, width), Math.Max(80, height))
            Using g As Graphics = Graphics.FromImage(bmp)
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.Clear(Color.FromArgb(241, 245, 249))

                ' Border
                Using pBorder As New Pen(Color.FromArgb(203, 213, 225), 1.5F)
                    g.DrawRectangle(pBorder, 1, 1, bmp.Width - 3, bmp.Height - 3)
                End Using

                ' Icon / Box
                Dim iconRect As New RectangleF((bmp.Width - 48) / 2.0F, (bmp.Height - 48) / 2.0F - 12, 48, 48)
                Using brushIcon As New SolidBrush(Color.FromArgb(148, 163, 184))
                    g.FillRectangle(brushIcon, iconRect)
                End Using

                ' Inner icon detail
                Using brushInner As New SolidBrush(Color.FromArgb(241, 245, 249))
                    Dim mountainPoints As PointF() = {
                        New PointF(iconRect.Left + 6, iconRect.Bottom - 8),
                        New PointF(iconRect.Left + 20, iconRect.Top + 14),
                        New PointF(iconRect.Left + 32, iconRect.Bottom - 8)
                    }
                    g.FillPolygon(brushInner, mountainPoints)
                    g.FillEllipse(brushInner, iconRect.Right - 16, iconRect.Top + 8, 8, 8)
                End Using

                ' Label Text
                Using fontText As New Font("Segoe UI", 9.0F, FontStyle.Regular)
                    Using brushText As New SolidBrush(Color.FromArgb(100, 116, 139))
                        Dim sf As New StringFormat With {
                            .Alignment = StringAlignment.Center,
                            .LineAlignment = StringAlignment.Center
                        }
                        Dim textRect As New RectangleF(8, iconRect.Bottom + 4, bmp.Width - 16, 26)
                        g.DrawString(text, fontText, brushText, textRect, sf)
                    End Using
                End Using
            End Using
            Return bmp
        End Function
    End Module
End Namespace

