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
        Private _borderColor As Color = Color.FromArgb(60, 255, 255, 255)  ' Frosted Specular Border
        Private _hoverBorderColor As Color = Color.FromArgb(130, 255, 255, 255) ' Hover Specular Border

        Public Sub New()
            MyBase.New()
            SetStyle(ControlStyles.UserPaint Or
                     ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.SupportsTransparentBackColor, False)
            Me.DoubleBuffered = True
            Me.BackColor = Color.FromArgb(17, 24, 38)
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

        Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As Color)
                If value = Color.Transparent Then
                    MyBase.BackColor = Color.FromArgb(17, 24, 38)
                Else
                    MyBase.BackColor = value
                End If
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

        <Category("Glassmorphism")>
        Public Property HoverBorderColor As Color
            Get
                Return _hoverBorderColor
            End Get
            Set(value As Color)
                _hoverBorderColor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Configures primary frosted cyan/navy action button style.
        ''' </summary>
        Public Sub SetPrimaryStyle()
            _primaryColor = Color.FromArgb(30, 62, 98)
            _hoverColor = Color.FromArgb(44, 82, 130)
            _pressedColor = Color.FromArgb(20, 45, 75)
            _textColor = Color.White
            Me.Invalidate()
        End Sub

        ''' <summary>
        ''' Configures secondary translucent frosted outline button style.
        ''' </summary>
        Public Sub SetSecondaryStyle()
            _primaryColor = Color.FromArgb(30, 255, 255, 255)
            _hoverColor = Color.FromArgb(50, 255, 255, 255)
            _pressedColor = Color.FromArgb(20, 255, 255, 255)
            _textColor = Color.FromArgb(226, 232, 240)
            Me.Invalidate()
        End Sub

        ''' <summary>
        ''' Configures glowing emerald success button style.
        ''' </summary>
        Public Sub SetSuccessStyle()
            _primaryColor = Color.FromArgb(16, 185, 129)
            _hoverColor = Color.FromArgb(34, 197, 94)
            _pressedColor = Color.FromArgb(5, 150, 105)
            _textColor = Color.White
            Me.Invalidate()
        End Sub

        ''' <summary>
        ''' Configures glowing danger coral button style.
        ''' </summary>
        Public Sub SetDangerStyle()
            _primaryColor = Color.FromArgb(220, 38, 38)
            _hoverColor = Color.FromArgb(239, 68, 68)
            _pressedColor = Color.FromArgb(185, 28, 28)
            _textColor = Color.White
            Me.Invalidate()
        End Sub

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

        Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
            ' Intentionally suppressed: GDI+ transparency on nested containers erroneously
            ' blits mismatched sibling control pixels into the backbuffer. Surface clearing is handled in OnPaint.
        End Sub

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            Dim g As Graphics = pevent.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

            ' Clear background with true parent card tone to eliminate any WinForms artifact blitting
            Dim parentBg As Color = Color.FromArgb(17, 24, 38)
            If Me.Parent IsNot Nothing Then
                If TypeOf Me.Parent Is GlassPanel Then
                    parentBg = DirectCast(Me.Parent, GlassPanel).FillColor
                ElseIf Me.Parent.BackColor <> Color.Transparent Then
                    parentBg = Me.Parent.BackColor
                End If
            End If
            Dim opaqueBase As Color = Color.FromArgb(255, parentBg.R, parentBg.G, parentBg.B)
            Using baseBrush As New SolidBrush(opaqueBase)
                g.FillRectangle(baseBrush, Me.ClientRectangle)
            End Using

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
                                          TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix)
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

                ' 4. Crisp Border with Dynamic State Interpolation
                Dim curBorderColor As Color = InterpolateColor(_borderColor, _hoverBorderColor, _hoverProgress)
                Using borderPen As New Pen(curBorderColor, 1.0F)
                    g.DrawPath(borderPen, path)
                End Using

                ' 5. Draw Centered Text
                Dim flags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.NoPrefix
                Dim textRect As Rectangle = Me.ClientRectangle
                If _isPressed Then
                    textRect.Offset(0, 1)
                End If
                TextRenderer.DrawText(g, Me.Text, Me.Font, textRect, _textColor, flags)
            End Using
        End Sub

        Private Function InterpolateColor(c1 As Color, c2 As Color, factor As Single) As Color
            Dim f As Single = Math.Max(0.0F, Math.Min(1.0F, factor))
            Dim a As Integer = Math.Min(255, Math.Max(0, CInt(CInt(c1.A) + (CInt(c2.A) - CInt(c1.A)) * f)))
            Dim r As Integer = Math.Min(255, Math.Max(0, CInt(CInt(c1.R) + (CInt(c2.R) - CInt(c1.R)) * f)))
            Dim g As Integer = Math.Min(255, Math.Max(0, CInt(CInt(c1.G) + (CInt(c2.G) - CInt(c1.G)) * f)))
            Dim b As Integer = Math.Min(255, Math.Max(0, CInt(CInt(c1.B) + (CInt(c2.B) - CInt(c1.B)) * f)))
            Return Color.FromArgb(a, r, g, b)
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
