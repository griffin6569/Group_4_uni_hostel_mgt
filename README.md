# University Hostel Management System — Prestige Academic Edition

[![.NET 11 Windows](https://img.shields.io/badge/.NET-11.0%20Windows-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/Framework-Windows%20Forms-0078D4?logo=windows)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![SQL Server 2025](https://img.shields.io/badge/Database-SQL%20Server%202025-CC292B?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server/)
[![ADO.NET](https://img.shields.io/badge/DataAccess-Direct%20ADO.NET-00599C)](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/)
[![Security PBKDF2](https://img.shields.io/badge/Security-PBKDF2--SHA256-success)](#)

A production-grade, enterprise desktop software application designed for university accommodation offices and bursaries. The system manages student residential accommodation strictly at the individual **BED** level with per-semester rate freezing, duplicate conflict safeguards, partial payment tracking, check-in/out clearance inspections, and immutable activity auditing.

---

## 🏛️ System Architecture & Layered Design

```mermaid
graph TD
    UI["Presentation Layer (Windows Forms)"] --> BLL["Business Logic Layer (Services)"]
    BLL --> DAL["Data Access Layer (Direct ADO.NET)"]
    DAL --> DB[("Microsoft SQL Server 2025")]
    MOD["Models Layer"] -.-> UI
    MOD -.-> BLL
    MOD -.-> DAL
    UTIL["Utilities Layer (Security, DotEnv, ThemeManager)"] -.-> UI
    UTIL -.-> BLL
    UTIL -.-> DAL
```

- **Presentation Layer (`Forms/`, `Controls/Views/`)**:
  - Executive Dashboard with 4 real-time calculated KPI cards and occupancy distribution meters.
  - Dedicated operational modules: Hostels, Rooms, Bed Matrix, Student Directory, Bed Allocations, and Bursar Billing Ledger.
  - Custom modals with input validation, key handover checklists, rate differential calculators, and receipt print preview.
- **Business Logic Layer (`Services/`)**:
  - `AuthenticationService`: PBKDF2 password verification with brute-force lockout protection.
  - Allocation rules: strictly validates available bed state, semester allocation windows, and duplicate allocation prevention.
- **Data Access Layer (`Data/Repositories/`)**:
  - Direct ADO.NET (`Microsoft.Data.SqlClient`) using parameterized commands, `SqlTransaction` boundaries, and no ORM overhead.
- **Models Layer (`Models/`)**:
  - Strongly typed entities: `Student`, `Hostel`, `Room`, `Bed`, `BedAllocation`, `Payment`, `Semester`, `User`, and `DashboardStatistics`.
- **Design System (`Helpers/ThemeManager.vb`)**:
  - Governed by Anthropic's `frontend-design` institutional tokens: Oxford Navy (`#0B192C` to `#1E3E62`), Academic Gold (`#D97706`), Ceramic Slate (`#F8FAFC`), and Pristine White (`#FFFFFF`).

---

## 📸 Interface Screenshots

| Screen | View |
|---|---|
| **Executive Dashboard** | ![Dashboard Overview](docs/screenshots/dashboard_overview.png) |
| **Bed Space Matrix** | ![Bed Matrix](docs/screenshots/beds_matrix.png) |
| **Bed Allocations Desk** | ![Bed Allocations](docs/screenshots/allocations_matrix.png) |
| **Bursar Financial Ledger** | ![Bursar Billing](docs/screenshots/billing_ledger.png) |
| **Official Electronic Receipt** | ![Receipt Preview](docs/screenshots/receipt_preview.png) |
| **Student Directory** | ![Students Directory](docs/screenshots/students_directory.png) |
| **Login Vault** | ![Login Screen](docs/screenshots/login_screen.png) |
| **Diagnostic Splash Screen** | ![Splash Screen](docs/screenshots/splash_screen.png) |

---

## ⚙️ Core Domain Business Rules

1. **Individual Bed Primacy**: Accommodation is inventoried, managed, and billed strictly at the **BED** space level rather than the room level.
2. **Permanent Rate Snapshotting**: When a bed is allocated to a student, the bed's active `semester_rate` is frozen into `dbo.bed_allocations.semester_fee`. Future price changes never recalculate historical fees.
3. **Database Uniqueness & Conflict Safeguards**:
   - `UQ_active_bed_per_semester`: Enforces that two students cannot occupy the same bed in the same academic semester.
   - `UQ_active_student_per_semester`: Enforces that one student cannot hold multiple active bed spaces in the same semester.
   - Bed status strictly transitions: `AVAILABLE` ➔ `OCCUPIED` ➔ `CHECKED_OUT` (or `TRANSFERRED`).
4. **Maintenance Clearance Gating**: Beds under `MAINTENANCE` cannot be allocated. During student check-out, damage assessments can release a bed directly to maintenance with issue tracking.
5. **Bursar Financial Ledger**: Supports multi-channel partial payments (`MPESA`, `BANK`, `CASH`, `OTHER`) with auto-reconciled student arrears.

---

## 🚀 Setup & Execution

### Prerequisites
- Microsoft SQL Server 2022 / 2025 running locally (or remote instance).
- .NET 8.0 or .NET 11.0 Windows Desktop SDK.

### Database Deployment
1. Run the database migration script via `sqlcmd`:
   ```powershell
   sqlcmd -S localhost -E -i database\create_database.sql -C
   ```
2. Copy environment template and configure connection:
   ```powershell
   copy .env.example .env
   ```

### Diagnostic Test Runners
The application includes integrated CLI diagnostic suites to verify database constraints, transactions, and UI handles:

```powershell
# Phase 1 & 2: Database connectivity, PBKDF2 hashing & Authentication
dotnet run --no-build -- --test-auth

# Phase 3: Core management modules & live dashboard KPI calculations
dotnet run --no-build -- --test-dashboard

# Phase 4: Bed allocations, rate snapshotting, check-in, transfers & billing ledger
dotnet run --no-build -- --test-phase4

# Render and capture UI screen previews
dotnet run --no-build -- --capture-screens
```

### Launching the Application
```powershell
dotnet run
```
Default Administrator Login:
- **Username**: `superadmin`
- **Password**: `Admin@2026!`

---

## 👥 Authors
- **Group 4 Software Engineering Team** — University Hostel Management System
