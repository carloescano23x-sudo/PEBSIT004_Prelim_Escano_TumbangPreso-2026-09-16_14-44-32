# Tumbang Preso: First-Person Challenge
## Issue and Troubleshooting Record

---

# Issue 1 — Mobile Camera Drifting and Unstable Aiming

## Actual Issue

During Android testing, the first-person camera was difficult to control. While dragging the screen, the camera could become unstable and move up and down. After releasing the finger, the camera could also change its aiming position or move upward.

This made it difficult for the player to accurately aim at the Can.

## Steps to Reproduce

1. Launch the Android version of the game.
2. Press Start Game.
3. Drag the screen to aim at the Can.
4. Stop dragging or release the aiming finger.
5. Observe the camera behavior.

## Expected Result

The camera should move according to the player's touch movement. When the player stops dragging or releases the finger, camera movement should stop immediately and remain at the selected aiming position.

## Actual Result

The camera became unstable and could continue moving or suddenly change its vertical aiming position.

## Probable Cause

The project contained separate PC and mobile camera-control scripts. The PC FirstPersonLook script was not originally restricted to the Editor/standalone platform, allowing its mouse input logic to participate alongside the Android MobileLook system.

The earlier mobile camera implementation also used smoothing, which made precise aiming more difficult.

## Correction Applied

FirstPersonLook was restricted using platform-specific conditional compilation so that its mouse-look code runs only in the Unity Editor or standalone PC builds.

MobileLook was assigned responsibility for Android touch aiming.

The mobile camera control was also simplified to use direct touch movement without continued smoothing or camera inertia.

## Result After Correction

The Android build was tested again. The camera stopped drifting, remained in position after touch movement ended, and the player was able to aim properly.

## Additional Work

Touch sensitivity may be adjusted in the future for different Android screen sizes and player preferences.

---

# Issue 2 — Life Decreased Without Player Throwing

## Actual Issue

After starting the game, the player's lives decreased from 3 to 2 even though the player had not thrown a slipper.

## Steps to Reproduce

1. Start the game.
2. Do not press the Throw button.
3. Wait several seconds.
4. Observe the Lives display.

## Expected Result

The player should remain at 3 lives until an actual thrown slipper is registered as a miss.

## Actual Result

One life was automatically removed even though the player had not performed a throw.

## Probable Cause

A Slipper GameObject had been permanently placed inside GameScene during development.

The Slipper script automatically checked whether the projectile had successfully hit the Can after its configured lifetime. Because the scene Slipper had not hit the target, it was incorrectly processed as a missed throw.

## Correction Applied

The permanent Slipper GameObject was removed from the scene.

The Slipper remains as a prefab and is instantiated only by SlipperThrower when the player actually performs a throw.

## Result After Correction

The game was tested again. The player remained at 3 lives when starting the game and no life was deducted until an actual thrown slipper missed.

---

# Issue 3 — Android Build Class Layout Error

## Actual Issue

An Android build attempt produced a class-layout-related error after script fields and GameManager configuration had been modified.

## Expected Result

Unity should compile the project and generate the Android APK successfully.

## Actual Result

The Android build could not complete successfully during that attempt.

## Probable Cause

Generated Unity cache data became inconsistent with the updated script/component layout.

## Correction Applied

The Unity project was saved and closed. Generated cache folders were cleared while preserving the important project folders such as Assets, Packages, and ProjectSettings.

Unity was reopened and regenerated the required cache data. The scene and component references were then verified before another build attempt.

## Result After Correction

The Android build completed successfully and the APK was generated.

---

# Issue 4 — Background Music Not Audible During Testing

## Actual Issue

The background music AudioSource and audio clip were configured, but no background music could be heard during Unity testing.

## Expected Result

Background music should play during the game.

## Actual Result

The game appeared to be playing normally but the background music could not be heard.

## Probable Cause

Audio was muted in the Unity Game view during testing.

## Correction Applied

The Game view audio control was enabled and the AudioSource configuration was checked.

## Result After Correction

Background music became audible and played correctly during testing.

---

# Troubleshooting Summary

The project encountered issues involving mobile input, scene objects, Unity-generated project data, and audio testing.

Each issue was investigated by identifying the actual behavior, comparing it with the expected behavior, determining a probable cause, applying a correction, and testing the result again.

The final Android build was tested after the corrections were applied.