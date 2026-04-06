# Impactful Medals

A Xenonauts 2 mod that overhauls the medal system so each medal grants unique, thematically appropriate stat bonuses instead of the generic +1 to all attributes.

## Overview

In the base game, all 8 medals grant an identical **+1 to every stat**. This mod replaces those with focused, high-impact bonuses (+5 to +10) that reflect how each medal was earned. Veterans become noticeably powerful, and soldiers develop distinct strengths based on their combat history.

## Medal Bonuses

| Medal | How to Earn | Bonuses |
|-------|-------------|---------|
| **Distinguished Service** | Complete 10 missions | +5 Bravery, +5 TUs, +3 Reflexes |
| **Golden Star** | 5 career kills | +8 Accuracy, +5 Reflexes |
| **Crimson Heart** | Take 30+ HP damage in one mission | +10 HP, +5 Bravery, +3 Strength |
| **Crux Solaris** | 4+ kills in one mission | +5 Accuracy, +5 Reflexes, +5 TUs |
| **Gallantry Citation** | Win with >50% team casualties | +8 Bravery, +5 HP, +5 Strength |
| **Award for Bravery** | Complete a crash site | +5 Reflexes, +5 Accuracy, +3 TUs |
| **Award for Courage** | Complete a terror site | +8 Bravery, +5 TUs, +3 Strength |
| **Award for Valor** | Complete an alien base | +5 Strength, +5 HP, +5 Accuracy |

Psionic Strength is intentionally excluded from medal bonuses — psychic power comes from psi training, not combat valor.

## Emergent Soldier Archetypes

Medals naturally create specializations based on what a soldier has been through:

- **Sharpshooter** (Golden Star + Crux Solaris): +13 Accuracy, +10 Reflexes — dominates reaction fire
- **Tank** (Crimson Heart + Gallantry Citation): +15 HP, +13 Bravery — frontline anchor who never panics
- **Veteran** (Distinguished Service + Award for Courage): +13 Bravery, +10 TUs — steady and efficient

A fully decorated soldier with all 8 medals is an elite combatant. This is intentional — earning all 8 requires significant gameplay investment.

## Installation

### From Pre-built DLL

1. Copy the `impactful_medals` folder to your Xenonauts 2 mods directory:
   - **Linux:** `~/My Games/Xenonauts 2/Mods/`
   - **Windows:** `Documents\My Games\Xenonauts 2\Mods\`
2. Enable **Impactful Medals** in the in-game Mod Manager.

The mod folder should contain:
```
impactful_medals/
  manifest.json
  assembly/common/ImpactfulMedals.dll
  template/common/tooltips/medals/*.json
```

### Building from Source

Prerequisites: .NET SDK with .NET Framework 4.8 targeting pack.

1. Set the `UNITY_EDITOR_PATH` environment variable to your Unity 2022.3 installation, or edit `Directory.Build.props` directly.
2. Build:
   ```
   cd mods/impactful_medals
   dotnet build
   ```
3. The DLL is output to `assembly/common/ImpactfulMedals.dll`.

## How It Works

**Code layer:** Harmony postfix patches on each medal class's `get_Modifiers` property replace the generic +1-all modifiers with per-medal `RangeComponentModifier` lists.

**Data layer:** Tooltip JSON overrides at the same template path as the base game, with fresh `LocalizableGUID`s so the localization cache serves the updated descriptions.

## Compatibility

- Requires Xenonauts 2 with mod support enabled.
- Safe to add to existing saves — medals already earned will use the old bonuses, newly earned medals will use the new ones.
- Compatible with other mods unless they also patch medal `Modifiers` properties.
