Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports University_hostel_mgt.Helpers

Namespace Controls.Views
    ''' <summary>
    ''' Institutional placeholder view providing high-level previews of upcoming roadmap phases (Allocations, Billing, Reports).
    ''' </summary>
    Public Class PlaceholderControl
        Inherits UserControl

        Private pnlCard As Panel
        Private lblPill As Label
        Private lblTitle As Label
        Private lblSub As Label
        Private lblSpecs As Label

        Public Sub New(phaseTag As String, title As String, subtitle As String, specs As String)
            InitializeComponents(phaseTag, title, subtitle, specs)
            ThemeManager.EnableDoubleBuffering(Me)
        End Sub

        Private Sub InitializeComponents(phaseTag As String, title As String, subtitle As String, specs As String)
            Me.Dock = DockStyle.Fill
            Me.BackColor = ThemeManager.SurfaceCanvas
            Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)

            pnlCard = New Panel With {
                .Size = New Size(700, 420),
                .BackColor = Color.White,
                .Anchor = AnchorStyles.None,
                .Padding = New Padding(36)
            }
            pnlCard.Location = New Point((Me.ClientSize.Width - pnlCard.Width) \ 2, (Me.ClientSize.Height - pnlCard.Height) \ 2)

            AddHandler Me.Resize, Sub()
                                      pnlCard.Location = New Point(Math.Max(20, (Me.ClientSize.Width - pnlCard.Width) \ 2), Math.Max(20, (Me.ClientSize.Height - pnlCard.Height) \ 2))
                                  End Sub

            AddHandler pnlCard.Paint, Sub(s, e)
                                          Using pen As New Pen(ThemeManager.BorderSubtle, 1.0F)
                                              e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1)
                                          End Using
                                      End Sub

            ' Phase Badge Pill
            lblPill = New Label With {
                .Text = phaseTag.ToUpperInvariant(),
                .Font = New Font("Segoe UI", 8.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.AcademicGold,
                .BackColor = ThemeManager.AcademicGoldLight,
                .Size = New Size(180, 26),
                .Location = New Point(36, 36),
                .TextAlign = ContentAlignment.MiddleCenter
            }

            lblTitle = New Label With {
                .Text = title,
                .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
                .ForeColor = ThemeManager.TextDeepSlate,
                .Location = New Point(36, 76),
                .Size = New Size(628, 36),
                .UseMnemonic = False
            }

            lblSub = New Label With {
                .Text = subtitle,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Regular),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(36, 120),
                .Size = New Size(628, 48),
                .UseMnemonic = False
            }

            Dim pnlDivider As New Panel With {
                .BackColor = ThemeManager.BorderSubtle,
                .Location = New Point(36, 180),
                .Size = New Size(628, 1)
            }

            lblSpecs = New Label With {
                .Text = specs,
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Regular),
                .ForeColor = ThemeManager.TextSlateSecondary,
                .Location = New Point(36, 200),
                .Size = New Size(628, 180),
                .UseMnemonic = False
            }

            pnlCard.Controls.AddRange({lblPill, lblTitle, lblSub, pnlDivider, lblSpecs})
            Me.Controls.Add(pnlCard)
        End Sub
    End Class
End Namespace

