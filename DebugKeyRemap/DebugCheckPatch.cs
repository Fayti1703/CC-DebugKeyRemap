using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using JetBrains.Annotations;
using Microsoft.Xna.Framework.Input;
using ILInstruction = Mono.Cecil.Cil.Instruction;

namespace DebugKeyRemap;

[HarmonyPatch]
public static class DebugCheckPatch {

	[HarmonyPatch(typeof(EditorShortcuts), nameof(EditorShortcuts.DebugKeys))]
	[HarmonyTranspiler]
	[UsedImplicitly]
	public static IEnumerable<CodeInstruction> PreHotKeys(IEnumerable<CodeInstruction> iseq) {
		using IEnumerator<CodeInstruction> iter = iseq.GetEnumerator();

		while(iter.MoveNext()) {
			if(iter.Current.opcode != OpCodes.Ldc_I4 || (int) iter.Current.operand != (int) Keys.OemTilde) {
				yield return iter.Current;
				continue;
			}

			CodeInstruction candidate = iter.Current;
			if(!iter.MoveNext()) {
				yield return candidate;
				break;
			}

			if(iter.Current.opcode != OpCodes.Ldc_I4_1) {
				yield return candidate;
				yield return iter.Current;
				continue;
			}

			CodeInstruction n1 = iter.Current;

			if(!iter.MoveNext()) {
				yield return candidate;
				yield return n1;
				break;
			}

			if(iter.Current.opcode != OpCodes.Call || (MethodInfo) iter.Current.operand != typeof(Input).Rd().Mth("GetKeyDown")) {
				yield return candidate;
				yield return n1;
				yield return iter.Current;
			}

			candidate.operand = (int) Keys.OemBackslash;
			yield return candidate;
			yield return n1;
			yield return iter.Current;
			break;
		}

		while(iter.MoveNext())
			yield return iter.Current;
	}
}
