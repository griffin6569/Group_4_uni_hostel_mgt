Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports University_hostel_mgt.Data.Repositories
Imports University_hostel_mgt.Forms.Users
Imports University_hostel_mgt.Helpers
Imports University_hostel_mgt.Models

Namespace Controls.Views
    ''' <summary>
    ''' System User Administration & RBAC Security Control.
    ''' Manages staff accounts, security role assignment, password resets,
    ''' and visualizes immutable activity audit logs.
    ''' </summary>
    Public Class UsersControl
        Inherits UserControl

        Private ReadOnly _currentUserId As Integer
        Private ReadOnly _userRepo As New UserRepository()

        Private pnlKpi As Panel
        Private tabMain As TabControl
        Private tabUsers As TabPage
        Private tabAudit As TabPage

        ' Users Tab Controls
        Private pnlUserActions As Panel
        Private txtSearchUser As TextBox
        Private cboRoleFilter As ComboBox
        Private btnAddUser As Button
        Private btnEditUser As Button
        Private btnRefreshUsers As Button
        Private dgvUsers As DataGridView

        ' Audit Tab Controls
        Private pnlAuditActions As Panel
        Private txtSearchAudit As TextBox
        Private btnRefreshAudit As Button
        Private dgvAudit As DataGridView

        Private _users As List(Of User) = New List(Of User)()
        Private _selectedUser As User = Nothing

        Public Sub New(currentUserId As Integer)
            _currentUserId = currentUserId
            InitializeUI()
            Dock = DockStyle.Fill
            BackColor = Color.FromArgb(248, 250, 252)
        End Sub

        Private Sub InitializeUI()
            Me.SuspendLayout()

            ' 1. KPI Panel
            pnlKpi = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 84,
                .BackColor = Color.FromArgb(248, 250, 252),
                .Padding = New Padding(16, 8, 16, 8)
            }

            ' 2. Main Tab Control
            tabMain = New TabControl With {
                .Dock = DockStyle.Fill,
                .Font = New Font("Segoe UI Semibold", 9.0F)
            }

            ' TAB 1: USERS
            tabUsers = New TabPage("👥 Staff Directory & RBAC Roles") With {.BackColor = Color.White}
            InitializeUsersTab()

            ' TAB 2: AUDIT LOGS
            tabAudit = New TabPage("📜 System Activity Audit Trail") With {.BackColor = Color.White}
            InitializeAuditTab()

            tabMain.TabPages.AddRange({tabUsers, tabAudit})

            Controls.Add(tabMain)
            Controls.Add(pnlKpi)

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
            pnlUserActions = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 50,
                .BackColor = Color.White,
                .Padding = New Padding(16, 10, 16, 10)
            }

            txtSearchUser = New TextBox With {
                .Location = New Point(16, 13),
                .Width = 220,
                .PlaceholderText = "🔍 Search name, username, email..."
            }

            Dim lblR As New Label With {.Text = "Role:", .AutoSize = True, .Location = New Point(250, 16), .Font = New Font("Segoe UI Semibold", 8.5F)}
            cboRoleFilter = New ComboBox With {.Location = New Point(290, 13), .Width = 170, .DropDownStyle = ComboBoxStyle.DropDownList}
            cboRoleFilter.Items.Add("ALL ROLES")
            cboRoleFilter.SelectedIndex = 0

            btnAddUser = New Button With {
                .Text = "➕ Add Staff User",
                .BackColor = ThemeManager.NavyPrimary,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                .Size = New Size(140, 28),
                .Location = New Point(480, 11)
            }
            btnAddUser.FlatAppearance.BorderSize = 0

            btnEditUser = New Button With {
                .Text = "✏️ Edit Account",
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.NavyPrimary,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 8.5F),
                .Size = New Size(120, 28),
                .Location = New Point(630, 11),
                .Enabled = False
            }
            btnEditUser.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)

            btnRefreshUsers = New Button With {
                .Text = "🔄",
                .BackColor = Color.FromArgb(241, 245, 249),
                .FlatStyle = FlatStyle.Flat,
                .Size = New Size(32, 28),
                .Location = New Point(760, 11)
            }
            btnRefreshUsers.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)

            pnlUserActions.Controls.AddRange({txtSearchUser, lblR, cboRoleFilter, btnAddUser, btnEditUser, btnRefreshUsers})

            dgvUsers = New DataGridView With {
                .Dock = DockStyle.Fill,
                .BackgroundColor = Color.White,
                .BorderStyle = BorderStyle.None,
                .AllowUserToAddRows = False,
                .AllowUserToDeleteRows = False,
                .ReadOnly = True,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .MultiSelect = False,
                .RowHeadersVisible = False,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            }
            ThemeManager.ApplyPrestigeGridStyle(dgvUsers)

            tabUsers.Controls.Add(dgvUsers)
            tabUsers.Controls.Add(pnlUserActions)

            AddHandler btnAddUser.Click, AddressOf btnAddUser_Click
            AddHandler btnEditUser.Click, AddressOf btnEditUser_Click
            AddHandler btnRefreshUsers.Click, Sub() LoadUsersData()
            AddHandler txtSearchUser.TextChanged, Sub() ApplyUserFilter()
            AddHandler cboRoleFilter.SelectedIndexChanged, Sub() ApplyUserFilter()
            AddHandler dgvUsers.SelectionChanged, AddressOf dgvUsers_SelectionChanged
        End Sub

        Private Sub InitializeAuditTab()
            pnlAuditActions = New Panel With {
                .Dock = DockStyle.Top,
                .Height = 50,
                .BackColor = Color.White,
                .Padding = New Padding(16, 10, 16, 10)
            }

            txtSearchAudit = New TextBox With {
                .Location = New Point(16, 13),
                .Width = 260,
                .PlaceholderText = "🔍 Search action, staff, entity..."
            }

            btnRefreshAudit = New Button With {
                .Text = "🔄 Refresh Audit Trail",
                .BackColor = Color.FromArgb(241, 245, 249),
                .ForeColor = ThemeManager.NavyPrimary,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 8.5F),
                .Size = New Size(150, 28),
                .Location = New Point(290, 11)
            }
            btnRefreshAudit.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)

            pnlAuditActions.Controls.AddRange({txtSearchAudit, btnRefreshAudit})

            dgvAudit = New DataGridView With {
                .Dock = DockStyle.Fill,
                .BackgroundColor = Color.White,
                .BorderStyle = BorderStyle.None,
                .AllowUserToAddRows = False,
                .AllowUserToDeleteRows = False,
                .ReadOnly = True,
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                .MultiSelect = False,
                .RowHeadersVisible = False,
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            }
            ThemeManager.ApplyPrestigeGridStyle(dgvAudit)

            tabAudit.Controls.Add(dgvAudit)
            tabAudit.Controls.Add(pnlAuditActions)

            AddHandler btnRefreshAudit.Click, Sub() LoadAuditData()
            AddHandler txtSearchAudit.TextChanged, Sub() LoadAuditData()
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
            Dim search = txtSearchUser.Text.Trim().ToLowerInvariant()
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
            dgvUsers.Columns("Id").Width = 60

            dgvUsers.Columns.Add("FullName", "Full Name")
            dgvUsers.Columns("FullName").Width = 180

            dgvUsers.Columns.Add("Username", "Username")
            dgvUsers.Columns("Username").Width = 120

            dgvUsers.Columns.Add("Email", "Official Email")
            dgvUsers.Columns("Email").Width = 200

            dgvUsers.Columns.Add("Role", "Security Role")
            dgvUsers.Columns("Role").Width = 150

            dgvUsers.Columns.Add("Status", "Status")
            dgvUsers.Columns("Status").Width = 80

            dgvUsers.Columns.Add("LastLogin", "Last Login")
            dgvUsers.Columns("LastLogin").Width = 130

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
            Dim search = txtSearchAudit.Text.Trim().ToLowerInvariant()

            dgvAudit.Columns.Clear()
            dgvAudit.Columns.Add("Id", "Log ID")
            dgvAudit.Columns("Id").Width = 60

            dgvAudit.Columns.Add("Timestamp", "Timestamp (UTC)")
            dgvAudit.Columns("Timestamp").Width = 140

            dgvAudit.Columns.Add("User", "Staff User")
            dgvAudit.Columns("User").Width = 130

            dgvAudit.Columns.Add("Action", "Action Type")
            dgvAudit.Columns("Action").Width = 150

            dgvAudit.Columns.Add("Entity", "Entity Target")
            dgvAudit.Columns("Entity").Width = 120

            dgvAudit.Columns.Add("Description", "Audit Narrative / Event Description")
            dgvAudit.Columns("Description").Width = 320

            dgvAudit.Columns.Add("Ip", "IP Address")
            dgvAudit.Columns("Ip").Width = 90

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
            pnlKpi.Controls.Clear()
            Dim totalUsers = _users.Count
            Dim activeUsers = _users.Where(Function(u) u.AccountStatus = "ACTIVE").Count()
            Dim admins = _users.Where(Function(u) u.RoleName.Contains("ADMIN")).Count()
            Dim officers = _users.Where(Function(u) u.RoleName.Contains("OFFICER")).Count()

            Dim c1 = CreateKpiCard("TOTAL STAFF", totalUsers.ToString(), "Registered University Personnel", ThemeManager.NavyPrimary, 16)
            Dim c2 = CreateKpiCard("ACTIVE ACCOUNTS", activeUsers.ToString(), "Operational Credentials", Color.FromArgb(5, 150, 105), 246)
            Dim c3 = CreateKpiCard("ADMINISTRATORS", admins.ToString(), "SuperAdmin & Admin Privileges", Color.FromArgb(217, 119, 6), 476)
            Dim c4 = CreateKpiCard("DESK OFFICERS", officers.ToString(), "Accommodation & Bursary Staff", Color.FromArgb(15, 118, 110), 706)

            pnlKpi.Controls.AddRange({c1, c2, c3, c4})
        End Sub

        Private Function CreateKpiCard(title As String, value As String, subText As String, accentColor As Color, xPos As Integer) As Panel
            Dim pnl As New Panel With {
                .Size = New Size(215, 68),
                .Location = New Point(xPos, 8),
                .BackColor = Color.White
            }

            Dim pnlStripe As New Panel With {
                .Dock = DockStyle.Left,
                .Width = 4,
                .BackColor = accentColor
            }

            Dim lblT As New Label With {
                .Text = title,
                .Font = New Font("Segoe UI", 7.5F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(100, 116, 139),
                .Location = New Point(12, 8),
                .AutoSize = True
            }

            Dim lblV As New Label With {
                .Text = value,
                .Font = New Font("Segoe UI", 14.0F, FontStyle.Bold),
                .ForeColor = accentColor,
                .Location = New Point(12, 24),
                .AutoSize = True
            }

            Dim lblS As New Label With {
                .Text = subText,
                .Font = New Font("Segoe UI", 7.0F),
                .ForeColor = Color.FromArgb(148, 163, 184),
                .Location = New Point(12, 48),
                .AutoSize = True
            }

            pnl.Controls.AddRange({pnlStripe, lblT, lblV, lblS})
            Return pnl
        End Function

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
