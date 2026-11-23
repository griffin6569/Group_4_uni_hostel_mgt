Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers

Namespace Controls
    ''' <summary>
    ''' Executive Frosted Glass KPI Metric Widget.
    ''' Renders category eyebrow badge, large bold statistic, semantic trend subtitle,
    ''' and an integrated ambient progress indicator.
    ''' </summary>
    Public Class GlassKpiWidget
        Inherits PrestigeGlassCard

        Private _categoryTitle As String = "METRIC CATEGORY"
        Private _primaryValue As String = "0"
        Private _subtitle As String = "Description or trend"
        Private _progressPercentage As Single = -1.0F ' -1 means no progress bar
        Private _valueColor As Color = ThemeManager.TextChalkWhite
        Private _iconSymbol As String = String.Empty

        Public Sub New()
            MyBase.New()
            Me.Size = New Size(240, 110)
            Me.MinimumSize = New Size(180, 95)
            Me.ShowSpecularHighlight = True
            Me.AccentHeight = 3
        End Sub

        <Category("Glassmorphism")>
        Public Property CategoryTitle As String
            Get
                Return _categoryTitle
            End Get
            Set(value As String)
                _categoryTitle = If(value, String.Empty)
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property PrimaryValue As String
            Get
                Return _primaryValue
            End Get
            Set(value As String)
                _primaryValue = If(value, String.Empty)
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property Subtitle As String
            Get
                Return _subtitle
            End Get
            Set(value As String)
                _subtitle = If(value, String.Empty)
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism"), DefaultValue(-1.0F)>
        Public Property ProgressPercentage As Single
            Get
                Return _progressPercentage
            End Get
            Set(value As Single)
                _progressPercentage = Math.Max(-1.0F, Math.Min(100.0F, value))
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property ValueColor As Color
            Get
                Return _valueColor
            End Get
            Set(value As Color)
                _valueColor = value
                Me.Invalidate()
            End Set
        End Property

        <Category("Glassmorphism")>
        Public Property IconSymbol As String
            Get
                Return _iconSymbol
            End Get
            Set(value As String)
                _iconSymbol = If(value, String.Empty)
                Me.Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            ' Draw base glass card background and specular highlight
            MyBase.OnPaint(e)

            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

            Dim leftMargin As Single = 16.0F
            Dim rightMargin As Single = Me.Width - 16.0F

            ' 1. Category Eyebrow Title
            Dim titleY As Single = 14.0F
            Using titleFont As New Font("Segoe UI Semibold", 8.0F, FontStyle.Bold)
                Using brush As New SolidBrush(ThemeManager.TextMutedSlate)
                    Dim titleText As String = If(String.IsNullOrEmpty(_iconSymbol), _categoryTitle, $"{_iconSymbol}  {_categoryTitle}")
                    g.DrawString(titleText.ToUpperInvariant(), titleFont, brush, leftMargin, titleY)
                End Using
            End Using

            ' 2. Primary KPI Value (Chalk White or Accent)
            Dim valueY As Single = 34.0F
            Dim isMoney As Boolean = _primaryValue.StartsWith("KES") OrElse _primaryValue.Contains("$")
            Dim fontName As String = If(isMoney, "Consolas", "Segoe UI")
            Dim fontSize As Single = If(_primaryValue.Length > 12, 14.0F, 17.5F)

            Using valFont As New Font(fontName, fontSize, FontStyle.Bold)
                Using valBrush As New SolidBrush(_valueColor)
                    g.DrawString(_primaryValue, valFont, valBrush, leftMargin - 1.0F, valueY)
                End Using
            End Using

            ' 3. Subtitle / Trend
            Dim subY As Single = 72.0F
            Using subFont As New Font("Segoe UI", 8.2F, FontStyle.Regular)
                Using subBrush As New SolidBrush(ThemeManager.TextSilverSlate)
                    g.DrawString(_subtitle, subFont, subBrush, leftMargin, subY)
                End Using
            End Using

            ' 4. Mini Progress Meter (if enabled)
            If _progressPercentage >= 0.0F Then
                Dim barY As Single = Me.Height - 14.0F
                Dim barH As Single = 5.0F
                Dim barW As Single = rightMargin - leftMargin

                Using trackBrush As New SolidBrush(Color.FromArgb(40, 255, 255, 255))
                    Using trackPath As GraphicsPath = ThemeManager.CreateRoundedRectangle(New RectangleF(leftMargin, barY, barW, barH), 2.5F)
                        g.FillPath(trackBrush, trackPath)
                    End Using
                End Using

                Dim fillW As Single = Math.Max(4.0F, barW * (_progressPercentage / 100.0F))
                Dim fillCol As Color = If(Me.AccentColor <> Color.Transparent, Me.AccentColor, ThemeManager.GlassStatusInfo)
                Using fillBrush As New SolidBrush(fillCol)
                    Using fillPath As GraphicsPath = ThemeManager.CreateRoundedRectangle(New RectangleF(leftMargin, barY, fillW, barH), 2.5F)
                        g.FillPath(fillBrush, fillPath)
                    End Using
                End Using
            End If
        End Sub
    End Class
End Namespace

