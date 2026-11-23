-- =====================================================================
-- UNIVERSITY HOSTEL MANAGEMENT SYSTEM
-- SEED DATA SCRIPT (SQL Server 2022 / 2025)
-- Database Name: university_hostel_db
-- =====================================================================

USE university_hostel_db;
GO

SET NOCOUNT ON;

PRINT 'Populating seed data for university_hostel_db...';

-- 1. Roles
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

-- 2. Users (PBKDF2-HMAC-SHA256 Hashes)
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

-- 3. Semesters
IF NOT EXISTS (SELECT 1 FROM dbo.semesters)
BEGIN
    INSERT INTO dbo.semesters (semester_name, semester_code, academic_year, start_date, end_date, allocation_open_date, allocation_close_date, status) VALUES
    (N'Semester 2 2025/2026', N'SEM2-2025-2026', N'2025/2026', '2026-01-05', '2026-05-15', '2025-12-01', '2026-01-20', N'COMPLETED'),
    (N'Semester 1 2026/2027', N'SEM1-2026-2027', N'2026/2027', '2026-09-01', '2026-12-18', '2026-08-01', '2026-09-25', N'ACTIVE'),
    (N'Semester 2 2026/2027', N'SEM2-2026-2027', N'2026/2027', '2027-01-10', '2027-05-21', '2026-12-01', '2027-01-25', N'UPCOMING');
END
GO

-- 4. Hostels
IF NOT EXISTS (SELECT 1 FROM dbo.hostels)
BEGIN
    INSERT INTO dbo.hostels (hostel_name, hostel_code, location, description, gender_category, total_floors, status) VALUES
    (N'Mount Kenya Hall', N'MKH', N'North Campus, Zone A', N'Premier four-storey hall for undergraduate male students with high-speed internet and quiet study zones.', N'MALE', 4, N'ACTIVE'),
    (N'Aberdare Residence', N'ABR', N'East Campus, Zone B', N'Modern residency dedicated to female scholars featuring self-contained reading hubs and courtyard.', N'FEMALE', 4, N'ACTIVE'),
    (N'Kilimanjaro Executive Towers', N'KIL', N'Central Campus, Zone C', N'Executive multi-amenity hostel offering premium single and deluxe double studio suites.', N'MIXED', 3, N'ACTIVE'),
    (N'Ngong Hills Hall', N'NGH', N'South Campus, Zone D', N'Cozy three-storey hostel with verdant surroundings, designated for postgraduate and senior students.', N'FEMALE', 3, N'ACTIVE');
END
GO

-- 5. Rooms
IF NOT EXISTS (SELECT 1 FROM dbo.rooms)
BEGIN
    DECLARE @MKH_Id INT = (SELECT hostel_id FROM dbo.hostels WHERE hostel_code = N'MKH');
    DECLARE @ABR_Id INT = (SELECT hostel_id FROM dbo.hostels WHERE hostel_code = N'ABR');
    DECLARE @KIL_Id INT = (SELECT hostel_id FROM dbo.hostels WHERE hostel_code = N'KIL');

    INSERT INTO dbo.rooms (hostel_id, room_number, floor_number, room_type, capacity, description, status) VALUES
    (@MKH_Id, N'101', 1, N'DOUBLE', 2, N'Ground floor double room with natural ventilation and direct courtyard access.', N'AVAILABLE'),
    (@MKH_Id, N'102', 1, N'DOUBLE', 2, N'Ground floor double room with study cubicles.', N'AVAILABLE'),
    (@MKH_Id, N'201', 2, N'SINGLE', 1, N'First floor quiet single room with private balcony.', N'AVAILABLE'),
    (@MKH_Id, N'202', 2, N'QUAD', 4, N'First floor spacious quad room with dual wardrobe and study tables.', N'AVAILABLE'),
    (@MKH_Id, N'301', 3, N'DOUBLE', 2, N'Second floor double room undergoing window replacement.', N'MAINTENANCE'),

    (@ABR_Id, N'101', 1, N'DOUBLE', 2, N'Ground floor double room adjacent to the laundry pavilion.', N'AVAILABLE'),
    (@ABR_Id, N'102', 1, N'DOUBLE', 2, N'East wing ground floor double room.', N'AVAILABLE'),
    (@ABR_Id, N'201', 2, N'SINGLE', 1, N'First floor executive single room with study desk and campus view.', N'AVAILABLE'),
    (@ABR_Id, N'202', 2, N'DOUBLE', 2, N'First floor double room with private lockers.', N'AVAILABLE'),

    (@KIL_Id, N'101', 1, N'SINGLE', 1, N'Executive studio room with ensuite kitchenette and private bathroom.', N'AVAILABLE'),
    (@KIL_Id, N'102', 1, N'DOUBLE', 2, N'Deluxe double suite with scenic garden vista.', N'AVAILABLE');
END
GO

-- 6. Beds
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
    (@R_MKH_101, N'B1', N'MKH-101-B1', N'STANDARD', 30000.00, N'Window Side', 1, 1, 1, 1, N'OCCUPIED', N'Allocated to active student'),
    (@R_MKH_101, N'B2', N'MKH-101-B2', N'STANDARD', 30000.00, N'Door Side', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    (@R_MKH_102, N'B1', N'MKH-102-B1', N'STANDARD', 30000.00, N'Left Wall', 1, 1, 1, 1, N'OCCUPIED', N'Allocated to active student'),
    (@R_MKH_102, N'B2', N'MKH-102-B2', N'STANDARD', 30000.00, N'Right Wall', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    (@R_MKH_201, N'B1', N'MKH-201-B1', N'PREMIUM', 45000.00, N'Center Suite', 1, 1, 1, 1, N'OCCUPIED', N'Executive single occupied'),

    (@R_MKH_202, N'B1', N'MKH-202-B1', N'STANDARD', 20000.00, N'Lower Bunk Window', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_MKH_202, N'B2', N'MKH-202-B2', N'STANDARD', 20000.00, N'Upper Bunk Window', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_MKH_202, N'B3', N'MKH-202-B3', N'STANDARD', 20000.00, N'Lower Bunk Door', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_MKH_202, N'B4', N'MKH-202-B4', N'STANDARD', 20000.00, N'Upper Bunk Door', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    (@R_MKH_301, N'B1', N'MKH-301-B1', N'STANDARD', 30000.00, N'Window Side', 1, 1, 1, 1, N'MAINTENANCE', N'Damaged window latch pending carpenter'),
    (@R_MKH_301, N'B2', N'MKH-301-B2', N'STANDARD', 30000.00, N'Door Side', 1, 1, 0, 1, N'MAINTENANCE', N'Room maintenance closure'),

    (@R_ABR_101, N'B1', N'ABR-101-B1', N'STANDARD', 32000.00, N'Window Side', 1, 1, 1, 1, N'OCCUPIED', N'Allocated to active student'),
    (@R_ABR_101, N'B2', N'ABR-101-B2', N'STANDARD', 32000.00, N'Door Side', 1, 1, 0, 1, N'OCCUPIED', N'Allocated to active student'),

    (@R_ABR_102, N'B1', N'ABR-102-B1', N'STANDARD', 32000.00, N'Left Wall', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_ABR_102, N'B2', N'ABR-102-B2', N'STANDARD', 32000.00, N'Right Wall', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    (@R_ABR_201, N'B1', N'ABR-201-B1', N'PREMIUM', 48000.00, N'Center Suite', 1, 1, 1, 1, N'OCCUPIED', N'Executive female single suite'),

    (@R_ABR_202, N'B1', N'ABR-202-B1', N'STANDARD', 32000.00, N'Window Side', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_ABR_202, N'B2', N'ABR-202-B2', N'STANDARD', 32000.00, N'Door Side', 1, 1, 0, 1, N'AVAILABLE', N'Ready for allocation'),

    (@R_KIL_101, N'B1', N'KIL-101-B1', N'EXECUTIVE', 65000.00, N'Studio Deluxe', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),

    (@R_KIL_102, N'B1', N'KIL-102-B1', N'EXECUTIVE', 42000.00, N'Window Suite A', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation'),
    (@R_KIL_102, N'B2', N'KIL-102-B2', N'EXECUTIVE', 42000.00, N'Window Suite B', 1, 1, 1, 1, N'AVAILABLE', N'Ready for allocation');
END
GO

-- 7. Students
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

-- 8. Allocations
IF NOT EXISTS (SELECT 1 FROM dbo.bed_allocations)
BEGIN
    DECLARE @SemPrevId INT = (SELECT semester_id FROM dbo.semesters WHERE semester_code = N'SEM2-2025-2026');
    DECLARE @SemActiveId INT = (SELECT semester_id FROM dbo.semesters WHERE semester_code = N'SEM1-2026-2027');

    DECLARE @AdminUserId INT = (SELECT user_id FROM dbo.users WHERE username = N'admin');
    DECLARE @OfficerUserId INT = (SELECT user_id FROM dbo.users WHERE username = N'accomm_officer');

    DECLARE @S1 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'ENG-2023-0101');
    DECLARE @S2 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'COM-2024-0205');
    DECLARE @S3 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'ENG-2022-0044');
    DECLARE @S4 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'NUR-2024-0312');
    DECLARE @S5 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'BUS-2023-0418');
    DECLARE @S6 INT = (SELECT student_id FROM dbo.students WHERE registration_number = N'SCI-2025-0520');

    DECLARE @B_MKH_101_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'MKH-101-B1');
    DECLARE @B_MKH_102_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'MKH-102-B1');
    DECLARE @B_MKH_201_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'MKH-201-B1');
    DECLARE @B_ABR_101_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'ABR-101-B1');
    DECLARE @B_ABR_101_2 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'ABR-101-B2');
    DECLARE @B_ABR_201_1 INT = (SELECT bed_id FROM dbo.beds WHERE bed_label = N'ABR-201-B1');

    -- Historical Allocations (Completed Semester 2 2025/2026)
    INSERT INTO dbo.bed_allocations (student_id, bed_id, semester_id, semester_fee, allocation_date, check_in_date, check_out_date, allocation_status, notes, created_by) VALUES
    (@S3, @B_MKH_101_1, @SemPrevId, 28000.00, '2026-01-06 09:30:00', '2026-01-08 14:00:00', '2026-05-14 11:00:00', N'CHECKED_OUT', N'Completed prior semester safely. Original fee snapshotted at 28,000.', @AdminUserId),
    (@S5, @B_ABR_101_1, @SemPrevId, 30000.00, '2026-01-07 10:15:00', '2026-01-09 10:30:00', '2026-05-14 15:45:00', N'CHECKED_OUT', N'Prior semester allocation checked out.', @OfficerUserId);

    -- Active Allocations (Current Semester 1 2026/2027)
    INSERT INTO dbo.bed_allocations (student_id, bed_id, semester_id, semester_fee, allocation_date, check_in_date, check_out_date, allocation_status, notes, created_by) VALUES
    (@S1, @B_MKH_101_1, @SemActiveId, 30000.00, '2026-09-02 08:30:00', '2026-09-03 10:00:00', NULL, N'ACTIVE', N'Checked in successfully. Fee snapshotted at current rate 30,000.', @OfficerUserId),
    (@S2, @B_MKH_102_1, @SemActiveId, 30000.00, '2026-09-03 09:15:00', '2026-09-04 11:30:00', NULL, N'ACTIVE', N'Checked in with partial deposit.', @OfficerUserId),
    (@S3, @B_MKH_201_1, @SemActiveId, 45000.00, '2026-09-05 14:00:00', NULL, NULL, N'ACTIVE', N'Single suite allocated; payment and check-in pending.', @AdminUserId),
    (@S4, @B_ABR_101_1, @SemActiveId, 32000.00, '2026-09-02 11:00:00', '2026-09-03 13:45:00', NULL, N'ACTIVE', N'Checked in with full fee clearance.', @OfficerUserId),
    (@S5, @B_ABR_101_2, @SemActiveId, 32000.00, '2026-09-04 10:20:00', '2026-09-05 16:00:00', NULL, N'ACTIVE', N'Checked in; second installment due by mid-semester.', @OfficerUserId),
    (@S6, @B_ABR_201_1, @SemActiveId, 48000.00, '2026-09-06 09:00:00', '2026-09-06 14:15:00', NULL, N'ACTIVE', N'Executive single suite checked in.', @OfficerUserId);
END
GO

-- 9. Payments
IF NOT EXISTS (SELECT 1 FROM dbo.payments)
BEGIN
    DECLARE @FinanceUser INT = (SELECT user_id FROM dbo.users WHERE username = N'finance_officer');

    DECLARE @Alloc_S1 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'ENG-2023-0101' AND sem.semester_code = N'SEM1-2026-2027');
    DECLARE @Alloc_S2 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'COM-2024-0205' AND sem.semester_code = N'SEM1-2026-2027');
    DECLARE @Alloc_S4 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'NUR-2024-0312' AND sem.semester_code = N'SEM1-2026-2027');
    DECLARE @Alloc_S5 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'BUS-2023-0418' AND sem.semester_code = N'SEM1-2026-2027');
    DECLARE @Alloc_S6 INT = (SELECT allocation_id FROM dbo.bed_allocations a JOIN dbo.students s ON a.student_id = s.student_id JOIN dbo.semesters sem ON a.semester_id = sem.semester_id WHERE s.registration_number = N'SCI-2025-0520' AND sem.semester_code = N'SEM1-2026-2027');

    INSERT INTO dbo.payments (allocation_id, receipt_number, amount_paid, payment_method, transaction_reference, payment_date, recorded_by, payment_status, notes) VALUES
    (@Alloc_S1, N'RCP-2026-0001', 15000.00, N'MPESA', N'QKD8912JKS', '2026-09-02 08:45:00', @FinanceUser, N'CONFIRMED', N'First installment via M-Pesa'),
    (@Alloc_S1, N'RCP-2026-0002', 15000.00, N'BANK', N'EQT-8829102-BNK', '2026-09-03 09:30:00', @FinanceUser, N'CONFIRMED', N'Second installment via Equity Bank direct transfer'),
    (@Alloc_S2, N'RCP-2026-0003', 18000.00, N'MPESA', N'QLA4729DKL', '2026-09-03 10:15:00', @FinanceUser, N'CONFIRMED', N'Semester downpayment via M-Pesa'),
    (@Alloc_S4, N'RCP-2026-0004', 32000.00, N'BANK', N'KCB-9948201-TRF', '2026-09-02 11:20:00', @FinanceUser, N'CONFIRMED', N'Full semester payment via KCB Bank'),
    (@Alloc_S5, N'RCP-2026-0005', 20000.00, N'BANK', N'COOP-448201-TX', '2026-09-04 11:00:00', @FinanceUser, N'CONFIRMED', N'Installment 1 via Co-operative Bank'),
    (@Alloc_S6, N'RCP-2026-0006', 25000.00, N'MPESA', N'QME918299K', '2026-09-06 09:30:00', @FinanceUser, N'CONFIRMED', N'M-Pesa payment for executive single suite');
END
GO

PRINT 'Seed data population completed successfully.';
GO

