Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers

Namespace Controls
    ''' <summary>
    ''' Frosted glassmorphic pill search input with integrated search glyph,
    ''' glowing focus transition, and clean text clearing.
    ''' </summary>
    Public Class GlassSearchBox
        Inherits UserControl

        Private WithEvents _txtInput As New TextBox()
        Private _placeholder As String = "Search..."
        Private _cornerRadius As Integer = 12
        Private _isFocused As Boolean = False

        Public Event SearchTextChanged As EventHandler

        Public Sub New()
            MyBase.New()
            SetStyle(ControlStyles.UserPaint Or
                     ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw Or
                     ControlStyles.SupportsTransparentBackColor, True)
            Me.DoubleBuffered = True
            Me.BackColor = Color.Transparent
            Me.Size = New Size(240, 36)
            Me.MinimumSize = New Size(140, 32)
            Me.Padding = New Padding(34, 7, 12, 7)

            _txtInput.BorderStyle = BorderStyle.None
            _txtInput.BackColor = Color.FromArgb(14, 22, 35)
            _txtInput.ForeColor = ThemeManager.TextChalkWhite
            _txtInput.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
            _txtInput.Dock = DockStyle.Fill
            _txtInput.PlaceholderText = _placeholder

            AddHandler _txtInput.Enter, Sub()
                                            _isFocused = True
                                            Me.Invalidate()
                                        End Sub
            AddHandler _txtInput.Leave, Sub()
                                            _isFocused = False
                                            Me.Invalidate()
                                        End Sub
            AddHandler _txtInput.TextChanged, Sub(s, e)
                                                  RaiseEvent SearchTextChanged(Me, EventArgs.Empty)
                                              End Sub

            Me.Controls.Add(_txtInput)
        End Sub

        <Category("Glassmorphism")>
        Public Property Placeholder As String
            Get
                Return _placeholder
            End Get
            Set(value As String)
                _placeholder = value
                _txtInput.PlaceholderText = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property PlaceholderText As String
            Get
                Return Placeholder
            End Get
            Set(value As String)
                Placeholder = value
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property QueryText As String
            Get
                Return _txtInput.Text
            End Get
            Set(value As String)
                _txtInput.Text = value
            End Set
        End Property

        Public Sub Clear()
            _txtInput.Clear()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.PixelOffsetMode = PixelOffsetMode.HighQuality

            Dim rect As New RectangleF(1.0F, 1.0F, Me.Width - 2.0F, Me.Height - 2.0F)

            Using path As GraphicsPath = ThemeManager.CreateRoundedRectangle(rect, _cornerRadius)
                ' 1. Dark Acrylic Input Fill
                Using fillBrush As New SolidBrush(Color.FromArgb(14, 22, 35))
                    g.FillPath(fillBrush, path)
                End Using

                ' 2. Border & Focus Glow
                Dim borderCol As Color = If(_isFocused, ThemeManager.GlassBorderFocus, ThemeManager.GlassInputBorder)
                Dim borderW As Single = If(_isFocused, 1.5F, 1.0F)
                Using borderPen As New Pen(borderCol, borderW)
                    g.DrawPath(borderPen, path)
                End Using
            End Using

            ' 3. Search Icon Glyph (🔍)
            Using iconFont As New Font("Segoe UI", 9.0F, FontStyle.Regular)
                Using iconBrush As New SolidBrush(If(_isFocused, ThemeManager.GlassStatusInfo, ThemeManager.TextMutedSlate))
                    g.DrawString("🔍", iconFont, iconBrush, 10, (Me.Height - 16) / 2.0F)
                End Using
            End Using
        End Sub
    End Class
End Namespace

