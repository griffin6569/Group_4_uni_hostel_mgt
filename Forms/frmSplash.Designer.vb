Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class frmSplash
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.pnlTopGoldAccent = New System.Windows.Forms.Panel()
            Me.pnlBrandPillar = New System.Windows.Forms.Panel()
            Me.picCrest = New System.Windows.Forms.PictureBox()
            Me.lblUnivName = New System.Windows.Forms.Label()
            Me.lblDirectorate = New System.Windows.Forms.Label()
            Me.pnlPillarDivider = New System.Windows.Forms.Panel()
            Me.lblAppTitle = New System.Windows.Forms.Label()
            Me.lblAppDesc = New System.Windows.Forms.Label()
            Me.pnlBadgeArch = New System.Windows.Forms.Panel()
            Me.lblBadgeArch = New System.Windows.Forms.Label()
            Me.lblRelease = New System.Windows.Forms.Label()
            Me.pnlConsole = New System.Windows.Forms.Panel()
            Me.lblConsoleHeader = New System.Windows.Forms.Label()
            Me.lblConsoleSub = New System.Windows.Forms.Label()
            Me.pnlStepperCard = New System.Windows.Forms.Panel()
            Me.lblStep1Icon = New System.Windows.Forms.Label()
            Me.lblStep1Title = New System.Windows.Forms.Label()
            Me.lblStep1Detail = New System.Windows.Forms.Label()
            Me.lblStep2Icon = New System.Windows.Forms.Label()
            Me.lblStep2Title = New System.Windows.Forms.Label()
            Me.lblStep2Detail = New System.Windows.Forms.Label()
            Me.lblStep3Icon = New System.Windows.Forms.Label()
            Me.lblStep3Title = New System.Windows.Forms.Label()
            Me.lblStep3Detail = New System.Windows.Forms.Label()
            Me.lblStep4Icon = New System.Windows.Forms.Label()
            Me.lblStep4Title = New System.Windows.Forms.Label()
            Me.lblStep4Detail = New System.Windows.Forms.Label()
            Me.lblStep5Icon = New System.Windows.Forms.Label()
            Me.lblStep5Title = New System.Windows.Forms.Label()
            Me.lblStep5Detail = New System.Windows.Forms.Label()
            Me.lblProgressTitle = New System.Windows.Forms.Label()
            Me.lblPercent = New System.Windows.Forms.Label()
            Me.pnlProgressTrack = New System.Windows.Forms.Panel()
            Me.pnlProgressBar = New System.Windows.Forms.Panel()
            Me.lblStatusTicker = New System.Windows.Forms.Label()
            Me.btnRetry = New System.Windows.Forms.Button()
            Me.btnExit = New System.Windows.Forms.Button()
            Me.lblSecurityNotice = New System.Windows.Forms.Label()
            Me.tmrAnimation = New System.Windows.Forms.Timer(Me.components)
            Me.pnlBrandPillar.SuspendLayout()
            CType(Me.picCrest, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlBadgeArch.SuspendLayout()
            Me.pnlConsole.SuspendLayout()
            Me.pnlStepperCard.SuspendLayout()
            Me.pnlProgressTrack.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlTopGoldAccent
            '
            Me.pnlTopGoldAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
            Me.pnlTopGoldAccent.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopGoldAccent.Location = New System.Drawing.Point(0, 0)
            Me.pnlTopGoldAccent.Name = "pnlTopGoldAccent"
            Me.pnlTopGoldAccent.Size = New System.Drawing.Size(840, 3)
            Me.pnlTopGoldAccent.TabIndex = 0
            '
            'pnlBrandPillar
            '
            Me.pnlBrandPillar.BackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(44, Byte), Integer))
            Me.pnlBrandPillar.Controls.Add(Me.lblRelease)
            Me.pnlBrandPillar.Controls.Add(Me.pnlBadgeArch)
            Me.pnlBrandPillar.Controls.Add(Me.lblAppDesc)
            Me.pnlBrandPillar.Controls.Add(Me.lblAppTitle)
            Me.pnlBrandPillar.Controls.Add(Me.pnlPillarDivider)
            Me.pnlBrandPillar.Controls.Add(Me.lblDirectorate)
            Me.pnlBrandPillar.Controls.Add(Me.lblUnivName)
            Me.pnlBrandPillar.Controls.Add(Me.picCrest)
            Me.pnlBrandPillar.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlBrandPillar.Location = New System.Drawing.Point(0, 3)
            Me.pnlBrandPillar.Name = "pnlBrandPillar"
            Me.pnlBrandPillar.Size = New System.Drawing.Size(320, 497)
            Me.pnlBrandPillar.TabIndex = 1
            '
            'picCrest
            '
            Me.picCrest.BackColor = System.Drawing.Color.Transparent
            Me.picCrest.Location = New System.Drawing.Point(110, 28)
            Me.picCrest.Name = "picCrest"
            Me.picCrest.Size = New System.Drawing.Size(100, 110)
            Me.picCrest.TabIndex = 0
            Me.picCrest.TabStop = False
            '
            'lblUnivName
            '
            Me.lblUnivName.BackColor = System.Drawing.Color.Transparent
            Me.lblUnivName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblUnivName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
            Me.lblUnivName.Location = New System.Drawing.Point(20, 146)
            Me.lblUnivName.Name = "lblUnivName"
            Me.lblUnivName.Size = New System.Drawing.Size(280, 20)
            Me.lblUnivName.TabIndex = 1
            Me.lblUnivName.Text = "KENYA STATE UNIVERSITY"
            Me.lblUnivName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblUnivName.UseMnemonic = False
            '
            'lblDirectorate
            '
            Me.lblDirectorate.BackColor = System.Drawing.Color.Transparent
            Me.lblDirectorate.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblDirectorate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblDirectorate.Location = New System.Drawing.Point(20, 168)
            Me.lblDirectorate.Name = "lblDirectorate"
            Me.lblDirectorate.Size = New System.Drawing.Size(280, 18)
            Me.lblDirectorate.TabIndex = 2
            Me.lblDirectorate.Text = "DIRECTORATE OF RESIDENTIAL SERVICES"
            Me.lblDirectorate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblDirectorate.UseMnemonic = False
            '
            'pnlPillarDivider
            '
            Me.pnlPillarDivider.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(130, Byte), Integer))
            Me.pnlPillarDivider.Location = New System.Drawing.Point(36, 196)
            Me.pnlPillarDivider.Name = "pnlPillarDivider"
            Me.pnlPillarDivider.Size = New System.Drawing.Size(248, 1)
            Me.pnlPillarDivider.TabIndex = 3
            '
            'lblAppTitle
            '
            Me.lblAppTitle.BackColor = System.Drawing.Color.Transparent
            Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblAppTitle.ForeColor = System.Drawing.Color.White
            Me.lblAppTitle.Location = New System.Drawing.Point(20, 210)
            Me.lblAppTitle.Name = "lblAppTitle"
            Me.lblAppTitle.Size = New System.Drawing.Size(280, 48)
            Me.lblAppTitle.TabIndex = 4
            Me.lblAppTitle.Text = "Hostel & Bed Allocation" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Management System"
            Me.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblAppTitle.UseMnemonic = False
            '
            'lblAppDesc
            '
            Me.lblAppDesc.BackColor = System.Drawing.Color.Transparent
            Me.lblAppDesc.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblAppDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.lblAppDesc.Location = New System.Drawing.Point(25, 268)
            Me.lblAppDesc.Name = "lblAppDesc"
            Me.lblAppDesc.Size = New System.Drawing.Size(270, 56)
            Me.lblAppDesc.TabIndex = 5
            Me.lblAppDesc.Text = "Enterprise student accommodation management, individual bed rates, semester bill" &
    "ing, and audit controls."
            Me.lblAppDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblAppDesc.UseMnemonic = False
            '
            'pnlBadgeArch
            '
            Me.pnlBadgeArch.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(75, Byte), Integer))
            Me.pnlBadgeArch.Controls.Add(Me.lblBadgeArch)
            Me.pnlBadgeArch.Location = New System.Drawing.Point(30, 350)
            Me.pnlBadgeArch.Name = "pnlBadgeArch"
            Me.pnlBadgeArch.Size = New System.Drawing.Size(260, 32)
            Me.pnlBadgeArch.TabIndex = 6
            '
            'lblBadgeArch
            '
            Me.lblBadgeArch.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblBadgeArch.Font = New System.Drawing.Font("Consolas", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblBadgeArch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.lblBadgeArch.Location = New System.Drawing.Point(0, 0)
            Me.lblBadgeArch.Name = "lblBadgeArch"
            Me.lblBadgeArch.Size = New System.Drawing.Size(260, 32)
            Me.lblBadgeArch.TabIndex = 0
            Me.lblBadgeArch.Text = "ADO.NET • SQL SERVER 2025"
            Me.lblBadgeArch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblRelease
            '
            Me.lblRelease.BackColor = System.Drawing.Color.Transparent
            Me.lblRelease.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblRelease.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblRelease.Location = New System.Drawing.Point(20, 460)
            Me.lblRelease.Name = "lblRelease"
            Me.lblRelease.Size = New System.Drawing.Size(280, 18)
            Me.lblRelease.TabIndex = 7
            Me.lblRelease.Text = "Enterprise Edition • v2.0.26 (Build 2026.11)"
            Me.lblRelease.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlConsole
            '
            Me.pnlConsole.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
            Me.pnlConsole.Controls.Add(Me.lblSecurityNotice)
            Me.pnlConsole.Controls.Add(Me.btnExit)
            Me.pnlConsole.Controls.Add(Me.btnRetry)
            Me.pnlConsole.Controls.Add(Me.lblStatusTicker)
            Me.pnlConsole.Controls.Add(Me.pnlProgressTrack)
            Me.pnlConsole.Controls.Add(Me.lblPercent)
            Me.pnlConsole.Controls.Add(Me.lblProgressTitle)
            Me.pnlConsole.Controls.Add(Me.pnlStepperCard)
            Me.pnlConsole.Controls.Add(Me.lblConsoleSub)
            Me.pnlConsole.Controls.Add(Me.lblConsoleHeader)
            Me.pnlConsole.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlConsole.Location = New System.Drawing.Point(320, 3)
            Me.pnlConsole.Name = "pnlConsole"
            Me.pnlConsole.Padding = New System.Windows.Forms.Padding(32, 24, 32, 20)
            Me.pnlConsole.Size = New System.Drawing.Size(520, 497)
            Me.pnlConsole.TabIndex = 2
            '
            'lblConsoleHeader
            '
            Me.lblConsoleHeader.AutoSize = True
            Me.lblConsoleHeader.Font = New System.Drawing.Font("Segoe UI", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblConsoleHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblConsoleHeader.Location = New System.Drawing.Point(32, 22)
            Me.lblConsoleHeader.Name = "lblConsoleHeader"
            Me.lblConsoleHeader.Size = New System.Drawing.Size(298, 25)
            Me.lblConsoleHeader.TabIndex = 0
            Me.lblConsoleHeader.Text = "System Diagnostics & Initialization"
            Me.lblConsoleHeader.UseMnemonic = False
            '
            'lblConsoleSub
            '
            Me.lblConsoleSub.AutoSize = True
            Me.lblConsoleSub.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblConsoleSub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblConsoleSub.Location = New System.Drawing.Point(32, 49)
            Me.lblConsoleSub.Name = "lblConsoleSub"
            Me.lblConsoleSub.Size = New System.Drawing.Size(434, 15)
            Me.lblConsoleSub.TabIndex = 1
            Me.lblConsoleSub.Text = "Validating relational database engine, active semester, and cryptographic vault."
            '
            'pnlStepperCard
            '
            Me.pnlStepperCard.BackColor = System.Drawing.Color.White
            Me.pnlStepperCard.Controls.Add(Me.lblStep5Detail)
            Me.pnlStepperCard.Controls.Add(Me.lblStep5Title)
            Me.pnlStepperCard.Controls.Add(Me.lblStep5Icon)
            Me.pnlStepperCard.Controls.Add(Me.lblStep4Detail)
            Me.pnlStepperCard.Controls.Add(Me.lblStep4Title)
            Me.pnlStepperCard.Controls.Add(Me.lblStep4Icon)
            Me.pnlStepperCard.Controls.Add(Me.lblStep3Detail)
            Me.pnlStepperCard.Controls.Add(Me.lblStep3Title)
            Me.pnlStepperCard.Controls.Add(Me.lblStep3Icon)
            Me.pnlStepperCard.Controls.Add(Me.lblStep2Detail)
            Me.pnlStepperCard.Controls.Add(Me.lblStep2Title)
            Me.pnlStepperCard.Controls.Add(Me.lblStep2Icon)
            Me.pnlStepperCard.Controls.Add(Me.lblStep1Detail)
            Me.pnlStepperCard.Controls.Add(Me.lblStep1Title)
            Me.pnlStepperCard.Controls.Add(Me.lblStep1Icon)
            Me.pnlStepperCard.Location = New System.Drawing.Point(32, 76)
            Me.pnlStepperCard.Name = "pnlStepperCard"
            Me.pnlStepperCard.Padding = New System.Windows.Forms.Padding(16, 12, 16, 12)
            Me.pnlStepperCard.Size = New System.Drawing.Size(456, 224)
            Me.pnlStepperCard.TabIndex = 2
            '
            'lblStep1Icon
            '
            Me.lblStep1Icon.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep1Icon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblStep1Icon.Location = New System.Drawing.Point(12, 12)
            Me.lblStep1Icon.Name = "lblStep1Icon"
            Me.lblStep1Icon.Size = New System.Drawing.Size(24, 24)
            Me.lblStep1Icon.TabIndex = 0
            Me.lblStep1Icon.Text = "○"
            Me.lblStep1Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblStep1Title
            '
            Me.lblStep1Title.AutoSize = True
            Me.lblStep1Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep1Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStep1Title.Location = New System.Drawing.Point(38, 11)
            Me.lblStep1Title.Name = "lblStep1Title"
            Me.lblStep1Title.Size = New System.Drawing.Size(161, 15)
            Me.lblStep1Title.TabIndex = 1
            Me.lblStep1Title.Text = "Configuration & Environment"
            Me.lblStep1Title.UseMnemonic = False
            '
            'lblStep1Detail
            '
            Me.lblStep1Detail.Font = New System.Drawing.Font("Consolas", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStep1Detail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblStep1Detail.Location = New System.Drawing.Point(38, 27)
            Me.lblStep1Detail.Name = "lblStep1Detail"
            Me.lblStep1Detail.Size = New System.Drawing.Size(400, 16)
            Me.lblStep1Detail.TabIndex = 2
            Me.lblStep1Detail.Text = "Validating .env parameters..."
            Me.lblStep1Detail.UseMnemonic = False
            '
            'lblStep2Icon
            '
            Me.lblStep2Icon.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep2Icon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblStep2Icon.Location = New System.Drawing.Point(12, 52)
            Me.lblStep2Icon.Name = "lblStep2Icon"
            Me.lblStep2Icon.Size = New System.Drawing.Size(24, 24)
            Me.lblStep2Icon.TabIndex = 3
            Me.lblStep2Icon.Text = "○"
            Me.lblStep2Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblStep2Title
            '
            Me.lblStep2Title.AutoSize = True
            Me.lblStep2Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep2Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStep2Title.Location = New System.Drawing.Point(38, 51)
            Me.lblStep2Title.Name = "lblStep2Title"
            Me.lblStep2Title.Size = New System.Drawing.Size(155, 15)
            Me.lblStep2Title.TabIndex = 4
            Me.lblStep2Title.Text = "ADO.NET Database Pipeline"
            '
            'lblStep2Detail
            '
            Me.lblStep2Detail.Font = New System.Drawing.Font("Consolas", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStep2Detail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblStep2Detail.Location = New System.Drawing.Point(38, 67)
            Me.lblStep2Detail.Name = "lblStep2Detail"
            Me.lblStep2Detail.Size = New System.Drawing.Size(400, 16)
            Me.lblStep2Detail.TabIndex = 5
            Me.lblStep2Detail.Text = "Pending connection..."
            '
            'lblStep3Icon
            '
            Me.lblStep3Icon.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep3Icon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblStep3Icon.Location = New System.Drawing.Point(12, 92)
            Me.lblStep3Icon.Name = "lblStep3Icon"
            Me.lblStep3Icon.Size = New System.Drawing.Size(24, 24)
            Me.lblStep3Icon.TabIndex = 6
            Me.lblStep3Icon.Text = "○"
            Me.lblStep3Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblStep3Title
            '
            Me.lblStep3Title.AutoSize = True
            Me.lblStep3Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep3Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStep3Title.Location = New System.Drawing.Point(38, 91)
            Me.lblStep3Title.Name = "lblStep3Title"
            Me.lblStep3Title.Size = New System.Drawing.Size(146, 15)
            Me.lblStep3Title.TabIndex = 7
            Me.lblStep3Title.Text = "Active Academic Semester"
            '
            'lblStep3Detail
            '
            Me.lblStep3Detail.Font = New System.Drawing.Font("Consolas", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStep3Detail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblStep3Detail.Location = New System.Drawing.Point(38, 107)
            Me.lblStep3Detail.Name = "lblStep3Detail"
            Me.lblStep3Detail.Size = New System.Drawing.Size(400, 16)
            Me.lblStep3Detail.TabIndex = 8
            Me.lblStep3Detail.Text = "Waiting for database..."
            '
            'lblStep4Icon
            '
            Me.lblStep4Icon.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep4Icon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblStep4Icon.Location = New System.Drawing.Point(12, 132)
            Me.lblStep4Icon.Name = "lblStep4Icon"
            Me.lblStep4Icon.Size = New System.Drawing.Size(24, 24)
            Me.lblStep4Icon.TabIndex = 9
            Me.lblStep4Icon.Text = "○"
            Me.lblStep4Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblStep4Title
            '
            Me.lblStep4Title.AutoSize = True
            Me.lblStep4Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep4Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStep4Title.Location = New System.Drawing.Point(38, 131)
            Me.lblStep4Title.Name = "lblStep4Title"
            Me.lblStep4Title.Size = New System.Drawing.Size(199, 15)
            Me.lblStep4Title.TabIndex = 10
            Me.lblStep4Title.Text = "Room Gallery & Bed Asset Directorate"
            Me.lblStep4Title.UseMnemonic = False
            '
            'lblStep4Detail
            '
            Me.lblStep4Detail.Font = New System.Drawing.Font("Consolas", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStep4Detail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblStep4Detail.Location = New System.Drawing.Point(38, 147)
            Me.lblStep4Detail.Name = "lblStep4Detail"
            Me.lblStep4Detail.Size = New System.Drawing.Size(400, 16)
            Me.lblStep4Detail.TabIndex = 11
            Me.lblStep4Detail.Text = "Pending..."
            Me.lblStep4Detail.UseMnemonic = False
            '
            'lblStep5Icon
            '
            Me.lblStep5Icon.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep5Icon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblStep5Icon.Location = New System.Drawing.Point(12, 172)
            Me.lblStep5Icon.Name = "lblStep5Icon"
            Me.lblStep5Icon.Size = New System.Drawing.Size(24, 24)
            Me.lblStep5Icon.TabIndex = 12
            Me.lblStep5Icon.Text = "○"
            Me.lblStep5Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblStep5Title
            '
            Me.lblStep5Title.AutoSize = True
            Me.lblStep5Title.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblStep5Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
            Me.lblStep5Title.Location = New System.Drawing.Point(38, 171)
            Me.lblStep5Title.Name = "lblStep5Title"
            Me.lblStep5Title.Size = New System.Drawing.Size(239, 15)
            Me.lblStep5Title.TabIndex = 13
            Me.lblStep5Title.Text = "Cryptographic Security & Audit Subsystem"
            Me.lblStep5Title.UseMnemonic = False
            '
            'lblStep5Detail
            '
            Me.lblStep5Detail.Font = New System.Drawing.Font("Consolas", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStep5Detail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblStep5Detail.Location = New System.Drawing.Point(38, 187)
            Me.lblStep5Detail.Name = "lblStep5Detail"
            Me.lblStep5Detail.Size = New System.Drawing.Size(400, 16)
            Me.lblStep5Detail.TabIndex = 14
            Me.lblStep5Detail.Text = "Pending..."
            Me.lblStep5Detail.UseMnemonic = False
            '
            'lblProgressTitle
            '
            Me.lblProgressTitle.AutoSize = True
            Me.lblProgressTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblProgressTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.lblProgressTitle.Location = New System.Drawing.Point(32, 318)
            Me.lblProgressTitle.Name = "lblProgressTitle"
            Me.lblProgressTitle.Size = New System.Drawing.Size(91, 13)
            Me.lblProgressTitle.TabIndex = 3
            Me.lblProgressTitle.Text = "INITIALIZATION"
            '
            'lblPercent
            '
            Me.lblPercent.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.lblPercent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.lblPercent.Location = New System.Drawing.Point(408, 314)
            Me.lblPercent.Name = "lblPercent"
            Me.lblPercent.Size = New System.Drawing.Size(80, 20)
            Me.lblPercent.TabIndex = 4
            Me.lblPercent.Text = "0%"
            Me.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'pnlProgressTrack
            '
            Me.pnlProgressTrack.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
            Me.pnlProgressTrack.Controls.Add(Me.pnlProgressBar)
            Me.pnlProgressTrack.Location = New System.Drawing.Point(32, 338)
            Me.pnlProgressTrack.Name = "pnlProgressTrack"
            Me.pnlProgressTrack.Size = New System.Drawing.Size(456, 8)
            Me.pnlProgressTrack.TabIndex = 5
            '
            'pnlProgressBar
            '
            Me.pnlProgressBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.pnlProgressBar.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlProgressBar.Location = New System.Drawing.Point(0, 0)
            Me.pnlProgressBar.Name = "pnlProgressBar"
            Me.pnlProgressBar.Size = New System.Drawing.Size(40, 8)
            Me.pnlProgressBar.TabIndex = 0
            '
            'lblStatusTicker
            '
            Me.lblStatusTicker.Font = New System.Drawing.Font("Consolas", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblStatusTicker.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
            Me.lblStatusTicker.Location = New System.Drawing.Point(32, 354)
            Me.lblStatusTicker.Name = "lblStatusTicker"
            Me.lblStatusTicker.Size = New System.Drawing.Size(456, 36)
            Me.lblStatusTicker.TabIndex = 6
            Me.lblStatusTicker.Text = "Booting system kernel..."
            '
            'btnRetry
            '
            Me.btnRetry.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(98, Byte), Integer))
            Me.btnRetry.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnRetry.FlatAppearance.BorderSize = 0
            Me.btnRetry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnRetry.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.btnRetry.ForeColor = System.Drawing.Color.White
            Me.btnRetry.Location = New System.Drawing.Point(282, 396)
            Me.btnRetry.Name = "btnRetry"
            Me.btnRetry.Size = New System.Drawing.Size(100, 32)
            Me.btnRetry.TabIndex = 7
            Me.btnRetry.Text = "Retry"
            Me.btnRetry.UseVisualStyleBackColor = False
            Me.btnRetry.Visible = False
            '
            'btnExit
            '
            Me.btnExit.BackColor = System.Drawing.Color.White
            Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.btnExit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
            Me.btnExit.Location = New System.Drawing.Point(388, 396)
            Me.btnExit.Name = "btnExit"
            Me.btnExit.Size = New System.Drawing.Size(100, 32)
            Me.btnExit.TabIndex = 8
            Me.btnExit.Text = "Exit"
            Me.btnExit.UseVisualStyleBackColor = False
            Me.btnExit.Visible = False
            '
            'lblSecurityNotice
            '
            Me.lblSecurityNotice.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.lblSecurityNotice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
            Me.lblSecurityNotice.Location = New System.Drawing.Point(32, 460)
            Me.lblSecurityNotice.Name = "lblSecurityNotice"
            Me.lblSecurityNotice.Size = New System.Drawing.Size(456, 18)
            Me.lblSecurityNotice.TabIndex = 9
            Me.lblSecurityNotice.Text = "🔒 Authorized Accommodation Officers & Finance Staff Only • Activity Audited"
            Me.lblSecurityNotice.UseMnemonic = False
            '
            'tmrAnimation
            '
            Me.tmrAnimation.Interval = 20
            '
            'frmSplash
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(840, 500)
            Me.Controls.Add(Me.pnlConsole)
            Me.Controls.Add(Me.pnlBrandPillar)
            Me.Controls.Add(Me.pnlTopGoldAccent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "frmSplash"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "University Hostel Management System"
            Me.pnlBrandPillar.ResumeLayout(False)
            CType(Me.picCrest, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlBadgeArch.ResumeLayout(False)
            Me.pnlConsole.ResumeLayout(False)
            Me.pnlConsole.PerformLayout()
            Me.pnlStepperCard.ResumeLayout(False)
            Me.pnlStepperCard.PerformLayout()
            Me.pnlProgressTrack.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTopGoldAccent As System.Windows.Forms.Panel
        Friend WithEvents pnlBrandPillar As System.Windows.Forms.Panel
        Friend WithEvents picCrest As System.Windows.Forms.PictureBox
        Friend WithEvents lblUnivName As System.Windows.Forms.Label
        Friend WithEvents lblDirectorate As System.Windows.Forms.Label
        Friend WithEvents pnlPillarDivider As System.Windows.Forms.Panel
        Friend WithEvents lblAppTitle As System.Windows.Forms.Label
        Friend WithEvents lblAppDesc As System.Windows.Forms.Label
        Friend WithEvents pnlBadgeArch As System.Windows.Forms.Panel
        Friend WithEvents lblBadgeArch As System.Windows.Forms.Label
        Friend WithEvents lblRelease As System.Windows.Forms.Label
        Friend WithEvents pnlConsole As System.Windows.Forms.Panel
        Friend WithEvents lblConsoleHeader As System.Windows.Forms.Label
        Friend WithEvents lblConsoleSub As System.Windows.Forms.Label
        Friend WithEvents pnlStepperCard As System.Windows.Forms.Panel
        Friend WithEvents lblStep1Icon As System.Windows.Forms.Label
        Friend WithEvents lblStep1Title As System.Windows.Forms.Label
        Friend WithEvents lblStep1Detail As System.Windows.Forms.Label
        Friend WithEvents lblStep2Icon As System.Windows.Forms.Label
        Friend WithEvents lblStep2Title As System.Windows.Forms.Label
        Friend WithEvents lblStep2Detail As System.Windows.Forms.Label
        Friend WithEvents lblStep3Icon As System.Windows.Forms.Label
        Friend WithEvents lblStep3Title As System.Windows.Forms.Label
        Friend WithEvents lblStep3Detail As System.Windows.Forms.Label
        Friend WithEvents lblStep4Icon As System.Windows.Forms.Label
        Friend WithEvents lblStep4Title As System.Windows.Forms.Label
        Friend WithEvents lblStep4Detail As System.Windows.Forms.Label
        Friend WithEvents lblStep5Icon As System.Windows.Forms.Label
        Friend WithEvents lblStep5Title As System.Windows.Forms.Label
        Friend WithEvents lblStep5Detail As System.Windows.Forms.Label
        Friend WithEvents lblProgressTitle As System.Windows.Forms.Label
        Friend WithEvents lblPercent As System.Windows.Forms.Label
        Friend WithEvents pnlProgressTrack As System.Windows.Forms.Panel
        Friend WithEvents pnlProgressBar As System.Windows.Forms.Panel
        Friend WithEvents lblStatusTicker As System.Windows.Forms.Label
        Friend WithEvents btnRetry As System.Windows.Forms.Button
        Friend WithEvents btnExit As System.Windows.Forms.Button
        Friend WithEvents lblSecurityNotice As System.Windows.Forms.Label
        Friend WithEvents tmrAnimation As System.Windows.Forms.Timer
    End Class
End Namespace