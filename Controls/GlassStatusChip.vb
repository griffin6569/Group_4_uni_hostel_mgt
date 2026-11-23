Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers

Namespace Controls
    ''' <summary>
    ''' High-contrast frosted status chip.
    ''' Paints a rounded pill badge with semi-translucent background and glowing foreground text.
    ''' </summary>
    Public Class GlassStatusChip
        Inherits Control

        Private _statusText As String = "ACTIVE"
        Private _glowColor As Color = ThemeManager.GlassStatusSuccess
        Private _bgColor As Color = ThemeManager.GlassStatusSuccessBg

        Public Sub New()
            MyBase.New()
            SetStyle(ControlStyles.UserPaint Or
                     ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.SupportsTransparentBackColor, True)
            Me.DoubleBuffered = True
            Me.BackColor = Color.Transparent
            Me.Font = New Font("Segoe UI Semibold", 8.0F, FontStyle.Bold)
            Me.Size = New Size(90, 24)
        End Sub

        <Category("Appearance")>
        Public Property StatusText As String
            Get
                Return _statusText
            End Get
            Set(value As String)
                _statusText = If(value, String.Empty)
                UpdateColorsByStatus()
                Me.Invalidate()
            End Set
        End Property

        Public Sub SetCustomStatus(text As String, glowColor As Color, bgColor As Color)
            _statusText = text
            _glowColor = glowColor
            _bgColor = bgColor
            Me.Invalidate()
        End Sub

        Private Sub UpdateColorsByStatus()
            Dim s As String = _statusText.Trim().ToUpperInvariant()
            Select Case s
                Case "AVAILABLE", "PAID", "ACTIVE", "RESOLVED", "VERIFIED"
                    _glowColor = ThemeManager.GlassStatusSuccess
                    _bgColor = ThemeManager.GlassStatusSuccessBg
                Case "OCCUPIED", "OVERDUE", "DEFECTIVE", "SUSPENDED", "CANCELLED"
                    _glowColor = ThemeManager.GlassStatusDanger
                    _bgColor = ThemeManager.GlassStatusDangerBg
                Case "MAINTENANCE", "PENDING", "PARTIAL", "CHECKED_OUT"
                    _glowColor = ThemeManager.GlassStatusWarning
                    _bgColor = ThemeManager.GlassStatusWarningBg
                Case "RESERVED", "IN_PROGRESS", "TRANSFERRED"
                    _glowColor = ThemeManager.GlassStatusInfo
                    _bgColor = ThemeManager.GlassStatusInfoBg
                Case Else
                    _glowColor = ThemeManager.TextSilverSlate
                    _bgColor = Color.FromArgb(30, 255, 255, 255)
            End Select
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

            Dim rect As New RectangleF(1.0F, 1.0F, Me.Width - 2.0F, Me.Height - 2.0F)
            Dim radius As Single = rect.Height / 2.0F

            Using path As GraphicsPath = ThemeManager.CreateRoundedRectangle(rect, radius)
                Using bgBrush As New SolidBrush(_bgColor)
                    g.FillPath(bgBrush, path)
                End Using

                Using rimPen As New Pen(Color.FromArgb(80, _glowColor), 1.0F)
                    g.DrawPath(rimPen, path)
                End Using
            End Using

            ' Draw Text
            Dim sf As New StringFormat With {
                .Alignment = StringAlignment.Center,
                .LineAlignment = StringAlignment.Center
            }
            Using textBrush As New SolidBrush(_glowColor)
                g.DrawString(_statusText, Me.Font, textBrush, rect, sf)
            End Using
        End Sub
    End Class
End Namespace

