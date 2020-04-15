using HarmonyLib;
using Legend;
using UnityEngine;

namespace CoreLoader.LevelPatches
{
    [HarmonyPatch(typeof(Level), "HasFellowship", MethodType.Getter)]
    class LevelPatcher
    {
        [HarmonyPostfix]
        static void postfixFellowship(ref bool __result)
        {
            __result = true;
        }
    }
}
