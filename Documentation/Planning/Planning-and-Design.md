# Tumbang Preso: First-Person Challenge
## Planning and Design Documentation

---

# PART A — PLANNING AND TECHNICAL DECISION

## A1. Game Concept and Scope

### Project Title
Tumbang Preso: First-Person Challenge

### Category
Philippine Games and Sports

### Intended Users
The game is intended for students and casual mobile players who want to experience a simple digital adaptation of the traditional Filipino game Tumbang Preso.

### Game Objective
The objective of the game is to aim and throw a slipper at a can to earn points. A successful hit gives the player 10 points, while a missed throw removes one life. The player starts with three lives and must earn as many points as possible before losing all lives or before the 60-second timer expires.

### Basic Gameplay
The game uses a first-person perspective. The player controls the camera to aim using a crosshair and presses the Throw button to throw a slipper.

When the slipper successfully hits the can, 10 points are added to the score and the can moves to another position. When the slipper misses, one life is deducted.

The difficulty increases according to the player's score. Higher difficulty levels allow the can to use more positions and reduce its reset delay.

### Project Scope
The project includes:

- One main 3D gameplay environment
- First-person aiming
- PC mouse controls for Unity testing
- Android touch controls
- Slipper throwing
- Can hit detection
- Score system
- Lives system
- 60-second round timer
- Dynamic difficulty
- Dynamic can positions
- Loading screen
- Ready/Start screen
- Pause and Resume
- Game Over
- Play Again
- Local best score
- Sound effects and background music
- Android APK build

The project does not include online multiplayer, an online leaderboard, a database, or server-side functionality.

---

## A2. Requirements

### Functional Requirements

1. The system shall allow the player to start the game from the Ready screen.

2. The system shall allow the player to control the first-person camera to aim at the target.

3. The system shall allow the player to throw a slipper toward the position indicated by the crosshair.

4. The system shall detect when the slipper successfully hits the can and add 10 points to the player's score.

5. The system shall detect a missed throw and deduct one life from the player.

6. The system shall end the game when the player's lives reach zero.

7. The system shall use a 60-second round timer and end the game when the timer reaches zero.

8. The system shall change the game difficulty according to the player's current score.

9. The system shall move the can to different target positions after successful hits.

10. The system shall allow the player to pause and resume gameplay.

11. The system shall allow the player to restart the game after Game Over.

12. The system shall store and display the best score locally.

### Usability Requirement

The game shall provide simple controls, a visible crosshair, clearly labeled buttons, and visible score, lives, timer, and difficulty information so the player can easily understand the current game status.

### Performance / Resource Requirement

The game shall use a limited number of gameplay objects, controlled projectile lifetime, reusable prefabs, and a single primary gameplay environment to maintain acceptable performance on the target mobile device.

### Target Device / Deployment Requirement

The game shall support Android mobile devices in landscape orientation and shall be exportable as an Android APK.

---

## A3. Framework Comparison

### Unity

Unity provides built-in tools for 3D scenes, physics, collision detection, Rigidbody components, prefabs, user interfaces, audio, C# scripting, and Android deployment.

It allows the same project to be tested using PC controls in the Unity Editor while also supporting touch controls for Android.

Unity is suitable for this project because Tumbang Preso requires a 3D environment, projectile physics, collision detection, camera controls, game-state management, and mobile deployment.

### Native Android Development

Native Android development using Kotlin or Java provides direct access to Android features and allows applications to be developed specifically for Android devices.

However, implementing a 3D game environment, physics-based slipper throwing, collision detection, camera movement, and other game-related systems would require additional development compared with using a dedicated game engine.

### Comparison

Unity provides stronger built-in support for 3D game development and physics-based gameplay, while native Android development provides more direct access to Android platform functionality.

For this project, Unity is more suitable because the application is primarily a 3D game rather than a standard mobile application.

Unity also allows the project to remain organized using scenes, GameObjects, components, prefabs, and C# scripts.

---

## A4. Technical Decision

Unity was selected as the development framework because it provides the necessary tools for creating the game's 3D environment, first-person camera, physics-based slipper throwing, collision detection, user interface, audio, and Android build.

C# was used for the game logic, including scoring, lives, timer, difficulty, game states, can behavior, slipper behavior, and player input.

Android was selected as the target mobile platform because the project is designed to be playable using touch controls on a mobile device.

### Limitation

One limitation of Unity is that a Unity-based Android application may require more storage and device resources compared with a simple native mobile application.

### Response to the Limitation

The project uses one primary gameplay scene, relatively simple gameplay logic, controlled projectile lifetime, and a manageable number of active gameplay objects. Testing was also performed on an actual Android device to identify control and compatibility problems.

---

## A5. Three-Day Development Plan

### Day 1 — Design and Project Setup

- Define the Tumbang Preso game concept
- Identify the target users
- Define the game objective and mechanics
- Identify functional and non-functional requirements
- Create the Unity project
- Organize project folders
- Create the GameScene
- Set up the environment
- Set up the Player, Main Camera, Can, and basic UI

### Day 2 — Gameplay Implementation

- Implement first-person aiming
- Implement slipper throwing
- Implement hit and miss detection
- Implement score and lives
- Implement game states
- Implement Ready, Pause, and Game Over interfaces
- Implement timer
- Implement dynamic difficulty
- Implement dynamic can positions
- Implement Android touch controls
- Add audio and gameplay feedback

### Day 3 — Build, Testing and Documentation

- Configure Android build settings
- Build the Android APK
- Install and test the APK on an Android device
- Test gameplay interactions
- Identify and correct control problems
- Test scoring and lives
- Test timer and Game Over
- Test Pause and Resume
- Record troubleshooting issues
- Capture screenshots/evidence
- Complete project documentation
- Complete README
- Finalize GitHub repository

---

# PART B — INTERFACE AND GAME DESIGN

## B1. Screen and Interface Design

The game uses several interface states to clearly communicate the current game status to the player.

### Loading Screen

Displays a loading message while the game initializes before showing the Ready screen.

### Ready Screen

Contains:

- Game title
- Basic instructions
- Start Game button

The Ready screen allows the player to understand the basic rules before gameplay begins.

### Gameplay Screen

Contains:

- First-person game environment
- Can target
- Center crosshair
- Score
- Lives
- Timer
- Difficulty
- Pause button
- Throw button for mobile

The gameplay interface keeps the most important game information visible while the player aims and throws the slipper.

### Pause Screen

Contains:

- PAUSED message
- Resume button

The Pause screen stops active gameplay until the player chooses to continue.

### Game Over Screen

Contains:

- Game Over message
- Game Over reason
- Final score
- Best score
- Play Again button

The Game Over screen appears when the player loses all lives or when the round timer reaches zero.

---

## B2. Game Flow

The primary game flow is:

Launch Game

↓

Loading

↓

Ready

↓

Start Game

↓

Playing

During Playing:

- Aim at the can
- Throw slipper
- Hit = +10 score
- Miss = -1 life
- Can changes position after a valid hit
- Difficulty increases according to score
- Timer continues counting down

The player may also:

Playing → Pause → Resume → Playing

Game Over conditions:

Playing → Lives reach 0 → Game Over

or

Playing → Timer reaches 0 → Game Over

After Game Over:

Game Over → Play Again → New Game

---

## B3. Components and Responsibilities

### Player
Acts as the main first-person player object and provides the reference point for camera rotation and throwing.

### Main Camera
Provides the first-person view used by the player to aim.

### FirstPersonLook
Handles mouse-based first-person camera control during Unity Editor and PC testing.

### MobileLook
Handles Android touch-based camera aiming.

### SlipperThrower
Creates and throws the slipper toward the center crosshair and manages the throw cooldown.

### Slipper
Tracks whether the thrown slipper successfully hits the can or becomes a missed throw.

### CanTarget
Detects valid slipper hits, awards the hit through the game system, and moves the can between available target positions.

### GameManager
Controls the major game systems, including:

- Game states
- Score
- Lives
- Timer
- Difficulty
- UI updates
- Pause and Resume
- Game Over
- Restart
- Best score

### AudioManager
Controls background music and gameplay sound effects such as throwing, successful hits, misses, and Game Over.

### HUD
Displays the current score, lives, timer, difficulty, feedback messages, and gameplay buttons.

### Rigidbody and Colliders
Provide physics and collision detection for the slipper, can, ground, and other required gameplay objects.

---

## B4. Asset Plan

The project uses a combination of student-created and authorized third-party assets.

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

### Student-Created / Unity-Based Assets

- Gameplay layout
- Unity primitive objects
- Court layout
- Throwing area
- UI panels
- Buttons
- Crosshair
- Text elements
- Materials created within the project
- Gameplay scripts
- Game-state logic

### Third-Party Assets

Some environmental 3D models, visual assets, and audio resources were obtained from external asset sources.

The original asset names, creators, sources, and licenses should be retained and credited where required by their respective licenses.

Third-party assets are used primarily to improve the visual presentation of the environment and do not replace the project's main gameplay implementation.

---

## B5. Success Criteria

The prototype is considered successful when the following conditions are met:

1. The Unity project opens successfully.

2. GameScene loads correctly.

3. The Ready screen allows the player to begin the game.

4. The player can control the first-person camera.

5. Android touch aiming functions on the target mobile device.

6. The player can throw a slipper.

7. The slipper can interact with the can.

8. A successful hit adds 10 points.

9. A missed throw removes exactly one life.

10. The HUD displays score, lives, timer, and difficulty.

11. The player can pause and resume the game.

12. The game enters Game Over when lives reach zero or time expires.

13. The player can start another game after Game Over.

14. The project can be built as an Android APK.

15. The APK can be installed and tested on an Android mobile device.

16. Development problems and corrections are documented.

17. Required project documentation and evidence are prepared.