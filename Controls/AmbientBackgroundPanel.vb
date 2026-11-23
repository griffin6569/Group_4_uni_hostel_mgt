Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace Controls
    ''' <summary>
    ''' Atmospheric background panel that generates diffused colored light orbs.
    ''' Crucial for authentic Glassmorphism refraction behind translucent frosted panels.
    ''' </summary>
    Public Class AmbientBackgroundPanel
        Inherits Panel

        Public Sub New()
            MyBase.New()
            SetStyle(ControlStyles.UserPaint Or
                     ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw, True)
            Me.DoubleBuffered = True
            Me.BackColor = Color.FromArgb(7, 10, 14)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias

            ' 1. Base Dark Luxury Gradient Fill
            Using baseBrush As New LinearGradientBrush(
                New Point(0, 0),
                New Point(Me.Width, Me.Height),
                Color.FromArgb(7, 10, 14),
                Color.FromArgb(12, 17, 23))
                g.FillRectangle(baseBrush, Me.ClientRectangle)
            End Using

            ' 2. Emerald Light Orb (Top-Left glow)
            DrawDiffusedOrb(g, New Point(CInt(Me.Width * 0.15F), CInt(Me.Height * 0.2F)),
                            CInt(Math.Min(Me.Width, Me.Height) * 0.65F),
                            Color.FromArgb(28, 0, 230, 118))

            ' 3. Deep Cobalt / Cyan Orb (Bottom-Right glow)
            DrawDiffusedOrb(g, New Point(CInt(Me.Width * 0.85F), CInt(Me.Height * 0.8F)),
                            CInt(Math.Min(Me.Width, Me.Height) * 0.75F),
                            Color.FromArgb(20, 30, 64, 175))

            ' 4. Subtle Center Emerald Accent
            DrawDiffusedOrb(g, New Point(CInt(Me.Width * 0.55F), CInt(Me.Height * 0.45F)),
                            CInt(Math.Min(Me.Width, Me.Height) * 0.4F),
                            Color.FromArgb(14, 16, 185, 129))
        End Sub

        Private Sub DrawDiffusedOrb(g As Graphics, center As Point, radius As Integer, color As Color)
            If radius <= 0 Then Return
            Dim rect As New Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2)
            Using path As New GraphicsPath()
                path.AddEllipse(rect)
                Using pgb As New PathGradientBrush(path)
                    pgb.CenterPoint = center
                    pgb.CenterColor = color
                    pgb.SurroundColors = New Color() {Color.FromArgb(0, color.R, color.G, color.B)}
                    g.FillPath(pgb, path)
                End Using
            End Using
        End Sub
    End Class
End Namespace
