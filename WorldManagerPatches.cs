using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace WorldTweaker
{
    class WorldManagerPatches
    {
        [HarmonyPatch(typeof(WorldManager), MethodType.Constructor)]
        public static class PatchWorldSize
        {
            static void Postfix(WorldManager __instance)
            {
                __instance.GetType().GetProperty("worldColumns").SetValue(__instance, Plugin.ModConfig.WorldSize);
                __instance.GetType().GetProperty("worldRows").SetValue(__instance, Plugin.ModConfig.WorldSize);

                Plugin.Log.LogDebug($"patched world size. new width x height: {__instance.worldColumns}x{__instance.worldRows}");
            }
        }

        [HarmonyPatch(typeof(WorldManager), "InitNewGameData")]
        public static class RemoveHardCodedWorldSizePatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {   
                var transpiled = new List<CodeInstruction>(instructions);

                var indicesToRemoveAround = FindColumnsAndRowsSetIndicesIn(transpiled);

                if (indicesToRemoveAround == null)
                {
                    Plugin.Log.LogError("The game code has changed and this version of the mod can no longer tweak the world size");
                }
                else
                {
                    RemoveColumnsAndRowsSettersFrom(transpiled, indicesToRemoveAround);
                }

                return transpiled.AsEnumerable();
            }

            private static Tuple<int, int> FindColumnsAndRowsSetIndicesIn(List<CodeInstruction> instructions)
            {
                var columnsSetIndex = -1;
                var rowsSetIndex = -1;

                for (var i = 0; i < instructions.Count; i++)
                {
                    var opString = instructions[i].operand?.ToString();
                    if (opString == "Void set_worldColumns(Int32)")
                        columnsSetIndex = i;
                    if (opString == "Void set_worldRows(Int32)")
                        rowsSetIndex = i;
                }

                if (columnsSetIndex != -1 && rowsSetIndex != -1)
                    return new Tuple<int, int>(columnsSetIndex, rowsSetIndex);
                else
                    return null;
            }

            private static void RemoveColumnsAndRowsSettersFrom(List<CodeInstruction> instructions, Tuple<int, int> indices)
            {
                var removeFrom = Mathf.Min(indices.first, indices.second) - 1;
                var removeTo = Mathf.Max(indices.first, indices.second) + 1;
                instructions.RemoveRange(removeFrom, removeTo);
            }
        }
    }
}