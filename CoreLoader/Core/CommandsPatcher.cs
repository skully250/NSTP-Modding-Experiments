using Legend;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace CoreLoader.Core
{
    class CommandsPatcher
    {
        [ConsoleCommand(Name = "Bruh", Description = "Test command")]
        private static void CmdBruh(string[] arguments)
        {
            var gameObject = Level.Instance.ItemIconPrefab;
            Debug.Log("Prefab grabbed");
            var component = gameObject.GetComponents(typeof(ItemIcon));
            Debug.Log("ItemIcons grabbed");
            //var sprite = component.sprite;
            //Debug.Log("Sprite grabbed: " + sprite);
        }

        [ConsoleCommand(Name = "SpawnItem", Description="Spawn the item of the item id")]
        private static void CmdSpawnItem(string[] arguments)
        {
            Player mainPlayer = Player.Players[0];
            ItemId iId = (ItemId)int.Parse(arguments[0]);
            mainPlayer.CallCmdAddItem(iId, true);
        }
    }
}
