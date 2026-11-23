Namespace Forms.Maintenance
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmResolveMaintenanceModal
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
        Friend WithEvents lblIncidentInfo As System.Windows.Forms.Label
        Friend WithEvents lblNotes As System.Windows.Forms.Label
        Friend WithEvents txtNotes As System.Windows.Forms.TextBox
        Friend WithEvents chkRestoreBed As System.Windows.Forms.CheckBox
        Friend WithEvents btnConfirm As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblSubtitle = New System.Windows.Forms.Label()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.lblIncidentInfo = New System.Windows.Forms.Label()
            Me.lblNotes = New System.Windows.Forms.Label()
            Me.txtNotes = New System.Windows.Forms.TextBox()
            Me.chkRestoreBed = New System.Windows.Forms.CheckBox()
            Me.btnConfirm = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlHeader
            '
            Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.pnlHeader.Controls.Add(Me.lblSubtitle)
            Me.pnlHeader.Controls.Add(Me.lblTitle)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
            Me.pnlHeader.Size = New System.Drawing.Size(520, 60)
            Me.pnlHeader.TabIndex = 0
            '
            'lblSubtitle
            '
            Me.lblSubtitle.AutoSize = True
            Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(229, Byte), Integer))
            Me.lblSubtitle.Location = New System.Drawing.Point(20, 32)
            Me.lblSubtitle.Name = "lblSubtitle"
            Me.lblSubtitle.Size = New System.Drawing.Size(271, 15)
            Me.lblSubtitle.TabIndex = 1
            Me.lblSubtitle.Text = "Certify defect repair and restore bed-space status"
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblTitle.ForeColor = System.Drawing.Color.White
            Me.lblTitle.Location = New System.Drawing.Point(20, 10)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(252, 21)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "RESOLVE MAINTENANCE ISSUE"
            '
            'lblIncidentInfo
            '
            Me.lblIncidentInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.lblIncidentInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.lblIncidentInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblIncidentInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
            Me.lblIncidentInfo.Location = New System.Drawing.Point(20, 76)
            Me.lblIncidentInfo.Name = "lblIncidentInfo"
            Me.lblIncidentInfo.Padding = New System.Windows.Forms.Padding(10)
            Me.lblIncidentInfo.Size = New System.Drawing.Size(480, 48)
            Me.lblIncidentInfo.TabIndex = 1
            Me.lblIncidentInfo.Text = "Incident #1 — Window Latch & Hinge Replacement" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Facility: MKH - Rm 301 (MKH-301-B1)"
            '
            'lblNotes
            '
            Me.lblNotes.AutoSize = True
            Me.lblNotes.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblNotes.Location = New System.Drawing.Point(20, 138)
            Me.lblNotes.Name = "lblNotes"
            Me.lblNotes.Size = New System.Drawing.Size(168, 15)
            Me.lblNotes.TabIndex = 2
            Me.lblNotes.Text = "Resolution & Repair Notes *"
            '
            'txtNotes
            '
            Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.txtNotes.Location = New System.Drawing.Point(20, 156)
            Me.txtNotes.Multiline = True
            Me.txtNotes.Name = "txtNotes"
            Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtNotes.Size = New System.Drawing.Size(480, 72)
            Me.txtNotes.TabIndex = 3
            '
            'chkRestoreBed
            '
            Me.chkRestoreBed.AutoSize = True
            Me.chkRestoreBed.Checked = True
            Me.chkRestoreBed.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkRestoreBed.Font = New System.Drawing.Font("Segoe UI Semibold", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.chkRestoreBed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.chkRestoreBed.Location = New System.Drawing.Point(20, 240)
            Me.chkRestoreBed.Name = "chkRestoreBed"
            Me.chkRestoreBed.Size = New System.Drawing.Size(394, 19)
            Me.chkRestoreBed.TabIndex = 4
            Me.chkRestoreBed.Text = "✓ Automatically restore bed status to AVAILABLE (if no active resident)"
            Me.chkRestoreBed.UseVisualStyleBackColor = True
            '
            'btnConfirm
            '
            Me.btnConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnConfirm.FlatAppearance.BorderSize = 0
            Me.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnConfirm.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnConfirm.ForeColor = System.Drawing.Color.White
            Me.btnConfirm.Location = New System.Drawing.Point(260, 280)
            Me.btnConfirm.Name = "btnConfirm"
            Me.btnConfirm.Size = New System.Drawing.Size(150, 36)
            Me.btnConfirm.TabIndex = 5
            Me.btnConfirm.Text = "✅ Certify & Resolve"
            Me.btnConfirm.UseVisualStyleBackColor = False
            '
            'btnCancel
            '
            Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
            Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnCancel.Location = New System.Drawing.Point(420, 280)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(80, 36)
            Me.btnCancel.TabIndex = 6
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = False
            '
            'frmResolveMaintenanceModal
            '
            Me.AcceptButton = Me.btnConfirm
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(520, 335)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnConfirm)
            Me.Controls.Add(Me.chkRestoreBed)
            Me.Controls.Add(Me.txtNotes)
            Me.Controls.Add(Me.lblNotes)
            Me.Controls.Add(Me.lblIncidentInfo)
            Me.Controls.Add(Me.pnlHeader)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmResolveMaintenanceModal"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Resolve Maintenance Issue"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub
    End Class
End Namespace

