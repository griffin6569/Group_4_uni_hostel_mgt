Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmSplash
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

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.pnlTopAccent = New System.Windows.Forms.Panel()
            Me.picCrest = New System.Windows.Forms.PictureBox()
            Me.lblUnivName = New System.Windows.Forms.Label()
            Me.lblAppTitle = New System.Windows.Forms.Label()
            Me.lblDirectorate = New System.Windows.Forms.Label()
            Me.pnlCard = New University_hostel_mgt.Controls.GlassPanel()
            Me.btnExit = New University_hostel_mgt.Controls.GlassButton()
            Me.btnRetry = New University_hostel_mgt.Controls.GlassButton()
            Me.lblStatusTicker = New System.Windows.Forms.Label()
            Me.pnlProgressTrack = New System.Windows.Forms.Panel()
            Me.lblPercent = New System.Windows.Forms.Label()
            Me.lblProgressHeader = New System.Windows.Forms.Label()
            Me.lblFooter = New System.Windows.Forms.Label()
            Me.tmrAnimation = New System.Windows.Forms.Timer(Me.components)
            CType(Me.picCrest, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlCard.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopAccent
            '
            Me.pnlTopAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlTopAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopAccent.Name = "pnlTopAccent"
            Me.pnlTopAccent.Size = New System.Drawing.Size(760, 3)
            Me.pnlTopAccent.TabIndex = 0
            '
            'picCrest
            '
            Me.picCrest.BackColor = System.Drawing.Color.Transparent
            Me.picCrest.Location = New System.Drawing.Point(328, 22)
            Me.picCrest.Name = "picCrest"
            Me.picCrest.Size = New System.Drawing.Size(104, 104)
            Me.picCrest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picCrest.TabIndex = 1
            Me.picCrest.TabStop = False
            '
            'lblUnivName
            '
            Me.lblUnivName.BackColor = System.Drawing.Color.Transparent
            Me.lblUnivName.Font = New System.Drawing.Font("Segoe UI", 11.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUnivName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.lblUnivName.Location = New System.Drawing.Point(40, 134)
            Me.lblUnivName.Name = "lblUnivName"
            Me.lblUnivName.Size = New System.Drawing.Size(680, 24)
            Me.lblUnivName.TabIndex = 2
            Me.lblUnivName.Text = "DEDAN KIMATHI UNIVERSITY OF TECHNOLOGY"
            Me.lblUnivName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblUnivName.UseMnemonic = False
            '
            'lblAppTitle
            '
            Me.lblAppTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblAppTitle.ForeColor = System.Drawing.Color.White
            Me.lblAppTitle.Location = New System.Drawing.Point(40, 160)
            Me.lblAppTitle.Name = "lblAppTitle"
            Me.lblAppTitle.Size = New System.Drawing.Size(680, 32)
            Me.lblAppTitle.TabIndex = 3
            Me.lblAppTitle.Text = "HOSTEL MANAGEMENT SYSTEM"
            Me.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblAppTitle.UseMnemonic = False
            '
            'lblDirectorate
            '
            Me.lblDirectorate.BackColor = System.Drawing.Color.Transparent
            Me.lblDirectorate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblDirectorate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblDirectorate.Location = New System.Drawing.Point(40, 194)
            Me.lblDirectorate.Name = "lblDirectorate"
            Me.lblDirectorate.Size = New System.Drawing.Size(680, 20)
            Me.lblDirectorate.TabIndex = 4
            Me.lblDirectorate.Text = "Directorate of Student Accommodation • Residential Portal"
            Me.lblDirectorate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblDirectorate.UseMnemonic = False
            '
            'pnlCard
            '
            Me.pnlCard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.pnlCard.BorderWidth = 1.0!
            Me.pnlCard.Controls.Add(Me.btnExit)
            Me.pnlCard.Controls.Add(Me.btnRetry)
            Me.pnlCard.Controls.Add(Me.lblStatusTicker)
            Me.pnlCard.Controls.Add(Me.pnlProgressTrack)
            Me.pnlCard.Controls.Add(Me.lblPercent)
            Me.pnlCard.Controls.Add(Me.lblProgressHeader)
            Me.pnlCard.CornerRadius = 14
            Me.pnlCard.FillColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.pnlCard.GlowColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.pnlCard.Location = New System.Drawing.Point(60, 226)
            Me.pnlCard.Name = "pnlCard"
            Me.pnlCard.Padding = New System.Windows.Forms.Padding(20)
            Me.pnlCard.Size = New System.Drawing.Size(640, 115)
            Me.pnlCard.TabIndex = 5
            '
            'btnExit
            '
            Me.btnExit.CornerRadius = 10
            Me.btnExit.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnExit.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnExit.HoverColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
            Me.btnExit.Location = New System.Drawing.Point(325, 62)
            Me.btnExit.Name = "btnExit"
            Me.btnExit.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.btnExit.Size = New System.Drawing.Size(100, 32)
            Me.btnExit.TabIndex = 10
            Me.btnExit.Text = "Exit"
            Me.btnExit.Visible = False
            '
            'btnRetry
            '
            Me.btnRetry.CornerRadius = 10
            Me.btnRetry.CustomTextColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.btnRetry.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnRetry.HoverColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.btnRetry.Location = New System.Drawing.Point(215, 62)
            Me.btnRetry.Name = "btnRetry"
            Me.btnRetry.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.btnRetry.Size = New System.Drawing.Size(100, 32)
            Me.btnRetry.TabIndex = 9
            Me.btnRetry.Text = "Retry"
            Me.btnRetry.Visible = False
            '
            'lblStatusTicker
            '
            Me.lblStatusTicker.BackColor = System.Drawing.Color.Transparent
            Me.lblStatusTicker.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusTicker.ForeColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.lblStatusTicker.Location = New System.Drawing.Point(24, 66)
            Me.lblStatusTicker.Name = "lblStatusTicker"
            Me.lblStatusTicker.Size = New System.Drawing.Size(592, 24)
            Me.lblStatusTicker.TabIndex = 3
            Me.lblStatusTicker.Text = "Starting workspace..."
            '
            'pnlProgressTrack
            '
            Me.pnlProgressTrack.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(59, Byte), Integer))
            Me.pnlProgressTrack.Location = New System.Drawing.Point(24, 42)
            Me.pnlProgressTrack.Name = "pnlProgressTrack"
            Me.pnlProgressTrack.Size = New System.Drawing.Size(592, 12)
            Me.pnlProgressTrack.TabIndex = 2
            '
            'lblPercent
            '
            Me.lblPercent.BackColor = System.Drawing.Color.Transparent
            Me.lblPercent.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPercent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
            Me.lblPercent.Location = New System.Drawing.Point(516, 14)
            Me.lblPercent.Name = "lblPercent"
            Me.lblPercent.Size = New System.Drawing.Size(100, 22)
            Me.lblPercent.TabIndex = 1
            Me.lblPercent.Text = "0%"
            Me.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblProgressHeader
            '
            Me.lblProgressHeader.AutoSize = True
            Me.lblProgressHeader.BackColor = System.Drawing.Color.Transparent
            Me.lblProgressHeader.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblProgressHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblProgressHeader.Location = New System.Drawing.Point(24, 18)
            Me.lblProgressHeader.Name = "lblProgressHeader"
            Me.lblProgressHeader.Size = New System.Drawing.Size(160, 15)
            Me.lblProgressHeader.TabIndex = 0
            Me.lblProgressHeader.Text = "STARTING HOSTEL PORTAL"
            '
            'lblFooter
            '
            Me.lblFooter.BackColor = System.Drawing.Color.Transparent
            Me.lblFooter.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblFooter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
            Me.lblFooter.Location = New System.Drawing.Point(40, 364)
            Me.lblFooter.Name = "lblFooter"
            Me.lblFooter.Size = New System.Drawing.Size(680, 20)
            Me.lblFooter.TabIndex = 6
            Me.lblFooter.Text = "Dedan Kimathi University of Technology • Directorate of Accommodation"
            Me.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'tmrAnimation
            '
            Me.tmrAnimation.Interval = 16
            '
            'frmSplash
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(760, 410)
            Me.Controls.Add(Me.lblFooter)
            Me.Controls.Add(Me.pnlCard)
            Me.Controls.Add(Me.lblDirectorate)
            Me.Controls.Add(Me.lblAppTitle)
            Me.Controls.Add(Me.lblUnivName)
            Me.Controls.Add(Me.picCrest)
            Me.Controls.Add(Me.pnlTopAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmSplash"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Hostel Management System - Dedan Kimathi University of Technology"
            CType(Me.picCrest, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlCard.ResumeLayout(False)
            Me.pnlCard.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopAccent As System.Windows.Forms.Panel
        Friend WithEvents picCrest As System.Windows.Forms.PictureBox
        Friend WithEvents lblUnivName As System.Windows.Forms.Label
        Friend WithEvents lblAppTitle As System.Windows.Forms.Label
        Friend WithEvents lblDirectorate As System.Windows.Forms.Label
        Friend WithEvents pnlCard As University_hostel_mgt.Controls.GlassPanel
        Friend WithEvents lblProgressHeader As System.Windows.Forms.Label
        Friend WithEvents lblPercent As System.Windows.Forms.Label
        Friend WithEvents pnlProgressTrack As System.Windows.Forms.Panel
        Friend WithEvents lblStatusTicker As System.Windows.Forms.Label
        Friend WithEvents btnRetry As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents btnExit As University_hostel_mgt.Controls.GlassButton
        Friend WithEvents lblFooter As System.Windows.Forms.Label
        Friend WithEvents tmrAnimation As System.Windows.Forms.Timer
    End Class
End Namespace