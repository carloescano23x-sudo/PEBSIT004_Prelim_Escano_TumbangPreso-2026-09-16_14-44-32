# Tumbang Preso: First-Person Challenge
## Final Test Record

The following tests were performed to verify the main functions of the Unity project and Android build.

| Test Condition | Expected Result | Actual Result | Status | Action or Finding |
|---|---|---|---|---|
| Project opens | Unity project should open without critical errors preventing development or testing. | Project opened successfully and GameScene was accessible. | PASSED | Project and scene were successfully loaded. |
| Main scene loads | GameScene should display the environment, player, target, and interface. | GameScene loaded correctly with the environment, Can, player, and game interface. | PASSED | Main gameplay scene works correctly. |
| Ready / Start screen | Ready screen should appear before gameplay and Start Game should begin the round. | Ready screen appeared and Start Game successfully changed the game to the Playing state. | PASSED | Start sequence works correctly. |
| Player input - PC | Mouse movement should control the first-person camera during Unity Editor testing. | Mouse aiming successfully controlled the camera in the Unity Editor. | PASSED | FirstPersonLook works for Editor/PC testing. |
| Player input - Android | Touch dragging should allow the player to aim without unwanted camera movement. | Touch aiming worked correctly after separating PC and Android camera controls. | PASSED | Mobile camera control was corrected and tested on Android. |
| Slipper throwing | Pressing the Throw button should create and launch a slipper toward the crosshair. | Slipper was created and thrown toward the aiming position. | PASSED | Throw system works correctly. |
| Gameplay hit event | A slipper that hits the Can should register a successful hit. | Can detected the slipper collision and registered the successful hit. | PASSED | Hit detection works correctly. |
| Score update | Successful hit should add 10 points to the current score. | Score increased by 10 after a successful hit. | PASSED | Score system works correctly. |
| Miss / lives update | A missed slipper should remove exactly one life. | One life was removed after an unsuccessful throw. | PASSED | Miss and life deduction work correctly. |
| Can reset | Can should move/reset after a valid hit. | Can moved to another available target position after being hit. | PASSED | Dynamic Can positioning works correctly. |
| Difficulty update | Difficulty should change according to the player's score. | Difficulty changed based on the configured score thresholds. | PASSED | Difficulty system works correctly. |
| Round timer | Timer should count down during gameplay and end the round at zero. | Timer counted down during gameplay and triggered Game Over when time expired. | PASSED | Timer works correctly. |
| Pause | Pause button should stop active gameplay and display the Pause screen. | Pause screen appeared and active gameplay was paused. | PASSED | Pause system works correctly. |
| Resume | Resume should return the player to active gameplay. | Game returned to the Playing state after Resume was selected. | PASSED | Resume works correctly. |
| Game Over - lives | Game should end when lives reach zero. | Game Over was triggered when all lives were lost. | PASSED | Lives-based Game Over works correctly. |
| Game Over - timer | Game should end when the timer reaches zero. | Game Over was triggered when the timer expired. | PASSED | Timer-based Game Over works correctly. |
| Play Again | Player should be able to begin another game after Game Over. | Play Again successfully started another game. | PASSED | Restart functionality works correctly. |
| Audio | Background music and gameplay sound effects should be audible. | Background music and gameplay sounds played correctly after audio configuration was corrected. | PASSED | Audio system works correctly. |
| Android build | Unity should successfully generate an Android APK. | TumbangPreso.apk was successfully generated. | PASSED | Android build completed successfully. |
| Android installation | APK should install and launch on the Android test device. | APK installed and the game successfully launched on the Android device. | PASSED | Android deployment works correctly. |
| Test after camera correction | Camera should stop immediately when the player stops dragging and should remain controllable. | Camera no longer drifted and aiming became controllable after the input scripts were separated by platform. | PASSED | Camera correction successfully resolved the issue. |

---

## Test Summary

The main gameplay functions were successfully tested in both the Unity Editor and the Android build. The project successfully performs the required player interaction, gameplay event, status updates, state transitions, and Android build/export.

Problems identified during development were corrected and tested again before the final version was prepared.