# Tumbang Preso: First-Person Challenge

## Project Overview

Tumbang Preso: First-Person Challenge is a 3D first-person mobile game based on the traditional Filipino game Tumbang Preso.

The player aims at a can using a first-person camera and throws a slipper to knock it down. A successful hit gives 10 points, while a missed throw removes one life. The player starts with three lives and has 60 seconds to earn as many points as possible.

The game was developed using Unity and is designed primarily for Android mobile devices.

---

## Category

Philippine Games and Sports

---

## Intended Users

The game is intended for students and casual mobile players who want to experience a simple digital adaptation of the traditional Filipino game Tumbang Preso.

---

## Development Environment

- Unity 6
- C#
- Universal Render Pipeline (URP)
- Android Build Support
- Git
- GitHub
- Windows development environment

---

## Game Features

The project includes:

- 3D first-person gameplay
- First-person aiming
- Center crosshair
- Physics-based slipper throwing
- Can hit detection
- Score system
- Lives system
- 60-second round timer
- Dynamic difficulty
- Dynamic Can positions
- Loading screen
- Ready / Start screen
- Pause and Resume
- Game Over
- Play Again
- Local best score
- Hit and miss feedback
- Sound effects
- Background music
- PC controls for Unity Editor testing
- Android touch controls
- Android APK build

---

## Gameplay Rules

The player starts with:

- Score: 0
- Lives: 3
- Time: 60 seconds
- Difficulty: Easy

### Successful Hit

When the slipper successfully hits the Can:

- +10 points are added.
- Hit feedback is displayed.
- A hit sound effect is played.
- The Can moves to another available position.

### Miss

When a thrown slipper misses:

- 1 life is deducted.
- Miss feedback is displayed.
- A miss sound effect is played.

### Game Over

The game ends when:

- The player's lives reach 0, or
- The 60-second timer reaches 0.

The Game Over screen displays the final score and locally stored best score.

---

## Difficulty System

Difficulty automatically changes according to the player's score.

### Easy

Score:

0–29

The Can uses the central target positions and has the longest reset delay.

### Medium

Score:

30–59

Additional Can positions become available and the reset delay becomes shorter.

### Hard

Score:

60+

All configured Can positions may be used and the Can resets faster.

---

## Controls

### Unity Editor / PC

- Mouse Movement — Aim
- Left Mouse Button — Throw slipper
- Pause control — Pause gameplay

The `FirstPersonLook` script is used for Editor and standalone PC camera control.

### Android

- Drag / Swipe on the gameplay area — Aim
- THROW button — Throw slipper
- PAUSE button — Pause gameplay
- RESUME button — Continue gameplay

The `MobileLook` script handles touch-based camera control on Android.

---

## Project Structure

Important project folders include:

Assets/
- Art — visual game assets
- Materials — materials used by game objects
- Models — imported 3D models
- Prefabs — reusable game objects
- Scenes — Unity scenes
- Scripts — C# game scripts
- Sounds — game audio
- UI — interface-related assets

Documentation/
- Planning — project planning and design documentation
- Testing — testing and troubleshooting records
- Screenshots — project and gameplay evidence

---

## Important Game Components

### GameManager

Controls:

- Game states
- Score
- Lives
- Timer
- Difficulty
- UI updates
- Pause / Resume
- Game Over
- Restart
- Best score

### SlipperThrower

Creates and throws the slipper toward the position indicated by the center crosshair.

### Slipper

Determines whether a thrown slipper successfully hits the Can or becomes a missed throw.

### CanTarget

Handles successful Can hits, Can resetting, target positions, and difficulty-related reset behavior.

### FirstPersonLook

Handles mouse-based camera aiming during Unity Editor and PC testing.

### MobileLook

Handles touch-based camera aiming on Android devices.

### AudioManager

Controls background music and gameplay sound effects.

---

## How to Run the Project

1. Clone or download the project repository.
2. Open the project using the compatible Unity version.
3. Allow Unity to import and compile the project files.
4. Open:

   Assets/Scenes/GameScene.unity

5. Press the Play button in Unity.
6. Press START GAME to begin.

---

## Android Build

To create the Android version:

1. Open the Unity project.
2. Go to File > Build Profiles.
3. Select Android.
4. Make sure GameScene is included in the scene list.
5. Select Android as the active build platform.
6. Build the project.
7. Save the generated application as:

   TumbangPreso.apk

8. Install the APK on an Android device.
9. Launch the game and test the touch controls.

---

## Build Status

Android APK build: SUCCESSFUL

The game was successfully built as an Android APK, installed on an Android mobile device, and tested using touch controls.

---

## Testing

Testing was performed for:

- Project opening
- Main scene loading
- Ready / Start screen
- PC camera input
- Android touch input
- Slipper throwing
- Can hit detection
- Score updates
- Miss and life deduction
- Can repositioning
- Difficulty changes
- Round timer
- Pause
- Resume
- Game Over
- Play Again
- Audio
- Android APK generation
- Android installation and launch
- Testing after corrections

Detailed testing information is available in:

Documentation/Testing/Test-Record.md

---

## Troubleshooting

Several issues were identified and corrected during development.

### Mobile Camera Drifting

During Android testing, the camera became unstable and difficult to control.

The PC `FirstPersonLook` and Android `MobileLook` controls were separated by platform. Mouse-look code now runs only for Unity Editor / standalone PC testing, while `MobileLook` controls the camera on Android.

Mobile aiming was tested again and the drifting problem was resolved.

### Automatic Life Loss

The player previously lost one life without throwing a slipper.

A Slipper GameObject had accidentally remained permanently inside the scene. Its script eventually processed itself as a missed projectile.

The scene instance was removed so slippers are now created only when the player performs an actual throw.

### Android Build Issue

An Android build problem occurred after changes to script/component layouts.

Unity-generated cache data was refreshed while the project's Assets, Packages, and ProjectSettings were preserved. The project was reopened and the Android build was successfully completed.

Additional troubleshooting information is available in:

Documentation/Testing/Issue-Record.md

---

## Completed Features

The following major features are completed:

- Game environment
- First-person camera
- Mobile touch aiming
- PC Editor aiming
- Slipper throwing
- Crosshair-based aiming
- Can target
- Hit detection
- Miss detection
- Score
- Lives
- Timer
- Difficulty
- Dynamic Can positions
- Loading state
- Ready state
- Playing state
- Pause / Resume
- Game Over
- Restart / Play Again
- Local best score
- Gameplay feedback
- Sound effects
- Background music
- Android interface
- Android APK build
- Android device testing

---

## Limitations / Incomplete Features

The current version does not include:

- Online multiplayer
- Online leaderboard
- Online database
- Server-side services

The best score is stored locally on the device.

Touch sensitivity may also feel different depending on the Android device and screen size.

These limitations do not prevent the main Tumbang Preso gameplay from functioning.

---

## Assets and Credits

The project uses a combination of student-created content, Unity-created objects, and authorized third-party assets.

Student-created content includes:

- Gameplay implementation
- Game logic
- User interface arrangement
- Game-state implementation
- Court/gameplay layout
- C# scripts
- Materials and Unity primitive-based objects created for the project

Third-party assets used in the project should be credited using their original asset information.

### Third-Party Asset Credits

Add the downloaded assets used in the final project below:

Asset Name:Bench (LowPoly)
Creator:3dMuffin
Source:Sketchfab
License:CC Attribution

Asset Name:Goku (Super Saiyan 3)
Creator:DrewsDigitalDesigns
Source:Sketchfab
License:CC Attribution

Asset Name:Kid Buu
Creator:Igli Faslija
Source:Sketchfab
License:CC Attribution-NonCommercial

Asset Name:Abandoned Playground
Creator:sergeilihandristov
Source:Sketchfab
License:CC Attribution

Asset Name:Modular Urban Fence Pack (w/ graffiti textures)
Creator:TampaJoey
Source:Sketchfab
License:CC Attribution

Asset Name:cobblestone ground - lowpoly
Creator:SPLEEN VISION
Source:Sketchfab
License:CC Attribution

## Documentation

Project documentation is located in:

Documentation/

This contains:

- Planning and Design
- Final Test Record
- Issue and Troubleshooting Record
- Screenshots / Evidence

---

## Known Issues

No critical gameplay issue is currently known to prevent the game from being played.

Minor differences in touch sensitivity may occur depending on the Android device and display size.

---

## Future Improvements

Possible future improvements include:

- Adjustable touch sensitivity
- Additional environments
- Additional gameplay modes
- Improved animations
- Additional visual effects
- Additional sound effects
- More difficulty options
- Additional Android device testing
- Performance optimization
- Additional accessibility settings

---

## Project Status

Tumbang Preso: First-Person Challenge is a functional Unity mobile game prototype with completed core gameplay, Android touch controls, game-state management, testing documentation, troubleshooting records, and a successful Android APK build.