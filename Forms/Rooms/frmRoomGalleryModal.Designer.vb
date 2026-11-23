Namespace Forms.Rooms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmRoomGalleryModal
        Inherits System.Windows.Forms.Form

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        Friend WithEvents pnlHeader As System.Windows.Forms.Panel
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblRoomInfo As System.Windows.Forms.Label
        Friend WithEvents pnlThumbnails As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents pnlMainPreview As System.Windows.Forms.Panel
        Friend WithEvents picLargePreview As System.Windows.Forms.PictureBox
        Friend WithEvents lblCaption As System.Windows.Forms.Label
        Friend WithEvents lblCategoryBadge As System.Windows.Forms.Label
        Friend WithEvents pnlActions As System.Windows.Forms.Panel
        Friend WithEvents btnUpload As System.Windows.Forms.Button
        Friend WithEvents btnSetPrimary As System.Windows.Forms.Button
        Friend WithEvents btnDelete As System.Windows.Forms.Button
        Friend WithEvents btnClose As System.Windows.Forms.Button

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblRoomInfo = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlThumbnails = New System.Windows.Forms.FlowLayoutPanel()
            Me.pnlMainPreview = New System.Windows.Forms.Panel()
            Me.lblCategoryBadge = New System.Windows.Forms.Label()
            Me.lblCaption = New System.Windows.Forms.Label()
            Me.picLargePreview = New System.Windows.Forms.PictureBox()
            Me.pnlActions = New System.Windows.Forms.Panel()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.btnDelete = New System.Windows.Forms.Button()
            Me.btnSetPrimary = New System.Windows.Forms.Button()
            Me.btnUpload = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlMainPreview.SuspendLayout()
            CType(Me.picLargePreview, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlActions.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(47, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.lblRoomInfo)
            Me.pnlHeader.Controls.Add(Me.lblTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlHeader.Size = New System.Drawing.Size(920, 60)
            Me.pnlHeader.TabIndex = 0
            '
            'lblRoomInfo
            '
            Me.lblRoomInfo.AutoSize = True
            Me.lblRoomInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblRoomInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblRoomInfo.Location = New System.Drawing.Point(20, 32)
            Me.lblRoomInfo.Name = "lblRoomInfo"
            Me.lblRoomInfo.Size = New System.Drawing.Size(262, 15)
            Me.lblRoomInfo.TabIndex = 1
            Me.lblRoomInfo.Text = "Mount Kenya Hall — Room 101 (Standard 2 Beds)"
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(20, 10)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(267, 21)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "ROOM PHOTOGRAPH GALLERY"
            '
            'pnlThumbnails
            '
            Me.pnlThumbnails.AutoScroll = True
            Me.pnlThumbnails.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlThumbnails.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlThumbnails.Location = New System.Drawing.Point(0, 60)
            Me.pnlThumbnails.Name = "pnlThumbnails"
            Me.pnlThumbnails.Padding = New System.Windows.Forms.Padding(12)
            Me.pnlThumbnails.Size = New System.Drawing.Size(230, 480)
            Me.pnlThumbnails.TabIndex = 1
            '
            'pnlMainPreview
            '
            Me.pnlMainPreview.BackColor = System.Drawing.Color.White
            Me.pnlMainPreview.Controls.Add(Me.lblCategoryBadge)
            Me.pnlMainPreview.Controls.Add(Me.lblCaption)
            Me.pnlMainPreview.Controls.Add(Me.picLargePreview)
            Me.pnlMainPreview.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlMainPreview.Location = New System.Drawing.Point(230, 60)
            Me.pnlMainPreview.Name = "pnlMainPreview"
            Me.pnlMainPreview.Padding = New System.Windows.Forms.Padding(16)
            Me.pnlMainPreview.Size = New System.Drawing.Size(690, 480)
            Me.pnlMainPreview.TabIndex = 2
            '
            'lblCategoryBadge
            '
            Me.lblCategoryBadge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblCategoryBadge.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(47, Byte), Integer))
            Me.lblCategoryBadge.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblCategoryBadge.ForeColor = System.Drawing.Color.White
            Me.lblCategoryBadge.Location = New System.Drawing.Point(544, 442)
            Me.lblCategoryBadge.Name = "lblCategoryBadge"
            Me.lblCategoryBadge.Size = New System.Drawing.Size(130, 24)
            Me.lblCategoryBadge.TabIndex = 2
            Me.lblCategoryBadge.Text = "INTERIOR"
            Me.lblCategoryBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCaption
            '
            Me.lblCaption.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblCaption.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
            Me.lblCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblCaption.Location = New System.Drawing.Point(16, 442)
            Me.lblCaption.Name = "lblCaption"
            Me.lblCaption.Size = New System.Drawing.Size(522, 24)
            Me.lblCaption.TabIndex = 1
            Me.lblCaption.Text = "Full room view showing study desks and window light"
            '
            'picLargePreview
            '
            Me.picLargePreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.picLargePreview.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.picLargePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.picLargePreview.Location = New System.Drawing.Point(16, 16)
            Me.picLargePreview.Name = "picLargePreview"
            Me.picLargePreview.Size = New System.Drawing.Size(658, 416)
            Me.picLargePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picLargePreview.TabIndex = 0
            Me.picLargePreview.TabStop = False
            '
            'pnlActions
            '
            Me.pnlActions.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlActions.Controls.Add(Me.btnClose)
            Me.pnlActions.Controls.Add(Me.btnDelete)
            Me.pnlActions.Controls.Add(Me.btnSetPrimary)
            Me.pnlActions.Controls.Add(Me.btnUpload)
            Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlActions.Location = New System.Drawing.Point(0, 540)
            Me.pnlActions.Name = "pnlActions"
            Me.pnlActions.Padding = New System.Windows.Forms.Padding(16, 10, 16, 10)
            Me.pnlActions.Size = New System.Drawing.Size(920, 52)
            Me.pnlActions.TabIndex = 3
            '
            'btnClose
            '
            Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
            Me.btnClose.FlatAppearance.BorderSize = 0
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnClose.Location = New System.Drawing.Point(825, 10)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(83, 32)
            Me.btnClose.TabIndex = 3
            Me.btnClose.Text = "Close"
            Me.btnClose.UseVisualStyleBackColor = False
            '
            'btnDelete
            '
            Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
            Me.btnDelete.FlatAppearance.BorderSize = 0
            Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnDelete.ForeColor = System.Drawing.Color.White
            Me.btnDelete.Location = New System.Drawing.Point(308, 10)
            Me.btnDelete.Name = "btnDelete"
            Me.btnDelete.Size = New System.Drawing.Size(120, 32)
            Me.btnDelete.TabIndex = 2
            Me.btnDelete.Text = "🗑️ Delete Photo"
            Me.btnDelete.UseVisualStyleBackColor = False
            '
            'btnSetPrimary
            '
            Me.btnSetPrimary.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.btnSetPrimary.FlatAppearance.BorderSize = 0
            Me.btnSetPrimary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnSetPrimary.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnSetPrimary.ForeColor = System.Drawing.Color.White
            Me.btnSetPrimary.Location = New System.Drawing.Point(162, 10)
            Me.btnSetPrimary.Name = "btnSetPrimary"
            Me.btnSetPrimary.Size = New System.Drawing.Size(140, 32)
            Me.btnSetPrimary.TabIndex = 1
            Me.btnSetPrimary.Text = "⭐ Set As Primary"
            Me.btnSetPrimary.UseVisualStyleBackColor = False
            '
            'btnUpload
            '
            Me.btnUpload.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(47, Byte), Integer))
            Me.btnUpload.FlatAppearance.BorderSize = 0
            Me.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnUpload.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnUpload.ForeColor = System.Drawing.Color.White
            Me.btnUpload.Location = New System.Drawing.Point(16, 10)
            Me.btnUpload.Name = "btnUpload"
            Me.btnUpload.Size = New System.Drawing.Size(140, 32)
            Me.btnUpload.TabIndex = 0
            Me.btnUpload.Text = "📷 Upload Photo"
            Me.btnUpload.UseVisualStyleBackColor = False
            '
            'frmRoomGalleryModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(920, 592)
            Me.Controls.Add(Me.pnlMainPreview)
            Me.Controls.Add(Me.pnlThumbnails)
            Me.Controls.Add(Me.pnlActions)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmRoomGalleryModal"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Room Photograph Gallery"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlMainPreview.ResumeLayout(False)
            CType(Me.picLargePreview, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlActions.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Sub
    End Class
End Namespace

