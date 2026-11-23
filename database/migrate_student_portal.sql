-- =====================================================================
-- Dedan Kimathi University of Technology (DeKUT) Hostel Management System
-- Migration Script: Student Self-Service Portal Integration
-- =====================================================================

PRINT 'Applying migration: Student Self-Service Portal Integration...';

-- 1. Ensure STUDENT Role Exists
IF NOT EXISTS (SELECT 1 FROM dbo.roles WHERE role_name = N'STUDENT')
BEGIN
    PRINT 'Adding STUDENT role to dbo.roles...';
    INSERT INTO dbo.roles (role_name, description)
    VALUES (N'STUDENT', N'Enrolled Dedan Kimathi University student with access to self-service accommodation portal');
END
ELSE
BEGIN
    PRINT 'STUDENT role already exists.';
END
GO

-- 2. Link dbo.users to dbo.students via student_id foreign key
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.users') AND name = 'student_id')
BEGIN
    PRINT 'Adding student_id column to dbo.users...';
    ALTER TABLE dbo.users ADD student_id INT NULL;
    
    PRINT 'Adding FK_users_student constraint...';
    ALTER TABLE dbo.users ADD CONSTRAINT FK_users_student 
        FOREIGN KEY (student_id) REFERENCES dbo.students(student_id) ON DELETE NO ACTION;
        
    PRINT 'Creating index IX_users_student_id...';
    CREATE NONCLUSTERED INDEX IX_users_student_id ON dbo.users(student_id);
END
ELSE
BEGIN
    PRINT 'student_id column already exists in dbo.users.';
END
GO

-- 3. Update create_database.sql parity
PRINT 'Student Portal migration script completed successfully.';
GO

