-- =====================================================================
-- UNIVERSITY HOSTEL MANAGEMENT SYSTEM
-- MYSQL 8.0+ DATABASE CREATION SCRIPT
-- Database Name: university_hostel_db
-- =====================================================================

CREATE DATABASE IF NOT EXISTS university_hostel_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE university_hostel_db;

-- Application database user setup
CREATE USER IF NOT EXISTS 'hostel_app_user'@'localhost' IDENTIFIED BY 'HostelApp#Secure2026!';
GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON university_hostel_db.* TO 'hostel_app_user'@'localhost';
FLUSH PRIVILEGES;

-- 1. ROLES
CREATE TABLE IF NOT EXISTS roles (
    role_id INT AUTO_INCREMENT PRIMARY KEY,
    role_name VARCHAR(50) NOT NULL UNIQUE,
    description VARCHAR(255) NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- 2. USERS
CREATE TABLE IF NOT EXISTS users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(150) NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    role_id INT NOT NULL,
    account_status ENUM('ACTIVE', 'INACTIVE', 'SUSPENDED') NOT NULL DEFAULT 'ACTIVE',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    last_login TIMESTAMP NULL DEFAULT NULL,
    FOREIGN KEY (role_id) REFERENCES roles(role_id)
) ENGINE=InnoDB;

-- 3. SEMESTERS
CREATE TABLE IF NOT EXISTS semesters (
    semester_id INT AUTO_INCREMENT PRIMARY KEY,
    semester_name VARCHAR(100) NOT NULL,
    semester_code VARCHAR(50) NOT NULL UNIQUE,
    academic_year VARCHAR(20) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    allocation_open_date DATE NOT NULL,
    allocation_close_date DATE NOT NULL,
    status ENUM('UPCOMING', 'ACTIVE', 'COMPLETED', 'CLOSED') NOT NULL DEFAULT 'UPCOMING',
    active_flag TINYINT GENERATED ALWAYS AS (IF(status = 'ACTIVE', 1, NULL)) STORED,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY uq_active_semester (active_flag)
) ENGINE=InnoDB;

-- 4. HOSTELS
CREATE TABLE IF NOT EXISTS hostels (
    hostel_id INT AUTO_INCREMENT PRIMARY KEY,
    hostel_name VARCHAR(100) NOT NULL UNIQUE,
    hostel_code VARCHAR(20) NOT NULL UNIQUE,
    location VARCHAR(150) NOT NULL,
    description TEXT NULL,
    gender_category ENUM('MALE', 'FEMALE', 'MIXED') NOT NULL,
    total_floors INT NOT NULL DEFAULT 1,
    status ENUM('ACTIVE', 'INACTIVE', 'MAINTENANCE') NOT NULL DEFAULT 'ACTIVE',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- 5. ROOMS
CREATE TABLE IF NOT EXISTS rooms (
    room_id INT AUTO_INCREMENT PRIMARY KEY,
    hostel_id INT NOT NULL,
    room_number VARCHAR(20) NOT NULL,
    floor_number INT NOT NULL DEFAULT 1,
    room_type VARCHAR(50) NOT NULL DEFAULT 'STANDARD',
    capacity INT NOT NULL DEFAULT 1,
    description TEXT NULL,
    status ENUM('AVAILABLE', 'OCCUPIED', 'MAINTENANCE', 'INACTIVE') NOT NULL DEFAULT 'AVAILABLE',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    UNIQUE KEY uq_hostel_room (hostel_id, room_number),
    FOREIGN KEY (hostel_id) REFERENCES hostels(hostel_id)
) ENGINE=InnoDB;

-- 6. ROOM IMAGES
CREATE TABLE IF NOT EXISTS room_images (
    image_id INT AUTO_INCREMENT PRIMARY KEY,
    room_id INT NOT NULL,
    image_path VARCHAR(255) NOT NULL,
    image_caption VARCHAR(255) NULL,
    image_category VARCHAR(50) NOT NULL DEFAULT 'GENERAL',
    display_order INT NOT NULL DEFAULT 0,
    is_primary TINYINT(1) NOT NULL DEFAULT 0,
    uploaded_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (room_id) REFERENCES rooms(room_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- 7. BEDS
CREATE TABLE IF NOT EXISTS beds (
    bed_id INT AUTO_INCREMENT PRIMARY KEY,
    room_id INT NOT NULL,
    bed_number VARCHAR(20) NOT NULL,
    bed_label VARCHAR(100) NOT NULL,
    bed_type VARCHAR(50) NOT NULL DEFAULT 'STANDARD',
    semester_rate DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    bed_position VARCHAR(50) NULL,
    has_desk TINYINT(1) NOT NULL DEFAULT 1,
    has_locker TINYINT(1) NOT NULL DEFAULT 1,
    has_window TINYINT(1) NOT NULL DEFAULT 0,
    has_ventilation TINYINT(1) NOT NULL DEFAULT 1,
    status ENUM('AVAILABLE', 'OCCUPIED', 'MAINTENANCE', 'RESERVED', 'INACTIVE') NOT NULL DEFAULT 'AVAILABLE',
    notes TEXT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    UNIQUE KEY uq_room_bed (room_id, bed_number),
    FOREIGN KEY (room_id) REFERENCES rooms(room_id)
) ENGINE=InnoDB;

-- 8. STUDENTS
CREATE TABLE IF NOT EXISTS students (
    student_id INT AUTO_INCREMENT PRIMARY KEY,
    registration_number VARCHAR(50) NOT NULL UNIQUE,
    first_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(50) NULL,
    last_name VARCHAR(50) NOT NULL,
    gender ENUM('MALE', 'FEMALE', 'OTHER') NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone_number VARCHAR(30) NOT NULL,
    course VARCHAR(100) NOT NULL,
    department VARCHAR(100) NOT NULL,
    academic_year VARCHAR(20) NOT NULL,
    emergency_contact_name VARCHAR(100) NOT NULL,
    emergency_contact_phone VARCHAR(30) NOT NULL,
    student_status ENUM('ACTIVE', 'SUSPENDED', 'GRADUATED', 'DEFERRED') NOT NULL DEFAULT 'ACTIVE',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- 9. BED ALLOCATIONS
CREATE TABLE IF NOT EXISTS bed_allocations (
    allocation_id INT AUTO_INCREMENT PRIMARY KEY,
    student_id INT NOT NULL,
    bed_id INT NOT NULL,
    semester_id INT NOT NULL,
    semester_fee DECIMAL(12,2) NOT NULL,
    allocation_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    check_in_date DATETIME NULL DEFAULT NULL,
    check_out_date DATETIME NULL DEFAULT NULL,
    allocation_status ENUM('ACTIVE', 'CHECKED_OUT', 'CANCELLED', 'TRANSFERRED') NOT NULL DEFAULT 'ACTIVE',
    notes TEXT NULL,
    created_by INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    active_bed_allocation INT GENERATED ALWAYS AS (IF(allocation_status = 'ACTIVE', bed_id, NULL)) STORED,
    active_student_allocation INT GENERATED ALWAYS AS (IF(allocation_status = 'ACTIVE', student_id, NULL)) STORED,
    UNIQUE KEY uq_active_bed_semester (semester_id, active_bed_allocation),
    UNIQUE KEY uq_active_student_semester (semester_id, active_student_allocation),
    FOREIGN KEY (student_id) REFERENCES students(student_id),
    FOREIGN KEY (bed_id) REFERENCES beds(bed_id),
    FOREIGN KEY (semester_id) REFERENCES semesters(semester_id),
    FOREIGN KEY (created_by) REFERENCES users(user_id)
) ENGINE=InnoDB;

-- 10. PAYMENTS
CREATE TABLE IF NOT EXISTS payments (
    payment_id INT AUTO_INCREMENT PRIMARY KEY,
    allocation_id INT NOT NULL,
    receipt_number VARCHAR(50) NOT NULL UNIQUE,
    amount_paid DECIMAL(12,2) NOT NULL,
    payment_method ENUM('MPESA', 'BANK', 'CASH', 'OTHER') NOT NULL,
    transaction_reference VARCHAR(100) NOT NULL UNIQUE,
    payment_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    recorded_by INT NOT NULL,
    payment_status ENUM('CONFIRMED', 'REVERSED', 'ADJUSTED') NOT NULL DEFAULT 'CONFIRMED',
    notes TEXT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (allocation_id) REFERENCES bed_allocations(allocation_id),
    FOREIGN KEY (recorded_by) REFERENCES users(user_id)
) ENGINE=InnoDB;

-- 11. MAINTENANCE RECORDS
CREATE TABLE IF NOT EXISTS maintenance_records (
    maintenance_id INT AUTO_INCREMENT PRIMARY KEY,
    bed_id INT NULL,
    room_id INT NOT NULL,
    issue_title VARCHAR(150) NOT NULL,
    issue_description TEXT NOT NULL,
    maintenance_status ENUM('REPORTED', 'IN_PROGRESS', 'RESOLVED', 'CANCELLED') NOT NULL DEFAULT 'REPORTED',
    reported_by INT NOT NULL,
    assigned_to VARCHAR(100) NULL,
    reported_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    resolved_date DATETIME NULL DEFAULT NULL,
    resolution_notes TEXT NULL,
    evidence_image_path VARCHAR(255) NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (bed_id) REFERENCES beds(bed_id),
    FOREIGN KEY (room_id) REFERENCES rooms(room_id),
    FOREIGN KEY (reported_by) REFERENCES users(user_id)
) ENGINE=InnoDB;

-- 12. ACTIVITY LOGS
CREATE TABLE IF NOT EXISTS activity_logs (
    log_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NULL,
    action_type VARCHAR(50) NOT NULL,
    entity_type VARCHAR(50) NOT NULL,
    entity_id VARCHAR(50) NULL,
    description TEXT NOT NULL,
    action_timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ip_address VARCHAR(45) NULL,
    additional_data JSON NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL
) ENGINE=InnoDB;

