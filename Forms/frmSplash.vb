Imports System
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports University_hostel_mgt.Data
Imports University_hostel_mgt.Helpers

Namespace Forms
    ''' <summary>
    ''' Ultra-modern academic launch sequence featuring silky 60 FPS lerp-interpolated progress,
    ''' dynamic specular shimmer sweep, glowing leading-edge pulse, official Dedan Kimathi University
    ''' of Technology branding, and streamlined service bootloader.
    ''' </summary>
    Public Class frmSplash
        Private _currentProgress As Single = 0.0F
        Private _targetProgress As Single = 10.0F
        Private _shimmerOffset As Single = -120.0F
        Private _pulsePhase As Single = 0.0F
        Private _isInitializing As Boolean = False
        Private _initComplete As Boolean = False
        Private _initError As Boolean = False

        Public Property AutoInitialize As Boolean = True

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            ThemeManager.EnableDoubleBuffering(Me)
            ThemeManager.EnableDoubleBuffering(pnlCard)
            ThemeManager.EnableDoubleBuffering(pnlProgressTrack)
            ThemeManager.EnableDoubleBuffering(picCrest)

            picCrest.Image = ThemeManager.GetInstitutionalLogo()
            tmrAnimation.Start()

            If AutoInitialize Then
                StartInitializationSequence()
            End If
        End Sub

        ''' <summary>
        ''' Renders the progress bar in an active loading state for documentation and visual review.
        ''' </summary>
        Public Sub SetDiagnosticStateForPreview()
            _currentProgress = 82.0F
            _targetProgress = 82.0F
            lblPercent.Text = "82%"
            lblStatusTicker.Text = "Loading residential units & services..."
            lblStatusTicker.ForeColor = ThemeManager.EmeraldPrimary
            pnlProgressTrack.Invalidate()
        End Sub

        ' 1. Custom Paint: Dedan Kimathi University of Technology Crest Insignia
        Private Sub picCrest_Paint(sender As Object, e As PaintEventArgs) Handles picCrest.Paint
            ThemeManager.DrawAcademicCrest(e.Graphics, New RectangleF(4, 4, picCrest.Width - 8, picCrest.Height - 8),
                                           ThemeManager.AcademicGold, ThemeManager.NavyDark)
        End Sub

        ' 2. Custom Paint: Ultra-Premium Smooth Rounded Progress Bar with Multi-stop Gradient,
        ' Specular Shimmer Sweep, and Glowing Leading-Edge Pulse Beacon
        Private Sub pnlProgressTrack_Paint(sender As Object, e As PaintEventArgs) Handles pnlProgressTrack.Paint
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic

            Dim trackRect As New RectangleF(0, 0, pnlProgressTrack.Width - 1, pnlProgressTrack.Height - 1)
            Dim radius As Single = trackRect.Height / 2.0F

            ' A. Rounded dark track background well
            Using trackPath As GraphicsPath = ThemeManager.CreateRoundedRectangle(trackRect, radius)
                Using trackBrush As New SolidBrush(Color.FromArgb(14, 22, 35))
                    g.FillPath(trackBrush, trackPath)
                End Using
                Using borderPen As New Pen(Color.FromArgb(38, 255, 255, 255), 1.0F)
                    g.DrawPath(borderPen, trackPath)
                End Using
            End Using

            ' B. Rounded progress fill
            Dim fillWidth As Single = (Math.Max(0.0F, Math.Min(100.0F, _currentProgress)) / 100.0F) * trackRect.Width
            If fillWidth > 4.0F Then
                Dim fillRect As New RectangleF(0, 0, fillWidth, trackRect.Height)
                Using fillPath As GraphicsPath = ThemeManager.CreateRoundedRectangle(fillRect, radius)
                    If _initError Then
                        Using errorBrush As New SolidBrush(ThemeManager.Coral)
                            g.FillPath(errorBrush, fillPath)
                        End Using
                    Else
                        ' Rich 3-stop multi-tone gradient: Sapphire Ice (#38BDF8) -> Emerald (#10B981) -> Heraldic Gold (#F59E0B)
                        Using gradBrush As New LinearGradientBrush(
                            New PointF(0, 0),
                            New PointF(trackRect.Width, 0),
                            Color.FromArgb(56, 189, 248),
                            Color.FromArgb(245, 158, 11))

                            Dim colorBlend As New ColorBlend(3)
                            colorBlend.Colors = {
                                Color.FromArgb(56, 189, 248),
                                Color.FromArgb(16, 185, 129),
                                Color.FromArgb(245, 158, 11)
                            }
                            colorBlend.Positions = {0.0F, 0.55F, 1.0F}
                            gradBrush.InterpolationColors = colorBlend

                            g.FillPath(gradBrush, fillPath)
                        End Using

                        ' C. Dynamic specular shimmer light sweep
                        Dim shimmerWidth As Single = 100.0F
                        Dim shimmerRect As New RectangleF(_shimmerOffset, 0, shimmerWidth, trackRect.Height)
                        g.SetClip(fillPath)
                        Using shimmerBrush As New LinearGradientBrush(
                            shimmerRect,
                            Color.Transparent,
                            Color.FromArgb(180, 255, 255, 255),
                            LinearGradientMode.Horizontal)
                            Dim blend As New ColorBlend(3)
                            blend.Colors = {Color.FromArgb(0, 255, 255, 255), Color.FromArgb(190, 255, 255, 255), Color.FromArgb(0, 255, 255, 255)}
                            blend.Positions = {0.0F, 0.5F, 1.0F}
                            shimmerBrush.InterpolationColors = blend
                            g.FillRectangle(shimmerBrush, shimmerRect)
                        End Using
                        g.ResetClip()

                        ' D. Glowing neon pulse beacon at the leading edge
                        Dim pulseAlpha As Integer = CInt(160 + 80 * Math.Sin(_pulsePhase))
                        pulseAlpha = Math.Max(0, Math.Min(255, pulseAlpha))
                        Dim beaconRadius As Single = trackRect.Height * 0.9F
                        Dim beaconX As Single = fillWidth - (beaconRadius / 2.0F)
                        Dim beaconY As Single = (trackRect.Height - beaconRadius) / 2.0F

                        Using beaconBrush As New SolidBrush(Color.FromArgb(pulseAlpha, 255, 255, 255))
                            g.FillEllipse(beaconBrush, beaconX, beaconY, beaconRadius, beaconRadius)
                        End Using
                    End If
                End Using
            End If
        End Sub

        ' 3. Silky 60 FPS Lerp & Easing Animation Engine
        Private Sub tmrAnimation_Tick(sender As Object, e As EventArgs) Handles tmrAnimation.Tick
            ' Smooth exponential lerp toward target
            Dim delta As Single = _targetProgress - _currentProgress
            If Math.Abs(delta) > 0.02F Then
                _currentProgress += delta * 0.10F
                If Math.Abs(_targetProgress - _currentProgress) < 0.05F Then
                    _currentProgress = _targetProgress
                End If
            End If

            ' Specular highlight sweep loop
            _shimmerOffset += 5.5F
            If _shimmerOffset > pnlProgressTrack.Width + 140.0F Then
                _shimmerOffset = -120.0F
            End If

            ' Breathing pulse phase
            _pulsePhase += 0.12F
            If _pulsePhase > CSng(Math.PI * 2) Then
                _pulsePhase -= CSng(Math.PI * 2)
            End If

            lblPercent.Text = $"{CInt(Math.Round(Math.Min(100.0F, Math.Max(0.0F, _currentProgress))))}%"
            pnlProgressTrack.Invalidate()
        End Sub

        ' 4. Clean, Streamlined Service Initialization Sequence
        Private Async Sub StartInitializationSequence()
            If _isInitializing Then Return
            _isInitializing = True
            _initError = False
            btnRetry.Visible = False
            btnExit.Visible = False
            lblStatusTicker.ForeColor = Color.FromArgb(56, 189, 248)

            ' Phase 1: Environment & Configuration
            UpdateStatus("Starting workspace...", 22.0F)
            Await Task.Delay(260)
            DotEnv.Load()

            ' Phase 2: Relational Database Pipeline
            UpdateStatus("Connecting to database...", 48.0F)
            Await Task.Delay(220)

            Dim dbTest = Await DatabaseConnection.TestConnectionAsync()
            If Not dbTest.Success Then
                HandleInitializationFailure("Database Connection Failed", dbTest.Message)
                Return
            End If

            ' Phase 3: Academic Catalog & Semester Resolution
            UpdateStatus("Loading academic catalog...", 72.0F)
            Await Task.Delay(240)

            Try
                Dim sql As String = "SELECT semester_name, academic_year FROM dbo.semesters WHERE status = 'CURRENT';"
                Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql)
                If dt.Rows.Count > 0 Then
                    Dim semName = dt.Rows(0)("semester_name").ToString()
                    Dim yr = dt.Rows(0)("academic_year").ToString()
                    UpdateStatus($"Active Semester: {semName} ({yr})", 82.0F)
                    Await Task.Delay(220)
                End If
            Catch ex As Exception
                ' Fallback
            End Try

            ' Phase 4: Sync Residential Assets & Security Vault
            UpdateStatus("Syncing campus services...", 92.0F)
            Await Task.Delay(240)

            Try
                Dim assetsDir As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "rooms")
                Directory.CreateDirectory(assetsDir)
            Catch
            End Try

            ' Phase 5: Ready State & Launch
            lblStatusTicker.ForeColor = Color.FromArgb(16, 185, 129)
            UpdateStatus("Welcome to DeKUT Hostels", 100.0F)

            ' Wait for smooth progress lerp to finish visually
            While _currentProgress < 99.5F
                Await Task.Delay(20)
            End While

            Await Task.Delay(380)

            _initComplete = True
            tmrAnimation.Stop()

            ' Smooth transition to frmLogin
            Me.Hide()
            Dim loginForm As New frmLogin()
            loginForm.Show()
        End Sub

        Private Sub UpdateStatus(statusMessage As String, targetPercent As Single)
            lblStatusTicker.Text = statusMessage
            _targetProgress = Math.Min(100.0F, Math.Max(0.0F, targetPercent))
        End Sub

        Private Sub HandleInitializationFailure(title As String, errorMessage As String)
            _initError = True
            lblStatusTicker.ForeColor = ThemeManager.Coral
            lblStatusTicker.Text = $"Connection Notice: {errorMessage}"
            pnlProgressTrack.Invalidate()
            btnRetry.Visible = True
            btnExit.Visible = True
        End Sub

        Private Sub btnRetry_Click(sender As Object, e As EventArgs) Handles btnRetry.Click
            _currentProgress = 0.0F
            _targetProgress = 10.0F
            pnlProgressTrack.Invalidate()
            StartInitializationSequence()
        End Sub

        Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
            Application.Exit()
        End Sub
    End Class
End Namespace