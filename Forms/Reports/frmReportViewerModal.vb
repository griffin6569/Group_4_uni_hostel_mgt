Imports System.Diagnostics
Imports System.Drawing.Printing
Imports System.IO
Imports University_hostel_mgt.Services

Namespace Forms.Reports
    ''' <summary>
    ''' Code-behind for the Institutional Report Viewer dialog.
    ''' Coordinates high-fidelity document previewing, physical printing, and web-based archival export.
    ''' </summary>
    Public Class frmReportViewerModal
        Private ReadOnly _generator As InstitutionalReportGenerator
        Private ReadOnly _printDoc As PrintDocument
        Private ReadOnly _htmlContent As String
        Private ReadOnly _reportTitle As String

        Public Sub New(generator As InstitutionalReportGenerator, reportTitle As String)
            InitializeComponent()
            _generator = generator
            _reportTitle = reportTitle
            _printDoc = _generator.CreatePrintDocument()
            _htmlContent = _generator.GenerateHtmlReport()

            lblTitle.Text = reportTitle.ToUpperInvariant()
            previewControl.Document = _printDoc
        End Sub

        Private Sub frmReportViewerModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            previewControl.Zoom = 1.0
            previewControl.AutoZoom = True
        End Sub

        Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
            Using dlg As New PrintDialog()
                dlg.Document = _printDoc
                dlg.UseEXDialog = True
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    Try
                        _printDoc.Print()
                    Catch ex As Exception
                        MessageBox.Show($"Printing error: {ex.Message}", "Print Document Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End Sub

        Private Sub btnExportHtml_Click(sender As Object, e As EventArgs) Handles btnExportHtml.Click
            Try
                Dim tempDir As String = Path.Combine(Path.GetTempPath(), "UniversityHostelReports")
                If Not Directory.Exists(tempDir) Then Directory.CreateDirectory(tempDir)

                Dim safeTitle As String = String.Join("_", _reportTitle.Split(Path.GetInvalidFileNameChars()))
                Dim fileName As String = $"{safeTitle}_{DateTime.Now:yyyyMMdd_HHmmss}.html"
                Dim filePath As String = Path.Combine(tempDir, fileName)

                File.WriteAllText(filePath, _htmlContent)

                Dim psi As New ProcessStartInfo With {
                    .FileName = filePath,
                    .UseShellExecute = True
                }
                Process.Start(psi)
            Catch ex As Exception
                MessageBox.Show($"Export error: {ex.Message}", "Export Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnZoomIn_Click(sender As Object, e As EventArgs) Handles btnZoomIn.Click
            previewControl.AutoZoom = False
            previewControl.Zoom = Math.Min(3.0, previewControl.Zoom + 0.2)
        End Sub

        Private Sub btnZoomOut_Click(sender As Object, e As EventArgs) Handles btnZoomOut.Click
            previewControl.AutoZoom = False
            previewControl.Zoom = Math.Max(0.2, previewControl.Zoom - 0.2)
        End Sub

        Private Sub btnFitPage_Click(sender As Object, e As EventArgs) Handles btnFitPage.Click
            previewControl.AutoZoom = True
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub
    End Class
End Namespace

