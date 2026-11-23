-- =====================================================================
-- UNIVERSITY HOSTEL MANAGEMENT SYSTEM
-- MASTER DATABASE CREATION SCRIPT (SQL Server 2022 / 2025)
-- Database Name: university_hostel_db
-- =====================================================================

SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

-- 1. DATABASE CREATION
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'university_hostel_db')
BEGIN
    PRINT 'Creating database university_hostel_db...';
    CREATE DATABASE university_hostel_db
    COLLATE Latin1_General_100_CI_AS_SC_UTF8;
END
GO

USE university_hostel_db;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- =====================================================================
-- 2. APPLICATION ROLE & USER SETUP (Least Privilege Principle)
-- =====================================================================
-- Create application SQL login if mixed mode authentication is enabled.
IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = N'hostel_app_user')
BEGIN
    BEGIN TRY
        PRINT 'Creating SQL login hostel_app_user...';
        CREATE LOGIN hostel_app_user WITH PASSWORD = N'HostelApp#Secure2026!', CHECK_POLICY = ON;
    END TRY
    BEGIN CATCH
        PRINT 'Notice: Could not create SQL login hostel_app_user (Server may be in Windows Auth only mode). Integrated security will be used.';
    END CATCH
END
GO

IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = N'hostel_app_user')
BEGIN
    BEGIN TRY
        PRINT 'Creating database user hostel_app_user...';
        CREATE USER hostel_app_user FOR LOGIN hostel_app_user;
        -- Grant least privileges (DML only, no DDL/DROP)
        ALTER ROLE db_datareader ADD MEMBER hostel_app_user;
        ALTER ROLE db_datawriter ADD MEMBER hostel_app_user;
        GRANT EXECUTE TO hostel_app_user;
    END TRY
    BEGIN CATCH
        PRINT 'Notice: Skipping SQL user role assignment (Windows integrated auth in use).';
    END CATCH
END
GO

-- =====================================================================
-- 3. SCHEMA DDL: CORE TABLES
-- =====================================================================

-- 3.1 ROLES
IF OBJECT_ID(N'dbo.roles', N'U') IS NULL
BEGIN
    PRINT 'Creating table: roles...';
    CREATE TABLE dbo.roles (
        role_id INT IDENTITY(1,1) NOT NULL,
        role_name NVARCHAR(50) NOT NULL,
        description NVARCHAR(255) NULL,
        created_at DATETIME2 NOT NULL CONSTRAINT DF_roles_created_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_roles PRIMARY KEY CLUSTERED (role_id),
        CONSTRAINT UQ_roles_name UNIQUE NONCLUSTERED (role_name)
    );
END
GO

-- 3.2 USERS
IF OBJECT_ID(N'dbo.users', N'U') IS NULL
BEGIN
    PRINT 'Creating table: users...';
    CREATE TABLE dbo.users (
        user_id INT IDENTITY(1,1) NOT NULL,
        full_name NVARCHAR(150) NOT NULL,
        username NVARCHAR(50) NOT NULL,
        email NVARCHAR(100) NOT NULL,
        password_hash NVARCHAR(255) NOT NULL,
        role_id INT NOT NULL,
        account_status NVARCHAR(20) NOT NULL CONSTRAINT DF_users_account_status DEFAULT N'ACTIVE',
        created_at DATETIME2 NOT NULL CONSTRAINT DF_users_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2 NOT NULL CONSTRAINT DF_users_updated_at DEFAULT SYSUTCDATETIME(),
        last_login DATETIME2 NULL,
        CONSTRAINT PK_users PRIMARY KEY CLUSTERED (user_id),
        CONSTRAINT UQ_users_username UNIQUE NONCLUSTERED (username),
        CONSTRAINT UQ_users_email UNIQUE NONCLUSTERED (email),
        CONSTRAINT FK_users_role FOREIGN KEY (role_id) REFERENCES dbo.roles(role_id) ON DELETE NO ACTION,
        CONSTRAINT CK_users_account_status CHECK (account_status IN (N'ACTIVE', N'INACTIVE', N'SUSPENDED'))
    );
    CREATE NONCLUSTERED INDEX IX_users_role_id ON dbo.users(role_id);
END
GO

-- 3.3 SEMESTERS
IF OBJECT_ID(N'dbo.semesters', N'U') IS NULL
BEGIN
    PRINT 'Creating table: semesters...';
    CREATE TABLE dbo.semesters (
        semester_id INT IDENTITY(1,1) NOT NULL,
        semester_name NVARCHAR(100) NOT NULL,
        semester_code NVARCHAR(50) NOT NULL,
        academic_year NVARCHAR(20) NOT NULL,
        start_date DATE NOT NULL,
        end_date DATE NOT NULL,
        allocation_open_date DATE NOT NULL,
        allocation_close_date DATE NOT NULL,
        status NVARCHAR(20) NOT NULL CONSTRAINT DF_semesters_status DEFAULT N'UPCOMING',
        created_at DATETIME2 NOT NULL CONSTRAINT DF_semesters_created_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_semesters PRIMARY KEY CLUSTERED (semester_id),
        CONSTRAINT UQ_semesters_code UNIQUE NONCLUSTERED (semester_code),
        CONSTRAINT CK_semesters_status CHECK (status IN (N'UPCOMING', N'ACTIVE', N'COMPLETED', N'CLOSED')),
        CONSTRAINT CK_semesters_dates CHECK (end_date >= start_date AND allocation_close_date >= allocation_open_date)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UQ_semesters_single_active')
BEGIN
    SET ANSI_NULLS ON;
    SET QUOTED_IDENTIFIER ON;
    CREATE UNIQUE NONCLUSTERED INDEX UQ_semesters_single_active ON dbo.semesters(status) WHERE status = N'ACTIVE';
END
GO

-- 3.4 HOSTELS
IF OBJECT_ID(N'dbo.hostels', N'U') IS NULL
BEGIN
    PRINT 'Creating table: hostels...';
    CREATE TABLE dbo.hostels (
        hostel_id INT IDENTITY(1,1) NOT NULL,
        hostel_name NVARCHAR(100) NOT NULL,
        hostel_code NVARCHAR(20) NOT NULL,
        location NVARCHAR(150) NOT NULL,
        description NVARCHAR(MAX) NULL,
        gender_category NVARCHAR(20) NOT NULL,
        total_floors INT NOT NULL CONSTRAINT DF_hostels_total_floors DEFAULT 1,
        status NVARCHAR(20) NOT NULL CONSTRAINT DF_hostels_status DEFAULT N'ACTIVE',
        created_at DATETIME2 NOT NULL CONSTRAINT DF_hostels_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2 NOT NULL CONSTRAINT DF_hostels_updated_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_hostels PRIMARY KEY CLUSTERED (hostel_id),
        CONSTRAINT UQ_hostels_name UNIQUE NONCLUSTERED (hostel_name),
        CONSTRAINT UQ_hostels_code UNIQUE NONCLUSTERED (hostel_code),
        CONSTRAINT CK_hostels_gender CHECK (gender_category IN (N'MALE', N'FEMALE', N'MIXED')),
        CONSTRAINT CK_hostels_status CHECK (status IN (N'ACTIVE', N'INACTIVE', N'MAINTENANCE')),
        CONSTRAINT CK_hostels_floors CHECK (total_floors >= 1)
    );
END
GO

-- 3.5 ROOMS
IF OBJECT_ID(N'dbo.rooms', N'U') IS NULL
BEGIN
    PRINT 'Creating table: rooms...';
    CREATE TABLE dbo.rooms (
        room_id INT IDENTITY(1,1) NOT NULL,
        hostel_id INT NOT NULL,
        room_number NVARCHAR(20) NOT NULL,
        floor_number INT NOT NULL CONSTRAINT DF_rooms_floor_number DEFAULT 1,
        room_type NVARCHAR(50) NOT NULL CONSTRAINT DF_rooms_room_type DEFAULT N'STANDARD',
        capacity INT NOT NULL CONSTRAINT DF_rooms_capacity DEFAULT 1,
        description NVARCHAR(MAX) NULL,
        status NVARCHAR(20) NOT NULL CONSTRAINT DF_rooms_status DEFAULT N'AVAILABLE',
        created_at DATETIME2 NOT NULL CONSTRAINT DF_rooms_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2 NOT NULL CONSTRAINT DF_rooms_updated_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_rooms PRIMARY KEY CLUSTERED (room_id),
        CONSTRAINT UQ_rooms_hostel_room UNIQUE NONCLUSTERED (hostel_id, room_number),
        CONSTRAINT FK_rooms_hostel FOREIGN KEY (hostel_id) REFERENCES dbo.hostels(hostel_id) ON DELETE NO ACTION,
        CONSTRAINT CK_rooms_capacity CHECK (capacity >= 1),
        CONSTRAINT CK_rooms_status CHECK (status IN (N'AVAILABLE', N'OCCUPIED', N'MAINTENANCE', N'INACTIVE'))
    );
    CREATE NONCLUSTERED INDEX IX_rooms_hostel_id ON dbo.rooms(hostel_id);
    CREATE NONCLUSTERED INDEX IX_rooms_status ON dbo.rooms(status);
END
GO

-- 3.6 ROOM IMAGES
IF OBJECT_ID(N'dbo.room_images', N'U') IS NULL
BEGIN
    PRINT 'Creating table: room_images...';
    CREATE TABLE dbo.room_images (
        image_id INT IDENTITY(1,1) NOT NULL,
        room_id INT NOT NULL,
        image_path NVARCHAR(255) NOT NULL,
        image_caption NVARCHAR(255) NULL,
        image_category NVARCHAR(50) NOT NULL CONSTRAINT DF_room_images_category DEFAULT N'GENERAL',
        display_order INT NOT NULL CONSTRAINT DF_room_images_display_order DEFAULT 0,
        is_primary BIT NOT NULL CONSTRAINT DF_room_images_is_primary DEFAULT 0,
        uploaded_at DATETIME2 NOT NULL CONSTRAINT DF_room_images_uploaded_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_room_images PRIMARY KEY CLUSTERED (image_id),
        CONSTRAINT FK_room_images_room FOREIGN KEY (room_id) REFERENCES dbo.rooms(room_id) ON DELETE CASCADE
    );
    CREATE NONCLUSTERED INDEX IX_room_images_room_id ON dbo.room_images(room_id);
END
GO

-- 3.7 BEDS
IF OBJECT_ID(N'dbo.beds', N'U') IS NULL
BEGIN
    PRINT 'Creating table: beds...';
    CREATE TABLE dbo.beds (
        bed_id INT IDENTITY(1,1) NOT NULL,
        room_id INT NOT NULL,
        bed_number NVARCHAR(20) NOT NULL,
        bed_label NVARCHAR(100) NOT NULL,
        bed_type NVARCHAR(50) NOT NULL CONSTRAINT DF_beds_bed_type DEFAULT N'STANDARD',
        semester_rate DECIMAL(12,2) NOT NULL CONSTRAINT DF_beds_semester_rate DEFAULT 0.00,
        bed_position NVARCHAR(50) NULL,
        has_desk BIT NOT NULL CONSTRAINT DF_beds_has_desk DEFAULT 1,
        has_locker BIT NOT NULL CONSTRAINT DF_beds_has_locker DEFAULT 1,
        has_window BIT NOT NULL CONSTRAINT DF_beds_has_window DEFAULT 0,
        has_ventilation BIT NOT NULL CONSTRAINT DF_beds_has_ventilation DEFAULT 1,
        status NVARCHAR(20) NOT NULL CONSTRAINT DF_beds_status DEFAULT N'AVAILABLE',
        notes NVARCHAR(MAX) NULL,
        created_at DATETIME2 NOT NULL CONSTRAINT DF_beds_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2 NOT NULL CONSTRAINT DF_beds_updated_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_beds PRIMARY KEY CLUSTERED (bed_id),
        CONSTRAINT UQ_beds_room_bed UNIQUE NONCLUSTERED (room_id, bed_number),
        CONSTRAINT FK_beds_room FOREIGN KEY (room_id) REFERENCES dbo.rooms(room_id) ON DELETE NO ACTION,
        CONSTRAINT CK_beds_rate CHECK (semester_rate >= 0.00),
        CONSTRAINT CK_beds_status CHECK (status IN (N'AVAILABLE', N'OCCUPIED', N'MAINTENANCE', N'RESERVED', N'INACTIVE'))
    );
    CREATE NONCLUSTERED INDEX IX_beds_room_id ON dbo.beds(room_id);
    CREATE NONCLUSTERED INDEX IX_beds_status ON dbo.beds(status);
END
GO

-- 3.8 STUDENTS
IF OBJECT_ID(N'dbo.students', N'U') IS NULL
BEGIN
    PRINT 'Creating table: students...';
    CREATE TABLE dbo.students (
        student_id INT IDENTITY(1,1) NOT NULL,
        registration_number NVARCHAR(50) NOT NULL,
        first_name NVARCHAR(50) NOT NULL,
        middle_name NVARCHAR(50) NULL,
        last_name NVARCHAR(50) NOT NULL,
        gender NVARCHAR(20) NOT NULL,
        email NVARCHAR(100) NOT NULL,
        phone_number NVARCHAR(30) NOT NULL,
        course NVARCHAR(100) NOT NULL,
        department NVARCHAR(100) NOT NULL,
        academic_year NVARCHAR(20) NOT NULL,
        emergency_contact_name NVARCHAR(100) NOT NULL,
        emergency_contact_phone NVARCHAR(30) NOT NULL,
        student_status NVARCHAR(20) NOT NULL CONSTRAINT DF_students_status DEFAULT N'ACTIVE',
        created_at DATETIME2 NOT NULL CONSTRAINT DF_students_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2 NOT NULL CONSTRAINT DF_students_updated_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_students PRIMARY KEY CLUSTERED (student_id),
        CONSTRAINT UQ_students_reg_no UNIQUE NONCLUSTERED (registration_number),
        CONSTRAINT UQ_students_email UNIQUE NONCLUSTERED (email),
        CONSTRAINT CK_students_gender CHECK (gender IN (N'MALE', N'FEMALE', N'OTHER')),
        CONSTRAINT CK_students_status CHECK (student_status IN (N'ACTIVE', N'SUSPENDED', N'GRADUATED', N'DEFERRED'))
    );
    CREATE NONCLUSTERED INDEX IX_students_status ON dbo.students(student_status);
    CREATE NONCLUSTERED INDEX IX_students_department ON dbo.students(department);
END
GO

-- 3.9 BED ALLOCATIONS (Core Financial & Accommodation Matrix)
IF OBJECT_ID(N'dbo.bed_allocations', N'U') IS NULL
BEGIN
    PRINT 'Creating table: bed_allocations...';
    CREATE TABLE dbo.bed_allocations (
        allocation_id INT IDENTITY(1,1) NOT NULL,
        student_id INT NOT NULL,
        bed_id INT NOT NULL,
        semester_id INT NOT NULL,
        semester_fee DECIMAL(12,2) NOT NULL,
        allocation_date DATETIME2 NOT NULL CONSTRAINT DF_allocations_date DEFAULT SYSUTCDATETIME(),
        check_in_date DATETIME2 NULL,
        check_out_date DATETIME2 NULL,
        allocation_status NVARCHAR(20) NOT NULL CONSTRAINT DF_allocations_status DEFAULT N'ACTIVE',
        notes NVARCHAR(MAX) NULL,
        created_by INT NOT NULL,
        created_at DATETIME2 NOT NULL CONSTRAINT DF_allocations_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2 NOT NULL CONSTRAINT DF_allocations_updated_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_bed_allocations PRIMARY KEY CLUSTERED (allocation_id),
        CONSTRAINT FK_allocations_student FOREIGN KEY (student_id) REFERENCES dbo.students(student_id) ON DELETE NO ACTION,
        CONSTRAINT FK_allocations_bed FOREIGN KEY (bed_id) REFERENCES dbo.beds(bed_id) ON DELETE NO ACTION,
        CONSTRAINT FK_allocations_semester FOREIGN KEY (semester_id) REFERENCES dbo.semesters(semester_id) ON DELETE NO ACTION,
        CONSTRAINT FK_allocations_user FOREIGN KEY (created_by) REFERENCES dbo.users(user_id) ON DELETE NO ACTION,
        CONSTRAINT CK_allocations_fee CHECK (semester_fee >= 0.00),
        CONSTRAINT CK_allocations_status CHECK (allocation_status IN (N'ACTIVE', N'CHECKED_OUT', N'CANCELLED', N'TRANSFERRED'))
    );

    CREATE NONCLUSTERED INDEX IX_allocations_student_id ON dbo.bed_allocations(student_id);
    CREATE NONCLUSTERED INDEX IX_allocations_bed_id ON dbo.bed_allocations(bed_id);
    CREATE NONCLUSTERED INDEX IX_allocations_semester_id ON dbo.bed_allocations(semester_id);
    CREATE NONCLUSTERED INDEX IX_allocations_status ON dbo.bed_allocations(allocation_status);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UQ_active_bed_per_semester')
BEGIN
    SET ANSI_NULLS ON;
    SET QUOTED_IDENTIFIER ON;
    CREATE UNIQUE NONCLUSTERED INDEX UQ_active_bed_per_semester
        ON dbo.bed_allocations(semester_id, bed_id)
        WHERE allocation_status = N'ACTIVE';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UQ_active_student_per_semester')
BEGIN
    SET ANSI_NULLS ON;
    SET QUOTED_IDENTIFIER ON;
    CREATE UNIQUE NONCLUSTERED INDEX UQ_active_student_per_semester
        ON dbo.bed_allocations(semester_id, student_id)
        WHERE allocation_status = N'ACTIVE';
END
GO

-- 3.10 PAYMENTS
IF OBJECT_ID(N'dbo.payments', N'U') IS NULL
BEGIN
    PRINT 'Creating table: payments...';
    CREATE TABLE dbo.payments (
        payment_id INT IDENTITY(1,1) NOT NULL,
        allocation_id INT NOT NULL,
        receipt_number NVARCHAR(50) NOT NULL,
        amount_paid DECIMAL(12,2) NOT NULL,
        payment_method NVARCHAR(20) NOT NULL,
        transaction_reference NVARCHAR(100) NOT NULL,
        payment_date DATETIME2 NOT NULL CONSTRAINT DF_payments_date DEFAULT SYSUTCDATETIME(),
        recorded_by INT NOT NULL,
        payment_status NVARCHAR(20) NOT NULL CONSTRAINT DF_payments_status DEFAULT N'CONFIRMED',
        notes NVARCHAR(MAX) NULL,
        created_at DATETIME2 NOT NULL CONSTRAINT DF_payments_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2 NOT NULL CONSTRAINT DF_payments_updated_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_payments PRIMARY KEY CLUSTERED (payment_id),
        CONSTRAINT UQ_payments_receipt UNIQUE NONCLUSTERED (receipt_number),
        CONSTRAINT UQ_payments_tx_ref UNIQUE NONCLUSTERED (transaction_reference),
        CONSTRAINT FK_payments_allocation FOREIGN KEY (allocation_id) REFERENCES dbo.bed_allocations(allocation_id) ON DELETE NO ACTION,
        CONSTRAINT FK_payments_user FOREIGN KEY (recorded_by) REFERENCES dbo.users(user_id) ON DELETE NO ACTION,
        CONSTRAINT CK_payments_amount CHECK (amount_paid > 0.00),
        CONSTRAINT CK_payments_method CHECK (payment_method IN (N'MPESA', N'BANK', N'CASH', N'OTHER')),
        CONSTRAINT CK_payments_status CHECK (payment_status IN (N'CONFIRMED', N'REVERSED', N'ADJUSTED'))
    );
    CREATE NONCLUSTERED INDEX IX_payments_allocation_id ON dbo.payments(allocation_id);
    CREATE NONCLUSTERED INDEX IX_payments_date ON dbo.payments(payment_date);
    CREATE NONCLUSTERED INDEX IX_payments_method ON dbo.payments(payment_method);
    CREATE NONCLUSTERED INDEX IX_payments_status ON dbo.payments(payment_status);
END
GO

-- 3.11 MAINTENANCE RECORDS
IF OBJECT_ID(N'dbo.maintenance_records', N'U') IS NULL
BEGIN
    PRINT 'Creating table: maintenance_records...';
    CREATE TABLE dbo.maintenance_records (
        maintenance_id INT IDENTITY(1,1) NOT NULL,
        bed_id INT NULL,
        room_id INT NOT NULL,
        issue_title NVARCHAR(150) NOT NULL,
        issue_description NVARCHAR(MAX) NOT NULL,
        maintenance_status NVARCHAR(20) NOT NULL CONSTRAINT DF_maintenance_status DEFAULT N'REPORTED',
        reported_by INT NOT NULL,
        assigned_to NVARCHAR(100) NULL,
        reported_date DATETIME2 NOT NULL CONSTRAINT DF_maintenance_reported_date DEFAULT SYSUTCDATETIME(),
        resolved_date DATETIME2 NULL,
        resolution_notes NVARCHAR(MAX) NULL,
        evidence_image_path NVARCHAR(255) NULL,
        created_at DATETIME2 NOT NULL CONSTRAINT DF_maintenance_created_at DEFAULT SYSUTCDATETIME(),
        updated_at DATETIME2 NOT NULL CONSTRAINT DF_maintenance_updated_at DEFAULT SYSUTCDATETIME(),
        CONSTRAINT PK_maintenance_records PRIMARY KEY CLUSTERED (maintenance_id),
        CONSTRAINT FK_maintenance_bed FOREIGN KEY (bed_id) REFERENCES dbo.beds(bed_id) ON DELETE NO ACTION,
        CONSTRAINT FK_maintenance_room FOREIGN KEY (room_id) REFERENCES dbo.rooms(room_id) ON DELETE NO ACTION,
        CONSTRAINT FK_maintenance_reporter FOREIGN KEY (reported_by) REFERENCES dbo.users(user_id) ON DELETE NO ACTION,
        CONSTRAINT CK_maintenance_status CHECK (maintenance_status IN (N'REPORTED', N'IN_PROGRESS', N'RESOLVED', N'CANCELLED'))
    );
    CREATE NONCLUSTERED INDEX IX_maintenance_room_id ON dbo.maintenance_records(room_id);
    CREATE NONCLUSTERED INDEX IX_maintenance_bed_id ON dbo.maintenance_records(bed_id);
    CREATE NONCLUSTERED INDEX IX_maintenance_status ON dbo.maintenance_records(maintenance_status);
END
GO

-- 3.12 ACTIVITY LOGS (Immutable Audit Trail)
IF OBJECT_ID(N'dbo.activity_logs', N'U') IS NULL
BEGIN
    PRINT 'Creating table: activity_logs...';
    CREATE TABLE dbo.activity_logs (
        log_id BIGINT IDENTITY(1,1) NOT NULL,
        user_id INT NULL,
        action_type NVARCHAR(50) NOT NULL,
        entity_type NVARCHAR(50) NOT NULL,
        entity_id NVARCHAR(50) NULL,
        description NVARCHAR(MAX) NOT NULL,
        action_timestamp DATETIME2 NOT NULL CONSTRAINT DF_activity_logs_timestamp DEFAULT SYSUTCDATETIME(),
        ip_address NVARCHAR(45) NULL,
        additional_data NVARCHAR(MAX) NULL,
        CONSTRAINT PK_activity_logs PRIMARY KEY CLUSTERED (log_id),
        CONSTRAINT FK_activity_logs_user FOREIGN KEY (user_id) REFERENCES dbo.users(user_id) ON DELETE SET NULL
    );
    CREATE NONCLUSTERED INDEX IX_activity_logs_user_id ON dbo.activity_logs(user_id);
    CREATE NONCLUSTERED INDEX IX_activity_logs_timestamp ON dbo.activity_logs(action_timestamp);
    CREATE NONCLUSTERED INDEX IX_activity_logs_action ON dbo.activity_logs(action_type);
    CREATE NONCLUSTERED INDEX IX_activity_logs_entity ON dbo.activity_logs(entity_type, entity_id);
END
GO

-- =====================================================================
-- 4. DATABASE INTEGRITY TRIGGERS
-- =====================================================================

-- Trigger: Prevent allocating a bed under maintenance or inactive, or to a closed semester
CREATE OR ALTER TRIGGER dbo.trg_validate_bed_allocation
ON dbo.bed_allocations
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

    -- Only validate active allocations
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN dbo.beds b ON i.bed_id = b.bed_id
        WHERE i.allocation_status = N'ACTIVE'
          AND b.status IN (N'MAINTENANCE', N'INACTIVE')
    )
    BEGIN
        RAISERROR (N'Database Integrity Violation: Cannot create or maintain an ACTIVE allocation for a bed marked as MAINTENANCE or INACTIVE.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Ensure semester is open/active for new allocations
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN dbo.semesters s ON i.semester_id = s.semester_id
        WHERE i.allocation_status = N'ACTIVE'
          AND s.status IN (N'CLOSED', N'COMPLETED')
    )
    BEGIN
        RAISERROR (N'Database Integrity Violation: Cannot allocate accommodation in a CLOSED or COMPLETED semester.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- Trigger: Automatically synchronize bed status when an active allocation is inserted or status changes
CREATE OR ALTER TRIGGER dbo.trg_sync_bed_status_on_allocation
ON dbo.bed_allocations
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

    -- 1. When an allocation becomes ACTIVE, update bed status to OCCUPIED
    UPDATE b
    SET b.status = N'OCCUPIED',
        b.updated_at = SYSUTCDATETIME()
    FROM dbo.beds b
    INNER JOIN inserted i ON b.bed_id = i.bed_id
    WHERE i.allocation_status = N'ACTIVE'
      AND b.status <> N'OCCUPIED';

    -- 2. When an allocation is checked out, cancelled, or transferred,
    -- restore bed status to AVAILABLE if no other active allocation exists and bed is not in maintenance
    UPDATE b
    SET b.status = N'AVAILABLE',
        b.updated_at = SYSUTCDATETIME()
    FROM dbo.beds b
    INNER JOIN inserted i ON b.bed_id = i.bed_id
    WHERE i.allocation_status IN (N'CHECKED_OUT', N'CANCELLED', N'TRANSFERRED')
      AND b.status = N'OCCUPIED'
      AND NOT EXISTS (
          SELECT 1 FROM dbo.bed_allocations a
          WHERE a.bed_id = b.bed_id AND a.allocation_status = N'ACTIVE'
      );
END
GO

-- =====================================================================
-- 5. REALISTIC SEED DATA INSERTION
-- =====================================================================
PRINT 'Inserting realistic seed data...';

-- 5.1 Roles
IF NOT EXISTS (SELECT 1 FROM dbo.roles)
BEGIN
    INSERT INTO dbo.roles (role_name, description) VALUES
    (N'SUPER_ADMIN', N'System architect and lead IT administrator with unconditional access'),
    (N'ADMIN', N'Hostel administrator with management access across hostels, rooms, allocations, and users'),
    (N'ACCOMMODATION_OFFICER', N'Front office officer handling student registration, room allocations, check-ins, and bed transfers'),
    (N'FINANCE_OFFICER', N'Accounts officer managing semester fees, invoicing, payment receipts, clearances, and audit reconciliation'),
    (N'VIEWER', N'Auditor or warden with read-only visualization and reporting permissions');
END
GO

-- 5.2 Users (Verified PBKDF2-HMAC-SHA256 Hashes)
IF NOT EXISTS (SELECT 1 FROM dbo.users)
BEGIN
    DECLARE @SuperAdminRoleId INT = (SELECT role_id FROM dbo.roles WHERE role_name = N'SUPER_ADMIN');
    DECLARE @AdminRoleId INT = (SELECT role_id FROM dbo.roles WHERE role_name = N'ADMIN');
    DECLARE @AccommRoleId INT = (SELECT role_id FROM dbo.roles WHERE role_name = N'ACCOMMODATION_OFFICER');
    DECLARE @FinanceRoleId INT = (SELECT role_id FROM dbo.roles WHERE role_name = N'FINANCE_OFFICER');
    DECLARE @ViewerRoleId INT = (SELECT role_id FROM dbo.roles WHERE role_name = N'VIEWER');

    INSERT INTO dbo.users (full_name, username, email, password_hash, role_id, account_status) VALUES
    (N'System Super Administrator', N'superadmin', N'superadmin@university.edu', N'PBKDF2$SHA256$100000$CErD0ZOD5qMx+NywxXMISA==$6FYRT+MQ53M6moJ/CHnT2KUbdsVh9ZwJwOddAgsufys=', @SuperAdminRoleId, N'ACTIVE'),
    (N'Dr. Margaret Wanjiru', N'admin', N'm.wanjiru@university.edu', N'PBKDF2$SHA256$100000$CErD0ZOD5qMx+NywxXMISA==$6FYRT+MQ53M6moJ/CHnT2KUbdsVh9ZwJwOddAgsufys=', @AdminRoleId, N'ACTIVE'),
    (N'James Omondi Otieno', N'accomm_officer', N'j.omondi@university.edu', N'PBKDF2$SHA256$100000$K/HmUvaEQkqoTn+WdSCpGw==$u8OBJb5qLVzVdbba+7wDX4rh/k790YxeocjhgPpnNLM=', @AccommRoleId, N'ACTIVE'),
    (N'Grace Chepngetich', N'finance_officer', N'g.chepngetich@university.edu', N'PBKDF2$SHA256$100000$FawVk9jcZet4NCluL5z78w==$piC1/fU1JgJl1Jf0yGvNwcC3LqrDH7nPWv+SsLhV34c=', @FinanceRoleId, N'ACTIVE'),
    (N'Prof. David Kimani', N'viewer', N'd.kimani@university.edu', N'PBKDF2$SHA256$100000$XR3SbCKjeCh4ojKPJ+QpZA==$7oA1YdvrP49qY97bhwgRtN3dPsf7nfsjgCCDbnmXkXw=', @ViewerRoleId, N'ACTIVE');
END
GO

-- 5.3 Semesters
IF NOT EXISTS (SELECT 1 FROM dbo.semesters)
BEGIN
    INSERT INTO dbo.semesters (semester_name, semester_code, academic_year, start_date, end_date, allocation_open_date, allocation_close_date, status) VALUES
    (N'Semester 2 2025/2026', N'SEM2-2025-2026', N'2025/2026', '2026-01-05', '2026-05-15', '2025-12-01', '2026-01-20', N'COMPLETED'),
    (N'Semester 1 2026/2027', N'SEM1-2026-2027', N'2026/2027', '2026-09-01', '2026-12-18', '2026-08-01', '2026-09-25', N'ACTIVE'),
    (N'Semester 2 2026/2027', N'SEM2-2026-2027', N'2026/2027', '2027-01-10', '2027-05-21', '2026-12-01', '2027-01-25', N'UPCOMING');
END
GO

-- 5.4 Hostels
IF NOT EXISTS (SELECT 1 FROM dbo.hostels)
BEGIN
    INSERT INTO dbo.hostels (hostel_name, hostel_code, location, description, gender_category, total_floors, status) VALUES
    (N'Mount Kenya Hall', N'MKH', N'North Campus, Zone A', N'Premier four-storey hall for undergraduate male students with high-speed internet and quiet study zones.', N'MALE', 4, N'ACTIVE'),
    (N'Aberdare Residence', N'ABR', N'East Campus, Zone B', N'Modern residency dedicated to female scholars featuring self-contained reading hubs and courtyard.', N'FEMALE', 4, N'ACTIVE'),
    (N'Kilimanjaro Executive Towers', N'KIL', N'Central Campus, Zone C', N'Executive multi-amenity hostel offering premium single and deluxe double studio suites.', N'MIXED', 3, N'ACTIVE'),
    (N'Ngong Hills Hall', N'NGH', N'South Campus, Zone D', N'Cozy three-storey hostel with verdant surroundings, designated for postgraduate and senior students.', N'FEMALE', 3, N'ACTIVE');
END
GO

-- 5.5 Rooms
IF NOT EXISTS (SELECT 1 FROM dbo.rooms)
BEGIN
    DECLARE @MKH_Id INT = (SELECT hostel_id FROM dbo.hostels WHERE hostel_code = N'MKH');
    DECLARE @ABR_Id INT = (SELECT hostel_id FROM dbo.hostels WHERE hostel_code = N'ABR');
    DECLARE @KIL_Id INT = (SELECT hostel_id FROM dbo.hostels WHERE hostel_code = N'KIL');

    INSERT INTO dbo.rooms (hostel_id, room_number, floor_number, room_type, capacity, description, status) VALUES
    -- Mount Kenya Hall
    (@MKH_Id, N'101', 1, N'DOUBLE', 2, N'Ground floor double room with natural ventilation and direct courtyard access.', N'AVAILABLE'),
    (@MKH_Id, N'102', 1, N'DOUBLE', 2, N'Ground floor double room with study cubicles.', N'AVAILABLE'),
    (@MKH_Id, N'201', 2, N'SINGLE', 1, N'First floor quiet single room with private balcony.', N'AVAILABLE'),
    (@MKH_Id, N'202', 2, N'QUAD', 4, N'First floor spacious quad room with dual wardrobe and study tables.', N'AVAILABLE'),
    (@MKH_Id, N'301', 3, N'DOUBLE', 2, N'Second floor double room undergoing window replacement.', N'MAINTENANCE'),

    -- Aberdare Residence
    (@ABR_Id, N'101', 1, N'DOUBLE', 2, N'Ground floor double room adjacent to the laundry pavilion.', N'AVAILABLE'),
    (@ABR_Id, N'102', 1, N'DOUBLE', 2, N'East wing ground floor double room.', N'AVAILABLE'),
    (@ABR_Id, N'201', 2, N'SINGLE', 1, N'First floor executive single room with study desk and campus view.', N'AVAILABLE'),
    (@ABR_Id, N'202', 2, N'DOUBLE', 2, N'First floor double room with private lockers.', N'AVAILABLE'),

    -- Kilimanjaro Towers
    (@KIL_Id, N'101', 1, N'SINGLE', 1, N'Executive studio room with ensuite kitchenette and private bathroom.', N'AVAILABLE'),
    (@KIL_Id, N'102', 1, N'DOUBLE', 2, N'Deluxe double suite with scenic garden vista.', N'AVAILABLE');
END
GO

-- 5.6 Beds
IF NOT EXISTS (SELECT 1 FROM dbo.beds)
BEGIN
    DECLARE @R_MKH_101 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'MKH' AND r.room_number = N'101');
    DECLARE @R_MKH_102 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'MKH' AND r.room_number = N'102');
    DECLARE @R_MKH_201 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'MKH' AND r.room_number = N'201');
    DECLARE @R_MKH_202 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'MKH' AND r.room_number = N'202');
    DECLARE @R_MKH_301 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'MKH' AND r.room_number = N'301');

    DECLARE @R_ABR_101 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'ABR' AND r.room_number = N'101');
    DECLARE @R_ABR_102 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'ABR' AND r.room_number = N'102');
    DECLARE @R_ABR_201 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'ABR' AND r.room_number = N'201');
    DECLARE @R_ABR_202 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'ABR' AND r.room_number = N'202');

    DECLARE @R_KIL_101 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'KIL' AND r.room_number = N'101');
    DECLARE @R_KIL_102 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'KIL' AND r.room_number = N'102');

    INSERT INTO dbo.beds (room_id, bed_number, bed_label, bed_type, semester_rate, bed_position, has_desk, has_locker, has_window, has_ventilation, status, notes) VALUES
    -- MKH Room 101 (Double)
    (@R_MKH_101, N'B1', N'MKH-101-B1', N'STANDARD', 30000.00, N'Window Side', 1, 1, 1, 1, N'OCCUPIED', N'Allocated to active student'),
    (@R_MKH_101, N'B2', N'MKH-101-B2', N'STANDARD', 30000.00, N'Door Side', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    -- MKH Room 102 (Double)
    (@R_MKH_102, N'B1', N'MKH-102-B1', N'STANDARD', 30000.00, N'Left Wall', 1, 1, 1, 1, N'OCCUPIED', N'Allocated to active student'),
    (@R_MKH_102, N'B2', N'MKH-102-B2', N'STANDARD', 30000.00, N'Right Wall', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    -- MKH Room 201 (Single)
    (@R_MKH_201, N'B1', N'MKH-201-B1', N'PREMIUM', 45000.00, N'Center Suite', 1, 1, 1, 1, N'OCCUPIED', N'Executive single occupied'),

    -- MKH Room 202 (Quad)
    (@R_MKH_202, N'B1', N'MKH-202-B1', N'STANDARD', 20000.00, N'Lower Bunk Window', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_MKH_202, N'B2', N'MKH-202-B2', N'STANDARD', 20000.00, N'Upper Bunk Window', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_MKH_202, N'B3', N'MKH-202-B3', N'STANDARD', 20000.00, N'Lower Bunk Door', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_MKH_202, N'B4', N'MKH-202-B4', N'STANDARD', 20000.00, N'Upper Bunk Door', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    -- MKH Room 301 (Under Maintenance)
    (@R_MKH_301, N'B1', N'MKH-301-B1', N'STANDARD', 30000.00, N'Window Side', 1, 1, 1, 1, N'MAINTENANCE', N'Damaged window latch pending carpenter'),
    (@R_MKH_301, N'B2', N'MKH-301-B2', N'STANDARD', 30000.00, N'Door Side', 1, 1, 0, 1, N'MAINTENANCE', N'Room maintenance closure'),

    -- ABR Room 101 (Double)
    (@R_ABR_101, N'B1', N'ABR-101-B1', N'STANDARD', 32000.00, N'Window Side', 1, 1, 1, 1, N'OCCUPIED', N'Allocated to active student'),
    (@R_ABR_101, N'B2', N'ABR-101-B2', N'STANDARD', 32000.00, N'Door Side', 1, 1, 0, 1, N'OCCUPIED', N'Allocated to active student'),

    -- ABR Room 102 (Double)
    (@R_ABR_102, N'B1', N'ABR-102-B1', N'STANDARD', 32000.00, N'Left Wall', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_ABR_102, N'B2', N'ABR-102-B2', N'STANDARD', 32000.00, N'Right Wall', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    -- ABR Room 201 (Single)
    (@R_ABR_201, N'B1', N'ABR-201-B1', N'PREMIUM', 48000.00, N'Center Suite', 1, 1, 1, 1, N'OCCUPIED', N'Executive female single suite'),

    -- ABR Room 202 (Double)
    (@R_ABR_202, N'B1', N'ABR-202-B1', N'STANDARD', 32000.00, N'Window Side', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_ABR_202, N'B2', N'ABR-202-B2', N'STANDARD', 32000.00, N'Door Side', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    -- KIL Room 101 (Single Studio)
    (@R_KIL_101, N'B1', N'KIL-101-B1', N'EXECUTIVE', 65000.00, N'Studio Deluxe', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),

    -- KIL Room 102 (Double Suite)
    (@R_KIL_102, N'B1', N'KIL-102-B1', N'EXECUTIVE', 42000.00, N'Window Suite A', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_KIL_102, N'B2', N'KIL-102-B2', N'EXECUTIVE', 42000.00, N'Window Suite B', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation');
END
GO

-- 5.7 Students
IF NOT EXISTS (SELECT 1 FROM dbo.students)
BEGIN
    INSERT INTO dbo.students (registration_number, first_name, middle_name, last_name, gender, email, phone_number, course, department, academic_year, emergency_contact_name, emergency_contact_phone, student_status) VALUES
    (N'ENG-2023-0101', N'Brian', N'Kiprono', N'Koech', N'MALE', N'brian.koech@student.university.edu', N'+254712345678', N'BSc Mechanical Engineering', N'School of Engineering', N'Year 3', N'Samuel Koech (Father)', N'+254722112233', N'ACTIVE'),
    (N'COM-2024-0205', N'Dennis', N'Mwangi', N'Kamau', N'MALE', N'dennis.kamau@student.university.edu', N'+254723456789', N'BSc Computer Science', N'School of Computing', N'Year 2', N'Mary Kamau (Mother)', N'+254733223344', N'ACTIVE'),
    (N'ENG-2022-0044', N'Kelvin', N'Juma', N'Wafula', N'MALE', N'kelvin.wafula@student.university.edu', N'+254734567890', N'BSc Electrical & Electronics', N'School of Engineering', N'Year 4', N'George Wafula (Uncle)', N'+254711998877', N'ACTIVE'),
    (N'NUR-2024-0312', N'Sharon', N'Akinyi', N'Odhiambo', N'FEMALE', N'sharon.odhiambo@student.university.edu', N'+254745678901', N'BSc Nursing & Public Health', N'School of Health Sciences', N'Year 2', N'Beatrice Odhiambo (Mother)', N'+254722556677', N'ACTIVE'),
    (N'BUS-2023-0418', N'Faith', N'Mumbua', N'Mutua', N'FEMALE', N'faith.mutua@student.university.edu', N'+254756789012', N'BCom Finance & Accounting', N'School of Business', N'Year 3', N'Patrick Mutua (Father)', N'+254733445566', N'ACTIVE'),
    (N'SCI-2025-0520', N'Lilian', N'Cherono', N'Langat', N'FEMALE', N'lilian.langat@student.university.edu', N'+254767890123', N'BSc Actuarial Science', N'School of Mathematics', N'Year 1', N'Eunice Langat (Guardian)', N'+254700112233', N'ACTIVE'),
    (N'COM-2023-0188', N'Victor', N'Oduor', N'Ochieng', N'MALE', N'victor.ochieng@student.university.edu', N'+254778901234', N'BSc Software Engineering', N'School of Computing', N'Year 3', N'Wilson Ochieng (Father)', N'+254712987654', N'ACTIVE'),
    (N'LAW-2024-0610', N'Cynthia', N'Wanjiku', N'Njoroge', N'FEMALE', N'cynthia.njoroge@student.university.edu', N'+254789012345', N'Bachelor of Laws (LL.B)', N'School of Law', N'Year 2', N'Grace Njoroge (Mother)', N'+254721876543', N'ACTIVE');
END
GO

-- 5.8 Bed Allocations (Including Historical Semester Reuse Demonstration)
IF NOT EXISTS (SELECT 1 FROM dbo.bed_allocations)
BEGIN
    DECLARE @SemPrevId INT = (SELECT semester_id FROM dbo.semesters WHERE semester_code = N'SEM2-2025-2026');
    DECLARE @SemActiveId INT = (SELECT semester_id FROM dbo.semesters WHERE semester_code = N'SEM1-2026-2027');

    DECLARE @AdminUserId INT = (SELECT user_id FROM dbo.users WHERE username = N'admin');
    DECLARE @OfficerUserId INT = (SELECT user_id FROM dbo.users WHERE username = N'accomm_officer');

    -- Students
    DECLARE @S1 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'ENG-2023-0101');
    DECLARE @S2 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'COM-2024-0205');
    DECLARE @S3 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'ENG-2022-0044');
    DECLARE @S4 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'NUR-2024-0312');
    DECLARE @S5 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'BUS-2023-0418');
    DECLARE @S6 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'SCI-2025-0520');

    -- Beds
    DECLARE @B_MKH_101_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'MKH-101-B1');
    DECLARE @B_MKH_102_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'MKH-102-B1');
    DECLARE @B_MKH_201_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'MKH-201-B1');
    DECLARE @B_ABR_101_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'ABR-101-B1');
    DECLARE @B_ABR_101_2 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'ABR-101-B2');
    DECLARE @B_ABR_201_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'ABR-201-B1');

    -- Historical Semester 2 2025/2026 Allocations (Completed & Checked Out)
    -- Demonstrates that Bed MKH-101-B1 was occupied by student S3 in previous semester,
    -- and is now successfully reused in Semester 1 2026/2027 without deleting historical records!
    INSERT INTO dbo.bed_allocations (student_id, bed_id, semester_id, semester_fee, allocation_date, check_in_date, check_out_date, allocation_status, notes, created_by) VALUES
    (@S3, @B_MKH_101_1, @SemPrevId, 28000.00, '2026-01-06 09:30:00', '2026-01-08 14:00:00', '2026-05-14 11:00:00', N'CHECKED_OUT', N'Completed prior semester safely. Original fee snapshotted at 28,000.', @AdminUserId),
    (@S5, @B_ABR_101_1, @SemPrevId, 30000.00, '2026-01-07 10:15:00', '2026-01-09 10:30:00', '2026-05-14 15:45:00', N'CHECKED_OUT', N'Prior semester allocation checked out.', @OfficerUserId);

    -- Current Active Semester 1 2026/2027 Allocations
    INSERT INTO dbo.bed_allocations (student_id, bed_id, semester_id, semester_fee, allocation_date, check_in_date, check_out_date, allocation_status, notes, created_by) VALUES
    -- S1 in MKH-101-B1: Fully Paid (CLEARED)
    (@S1, @B_MKH_101_1, @SemActiveId, 30000.00, '2026-09-02 08:30:00', '2026-09-03 10:00:00', NULL, N'ACTIVE', N'Checked in successfully. Fee snapshotted at current rate 30,000.', @OfficerUserId),

    -- S2 in MKH-102-B1: Partially Paid (PARTIAL)
    (@S2, @B_MKH_102_1, @SemActiveId, 30000.00, '2026-09-03 09:15:00', '2026-09-04 11:30:00', NULL, N'ACTIVE', N'Checked in with partial deposit.', @OfficerUserId),

    -- S3 in MKH-201-B1: Allocated but Unpaid (UNPAID)
    (@S3, @B_MKH_201_1, @SemActiveId, 45000.00, '2026-09-05 14:00:00', NULL, NULL, N'ACTIVE', N'Single suite allocated; payment and check-in pending.', @AdminUserId),

    -- S4 in ABR-101-B1: Fully Paid (CLEARED)
    (@S4, @B_ABR_101_1, @SemActiveId, 32000.00, '2026-09-02 11:00:00', '2026-09-03 13:45:00', NULL, N'ACTIVE', N'Checked in with full fee clearance.', @OfficerUserId),

    -- S5 in ABR-101-B2: Partially Paid (PARTIAL)
    (@S5, @B_ABR_101_2, @SemActiveId, 32000.00, '2026-09-04 10:20:00', '2026-09-05 16:00:00', NULL, N'ACTIVE', N'Checked in; second installment due by mid-semester.', @OfficerUserId),

    -- S6 in ABR-201-B1: Partially Paid (PARTIAL)
    (@S6, @B_ABR_201_1, @SemActiveId, 48000.00, '2026-09-06 09:00:00', '2026-09-06 14:15:00', NULL, N'ACTIVE', N'Executive single suite checked in.', @OfficerUserId);
END
GO

-- 5.9 Payments (Accurate Financial Balancing)
IF NOT EXISTS (SELECT 1 FROM dbo.payments)
BEGIN
    DECLARE @FinanceUser INT = (SELECT user_id FROM dbo.users WHERE username = N'finance_officer');

    -- Allocations
    DECLARE @Alloc_S1 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'ENG-2023-0101' AND sem.semester_code = N'SEM1-2026-2027');
    DECLARE @Alloc_S2 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'COM-2024-0205' AND sem.semester_code = N'SEM1-2026-2027');
    DECLARE @Alloc_S4 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'NUR-2024-0312' AND sem.semester_code = N'SEM1-2026-2027');
    DECLARE @Alloc_S5 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'BUS-2023-0418' AND sem.semester_code = N'SEM1-2026-2027');
    DECLARE @Alloc_S6 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'SCI-2025-0520' AND sem.semester_code = N'SEM1-2026-2027');

    INSERT INTO dbo.payments (allocation_id, receipt_number, amount_paid, payment_method, transaction_reference, payment_date, recorded_by, payment_status, notes) VALUES
    -- S1: Fee 30,000 -> Paid in 2 installments (15,000 MPESA + 15,000 BANK) = 30,000 (CLEARED)
    (@Alloc_S1, N'RCP-2026-0001', 15000.00, N'MPESA', N'QKD8912JKS', '2026-09-02 08:45:00', @FinanceUser, N'CONFIRMED', N'First installment via M-Pesa'),
    (@Alloc_S1, N'RCP-2026-0002', 15000.00, N'BANK', N'EQT-8829102-BNK', '2026-09-03 09:30:00', @FinanceUser, N'CONFIRMED', N'Second installment via Equity Bank direct transfer'),

    -- S2: Fee 30,000 -> Paid 18,000 MPESA, Balance Due 12,000 (PARTIAL)
    (@Alloc_S2, N'RCP-2026-0003', 18000.00, N'MPESA', N'QLA4729DKL', '2026-09-03 10:15:00', @FinanceUser, N'CONFIRMED', N'Semester downpayment via M-Pesa'),

    -- S4: Fee 32,000 -> Paid in full 32,000 BANK = 32,000 (CLEARED)
    (@Alloc_S4, N'RCP-2026-0004', 32000.00, N'BANK', N'KCB-9948201-TRF', '2026-09-02 11:20:00', @FinanceUser, N'CONFIRMED', N'Full semester payment via KCB Bank'),

    -- S5: Fee 32,000 -> Paid 20,000 BANK, Balance Due 12,000 (PARTIAL)
    (@Alloc_S5, N'RCP-2026-0005', 20000.00, N'BANK', N'COOP-448201-TX', '2026-09-04 11:00:00', @FinanceUser, N'CONFIRMED', N'Installment 1 via Co-operative Bank'),

    -- S6: Fee 48,000 -> Paid 25,000 MPESA, Balance Due 23,000 (PARTIAL)
    (@Alloc_S6, N'RCP-2026-0006', 25000.00, N'MPESA', N'QME918299K', '2026-09-06 09:30:00', @FinanceUser, N'CONFIRMED', N'M-Pesa payment for executive single suite');
END
GO

-- 5.10 Maintenance Records
IF NOT EXISTS (SELECT 1 FROM dbo.maintenance_records)
BEGIN
    DECLARE @MKH_Room301 INT = (SELECT room_id FROM dbo.rooms r JOIN dbo.hostels h ON r.hostel_id = h.hostel_id WHERE h.hostel_code = N'MKH' AND r.room_number = N'301');
    DECLARE @Bed_MKH_301_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'MKH-301-B1');
    DECLARE @OfficerUser INT = (SELECT user_id FROM dbo.users WHERE username = N'accomm_officer');

    INSERT INTO dbo.maintenance_records (bed_id, room_id, issue_title, issue_description, maintenance_status, reported_by, assigned_to, reported_date, resolution_notes) VALUES
    (@Bed_MKH_301_1, @MKH_Room301, N'Window Latch & Hinge Replacement', N'Window frame damaged by heavy wind gust; frame cannot lock securely. Carpentry repairs required before student occupancy.', N'REPORTED', @OfficerUser, N'Estate Maintenance Team (Paul Ndwiga)', '2026-09-01 10:00:00', NULL);
END
GO

-- 5.11 Activity Logs
IF NOT EXISTS (SELECT 1 FROM dbo.activity_logs)
BEGIN
    DECLARE @AdminId INT = (SELECT user_id FROM dbo.users WHERE username = N'admin');
    DECLARE @OfficerId INT = (SELECT user_id FROM dbo.users WHERE username = N'accomm_officer');
    DECLARE @FinanceId INT = (SELECT user_id FROM dbo.users WHERE username = N'finance_officer');

    INSERT INTO dbo.activity_logs (user_id, action_type, entity_type, entity_id, description, ip_address) VALUES
    (@AdminId, N'SYSTEM_INITIALIZATION', N'DATABASE', N'1', N'University Hostel Database initialized with security parameters and constraints.', N'127.0.0.1'),
    (@AdminId, N'SEMESTER_ACTIVATED', N'SEMESTERS', N'SEM1-2026-2027', N'Semester 1 2026/2027 designated as the active operational semester.', N'127.0.0.1'),
    (@OfficerId, N'BED_ALLOCATION', N'BED_ALLOCATIONS', N'1', N'Allocated bed MKH-101-B1 to student Brian Kiprono Koech (ENG-2023-0101). Fee snapshotted at 30,000.', N'192.168.1.105'),
    (@FinanceId, N'PAYMENT_RECORDED', N'PAYMENTS', N'RCP-2026-0001', N'Recorded M-Pesa receipt RCP-2026-0001 for KES 15,000.00 for student Brian Kiprono Koech.', N'192.168.1.110'),
    (@OfficerId, N'CHECK_IN', N'BED_ALLOCATIONS', N'1', N'Checked in student Brian Kiprono Koech into Mount Kenya Hall Room 101 Bed B1.', N'192.168.1.105');
END
GO

PRINT '=====================================================================';
PRINT 'Database university_hostel_db created and seeded successfully!';
PRINT '=====================================================================';
GO
