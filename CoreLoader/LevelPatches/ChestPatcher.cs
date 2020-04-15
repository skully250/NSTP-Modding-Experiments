using System;
using HarmonyLib;
using Legend;
using UnityEngine;

namespace CoreLoader.LevelPatches
{
    [HarmonyPatch(typeof(Chest), "GetReservedPlayer", new Type[] { typeof(Vector3) })]
    class ChestPatcher
    {

        static int playerUpgradeNum = 0;
        [HarmonyPrefix]
        static bool getReserved(Vector3 position, ref Player __result)
        {
            if (playerUpgradeNum >= Player.Players.Count)
            {
                playerUpgradeNum = 0;
            }
            Player player = Player.Players[playerUpgradeNum];
            if (player == null)
            {
                __result = null;
            }
            Room room = player.Room;
            room.UsedPlayerUpgrades.Add(player);
            playerUpgradeNum++;
            __result = player;
            return false;
        }
    }
}