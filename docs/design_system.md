# University Hostel Management System — Design System Specification
## Prestige Academic & Executive Architecture (Phase 2 & Phase 3+)

---

## 1. Design Vision & Philosophy
This design system governs the entire University Hostel Management System. It transitions the application away from generic dark gaming/hacker templates ("AI tells") into an authoritative, world-class **Prestige Academic** aesthetic suited for university accommodation offices, bursars, and hall administrators.

### Core Tenets:
1. **Institutional Gravitas**: Oxford Navy (`#0B192C` to `#1E3E62`) with refined Academic Gold (`#D97706`) accents.
2. **Executive Legibility**: Work surfaces use crisp Ceramic Slate (`#F8FAFC`) and White (`#FFFFFF`) with high-contrast Slate Charcoal (`#0F172A`) typography.
3. **Data Clarity**: Numerical amounts, bed statuses, and allocation codes use clean tabular spacing with semantic status badges.
4. **No Shouting**: Sentence and Title case typography; avoid unnecessary uppercase labels and acid-neon glows.

---

## 2. Color Palette & Design Tokens

### Brand & Structural
- **`BrandNavyDark`**: `#0B192C` — Primary institutional sidebar and brand pillar base.
- **`BrandNavy`**: `#1E3E62` — Header banners, primary active navigation items, institutional titles.
- **`BrandNavyLight`**: `#2C5282` — Secondary brand accents, subtle table header highlights.
- **`AcademicGold`**: `#D97706` — Crest highlights, active badges, premium bed tiers.
- **`AcademicGoldHover`**: `#B45309` — Button hover state for gold accents.

### Work Surfaces & Neutrals
- **`SurfaceCanvas`**: `#F8FAFC` — Main application workspace background.
- **`SurfaceCard`**: `#FFFFFF` — Data cards, modal forms, table containers.
- **`SurfaceMuted`**: `#F1F5F9` — Input backgrounds, table alternating row tint, disabled states.
- **`BorderSubtle`**: `#E2E8F0` — Standard card borders, grid lines, dividers.
- **`BorderFocus`**: `#1E3E62` — Active input focus ring.

### Typography Hierarchy
- **`TextPrimary`**: `#0F172A` — Headings, table data, primary labels (high legibility).
- **`TextSecondary`**: `#475569` — Subtitles, supporting copy, field descriptions.
- **`TextMuted`**: `#94A3B8` — Metadata, timestamps, placeholder text.
- **`TextInverse`**: `#FFFFFF` — Text on navy surfaces.

### Semantic Status Indicators
- **`StatusSuccess` (Available / Active / Paid)**: `#16A34A` background / `#DCFCE7` chip.
- **`StatusWarning` (Maintenance / Partial / Pending)**: `#D97706` background / `#FEF3C7` chip.
- **`StatusDanger` (Occupied / Overdue / Suspended)**: `#DC2626` background / `#FEE2E2` chip.
- **`StatusInfo` (Reserved / In-Progress)**: `#2563EB` background / `#DBEAFE` chip.

---

## 3. UI Component Standards

### 3.1 Splash Screen (`frmSplash`)
- Golden-ratio split stage (`840 x 500`).
- Left pillar (310px): Deep Oxford Navy with vector University crest, Latin motto, and institutional mission.
- Right console (530px): Crisp Ceramic Slate with live diagnostic pipeline, real-time checklist items (`[✓]`), smooth progress bar, and status ticker.

### 3.2 Staff Authentication (`frmLogin`)
- Centered dual-card or split-stage presentation aligning with the same Oxford Navy + Ceramic White palette.
- Real-time password reveal toggle, credential memory, and clear role badges.

### 3.3 Main Dashboard & CRUD Modules (Phase 3+)
- **Left Navigation Rail (240px)**: Oxford Navy with clear icons, category separators, and active highlight pills.
- **Top Command Bar**: Institutional branding, active academic semester selector (`semesters` ADO.NET dropdown), user profile pill, and live date.
- **KPI Summary Cards**: Clean white elevated cards (`#FFFFFF`) with 12px rounded corners, subtle `#E2E8F0` borders, left semantic color accent, large high-contrast metrics, and supporting trends.
- **Data Tables**: Segoe UI 9.5pt, clean row padding, alternating row highlights (`#F8FAFC`), column sort headers, and instant search bars.

