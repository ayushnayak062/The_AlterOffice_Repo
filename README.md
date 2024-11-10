# The_AlterOffice_Repo_Assignment
 
# Unity Weapon System

## Overview

This Unity-based Weapon System allows you to create and manage multiple types of weapons with distinct behaviors (Fire, Ice, Electric). It follows SOLID principles and is highly extensible, making it easy to add new weapons, behaviors, and features. The system also integrates a UI manager for updating weapon info and an audio manager for handling sound effects.

## Features

- **Weapon Types**: Supports Fire, Ice, Electric weapons with distinct effects.
- **Weapon Behaviors**: Includes Fire and Reload behaviors, with flexibility to add more.
- **UI Integration**: Displays weapon icons, names, and descriptions in the UI.
- **Audio System**: Plays weapon sounds (fire, reload, empty magazine).
- **Flexible Design**: Easy to add new weapon types and behaviors through the Factory and Strategy patterns.

## Setup

1. **Import into Unity**: Drag and drop the scripts into your Unity project.
2. **Create Weapon Data**: Create new weapon data objects by right-clicking in the Project window and selecting `Create -> ScriptableObjects -> WeaponData`.
3. **Assign Weapon Data**: Assign weapon data to your weapons in the scene.
4. **Configure UI**: Use the `UIManager` to link UI elements (weapon icon, name, description).
5. **Test**: Play and switch between weapons using the `WeaponManager`.

## How to Use

1. **Switch Weapon**: Use the `WeaponManager` to switch between different weapons.
2. **Fire Weapon**: Use the `Fire` method to fire the currently equipped weapon.
3. **Reload Weapon**: The `WeaponManager` will handle reloading when ammo is depleted.

## License

MIT License
