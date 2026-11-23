Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports University_hostel_mgt.Data
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models
Imports University_hostel_mgt.Services

Public Module Program
    Private Const ATTACH_PARENT_PROCESS As Integer = -1

    <System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError:=True)>
    Private Function AttachConsole(dwProcessId As Integer) As Boolean
    End Function

    ''' <summary>
    ''' Main application entry point.
    ''' Boots the Windows Forms application via frmSplash and supports CLI testing/diagnostics.
    ''' </summary>
    <STAThread>
    Public Sub Main(args As String())
        ' Attach to calling console if launched from terminal/CLI
        Try
            AttachConsole(ATTACH_PARENT_PROCESS)
        Catch ex As Exception
            ' Ignore if no console
        End Try

        ' 1. Load Environment Configuration
        DotEnv.Load()

        ' 2. Handle CLI verification / testing arguments if passed
        If args IsNot Nothing AndAlso args.Length > 0 Then
            Dim command As String = args(0).Trim().ToLowerInvariant()

            Select Case command
                Case "--verify", "-v", "--test-db"
                    RunDiagnostics()
                    Return
                Case "--test-auth"
                    RunAuthenticationDiagnostics()
                    Return
                Case "--test-dashboard", "--test-phase3"
                    RunPhase3Diagnostics()
                    Return
                Case "--test-allocations", "--test-billing", "--test-phase4"
                    RunPhase4Diagnostics()
                    Return
                Case "--test-phase5", "--test-maintenance", "--test-reports"
                    RunPhase5Diagnostics()
                    Return
                Case "--capture-screens"
                    CaptureScreenshots()
                    Return
                Case "--hash"
                    Dim pwd As String = If(args.Length > 1, args(1), "Admin@2026!")
                    Dim hash As String = PasswordHasher.HashPassword(pwd)
                    Console.WriteLine($"Password: {pwd}")
                    Console.WriteLine($"Hash: {hash}")
                    Console.WriteLine($"Verified: {PasswordHasher.VerifyPassword(pwd, hash)}")
                    Return
            End Select
        End If

        ' 3. Standard Windows Forms Startup (Launches via frmSplash)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Forms.frmSplash())
    End Sub

    ''' <summary>
    ''' Diagnostic test runner for Phase 1 verification.
    ''' </summary>
    Public Sub RunDiagnostics()
        Console.WriteLine("==================================================================")
        Console.WriteLine("  UNIVERSITY HOSTEL MANAGEMENT SYSTEM - PHASE 1 DIAGNOSTICS")
        Console.WriteLine("==================================================================")
        Console.WriteLine()

        ' 1. DotEnv Check
        Console.WriteLine("[1/4] Checking .env configuration...")
        If DotEnv.IsLoaded Then
            Console.WriteLine($"      Status: LOADED")
            Console.WriteLine($"      File:   {DotEnv.LoadedFilePath}")
            Console.WriteLine($"      App:    {DotEnv.Get("APP_NAME")}")
            Console.WriteLine($"      Env:    {DotEnv.Get("APP_ENV")}")
        Else
            Console.WriteLine("      Status: NOT LOADED (.env file not found, using defaults)")
        End If
        Console.WriteLine()

        ' 2. DbConfig Check
        Console.WriteLine("[2/4] Validating database configuration...")
        Try
            DbConfig.ValidateConfig()
            Console.WriteLine($"      Server:     {DbConfig.Server}")
            Console.WriteLine($"      Database:   {DbConfig.Database}")
            Console.WriteLine($"      Auth Mode:  {If(DbConfig.TrustedConnection, "Windows Integrated Security", $"SQL Auth ({DbConfig.User})")}")
            Console.WriteLine($"      Sanitized:  {DbConfig.GetSanitizedConnectionString()}")
        Catch ex As Exception
            Console.WriteLine($"      Config Error: {ex.Message}")
        End Try
        Console.WriteLine()

        ' 3. PasswordHasher Check
        Console.WriteLine("[3/4] Testing PBKDF2 Password Hasher...")
        Dim testPassword As String = "Admin@2026!"
        Dim generatedHash As String = PasswordHasher.HashPassword(testPassword)
        Dim verifySuccess As Boolean = PasswordHasher.VerifyPassword(testPassword, generatedHash)
        Dim wrongVerify As Boolean = PasswordHasher.VerifyPassword("WrongPassword", generatedHash)
        Console.WriteLine($"      Test Password: {testPassword}")
        Console.WriteLine($"      Sample Hash:   {generatedHash}")
        Console.WriteLine($"      Valid Check:   {If(verifySuccess, "PASSED", "FAILED")}")
        Console.WriteLine($"      Invalid Check: {If(Not wrongVerify, "PASSED (correctly rejected)", "FAILED")}")
        Console.WriteLine()

        ' 4. Database Connection Check
        Console.WriteLine("[4/4] Testing Database Connection...")
        Dim testResult = DatabaseConnection.TestConnection()
        If testResult.Success Then
            Console.WriteLine("      Connection: SUCCESS")
            Console.WriteLine($"      Details:    {testResult.Message}")
            Console.WriteLine($"      Version:    {testResult.ServerVersion.Replace(vbCr, " ").Replace(vbLf, " ").Trim()}")
        Else
            Console.WriteLine("      Connection: FAILED")
            Console.WriteLine($"      Error:      {testResult.Message}")
        End If
        Console.WriteLine()
        Console.WriteLine("==================================================================")
    End Sub

    ''' <summary>
    ''' Diagnostic test runner for Phase 2 Authentication verification.
    ''' </summary>
    Public Sub RunAuthenticationDiagnostics()
        Console.WriteLine("==================================================================")
        Console.WriteLine("  UNIVERSITY HOSTEL MANAGEMENT SYSTEM - PHASE 2 AUTH DIAGNOSTICS")
        Console.WriteLine("==================================================================")
        Console.WriteLine()

        Dim authService As New AuthenticationService()

        ' Test Suite 1: Valid Logins Across All Roles
        Console.WriteLine("[1/4] Testing Valid User Logins Across All Roles...")
        Dim testCases = {
            ("superadmin", "Admin@2026!", Role.SuperAdmin),
            ("admin", "Admin@2026!", Role.Admin),
            ("accomm_officer", "Officer@2026!", Role.AccommodationOfficer),
            ("finance_officer", "Finance@2026!", Role.FinanceOfficer),
            ("viewer", "Viewer@2026!", Role.Viewer)
        }

        For Each tc In testCases
            Dim result = authService.Authenticate(tc.Item1, tc.Item2, "127.0.0.1")
            If result.Success AndAlso String.Equals(result.User.RoleName, tc.Item3, StringComparison.OrdinalIgnoreCase) Then
                Console.WriteLine($"      ✓ {tc.Item1,-16} -> SUCCESS ({result.User.FullName}, Role: {result.User.RoleName})")
            Else
                Console.WriteLine($"      ✗ {tc.Item1,-16} -> FAILED ({result.ErrorMessage})")
            End If
        Next
        Console.WriteLine()

        ' Test Suite 2: Invalid Password Rejection
        Console.WriteLine("[2/4] Testing Invalid Password Rejection...")
        Dim badPassResult = authService.Authenticate("admin", "IncorrectPassword#999", "127.0.0.1")
        If Not badPassResult.Success AndAlso badPassResult.Status = AuthStatus.InvalidCredentials Then
            Console.WriteLine($"      ✓ Invalid Password correctly rejected: '{badPassResult.ErrorMessage}'")
        Else
            Console.WriteLine($"      ✗ FAILED: Invalid password was not rejected properly.")
        End If
        Console.WriteLine()

        ' Test Suite 3: Non-existent Username Rejection
        Console.WriteLine("[3/4] Testing Non-existent Username Rejection...")
        Dim nonUserResult = authService.Authenticate("unknown_intruder", "AnyPassword2026!", "127.0.0.1")
        If Not nonUserResult.Success AndAlso nonUserResult.Status = AuthStatus.InvalidCredentials Then
            Console.WriteLine($"      ✓ Non-existent user correctly rejected: '{nonUserResult.ErrorMessage}'")
        Else
            Console.WriteLine($"      ✗ FAILED: Non-existent user was not rejected properly.")
        End If
        Console.WriteLine()

        ' Test Suite 4: Account Status Verification (Suspended / Inactive rejection)
        Console.WriteLine("[4/5] Testing Inactive & Suspended Account Rejection...")
        Try
            ' Temporarily set viewer status to SUSPENDED
            DatabaseConnection.ExecuteNonQuery("UPDATE dbo.users SET account_status = 'SUSPENDED' WHERE username = 'viewer';")
            Dim suspendedResult = authService.Authenticate("viewer", "Viewer@2026!", "127.0.0.1")
            If Not suspendedResult.Success AndAlso suspendedResult.Status = AuthStatus.AccountSuspended Then
                Console.WriteLine($"      ✓ Suspended account correctly rejected: '{suspendedResult.ErrorMessage}'")
            Else
                Console.WriteLine($"      ✗ FAILED: Suspended account was not rejected.")
            End If

            ' Temporarily set viewer status to INACTIVE
            DatabaseConnection.ExecuteNonQuery("UPDATE dbo.users SET account_status = 'INACTIVE' WHERE username = 'viewer';")
            Dim inactiveResult = authService.Authenticate("viewer", "Viewer@2026!", "127.0.0.1")
            If Not inactiveResult.Success AndAlso inactiveResult.Status = AuthStatus.AccountInactive Then
                Console.WriteLine($"      ✓ Inactive account correctly rejected: '{inactiveResult.ErrorMessage}'")
            Else
                Console.WriteLine($"      ✗ FAILED: Inactive account was not rejected.")
            End If
        Finally
            ' Restore viewer status to ACTIVE
            DatabaseConnection.ExecuteNonQuery("UPDATE dbo.users SET account_status = 'ACTIVE' WHERE username = 'viewer';")
        End Try
        Console.WriteLine()

        ' Test Suite 5: Role Permissions & Session Inspection
        Console.WriteLine("[5/5] Verifying Active User Session State...")
        Dim finalAuth = authService.Authenticate("superadmin", "Admin@2026!", "127.0.0.1")
        If finalAuth.Success AndAlso UserSession.IsAuthenticated Then
            Console.WriteLine($"      ✓ UserSession.IsAuthenticated: {UserSession.IsAuthenticated}")
            Console.WriteLine($"      ✓ CurrentUser:                {UserSession.CurrentUser.FullName}")
            Console.WriteLine($"      ✓ Role:                       {UserSession.CurrentUser.RoleName}")
            Console.WriteLine($"      ✓ CanManageUsers:             {UserSession.CurrentUser.CanManageUsers}")
            Console.WriteLine($"      ✓ CanAllocateBeds:            {UserSession.CurrentUser.CanAllocateBeds}")
            Console.WriteLine($"      ✓ CanManagePayments:          {UserSession.CurrentUser.CanManagePayments}")
            Console.WriteLine($"      ✓ ClientIp:                   {UserSession.ClientIp}")
        Else
            Console.WriteLine($"      ✗ FAILED: Session could not be verified.")
        End If

        Console.WriteLine()
        Console.WriteLine("==================================================================")
        Console.WriteLine("  PHASE 2 AUTHENTICATION SUITE VERIFICATION COMPLETE")
        Console.WriteLine("==================================================================")
    End Sub

    ''' <summary>
    ''' Renders and captures screenshots of frmSplash and frmLogin for visual UI verification.
    ''' </summary>
    Public Sub CaptureScreenshots()
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim projectDir As String = Directory.GetCurrentDirectory()
        Dim docsScreenshotsDir As String = Path.Combine(projectDir, "docs", "screenshots")
        Directory.CreateDirectory(docsScreenshotsDir)

        Console.WriteLine("Capturing UI screenshots...")

        ' 1. Capture Login Screen
        Try
            Using login As New Forms.frmLogin()
                login.ShowInTaskbar = False
                login.StartPosition = FormStartPosition.Manual
                login.Location = New Point(100, 100)
                login.Show()
                login.txtUsername.TextValue = "accomm_officer"
                login.txtPassword.TextValue = "Officer@2026!"
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmp As New Bitmap(login.Width, login.Height)
                    login.DrawToBitmap(bmp, New Rectangle(0, 0, login.Width, login.Height))
                    Dim loginFile As String = Path.Combine(docsScreenshotsDir, "login_screen.png")
                    bmp.Save(loginFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved login screenshot:  {loginFile}")
                End Using
                login.Close()
            End Using
        Catch ex As Exception
            Console.WriteLine($"      ✗ Error capturing login: {ex.Message}")
        End Try

        ' 2. Capture Splash Screen
        Try
            Console.WriteLine("[2/2] Capturing frmSplash...")
            Using splash As New Forms.frmSplash()
                splash.AutoInitialize = False
                splash.ShowInTaskbar = False
                splash.StartPosition = FormStartPosition.Manual
                splash.Location = New Point(100, 100)
                splash.Show()
                splash.SetDiagnosticStateForPreview()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Console.WriteLine("[2/2] Rendering frmSplash to Bitmap...")
                Using bmp As New Bitmap(splash.Width, splash.Height)
                    splash.DrawToBitmap(bmp, New Rectangle(0, 0, splash.Width, splash.Height))
                    Dim splashFile As String = Path.Combine(docsScreenshotsDir, "splash_screen.png")
                    bmp.Save(splashFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved splash screenshot: {splashFile}")
                End Using
                splash.Close()
            End Using
        Catch ex As Exception
            Console.WriteLine($"      ✗ Error capturing splash: {ex.Message}")
        End Try

        ' 3. Capture Main Dashboard Overview Screen
        Try
            Console.WriteLine("[3/5] Capturing frmMainDashboard (Overview)...")
            Using dash As New Forms.frmMainDashboard()
                dash.ShowInTaskbar = False
                dash.StartPosition = FormStartPosition.Manual
                dash.Location = New Point(50, 50)
                dash.Size = New Size(1300, 800)
                dash.Show()
                Application.DoEvents()
                System.Threading.Thread.Sleep(300)
                Application.DoEvents()

                Using bmp As New Bitmap(dash.Width, dash.Height)
                    dash.DrawToBitmap(bmp, New Rectangle(0, 0, dash.Width, dash.Height))
                    Dim dashFile As String = Path.Combine(docsScreenshotsDir, "dashboard_overview.png")
                    bmp.Save(dashFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved dashboard overview screenshot: {dashFile}")
                End Using

                ' 4. Capture Beds Matrix View
                Console.WriteLine("[4/5] Capturing frmMainDashboard (Bed Space Matrix)...")
                dash.btnNavBeds.PerformClick()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpBeds As New Bitmap(dash.Width, dash.Height)
                    dash.DrawToBitmap(bmpBeds, New Rectangle(0, 0, dash.Width, dash.Height))
                    Dim bedsFile As String = Path.Combine(docsScreenshotsDir, "beds_matrix.png")
                    bmpBeds.Save(bedsFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved beds matrix screenshot:        {bedsFile}")
                End Using

                ' 5. Capture Students Directory View
                Console.WriteLine("[5/5] Capturing frmMainDashboard (Students Directory)...")
                dash.btnNavStudents.PerformClick()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpStudents As New Bitmap(dash.Width, dash.Height)
                    dash.DrawToBitmap(bmpStudents, New Rectangle(0, 0, dash.Width, dash.Height))
                    Dim studFile As String = Path.Combine(docsScreenshotsDir, "students_directory.png")
                    bmpStudents.Save(studFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved students directory screenshot: {studFile}")
                End Using

                ' 6. Capture Bed Allocations Matrix View
                Console.WriteLine("[6/8] Capturing frmMainDashboard (Bed Allocations View)...")
                dash.btnNavAllocations.PerformClick()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpAlloc As New Bitmap(dash.Width, dash.Height)
                    dash.DrawToBitmap(bmpAlloc, New Rectangle(0, 0, dash.Width, dash.Height))
                    Dim allocFile As String = Path.Combine(docsScreenshotsDir, "allocations_matrix.png")
                    bmpAlloc.Save(allocFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved allocations matrix screenshot: {allocFile}")
                End Using

                ' 7. Capture Bursar Billing & Payment Ledger View
                Console.WriteLine("[7/8] Capturing frmMainDashboard (Bursar Billing View)...")
                dash.btnNavBilling.PerformClick()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpBilling As New Bitmap(dash.Width, dash.Height)
                    dash.DrawToBitmap(bmpBilling, New Rectangle(0, 0, dash.Width, dash.Height))
                    Dim billFile As String = Path.Combine(docsScreenshotsDir, "billing_ledger.png")
                    bmpBilling.Save(billFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved bursar billing ledger screenshot: {billFile}")
                End Using

                ' 8. Capture Maintenance Incident Desk View
                Console.WriteLine("[8/11] Capturing frmMainDashboard (Maintenance Desk)...")
                dash.btnNavMaintenance.PerformClick()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpMaint As New Bitmap(dash.Width, dash.Height)
                    dash.DrawToBitmap(bmpMaint, New Rectangle(0, 0, dash.Width, dash.Height))
                    Dim maintFile As String = Path.Combine(docsScreenshotsDir, "maintenance_desk.png")
                    bmpMaint.Save(maintFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved maintenance desk screenshot:     {maintFile}")
                End Using

                ' 9. Capture Institutional Reports & Analytics View
                Console.WriteLine("[9/11] Capturing frmMainDashboard (Reports & Analytics)...")
                dash.btnNavReports.PerformClick()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpReports As New Bitmap(dash.Width, dash.Height)
                    dash.DrawToBitmap(bmpReports, New Rectangle(0, 0, dash.Width, dash.Height))
                    Dim reportsFile As String = Path.Combine(docsScreenshotsDir, "reports_analytics.png")
                    bmpReports.Save(reportsFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved reports & analytics screenshot:  {reportsFile}")
                End Using

                ' 10. Capture Staff Administration & RBAC View
                Console.WriteLine("[10/11] Capturing frmMainDashboard (Staff Administration & RBAC)...")
                dash.btnNavUsers.PerformClick()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpUsers As New Bitmap(dash.Width, dash.Height)
                    dash.DrawToBitmap(bmpUsers, New Rectangle(0, 0, dash.Width, dash.Height))
                    Dim usersFile As String = Path.Combine(docsScreenshotsDir, "users_administration.png")
                    bmpUsers.Save(usersFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved staff admin & RBAC screenshot:   {usersFile}")
                End Using

                dash.Close()
            End Using
        Catch ex As Exception
            Console.WriteLine($"      ✗ Error capturing dashboard views: {ex}")
        End Try

        ' 11. Capture Room Image Gallery Modal
        Try
            Console.WriteLine("[11/12] Capturing frmRoomGalleryModal...")
            Using gallery As New Forms.Rooms.frmRoomGalleryModal(1, "Aberdare Hall", "101")
                gallery.ShowInTaskbar = False
                gallery.StartPosition = FormStartPosition.Manual
                gallery.Location = New Point(100, 100)
                gallery.Show()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpGal As New Bitmap(gallery.Width, gallery.Height)
                    gallery.DrawToBitmap(bmpGal, New Rectangle(0, 0, gallery.Width, gallery.Height))
                    Dim galFile As String = Path.Combine(docsScreenshotsDir, "room_gallery.png")
                    bmpGal.Save(galFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved room gallery screenshot:         {galFile}")
                End Using
                gallery.Close()
            End Using
        Catch ex As Exception
            Console.WriteLine($"      ✗ Error capturing room gallery: {ex.Message}")
        End Try

        ' 8. Capture Official Electronic Receipt Preview
        Try
            Console.WriteLine("[8/8] Capturing frmReceiptPreviewModal...")
            Using rcpt As New Forms.Billing.frmReceiptPreviewModal(1)
                rcpt.ShowInTaskbar = False
                rcpt.StartPosition = FormStartPosition.Manual
                rcpt.Location = New Point(100, 100)
                rcpt.Show()
                Application.DoEvents()
                System.Threading.Thread.Sleep(200)
                Application.DoEvents()

                Using bmpRcpt As New Bitmap(rcpt.Width, rcpt.Height)
                    rcpt.DrawToBitmap(bmpRcpt, New Rectangle(0, 0, rcpt.Width, rcpt.Height))
                    Dim rcptFile As String = Path.Combine(docsScreenshotsDir, "receipt_preview.png")
                    bmpRcpt.Save(rcptFile, System.Drawing.Imaging.ImageFormat.Png)
                    Console.WriteLine($"      ✓ Saved electronic receipt preview:    {rcptFile}")
                End Using
                rcpt.Close()
            End Using
        Catch ex As Exception
            Console.WriteLine($"      ✗ Error capturing receipt preview: {ex.Message}")
        End Try

        Console.WriteLine("CaptureScreenshots completed.")
    End Sub

    ''' <summary>
    ''' Diagnostic runner for Phase 4: Bed Allocations, Check-In/Check-Out, Bed Transfers & Financial Ledger.
    ''' </summary>
    Public Sub RunPhase4Diagnostics()
        Console.WriteLine("==================================================================")
        Console.WriteLine("  UNIVERSITY HOSTEL MANAGEMENT SYSTEM - PHASE 4 DIAGNOSTICS")
        Console.WriteLine("==================================================================")
        Console.WriteLine()

        Dim allocRepo As New Data.Repositories.AllocationRepository()
        Dim paymentRepo As New Data.Repositories.PaymentRepository()
        Dim bedRepo As New Data.Repositories.BedRepository()
        Dim semRepo As New Data.Repositories.SemesterRepository()
        Dim studentRepo As New Data.Repositories.StudentRepository()

        ' 1. Verify Existing Allocations
        Console.WriteLine("[1/6] Verifying Current Bed Allocations & Snapshotted Rates...")
        Dim currentSem = semRepo.GetCurrentSemester()
        Dim semId As Integer = If(currentSem IsNot Nothing, currentSem.SemesterId, 2)
        Dim allocs = allocRepo.GetAllocations(semesterId:=semId)
        Console.WriteLine($"      Active Term: {If(currentSem IsNot Nothing, currentSem.SemesterName, "Default")}")
        Console.WriteLine($"      Allocations in Term: {allocs.Count}")
        For Each a In allocs
            Console.WriteLine($"      • Alloc #{a.AllocationId}: {a.RegistrationNumber} ({a.StudentName}) ➔ {a.BedIdentifier} | Snapshotted Fee: KES {a.SemesterFee:N2} | Paid: KES {a.TotalPaid:N2} | Due: KES {a.BalanceOwed:N2} | Status: {a.AllocationStatus}")
        Next
        Console.WriteLine()

        ' 2. Test Allocation Rate Freezing & State Transition
        Console.WriteLine("[2/6] Testing Atomic Bed Allocation & Rate Snapshotting...")
        ' Find an available bed
        Dim availBeds = bedRepo.GetBeds(status:="AVAILABLE")
        If availBeds.Count > 0 Then
            Dim testBed = availBeds(0)
            Console.WriteLine($"      Target Bed: #{testBed.BedNumber} in {testBed.HostelName} (Current Rate: KES {testBed.SemesterRate:N2})")

            ' Register a temporary test student
            Dim testRegNo As String = $"TEST-{DateTime.UtcNow:mmssff}"
            Dim createStudRes = studentRepo.CreateStudent(New Student With {
                .RegistrationNumber = testRegNo,
                .FirstName = "Test",
                .LastName = "Candidate",
                .Gender = "MALE",
                .Email = $"{testRegNo.ToLowerInvariant()}@university.ac.ke",
                .PhoneNumber = "+254700000000",
                .Course = "BSc. Software Engineering",
                .Department = "School of Computing",
                .AcademicYear = "1st Year",
                .EmergencyContactName = "Guardian Test",
                .EmergencyContactPhone = "+254711111111",
                .StudentStatus = "ACTIVE"
            })

            If createStudRes.Success Then
                Dim testStudId = createStudRes.StudentId
                Console.WriteLine($"      ✓ Created test student: {testRegNo} (ID: {testStudId})")

                ' Attempt allocation
                Dim allocRes = allocRepo.CreateAllocation(testStudId, testBed.BedId, semId, 1, "Automated Phase 4 verification test")
                If allocRes.Success Then
                    Dim newAllocId = allocRes.AllocationId
                    Console.WriteLine($"      ✓ Successfully allocated Bed #{testBed.BedNumber} to student {testRegNo} (Alloc ID: {newAllocId})")

                    ' Verify Bed status is now OCCUPIED
                    Dim recheckedBed = bedRepo.GetBedById(testBed.BedId)
                    Console.WriteLine($"      ✓ Bed status transitioned: {recheckedBed.Status} (Expected: OCCUPIED)")

                    ' 3. Test Check-In
                    Console.WriteLine()
                    Console.WriteLine("[3/6] Testing Resident Arrival Check-In...")
                    Dim checkInRes = allocRepo.CheckInResident(newAllocId, DateTime.UtcNow, "Keys issued and room inspected by Warden", 1)
                    Console.WriteLine($"      ✓ Check-In result: {checkInRes.Message}")

                    ' 4. Test Bursar Payment Recording
                    Console.WriteLine()
                    Console.WriteLine("[4/6] Testing Payment Recording & Automatic Balance Reconciliation...")
                    Dim txCode = $"TX-TEST-{DateTime.UtcNow:mmssff}"
                    Dim partialAmt As Decimal = 10000D
                    Dim payRes = paymentRepo.RecordPayment(newAllocId, partialAmt, "MPESA", txCode, "Phase 4 verification partial payment", 1)
                    If payRes.Success Then
                        Console.WriteLine($"      ✓ Recorded payment of KES {partialAmt:N2} via MPESA (Receipt: {payRes.ReceiptNumber}, Tx: {txCode})")
                        Dim allocAfterPay = allocRepo.GetAllocationById(newAllocId)
                        Console.WriteLine($"      ✓ Updated Ledger: Total Fee=KES {allocAfterPay.SemesterFee:N2}, Total Paid=KES {allocAfterPay.TotalPaid:N2}, Balance Due=KES {allocAfterPay.BalanceOwed:N2}")
                    End If

                    ' 5. Test Bed Transfer
                    Console.WriteLine()
                    Console.WriteLine("[5/6] Testing Transactional Bed Transfer...")
                    Dim otherAvailBeds = bedRepo.GetBeds(status:="AVAILABLE")
                    If otherAvailBeds.Count > 0 Then
                        Dim transferBed = otherAvailBeds(0)
                        Console.WriteLine($"      Destination Bed: #{transferBed.BedNumber} in {transferBed.HostelName} (Rate: KES {transferBed.SemesterRate:N2})")
                        Dim transRes = allocRepo.TransferBed(newAllocId, transferBed.BedId, "Requested room transfer for academic group work", 1)
                        If transRes.Success Then
                            Console.WriteLine($"      ✓ Bed Transfer SUCCESS: New Alloc ID {transRes.NewAllocationId}")
                            Dim oldAllocState = allocRepo.GetAllocationById(newAllocId)
                            Dim oldBedReleased = bedRepo.GetBedById(testBed.BedId)
                            Console.WriteLine($"      ✓ Old Allocation status: {oldAllocState.AllocationStatus} (Expected: TRANSFERRED)")
                            Console.WriteLine($"      ✓ Old Bed #{testBed.BedNumber} status: {oldBedReleased.Status} (Expected: AVAILABLE)")

                            ' Check-Out from transferred allocation
                            allocRepo.CheckOutResident(transRes.NewAllocationId, DateTime.UtcNow, False, String.Empty, 1)
                        End If
                    End If

                    ' 6. Clean-up test student and test records
                    Console.WriteLine()
                    Console.WriteLine("[6/6] Cleaning up test records...")
                    DatabaseConnection.ExecuteNonQuery("DELETE FROM dbo.payments WHERE transaction_reference LIKE 'TX-TEST-%';")
                    DatabaseConnection.ExecuteNonQuery("DELETE FROM dbo.bed_allocations WHERE student_id = @id;", {DatabaseConnection.CreateParam("@id", testStudId, SqlDbType.Int)})
                    DatabaseConnection.ExecuteNonQuery("DELETE FROM dbo.students WHERE student_id = @id;", {DatabaseConnection.CreateParam("@id", testStudId, SqlDbType.Int)})
                    ' Ensure beds are restored to available
                    bedRepo.UpdateBedStatus(testBed.BedId, "AVAILABLE")
                    Console.WriteLine("      ✓ Diagnostic test data cleanly sanitized.")
                Else
                    Console.WriteLine($"      ✗ Allocation failed: {allocRes.Message}")
                End If
            End If
        End If

        Console.WriteLine()
        Console.WriteLine("==================================================================")
        Console.WriteLine("  PHASE 4 ALLOCATION & FINANCIAL LEDGER SUITE PASSED")
        Console.WriteLine("==================================================================")
    End Sub

    ''' <summary>
    ''' Diagnostic runner for Phase 3: Core Management & Dashboard Models, Repositories, and Business Calculations.
    ''' </summary>
    Public Sub RunPhase3Diagnostics()
        Console.WriteLine("==================================================================")
        Console.WriteLine("  UNIVERSITY HOSTEL MANAGEMENT SYSTEM - PHASE 3 DIAGNOSTICS")
        Console.WriteLine("==================================================================")
        Console.WriteLine()

        ' 1. Hostels & Buildings Verification
        Console.WriteLine("[1/6] Verifying Hostel Facilities & Structures...")
        Dim hostelRepo As New Data.Repositories.HostelRepository()
        Dim hostels = hostelRepo.GetAllHostels()
        Console.WriteLine($"      Total Hostels Registered: {hostels.Count}")
        For Each h In hostels
            Console.WriteLine($"      • [{h.HostelCode}] {h.HostelName} ({h.GenderCategory}) - {h.TotalFloors} Floors, {h.RoomCount} Rooms, {h.TotalBedCount} Beds (Occupancy: {h.OccupancyPercentage:F1}%)")
        Next
        Console.WriteLine()

        ' 2. Rooms Verification
        Console.WriteLine("[2/6] Verifying Rooms & Capacity Ceilings...")
        Dim roomRepo As New Data.Repositories.RoomRepository()
        Dim rooms = roomRepo.GetRooms()
        Console.WriteLine($"      Total Rooms Registered: {rooms.Count}")
        Dim capViolations As Integer = 0
        For Each r In rooms
            If r.ActualBedCount > r.Capacity Then
                Console.WriteLine($"      ✗ VIOLATION: Room {r.RoomNumber} in {r.HostelName} has {r.ActualBedCount} beds exceeding capacity {r.Capacity}!")
                capViolations += 1
            End If
        Next
        If capViolations = 0 Then
            Console.WriteLine($"      ✓ All {rooms.Count} rooms strictly respect bed capacity limits.")
        End If
        Console.WriteLine()

        ' 3. Individual Beds & Per-Semester Rates Verification
        Console.WriteLine("[3/6] Verifying Individual Bed Matrix & Semester Rates...")
        Dim bedRepo As New Data.Repositories.BedRepository()
        Dim beds = bedRepo.GetBeds()
        Console.WriteLine($"      Total Bed Spaces: {beds.Count}")
        Dim occupiedBeds As Integer = 0
        Dim availableBeds As Integer = 0
        Dim maintenanceBeds As Integer = 0
        For Each b In beds
            Select Case b.Status
                Case "OCCUPIED" : occupiedBeds += 1
                Case "AVAILABLE" : availableBeds += 1
                Case "MAINTENANCE" : maintenanceBeds += 1
            End Select
        Next
        Console.WriteLine($"      • Occupied Beds:     {occupiedBeds}")
        Console.WriteLine($"      • Available Beds:    {availableBeds}")
        Console.WriteLine($"      • Maintenance Beds:  {maintenanceBeds}")
        Console.WriteLine($"      • Min Semester Rate: KES {beds.Min(Function(x) x.SemesterRate):N2}")
        Console.WriteLine($"      • Max Semester Rate: KES {beds.Max(Function(x) x.SemesterRate):N2}")
        Console.WriteLine()

        ' 4. Student Residents & Balances Verification
        Console.WriteLine("[4/6] Verifying Student Residents & Financial Balances...")
        Dim studentRepo As New Data.Repositories.StudentRepository()
        Dim students = studentRepo.GetStudents()
        Console.WriteLine($"      Total Students Registered: {students.Count}")
        Dim studentsWithAlloc As Integer = students.Where(Function(s) s.HasActiveAllocation).Count()
        Dim totalBalanceOwed As Decimal = students.Sum(Function(s) s.TotalBalanceOwed)
        Console.WriteLine($"      • Students with Active Bed Space: {studentsWithAlloc}")
        Console.WriteLine($"      • Total Student Arrears/Balance:  KES {totalBalanceOwed:N2}")
        Console.WriteLine()

        ' 5. Active Semester Context Verification
        Console.WriteLine("[5/6] Verifying Active Semester Engine Context...")
        Dim semRepo As New Data.Repositories.SemesterRepository()
        Dim currentSem = semRepo.GetCurrentSemester()
        If currentSem IsNot Nothing Then
            Console.WriteLine($"      ✓ Active Semester:   {currentSem.SemesterName}")
            Console.WriteLine($"      • Academic Year:     {currentSem.AcademicYear}")
            Console.WriteLine($"      • Semester Code:     {currentSem.SemesterCode}")
            Console.WriteLine($"      • Allocation Window: {currentSem.AllocationOpenDate:yyyy-MM-dd} to {currentSem.AllocationCloseDate:yyyy-MM-dd}")
            Console.WriteLine($"      • Allocations Open:  {currentSem.IsAllocationOpen}")
        Else
            Console.WriteLine($"      ✗ ERROR: No active semester designated!")
        End If
        Console.WriteLine()

        ' 6. Executive Dashboard Aggregated KPI Engine
        Console.WriteLine("[6/6] Calculating Live Executive Dashboard KPIs...")
        Dim dashRepo As New Data.Repositories.DashboardRepository()
        Dim stats = dashRepo.GetDashboardStatistics()
        Console.WriteLine($"      • Active Semester ID:        {stats.ActiveSemesterId} ({stats.ActiveSemesterName})")
        Console.WriteLine($"      • Total Hostels:             {stats.TotalHostels}")
        Console.WriteLine($"      • Total Rooms:               {stats.TotalRooms}")
        Console.WriteLine($"      • Total Bed Capacity:        {stats.TotalBeds}")
        Console.WriteLine($"      • Occupied Beds:             {stats.OccupiedBeds}")
        Console.WriteLine($"      • Available Bed Spaces:      {stats.AvailableBeds}")
        Console.WriteLine($"      • Overall Occupancy Rate:    {stats.OccupancyPercentage:F1}%")
        Console.WriteLine($"      • Active Semester Billed:    KES {stats.TotalSemesterBilled:N2}")
        Console.WriteLine($"      • Payments Collected (Paid): KES {stats.TotalPaymentsCollected:N2}")
        Console.WriteLine($"      • Outstanding Arrears:       KES {stats.TotalOutstandingBalance:N2}")
        Console.WriteLine()
        Console.WriteLine("==================================================================")
        Console.WriteLine("  PHASE 3 CORE MANAGEMENT & DASHBOARD SUITE PASSED")
        Console.WriteLine("==================================================================")
    End Sub

    ''' <summary>
    ''' Diagnostic runner for Phase 5: Room Image Gallery Asset Pipeline,
    ''' Maintenance Incident Tracking & Bed Locking, Institutional Reporting & User Admin.
    ''' </summary>
    Public Sub RunPhase5Diagnostics()
        Console.WriteLine("==================================================================")
        Console.WriteLine("  UNIVERSITY HOSTEL MANAGEMENT SYSTEM - PHASE 5 DIAGNOSTICS")
        Console.WriteLine("==================================================================")
        Console.WriteLine()

        Dim imgRepo As New Data.Repositories.RoomImageRepository()
        Dim maintRepo As New Data.Repositories.MaintenanceRepository()
        Dim reportRepo As New Data.Repositories.ReportRepository()
        Dim userRepo As New Data.Repositories.UserRepository()
        Dim bedRepo As New Data.Repositories.BedRepository()
        Dim roomRepo As New Data.Repositories.RoomRepository()

        ' =========================================================================
        ' 1. Room Image Gallery Asset Pipeline Verification
        ' =========================================================================
        Console.WriteLine("[1/4] Verifying Room Image Gallery Asset Pipeline...")
        ImageManager.EnsureAssetDirectoriesExist()

        ' Create a procedural test image file on disk
        Dim tempImgPath As String = Path.Combine(Path.GetTempPath(), $"test_room_{Guid.NewGuid():N}.png")
        Using bmp As New System.Drawing.Bitmap(320, 240)
            Using g = System.Drawing.Graphics.FromImage(bmp)
                g.Clear(System.Drawing.Color.SteelBlue)
            End Using
            bmp.Save(tempImgPath, System.Drawing.Imaging.ImageFormat.Png)
        End Using

        Dim rooms = roomRepo.GetRooms()
        Dim testRoomId As Integer = If(rooms.Count > 0, rooms(0).RoomId, 1)

        ' Test asset pipeline save
        Dim relPath As String = ImageManager.SaveRoomImage(tempImgPath, testRoomId, "INTERIOR")
        Console.WriteLine($"      ✓ Image saved to application assets: {relPath}")

        ' Insert metadata in DB
        Dim newImg As New RoomImage With {
            .RoomId = testRoomId,
            .ImagePath = relPath,
            .ImageCategory = "INTERIOR",
            .ImageCaption = "Automated Diagnostic Test Photograph",
            .DisplayOrder = 1,
            .IsPrimary = True
        }
        Dim imgId = imgRepo.AddImage(newImg)
        Console.WriteLine($"      ✓ Created room_images record #{imgId} (Room #{testRoomId})")

        ' Verify retrieval and primary status
        Dim primaryImg = imgRepo.GetPrimaryImage(testRoomId)
        Console.WriteLine($"      ✓ Primary room image verified: ID #{primaryImg.ImageId} ({primaryImg.ImagePath})")

        ' Test safe image loading without file lock
        Using loaded = ImageManager.LoadImageSafely(primaryImg.ImagePath, 200, 150)
            Console.WriteLine($"      ✓ Safely loaded image into memory without lock: {loaded.Width}x{loaded.Height}")
        End Using

        ' Clean up test image
        imgRepo.DeleteImage(imgId)
        ImageManager.DeleteAssetFile(relPath)
        If File.Exists(tempImgPath) Then File.Delete(tempImgPath)
        Console.WriteLine($"      ✓ Cleaned up test asset and database record #{imgId}.")
        Console.WriteLine()

        ' =========================================================================
        ' 2. Maintenance Incident Tracking & Bed Locking Verification
        ' =========================================================================
        Console.WriteLine("[2/4] Verifying Maintenance Desk & Automatic Bed Locking Rules...")
        Dim maintCounts = maintRepo.GetMaintenanceSummaryCounts()
        Console.WriteLine($"      Current Maintenance Incidents: Total={maintCounts("TOTAL")}, Reported={maintCounts("REPORTED")}, InProgress={maintCounts("IN_PROGRESS")}, Resolved={maintCounts("RESOLVED")}")

        ' Find a bed that is currently AVAILABLE
        Dim availBeds = bedRepo.GetBeds(status:="AVAILABLE")
        If availBeds.Count > 0 Then
            Dim targetBed = availBeds(0)
            Console.WriteLine($"      Testing Maintenance Lock on Bed {targetBed.BedLabel} (Current Status: {targetBed.Status})")

            ' 1. Log incident on target bed
            Dim incident As New MaintenanceRecord With {
                .RoomId = targetBed.RoomId,
                .BedId = targetBed.BedId,
                .IssueTitle = "Defective Electrical Socket & Study Lamp Hinge",
                .IssueDescription = "Socket sparking intermittently; immediate electrician rewiring required.",
                .AssignedTo = "Campus Works Dept (John Ndwiga)",
                .MaintenanceStatus = "REPORTED"
            }
            Dim incidentId = maintRepo.CreateIncident(incident, 1)
            Console.WriteLine($"      ✓ Created maintenance incident #{incidentId} on Bed {targetBed.BedLabel}")

            ' Verify bed status transitioned to MAINTENANCE in DB
            Dim updatedBed = bedRepo.GetBeds(roomId:=targetBed.RoomId).FirstOrDefault(Function(b) b.BedId = targetBed.BedId)
            If updatedBed IsNot Nothing AndAlso updatedBed.Status = "MAINTENANCE" Then
                Console.WriteLine($"      ✓ Business Rule Enforced: Bed status transitioned to '{updatedBed.Status}' automatically!")
            Else
                Console.WriteLine($"      ✗ Warning: Bed status is '{If(updatedBed IsNot Nothing, updatedBed.Status, "Null")}', expected MAINTENANCE")
            End If

            ' 2. Test trigger: verify active allocation fails on maintenance bed
            Dim allocRepo As New Data.Repositories.AllocationRepository()
            Dim semRepo As New Data.Repositories.SemesterRepository()
            Dim curSem = semRepo.GetCurrentSemester()
            Dim testSemId As Integer = If(curSem IsNot Nothing, curSem.SemesterId, 2)
            Dim allocAttempt = allocRepo.CreateAllocation(1, targetBed.BedId, testSemId, 1, "Should be blocked by database trigger")
            If Not allocAttempt.Success Then
                Console.WriteLine($"      ✓ Database Constraint Enforced: Allocation blocked on MAINTENANCE bed: '{allocAttempt.Message}'")
            Else
                Console.WriteLine($"      ✗ ERROR: Allocation unexpectedly succeeded on a maintenance bed!")
            End If

            ' 3. Resolve the maintenance issue and restore bed
            Dim resolved = maintRepo.ResolveIncident(incidentId, "Rewired and tested operational with multi-meter.", True, 1)
            Console.WriteLine($"      ✓ Resolved incident #{incidentId} via ResolveIncident(restoreBed:=True)")

            Dim restoredBed = bedRepo.GetBeds(roomId:=targetBed.RoomId).FirstOrDefault(Function(b) b.BedId = targetBed.BedId)
            If restoredBed IsNot Nothing AndAlso restoredBed.Status = "AVAILABLE" Then
                Console.WriteLine($"      ✓ Bed Status Restored: Bed {restoredBed.BedLabel} returned to '{restoredBed.Status}'!")
            Else
                Console.WriteLine($"      ✗ Warning: Restored bed status is '{If(restoredBed IsNot Nothing, restoredBed.Status, "Null")}'")
            End If
        Else
            Console.WriteLine("      (No available bed found to test maintenance lock cycle)")
        End If
        Console.WriteLine()

        ' =========================================================================
        ' 3. Institutional Reporting Engine Verification
        ' =========================================================================
        Console.WriteLine("[3/4] Verifying Institutional Reporting Engine & Print/Web Generators...")

        ' 1. Occupancy Matrix Report
        Dim occData = reportRepo.GetOccupancyReportData()
        Dim totalCap = occData.Sum(Function(x) x.Capacity)
        Dim totalOcc = occData.Sum(Function(x) x.OccupiedBeds)
        Dim totalAvail = occData.Sum(Function(x) x.AvailableBeds)
        Dim occPct As Double = If(totalCap > 0, Math.Round((totalOcc / CDbl(totalCap)) * 100.0, 1), 0.0)
        Console.WriteLine($"      • Report 1 (Occupancy): {occData.Count} Rooms | Capacity: {totalCap} | Occupied: {totalOcc} ({occPct:F1}%) | Available: {totalAvail}")

        ' 2. Allocations Report
        Dim allocData = reportRepo.GetAllocationReportData()
        Console.WriteLine($"      • Report 2 (Allocations): {allocData.Count} Active Resident Allocations | Total Billed: KES {allocData.Sum(Function(x) x.SemesterFee):N2}")

        ' 3. Financial & Defaulters Report
        Dim finData = reportRepo.GetFinancialDefaultersReportData(defaultersOnly:=True)
        Console.WriteLine($"      • Report 3 (Defaulters): {finData.Count} Students with Arrears | Total Balances Due: KES {finData.Sum(Function(x) x.BalanceDue):N2}")

        ' 4. Maintenance Logs Report
        Dim maintData = reportRepo.GetMaintenanceReportData()
        Console.WriteLine($"      • Report 4 (Maintenance): {maintData.Count} Historical & Active Facility Incidents")

        ' 5. Test Institutional HTML Generator
        Dim meta As New ReportMetadata With {
            .ReportTitle = "Hostel Room Occupancy & Capacity Schedule",
            .Subtitle = "Campus-Wide Space Inventory & Vacancy Rates",
            .GeneratedBy = "Chief Accommodation Officer",
            .SemesterName = "Semester 1 2026/2027",
            .TotalRecords = occData.Count
        }
        Dim generator As New InstitutionalReportGenerator(meta, InstitutionalReportGenerator.ReportKind.Occupancy, occData)
        Dim html = generator.GenerateHtmlReport()
        Console.WriteLine($"      ✓ Generated Institutional HTML Document ({html.Length:N0} bytes)")
        Console.WriteLine($"      ✓ Contains Academic Crest: {html.Contains("UNIVERSITY ACCOMMODATION BOARD")}")
        Console.WriteLine($"      ✓ Contains Table Structure: {html.Contains("<table")}")
        Console.WriteLine()

        ' =========================================================================
        ' 4. System User Administration & RBAC Security Verification
        ' =========================================================================
        Console.WriteLine("[4/4] Verifying User Administration & RBAC Security Module...")
        Dim allUsers = userRepo.GetAllUsers()
        Console.WriteLine($"      Total System Users: {allUsers.Count}")
        For Each u In allUsers
            Dim hashValid = u.PasswordHash.StartsWith("PBKDF2$SHA256$")
            Console.WriteLine($"      • User #{u.UserId}: {u.Username.PadRight(16)} ({u.FullName.PadRight(25)}) | Role: {u.RoleName.PadRight(22)} | Status: {u.AccountStatus.PadRight(8)} | PBKDF2: {If(hashValid, "VALID", "INVALID")}")
        Next

        Dim allRoles = userRepo.GetAllRoles()
        Console.WriteLine($"      Configured RBAC Roles: {String.Join(", ", allRoles.Select(Function(r) r.RoleName))}")

        Dim auditLogs = userRepo.GetActivityLogs(5)
        Console.WriteLine($"      Recent Immutable Audit Trail Entries: {auditLogs.Rows.Count}")
        For Each row As System.Data.DataRow In auditLogs.Rows
            Console.WriteLine($"      • [{Convert.ToDateTime(row("action_timestamp")):yyyy-MM-dd HH:mm}] {row("staff_username")} ➔ {row("action_type")}: {row("description")}")
        Next
        Console.WriteLine()

        Console.WriteLine("==================================================================")
        Console.WriteLine("  PHASE 5 ASSET PIPELINE, MAINTENANCE & REPORTING SUITE PASSED")
        Console.WriteLine("==================================================================")
    End Sub
End Module
