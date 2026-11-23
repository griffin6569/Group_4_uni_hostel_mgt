Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Microsoft.Data.SqlClient
Imports University_hostel_mgt.Models

Namespace Data.Repositories
    ''' <summary>
    ''' ADO.NET repository for managing academic semesters and active semester context.
    ''' </summary>
    Public Class SemesterRepository
        Public Function GetSemesters() As List(Of Semester)
            Dim list As New List(Of Semester)()
            Dim sql As String =
                "SELECT semester_id, semester_name, semester_code, academic_year, " &
                "       start_date, end_date, allocation_open_date, allocation_close_date, " &
                "       status, created_at " &
                "FROM dbo.semesters " &
                "ORDER BY academic_year DESC, start_date DESC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql)
            For Each row As DataRow In dt.Rows
                list.Add(MapSemesterFromRow(row))
            Next
            Return list
        End Function

        Public Function GetCurrentSemester() As Semester
            Dim sql As String =
                "SELECT TOP 1 semester_id, semester_name, semester_code, academic_year, " &
                "             start_date, end_date, allocation_open_date, allocation_close_date, " &
                "             status, created_at " &
                "FROM dbo.semesters " &
                "WHERE status = 'ACTIVE' " &
                "ORDER BY start_date DESC;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql)
            If dt.Rows.Count > 0 Then
                Return MapSemesterFromRow(dt.Rows(0))
            End If

            ' Fallback to most recent semester if none is explicitly active
            Dim fallbackSql As String =
                "SELECT TOP 1 semester_id, semester_name, semester_code, academic_year, " &
                "             start_date, end_date, allocation_open_date, allocation_close_date, " &
                "             status, created_at " &
                "FROM dbo.semesters " &
                "ORDER BY start_date DESC;"
            Dim fbDt As DataTable = DatabaseConnection.ExecuteDataTable(fallbackSql)
            If fbDt.Rows.Count > 0 Then
                Return MapSemesterFromRow(fbDt.Rows(0))
            End If

            Return Nothing
        End Function

        Public Function GetSemesterById(semesterId As Integer) As Semester
            Dim sql As String =
                "SELECT semester_id, semester_name, semester_code, academic_year, " &
                "       start_date, end_date, allocation_open_date, allocation_close_date, " &
                "       status, created_at " &
                "FROM dbo.semesters WHERE semester_id = @id;"

            Dim dt As DataTable = DatabaseConnection.ExecuteDataTable(sql, {DatabaseConnection.CreateParam("@id", semesterId, SqlDbType.Int)})
            If dt.Rows.Count > 0 Then
                Return MapSemesterFromRow(dt.Rows(0))
            End If
            Return Nothing
        End Function

        Public Function SetActiveSemester(semesterId As Integer) As Boolean
            Using conn = DatabaseConnection.GetOpenConnection()
                Using trans = conn.BeginTransaction()
                    Try
                        ' Deactivate any previously active semester
                        Dim deactivateSql As String = "UPDATE dbo.semesters SET status = 'UPCOMING' WHERE status = 'ACTIVE' AND semester_id <> @id;"
                        Using cmdDeact As New SqlCommand(deactivateSql, conn, trans)
                            cmdDeact.Parameters.Add(DatabaseConnection.CreateParam("@id", semesterId, SqlDbType.Int))
                            cmdDeact.ExecuteNonQuery()
                        End Using

                        ' Activate target semester
                        Dim activateSql As String = "UPDATE dbo.semesters SET status = 'ACTIVE' WHERE semester_id = @id;"
                        Using cmdAct As New SqlCommand(activateSql, conn, trans)
                            cmdAct.Parameters.Add(DatabaseConnection.CreateParam("@id", semesterId, SqlDbType.Int))
                            cmdAct.ExecuteNonQuery()
                        End Using

                        trans.Commit()
                        Return True
                    Catch
                        trans.Rollback()
                        Return False
                    End Try
                End Using
            End Using
        End Function

        Private Function MapSemesterFromRow(row As DataRow) As Semester
            Return New Semester() With {
                .SemesterId = Convert.ToInt32(row("semester_id")),
                .SemesterName = row("semester_name").ToString(),
                .SemesterCode = row("semester_code").ToString(),
                .AcademicYear = row("academic_year").ToString(),
                .StartDate = Convert.ToDateTime(row("start_date")),
                .EndDate = Convert.ToDateTime(row("end_date")),
                .AllocationOpenDate = Convert.ToDateTime(row("allocation_open_date")),
                .AllocationCloseDate = Convert.ToDateTime(row("allocation_close_date")),
                .Status = row("status").ToString(),
                .CreatedAt = Convert.ToDateTime(row("created_at"))
            }
        End Function
    End Class
End Namespace

