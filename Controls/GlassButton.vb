Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

Namespace Controls
    ''' <summary>
    ''' Modern rounded button featuring animated glassmorphic hover illumination,
    ''' subtle specular highlights, click ripple feedback, and custom typography.
    ''' </summary>
    Public Class GlassButton
        Inherits Button

        Private WithEvents _hoverTimer As New Timer()
        Private _cornerRadius As Integer = 12
        Private _hoverProgress As Single = 0.0F
        Private _isHovered As Boolean = False
        Private _isPressed As Boolean = False

        ' Customizable Palette
        Private _primaryColor As Color = Color.FromArgb(0, 230, 118)       ' Emerald
        Private _hoverColor As Color = Color.FromArgb(34, 197, 94)         ' Bright Emerald
        Private _pressedColor As Color = Color.FromArgb(16, 185, 129)      ' Dark Emerald
        Private _textColor As Color = Color.FromArgb(11, 15, 20)           ' Dark Text for contrast

        Public Sub New()
            MyBase.New()
            SetStyle(ControlStyles.UserPaint Or
                     ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or
                     ControlStyles.SupportsTransparentBackColor, True)
            Me.DoubleBuffered = True
            Me.BackColor = Color.Transparent
            Me.FlatStyle = FlatStyle.Flat
            Me.FlatAppearance.BorderSize = 0
            Me.Cursor = Cursors.Hand
            Me.Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold)
            Me.Size = New Size(160, 42)

            _hoverTimer.Interval = 16
            _hoverTimer.Start()
        End Sub

        <Category("Glassmorphism"), DefaultValue(12)>
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
        Public Property PrimaryColor As Color
            Get
                Return _primaryColor
            End Get
            Set(value As Color)
                _primaryColor = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property HoverColor As Color
            Get
                Return _hoverColor
            End Get
            Set(value As Color)
                _hoverColor = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property CustomTextColor As Color
            Get
                Return _textColor
            End Get
            Set(value As Color)
                _textColor = value
                Me.Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            _isHovered = True
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            _isHovered = False
            _isPressed = False
        End Sub

        Protected Overrides Sub OnMouseDown(mevent As MouseEventArgs)
            MyBase.OnMouseDown(mevent)
            If mevent.Button = MouseButtons.Left Then
                _isPressed = True
                Me.Invalidate()
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(mevent As MouseEventArgs)
            MyBase.OnMouseUp(mevent)
            _isPressed = False
            Me.Invalidate()
        End Sub

        Private Sub _hoverTimer_Tick(sender As Object, e As EventArgs) Handles _hoverTimer.Tick
            Dim target As Single = If(_isHovered, 1.0F, 0.0F)
            If Math.Abs(_hoverProgress - target) > 0.03F Then
                _hoverProgress += (target - _hoverProgress) * 0.25F
                Me.Invalidate()
            ElseIf _hoverProgress <> target Then
                _hoverProgress = target
                Me.Invalidate()
            End If
        End Sub

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            Dim g As Graphics = pevent.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

            Dim rect As New RectangleF(1.0F, 1.0F, Me.Width - 3.0F, Me.Height - 3.0F)
            If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

            Using path As GraphicsPath = GetRoundedPath(rect, _cornerRadius)
                If Not Me.Enabled Then
                    ' Disabled State
                    Using disBrush As New SolidBrush(Color.FromArgb(40, 255, 255, 255))
                        g.FillPath(disBrush, path)
                    End Using
                    Using disPen As New Pen(Color.FromArgb(50, 255, 255, 255), 1.0F)
                        g.DrawPath(disPen, path)
                    End Using
                    TextRenderer.DrawText(g, Me.Text, Me.Font, Me.ClientRectangle, Color.FromArgb(148, 163, 184),
                                          TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                    Return
                End If

                ' 1. Ambient Glow when hovered or focused
                If _hoverProgress > 0.01F AndAlso Not _isPressed Then
                    Dim glowAlpha As Integer = CInt(_hoverProgress * 100)
                    Using glowPen As New Pen(Color.FromArgb(glowAlpha, _hoverColor), 4.0F)
                        g.DrawPath(glowPen, path)
                    End Using
                End If

                ' 2. Button Body Gradient (Interpolated between Primary and Hover)
                Dim currColor As Color = If(_isPressed, _pressedColor,
                                            InterpolateColor(_primaryColor, _hoverColor, _hoverProgress))

                Using bodyBrush As New SolidBrush(currColor)
                    g.FillPath(bodyBrush, path)
                End Using

                ' 3. Subtle Top Specular Glass Reflection
                Dim highlightHeight As Single = rect.Height * 0.45F
                Dim hlRect As New RectangleF(rect.X, rect.Y, rect.Width, highlightHeight)
                Using hlPath As GraphicsPath = GetRoundedTopPath(hlRect, _cornerRadius)
                    Using hlBrush As New LinearGradientBrush(
                        New PointF(0, hlRect.Top),
                        New PointF(0, hlRect.Bottom),
                        Color.FromArgb(60, 255, 255, 255),
                        Color.FromArgb(0, 255, 255, 255))
                        g.FillPath(hlBrush, hlPath)
                    End Using
                End Using

                ' 4. Crisp Border
                Using borderPen As New Pen(Color.FromArgb(100, 255, 255, 255), 1.0F)
                    g.DrawPath(borderPen, path)
                End Using

                ' 5. Draw Centered Text
                Dim flags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine
                Dim textRect As Rectangle = Me.ClientRectangle
                If _isPressed Then
                    textRect.Offset(0, 1)
                End If
                TextRenderer.DrawText(g, Me.Text, Me.Font, textRect, _textColor, flags)
            End Using
        End Sub

        Private Function InterpolateColor(c1 As Color, c2 As Color, factor As Single) As Color
            Dim f As Single = Math.Max(0.0F, Math.Min(1.0F, factor))
            Dim r As Integer = Math.Min(255, Math.Max(0, CInt(CInt(c1.R) + (CInt(c2.R) - CInt(c1.R)) * f)))
            Dim g As Integer = Math.Min(255, Math.Max(0, CInt(CInt(c1.G) + (CInt(c2.G) - CInt(c1.G)) * f)))
            Dim b As Integer = Math.Min(255, Math.Max(0, CInt(CInt(c1.B) + (CInt(c2.B) - CInt(c1.B)) * f)))
            Return Color.FromArgb(255, r, g, b)
        End Function

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
