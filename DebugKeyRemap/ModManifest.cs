using System.Reflection;
using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework.Input;

namespace DebugKeyRemap;

public class ModManifest : IModManifest {
	public IEnumerable<DependencyEntry> Dependencies { get { yield break; } }

	public DirectoryInfo? GameRootFolder { get; set; }

	public ILogger? Logger { get; set; }

	public DirectoryInfo? ModRootFolder { get; set; }

	public string Name => typeof(ModManifest).ILName();

	public void BootMod(IModLoaderContact contact) {
		Harmony patcher = new(this.Name);
		patcher.PatchAll(typeof(ModManifest).Assembly);
	}
}
