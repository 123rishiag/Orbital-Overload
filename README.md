# Orbital Overload

## Overview
"Orbital Overload" is an exciting 2D arcade game where players control an orb that can shoot smaller orbs to destroy incoming enemies. The objective is to survive as long as possible, utilizing various power-ups to enhance gameplay.

## Controls
- **Movement**: W/A/S/D
- **Shooting**: Hold the left mouse button for constant shooting
- **Direction**: Move the mouse to change the shooting direction

## Features
- **Smooth Player Movement**: Control your orb using W/A/S/D keys.
- **Constant Shooting Mechanism**: Hold the left mouse button to shoot continuously.
- **Mouse Direction Control**: Aim and shoot in the direction of the mouse pointer.
- **Diverse Power-ups**:
  - **HealthPick**: Restores health.
  - **HomingOrbs**: Orbs that home in on enemies.
  - **RapidFire**: Increases firing rate.
  - **Shield**: Provides temporary invincibility.
  - **SlowMotion**: Slows down time for better maneuvering.
  - **Teleport**: Teleports the player to a random location.

## Theme
### Controlling Many
The theme "Controlling Many" is reflected in the gameplay of "Orbital Overload" by allowing the player to control a central orb that can shoot multiple smaller orbs to defeat incoming enemies. The player manages the movement and direction of these orbs to survive and score points. The diverse power-ups further enhance the theme by providing various ways to control and influence the orbs' behavior and effectiveness in combat.

## Game Loop
- **Start**: Begin the game from the main menu.
- **Pause**: Pause the game using the escape key.
- **Restart**: Restart the game from the pause or game over menu.
- **Game Over**: Triggered when the player's orb is destroyed.

## UI Elements
- **Lobby Screen**: Contains buttons for starting the game, quitting, and muting/unmuting sounds.
- **Game Screen**: Displays health, score, and active power-ups.
- **Pause Screen**: Allows players to resume or restart the game or return to the main menu.
- **Game Over Screen**: Provides options to restart the game or go back to the main menu.

## Code Structure
### BulletController.cs
Manages the behavior of the bullets, including movement, collision, and homing functionality.

### PlayerController.cs
Handles player movement, shooting, power-ups, and interactions.

### EnemyController.cs
Manages enemy movement, shooting, and interactions with the player.

### EnemyManager.cs
Spawns enemies at regular intervals, ensuring they appear away from the player.

### PowerUpController.cs
Defines the behavior of power-ups, including activation and destruction.

### PowerUpSpawner.cs
Spawns power-ups at regular intervals around the player.

### GameManager.cs
Controls the game state, including pausing, restarting, and game over scenarios.

### LobbyController.cs
Manages the lobby menu interactions, including starting and quitting the game.

### SoundManager.cs
Handles all sound effects and background music, including muting functionality.

## Setting Up the Project
1. Clone the repository.
```bash
   git clone https://github.com/123rishiag/Orbital-Overload.git 
```
2. Open the project in Unity.
3. Ensure all scripts are correctly attached to their respective game objects.
4. Set up the UI elements as described in the provided code.

## Video Demo

[Watch the Video Demo](https://www.loom.com/share/196ce76c10e4471f8994ede99576c10e?sid=2a4ed09a-ced2-4e90-a977-f30deaa45d51)

## Play Link

[Play the Game](https://outscal.com/narishabhgarg/game/play-orbital-overload-game)
