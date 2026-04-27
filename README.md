# KeyCleanr

A simple keyboard cleaning utility for Windows, inspired by KeyboardCleanTool.
Inspiration: https://folivora.ai/keyboardcleantool

## What it does

Locks your keyboard completely so you can wipe it down without accidentally triggering keystrokes. Click the button again to unlock.

## Requirements

- Windows 10 or 11
- .NET 8+

## Installation

1. Download the `.msix` and `.cer` files from the latest release

2. Install the certificate first:
   - Double-click the `.cer` file
   - Click **Install Certificate**
   - Select **Local Machine**
   - Select **Place all certificates in the following store**
   - Click **Browse** and select **Trusted Root Certification Authorities**
   - Click **OK** then **Finish**

3. Double-click the `.msix` file to install

You only need to install the certificate once. Future updates will install without repeating this step.

## Building

1. Open `KeyCleanr.sln` in Visual Studio 2022
2. Restore NuGet packages
3. Build and run

## Usage

1. Launch KeyCleanr
2. Click the button to lock your keyboard
3. Clean your keyboard
4. Click the button again to unlock

## Notes
- The app requires Developer Mode to be enabled on Windows for sideloading and installing the release build. You can enable Developer Mode in Settings > Update & Security > For developers.
- All keystrokes are blocked at the system level while locked — this includes shortcuts like Alt+F4 and Win key

## Known Limitations

- OEM special keys (e.g. ASUS MyASUS key, Dell, Lenovo function shortcuts) may not be blocked. These are handled at the driver level and bypass the standard Windows input pipeline that KeyCleanr hooks into.