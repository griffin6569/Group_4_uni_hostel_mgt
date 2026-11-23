Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace Controls
    ''' <summary>
    ''' Ultra-modern glassmorphic container panel featuring anti-aliased rounded corners,
    ''' translucent frosted acrylic filling, top-edge light refraction, and subtle ambient borders.
    ''' </summary>
    Public Class GlassPanel
        Inherits Panel

        Private _cornerRadius As Integer = 16
        Private _fillColor As Color = Color.FromArgb(200, 20, 28, 36)      ' Translucent dark panel
        Private _borderColor As Color = Color.FromArgb(70, 255, 255, 255)    ' Subtle frosted border
        Private _borderWidth As Single = 1.0F
        Private _enableGlassGlow As Boolean = True
        Private _glowColor As Color = Color.FromArgb(25, 0, 230, 118)

        Public Sub New()
            MyBase.New()
            SetStyle(ControlStyles.UserPaint Or
                     ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or
                     ControlStyles.SupportsTransparentBackColor, True)
            Me.DoubleBuffered = True
            Me.BackColor = Color.Transparent
            Me.Padding = New Padding(16)
        End Sub

        <Category("Glassmorphism"), DefaultValue(16)>
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

        <Category("Glassmorphism"), DefaultValue(1.0F)>
        Public Property BorderWidth As Single
            Get
                Return _borderWidth
            End Get
            Set(value As Single)
                _borderWidth = Math.Max(0.5F, value)
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism"), DefaultValue(True)>
        Public Property EnableGlassGlow As Boolean
            Get
                Return _enableGlassGlow
            End Get
            Set(value As Boolean)
                _enableGlassGlow = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property GlowColor As Color
            Get
                Return _glowColor
            End Get
            Set(value As Color)
                _glowColor = value
                Me.Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Try
                Dim g As Graphics = e.Graphics
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.PixelOffsetMode = PixelOffsetMode.HighQuality

                Dim rect As New RectangleF(_borderWidth / 2.0F,
                                           _borderWidth / 2.0F,
                                           Me.Width - _borderWidth - 1.0F,
                                           Me.Height - _borderWidth - 1.0F)

                If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

                Using path As GraphicsPath = GetRoundedPath(rect, _cornerRadius)
                    ' 1. Ambient Glow / Drop highlight (if enabled)
                    If _enableGlassGlow AndAlso _glowColor.A > 0 Then
                        Using glowPen As New Pen(_glowColor, _borderWidth + 3.0F)
                            g.DrawPath(glowPen, path)
                        End Using
                    End If

                    ' 2. Frosted Glass Body Fill with subtle vertical tint
                    Using brush As New SolidBrush(_fillColor)
                        g.FillPath(brush, path)
                    End Using

                    ' 3. Subtle Glass Specular Gradient (top highlight simulating frosted glass reflection)
                    Dim highlightHeight As Single = Math.Min(40.0F, rect.Height * 0.4F)
                    Dim highlightRect As New RectangleF(rect.X, rect.Y, rect.Width, highlightHeight)
                    If highlightRect.Width > 1.0F AndAlso highlightRect.Height > 1.0F Then
                        Using highlightPath As GraphicsPath = GetRoundedTopPath(highlightRect, _cornerRadius)
                            Using gradBrush As New LinearGradientBrush(
                                New PointF(0, highlightRect.Top),
                                New PointF(0, highlightRect.Bottom),
                                Color.FromArgb(35, 255, 255, 255),
                                Color.FromArgb(0, 255, 255, 255))
                                g.FillPath(gradBrush, highlightPath)
                            End Using
                        End Using
                    End If

                    ' 4. Crisp Translucent Border (Glass Edge)
                    Using borderPen As New Pen(_borderColor, _borderWidth)
                        g.DrawPath(borderPen, path)
                    End Using
                End Using
            Catch ex As Exception
                Console.WriteLine($"[GlassPanel.OnPaint Exception] {ex.Message} -> {ex.StackTrace}")
            End Try
        End Sub

        Private Function GetRoundedPath(rect As RectangleF, radius As Single) As GraphicsPath
            Dim path As New GraphicsPath()
            If radius <= 0 Then
                path.AddRectangle(rect)
                Return path
            End If

            Dim d As Single = radius * 2.0F
            path.AddArc(rect.X, rect.Y, d, d, 180, 90)
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
            path.CloseFigure()
            Return path
        End Function

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
