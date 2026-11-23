Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Forms.Rooms
    ''' <summary>
    ''' Code-behind for the Room Photograph Gallery modal.
    ''' Allows viewing, uploading, setting primary status, and managing room photo assets.
    ''' </summary>
    Public Class frmRoomGalleryModal
        Private ReadOnly _roomId As Integer
        Private ReadOnly _hostelName As String
        Private ReadOnly _roomNumber As String
        Private ReadOnly _imageRepo As New RoomImageRepository()

        Private _images As List(Of RoomImage) = New List(Of RoomImage)()
        Private _selectedImage As RoomImage = Nothing

        Public Sub New(roomId As Integer, hostelName As String, roomNumber As String)
            InitializeComponent()
            _roomId = roomId
            _hostelName = hostelName
            _roomNumber = roomNumber

            lblRoomInfo.Text = $"{hostelName} — Room {roomNumber}"
        End Sub

        Private Sub frmRoomGalleryModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadRoomGallery()
        End Sub

        Private Sub LoadRoomGallery()
            _images = _imageRepo.GetImagesByRoom(_roomId)
            pnlThumbnails.Controls.Clear()

            If _images.Count = 0 Then
                picLargePreview.Image = ImageManager.CreateProceduralPlaceholder(600, 400, "No Photographs Uploaded")
                lblCaption.Text = "No photographs uploaded for this room yet. Click '+ Upload Photo' to add one."
                lblCategoryBadge.Visible = False
                btnSetPrimary.Enabled = False
                btnDelete.Enabled = False
                _selectedImage = Nothing
                Return
            End If

            For Each img In _images
                Dim thumbPanel As Panel = CreateThumbnailCard(img)
                pnlThumbnails.Controls.Add(thumbPanel)
            Next

            ' Select primary or first image
            Dim primary = _images.FirstOrDefault(Function(x) x.IsPrimary)
            If primary Is Nothing Then primary = _images(0)

            SelectImage(primary)
        End Sub

        Private Function CreateThumbnailCard(img As RoomImage) As Panel
            Dim card As New Panel With {
                .Size = New Size(200, 100),
                .BackColor = Color.White,
                .Margin = New Padding(0, 0, 0, 10),
                .Cursor = Cursors.Hand,
                .Tag = img
            }

            Dim pic As New PictureBox With {
                .Location = New Point(6, 6),
                .Size = New Size(70, 70),
                .SizeMode = PictureBoxSizeMode.Zoom,
                .BackColor = Color.FromArgb(241, 245, 249),
                .Image = ImageManager.LoadImageSafely(img.ImagePath, 70, 70, "...")
            }

            Dim lblCat As New Label With {
                .Text = img.ImageCategory.ToUpperInvariant(),
                .Font = New Font("Segoe UI Semibold", 7.5F),
                .ForeColor = ThemeManager.NavyPrimary,
                .Location = New Point(82, 8),
                .AutoSize = True
            }

            Dim lblCap As New Label With {
                .Text = If(String.IsNullOrWhiteSpace(img.ImageCaption), "No caption", img.ImageCaption),
                .Font = New Font("Segoe UI", 7.5F),
                .ForeColor = Color.FromArgb(100, 116, 139),
                .Location = New Point(82, 28),
                .Size = New Size(110, 36)
            }

            card.Controls.AddRange({pic, lblCat, lblCap})

            If img.IsPrimary Then
                Dim lblBadge As New Label With {
                    .Text = "★ PRIMARY",
                    .Font = New Font("Segoe UI", 7.0F, FontStyle.Bold),
                    .BackColor = ThemeManager.AcademicGold,
                    .ForeColor = ThemeManager.NavyPrimary,
                    .Location = New Point(82, 68),
                    .Size = New Size(70, 18),
                    .TextAlign = ContentAlignment.MiddleCenter
                }
                card.Controls.Add(lblBadge)
            End If

            Dim clickHandler As EventHandler = Sub(s, e) SelectImage(img)
            AddHandler card.Click, clickHandler
            AddHandler pic.Click, clickHandler
            AddHandler lblCat.Click, clickHandler
            AddHandler lblCap.Click, clickHandler

            Return card
        End Function

        Private Sub SelectImage(img As RoomImage)
            _selectedImage = img
            If img Is Nothing Then Return

            ' Highlight selected thumbnail card
            For Each c As Control In pnlThumbnails.Controls
                Dim pnl = TryCast(c, Panel)
                If pnl IsNot Nothing Then
                    Dim it = TryCast(pnl.Tag, RoomImage)
                    If it IsNot Nothing AndAlso it.ImageId = img.ImageId Then
                        pnl.BackColor = Color.FromArgb(238, 242, 255)
                        pnl.BorderStyle = BorderStyle.FixedSingle
                    Else
                        pnl.BackColor = Color.White
                        pnl.BorderStyle = BorderStyle.None
                    End If
                End If
            Next

            ' Update Large Preview
            picLargePreview.Image = ImageManager.LoadImageSafely(img.ImagePath, 650, 400, "Photograph Preview")
            lblCaption.Text = If(String.IsNullOrWhiteSpace(img.ImageCaption), "(No caption provided)", img.ImageCaption)
            lblCategoryBadge.Text = img.ImageCategory.ToUpperInvariant()
            lblCategoryBadge.Visible = True

            btnSetPrimary.Enabled = Not img.IsPrimary
            btnDelete.Enabled = True
        End Sub

        Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
            Using ofd As New OpenFileDialog()
                ofd.Title = "Select Room Photograph Asset"
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.webp)|*.jpg;*.jpeg;*.png;*.webp|All Files (*.*)|*.*"
                If ofd.ShowDialog(Me) = DialogResult.OK Then
                    Try
                        ' Determine category
                        Dim category As String = "INTERIOR"
                        Dim caption As String = $"Room {_roomNumber} Photograph"

                        ' Copy file to Assets/rooms/ and obtain relative DB path
                        Dim relPath As String = ImageManager.SaveRoomImage(ofd.FileName, _roomId, category)

                        ' Insert DB record
                        Dim isFirstPhoto As Boolean = (_images.Count = 0)
                        Dim newImg As New RoomImage With {
                            .RoomId = _roomId,
                            .ImagePath = relPath,
                            .ImageCategory = category,
                            .ImageCaption = caption,
                            .DisplayOrder = _images.Count + 1,
                            .IsPrimary = isFirstPhoto
                        }

                        _imageRepo.AddImage(newImg)
                        LoadRoomGallery()

                        MessageBox.Show("Room photograph uploaded successfully!", "Gallery Asset Manager", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show($"Upload failed: {ex.Message}", "Error Uploading Image", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End Sub

        Private Sub btnSetPrimary_Click(sender As Object, e As EventArgs) Handles btnSetPrimary.Click
            If _selectedImage Is Nothing Then Return

            Try
                _imageRepo.SetPrimaryImage(_selectedImage.ImageId, _roomId)
                LoadRoomGallery()
                MessageBox.Show("Photograph marked as the primary image for this room.", "Primary Image", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Error setting primary image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
            If _selectedImage Is Nothing Then Return

            Dim confirm = MessageBox.Show("Are you sure you want to delete this room photograph?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Try
                    Dim pathToDelete As String = _selectedImage.ImagePath
                    _imageRepo.DeleteImage(_selectedImage.ImageId)

                    ' Clean up disk asset
                    ImageManager.DeleteAssetFile(pathToDelete)

                    LoadRoomGallery()
                Catch ex As Exception
                    MessageBox.Show($"Error deleting photo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub
    End Class
End Namespace
