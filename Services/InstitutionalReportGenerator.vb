Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Services
    ''' <summary>
    ''' Service that generates institutional university reports formatted for printing and digital archiving.
    ''' Produces print documents with university crests, headers, tabular layouts, and export to HTML.
    ''' </summary>
    Public Class InstitutionalReportGenerator
        Public Enum ReportKind
            Occupancy
            Allocations
            FinancialDefaulters
            Maintenance
        End Enum

        Private ReadOnly _meta As ReportMetadata
        Private ReadOnly _kind As ReportKind
        Private ReadOnly _data As Object

        Public Sub New(meta As ReportMetadata, kind As ReportKind, data As Object)
            _meta = meta
            _kind = kind
            _data = data
        End Sub

        ''' <summary>
        ''' Creates a configured PrintDocument ready for PrintPreviewDialog or physical printing.
        ''' </summary>
        Public Function CreatePrintDocument() As PrintDocument
            Dim doc As New PrintDocument()
            doc.DefaultPageSettings.Margins = New Margins(40, 40, 40, 40)
            doc.DefaultPageSettings.Landscape = (_kind = ReportKind.Allocations OrElse _kind = ReportKind.FinancialDefaulters)

            Dim currentPage As Integer = 1
            Dim currentIndex As Integer = 0

            AddHandler doc.BeginPrint, Sub(s, e)
                                           currentPage = 1
                                           currentIndex = 0
                                       End Sub

            AddHandler doc.PrintPage, Sub(s, e)
                                          Dim g As Graphics = e.Graphics
                                          g.SmoothingMode = SmoothingMode.AntiAlias
                                          g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

                                          Dim bounds As Rectangle = e.MarginBounds
                                          Dim currentY As Single = bounds.Top

                                          ' 1. Header (Crest, Title, Metadata)
                                          currentY = RenderOfficialHeader(g, bounds, currentY, currentPage)

                                          ' 2. Render Table Headers and rows
                                          Dim hasMorePages As Boolean = False
                                          Select Case _kind
                                              Case ReportKind.Occupancy
                                                  hasMorePages = RenderOccupancyTable(g, bounds, currentY, currentIndex, DirectCast(_data, List(Of OccupancyReportItem)))
                                              Case ReportKind.Allocations
                                                  hasMorePages = RenderAllocationsTable(g, bounds, currentY, currentIndex, DirectCast(_data, List(Of AllocationReportItem)))
                                              Case ReportKind.FinancialDefaulters
                                                  hasMorePages = RenderFinancialTable(g, bounds, currentY, currentIndex, DirectCast(_data, List(Of FinancialReportItem)))
                                              Case ReportKind.Maintenance
                                                  hasMorePages = RenderMaintenanceTable(g, bounds, currentY, currentIndex, DirectCast(_data, List(Of MaintenanceReportItem)))
                                          End Select

                                          ' 3. Render Footer (Page numbers & Signatures on last page)
                                          RenderFooter(g, bounds, currentPage, Not hasMorePages)

                                          If hasMorePages Then
                                              currentPage += 1
                                              e.HasMorePages = True
                                          Else
                                              e.HasMorePages = False
                                          End If
                                      End Sub

            Return doc
        End Function

        ''' <summary>
        ''' Generates a standalone, beautifully styled HTML report ready for browser preview or PDF printing.
        ''' </summary>
        Public Function GenerateHtmlReport() As String
            Dim sb As New StringBuilder()
            sb.AppendLine("<!DOCTYPE html>")
            sb.AppendLine("<html lang='en'>")
            sb.AppendLine("<head>")
            sb.AppendLine("<meta charset='utf-8'/>")
            sb.AppendLine($"<title>{_meta.ReportTitle}</title>")
            sb.AppendLine("<style>")
            sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 30px; color: #0F172A; background: #fff; }")
            sb.AppendLine(".header { text-align: center; border-bottom: 3px double #0A192F; padding-bottom: 12px; margin-bottom: 20px; }")
            sb.AppendLine(".crest { font-size: 26px; font-weight: bold; color: #D97706; letter-spacing: 2px; }")
            sb.AppendLine(".title { font-size: 20px; font-weight: bold; color: #0A192F; margin: 4px 0; text-transform: uppercase; }")
            sb.AppendLine(".sub { font-size: 13px; color: #475569; }")
            sb.AppendLine(".meta-grid { display: flex; justify-content: space-between; font-size: 11px; margin-bottom: 15px; background: #F8FAFC; padding: 10px 15px; border-radius: 6px; border: 1px solid #E2E8F0; }")
            sb.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 10px; font-size: 11px; }")
            sb.AppendLine("th { background: #0A192F; color: #ffffff; padding: 8px 10px; text-align: left; font-weight: 600; }")
            sb.AppendLine("td { padding: 6px 10px; border-bottom: 1px solid #E2E8F0; }")
            sb.AppendLine("tr:nth-child(even) { background: #F8FAFC; }")
            sb.AppendLine(".badge { display: inline-block; padding: 2px 8px; border-radius: 4px; font-weight: 600; font-size: 10px; }")
            sb.AppendLine(".badge-green { background: #DCFCE7; color: #166534; }")
            sb.AppendLine(".badge-amber { background: #FEF3C7; color: #92400E; }")
            sb.AppendLine(".badge-red { background: #FEE2E2; color: #991B1B; }")
            sb.AppendLine(".text-right { text-align: right; }")
            sb.AppendLine(".footer { margin-top: 40px; font-size: 11px; border-top: 1px solid #CBD5E1; padding-top: 15px; }")
            sb.AppendLine(".signatures { display: flex; justify-content: space-between; margin-top: 30px; }")
            sb.AppendLine(".sign-box { border-top: 1px dashed #64748B; width: 220px; text-align: center; padding-top: 5px; }")
            sb.AppendLine("@media print { .no-print { display: none; } }")
            sb.AppendLine("</style>")
            sb.AppendLine("</head>")
            sb.AppendLine("<body>")

            ' Header
            sb.AppendLine("<div class='header'>")
            sb.AppendLine("<div class='crest'>DEDAN KIMATHI UNIVERSITY OF TECHNOLOGY — DIRECTORATE OF ACCOMMODATION</div>")
            sb.AppendLine($"<div class='title'>{_meta.ReportTitle}</div>")
            sb.AppendLine($"<div class='sub'>{_meta.Subtitle}</div>")
            sb.AppendLine("</div>")

            ' Meta
            sb.AppendLine("<div class='meta-grid'>")
            sb.AppendLine($"<div><strong>Academic Year / Semester:</strong> {_meta.SemesterName}</div>")
            sb.AppendLine($"<div><strong>Generated By:</strong> {_meta.GeneratedBy}</div>")
            sb.AppendLine($"<div><strong>Date & Time:</strong> {_meta.GeneratedAt:yyyy-MM-dd HH:mm:ss}</div>")
            sb.AppendLine($"<div><strong>Filter Scope:</strong> {_meta.FilterDescription}</div>")
            sb.AppendLine("</div>")

            ' Table Content
            Select Case _kind
                Case ReportKind.Occupancy
                    AppendOccupancyHtml(sb, DirectCast(_data, List(Of OccupancyReportItem)))
                Case ReportKind.Allocations
                    AppendAllocationsHtml(sb, DirectCast(_data, List(Of AllocationReportItem)))
                Case ReportKind.FinancialDefaulters
                    AppendFinancialHtml(sb, DirectCast(_data, List(Of FinancialReportItem)))
                Case ReportKind.Maintenance
                    AppendMaintenanceHtml(sb, DirectCast(_data, List(Of MaintenanceReportItem)))
            End Select

            ' Signatures
            sb.AppendLine("<div class='signatures'>")
            sb.AppendLine("<div class='sign-box'>Prepared By (Officer)</div>")
            sb.AppendLine("<div class='sign-box'>Verified By (Hostel Warden)</div>")
            sb.AppendLine("<div class='sign-box'>Authorized By (University Bursar)</div>")
            sb.AppendLine("</div>")

            sb.AppendLine("<div class='footer'>")
            sb.AppendLine($"<div style='float:right;'>Total Records: {_meta.TotalRecords}</div>")
            sb.AppendLine("<div>University Hostel Management System — Official Academic Record</div>")
            sb.AppendLine("</div>")

            sb.AppendLine("</body>")
            sb.AppendLine("</html>")

            Return sb.ToString()
        End Function

        Private Function RenderOfficialHeader(g As Graphics, bounds As Rectangle, startY As Single, pageNum As Integer) As Single
            Dim y As Single = startY

            ' Draw crest shield on first page
            Dim crestRect As New RectangleF(bounds.Left + 5, y, 40, 44)
            ThemeManager.DrawAcademicCrest(g, crestRect, ThemeManager.AcademicGold, ThemeManager.NavyPrimary)

            ' Titles
            Using fontCrest As New Font("Segoe UI", 11.0F, FontStyle.Bold)
                Using brushNavy As New SolidBrush(ThemeManager.NavyPrimary)
                    g.DrawString("DEDAN KIMATHI UNIVERSITY OF TECHNOLOGY — ACCOMMODATION BOARD", fontCrest, brushNavy, bounds.Left + 55, y)
                End Using
            End Using

            Using fontTitle As New Font("Segoe UI", 10.0F, FontStyle.Bold)
                Using brushGold As New SolidBrush(ThemeManager.AcademicGold)
                    g.DrawString(_meta.ReportTitle.ToUpperInvariant(), fontTitle, brushGold, bounds.Left + 55, y + 20)
                End Using
            End Using

            Using fontSub As New Font("Segoe UI", 8.0F, FontStyle.Regular)
                Using brushSlate As New SolidBrush(Color.FromArgb(100, 116, 139))
                    g.DrawString($"{_meta.Subtitle}  |  Semester: {_meta.SemesterName}  |  Date: {_meta.GeneratedAt:yyyy-MM-dd HH:mm}", fontSub, brushSlate, bounds.Left + 55, y + 36)
                End Using
            End Using

            y += 50

            ' Divider
            Using penLine As New Pen(ThemeManager.NavyPrimary, 1.5F)
                g.DrawLine(penLine, bounds.Left, y, bounds.Right, y)
            End Using

            Return y + 8
        End Function

        Private Function RenderOccupancyTable(g As Graphics, bounds As Rectangle, startY As Single, ByRef index As Integer, items As List(Of OccupancyReportItem)) As Boolean
            Dim y As Single = startY
            Dim fontH As New Font("Segoe UI", 8.0F, FontStyle.Bold)
            Dim fontD As New Font("Segoe UI", 8.0F, FontStyle.Regular)
            Dim rowH As Single = 20.0F

            ' Headers
            Dim colX As Single() = {bounds.Left, bounds.Left + 180, bounds.Left + 260, bounds.Left + 330, bounds.Left + 400, bounds.Left + 470, bounds.Left + 540, bounds.Left + 610}
            Dim colW As Single() = {180, 80, 70, 70, 70, 70, 70, bounds.Right - (bounds.Left + 610)}
            Dim headers As String() = {"Hostel Facility", "Room", "Floor", "Type", "Capacity", "Occupied", "Available", "Occupancy %"}

            Using brushH As New SolidBrush(ThemeManager.NavyPrimary)
                g.FillRectangle(brushH, bounds.Left, y, bounds.Width, rowH)
            End Using
            For i As Integer = 0 To headers.Length - 1
                g.DrawString(headers(i), fontH, Brushes.White, colX(i) + 4, y + 3)
            Next
            y += rowH

            ' Rows
            While index < items.Count
                Dim item = items(index)
                If y + rowH > bounds.Bottom - 60 Then
                    Return True ' Request next page
                End If

                If index Mod 2 = 1 Then
                    Using brushRow As New SolidBrush(Color.FromArgb(248, 250, 252))
                        g.FillRectangle(brushRow, bounds.Left, y, bounds.Width, rowH)
                    End Using
                End If

                g.DrawString($"{item.HostelName} ({item.HostelCode})", fontD, Brushes.Black, colX(0) + 4, y + 3)
                g.DrawString(item.RoomNumber, fontD, Brushes.Black, colX(1) + 4, y + 3)
                g.DrawString($"Flr {item.FloorNumber}", fontD, Brushes.Black, colX(2) + 4, y + 3)
                g.DrawString(item.RoomType, fontD, Brushes.Black, colX(3) + 4, y + 3)
                g.DrawString(item.Capacity.ToString(), fontD, Brushes.Black, colX(4) + 4, y + 3)
                g.DrawString(item.OccupiedBeds.ToString(), fontD, Brushes.Black, colX(5) + 4, y + 3)
                g.DrawString(item.AvailableBeds.ToString(), fontD, Brushes.Black, colX(6) + 4, y + 3)
                g.DrawString($"{item.OccupancyPercentage:F1}%", fontD, Brushes.Black, colX(7) + 4, y + 3)

                Using penRow As New Pen(Color.FromArgb(226, 232, 240), 1)
                    g.DrawLine(penRow, bounds.Left, y + rowH, bounds.Right, y + rowH)
                End Using

                y += rowH
                index += 1
            End While

            Return False
        End Function

        Private Function RenderAllocationsTable(g As Graphics, bounds As Rectangle, startY As Single, ByRef index As Integer, items As List(Of AllocationReportItem)) As Boolean
            Dim y As Single = startY
            Dim fontH As New Font("Segoe UI", 7.5F, FontStyle.Bold)
            Dim fontD As New Font("Segoe UI", 7.5F, FontStyle.Regular)
            Dim rowH As Single = 19.0F

            Dim colW As Single() = {90, 140, 110, 80, 80, 75, 75, 75, 70}
            Dim headers As String() = {"Reg Number", "Student Name", "Course", "Bed Unit", "Fee (KES)", "Paid", "Balance", "Check-In", "Status"}

            Using brushH As New SolidBrush(ThemeManager.NavyPrimary)
                g.FillRectangle(brushH, bounds.Left, y, bounds.Width, rowH)
            End Using

            Dim curX As Single = bounds.Left
            For i As Integer = 0 To headers.Length - 1
                g.DrawString(headers(i), fontH, Brushes.White, curX + 3, y + 3)
                curX += colW(i)
            Next
            y += rowH

            While index < items.Count
                Dim item = items(index)
                If y + rowH > bounds.Bottom - 60 Then Return True

                If index Mod 2 = 1 Then
                    Using brushRow As New SolidBrush(Color.FromArgb(248, 250, 252))
                        g.FillRectangle(brushRow, bounds.Left, y, bounds.Width, rowH)
                    End Using
                End If

                curX = bounds.Left
                g.DrawString(item.RegistrationNumber, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(0)
                g.DrawString(item.StudentName, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(1)
                g.DrawString(item.Course, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(2)
                g.DrawString(item.BedLabel, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(3)
                g.DrawString(item.SemesterFee.ToString("N0"), fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(4)
                g.DrawString(item.AmountPaid.ToString("N0"), fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(5)
                g.DrawString(item.BalanceDue.ToString("N0"), fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(6)
                g.DrawString(If(item.CheckInDate.HasValue, item.CheckInDate.Value.ToString("yyyy-MM-dd"), "Pending"), fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(7)
                g.DrawString(item.AllocationStatus, fontD, Brushes.Black, curX + 3, y + 3)

                Using penRow As New Pen(Color.FromArgb(226, 232, 240), 1)
                    g.DrawLine(penRow, bounds.Left, y + rowH, bounds.Right, y + rowH)
                End Using

                y += rowH
                index += 1
            End While

            Return False
        End Function

        Private Function RenderFinancialTable(g As Graphics, bounds As Rectangle, startY As Single, ByRef index As Integer, items As List(Of FinancialReportItem)) As Boolean
            Dim y As Single = startY
            Dim fontH As New Font("Segoe UI", 7.5F, FontStyle.Bold)
            Dim fontD As New Font("Segoe UI", 7.5F, FontStyle.Regular)
            Dim rowH As Single = 19.0F

            Dim colW As Single() = {95, 140, 85, 110, 80, 80, 80, 80, 65}
            Dim headers As String() = {"Reg Number", "Student Name", "Phone", "Bed Assigned", "Invoiced", "Paid", "Balance Due", "Status", "Paid %"}

            Using brushH As New SolidBrush(ThemeManager.NavyPrimary)
                g.FillRectangle(brushH, bounds.Left, y, bounds.Width, rowH)
            End Using

            Dim curX As Single = bounds.Left
            For i As Integer = 0 To headers.Length - 1
                g.DrawString(headers(i), fontH, Brushes.White, curX + 3, y + 3)
                curX += colW(i)
            Next
            y += rowH

            While index < items.Count
                Dim item = items(index)
                If y + rowH > bounds.Bottom - 60 Then Return True

                If index Mod 2 = 1 Then
                    Using brushRow As New SolidBrush(Color.FromArgb(248, 250, 252))
                        g.FillRectangle(brushRow, bounds.Left, y, bounds.Width, rowH)
                    End Using
                End If

                curX = bounds.Left
                g.DrawString(item.RegistrationNumber, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(0)
                g.DrawString(item.StudentName, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(1)
                g.DrawString(item.PhoneNumber, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(2)
                g.DrawString(item.BedLabel, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(3)
                g.DrawString(item.SemesterFee.ToString("N0"), fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(4)
                g.DrawString(item.TotalPaid.ToString("N0"), fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(5)
                g.DrawString(item.BalanceDue.ToString("N0"), fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(6)
                g.DrawString(item.PaymentStatus, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(7)
                g.DrawString($"{item.CollectionPercentage:F1}%", fontD, Brushes.Black, curX + 3, y + 3)

                Using penRow As New Pen(Color.FromArgb(226, 232, 240), 1)
                    g.DrawLine(penRow, bounds.Left, y + rowH, bounds.Right, y + rowH)
                End Using

                y += rowH
                index += 1
            End While

            Return False
        End Function

        Private Function RenderMaintenanceTable(g As Graphics, bounds As Rectangle, startY As Single, ByRef index As Integer, items As List(Of MaintenanceReportItem)) As Boolean
            Dim y As Single = startY
            Dim fontH As New Font("Segoe UI", 8.0F, FontStyle.Bold)
            Dim fontD As New Font("Segoe UI", 8.0F, FontStyle.Regular)
            Dim rowH As Single = 20.0F

            Dim colW As Single() = {45, 140, 180, 85, 100, 80, 55}
            Dim headers As String() = {"ID", "Target Facility", "Issue Description", "Status", "Technician", "Reported", "Downtime"}

            Using brushH As New SolidBrush(ThemeManager.NavyPrimary)
                g.FillRectangle(brushH, bounds.Left, y, bounds.Width, rowH)
            End Using

            Dim curX As Single = bounds.Left
            For i As Integer = 0 To headers.Length - 1
                g.DrawString(headers(i), fontH, Brushes.White, curX + 3, y + 3)
                curX += colW(i)
            Next
            y += rowH

            While index < items.Count
                Dim item = items(index)
                If y + rowH > bounds.Bottom - 60 Then Return True

                If index Mod 2 = 1 Then
                    Using brushRow As New SolidBrush(Color.FromArgb(248, 250, 252))
                        g.FillRectangle(brushRow, bounds.Left, y, bounds.Width, rowH)
                    End Using
                End If

                curX = bounds.Left
                g.DrawString($"#{item.MaintenanceId}", fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(0)
                g.DrawString(item.TargetDescription, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(1)
                g.DrawString(item.IssueTitle, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(2)
                g.DrawString(item.MaintenanceStatus, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(3)
                g.DrawString(item.AssignedTo, fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(4)
                g.DrawString(item.ReportedDate.ToString("yyyy-MM-dd"), fontD, Brushes.Black, curX + 3, y + 3) : curX += colW(5)
                g.DrawString($"{item.DowntimeDays} d", fontD, Brushes.Black, curX + 3, y + 3)

                Using penRow As New Pen(Color.FromArgb(226, 232, 240), 1)
                    g.DrawLine(penRow, bounds.Left, y + rowH, bounds.Right, y + rowH)
                End Using

                y += rowH
                index += 1
            End While

            Return False
        End Function

        Private Sub RenderFooter(g As Graphics, bounds As Rectangle, pageNum As Integer, isLastPage As Boolean)
            Dim y As Single = bounds.Bottom - 30

            ' Page Number
            Using fontFoot As New Font("Segoe UI", 8.0F, FontStyle.Regular)
                Using brushFoot As New SolidBrush(Color.FromArgb(100, 116, 139))
                    g.DrawString($"Page {pageNum}", fontFoot, brushFoot, bounds.Right - 60, y)
                    g.DrawString("University Hostel Management System — Official Academic Document", fontFoot, brushFoot, bounds.Left, y)
                End Using
            End Using
        End Sub

        ' HTML Content appends
        Private Sub AppendOccupancyHtml(sb As StringBuilder, items As List(Of OccupancyReportItem))
            sb.AppendLine("<table>")
            sb.AppendLine("<thead><tr><th>Hostel Facility</th><th>Room</th><th>Floor</th><th>Type</th><th class='text-right'>Capacity</th><th class='text-right'>Occupied</th><th class='text-right'>Available</th><th>Status</th><th class='text-right'>Occupancy %</th></tr></thead>")
            sb.AppendLine("<tbody>")
            For Each it In items
                Dim badgeClass As String = If(it.OccupancyPercentage >= 100.0, "badge-red", If(it.OccupancyPercentage > 0, "badge-amber", "badge-green"))
                sb.AppendLine($"<tr><td><strong>{it.HostelName}</strong> ({it.HostelCode})</td><td>{it.RoomNumber}</td><td>Floor {it.FloorNumber}</td><td>{it.RoomType}</td><td class='text-right'>{it.Capacity}</td><td class='text-right'>{it.OccupiedBeds}</td><td class='text-right'>{it.AvailableBeds}</td><td><span class='badge {badgeClass}'>{it.StatusText}</span></td><td class='text-right'>{it.OccupancyPercentage:F1}%</td></tr>")
            Next
            sb.AppendLine("</tbody></table>")
        End Sub

        Private Sub AppendAllocationsHtml(sb As StringBuilder, items As List(Of AllocationReportItem))
            sb.AppendLine("<table>")
            sb.AppendLine("<thead><tr><th>Reg Number</th><th>Student Name</th><th>Course</th><th>Bed Assigned</th><th class='text-right'>Fee (KES)</th><th class='text-right'>Paid (KES)</th><th class='text-right'>Balance (KES)</th><th>Check-In Date</th><th>Status</th></tr></thead>")
            sb.AppendLine("<tbody>")
            For Each it In items
                Dim checkIn As String = If(it.CheckInDate.HasValue, it.CheckInDate.Value.ToString("yyyy-MM-dd"), "<span style='color:#94A3B8;'>Pending</span>")
                sb.AppendLine($"<tr><td><code>{it.RegistrationNumber}</code></td><td><strong>{it.StudentName}</strong></td><td>{it.Course}</td><td>{it.BedLabel} ({it.HostelCode}-{it.RoomNumber})</td><td class='text-right'>{it.SemesterFee:N2}</td><td class='text-right'>{it.AmountPaid:N2}</td><td class='text-right'><strong>{it.BalanceDue:N2}</strong></td><td>{checkIn}</td><td><span class='badge badge-green'>{it.AllocationStatus}</span></td></tr>")
            Next
            sb.AppendLine("</tbody></table>")
        End Sub

        Private Sub AppendFinancialHtml(sb As StringBuilder, items As List(Of FinancialReportItem))
            sb.AppendLine("<table>")
            sb.AppendLine("<thead><tr><th>Reg Number</th><th>Student Name</th><th>Phone</th><th>Bed Unit</th><th class='text-right'>Invoiced (KES)</th><th class='text-right'>Paid (KES)</th><th class='text-right'>Balance Due (KES)</th><th>Status</th><th class='text-right'>Collection %</th></tr></thead>")
            sb.AppendLine("<tbody>")
            For Each it In items
                Dim badgeClass As String = If(it.PaymentStatus = "CLEARED", "badge-green", If(it.PaymentStatus = "PARTIAL", "badge-amber", "badge-red"))
                sb.AppendLine($"<tr><td><code>{it.RegistrationNumber}</code></td><td><strong>{it.StudentName}</strong></td><td>{it.PhoneNumber}</td><td>{it.BedLabel} ({it.HostelCode}-{it.RoomNumber})</td><td class='text-right'>{it.SemesterFee:N2}</td><td class='text-right'>{it.TotalPaid:N2}</td><td class='text-right' style='color:#DC2626;'><strong>{it.BalanceDue:N2}</strong></td><td><span class='badge {badgeClass}'>{it.PaymentStatus}</span></td><td class='text-right'>{it.CollectionPercentage:F1}%</td></tr>")
            Next
            sb.AppendLine("</tbody></table>")
        End Sub

        Private Sub AppendMaintenanceHtml(sb As StringBuilder, items As List(Of MaintenanceReportItem))
            sb.AppendLine("<table>")
            sb.AppendLine("<thead><tr><th>ID</th><th>Facility</th><th>Issue Title</th><th>Status</th><th>Technician</th><th>Reported Date</th><th>Resolved Date</th><th class='text-right'>Downtime</th></tr></thead>")
            sb.AppendLine("<tbody>")
            For Each it In items
                Dim badgeClass As String = If(it.MaintenanceStatus = "RESOLVED", "badge-green", If(it.MaintenanceStatus = "IN_PROGRESS", "badge-amber", "badge-red"))
                Dim resDate As String = If(it.ResolvedDate.HasValue, it.ResolvedDate.Value.ToString("yyyy-MM-dd"), "-")
                sb.AppendLine($"<tr><td>#{it.MaintenanceId}</td><td><strong>{it.TargetDescription}</strong></td><td>{it.IssueTitle}</td><td><span class='badge {badgeClass}'>{it.MaintenanceStatus}</span></td><td>{it.AssignedTo}</td><td>{it.ReportedDate:yyyy-MM-dd}</td><td>{resDate}</td><td class='text-right'>{it.DowntimeDays} days</td></tr>")
            Next
            sb.AppendLine("</tbody></table>")
        End Sub
    End Class
End Namespace

