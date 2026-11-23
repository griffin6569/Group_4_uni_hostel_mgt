Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Windows.Forms

Namespace Helpers
    ''' <summary>
    ''' Central Design System provider implementing the Prestige Academic and Executive theme tokens across Windows Forms.
    ''' </summary>
    Public NotInheritable Class ThemeManager
        Private Sub New()
        End Sub

        ' =========================================================================
        ' 1. PRESTIGE ACADEMIC PALETTE (Enterprise & Directorate Standards)
        ' =========================================================================
        Public Shared ReadOnly NavyDark As Color = Color.FromArgb(11, 25, 44)           ' #0B192C (Deep Oxford Navy Base)
        Public Shared ReadOnly NavyPrimary As Color = Color.FromArgb(30, 62, 98)        ' #1E3E62 (Institutional Header / Primary)
        Public Shared ReadOnly NavyLight As Color = Color.FromArgb(44, 82, 130)         ' #2C5282 (Active pill / secondary navy)
        Public Shared ReadOnly AcademicGold As Color = Color.FromArgb(217, 119, 6)      ' #D97706 (Refined heraldic gold / warm brass)
        Public Shared ReadOnly AcademicGoldHover As Color = Color.FromArgb(180, 83, 9)  ' #B45309 (Gold hover state)
        Public Shared ReadOnly AcademicGoldLight As Color = Color.FromArgb(254, 243, 199) ' #FEF3C7 (Gold chip surface)

        ' Work Surface Tokens
        Public Shared ReadOnly SurfaceCanvas As Color = Color.FromArgb(248, 250, 252)   ' #F8FAFC (Ceramic slate workspace)
        Public Shared ReadOnly SurfaceCard As Color = Color.FromArgb(255, 255, 255)     ' #FFFFFF (Pristine elevated white cards)
        Public Shared ReadOnly SurfaceMuted As Color = Color.FromArgb(241, 245, 249)    ' #F1F5F9 (Subtle row tint / input backdrops)
        Public Shared ReadOnly BorderSubtle As Color = Color.FromArgb(226, 232, 240)    ' #E2E8F0 (Ultra-thin crisp dividers)
        Public Shared ReadOnly BorderMedium As Color = Color.FromArgb(203, 213, 225)    ' #CBD5E1 (Card outlines)

        ' Typography Tokens (High-Legibility Slate)
        Public Shared ReadOnly TextDeepSlate As Color = Color.FromArgb(15, 23, 42)      ' #0F172A (Primary headings & numbers)
        Public Shared ReadOnly TextSlateSecondary As Color = Color.FromArgb(71, 85, 105)' #475569 (Body & field labels)
        Public Shared ReadOnly TextSlateMuted As Color = Color.FromArgb(148, 163, 184) ' #94A3B8 (Metadata, timestamps, icons)
        Public Shared ReadOnly TextWhite As Color = Color.FromArgb(255, 255, 255)       ' #FFFFFF (Text on navy cards)

        ' Semantic Status Tokens
        Public Shared ReadOnly StatusSuccess As Color = Color.FromArgb(22, 163, 74)     ' #16A34A (Verified / Available / Paid)
        Public Shared ReadOnly StatusSuccessBg As Color = Color.FromArgb(220, 252, 231) ' #DCFCE7 (Success chip background)
        Public Shared ReadOnly StatusWarning As Color = Color.FromArgb(217, 119, 6)     ' #D97706 (Maintenance / Pending / Partial)
        Public Shared ReadOnly StatusWarningBg As Color = Color.FromArgb(254, 243, 199) ' #FEF3C7 (Warning chip background)
        Public Shared ReadOnly StatusDanger As Color = Color.FromArgb(220, 38, 38)      ' #DC2626 (Occupied / Overdue / Suspended)
        Public Shared ReadOnly StatusDangerBg As Color = Color.FromArgb(254, 226, 226)  ' #FEE2E2 (Danger chip background)
        Public Shared ReadOnly StatusInfo As Color = Color.FromArgb(37, 99, 235)        ' #2563EB (Reserved / In-Progress)
        Public Shared ReadOnly StatusInfoBg As Color = Color.FromArgb(219, 234, 254)   ' #DBEAFE (Info chip background)

        ' =========================================================================
        ' 2. LEGACY COMPATIBILITY TOKENS
        ' =========================================================================
        Public Shared ReadOnly BgDark As Color = Color.FromArgb(11, 15, 20)          ' #0B0F14
        Public Shared ReadOnly PanelDark As Color = Color.FromArgb(20, 28, 36)       ' #141C24
        Public Shared ReadOnly PanelAlt As Color = Color.FromArgb(26, 36, 47)        ' #1A242F
        Public Shared ReadOnly InputBg As Color = Color.FromArgb(15, 23, 32)         ' #0F1720
        Public Shared ReadOnly BorderColor As Color = Color.FromArgb(36, 50, 64)     ' #243240
        Public Shared ReadOnly BorderFocus As Color = Color.FromArgb(0, 230, 118)    ' Emerald focus
        
        Public Shared ReadOnly EmeraldPrimary As Color = Color.FromArgb(0, 230, 118) ' #00E676
        Public Shared ReadOnly EmeraldDark As Color = Color.FromArgb(16, 185, 129)   ' #10B981
        Public Shared ReadOnly EmeraldHover As Color = Color.FromArgb(34, 197, 94)   ' #22C55E
        Public Shared ReadOnly EmeraldGlow As Color = Color.FromArgb(20, 0, 230, 118)
        
        Public Shared ReadOnly Amber As Color = Color.FromArgb(245, 158, 11)         ' #F59E0B
        Public Shared ReadOnly Coral As Color = Color.FromArgb(239, 68, 68)          ' #EF4444
        Public Shared ReadOnly Blue As Color = Color.FromArgb(59, 130, 246)          ' #3B82F6

        Public Shared ReadOnly TextPrimary As Color = Color.FromArgb(248, 250, 252)  ' #F8FAFC
        Public Shared ReadOnly TextSecondary As Color = Color.FromArgb(148, 163, 184)' #94A3B8
        Public Shared ReadOnly TextMuted As Color = Color.FromArgb(100, 116, 139)    ' #64748B

        ' Typography Standard
        Public Shared ReadOnly TitleFont As New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Public Shared ReadOnly HeadingFont As New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Public Shared ReadOnly SubheadingFont As New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold)
        Public Shared ReadOnly BodyFont As New Font("Segoe UI", 9.5F, FontStyle.Regular)
        Public Shared ReadOnly SmallFont As New Font("Segoe UI", 8.5F, FontStyle.Regular)
        Public Shared ReadOnly MonoFont As New Font("Consolas", 10.0F, FontStyle.Regular)
        Public Shared ReadOnly MonoBoldFont As New Font("Consolas", 10.5F, FontStyle.Bold)

        ''' <summary>
        ''' Enables double-buffering on any Control using reflection to eliminate flicker.
        ''' </summary>
        Public Shared Sub EnableDoubleBuffering(ctrl As Control)
            If ctrl Is Nothing Then Return
            Try
                Dim prop As PropertyInfo = GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
                If prop IsNot Nothing Then
                    prop.SetValue(ctrl, True, Nothing)
                End If
            Catch
            End Try
        End Sub

        ''' <summary>
        ''' Creates a GraphicsPath with rounded corners for custom painting.
        ''' </summary>
        Public Shared Function CreateRoundedRectangle(rect As RectangleF, radius As Single) As GraphicsPath
            Dim path As New GraphicsPath()
            If radius <= 0 Then
                path.AddRectangle(rect)
                Return path
            End If

            Dim diameter As Single = radius * 2.0F
            Dim size As New SizeF(diameter, diameter)
            Dim arc As New RectangleF(rect.Location, size)

            ' Top Left
            path.AddArc(arc, 180, 90)

            ' Top Right
            arc.X = rect.Right - diameter
            path.AddArc(arc, 270, 90)

            ' Bottom Right
            arc.Y = rect.Bottom - diameter
            path.AddArc(arc, 0, 90)

            ' Bottom Left
            arc.X = rect.Left
            path.AddArc(arc, 90, 90)

            path.CloseFigure()
            Return path
        End Function

        ''' <summary>
        ''' Draws a prestigious academic shield insignia with an open book and architectural arch.
        ''' </summary>
        Public Shared Sub DrawAcademicCrest(g As Graphics, bounds As RectangleF, goldColor As Color, accentNavy As Color)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.PixelOffsetMode = PixelOffsetMode.HighQuality

            ' Outer Shield Path
            Using shieldPath As New GraphicsPath()
                Dim w As Single = bounds.Width
                Dim h As Single = bounds.Height
                Dim left As Single = bounds.Left
                Dim top As Single = bounds.Top
                Dim midX As Single = left + w / 2.0F
                Dim bottom As Single = top + h

                shieldPath.AddLine(left + 8, top, left + w - 8, top)
                shieldPath.AddArc(left + w - 16, top, 16, 16, 270, 90)
                shieldPath.AddLine(left + w, top + 8, left + w, top + h * 0.65F)
                shieldPath.AddBezier(New PointF(left + w, top + h * 0.65F), New PointF(left + w * 0.88F, bottom - 6), New PointF(midX + 12, bottom), New PointF(midX, bottom))
                shieldPath.AddBezier(New PointF(midX, bottom), New PointF(midX - 12, bottom), New PointF(left + w * 0.12F, bottom - 6), New PointF(left, top + h * 0.65F))
                shieldPath.AddLine(left, top + h * 0.65F, left, top + 8)
                shieldPath.AddArc(left, top, 16, 16, 180, 90)
                shieldPath.CloseFigure()

                ' Fill shield with deep navy gradient
                Using bgBrush As New LinearGradientBrush(bounds, Color.FromArgb(20, 45, 75), Color.FromArgb(10, 22, 38), LinearGradientMode.Vertical)
                    g.FillPath(bgBrush, shieldPath)
                End Using

                ' Shield Rim Gold Border
                Using rimPen As New Pen(goldColor, 2.5F)
                    g.DrawPath(rimPen, shieldPath)
                End Using

                ' Inner Thin Gold Accent
                Using innerShield As New GraphicsPath()
                    Dim inB As RectangleF = RectangleF.Inflate(bounds, -5, -5)
                    Dim inMidX As Single = inB.Left + inB.Width / 2.0F
                    Dim inBottom As Single = inB.Top + inB.Height
                    innerShield.AddLine(inB.Left + 6, inB.Top, inB.Left + inB.Width - 6, inB.Top)
                    innerShield.AddArc(inB.Left + inB.Width - 12, inB.Top, 12, 12, 270, 90)
                    innerShield.AddLine(inB.Right, inB.Top + 6, inB.Right, inB.Top + inB.Height * 0.63F)
                    innerShield.AddBezier(New PointF(inB.Right, inB.Top + inB.Height * 0.63F), New PointF(inB.Left + inB.Width * 0.88F, inBottom - 5), New PointF(inMidX + 10, inBottom), New PointF(inMidX, inBottom))
                    innerShield.AddBezier(New PointF(inMidX, inBottom), New PointF(inMidX - 10, inBottom), New PointF(inB.Left + inB.Width * 0.12F, inBottom - 5), New PointF(inB.Left, inB.Top + inB.Height * 0.63F))
                    innerShield.AddLine(inB.Left, inB.Top + inB.Height * 0.63F, inB.Left, inB.Top + 6)
                    innerShield.AddArc(inB.Left, inB.Top, 12, 12, 180, 90)
                    innerShield.CloseFigure()
                    Using innerPen As New Pen(Color.FromArgb(90, goldColor), 1.0F)
                        g.DrawPath(innerPen, innerShield)
                    End Using
                End Using

                ' Center Academic Emblem: Open Book & Architectural Arch
                Dim cx As Single = midX
                Dim cy As Single = top + h * 0.48F

                ' Architectural Arch
                Dim archRect As New RectangleF(cx - 20, cy - 22, 40, 42)
                Using archPen As New Pen(Color.FromArgb(180, goldColor), 1.8F)
                    g.DrawArc(archPen, archRect.Left, archRect.Top, archRect.Width, 24, 180, 180)
                    g.DrawLine(archPen, archRect.Left, archRect.Top + 12, archRect.Left, archRect.Bottom)
                    g.DrawLine(archPen, archRect.Right, archRect.Top + 12, archRect.Right, archRect.Bottom)
                End Using

                ' Stylized Open Book
                Using bookPen As New Pen(goldColor, 2.0F)
                    ' Left Page
                    g.DrawBezier(bookPen, cx, cy + 2, cx - 8, cy - 4, cx - 18, cy - 2, cx - 22, cy + 4)
                    g.DrawLine(bookPen, cx - 22, cy + 4, cx - 22, cy + 16)
                    g.DrawBezier(bookPen, cx - 22, cy + 16, cx - 18, cy + 10, cx - 8, cy + 8, cx, cy + 14)

                    ' Right Page
                    g.DrawBezier(bookPen, cx, cy + 2, cx + 8, cy - 4, cx + 18, cy - 2, cx + 22, cy + 4)
                    g.DrawLine(bookPen, cx + 22, cy + 4, cx + 22, cy + 16)
                    g.DrawBezier(bookPen, cx + 22, cy + 16, cx + 18, cy + 10, cx + 8, cy + 8, cx, cy + 14)

                    ' Spine
                    g.DrawLine(bookPen, cx, cy + 2, cx, cy + 15)
                End Using

                ' Golden Stars (Representing Excellence & Accommodation Directorate)
                Using starBrush As New SolidBrush(goldColor)
                    DrawSmallStar(g, starBrush, cx - 16, top + 15, 3.0F)
                    DrawSmallStar(g, starBrush, cx, top + 10, 4.0F)
                    DrawSmallStar(g, starBrush, cx + 16, top + 15, 3.0F)
                End Using
            End Using
        End Sub

        Private Shared Sub DrawSmallStar(g As Graphics, brush As Brush, cx As Single, cy As Single, radius As Single)
            Dim pts(7) As PointF
            For i As Integer = 0 To 7
                Dim r As Single = If(i Mod 2 = 0, radius, radius * 0.45F)
                Dim angle As Double = (i * Math.PI / 4.0) - (Math.PI / 2.0)
                pts(i) = New PointF(cx + CSng(Math.Cos(angle) * r), cy + CSng(Math.Sin(angle) * r))
            Next
            g.FillPolygon(brush, pts)
        End Sub

        ''' <summary>
        ''' Applies standard primary button styling.
        ''' </summary>
        Public Shared Sub StylePrimaryButton(btn As Button)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.BackColor = NavyPrimary
            btn.ForeColor = TextWhite
            btn.Font = SubheadingFont
            btn.Cursor = Cursors.Hand
            btn.UseVisualStyleBackColor = False
        End Sub

        ''' <summary>
        ''' Applies secondary/cancel button styling.
        ''' </summary>
        Public Shared Sub StyleSecondaryButton(btn As Button)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderColor = BorderSubtle
            btn.FlatAppearance.BorderSize = 1
            btn.BackColor = SurfaceCanvas
            btn.ForeColor = TextSlateSecondary
            btn.Font = BodyFont
            btn.Cursor = Cursors.Hand
            btn.UseVisualStyleBackColor = False
        End Sub

        ''' <summary>
        ''' Applies the authoritative Prestige Academic table styling to any DataGridView.
        ''' </summary>
        Public Shared Sub ApplyPrestigeGridStyle(dgv As DataGridView)
            If dgv Is Nothing Then Return

            EnableDoubleBuffering(dgv)

            dgv.BackgroundColor = SurfaceCanvas
            dgv.BorderStyle = BorderStyle.None
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            dgv.GridColor = BorderSubtle
            dgv.RowHeadersVisible = False
            dgv.EnableHeadersVisualStyles = False
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.MultiSelect = False
            dgv.AllowUserToAddRows = False
            dgv.AllowUserToDeleteRows = False
            dgv.AllowUserToResizeRows = False
            dgv.AutoGenerateColumns = False
            dgv.RowTemplate.Height = 36

            ' Column Headers
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            dgv.ColumnHeadersHeight = 38
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            dgv.ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle() With {
                .BackColor = NavyDark,
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
                .Alignment = DataGridViewContentAlignment.MiddleLeft,
                .Padding = New Padding(8, 0, 8, 0)
            }

            ' Default Cell Style
            dgv.DefaultCellStyle = New DataGridViewCellStyle() With {
                .BackColor = Color.White,
                .ForeColor = TextDeepSlate,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                .SelectionBackColor = Color.FromArgb(226, 238, 253),
                .SelectionForeColor = TextDeepSlate,
                .Padding = New Padding(8, 0, 8, 0)
            }

            ' Alternating Row Style
            dgv.AlternatingRowsDefaultCellStyle = New DataGridViewCellStyle() With {
                .BackColor = Color.FromArgb(248, 250, 252),
                .ForeColor = TextDeepSlate,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                .SelectionBackColor = Color.FromArgb(226, 238, 253),
                .SelectionForeColor = TextDeepSlate,
                .Padding = New Padding(8, 0, 8, 0)
            }
        End Sub
    End Class
End Namespace

