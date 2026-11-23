Namespace Forms.Reports
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmReportViewerModal
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
        Friend WithEvents lblSubtitle As System.Windows.Forms.Label
        Friend WithEvents pnlToolbar As System.Windows.Forms.Panel
        Friend WithEvents btnPrint As System.Windows.Forms.Button
        Friend WithEvents btnExportHtml As System.Windows.Forms.Button
        Friend WithEvents btnZoomIn As System.Windows.Forms.Button
        Friend WithEvents btnZoomOut As System.Windows.Forms.Button
        Friend WithEvents btnFitPage As System.Windows.Forms.Button
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents previewControl As System.Windows.Forms.PrintPreviewControl

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.pnlToolbar = New System.Windows.Forms.Panel()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.btnExportHtml = New System.Windows.Forms.Button()
            Me.btnPrint = New System.Windows.Forms.Button()
            Me.btnFitPage = New System.Windows.Forms.Button()
            Me.btnZoomOut = New System.Windows.Forms.Button()
            Me.btnZoomIn = New System.Windows.Forms.Button()
            Me.previewControl = New System.Windows.Forms.PrintPreviewControl()
            Me.pnlHeader.SuspendLayout()
            Me.pnlToolbar.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(47, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.lblSubtitle)
            Me.pnlHeader.Controls.Add(Me.lblTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlHeader.Size = New System.Drawing.Size(1000, 60)
            Me.pnlHeader.TabIndex = 0
            '
            'lblSubtitle
            '
            Me.lblSubtitle.AutoSize = True
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(20, 32)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(262, 15)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Official Document Preview & Distribution Terminal"
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(20, 10)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(261, 21)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "INSTITUTIONAL REPORT VIEWER"
            '
            'pnlToolbar
            '
            Me.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.pnlToolbar.Controls.Add(Me.btnClose)
            Me.pnlToolbar.Controls.Add(Me.btnExportHtml)
            Me.pnlToolbar.Controls.Add(Me.btnPrint)
            Me.pnlToolbar.Controls.Add(Me.btnFitPage)
            Me.pnlToolbar.Controls.Add(Me.btnZoomOut)
            Me.pnlToolbar.Controls.Add(Me.btnZoomIn)
            Me.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlToolbar.Location = New System.Drawing.Point(0, 60)
            Me.pnlToolbar.Name = "pnlToolbar"
            Me.pnlToolbar.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
            Me.pnlToolbar.Size = New System.Drawing.Size(1000, 46)
            Me.pnlToolbar.TabIndex = 1
            '
            'btnClose
            '
            Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
            Me.btnClose.FlatAppearance.BorderSize = 0
            Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
            Me.btnClose.Location = New System.Drawing.Point(905, 8)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(83, 30)
            Me.btnClose.TabIndex = 5
            Me.btnClose.Text = "Close"
            Me.btnClose.UseVisualStyleBackColor = False
            '
            'btnExportHtml
            '
            Me.btnExportHtml.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(110, Byte), Integer))
            Me.btnExportHtml.FlatAppearance.BorderSize = 0
            Me.btnExportHtml.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnExportHtml.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnExportHtml.ForeColor = System.Drawing.Color.White
            Me.btnExportHtml.Location = New System.Drawing.Point(120, 8)
            Me.btnExportHtml.Name = "btnExportHtml"
            Me.btnExportHtml.Size = New System.Drawing.Size(140, 30)
            Me.btnExportHtml.TabIndex = 1
            Me.btnExportHtml.Text = "🌐 Open in Browser"
            Me.btnExportHtml.UseVisualStyleBackColor = False
            '
            'btnPrint
            '
            Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(47, Byte), Integer))
            Me.btnPrint.FlatAppearance.BorderSize = 0
            Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnPrint.ForeColor = System.Drawing.Color.White
            Me.btnPrint.Location = New System.Drawing.Point(12, 8)
            Me.btnPrint.Name = "btnPrint"
            Me.btnPrint.Size = New System.Drawing.Size(100, 30)
            Me.btnPrint.TabIndex = 0
            Me.btnPrint.Text = "🖨️ Print"
            Me.btnPrint.UseVisualStyleBackColor = False
            '
            'btnFitPage
            '
            Me.btnFitPage.BackColor = System.Drawing.Color.White
            Me.btnFitPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnFitPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnFitPage.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnFitPage.Location = New System.Drawing.Point(370, 8)
            Me.btnFitPage.Name = "btnFitPage"
            Me.btnFitPage.Size = New System.Drawing.Size(75, 30)
            Me.btnFitPage.TabIndex = 4
            Me.btnFitPage.Text = "Fit Page"
            Me.btnFitPage.UseVisualStyleBackColor = False
            '
            'btnZoomOut
            '
            Me.btnZoomOut.BackColor = System.Drawing.Color.White
            Me.btnZoomOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoomOut.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnZoomOut.Location = New System.Drawing.Point(325, 8)
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.Size = New System.Drawing.Size(38, 30)
            Me.btnZoomOut.TabIndex = 3
            Me.btnZoomOut.Text = "🔍-"
            Me.btnZoomOut.UseVisualStyleBackColor = False
            '
            'btnZoomIn
            '
            Me.btnZoomIn.BackColor = System.Drawing.Color.White
            Me.btnZoomIn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoomIn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnZoomIn.Location = New System.Drawing.Point(280, 8)
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.Size = New System.Drawing.Size(38, 30)
            Me.btnZoomIn.TabIndex = 2
            Me.btnZoomIn.Text = "🔍+"
            Me.btnZoomIn.UseVisualStyleBackColor = False
            '
            'previewControl
            '
            Me.previewControl.AutoZoom = True
            Me.previewControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.previewControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.previewControl.Location = New System.Drawing.Point(0, 106)
            Me.previewControl.Name = "previewControl"
            Me.previewControl.Size = New System.Drawing.Size(1000, 594)
            Me.previewControl.TabIndex = 2
            '
            'frmReportViewerModal
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(1000, 700)
            Me.Controls.Add(Me.previewControl)
            Me.Controls.Add(Me.pnlToolbar)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.MinimizeBox = False
            Me.Name = "frmReportViewerModal"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Official Institutional Report Viewer"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlToolbar.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Sub
    End Class
End Namespace

