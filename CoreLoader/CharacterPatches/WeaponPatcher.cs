using HarmonyLib;
using Legend;
using System;
using UnityEngine;

namespace CoreLoader.CharacterPatches
{
    [HarmonyPatch(typeof(Weapon), "EquipClassWeapon")]
    class EquipClassPatcher
    {
        [HarmonyPrefix]
        static bool prefixEquipWeapon(Weapon __instance)
        {
            var origPlayer = __instance.GetComponent<Player>();
            if ((int)origPlayer.CharacterClassType <= 8)
            {
                __instance.ProjectilePrefab = __instance.ClassProjecilePrefabs[(int)origPlayer.CharacterClassType];
            }
            else
            {
                __instance.ProjectilePrefab = __instance.ClassProjecilePrefabs[(int)CharacterClassType.Fighter];
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(Weapon), "DoShoot", new Type[] { typeof(Direction) })]
    class DoShootPatcher
    {
        [HarmonyPostfix]
        static void postfixDoShoot(Weapon __instance)
        {
            Player player = __instance.GetComponent<Player>();
            if (player.CharacterClassType == (CharacterClassType)9)
            {
                var gameObject = __instance.GetComponent<Projectile>();
                if (gameObject != null && player.Inventory.HasItem(ItemId.Seeker))
                {
                    gameObject.Homing += 10f;
                    Debug.Log("Added extra homing");
                }
            }
        }
    }
}