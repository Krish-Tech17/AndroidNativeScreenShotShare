# AndroidNativeShare Submodule

## Overview

**AndroidNativeShare** is a reusable Unity utility submodule that enables sharing a screenshot of the current app view using **Android’s native system share sheet**.  
It is designed to be **platform-isolated**, **UI-agnostic**, and **independent of AR or business logic**, making it suitable for reuse across multiple Unity and AR-based workflows.

The module leverages Android’s native `ACTION_SEND` intent to allow users to share screenshots via installed apps such as **Outlook, Gmail, Teams, WhatsApp**, or any other supported app chosen by the user.

---

## Why This Submodule Exists

Android sharing is a **platform-specific capability**. Embedding Android intent logic directly into AR features or UI scripts leads to:
- Tight coupling
- Reduced reusability
- Harder maintenance

This submodule isolates all Android sharing concerns into a single, well-defined utility layer, ensuring:
- Clean architecture
- SOLID compliance
- Easy reuse across projects and features

---

## Key Capabilities

- Capture the current rendered view (including AR content)
- Share the captured screenshot using Android’s native share sheet
- Allow the user to choose the target app (Outlook, Gmail, etc.)
- Avoid AR session disruption
- Work seamlessly on supported Android tablets

---

## Folder Structure

```
AndroidNativeShare/
 ├── Scripts/
 │   ├── Interfaces/
 │   │   └── IShareService.cs
 │   ├── Services/
 │   │   ├── ScreenshotService.cs
 │   │   └── AndroidShareService.cs
 │   └── Controllers/
 │       └── ShareController.cs
 └── Plugins/
     └── Android/
```

> **Note:**  
> This module intentionally does **not** contain UI, scenes, or prefabs.  
> It is a utility module, not a feature module.

---

## Architectural Principles

This module strictly follows **SOLID principles**:

- **Single Responsibility**  
  Each class has exactly one responsibility (capture, share, orchestrate).

- **Open / Closed**  
  New sharing behaviors (e.g., text + image) can be added without modifying existing classes.

- **Dependency Inversion**  
  Controllers depend on interfaces, not Android-specific implementations.

- **Platform Isolation**  
  Android-specific logic is fully contained and guarded by platform directives.

---

## How It Works (High-Level Flow)

```
User taps "Share Screenshot"
        ↓
ShareController
        ↓
ScreenshotService captures frame
        ↓
AndroidShareService invokes native share
        ↓
Android system share sheet opens
```

---

## How to Use This Submodule

### 1. Add the Controller to Your Scene

- Create an empty GameObject (e.g., `AndroidShareManager`)
- Attach the `ShareController` component

No additional setup is required.

---

### 2. Hook It to Any UI Trigger

From your UI button or interaction:

```
ShareController → ShareCurrentView()
```

That is the **only public method** required to use the module.

---

### 3. Build and Run on Android

- Build the project for Android
- Tap the share button
- Select an app (e.g., Outlook)
- Screenshot is attached automatically

> ⚠️ The native share sheet will **not appear in the Unity Editor**.  
> Always test on a real Android device.

---

## Example Usage Scenario

### Task Example: Share AR Inspection Evidence

**Acceptance Criteria**
1. User captures the current AR inspection view
2. User shares the image via any installed app
3. No AR session disruption

**Implementation**
- Add `ShareController` to the scene
- Call `ShareCurrentView()` on button click

No Android code, screenshot logic, or file handling is required in the feature code.

---

## Extending the Module

### 1. Share Image + Text (Extension Example)

**Use case:**  
User wants to share an AR screenshot with a description.

**How to extend (without modifying existing code):**
- Add a new interface (e.g., `IShareWithTextService`)
- Implement a new Android service (e.g., `AndroidShareWithTextService`)
- Optionally expose a new controller method

Existing classes remain unchanged.

---

## Platform Support

| Platform | Supported |
|--------|-----------|
Android  | ✅ |
iOS      | can be extended |

---

## Benefits of Using This Submodule

- Avoids duplicating Android intent logic
- Reduces feature complexity
- Improves maintainability
- Enables faster future feature development
- Aligns with enterprise Unity architecture standards

---

## Summary

**AndroidNativeShare** provides a clean, reusable, and extensible solution for Android screenshot sharing in Unity applications.  
It isolates platform concerns, follows best practices, and can be reused across AR and non-AR workflows with minimal integration effort.
