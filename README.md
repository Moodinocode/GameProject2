# FRACTURED: Rise of the Damned

A third-person zombie survival game built in Unity. Fight through two levels, find keys to unlock chests, survive waves of zombies spawning around the map, and reach the portal to escape.

**▶ [Play it on itch.io](https://moodino.itch.io/fractured-rise-of-the-damned)**

University game development project, November–December 2025.

## Gameplay

- Move, sprint, and crouch; aim down sights or fire from the hip
- Weapon handling with recoil, spread that widens as you fire, reloading, and limited ammo
- Zombies patrol, notice you, chase, and attack, navigating the level with Unity's NavMesh
- A friendly AI teammate fights alongside you
- Chests unlocked with keys found around the map
- Two levels connected by portals, plus a main menu, pause, credits, and game-over screens
- Save and load progress

## Architecture

The part worth reading. Rather than one large player script, behaviour is split into **state machines**, each with a manager and a set of states inheriting a shared base:

| State machine | States |
|---|---|
| `MovementStates` | Idle, Walk, Run, Crouch |
| `AimStates` | HipFire, Aim |
| `PlayerActions` | Default, Reload |
| `EnemyScripts/States` | Zombie Idle, Patrolling, Chase, Attack |

Each state handles its own enter/update logic and decides when to hand over, so adding a new movement mode or enemy behaviour means adding a state rather than extending a growing `if`/`else` chain.

**Object pooling** (`ObjectPooling/ObjectPooler.cs` with an `IPooledObject` interface) reuses bullets and zombies instead of instantiating and destroying them, which avoids the garbage-collection stutter that repeated `Instantiate`/`Destroy` causes during sustained combat.

**Managers** keep cross-cutting concerns out of gameplay code:

| Manager | Responsibility |
|---|---|
| `AudioManager` | Sound playback, with fatigue handling so repeated zombie noises do not stack up |
| `CanvasManager` | UI state across menus, pause, and HUD |
| `SaveLoadManager` / `SaveSystem` | Persisting and restoring progress |
| `PlayerUiManager` | Health, ammo, and crosshair display |
| `PostProcessingManager` | Camera effects |
| `CursorController` | Cursor lock and visibility, including while paused |

## Project layout

```
Project2/Assets/
├── _Scripts/
│   ├── MovementStates/     Player movement state machine
│   ├── AimStates/          Aiming state machine
│   ├── PlayerActions/      Reload and action state machine
│   ├── EnemyScripts/       Zombie AI, stats, audio, spawning
│   │   └── States/         Zombie behaviour states
│   ├── Weapons/            Firing, ammo, recoil, bloom, crosshair
│   ├── Managers/           Audio, canvas, save/load, UI, cursor
│   ├── ObjectPooling/      Pooling for bullets and enemies
│   ├── TeammateScripts/    Friendly AI
│   ├── ChestScripts/       Chests and keys
│   └── UI Scripts/         Credits, death messages
├── Scenes/                 Main Menu, Level1, Level2, EndGameScene
└── Components/             Models, animations, audio, map kits
```

## Running it

Open `Project2/` in Unity and load `Assets/Scenes/Main Menu.unity`.

The project uses **Git LFS** for models and textures, so clone with LFS installed or the art assets will arrive as pointer files:

```bash
git lfs install
git clone https://github.com/Moodinocode/GameProject2.git
```

On Windows, enable long path support before cloning — some asset paths exceed the default limit:

```bash
git config --global core.longpaths true
```

## Credits

Built by Mohamad Mehdi, with zombie wave spawning contributed by [adamsahelii](https://github.com/adamsahelii).

Third-party assets: weapon models from [devassets.com](http://devassets.com/), plus map and character asset kits. The extracted asset folders are what the project uses.
