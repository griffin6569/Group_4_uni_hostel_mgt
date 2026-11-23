Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers

Namespace Controls
    ''' <summary>
    ''' Ultra-modern frosted glassmorphic container card.
    ''' Features anti-aliased rounded geometry, translucent acrylic fill, top-edge specular refraction,
    ''' and optional semantic accent header borders.
    ''' </summary>
    Public Class PrestigeGlassCard
        Inherits Panel

        Private _cornerRadius As Integer = 14
        Private _fillColor As Color = ThemeManager.GlassCardFill
        Private _borderColor As Color = ThemeManager.GlassBorder
        Private _borderWidth As Single = 1.0F
        Private _showSpecularHighlight As Boolean = True
        Private _accentColor As Color = Color.Transparent
        Private _accentHeight As Integer = 3

        Public Sub New()
            MyBase.New()
            SetStyle(ControlStyles.UserPaint Or
                     ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or
                     ControlStyles.SupportsTransparentBackColor, True)
            Me.DoubleBuffered = True
            Me.BackColor = Color.Transparent
            Me.Padding = New Padding(14)
        End Sub

        <Category("Glassmorphism"), DefaultValue(14)>
        Public Property CornerRadius As Integer
            Get
                Return _cornerRadius
            End Get
            Set(value As Integer)
                _cornerRadius = Math.Max(0, value)
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property FillColor As Color
            Get
                Return _fillColor
            End Get
            Set(value As Color)
                _fillColor = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property BorderColor As Color
            Get
                Return _borderColor
            End Get
            Set(value As Color)
                _borderColor = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism"), DefaultValue(True)>
        Public Property ShowSpecularHighlight As Boolean
            Get
                Return _showSpecularHighlight
            End Get
            Set(value As Boolean)
                _showSpecularHighlight = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property AccentColor As Color
            Get
                Return _accentColor
            End Get
            Set(value As Color)
                _accentColor = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism"), DefaultValue(3)>
        Public Property AccentHeight As Integer
            Get
                Return _accentHeight
            End Get
            Set(value As Integer)
                _accentHeight = Math.Max(0, value)
                Me.Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.PixelOffsetMode = PixelOffsetMode.HighQuality

            Dim rect As New RectangleF(_borderWidth / 2.0F,
                                       _borderWidth / 2.0F,
                                       Me.Width - _borderWidth - 1.0F,
                                       Me.Height - _borderWidth - 1.0F)

            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            Using path As GraphicsPath = ThemeManager.CreateRoundedRectangle(rect, _cornerRadius)
                ' 1. Acrylic Translucent Fill
                Using brush As New SolidBrush(_fillColor)
                    g.FillPath(brush, path)
                End Using

                ' 2. Top-Edge Specular Glass Refraction
                If _showSpecularHighlight Then
                    Dim highlightH As Single = Math.Min(36.0F, rect.Height * 0.45F)
                    Dim highlightRect As New RectangleF(rect.X, rect.Y, rect.Width, highlightH)
                    If highlightRect.Width > 2.0F AndAlso highlightRect.Height > 2.0F Then
                        Using hlPath As GraphicsPath = GetRoundedTopPath(highlightRect, _cornerRadius)
                            Using grad As New LinearGradientBrush(
                                New PointF(0, highlightRect.Top),
                                New PointF(0, highlightRect.Bottom),
                                Color.FromArgb(32, 255, 255, 255),
                                Color.FromArgb(0, 255, 255, 255))
                                g.FillPath(grad, hlPath)
                            End Using
                        End Using
                    End If
                End If

                ' 3. Semantic Top Accent Stripe (if configured)
                If _accentColor <> Color.Transparent AndAlso _accentHeight > 0 Then
                    Dim accentRect As New RectangleF(rect.X, rect.Y, rect.Width, _accentHeight)
                    Using accPath As GraphicsPath = GetRoundedTopPath(accentRect, _cornerRadius)
                        Using accBrush As New SolidBrush(_accentColor)
                            g.FillPath(accBrush, accPath)
                        End Using
                    End Using
                End If

                ' 4. Crisp Frosted Border
                Using pen As New Pen(_borderColor, _borderWidth)
                    g.DrawPath(pen, path)
                End Using
            End Using
        End Sub

        Private Function GetRoundedTopPath(rect As RectangleF, radius As Single) As GraphicsPath
            Dim path As New GraphicsPath()
            Dim d As Single = radius * 2.0F
            path.AddArc(rect.X, rect.Y, d, d, 180, 90)
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
            path.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom)
            path.CloseFigure()
            Return path
        End Function
    End Class
End Namespace

