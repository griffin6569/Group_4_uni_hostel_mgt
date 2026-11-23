Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports University_hostel_mgt.Controls
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Users
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' System User Administration & RBAC Security Control.
    ''' Manages staff accounts, security role assignment, password resets,
    ''' and visualizes immutable activity audit logs in Frosted Glassmorphism.
    ''' </summary>
    Public Class UsersControl
        Inherits UserControl

        Private ReadOnly _currentUserId As Integer
        Private ReadOnly _userRepo As New UserRepository()

        ' Metrics Strip (Responsive 4-Widget TableLayoutPanel)
        Private pnlMetricsRow As TableLayoutPanel
        Private kpiTotalUsers As GlassKpiWidget
        Private kpiActiveUsers As GlassKpiWidget
        Private kpiAdmins As GlassKpiWidget
        Private kpiOfficers As GlassKpiWidget

        Private tabMain As TabControl
        Private tabUsers As TabPage
        Private tabAudit As TabPage

        ' Users Tab Controls
        Private pnlUserActions As PrestigeGlassCard
        Private searchBoxUser As GlassSearchBox
        Private cboRoleFilter As ComboBox
        Private btnAddUser As GlassButton
        Private btnEditUser As GlassButton
        Private btnRefreshUsers As GlassButton
        Private dgvUsers As DataGridView

        ' Audit Tab Controls
        Private pnlAuditActions As PrestigeGlassCard
        Private searchBoxAudit As GlassSearchBox
        Private btnRefreshAudit As GlassButton
        Private dgvAudit As DataGridView

        Private _users As List(Of User) = New List(Of User)()
        Private _selectedUser As User = Nothing

        Public Sub New(currentUserId As Integer)
            _currentUserId = currentUserId
            InitializeUI()
            Dock = DockStyle.Fill
            BackColor = ThemeManager.CanvasDark
            ThemeManager.EnableDoubleBuffering(Me)
        End Sub

        Private Sub InitializeUI()
            Me.SuspendLayout()

            ' =========================================================================
            ' 1. KPI SUMMARY ROW (4 Frosted Glass Widgets)
            ' =========================================================================
            pnlMetricsRow = New TableLayoutPanel With {
                .Dock = DockStyle.Top,
                .Height = 100,
                .ColumnCount = 4,
                .RowCount = 1,
                .BackColor = Color.Transparent,
                .Padding = New Padding(16, 12, 16, 8)
            }
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
            pnlMetricsRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))

            kpiTotalUsers = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(0, 0, 6, 0),
                .CategoryTitle = "TOTAL STAFF MEMBERS",
                .PrimaryValue = "0",
                .Subtitle = "Registered university personnel",
                .AccentColor = ThemeManager.GlassStatusInfo,
                .IconSymbol = "👥"
            }

            kpiActiveUsers = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4, 0, 4, 0),
                .CategoryTitle = "ACTIVE CREDENTIALS",
                .PrimaryValue = "0",
                .Subtitle = "Operational system logins",
                .AccentColor = ThemeManager.GlassStatusSuccess,
                .ValueColor = ThemeManager.GlassStatusSuccess,
                .IconSymbol = "🛡️"
            }

            kpiAdmins = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(4, 0, 4, 0),
                .CategoryTitle = "ADMINISTRATORS",
                .PrimaryValue = "0",
                .Subtitle = "Privileged security access",
                .AccentColor = ThemeManager.GlassStatusWarning,
                .ValueColor = ThemeManager.GlassStatusWarning,
                .IconSymbol = "🔑"
            }

            kpiOfficers = New GlassKpiWidget With {
                .Dock = DockStyle.Fill,
                .Margin = New Padding(6, 0, 0, 0),
                .CategoryTitle = "DEPARTMENT OFFICERS",
                .PrimaryValue = "0",
                .Subtitle = "Hostel & Bursary desk staff",
                .AccentColor = ThemeManager.AccentSapphire,
                .ValueColor = ThemeManager.GlassStatusInfo,
                .IconSymbol = "💼"
            }

            pnlMetricsRow.Controls.Add(kpiTotalUsers, 0, 0)
            pnlMetricsRow.Controls.Add(kpiActiveUsers, 1, 0)
            pnlMetricsRow.Controls.Add(kpiAdmins, 2, 0)
            pnlMetricsRow.Controls.Add(kpiOfficers, 3, 0)

            ' =========================================================================
            ' 2. MAIN TAB CONTROL
            ' =========================================================================
            tabMain = New TabControl With {
                .Dock = DockStyle.Fill,
                .Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                .Padding = New Point(12, 6)
            }

            ' TAB 1: USERS
            tabUsers = New TabPage("  👥 Staff Directory & RBAC Roles  ") With {
                .BackColor = ThemeManager.CanvasDark,
                .Padding = New Padding(12)
            }
            InitializeUsersTab()

            ' TAB 2: AUDIT LOGS
            tabAudit = New TabPage("  📜 System Activity Audit Trail  ") With {
                .BackColor = ThemeManager.CanvasDark,
                .Padding = New Padding(12)
            }
            InitializeAuditTab()

            tabMain.TabPages.AddRange({tabUsers, tabAudit})

            Controls.Add(tabMain)
            Controls.Add(pnlMetricsRow)

            Me.ResumeLayout(False)

            AddHandler tabMain.SelectedIndexChanged, Sub()
                                                         If tabMain.SelectedTab Is tabUsers Then
                                                             LoadUsersData()
                                                         Else
                                                             LoadAuditData()
                                                         End If
                                                     End Sub
        End Sub

        Private Sub InitializeUsersTab()
            pnlUserActions = New PrestigeGlassCard With {
                .Dock = DockStyle.Top,
                .Height = 58,
                .Margin = New Padding(0, 0, 0, 8),
                .Padding = New Padding(16, 10, 16, 10),
                .AccentColor = ThemeManager.GlassBorderFocus,
                .AccentHeight = 2
            }

            searchBoxUser = New GlassSearchBox With {
                .Location = New Point(16, 11),
                .Width = 240,
                .PlaceholderText = "Search staff, username, email..."
            }

            Dim lblR As New Label With {
                .Text = "Filter Role:",
                .AutoSize = True,
                .Location = New Point(270, 18),
                .Font = ThemeManager.FontSmallBold,
                .ForeColor = ThemeManager.TextSilverSlate
            }

            cboRoleFilter = New ComboBox With {
                .Location = New Point(345, 14),
                .Width = 160,
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .BackColor = ThemeManager.GlassInputFill,
                .ForeColor = ThemeManager.TextChalkWhite,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 9.0F)
            }
            cboRoleFilter.Items.Add("ALL ROLES")
            cboRoleFilter.SelectedIndex = 0

            btnAddUser = New GlassButton With {
                .Text = "➕ Add Staff User",
                .Size = New Size(150, 32),
                .Location = New Point(520, 11)
            }
            btnAddUser.SetPrimaryStyle()

            btnEditUser = New GlassButton With {
                .Text = "✏️ Edit Account",
                .Size = New Size(130, 32),
                .Location = New Point(680, 11),
                .Enabled = False
            }
            btnEditUser.SetSecondaryStyle()

            btnRefreshUsers = New GlassButton With {
                .Text = "🔄 Refresh",
                .Size = New Size(95, 32),
                .Location = New Point(820, 11)
            }
            btnRefreshUsers.SetSecondaryStyle()

            pnlUserActions.Controls.AddRange({searchBoxUser, lblR, cboRoleFilter, btnAddUser, btnEditUser, btnRefreshUsers})

            dgvUsers = New DataGridView With {
                .Dock = DockStyle.Fill,
                .BorderStyle = BorderStyle.None,
                .AllowUserToAddRows = False,
                .AllowUserToDeleteRows = False,
                .ReadOnly = True,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .MultiSelect = False,
                .RowHeadersVisible = False,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvUsers)
            ThemeManager.EnableDoubleBuffering(dgvUsers)

            AddHandler dgvUsers.CellFormatting, AddressOf dgvUsers_CellFormatting

            tabUsers.Controls.Add(dgvUsers)
            tabUsers.Controls.Add(pnlUserActions)

            AddHandler btnAddUser.Click, AddressOf btnAddUser_Click
            AddHandler btnEditUser.Click, AddressOf btnEditUser_Click
            AddHandler btnRefreshUsers.Click, Sub() LoadUsersData()
            AddHandler searchBoxUser.SearchTextChanged, Sub() ApplyUserFilter()
            AddHandler cboRoleFilter.SelectedIndexChanged, Sub() ApplyUserFilter()
            AddHandler dgvUsers.SelectionChanged, AddressOf dgvUsers_SelectionChanged
        End Sub

        Private Sub InitializeAuditTab()
            pnlAuditActions = New PrestigeGlassCard With {
                .Dock = DockStyle.Top,
                .Height = 58,
                .Margin = New Padding(0, 0, 0, 8),
                .Padding = New Padding(16, 10, 16, 10),
                .AccentColor = ThemeManager.AcademicGold,
                .AccentHeight = 2
            }

            searchBoxAudit = New GlassSearchBox With {
                .Location = New Point(16, 11),
                .Width = 320,
                .PlaceholderText = "Search action, staff username, target..."
            }

            btnRefreshAudit = New GlassButton With {
                .Text = "🔄 Refresh Audit Logs",
                .Size = New Size(170, 32),
                .Location = New Point(350, 11)
            }
            btnRefreshAudit.SetSecondaryStyle()

            pnlAuditActions.Controls.AddRange({searchBoxAudit, btnRefreshAudit})

            dgvAudit = New DataGridView With {
                .Dock = DockStyle.Fill,
                .BorderStyle = BorderStyle.None,
                .AllowUserToAddRows = False,
                .AllowUserToDeleteRows = False,
                .ReadOnly = True,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .MultiSelect = False,
                .RowHeadersVisible = False,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            }
            ThemeManager.ApplyGlassGridStyle(dgvAudit)
            ThemeManager.EnableDoubleBuffering(dgvAudit)

            AddHandler dgvAudit.CellFormatting, AddressOf dgvAudit_CellFormatting

            tabAudit.Controls.Add(dgvAudit)
            tabAudit.Controls.Add(pnlAuditActions)

            AddHandler btnRefreshAudit.Click, Sub() LoadAuditData()
            AddHandler searchBoxAudit.SearchTextChanged, Sub() LoadAuditData()
        End Sub

        Public Sub LoadUsersData()
            ' Populate role filter
            If cboRoleFilter.Items.Count <= 1 Then
                For Each r In _userRepo.GetAllRoles()
                    cboRoleFilter.Items.Add(r.RoleName)
                Next
            End If

            _users = _userRepo.GetAllUsers()
            ApplyUserFilter()
            UpdateKpiCards()
        End Sub

        Private Sub ApplyUserFilter()
            Dim search = searchBoxUser.Text.Trim().ToLowerInvariant()
            Dim roleFilter = If(cboRoleFilter.SelectedItem IsNot Nothing, cboRoleFilter.SelectedItem.ToString(), "ALL ROLES")

            Dim filtered = _users.Where(Function(u)
                                            Dim matchesSearch = String.IsNullOrWhiteSpace(search) OrElse
                                                                u.FullName.ToLowerInvariant().Contains(search) OrElse
                                                                u.Username.ToLowerInvariant().Contains(search) OrElse
                                                                u.Email.ToLowerInvariant().Contains(search)

                                            Dim matchesRole = (roleFilter = "ALL ROLES") OrElse (u.RoleName.Equals(roleFilter, StringComparison.OrdinalIgnoreCase))
                                            Return matchesSearch AndAlso matchesRole
                                        End Function).ToList()

            dgvUsers.Columns.Clear()
            dgvUsers.Columns.Add("Id", "User ID")
            dgvUsers.Columns("Id").Width = 70

            dgvUsers.Columns.Add("FullName", "Full Name")
            dgvUsers.Columns("FullName").Width = 180

            dgvUsers.Columns.Add("Username", "Username")
            dgvUsers.Columns("Username").Width = 120

            dgvUsers.Columns.Add("Email", "Official Email")
            dgvUsers.Columns("Email").Width = 200

            dgvUsers.Columns.Add("Role", "Security Role")
            dgvUsers.Columns("Role").Width = 150

            dgvUsers.Columns.Add("Status", "Account Status")
            dgvUsers.Columns("Status").Width = 110

            dgvUsers.Columns.Add("LastLogin", "Last Active Session")
            dgvUsers.Columns("LastLogin").Width = 140

            For Each u In filtered
                Dim rowIdx = dgvUsers.Rows.Add(
                    $"#{u.UserId}",
                    u.FullName,
                    u.Username,
                    u.Email,
                    u.RoleName,
                    u.AccountStatus,
                    If(u.LastLogin.HasValue, u.LastLogin.Value.ToString("yyyy-MM-dd HH:mm"), "Never")
                )
                dgvUsers.Rows(rowIdx).Tag = u
            Next

            btnEditUser.Enabled = (dgvUsers.SelectedRows.Count > 0)
        End Sub

        Public Sub LoadAuditData()
            Dim dt = _userRepo.GetActivityLogs(150)
            Dim search = searchBoxAudit.Text.Trim().ToLowerInvariant()

            dgvAudit.Columns.Clear()
            dgvAudit.Columns.Add("Id", "Log ID")
            dgvAudit.Columns("Id").Width = 70

            dgvAudit.Columns.Add("Timestamp", "Timestamp (UTC)")
            dgvAudit.Columns("Timestamp").Width = 140

            dgvAudit.Columns.Add("User", "Staff User")
            dgvAudit.Columns("User").Width = 130

            dgvAudit.Columns.Add("Action", "Action Type")
            dgvAudit.Columns("Action").Width = 150

            dgvAudit.Columns.Add("Entity", "Entity Target")
            dgvAudit.Columns("Entity").Width = 130

            dgvAudit.Columns.Add("Description", "Audit Narrative / Event Description")
            dgvAudit.Columns("Description").Width = 320

            dgvAudit.Columns.Add("Ip", "IP Address")
            dgvAudit.Columns("Ip").Width = 100

            For Each row As DataRow In dt.Rows
                Dim desc = row("description").ToString()
                Dim actionType = row("action_type").ToString()
                Dim user = row("staff_username").ToString()
                Dim entity = $"{row("entity_type")} ({row("entity_id")})"

                If Not String.IsNullOrWhiteSpace(search) Then
                    If Not desc.ToLowerInvariant().Contains(search) AndAlso
                       Not actionType.ToLowerInvariant().Contains(search) AndAlso
                       Not user.ToLowerInvariant().Contains(search) Then
                        Continue For
                    End If
                End If

                dgvAudit.Rows.Add(
                    $"#{row("log_id")}",
                    Convert.ToDateTime(row("action_timestamp")).ToString("yyyy-MM-dd HH:mm:ss"),
                    user,
                    actionType,
                    entity,
                    desc,
                    row("ip_address").ToString()
                )
            Next
        End Sub

        Private Sub UpdateKpiCards()
            Dim totalUsers = _users.Count
            Dim activeUsers = _users.Where(Function(u) u.AccountStatus = "ACTIVE").Count()
            Dim admins = _users.Where(Function(u) u.RoleName.ToUpperInvariant().Contains("ADMIN")).Count()
            Dim officers = _users.Where(Function(u) u.RoleName.ToUpperInvariant().Contains("OFFICER")).Count()

            kpiTotalUsers.PrimaryValue = totalUsers.ToString()
            kpiActiveUsers.PrimaryValue = activeUsers.ToString()
            kpiAdmins.PrimaryValue = admins.ToString()
            kpiOfficers.PrimaryValue = officers.ToString()
        End Sub

        Private Sub dgvUsers_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If e.RowIndex < 0 Then Return

            Dim colName = dgvUsers.Columns(e.ColumnIndex).Name
            If colName = "Status" AndAlso e.Value IsNot Nothing Then
                Dim val = e.Value.ToString().ToUpperInvariant()
                If val = "ACTIVE" Then
                    e.CellStyle.ForeColor = ThemeManager.GlassStatusSuccess
                    e.CellStyle.Font = New Font(dgvUsers.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = ThemeManager.GlassStatusDanger
                    e.CellStyle.Font = New Font(dgvUsers.Font, FontStyle.Bold)
                End If
            ElseIf colName = "Role" AndAlso e.Value IsNot Nothing Then
                Dim rVal = e.Value.ToString().ToUpperInvariant()
                If rVal.Contains("ADMIN") Then
                    e.CellStyle.ForeColor = ThemeManager.GlassStatusWarning
                    e.CellStyle.Font = New Font(dgvUsers.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = ThemeManager.GlassStatusInfo
                End If
            ElseIf colName = "Id" Then
                e.CellStyle.ForeColor = ThemeManager.AcademicGold
                e.CellStyle.Font = New Font(dgvUsers.Font, FontStyle.Bold)
            End If
        End Sub

        Private Sub dgvAudit_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If e.RowIndex < 0 Then Return

            Dim colName = dgvAudit.Columns(e.ColumnIndex).Name
            If colName = "Action" AndAlso e.Value IsNot Nothing Then
                Dim act = e.Value.ToString().ToUpperInvariant()
                If act.Contains("CREATE") OrElse act.Contains("ALLOCATE") OrElse act.Contains("LOGIN") OrElse act.Contains("SUCCESS") Then
                    e.CellStyle.ForeColor = ThemeManager.GlassStatusSuccess
                    e.CellStyle.Font = New Font(dgvAudit.Font, FontStyle.Bold)
                ElseIf act.Contains("DELETE") OrElse act.Contains("REVOKE") OrElse act.Contains("FAILED") Then
                    e.CellStyle.ForeColor = ThemeManager.GlassStatusDanger
                    e.CellStyle.Font = New Font(dgvAudit.Font, FontStyle.Bold)
                ElseIf act.Contains("UPDATE") OrElse act.Contains("RESET") Then
                    e.CellStyle.ForeColor = ThemeManager.GlassStatusWarning
                    e.CellStyle.Font = New Font(dgvAudit.Font, FontStyle.Bold)
                Else
                    e.CellStyle.ForeColor = ThemeManager.TextPlatinum
                End If
            ElseIf colName = "Id" Then
                e.CellStyle.ForeColor = ThemeManager.AcademicGold
                e.CellStyle.Font = New Font(dgvAudit.Font, FontStyle.Bold)
            End If
        End Sub

        Private Sub dgvUsers_SelectionChanged(sender As Object, e As EventArgs)
            If dgvUsers.SelectedRows.Count > 0 Then
                _selectedUser = TryCast(dgvUsers.SelectedRows(0).Tag, User)
                btnEditUser.Enabled = (_selectedUser IsNot Nothing)
            Else
                _selectedUser = Nothing
                btnEditUser.Enabled = False
            End If
        End Sub

        Private Sub btnAddUser_Click(sender As Object, e As EventArgs)
            Using frm As New frmUserModal(_currentUserId)
                If frm.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadUsersData()
                End If
            End Using
        End Sub

        Private Sub btnEditUser_Click(sender As Object, e As EventArgs)
            If _selectedUser Is Nothing Then Return
            Using frm As New frmUserModal(_currentUserId, _selectedUser)
                If frm.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadUsersData()
                End If
            End Using
        End Sub
    End Class
End Namespace
