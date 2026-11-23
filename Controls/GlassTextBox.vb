Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace Controls
    ''' <summary>
    ''' Ultra-modern glassmorphic text box with anti-aliased rounded corners,
    ''' glowing focus transition, dynamic typing ripple animation, and password visibility toggle.
    ''' </summary>
    Public Class GlassTextBox
        Inherits UserControl

        Private WithEvents _innerTextBox As New TextBox()
        Private WithEvents _animTimer As New Timer()
        Private WithEvents _togglePasswordBtn As New Label()

        Private _cornerRadius As Integer = 12
        Private _placeholder As String = String.Empty
        Private _isPassword As Boolean = False
        Private _showPassword As Boolean = False
        Private _leadingIconText As String = String.Empty

        ' Animation State
        Private _focusProgress As Single = 0.0F      ' 0.0 to 1.0 (smooth focus transition)
        Private _typingPulseProgress As Single = 0.0F ' 0.0 to 1.0 (ripple when typing)
        Private _isFocused As Boolean = False

        ' Palette
        Private _fillColor As Color = Color.FromArgb(210, 15, 23, 32)
        Private _borderIdleColor As Color = Color.FromArgb(70, 36, 50, 64)
        Private _borderFocusColor As Color = Color.FromArgb(0, 230, 118)
        Private _glowColor As Color = Color.FromArgb(80, 0, 230, 118)

        Public Event TextChangedEvent As EventHandler

        Public Sub New()
            MyBase.New()
            SetStyle(ControlStyles.UserPaint Or
                     ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or
                     ControlStyles.SupportsTransparentBackColor, True)
            Me.DoubleBuffered = True
            Me.BackColor = Color.Transparent
            Me.Size = New Size(300, 44)
            Me.MinimumSize = New Size(120, 38)
            Me.Font = New Font("Segoe UI", 10.5F, FontStyle.Regular)

            ' Configure Inner TextBox
            _innerTextBox.BorderStyle = BorderStyle.None
            _innerTextBox.BackColor = Color.FromArgb(15, 23, 32)
            _innerTextBox.ForeColor = Color.FromArgb(248, 250, 252)
            _innerTextBox.Font = Me.Font
            Me.Controls.Add(_innerTextBox)

            ' Configure Show/Hide Password Eye Button
            _togglePasswordBtn.Text = "👁"
            _togglePasswordBtn.Font = New Font("Segoe UI Symbol", 10.0F, FontStyle.Regular)
            _togglePasswordBtn.ForeColor = Color.FromArgb(148, 163, 184)
            _togglePasswordBtn.BackColor = Color.Transparent
            _togglePasswordBtn.Cursor = Cursors.Hand
            _togglePasswordBtn.TextAlign = ContentAlignment.MiddleCenter
            _togglePasswordBtn.Size = New Size(32, 28)
            _togglePasswordBtn.Visible = False
            Me.Controls.Add(_togglePasswordBtn)

            ' Animation Timer (60 FPS)
            _animTimer.Interval = 16
            _animTimer.Start()

            LayoutControls()
        End Sub

        <Category("Glassmorphism")>
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
        Public Property PlaceholderText As String
            Get
                Return _placeholder
            End Get
            Set(value As String)
                _placeholder = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property IsPassword As Boolean
            Get
                Return _isPassword
            End Get
            Set(value As Boolean)
                _isPassword = value
                _innerTextBox.UseSystemPasswordChar = value AndAlso Not _showPassword
                _togglePasswordBtn.Visible = value
                LayoutControls()
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property TextValue As String
            Get
                Return _innerTextBox.Text
            End Get
            Set(value As String)
                _innerTextBox.Text = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property LeadingIconText As String
            Get
                Return _leadingIconText
            End Get
            Set(value As String)
                _leadingIconText = value
                LayoutControls()
                Me.Invalidate()
            End Set
        End Property

        Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
                _innerTextBox.Font = value
                LayoutControls()
            End Set
        End Property

        Private Sub LayoutControls()
            Dim leftPadding As Integer = If(String.IsNullOrEmpty(_leadingIconText), 14, 40)
            Dim rightPadding As Integer = If(_isPassword, 40, 14)
            Dim tbHeight As Integer = _innerTextBox.PreferredHeight

            _innerTextBox.Location = New Point(leftPadding, (Me.Height - tbHeight) \ 2)
            _innerTextBox.Width = Math.Max(10, Me.Width - leftPadding - rightPadding)

            If _isPassword Then
                _togglePasswordBtn.Location = New Point(Me.Width - 36, (Me.Height - _togglePasswordBtn.Height) \ 2)
            End If
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            LayoutControls()
        End Sub

        Private Sub _innerTextBox_Enter(sender As Object, e As EventArgs) Handles _innerTextBox.Enter
            _isFocused = True
        End Sub

        Private Sub _innerTextBox_Leave(sender As Object, e As EventArgs) Handles _innerTextBox.Leave
            _isFocused = False
        End Sub

        Private Sub _innerTextBox_TextChanged(sender As Object, e As EventArgs) Handles _innerTextBox.TextChanged
            ' Trigger typing ripple pulse
            _typingPulseProgress = 1.0F
            RaiseEvent TextChangedEvent(Me, EventArgs.Empty)
            Me.Invalidate()
        End Sub

        Private Sub _togglePasswordBtn_Click(sender As Object, e As EventArgs) Handles _togglePasswordBtn.Click
            _showPassword = Not _showPassword
            _innerTextBox.UseSystemPasswordChar = _isPassword AndAlso Not _showPassword
            _togglePasswordBtn.Text = If(_showPassword, "🔒", "👁")
            _togglePasswordBtn.ForeColor = If(_showPassword, Color.FromArgb(0, 230, 118), Color.FromArgb(148, 163, 184))
            _innerTextBox.Focus()
        End Sub

        Private Sub _animTimer_Tick(sender As Object, e As EventArgs) Handles _animTimer.Tick
            Dim needsRedraw As Boolean = False

            ' 1. Smooth Focus Transition (Interpolate _focusProgress)
            Dim targetFocus As Single = If(_isFocused, 1.0F, 0.0F)
            If Math.Abs(_focusProgress - targetFocus) > 0.02F Then
                _focusProgress += (targetFocus - _focusProgress) * 0.25F
                needsRedraw = True
            ElseIf _focusProgress <> targetFocus Then
                _focusProgress = targetFocus
                needsRedraw = True
            End If

            ' 2. Typing Ripple Decay
            If _typingPulseProgress > 0.0F Then
                _typingPulseProgress = Math.Max(0.0F, _typingPulseProgress - 0.06F)
                needsRedraw = True
            End If

            If needsRedraw Then
                Me.Invalidate()
            End If
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Try
                Dim g As Graphics = e.Graphics
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.PixelOffsetMode = PixelOffsetMode.HighQuality

                Dim rect As New RectangleF(1.0F, 1.0F, Me.Width - 3.0F, Me.Height - 3.0F)
                If rect.Width <= 0 OrElse rect.Height <= 0 Then Return

                Using path As GraphicsPath = GetRoundedPath(rect, _cornerRadius)
                    ' 1. Background Fill
                    Using bgBrush As New SolidBrush(_fillColor)
                        g.FillPath(bgBrush, path)
                    End Using

                    ' 2. Animated Focus Glow
                    If _focusProgress > 0.01F Then
                        Dim glowAlpha As Integer = CInt(Math.Min(180, _focusProgress * 140))
                        Using glowPen As New Pen(Color.FromArgb(glowAlpha, _glowColor), 3.0F)
                            g.DrawPath(glowPen, path)
                        End Using
                    End If

                    ' 3. Base Rounded Border (Interpolated between Idle and Focus Color)
                    Dim r As Integer = Math.Min(255, Math.Max(0, CInt(CInt(_borderIdleColor.R) + (CInt(_borderFocusColor.R) - CInt(_borderIdleColor.R)) * _focusProgress)))
                    Dim gVal As Integer = Math.Min(255, Math.Max(0, CInt(CInt(_borderIdleColor.G) + (CInt(_borderFocusColor.G) - CInt(_borderIdleColor.G)) * _focusProgress)))
                    Dim b As Integer = Math.Min(255, Math.Max(0, CInt(CInt(_borderIdleColor.B) + (CInt(_borderFocusColor.B) - CInt(_borderIdleColor.B)) * _focusProgress)))
                    Dim currentBorderColor As Color = Color.FromArgb(255, r, gVal, b)

                    Using borderPen As New Pen(currentBorderColor, 1.2F)
                        g.DrawPath(borderPen, path)
                    End Using

                    ' 4. DYNAMIC TYPING ANIMATION (Pulsing glowing beam along the bottom edge)
                    If _typingPulseProgress > 0.01F Then
                        Dim pulseWidth As Single = rect.Width * (0.3F + (0.7F * (1.0F - _typingPulseProgress)))
                        If pulseWidth > 2.0F Then
                            Dim pulseX As Single = rect.X + (rect.Width - pulseWidth) / 2.0F
                            Dim pulseAlpha As Integer = CInt(Math.Min(255, Math.Max(0, _typingPulseProgress * 230)))

                            Using pulseBrush As New LinearGradientBrush(
                                New PointF(pulseX, rect.Bottom - 2),
                                New PointF(pulseX + pulseWidth, rect.Bottom - 2),
                                Color.FromArgb(0, 0, 230, 118),
                                Color.FromArgb(pulseAlpha, 0, 230, 118))
                                pulseBrush.SetBlendTriangularShape(0.5F)
                                Using pulsePen As New Pen(pulseBrush, 2.5F)
                                    pulsePen.StartCap = LineCap.Round
                                    pulsePen.EndCap = LineCap.Round
                                    g.DrawLine(pulsePen, pulseX, rect.Bottom - 1.5F, pulseX + pulseWidth, rect.Bottom - 1.5F)
                                End Using
                            End Using
                        End If
                    End If

                    ' 5. Draw Leading Icon if specified
                    If Not String.IsNullOrEmpty(_leadingIconText) Then
                        Using iconBrush As New SolidBrush(If(_isFocused, Color.FromArgb(0, 230, 118), Color.FromArgb(148, 163, 184)))
                            Using iconFont As New Font("Segoe UI Symbol", 10.0F, FontStyle.Regular)
                                g.DrawString(_leadingIconText, iconFont, iconBrush, New PointF(14, (Me.Height - 16) \ 2))
                            End Using
                        End Using
                    End If

                    ' 6. Draw Placeholder text when empty and not typing
                    If String.IsNullOrEmpty(_innerTextBox.Text) AndAlso Not String.IsNullOrEmpty(_placeholder) Then
                        Dim leftPadding As Integer = If(String.IsNullOrEmpty(_leadingIconText), 16, 42)
                        Dim placeholderAlpha As Integer = If(_isFocused, 110, 160)
                        Using phBrush As New SolidBrush(Color.FromArgb(placeholderAlpha, 148, 163, 184))
                            g.DrawString(_placeholder, Me.Font, phBrush, New PointF(leftPadding, (Me.Height - _innerTextBox.PreferredHeight) \ 2))
                        End Using
                    End If
                End Using
            Catch ex As Exception
                Console.WriteLine($"[GlassTextBox.OnPaint Exception] {ex.Message} -> {ex.StackTrace}")
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

        Protected Overrides Sub OnClick(e As EventArgs)
            MyBase.OnClick(e)
            _innerTextBox.Focus()
        End Sub
    End Class
End Namespace
