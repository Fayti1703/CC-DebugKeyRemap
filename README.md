# Cobalt Core Debug Key Remapper

This is a very simple mod for Cobalt Core that changes the key bound to the debug menu.

## Dependencies

* An unbundled version of Cobalt Core. 
  You can obtain this by [buying the game on Steam](https://store.steampowered.com/app/2179850/Cobalt_Core/), then running the [SingleFileExtractor](https://github.com/Droppers/SingleFileExtractor) over the main game binary.
* [EWanderer's Cobalt Core Mod Loader](https://github.com/EWanderer/CobaltCoreModLoader).

## Building

1. Copy `Path.template.props` to `Path.props` and add the appropriate paths for the dependencies.
2. Build in your IDE or run `dotnet build`.
3. Ignore the dependency warnings.

The compiled mod will appear in the `DebugKeyRemap/launch` folder.
Make sure to load from there and not the `bin` folder to prevent assembly loading issues.
