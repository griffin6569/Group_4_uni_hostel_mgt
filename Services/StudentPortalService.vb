Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Threading.Tasks
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Data
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Services
    ''' <summary>
    ''' Student portal view model representing a student's active room assignment,
    ''' amenities, and fellow roommates.
    ''' </summary>
    Public Class StudentAllocationDetail
        Public Property AllocationId As Integer
        Public Property BedId As Integer
        Public Property RoomId As Integer
        Public Property HostelId As Integer
        Public Property HostelName As String = String.Empty
        Public Property RoomNumber As String = String.Empty
        Public Property FloorNumber As Integer
        Public Property RoomType As String = String.Empty
        Public Property BedNumber As String = String.Empty
        Public Property SemesterFee As Decimal
        Public Property TotalPaid As Decimal
        Public Property BalanceOwed As Decimal
        Public Property AllocationStatus As String = String.Empty
        Public Property AllocationDate As DateTime
        Public Property CheckInDate As DateTime?
        Public Property HasDesk As Boolean
        Public Property HasLocker As Boolean
        Public Property HasWindow As Boolean
        Public Property HasVentilation As Boolean
        Public Property RoomImages As New List(Of String)()
        Public Property Roommates As New List(Of RoommateInfo)()
    End Class

    Public Class RoommateInfo
        Public Property FullName As String = String.Empty
        Public Property Course As String = String.Empty
        Public Property BedNumber As String = String.Empty
    End Class

    Public Class AvailableRoomCard
        Public Property RoomId As Integer
        Public Property HostelId As Integer
        Public Property HostelName As String = String.Empty
        Public Property RoomNumber As String = String.Empty
        Public Property FloorNumber As Integer
        Public Property RoomType As String = String.Empty
        Public Property Capacity As Integer
        Public Property AvailableBedsCount As Integer
        Public Property SemesterRate As Decimal
        Public Property PrimaryImagePath As String = String.Empty
        Public Property HasDesk As Boolean
        Public Property HasLocker As Boolean
        Public Property HasWindow As Boolean
        Public Property HasVentilation As Boolean
        Public Property AvailableBeds As New List(Of Bed)()
    End Class

    ''' <summary>
    ''' Core business logic service powering the Dedan Kimathi University of Technology
    ''' Student Self-Service Accommodation Portal.
    ''' </summary>
    Public Class StudentPortalService
        Private ReadOnly _userRepo As New UserRepository()
        Private ReadOnly _studentRepo As New StudentRepository()

        ''' <summary>
        ''' Registers a new student account using their official DeKUT email (@students.dkut.ac.ke).
        ''' </summary>
        Public Async Function RegisterStudentAccountAsync(regNo As String,
                                                          email As String,
                                                          firstName As String,
                                                          lastName As String,
                                                          gender As String,
                                                          course As String,
                                                          department As String,
                                                          phone As String,
                                                          password As String,
                                                          Optional middleName As String = "",
                                                          Optional academicYear As String = "2025/2026") As Task(Of (Success As Boolean, Message As String, User As User))
            Return Await Task.Run(Function()
                ' 1. Validate DeKUT Student Email
                Dim cleanEmail = email.Trim().ToLowerInvariant()
                If Not ValidationHelper.IsValidDeKutStudentEmail(cleanEmail) Then
                    Return (False, "Registration requires a valid Dedan Kimathi University student email (@students.dkut.ac.ke).", Nothing)
                End If

                ' 2. Validate Reg No
                Dim cleanRegNo = regNo.Trim().ToUpperInvariant()
                If String.IsNullOrWhiteSpace(cleanRegNo) Then
                    Return (False, "Please enter your university registration number.", Nothing)
                End If

                ' 3. Validate Names
                If String.IsNullOrWhiteSpace(firstName) OrElse String.IsNullOrWhiteSpace(lastName) Then
                    Return (False, "First name and last name are required.", Nothing)
                End If

                ' 4. Validate Password
                Dim passError As String = String.Empty
                If Not ValidationHelper.ValidatePasswordStrength(password, passError) Then
                    Return (False, passError, Nothing)
                End If

                ' 5. Check if user already exists
                Dim existingUser = _userRepo.GetUserByUsername(cleanEmail)
                If existingUser IsNot Nothing Then
                    Return (False, "An account is already registered with this student email. Please sign in.", Nothing)
                End If

                ' 6. Resolve or create student roster record
                Dim student = _studentRepo.GetStudentByRegistrationNumber(cleanRegNo)
                If student Is Nothing Then
                    student = _studentRepo.GetStudentByEmail(cleanEmail)
                End If

                Dim studentId As Integer = 0
                If student IsNot Nothing Then
                    studentId = student.StudentId
                Else
                    Dim newStudent As New Student() With {
                        .RegistrationNumber = cleanRegNo,
                        .FirstName = firstName.Trim(),
                        .MiddleName = middleName.Trim(),
                        .LastName = lastName.Trim(),
                        .Gender = If(gender.ToUpperInvariant().StartsWith("F"), "FEMALE", "MALE"),
                        .Email = cleanEmail,
                        .PhoneNumber = If(String.IsNullOrWhiteSpace(phone), "0700000000", phone.Trim()),
                        .Course = If(String.IsNullOrWhiteSpace(course), "Computer Science", course.Trim()),
                        .Department = If(String.IsNullOrWhiteSpace(department), "Computer Science", department.Trim()),
                        .AcademicYear = academicYear,
                        .EmergencyContactName = "Parent/Guardian",
                        .EmergencyContactPhone = "0700000000",
                        .StudentStatus = "ACTIVE"
                    }
                    Dim createResult = _studentRepo.CreateStudent(newStudent)
                    If Not createResult.Success Then
                        Return (False, createResult.Message, Nothing)
                    End If
                    studentId = createResult.StudentId
                End If

                ' 7. Create User Account with hashed password
                Dim passwordHash = PasswordHasher.HashPassword(password)
                Dim fullName = $"{firstName.Trim()} {lastName.Trim()}"
                Dim newUser = _userRepo.CreateStudentUser(fullName, cleanRegNo, cleanEmail, passwordHash, studentId)

                ' 8. Audit log
                _userRepo.LogActivity(newUser.UserId, "STUDENT_REGISTER", "USER", cleanRegNo,
                                      $"Student {fullName} self-registered with email {cleanEmail}.")

                Return (True, "Account created successfully! You can now sign in to the student portal.", newUser)
            End Function).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Gets the active allocation and room residency status for the given student.
        ''' </summary>
        Public Async Function GetStudentCurrentAllocationAsync(studentId As Integer) As Task(Of StudentAllocationDetail)
            Return Await Task.Run(Function()
                Const sql As String = "
                    SELECT 
                        a.allocation_id,
                        a.bed_id,
                        b.room_id,
                        r.hostel_id,
                        h.hostel_name,
                        r.room_number,
                        r.floor_number,
                        r.room_type,
                        b.bed_number,
                        a.semester_fee,
                        ISNULL(p.total_paid, 0.00) AS total_paid,
                        (a.semester_fee - ISNULL(p.total_paid, 0.00)) AS balance_owed,
                        a.allocation_status,
                        a.allocation_date,
                        a.check_in_date,
                        b.has_desk,
                        b.has_locker,
                        b.has_window,
                        b.has_ventilation
                    FROM dbo.bed_allocations a
                    INNER JOIN dbo.beds b ON a.bed_id = b.bed_id
                    INNER JOIN dbo.rooms r ON b.room_id = r.room_id
                    INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id
                    LEFT JOIN (
                        SELECT allocation_id, SUM(amount_paid) AS total_paid
                        FROM dbo.payments
                        WHERE payment_status = 'CONFIRMED'
                        GROUP BY allocation_id
                    ) p ON a.allocation_id = p.allocation_id
                    WHERE a.student_id = @StudentId AND a.allocation_status IN ('ACTIVE', 'CHECKED_IN');"

                Dim dt = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@StudentId", studentId, SqlDbType.Int)})
                If dt.Rows.Count = 0 Then Return Nothing

                Dim row = dt.Rows(0)
                Dim detail As New StudentAllocationDetail() With {
                    .AllocationId = Convert.ToInt32(row("allocation_id")),
                    .BedId = Convert.ToInt32(row("bed_id")),
                    .RoomId = Convert.ToInt32(row("room_id")),
                    .HostelId = Convert.ToInt32(row("hostel_id")),
                    .HostelName = row("hostel_name").ToString(),
                    .RoomNumber = row("room_number").ToString(),
                    .FloorNumber = Convert.ToInt32(row("floor_number")),
                    .RoomType = row("room_type").ToString(),
                    .BedNumber = row("bed_number").ToString(),
                    .SemesterFee = Convert.ToDecimal(row("semester_fee")),
                    .TotalPaid = Convert.ToDecimal(row("total_paid")),
                    .BalanceOwed = Convert.ToDecimal(row("balance_owed")),
                    .AllocationStatus = row("allocation_status").ToString(),
                    .AllocationDate = Convert.ToDateTime(row("allocation_date")),
                    .CheckInDate = If(IsDBNull(row("check_in_date")), Nothing, CType(Convert.ToDateTime(row("check_in_date")), DateTime?)),
                    .HasDesk = Convert.ToBoolean(row("has_desk")),
                    .HasLocker = Convert.ToBoolean(row("has_locker")),
                    .HasWindow = Convert.ToBoolean(row("has_window")),
                    .HasVentilation = Convert.ToBoolean(row("has_ventilation"))
                }

                ' Fetch fellow roommates in the room
                Const roommateSql As String = "
                    SELECT s.first_name, s.last_name, s.course, b2.bed_number
                    FROM dbo.bed_allocations a2
                    INNER JOIN dbo.students s ON a2.student_id = s.student_id
                    INNER JOIN dbo.beds b2 ON a2.bed_id = b2.bed_id
                    WHERE b2.room_id = @RoomId 
                      AND a2.allocation_status IN ('ACTIVE', 'CHECKED_IN')
                      AND a2.student_id <> @StudentId;"

                Dim dtRoommates = DatabaseConnection.ExecuteDataTable(roommateSql, {
                    DatabaseConnection.CreateParam("@RoomId", detail.RoomId, SqlDbType.Int),
                    DatabaseConnection.CreateParam("@StudentId", studentId, SqlDbType.Int)
                })

                For Each rRow As DataRow In dtRoommates.Rows
                    detail.Roommates.Add(New RoommateInfo() With {
                        .FullName = $"{rRow("first_name")} {rRow("last_name")}".Trim(),
                        .Course = rRow("course").ToString(),
                        .BedNumber = rRow("bed_number").ToString()
                    })
                Next

                ' Fetch room images
                Const imgSql As String = "SELECT image_path FROM dbo.room_images WHERE room_id = @RoomId ORDER BY is_primary DESC, display_order ASC;"
                Dim dtImgs = DatabaseConnection.ExecuteDataTable(imgSql, {DatabaseConnection.CreateParam("@RoomId", detail.RoomId, SqlDbType.Int)})
                For Each iRow As DataRow In dtImgs.Rows
                    detail.RoomImages.Add(iRow("image_path").ToString())
                Next

                Return detail
            End Function).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Retrieves available rooms and bed spaces filtered by student's gender and active semester.
        ''' </summary>
        Public Async Function GetAvailableRoomsForStudentAsync(studentGender As String, Optional hostelFilter As Integer? = Nothing) As Task(Of List(Of AvailableRoomCard))
            Return Await Task.Run(Function()
                Dim cards As New List(Of AvailableRoomCard)()
                Dim cleanGender = If(studentGender.ToUpperInvariant().StartsWith("F"), "FEMALE", "MALE")

                Dim sql As String = "
                    SELECT 
                        r.room_id,
                        r.hostel_id,
                        h.hostel_name,
                        r.room_number,
                        r.floor_number,
                        r.room_type,
                        r.capacity,
                        COUNT(b.bed_id) AS available_beds_count,
                        MIN(b.semester_rate) AS semester_rate,
                        MAX(CASE WHEN b.has_desk = 1 THEN 1 ELSE 0 END) AS has_desk,
                        MAX(CASE WHEN b.has_locker = 1 THEN 1 ELSE 0 END) AS has_locker,
                        MAX(CASE WHEN b.has_window = 1 THEN 1 ELSE 0 END) AS has_window,
                        MAX(CASE WHEN b.has_ventilation = 1 THEN 1 ELSE 0 END) AS has_ventilation,
                        (SELECT TOP 1 image_path FROM dbo.room_images WHERE room_id = r.room_id ORDER BY is_primary DESC) AS primary_image
                    FROM dbo.rooms r
                    INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id
                    INNER JOIN dbo.beds b ON r.room_id = b.room_id
                    WHERE b.status = 'AVAILABLE'
                      AND r.status = 'AVAILABLE'
                      AND h.status = 'ACTIVE'
                      AND (h.gender_category = @Gender OR h.gender_category = 'COED' OR h.gender_category = 'MIXED') "

                Dim params As New List(Of SqlParameter) From {
                    DatabaseConnection.CreateParam("@Gender", cleanGender, SqlDbType.NVarChar)
                }

                If hostelFilter.HasValue AndAlso hostelFilter.Value > 0 Then
                    sql &= " AND r.hostel_id = @HostelId "
                    params.Add(DatabaseConnection.CreateParam("@HostelId", hostelFilter.Value, SqlDbType.Int))
                End If

                sql &= " GROUP BY r.room_id, r.hostel_id, h.hostel_name, r.room_number, r.floor_number, r.room_type, r.capacity HAVING COUNT(b.bed_id) > 0 ORDER BY h.hostel_name, r.room_number;"

                Dim dt = DatabaseConnection.ExecuteDataTable(sql, params.ToArray())
                For Each row As DataRow In dt.Rows
                    Dim card As New AvailableRoomCard() With {
                        .RoomId = Convert.ToInt32(row("room_id")),
                        .HostelId = Convert.ToInt32(row("hostel_id")),
                        .HostelName = row("hostel_name").ToString(),
                        .RoomNumber = row("room_number").ToString(),
                        .FloorNumber = Convert.ToInt32(row("floor_number")),
                        .RoomType = row("room_type").ToString(),
                        .Capacity = Convert.ToInt32(row("capacity")),
                        .AvailableBedsCount = Convert.ToInt32(row("available_beds_count")),
                        .SemesterRate = Convert.ToDecimal(row("semester_rate")),
                        .PrimaryImagePath = If(IsDBNull(row("primary_image")), String.Empty, row("primary_image").ToString()),
                        .HasDesk = Convert.ToInt32(row("has_desk")) = 1,
                        .HasLocker = Convert.ToInt32(row("has_locker")) = 1,
                        .HasWindow = Convert.ToInt32(row("has_window")) = 1,
                        .HasVentilation = Convert.ToInt32(row("has_ventilation")) = 1
                    }

                    ' Load available beds for this room
                    Const bedsSql As String = "SELECT bed_id, room_id, bed_number, bed_label, bed_type, semester_rate, bed_position, has_desk, has_locker, has_window, has_ventilation, status FROM dbo.beds WHERE room_id = @RoomId AND status = 'AVAILABLE' ORDER BY bed_number ASC;"
                    Dim dtBeds = DatabaseConnection.ExecuteDataTable(bedsSql, {DatabaseConnection.CreateParam("@RoomId", card.RoomId, SqlDbType.Int)})
                    For Each bRow As DataRow In dtBeds.Rows
                        card.AvailableBeds.Add(New Bed() With {
                            .BedId = Convert.ToInt32(bRow("bed_id")),
                            .RoomId = Convert.ToInt32(bRow("room_id")),
                            .BedNumber = bRow("bed_number").ToString(),
                            .BedLabel = bRow("bed_label").ToString(),
                            .BedType = bRow("bed_type").ToString(),
                            .SemesterRate = Convert.ToDecimal(bRow("semester_rate")),
                            .Status = bRow("status").ToString()
                        })
                    Next

                    cards.Add(card)
                Next

                Return cards
            End Function).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Books an available bed space for the student in the active academic semester.
        ''' </summary>
        Public Async Function BookBedAsync(studentId As Integer, bedId As Integer, userId As Integer) As Task(Of (Success As Boolean, Message As String, AllocationId As Integer))
            Return Await Task.Run(Function()
                ' 1. Resolve current active semester
                Const semSql As String = "SELECT TOP 1 semester_id, semester_name FROM dbo.semesters WHERE status IN ('CURRENT', 'ACTIVE') ORDER BY start_date DESC;"
                Dim dtSem = DatabaseConnection.ExecuteDataTable(semSql)
                If dtSem.Rows.Count = 0 Then
                    Return (False, "There is no currently open semester for accommodation booking.", 0)
                End If
                Dim semesterId = Convert.ToInt32(dtSem.Rows(0)("semester_id"))
                Dim semesterName = dtSem.Rows(0)("semester_name").ToString()

                ' 2. Verify student doesn't already have an active allocation
                Const existingSql As String = "SELECT COUNT(*) FROM dbo.bed_allocations WHERE student_id = @StudentId AND semester_id = @SemesterId AND allocation_status IN ('ACTIVE', 'CHECKED_IN');"
                Dim count = Convert.ToInt32(DatabaseConnection.ExecuteScalar(existingSql, {
                    DatabaseConnection.CreateParam("@StudentId", studentId, SqlDbType.Int),
                    DatabaseConnection.CreateParam("@SemesterId", semesterId, SqlDbType.Int)
                }))
                If count > 0 Then
                    Return (False, $"You already have an active room allocation for {semesterName}.", 0)
                End If

                ' 3. Verify bed availability and fee
                Const bedSql As String = "SELECT semester_rate, status FROM dbo.beds WHERE bed_id = @BedId;"
                Dim dtBed = DatabaseConnection.ExecuteDataTable(bedSql, {DatabaseConnection.CreateParam("@BedId", bedId, SqlDbType.Int)})
                If dtBed.Rows.Count = 0 Then
                    Return (False, "The selected bed was not found.", 0)
                End If
                If dtBed.Rows(0)("status").ToString() <> "AVAILABLE" Then
                    Return (False, "This bed space was just occupied by another student. Please select another.", 0)
                End If
                Dim semesterFee = Convert.ToDecimal(dtBed.Rows(0)("semester_rate"))

                ' 4. Atomic Booking
                Using conn = DatabaseConnection.GetOpenConnection()
                    Using tx = conn.BeginTransaction()
                        Try
                            Const insertAllocSql As String = "
                                INSERT INTO dbo.bed_allocations (student_id, bed_id, semester_id, semester_fee, allocation_date, allocation_status, created_by, created_at, updated_at)
                                VALUES (@StudentId, @BedId, @SemesterId, @Fee, SYSUTCDATETIME(), 'ACTIVE', @CreatedBy, SYSUTCDATETIME(), SYSUTCDATETIME());
                                SELECT CAST(SCOPE_IDENTITY() AS INT);"

                            Dim allocParams As SqlParameter() = {
                                DatabaseConnection.CreateParam("@StudentId", studentId, SqlDbType.Int),
                                DatabaseConnection.CreateParam("@BedId", bedId, SqlDbType.Int),
                                DatabaseConnection.CreateParam("@SemesterId", semesterId, SqlDbType.Int),
                                DatabaseConnection.CreateParam("@Fee", semesterFee, SqlDbType.Decimal),
                                DatabaseConnection.CreateParam("@CreatedBy", userId, SqlDbType.Int)
                            }

                            Dim newAllocId = 0
                            Using cmd As New SqlCommand(insertAllocSql, conn, tx)
                                cmd.Parameters.AddRange(allocParams)
                                newAllocId = Convert.ToInt32(cmd.ExecuteScalar())
                            End Using

                            ' Update bed status
                            Const updateBedSql As String = "UPDATE dbo.beds SET status = 'OCCUPIED', updated_at = SYSUTCDATETIME() WHERE bed_id = @BedId;"
                            Using cmd As New SqlCommand(updateBedSql, conn, tx)
                                cmd.Parameters.Add(DatabaseConnection.CreateParam("@BedId", bedId, SqlDbType.Int))
                                cmd.ExecuteNonQuery()
                            End Using

                            tx.Commit()

                            _userRepo.LogActivity(userId, "STUDENT_BOOK_BED", "BED_ALLOCATION", newAllocId.ToString(),
                                                  $"Student ID {studentId} booked Bed ID {bedId} for {semesterName} (Fee: KES {semesterFee:N2}).")

                            Return (True, $"Congratulations! You have booked your bed space for {semesterName}.", newAllocId)
                        Catch ex As Exception
                            tx.Rollback()
                            Return (False, $"Booking failed: {ex.Message}", 0)
                        End Try
                    End Using
                End Using
            End Function).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Processes a student accommodation fee payment (M-Pesa or Bank).
        ''' </summary>
        Public Async Function ProcessPaymentAsync(allocationId As Integer,
                                                  studentId As Integer,
                                                  amount As Decimal,
                                                  paymentMethod As String,
                                                  txRef As String,
                                                  userId As Integer) As Task(Of (Success As Boolean, Message As String, ReceiptNumber As String, PaymentId As Integer))
            Return Await Task.Run(Function()
                If amount <= 0 Then
                    Return (False, "Payment amount must be greater than zero.", String.Empty, 0)
                End If

                Dim receiptNo = $"REC-{DateTime.Now:yyyyMMdd}-{New Random().Next(1000, 9999)}"
                Dim cleanTxRef = If(String.IsNullOrWhiteSpace(txRef), $"MPESA-{DateTime.Now:HHmmss}-{New Random().Next(100, 999)}", txRef.Trim().ToUpperInvariant())
                Dim cleanMethod = paymentMethod.Trim().Replace("-", "").ToUpperInvariant()
                If cleanMethod <> "MPESA" AndAlso cleanMethod <> "BANK" AndAlso cleanMethod <> "CASH" Then
                    cleanMethod = "OTHER"
                End If

                Const insertSql As String = "
                    INSERT INTO dbo.payments (allocation_id, receipt_number, amount_paid, payment_method, transaction_reference, payment_date, recorded_by, payment_status, created_at, updated_at)
                    VALUES (@AllocId, @ReceiptNo, @Amount, @Method, @TxRef, SYSUTCDATETIME(), @UserId, 'CONFIRMED', SYSUTCDATETIME(), SYSUTCDATETIME());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);"

                Dim params As SqlParameter() = {
                    DatabaseConnection.CreateParam("@AllocId", allocationId, SqlDbType.Int),
                    DatabaseConnection.CreateParam("@ReceiptNo", receiptNo, SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@Amount", amount, SqlDbType.Decimal),
                    DatabaseConnection.CreateParam("@Method", cleanMethod, SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@TxRef", cleanTxRef, SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@UserId", userId, SqlDbType.Int)
                }

                Dim newPayId = Convert.ToInt32(DatabaseConnection.ExecuteScalar(insertSql, params))

                _userRepo.LogActivity(userId, "STUDENT_PAYMENT", "PAYMENT", newPayId.ToString(),
                                      $"Payment of KES {amount:N2} processed via {paymentMethod} (Receipt: {receiptNo}, Ref: {cleanTxRef}).")

                Return (True, $"Payment of KES {amount:N2} confirmed successfully!", receiptNo, newPayId)
            End Function).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Gets all payment history entries for the student.
        ''' </summary>
        Public Async Function GetStudentPaymentHistoryAsync(studentId As Integer) As Task(Of DataTable)
            Return Await Task.Run(Function()
                Const sql As String = "
                    SELECT 
                        p.payment_id,
                        p.receipt_number,
                        p.amount_paid,
                        p.payment_method,
                        p.transaction_reference,
                        p.payment_date,
                        p.payment_status,
                        CONCAT(h.hostel_name, ' - Rm ', r.room_number, ' (Bed ', b.bed_number, ')') AS unit_name
                    FROM dbo.payments p
                    INNER JOIN dbo.bed_allocations a ON p.allocation_id = a.allocation_id
                    INNER JOIN dbo.beds b ON a.bed_id = b.bed_id
                    INNER JOIN dbo.rooms r ON b.room_id = r.room_id
                    INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id
                    WHERE a.student_id = @StudentId
                    ORDER BY p.payment_date DESC;"

                Return DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@StudentId", studentId, SqlDbType.Int)})
            End Function).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Logs a room maintenance or defect ticket submitted by the student.
        ''' </summary>
        Public Async Function ReportMaintenanceIssueAsync(studentId As Integer,
                                                          roomId As Integer,
                                                          bedId As Integer?,
                                                          issueTitle As String,
                                                          issueDescription As String,
                                                          evidenceImagePath As String,
                                                          userId As Integer) As Task(Of (Success As Boolean, Message As String, MaintenanceId As Integer))
            Return Await Task.Run(Function()
                If String.IsNullOrWhiteSpace(issueTitle) Then
                    Return (False, "Please provide a title describing the maintenance issue.", 0)
                End If

                Const insertSql As String = "
                    INSERT INTO dbo.maintenance_records (bed_id, room_id, issue_title, issue_description, maintenance_status, reported_by, reported_date, evidence_image_path, created_at, updated_at)
                    VALUES (@BedId, @RoomId, @Title, @Desc, 'REPORTED', @UserId, SYSUTCDATETIME(), @Evidence, SYSUTCDATETIME(), SYSUTCDATETIME());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);"

                Dim params As SqlParameter() = {
                    DatabaseConnection.CreateParam("@BedId", If(bedId.HasValue, CObj(bedId.Value), DBNull.Value), SqlDbType.Int),
                    DatabaseConnection.CreateParam("@RoomId", roomId, SqlDbType.Int),
                    DatabaseConnection.CreateParam("@Title", issueTitle.Trim(), SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@Desc", issueDescription.Trim(), SqlDbType.NVarChar),
                    DatabaseConnection.CreateParam("@UserId", userId, SqlDbType.Int),
                    DatabaseConnection.CreateParam("@Evidence", If(String.IsNullOrEmpty(evidenceImagePath), DBNull.Value, CObj(evidenceImagePath)), SqlDbType.NVarChar)
                }

                Dim newMaintId = Convert.ToInt32(DatabaseConnection.ExecuteScalar(insertSql, params))

                _userRepo.LogActivity(userId, "STUDENT_MAINTENANCE_REPORT", "MAINTENANCE", newMaintId.ToString(),
                                      $"Student lodged ticket #{newMaintId}: '{issueTitle}' for Room ID {roomId}.")

                Return (True, "Maintenance ticket lodged successfully. University technicians have been notified.", newMaintId)
            End Function).ConfigureAwait(False)
        End Function

        ''' <summary>
        ''' Gets maintenance tickets filed by or relevant to the student.
        ''' </summary>
        Public Async Function GetStudentTicketsAsync(studentId As Integer, roomId As Integer) As Task(Of DataTable)
            Return Await Task.Run(Function()
                Const sql As String = "
                    SELECT 
                        m.maintenance_id,
                        m.issue_title,
                        m.issue_description,
                        m.maintenance_status,
                        m.reported_date,
                        m.resolved_date,
                        m.resolution_notes,
                        m.evidence_image_path,
                        CONCAT(h.hostel_name, ' - Rm ', r.room_number) AS room_name
                    FROM dbo.maintenance_records m
                    INNER JOIN dbo.rooms r ON m.room_id = r.room_id
                    INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id
                    WHERE m.room_id = @RoomId
                    ORDER BY m.reported_date DESC;"

                Return DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@RoomId", roomId, SqlDbType.Int)})
            End Function).ConfigureAwait(False)
        End Function
    End Class
End Namespace

