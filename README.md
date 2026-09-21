# Tumbang Preso: First-Person Challenge

## Project Concept

Tumbang Preso: First-Person Challenge is a 3D first-person mobile game based on the traditional Filipino game Tumbang Preso. The player aims and throws a slipper at a can while trying to earn points before losing all lives or running out of time.

## Intended Users

The game is intended for students and casual mobile players who want a simple digital adaptation of a traditional Filipino game.

## Development Environment

- Unity 6
- C#
- Universal Render Pipeline (URP)
- Android
- Visual Studio / VS Code
- Git and GitHub

## Project Structure

- Assets/Art - visual assets
- Assets/Materials - game materials
- Assets/Models - imported 3D models
- Assets/Prefabs - reusable game objects
- Assets/Scenes - Unity scenes
- Assets/Scripts - gameplay and system scripts
- Assets/Sounds - audio assets
- Assets/UI - interface-related assets

## How to Run

1. Open the project using the compatible Unity version.
2. Open Assets/Scenes/GameScene.
3. Press Play to test the game in the Unity Editor.
4. For Android, select Android in Build Profiles and build the project as an APK.

## Controls

### PC / Unity Editor

- Mouse movement - Aim
- Left Mouse Button - Throw slipper
- Pause button / configured pause input - Pause game

### Android

- Swipe on an empty part of the screen - Aim
- THROW button - Throw slipper
- PAUSE button - Pause game

## Gameplay

- Hit the Can: +10 points
- Miss: -1 life
- Starting lives: 3
- Round duration: 60 seconds
- Easy difficulty: 0-29 points
- Medium difficulty: 30-59 points
- Hard difficulty: 60+ points

The Can changes position and resets faster as the difficulty increases.

## Completed Features

- Loading screen
- Ready/start screen
- First-person aiming
- Mobile touch aiming
- Slipper throwing
- Can collision detection
- Score system
- Lives system
- Round timer
- Dynamic difficulty
- Dynamic Can positions
- Pause and Resume
- Game Over
- Play Again
- Local best score
- Hit and miss feedback
- Sound effects
- Background music
- Android interface
- Android APK build
- 3D environment and visual assets

## Build Status

The project was successfully built as an Android APK and tested on an Android mobile device.

## Known Issues / Limitations

- Touch sensitivity may feel slightly different depending on the Android device and screen size.
- The game uses a single main gameplay environment.
- The best score is stored locally on the device.
- The game does not use an online leaderboard, database, or multiplayer service.

## Troubleshooting

During development, the Android camera became unstable because PC mouse-look and mobile touch-look controls could interfere with camera rotation. The controls were separated by platform so FirstPersonLook is used for PC/Editor testing while MobileLook handles Android touch input.

Another issue caused lives to decrease without a throw because a Slipper object was permanently present in the scene. The scene object was removed and slippers are now instantiated only when the player throws.

## Assets

The project uses a combination of student-created Unity primitives, materials, UI elements, audio, and authorized third-party 3D assets.

Third-party assets should retain their original creator/source/license information where required.

## Next Steps

Possible future improvements include additional environments, improved animations, more difficulty options, additional sound and visual effects, settings for sensitivity and audio, and further optimization for different Android devices.