using HarmonyLib;
using Legend;
using UnityEngine;

namespace CoreLoader.CharacterPatches
{
    [HarmonyPatch(typeof(Weapon), "EquipClassWeapon")]
    class WeaponPatcher
    {
        [HarmonyPrefix]
        static bool prefixEquipWeapon(Weapon __instance)
        {
            Player origPlayer = AccessTools.FieldRefAccess<Weapon, Player>("player").Invoke(__instance);
            if ((int)origPlayer.CharacterClassType <= 8)
                __instance.ProjectilePrefab = __instance.ClassProjecilePrefabs[(int)origPlayer.CharacterClassType];
            else
                __instance.ProjectilePrefab = __instance.ClassProjecilePrefabs[(int)CharacterClassType.Fighter];
            return false;
        }
    }
}