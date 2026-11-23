# University Hostel Management System — Database Architecture & Specification

## 1. Overview
The database for the **University Hostel Management System** is designed for high reliability, financial integrity, and strict business rule enforcement at the relational schema level. 

- **Primary Target Database:** Microsoft SQL Server 2022 / 2025
- **Alternative Target Database:** MySQL 8.0+ (Full script provided in `database/create_database_mysql.sql`)
- **Database Name:** `university_hostel_db`
- **Application User:** `hostel_app_user` (Configured with least-privilege DML permissions only: `SELECT`, `INSERT`, `UPDATE`, `DELETE`, `EXECUTE`)

---

## 2. Core Entity Relationships

```
[roles] 1 ────< [users] 1 ────< [activity_logs]
                  │
                  ├────< [bed_allocations] (created_by)
                  ├────< [payments] (recorded_by)
                  └────< [maintenance_records] (reported_by)

[hostels] 1 ────< [rooms] 1 ────< [beds] 1 ────< [bed_allocations] 1 ────< [payments]
                    │                                   │
                    ├────< [room_images]                │
                    └────< [maintenance_records]        │
                                                        │
[students]  1 ──────────────────────────────────────────┤
                                                        │
[semesters] 1 ──────────────────────────────────────────┘
```

---

## 3. Critical Business Rules & Relational Constraint Enforcements

### 3.1 Individual Bed-Level Allocation (Not Room-Level)
Accommodation is strictly allocated per **individual bed** (`bed_id`), never as a generic room reservation. Each bed belongs to a room and hostel, possesses an independent semester rate (`semester_rate`), and has individually tracked amenities (`has_desk`, `has_locker`, `has_window`, `has_ventilation`).

### 3.2 Historical Semester Reuse
A bed allocated in Semester 1 can be allocated to a different student in Semester 2 without destroying historical records.
- Historical allocations maintain `allocation_status = 'CHECKED_OUT'`.
- Unique indexes on active allocations use **filtered indexes** (`WHERE allocation_status = 'ACTIVE'`). Multiple inactive allocations for the same bed can coexist peacefully for historical reporting.

### 3.3 Protection Against Overbooking (Active Bed Uniqueness)
- **Constraint:** `UQ_active_bed_per_semester`
- **Definition:** `UNIQUE NONCLUSTERED (semester_id, bed_id) WHERE allocation_status = 'ACTIVE'`
- **Enforcement:** Impossible for two students to hold an active allocation for the same bed in the same semester.

### 3.4 Protection Against Multiple Concurrent Allocations for a Single Student
- **Constraint:** `UQ_active_student_per_semester`
- **Definition:** `UNIQUE NONCLUSTERED (semester_id, student_id) WHERE allocation_status = 'ACTIVE'`
- **Enforcement:** A student cannot be allocated to more than one bed in any given semester.

### 3.5 Single Active Operational Semester
- **Constraint:** `UQ_semesters_single_active`
- **Definition:** `UNIQUE NONCLUSTERED (status) WHERE status = 'ACTIVE'`
- **Enforcement:** At most one semester can be designated with `status = 'ACTIVE'` at any time.

### 3.6 Snapshotted Historical Fees
The `bed_allocations.semester_fee` column stores a fixed historical snapshot of `beds.semester_rate` at the time of allocation. Any subsequent changes to standard room or bed prices never alter historical financial obligations.

### 3.7 Maintenance & Inactive Bed Protection
Enforced via database trigger `dbo.trg_validate_bed_allocation`:
- Attempting to create an `ACTIVE` allocation for a bed marked as `MAINTENANCE` or `INACTIVE` is immediately aborted with a transaction rollback.
- Attempting to create an allocation in a `CLOSED` or `COMPLETED` semester is rejected.

### 3.8 Automatic Status Synchronization
Enforced via database trigger `dbo.trg_sync_bed_status_on_allocation`:
- When an allocation becomes `ACTIVE`, the bed's status automatically updates to `OCCUPIED`.
- When an allocation changes to `CHECKED_OUT`, `CANCELLED`, or `TRANSFERRED`, the bed's status reverts to `AVAILABLE` (provided no other active allocation exists and the bed is not in maintenance).

---

## 4. Seed Data & Default User Accounts

All sample passwords use PBKDF2-HMAC-SHA256 with 100,000 iterations and unique cryptographic salts.

| Username | Role | Full Name | Default Password | Status |
|---|---|---|---|---|
| `superadmin` | `SUPER_ADMIN` | System Super Administrator | `Admin@2026!` | `ACTIVE` |
| `admin` | `ADMIN` | Dr. Margaret Wanjiru | `Admin@2026!` | `ACTIVE` |
| `accomm_officer` | `ACCOMMODATION_OFFICER` | James Omondi Otieno | `Officer@2026!` | `ACTIVE` |
| `finance_officer` | `FINANCE_OFFICER` | Grace Chepngetich | `Finance@2026!` | `ACTIVE` |
| `viewer` | `VIEWER` | Prof. David Kimani | `Viewer@2026!` | `ACTIVE` |

