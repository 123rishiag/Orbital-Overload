# Orbital Overload

## Overview
An engaging 2D arcade game where players control a central orb, shooting smaller orbs to destroy waves of enemies. The game employs several design patterns, including **Service Locator**, **Dependency Injection**, **Model-View-Controller (MVC)**, **Observer Pattern**, **Object Pooling**, and **State Machine** alongside **Scriptable Objects** for modular data management and a **New Input System** for seamless and precise controls.

---

## Architectural Overview

Below is the block diagram illustrating the architecture:

![Architectural Overview](Documents/block_diagram.png)

---

## Gameplay Elements

### Controls
- **Movement**: Use W/A/S/D keys for precise navigation.
- **Shooting**: Hold the left mouse button for continuous firing.
- **Direction**: Move the mouse to aim and shoot.

### Input System
The game uses Unity's New Input System for highly configurable and responsive controls:

| **Action**         | **Key/Input**                     |
|---------------------|-----------------------------------|
| Move                | W, A, S, D keys                  |
| Shoot               | Left mouse button (Hold)         |
| Aim                 | Mouse pointer                    |
| Pause Menu          | Escape key during gameplay       |

### Power-ups
| **Power-Up**   | **Effect**                                    |
|-----------------|-----------------------------------------------|
| HealthPick      | Restores health.                             |
| HomingOrbs      | Launches orbs that home in on enemies.       |
| RapidFire       | Temporarily increases firing speed.          |
| Shield          | Grants temporary invincibility.             |
| SlowMotion      | Slows down time for better maneuvering.      |
| Teleport        | Instantly relocates the player orb.          |

### Game States
| **Game State**   | **Description**                          |
|-------------------|------------------------------------------|
| Game_Start        | Initializes the game and its resources. |
| Game_Menu         | Displays the main menu.                 |
| Game_Play         | Active gameplay state.                  |
| Game_Pause        | Halts gameplay when paused.             |
| Game_Restart      | Resets the game to replay.              |
| Game_Over         | Triggers when the player orb is destroyed. |

### Actor Types
| **Actor Type**    | **Description**                          |
|-------------------|------------------------------------------|
| Player            | The main player-controlled orb.         |
| Normal_Enemy      | A standard enemy with moderate speed.   |
| Fast_Enemy        | A faster-moving enemy.                  |

### Projectile Types
| **Projectile Type** | **Description**                           |
|---------------------|-------------------------------------------|
| Normal_Bullet       | A straight-firing bullet.                |
| Homing_Bullet       | A bullet that tracks enemies.            |

---

## Design Patterns and Programming Principles

### Service Locator
Centralizes access to shared services such as `GameService`, `SoundService`, and `EventService`.

### Dependency Injection
Decouples services for flexibility and maintainability.

### Model-View-Controller (MVC)
Separates concerns for data, visuals, and interactions:
- **Controller**: Coordinates interactions between the model and view.
- **Model**: Handles data and game logic.
- **View**: Manages visuals and rendering.

### Observer Pattern
Enables event-driven communication between game elements, ensuring modular design.

### Object Pooling
Optimizes memory usage for actors, power-ups, projectiles, and bullets.

### State Machine
Manages transitions between states like `Game_Start`, `Game_Play`, and `Game_Over`.

### Scriptable Objects
Stores reusable configurations for power-ups, projectiles, and actors etc.

---

## Services and Components

**GameService**: Centralized service for managing the game's core mechanics, including game state transitions and controller management.
- **Game Controller**: Initializes and manages the main gameplay components, including the state machine and input systems.
- **GameGenericStateMachine**: A generic state machine designed to manage state transitions for the `GameController`, allowing future extensions.
  - **GameStateMachine**: A specific implementation of `GameGenericStateMachine` that governs the overall game flow.
- **IGameState**: Interface defining common behaviors for all game states.
    - **GameStartState**: Prepares the game and initializes resources.
    - **GameMenuState**: Displays the main menu.
    - **GamePlayState**: Manages active gameplay.
    - **GamePauseState**: Freezes gameplay for a pause.
    - **GameOverState**: Handles game-over logic.
    - **GameRestartState**: Resets the game for replay.

**EventService**: Manages event-driven communication across services.
   - **EventController**: Inherits from EventController to manage event registrations and notifications.

**SoundService**: Manages sound effects and music for immersive gameplay.
- **SoundType**: Enum categorizing sound effects (e.g., shooting, explosions).
- **SoundConfig**: Stores configuration for audio playback.

**UIService**: Manages user interface interactions and HUD updates dynamically.
- **UIController**: Handles menu interactions and HUD logic.
- **UIView**: Manages the visual representation of UI elements.

**InputService**: Processes and manages player inputs using Unity's New Input System for precise control of movement and shooting.

**VFXService**: Controls visual effects for immersive feedback and dynamic visuals.
- **VFXController**: Governs the logic of visual effects.
- **VFXModel**: Stores runtime data for each effect.
- **VFXView**: Manages the rendering of effects.
- **VFXType**: Enum defining various effect types (e.g., explosions, screen shakes).
- **VFXConfig**: Configures visual effect properties.
- **VFXPool**: Optimizes performance by reusing VFX objects.

**CameraService**: Handles camera movements and effects such as screen shakes.
- **CameraShakeType**: Enum defining shake patterns.
- **CameraConfig**: Stores camera-related configurations.

**ActorService**: Manages logic related to players and enemies.
- **ActorType**: Enum defining types of actors (Player, Normal Enemy, Fast Enemy).
- **ActorConfig**: Stores properties like speed and health for actors.
- **ActorController**: Governs behavior for actors.
  - **PlayerActorController**: Specialized logic for the player orb.
  - **EnemyActorController**: Manages enemy actions and interactions.
- **ActorModel**: Stores runtime data for actors.
- **ActorView**: Handles visual representation of actors.
- **ActorPool**: Reuses actor instances to enhance performance.

**ProjectileService**: Manages all logic related to bullets and other projectiles.
- **ProjectileType**: Enum defining projectile types (Normal Bullet, Homing Bullet).
- **ProjectileConfig**: Stores configuration for projectile behavior.
- **ProjectileController**: Handles general projectile logic.
  - **HomingBulletProjectileController**: Adds homing behavior to bullets.
- **ProjectileModel**: Manages runtime data for projectiles.
- **ProjectileView**: Manages visual representation of projectiles.
- **ProjectilePool**: Reuses projectile instances to save resources.

**PowerUpService**: Handles spawning and management of power-ups to enhance gameplay.
- **PowerUpType**: Enum defining types of power-ups (e.g., HealthPick, HomingOrbs).
- **PowerUpConfig**: Configurable properties for power-ups, such as duration, effect magnitude, and spawn frequency.
- **PowerUpController**: Governs logic for general power-up behavior, such as activation and deactivation.
    - **HealthPickPowerUpController**: Restores the player’s health by a specified amount.
    - **HomingOrbsPowerUpController**: Enables the player to fire homing projectiles for a limited time.
    - **RapidFirePowerUpController**: Temporarily increases the player’s firing rate, allowing faster shooting.
    - **SlowMotionPowerUpController**: Slows down the game’s time scale, enhancing strategic gameplay.
    - **ShieldPowerUpController**: Provides the player with temporary invincibility, absorbing all damage.
    - **TeleportPowerUpController**: Instantly moves the player orb to a random location on the map.
- **PowerUpModel**: Stores runtime data for individual power-ups, such as active duration and cooldown state.
- **PowerUpView**: Renders the visual aspects of power-ups, such as icons or particle effects.
- **PowerUpPool**: Reuses power-up objects to reduce instantiation overhead and optimize performance.

**SpawnService**: Manages the spawning of both enemies and power-ups.
- **SpawnController**: Configures spawn intervals and locations dynamically.

**Utilities**: Provides generic, reusable utilities for better performance and scalability.
- **GenericObjectPool**: Implements object pooling for actors, projectiles, power-ups, and VFX.

---

## Development Workflow

### Branches

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
| OnPlaySoundEffectEvent      | Plays a specific sound effect.                    |
| OnGetUIControllerEvent      | Retrieves the active `UIController`.              |
| OnDoShakeScreenEvent        | Triggers a screen shake effect.                   |
| OnCreateVFXEvent            | Creates visual effects at specified locations.    |

---

## Script and Asset Hierarchy

### Scripts
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

### Assets
- **Prefabs**: Self-created using Unity tools and ChatGPT guidance.
- **Art**: Designed using ChatGPT-generated ideas.
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

[Watch the Gameplay Demo](https://www.loom.com/share/6e274a2d289749569f48b74d7e95af3d?sid=e2eb7715-5eb0-41ec-8bd2-5f8cf835a3f0)  
[Watch the Architecture Explanation](https://www.loom.com/share/eac8c7cdcbd34a48b9aa3e2cda2dc04d?sid=1e0ced44-a7f9-4aa3-a219-8c99d92679e8)

---

## Play Link

[Play the Game](https://outscal.com/narishabhgarg/game/play-chest-system-simulator-game)

---