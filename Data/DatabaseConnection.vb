Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Data.SqlClient

Namespace Data
    ''' <summary>
    ''' Centralized ADO.NET connection provider and query execution helper.
    ''' Manages SqlConnection lifecycle, connection testing, transactions and parameterized query execution.
    ''' </summary>
    Public NotInheritable Class DatabaseConnection
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Creates a new, unopened SqlConnection instance.
        ''' Caller is responsible for opening and disposing.
        ''' </summary>
        Public Shared Function CreateConnection() As SqlConnection
            Return New SqlConnection(DbConfig.GetConnectionString())
        End Function

        ''' <summary>
        ''' Creates and opens a new SqlConnection.
        ''' Caller is responsible for disposing.
        ''' </summary>
        Public Shared Function GetOpenConnection() As SqlConnection
            Dim conn As SqlConnection = CreateConnection()
            conn.Open()
            Return conn
        End Function

        ''' <summary>
        ''' Asynchronously creates and opens a new SqlConnection.
        ''' </summary>
        Public Shared Async Function GetOpenConnectionAsync(Optional cancellationToken As CancellationToken = Nothing) As Task(Of SqlConnection)
            Dim conn As SqlConnection = CreateConnection()
            Await conn.OpenAsync(cancellationToken).ConfigureAwait(False)
            Return conn
        End Function

        ''' <summary>
        ''' Tests connectivity to the database server and returns diagnostics.
        ''' </summary>
        Public Shared Function TestConnection() As (Success As Boolean, Message As String, ServerVersion As String)
            Try
                DbConfig.ValidateConfig()

                Using conn As SqlConnection = CreateConnection()
                    conn.Open()
                    Dim version As String = conn.ServerVersion
                    Using cmd As New SqlCommand("SELECT @@VERSION;", conn)
                        Dim fullVer As Object = cmd.ExecuteScalar()
                        Return (True, "Connection successful.", If(fullVer IsNot Nothing, fullVer.ToString(), version))
                    End Using
                End Using
            Catch ex As SqlException
                Return (False, $"SQL Connection Error [{ex.Number}]: {ex.Message}", String.Empty)
            Catch ex As Exception
                Return (False, $"Database Error: {ex.Message}", String.Empty)
            End Try
        End Function

        ''' <summary>
        ''' Asynchronously tests connectivity to the database server.
        ''' </summary>
        Public Shared Async Function TestConnectionAsync(Optional cancellationToken As CancellationToken = Nothing) As Task(Of (Success As Boolean, Message As String, ServerVersion As String))
            Try
                DbConfig.ValidateConfig()

                Using conn As SqlConnection = CreateConnection()
                    Await conn.OpenAsync(cancellationToken).ConfigureAwait(False)
                    Dim version As String = conn.ServerVersion
                    Using cmd As New SqlCommand("SELECT @@VERSION;", conn)
                        Dim fullVer As Object = Await cmd.ExecuteScalarAsync(cancellationToken).ConfigureAwait(False)
                        Return (True, "Connection successful.", If(fullVer IsNot Nothing, fullVer.ToString(), version))
                    End Using
                End Using
            Catch ex As SqlException
                Return (False, $"SQL Connection Error [{ex.Number}]: {ex.Message}", String.Empty)
            Catch ex As Exception
                Return (False, $"Database Error: {ex.Message}", String.Empty)
            End Try
        End Function

        ''' <summary>
        ''' Executes a parameterized SQL command that returns the number of rows affected.
        ''' </summary>
        Public Shared Function ExecuteNonQuery(sql As String,
                                               Optional parameters As SqlParameter() = Nothing,
                                               Optional commandType As CommandType = CommandType.Text,
                                               Optional transaction As SqlTransaction = Nothing) As Integer
            Dim shouldClose As Boolean = False
            Dim conn As SqlConnection = If(transaction IsNot Nothing, transaction.Connection, Nothing)

            If conn Is Nothing Then
                conn = GetOpenConnection()
                shouldClose = True
            End If

            Try
                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = commandType
                    If transaction IsNot Nothing Then cmd.Transaction = transaction
                    If parameters IsNot Nothing AndAlso parameters.Length > 0 Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    Return cmd.ExecuteNonQuery()
                End Using
            Finally
                If shouldClose AndAlso conn IsNot Nothing Then
                    conn.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Asynchronously executes a parameterized SQL command that returns rows affected.
        ''' </summary>
        Public Shared Async Function ExecuteNonQueryAsync(sql As String,
                                                          Optional parameters As SqlParameter() = Nothing,
                                                          Optional commandType As CommandType = CommandType.Text,
                                                          Optional transaction As SqlTransaction = Nothing,
                                                          Optional cancellationToken As CancellationToken = Nothing) As Task(Of Integer)
            Dim shouldClose As Boolean = False
            Dim conn As SqlConnection = If(transaction IsNot Nothing, transaction.Connection, Nothing)

            If conn Is Nothing Then
                conn = Await GetOpenConnectionAsync(cancellationToken).ConfigureAwait(False)
                shouldClose = True
            End If

            Try
                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = commandType
                    If transaction IsNot Nothing Then cmd.Transaction = transaction
                    If parameters IsNot Nothing AndAlso parameters.Length > 0 Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    Return Await cmd.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(False)
                End Using
            Finally
                If shouldClose AndAlso conn IsNot Nothing Then
                    conn.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Executes a parameterized SQL query that returns a single scalar value.
        ''' </summary>
        Public Shared Function ExecuteScalar(sql As String,
                                             Optional parameters As SqlParameter() = Nothing,
                                             Optional commandType As CommandType = CommandType.Text,
                                             Optional transaction As SqlTransaction = Nothing) As Object
            Dim shouldClose As Boolean = False
            Dim conn As SqlConnection = If(transaction IsNot Nothing, transaction.Connection, Nothing)

            If conn Is Nothing Then
                conn = GetOpenConnection()
                shouldClose = True
            End If

            Try
                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = commandType
                    If transaction IsNot Nothing Then cmd.Transaction = transaction
                    If parameters IsNot Nothing AndAlso parameters.Length > 0 Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    Return cmd.ExecuteScalar()
                End Using
            Finally
                If shouldClose AndAlso conn IsNot Nothing Then
                    conn.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Asynchronously executes a parameterized SQL query that returns a single scalar value.
        ''' </summary>
        Public Shared Async Function ExecuteScalarAsync(sql As String,
                                                        Optional parameters As SqlParameter() = Nothing,
                                                        Optional commandType As CommandType = CommandType.Text,
                                                        Optional transaction As SqlTransaction = Nothing,
                                                        Optional cancellationToken As CancellationToken = Nothing) As Task(Of Object)
            Dim shouldClose As Boolean = False
            Dim conn As SqlConnection = If(transaction IsNot Nothing, transaction.Connection, Nothing)

            If conn Is Nothing Then
                conn = Await GetOpenConnectionAsync(cancellationToken).ConfigureAwait(False)
                shouldClose = True
            End If

            Try
                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = commandType
                    If transaction IsNot Nothing Then cmd.Transaction = transaction
                    If parameters IsNot Nothing AndAlso parameters.Length > 0 Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    Return Await cmd.ExecuteScalarAsync(cancellationToken).ConfigureAwait(False)
                End Using
            Finally
                If shouldClose AndAlso conn IsNot Nothing Then
                    conn.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Executes a query and returns the results in a DataTable.
        ''' </summary>
        Public Shared Function ExecuteDataTable(sql As String,
                                                Optional parameters As SqlParameter() = Nothing,
                                                Optional commandType As CommandType = CommandType.Text,
                                                Optional transaction As SqlTransaction = Nothing) As DataTable
            Dim table As New DataTable()
            Dim shouldClose As Boolean = False
            Dim conn As SqlConnection = If(transaction IsNot Nothing, transaction.Connection, Nothing)

            If conn Is Nothing Then
                conn = GetOpenConnection()
                shouldClose = True
            End If

            Try
                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = commandType
                    If transaction IsNot Nothing Then cmd.Transaction = transaction
                    If parameters IsNot Nothing AndAlso parameters.Length > 0 Then
                        cmd.Parameters.AddRange(parameters)
                    End If

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        table.Load(reader)
                    End Using
                End Using
            Finally
                If shouldClose AndAlso conn IsNot Nothing Then
                    conn.Dispose()
                End If
            End Try

            Return table
        End Function

        ''' <summary>
        ''' Creates a parameterized SqlParameter with DBNull protection.
        ''' </summary>
        Public Shared Function CreateParam(name As String, value As Object, Optional dbType As SqlDbType? = Nothing) As SqlParameter
            Dim param As New SqlParameter()
            param.ParameterName = If(name.StartsWith("@"), name, "@" & name)
            param.Value = If(value Is Nothing, DBNull.Value, value)
            If dbType.HasValue Then
                param.SqlDbType = dbType.Value
            End If
            Return param
        End Function
    End Class
End Namespace

