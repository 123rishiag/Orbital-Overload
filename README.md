# Orbital Overload

## Overview
Orbital Overload is a thrilling 2D arcade game where players command a tank, battling waves of enemy tanks to survive the battlefield 
while collecting scattered power-ups to increase survival rate.
The game employs several design patterns, including **Service Locator**, **Dependency Injection**, **Model-View-Controller (MVC)**, **Observer Pattern**, 
**Object Pooling**, and **State Machine** alongside **Scriptable Objects** for modular data management and a **New Input System** 
for seamless and precise controls.

---

## Architectural Overview

Below is the block diagram illustrating the architecture:

![Architectural Overview](Documents/block_diagram.png)

---

## Gameplay Elements

### **1. Controls**
- **Movement**: Use W/A/S/D keys for precise navigation.
- **Shooting**: Hold the left mouse button for continuous firing.
- **Direction**: Move the mouse to aim and shoot.

### **2. Game States**
| **Game State**   | **Description**                          |
|-------------------|------------------------------------------|
| Game_Start        | Initializes the game and its resources. |
| Game_Menu         | Displays the main menu.                 |
| Game_Play         | Active gameplay state.                  |
| Game_Pause        | Halts gameplay when paused.             |
| Game_Restart      | Resets the game to replay.              |
| Game_Over         | Triggers when the player is destroyed.  |

### **3. Power-ups**
| **Power-Up**   | **Effect**                                    |
|-----------------|-----------------------------------------------|
| HealthPick      | Restores health.                             |
| HomingOrbs      | Launches orbs that home in on enemies.       |
| RapidFire       | Temporarily increases firing speed.          |
| Shield          | Grants temporary invincibility.             |
| SlowMotion      | Slows down time for better maneuvering.      |
| Teleport        | Instantly relocates the player.             |

### **4. Actor Types**
| **Actor Type**    | **Description**                          |
|-------------------|------------------------------------------|
| Player            | The main player-controller.             |
| Weak_Enemy        | A slow-moving enemy with low shooting speed. |
| Normal_Enemy      | An enemy with balanced movement and shooting speed. |
| Fast_Enemy        | A quick-moving enemy with high shooting speed.      |
| Boss_Enemy        | The fastest-moving enemy with the highest shooting speed.|

### **5. Projectile Types**
| **Projectile Type** | **Description**                           |
|---------------------|-------------------------------------------|
| Normal_Bullet       | A straight-firing bullet.                |
| Homing_Bullet       | A bullet that tracks enemies.            |

### **6. Input System**
The game uses Unity's New Input System for highly configurable and responsive controls:

| **Action**         | **Key/Input**                     |
|---------------------|-----------------------------------|
| Move                | W, A, S, D keys                  |
| Shoot               | Left mouse button (Hold)         |
| Aim                 | Mouse pointer                    |
| Pause Menu          | Escape key during gameplay       |

---

## Design Patterns and Programming Principles

### 1. **Service Locator**  
Centralizes access to shared services such as `UIService`, `SoundService`, and `EventService`.

### 2. **Dependency Injection**  
Decouples services for flexibility and maintainability.

### 3. **Model-View-Controller (MVC)**  
Separates concerns for data, visuals, and interactions:
- **Controller**: Coordinates interactions between the model and view.
- **Model**: Handles data and game logic.
- **View**: Manages visuals and rendering.

### 4. **Observer Pattern**  
Enables event-driven communication between game elements, ensuring modular design.

### 5. **Object Pooling**  
Optimizes memory usage for actors, power-ups, projectiles, and bullets.

### 6. **State Machine** 
Manages transitions between states like `Game_Start`, `Game_Play`, and `Game_Over`.

### 7. **Scriptable Objects**  
Stores reusable configurations for power-ups, projectiles, and actors etc.

---

## Services and Components

1. **GameService**: Manages and fetches the core game components and initialize the `Game Controller`.
   - **Game Controller**: Centralized service for managing the game's core mechanics, including game state transitions and controller management.
   - **GameGenericStateMachine**: A generic state machine designed to manage state transitions for the `GameController`, allowing future extensions.
     - **GameStateMachine**: A specific implementation of `GameGenericStateMachine` that governs the overall game flow.
   - **IGameState**: Interface defining common behaviors for all game states.
     - **GameStartState**: Prepares the game.
     - **GameMenuState**: Displays the main menu.
     - **GamePlayState**: Manages active gameplay.
     - **GamePauseState**: Freezes gameplay for a pause.
     - **GameRestartState**: Resets the game for replay.
     - **GameOverState**: Handles game-over logic.

2. **EventService**: Manages event-driven communication across services.
   - **EventController**: Inherits from EventController to manage event registrations and notifications.

3. **SoundService**: Manages sound effects and music for immersive gameplay.
   - **SoundType**: Enum categorizing sound effects (e.g., shooting, hurt).
   - **SoundConfig**: Stores configuration for audio playback, such as volume, and looping settings.
   - **SoundPool**: A pool of audio sources to efficiently manage and reuse sound objects, reducing overhead when playing multiple sounds simultaneously.
   - **SoundController**: Handles sound-related logic, such as playing, pausing sounds based on game events.
   - **SoundModel**: Stores the metadata for individual sounds, like Sound Type, volume, and clip properties.
   - **SoundView**: Responsible for interacting with Unity's audio components (e.g., AudioSource and AudioClip) to render the audio in the game environment.

4. **UIService**: Manages user interface interactions and HUD updates dynamically.
   - **UIController**: Handles menu interactions and HUD logic.
   - **UIView**: Manages the visual representation of UI elements.

5. **InputService**: Processes and manages player inputs using Unity's New Input System for precise control of movement and shooting.

6. **VFXService**: Controls visual effects for immersive feedback and dynamic visuals.
   - **VFXType**: Enum defining various effect types (e.g., screen shakes).
   - **VFXConfig**: Configures visual effect properties.
   - **VFXPool**: Optimizes performance by reusing VFX objects.
   - **VFXController**: Governs the logic of visual effects.
   - **VFXModel**: Stores runtime data for each effect.
   - **VFXView**: Manages the rendering of effects.

7. **CameraService**: Handles camera movements and effects such as screen shakes.
   - **CameraShakeType**: Enum defining shake patterns.
   - **CameraConfig**: Stores camera-related configurations.

8. **ActorService**: Manages logic related to players and enemies.
   - **ActorType**: Enum defining types of actors (Player, Normal Enemy, Fast Enemy).
   - **ActorConfig**: Stores properties like speed and health for actors.
   - **ActorPool**: Reuses actor instances to enhance performance.
   - **ActorController**: Governs behavior for actors.
     - **PlayerActorController**: Specialized logic for the player.
     - **EnemyActorController**: Manages enemy actions and interactions.
   - **ActorModel**: Stores runtime data for actors.
   - **ActorView**: Handles visual representation of actors.

9. **ProjectileService**: Manages all logic related to bullets and other projectiles.
   - **ProjectileType**: Enum defining projectile types (Normal Bullet, Homing Bullet).
   - **ProjectileConfig**: Stores configuration for projectile behavior.
   - **ProjectilePool**: Reuses projectile instances to save resources.
   - **ProjectileController**: Handles general projectile logic.
     - **HomingBulletProjectileController**: Adds homing behavior to bullets.
   - **ProjectileModel**: Manages runtime data for projectiles.
   - **ProjectileView**: Manages visual representation of projectiles.

10. **PowerUpService**: Handles management of power-ups to enhance gameplay.
    - **PowerUpType**: Enum defining types of power-ups (e.g., HealthPick, HomingOrbs).
    - **PowerUpConfig**: Configurable properties for power-ups, such as duration, and effect magnitude.
    - **PowerUpPool**: Reuses power-up objects to reduce instantiation overhead and optimize performance.
    - **PowerUpController**: Governs logic for general power-up behavior, such as activation and deactivation.
      - **HealthPickPowerUpController**: Restores the player’s health by a specified amount.
      - **HomingOrbsPowerUpController**: Enables the player to fire homing projectiles for a limited time.
      - **RapidFirePowerUpController**: Temporarily increases the player’s firing rate, allowing faster shooting.
      - **ShieldPowerUpController**: Provides the player with temporary invincibility, absorbing all damage.
      - **SlowMotionPowerUpController**: Slows down the game’s time scale, enhancing strategic gameplay.
      - **TeleportPowerUpController**: Instantly moves the player to a random location on the map.
    - **PowerUpModel**: Stores runtime data for individual power-ups, such as active duration and cooldown state.
    - **PowerUpView**: Renders the visual aspects of power-ups, such as icons or particle effects.

11. **SpawnService**: Manages the spawning of both enemies and power-ups.
    - **SpawnController**: Configures spawn intervals and locations dynamically.

12. **Utilities**: Provides generic, reusable utilities for better performance and scalability.
    - **GenericObjectPool**: Manages object pooling for optimal memory usage and performance.

---

## Development Workflow

| **Branch**                  | **Feature**                                      |
|-----------------------------|--------------------------------------------------|
| Branch1-SettingUp           | Project setup with folders for scripts, assets, and scenes. |
| Branch2-Basics              | Basic player mechanics and movement system.      |
| Branch3-Powerups            | Initial implementation of power-ups with effects.|
| Branch4-ScoreSystem         | Added scoring mechanics based on enemy kills.    |
| Branch5-LobbyAndUI          | Developed lobby menu and basic in-game UI.       |
| Branch6-HealthSystem        | Integrated player health system with UI feedback.|
| Branch7-SoundSystem         | Added sound effects for shooting, collisions, and power-ups. |
| Branch8-Polish              | Enhanced visuals, animations, and user feedback. |
| Branch9-Documentation       | Drafted project documentation and README structure.|
| Branch10-ServiceLocator-DI  | Centralized services with Dependency Injection.  |
| Branch11-MVC                | Refactored systems into the MVC architecture.    |
| Branch12-ActorSystem        | Merged player and enemy logic inherting from parent 'actor'.        |
| Branch13-AddUIMetrics       | Added metrics like score, health, and power-ups to the UI. |
| Branch14-ProjectileSystem   | Developed general projectile behaviors and added homing projectiles. |
| Branch15-SpawnSystem        | Spawn logic for both enemies and power-ups.      |
| Branch16-InputSystem        | Modularized input handling for player controls.  |
| Branch17-ObjectPooling      | Added pooling for actors, power-ups, projectiles, and bullets. |
| Branch18-PowerUpSystem      | Made power-up functionality more modular, creating sub-controllers.     |
| Branch19-StateMachine       | Integrated state machine for managing game states.|
| Branch20-ObserverPattern    | Added event-driven communication using Observer Pattern. |
| Branch21-Repolish           | Final refinements and optimizations.             |

---

## Events

| **Event**                   | **Description**                                   |
|-----------------------------|---------------------------------------------------|
| OnGetGameControllerEvent    | Returns the `GameController` instance.            |
| OnPlaySoundEvent            | Plays a specific sound.                           |
| OnGetUIControllerEvent      | Retrieves the active `UIController`.              |
| OnDoShakeScreenEvent        | Triggers a screen shake effect.                   |
| OnCreateVFXEvent            | Creates visual effects at specified locations.    |

---

## Script and Asset Hierarchy

1. **Scripts**:
   - **Main**: Core game mechanics and states.
   - **Actor**: Player and enemy behaviors.
   - **Projectile**: Bullet logic and pooling.
   - **PowerUp**: Power-Up system.
   - **Camera**: Camera maintenance.
   - **UI**: Menus and in-game UI.
   - **VFX**: Visual effects management.
   - **Sound**: Audio playback.
   - **Event**: Event-based communication.
   - **Spawn**: Spawn system.
   - **Input**: Decoupled new input system.
   - **Utility**: Game utilities.

2. **Assets**:
   - **Prefabs**: Self-created using Unity tools and ChatGPT guidance.
   - **Art**: Art is downloaded from www.kenney.nl.
   - **Sounds**: Royalty-free sources.

---

## Setting Up the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/123rishiag/Orbital-Overload.git
   ```
2. Open the project in Unity.

---

## Video Demo

[Watch the Gameplay Demo](https://www.loom.com/share/e8714fc825e1496d8441aa05ab8b203a?sid=4ebda7df-a3f2-441b-8cf9-c914794b5fdd)  
[Watch the Architecture Explanation](https://www.loom.com/share/eac8c7cdcbd34a48b9aa3e2cda2dc04d?sid=1e0ced44-a7f9-4aa3-a219-8c99d92679e8)

---

## Play Link

[Play the Game](https://123rishiag.github.io/Orbital-Overload/)

---