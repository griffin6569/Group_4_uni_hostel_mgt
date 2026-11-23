Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET repository for managing registered university students.
    ''' </summary>
    Public Class StudentRepository
        Public Function GetStudents(Optional status As String = Nothing,
                                    Optional department As String = Nothing,
                                    Optional search As String = Nothing) As List(Of Student)
            Dim list As New List(Of Student)()
            Dim sql As String =
                "SELECT s.student_id, s.registration_number, s.first_name, s.middle_name, s.last_name, " &
                "       s.gender, s.email, s.phone_number, s.course, s.department, s.academic_year, " &
                "       s.emergency_contact_name, s.emergency_contact_phone, s.student_status, " &
                "       s.created_at, s.updated_at, " &
                "       CASE WHEN alloc.allocation_id IS NOT NULL THEN 1 ELSE 0 END AS has_active_allocation, " &
                "       alloc.bed_identifier, " &
                "       ISNULL(bal.total_balance, 0.00) AS total_balance " &
                "FROM dbo.students s " &
                "LEFT JOIN ( " &
                "    SELECT a.student_id, a.allocation_id, " &
                "           CONCAT(h.hostel_code, ' - Rm ', r.room_number, ' - Bed ', b.bed_number) AS bed_identifier " &
                "    FROM dbo.bed_allocations a " &
                "    INNER JOIN dbo.beds b ON a.bed_id = b.bed_id " &
                "    INNER JOIN dbo.rooms r ON b.room_id = r.room_id " &
                "    INNER JOIN dbo.hostels h ON r.hostel_id = h.hostel_id " &
                "    WHERE a.allocation_status = 'ACTIVE' " &
                ") alloc ON s.student_id = alloc.student_id " &
                "LEFT JOIN ( " &
                "    SELECT a.student_id, " &
                "           SUM(a.semester_fee) - ISNULL(SUM(p.total_paid), 0.00) AS total_balance " &
                "    FROM dbo.bed_allocations a " &
                "    LEFT JOIN ( " &
                "        SELECT allocation_id, SUM(amount_paid) AS total_paid " &
                "        FROM dbo.payments " &
                "        WHERE payment_status = 'CONFIRMED' " &
                "        GROUP BY allocation_id " &
                "    ) p ON a.allocation_id = p.allocation_id " &
                "    WHERE a.allocation_status IN ('ACTIVE', 'CHECKED_OUT') " &
                "    GROUP BY a.student_id " &
                ") bal ON s.student_id = bal.student_id " &
                "WHERE 1=1 "

            Dim parameters As New List(Of SqlParameter)()

            If Not String.IsNullOrEmpty(status) AndAlso status <> "ALL" Then
                sql &= " AND s.student_status = @status "
                parameters.Add(DatabaseConnection.CreateParam("@status", status, SqlDbType.NVarChar))
            End If

            If Not String.IsNullOrEmpty(department) AndAlso department <> "ALL" Then
                sql &= " AND s.department = @dept "
                parameters.Add(DatabaseConnection.CreateParam("@dept", department, SqlDbType.NVarChar))
            End If

            If Not String.IsNullOrEmpty(search) Then
                sql &= " AND (s.registration_number LIKE @search OR s.first_name LIKE @search OR s.last_name LIKE @search OR s.email LIKE @search OR s.course LIKE @search) "
                parameters.Add(DatabaseConnection.CreateParam("@search", "%" & search.Trim() & "%", SqlDbType.NVarChar))
            End If

            sql &= " ORDER BY s.registration_number ASC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, parameters.ToArray())
            For Each row As DataRow In dt.Rows
                list.Add(MapStudentFromRow(row))
            Next

            Return list
        End Function

        Public Function GetStudentById(studentId As Integer) As Student
            Dim sql As String =
                "SELECT s.student_id, s.registration_number, s.first_name, s.middle_name, s.last_name, " &
                "       s.gender, s.email, s.phone_number, s.course, s.department, s.academic_year, " &
                "       s.emergency_contact_name, s.emergency_contact_phone, s.student_status, " &
                "       s.created_at, s.updated_at, " &
                "       0 AS has_active_allocation, '' AS bed_identifier, 0.00 AS total_balance " &
                "FROM dbo.students s WHERE s.student_id = @id;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@id", studentId, SqlDbType.Int)})
            If dt.Rows.Count > 0 Then
                Return MapStudentFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        Public Function GetStudentByRegistrationNumber(regNo As String) As Student
            If String.IsNullOrWhiteSpace(regNo) Then Return Nothing
            Dim sql As String =
                "SELECT s.student_id, s.registration_number, s.first_name, s.middle_name, s.last_name, " &
                "       s.gender, s.email, s.phone_number, s.course, s.department, s.academic_year, " &
                "       s.emergency_contact_name, s.emergency_contact_phone, s.student_status, " &
                "       s.created_at, s.updated_at, " &
                "       0 AS has_active_allocation, '' AS bed_identifier, 0.00 AS total_balance " &
                "FROM dbo.students s WHERE s.registration_number = @regNo;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@regNo", regNo.Trim(), SqlDbType.NVarChar)})
            If dt.Rows.Count > 0 Then
                Return MapStudentFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        Public Function GetStudentByEmail(email As String) As Student
            If String.IsNullOrWhiteSpace(email) Then Return Nothing
            Dim sql As String =
                "SELECT s.student_id, s.registration_number, s.first_name, s.middle_name, s.last_name, " &
                "       s.gender, s.email, s.phone_number, s.course, s.department, s.academic_year, " &
                "       s.emergency_contact_name, s.emergency_contact_phone, s.student_status, " &
                "       s.created_at, s.updated_at, " &
                "       0 AS has_active_allocation, '' AS bed_identifier, 0.00 AS total_balance " &
                "FROM dbo.students s WHERE s.email = @email;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@email", email.Trim().ToLowerInvariant(), SqlDbType.NVarChar)})
            If dt.Rows.Count > 0 Then
                Return MapStudentFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        Public Function CreateStudent(student As Student) As (Success As Boolean, Message As String, StudentId As Integer)
            ' Registration number uniqueness check
            Dim checkRegSql As String = "SELECT COUNT(*) FROM dbo.students WHERE registration_number = @regNo;"
            Dim existsCount As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkRegSql, {DatabaseConnection.CreateParam("@regNo", student.RegistrationNumber.Trim(), SqlDbType.NVarChar)}))
            If existsCount > 0 Then
                Return (False, $"Student registration number '{student.RegistrationNumber}' already exists.", 0)
            End If

            ' Email uniqueness check
            Dim checkEmailSql As String = "SELECT COUNT(*) FROM dbo.students WHERE email = @email;"
            Dim emailCount As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkEmailSql, {DatabaseConnection.CreateParam("@email", student.Email.Trim().ToLowerInvariant(), SqlDbType.NVarChar)}))
            If emailCount > 0 Then
                Return (False, $"Student email address '{student.Email}' is already registered.", 0)
            End If

            Dim sql As String =
                "INSERT INTO dbo.students (registration_number, first_name, middle_name, last_name, gender, email, phone_number, " &
                "                          course, department, academic_year, emergency_contact_name, emergency_contact_phone, " &
                "                          student_status, created_at, updated_at) " &
                "VALUES (@regNo, @first, @mid, @last, @gender, @email, @phone, @course, @dept, @year, @emName, @emPhone, @status, SYSUTCDATETIME(), SYSUTCDATETIME()); " &
                "SELECT CAST(SCOPE_IDENTITY() AS INT);"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@regNo", student.RegistrationNumber.Trim().ToUpperInvariant(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@first", student.FirstName.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@mid", If(String.IsNullOrEmpty(student.MiddleName), DBNull.Value, student.MiddleName.Trim()), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@last", student.LastName.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@gender", student.Gender, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@email", student.Email.Trim().ToLowerInvariant(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@phone", student.PhoneNumber.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@course", student.Course.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@dept", student.Department.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@year", student.AcademicYear.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@emName", student.EmergencyContactName.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@emPhone", student.EmergencyContactPhone.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@status", student.StudentStatus, SqlDbType.NVarChar)
            }

            Dim result = DatabaseConnection.ExecuteScalar(sql, parameters)
            If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                student.StudentId = Convert.ToInt32(result)
                Return (True, "Student registered successfully.", student.StudentId)
            End If
            Return (False, "Failed to register student.", 0)
        End Function

        Public Function UpdateStudent(student As Student) As (Success As Boolean, Message As String)
            ' Check email uniqueness excluding self
            Dim checkEmailSql As String = "SELECT COUNT(*) FROM dbo.students WHERE email = @email AND student_id <> @id;"
            Dim emailParams As SqlParameter() = {
                DatabaseConnection.CreateParam("@email", student.Email.Trim().ToLowerInvariant(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@id", student.StudentId, SqlDbType.Int)
            }
            Dim emailCount As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkEmailSql, emailParams))
            If emailCount > 0 Then
                Return (False, $"Email address '{student.Email}' is used by another student.")
            End If

            Dim sql As String =
                "UPDATE dbo.students " &
                "SET first_name = @first, " &
                "    middle_name = @mid, " &
                "    last_name = @last, " &
                "    gender = @gender, " &
                "    email = @email, " &
                "    phone_number = @phone, " &
                "    course = @course, " &
                "    department = @dept, " &
                "    academic_year = @year, " &
                "    emergency_contact_name = @emName, " &
                "    emergency_contact_phone = @emPhone, " &
                "    student_status = @status, " &
                "    updated_at = SYSUTCDATETIME() " &
                "WHERE student_id = @id;"

            Dim parameters As SqlParameter() = {
                DatabaseConnection.CreateParam("@id", student.StudentId, SqlDbType.Int),
                DatabaseConnection.CreateParam("@first", student.FirstName.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@mid", If(String.IsNullOrEmpty(student.MiddleName), DBNull.Value, student.MiddleName.Trim()), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@last", student.LastName.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@gender", student.Gender, SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@email", student.Email.Trim().ToLowerInvariant(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@phone", student.PhoneNumber.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@course", student.Course.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@dept", student.Department.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@year", student.AcademicYear.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@emName", student.EmergencyContactName.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@emPhone", student.EmergencyContactPhone.Trim(), SqlDbType.NVarChar),
                DatabaseConnection.CreateParam("@status", student.StudentStatus, SqlDbType.NVarChar)
            }

            Dim rows = DatabaseConnection.ExecuteNonQuery(sql, parameters)
            If rows > 0 Then
                Return (True, "Student profile updated successfully.")
            Else
                Return (False, "Student not found.")
            End If
        End Function

        Public Function DeleteStudent(studentId As Integer) As (Success As Boolean, Message As String)
            ' Check active or historical allocations
            Dim checkAllocSql As String = "SELECT COUNT(*) FROM dbo.bed_allocations WHERE student_id = @id;"
            Dim allocCount As Integer = Convert.ToInt32(DatabaseConnection.ExecuteScalar(checkAllocSql, {DatabaseConnection.CreateParam("@id", studentId, SqlDbType.Int)}))
            If allocCount > 0 Then
                Return (False, $"Cannot delete student: {allocCount} allocation record(s) exist. Set student status to SUSPENDED or GRADUATED to preserve historical records.")
            End If

            Dim sql As String = "DELETE FROM dbo.students WHERE student_id = @id;"
            Dim rows = DatabaseConnection.ExecuteNonQuery(sql, {DatabaseConnection.CreateParam("@id", studentId, SqlDbType.Int)})
            If rows > 0 Then
                Return (True, "Student record deleted successfully.")
            Else
                Return (False, "Student not found.")
            End If
        End Function

        Private Function MapStudentFromRow(row As DataRow) As Student
            Return New Student() With {
                .StudentId = Convert.ToInt32(row("student_id")),
                .RegistrationNumber = row("registration_number").ToString(),
                .FirstName = row("first_name").ToString(),
                .MiddleName = If(IsDBNull(row("middle_name")), String.Empty, row("middle_name").ToString()),
                .LastName = row("last_name").ToString(),
                .Gender = row("gender").ToString(),
                .Email = row("email").ToString(),
                .PhoneNumber = row("phone_number").ToString(),
                .Course = row("course").ToString(),
                .Department = row("department").ToString(),
                .AcademicYear = row("academic_year").ToString(),
                .EmergencyContactName = row("emergency_contact_name").ToString(),
                .EmergencyContactPhone = row("emergency_contact_phone").ToString(),
                .StudentStatus = row("student_status").ToString(),
                .CreatedAt = Convert.ToDateTime(row("created_at")),
                .UpdatedAt = Convert.ToDateTime(row("updated_at")),
                .HasActiveAllocation = If(row.Table.Columns.Contains("has_active_allocation"), Convert.ToBoolean(row("has_active_allocation")), False),
                .CurrentBedIdentifier = If(row.Table.Columns.Contains("bed_identifier") AndAlso Not IsDBNull(row("bed_identifier")), row("bed_identifier").ToString(), String.Empty),
                .TotalBalanceOwed = If(row.Table.Columns.Contains("total_balance") AndAlso Not IsDBNull(row("total_balance")), Convert.ToDecimal(row("total_balance")), 0.0D)
            }
        End Function
    End Class
End Namespace

