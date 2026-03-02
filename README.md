# Glider Revamp

## Core functionality

An attempt to enhance the glider physics system.
The mod replaces Vintage Story's vanilla glider mechanics with a simple and probably more fun flight model that gives players more control and realistic gliding behavior. While preventing tower freeways.

## Key Features

- Speed-Based Flight Dynamics
  - Players must maintain a minimum stall speed (default 6 m/s) to keep gliding
  - Below 66% of stall speed, the player automatically stops gliding and free falls
  - Speed is continuously displayed in the HUD (optional)
- Energy Management System
  - Drag Coefficient: Air resistance gradually slows the glider (0.1 = 10% speed loss per second)
  - Climb Efficiency: Climbing upward costs energy (0.1 = 1 meter climbed costs 0.1 m/s)
  - Prevents infinite flying by making sustained gliding impossible without careful energy management
- Directional Control
  - Turn Rate: Players can rotate their flight direction smoothly (configurable degrees per second)
- Terminal Velocity
  - Players can't achieve unrealistic speeds; maximum speed is capped at Vintage Story's terminal velocity (40 m/s)

## Quality of Life
- Speed Display: Optional HUD element showing current glide speed in m/s
- Activation gate: Glider can only be activated when moving fast enough (above stall speed).
- Fully Configurable: All physics parameters can be adjusted without code changes
