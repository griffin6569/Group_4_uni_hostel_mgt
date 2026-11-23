# Typography & Custom Fonts Directory

This directory is monitored by `University_hostel_mgt.Helpers.ThemeManager`.

## Automatic Font Resolution Stack
1. **Apple SF Pro** (`SF Pro Display`, `SF Pro Text`, `San Francisco`)
   - If `.otf` or `.ttf` font files are placed in this directory (or installed in Windows), `ThemeManager` automatically loads and applies them across the application via GDI+ `PrivateFontCollection`.
2. **Microsoft Segoe UI Variable** (`Segoe UI Variable Display`, `Segoe UI Variable Text`, `Segoe UI Variable Small`)
   - Pre-installed natively on modern Windows 11. Designed by Microsoft as the optical counterpart to Apple SF Pro.
3. **Segoe UI**
   - Universal high-legibility fallback for older Windows environments.

## How to add Apple SF Pro fonts:
Place any of the following font files directly in this folder:
- `SF-Pro-Display-Bold.otf`
- `SF-Pro-Display-Regular.otf`
- `SF-Pro-Text-Regular.otf`
- `SF-Pro-Text-Semibold.otf`
- `SF-Pro-Text-Bold.otf`
- `SF-Mono-Regular.otf`

They will be loaded into application memory automatically on boot without requiring system administrator font installation.

