using HarmonyLib;
using Legend;
using System;
using UnityEngine;

namespace CoreLoader.ItemPatches
{
    [HarmonyPatch(typeof(Items), "AddUpgrades")]
    class ItemPatcher
    {
		public static ItemType AddItem(ItemId id, string name, ItemCategory category, string description, string flavorText, Action<Item, Player> onFound)
		{
			ItemType newItem = new ItemType();
			newItem.Id = id;
			newItem.Name = name;
			newItem.Category = category;
			newItem.FlavorText = flavorText;
			newItem.OnFound = onFound;
			return newItem;
		}

        [HarmonyPostfix]
        static void addNewItem()
		{
			ItemType itemType = AddItem((ItemId)ItemEnum.DoublePen, "Penetration Boogaloo", ItemCategory.Upgrade, "Electric Boogaloo", "This is the flavour text", 
				delegate (Item itemUse, Player player)
			{
				player.Damage /= 2f;
			});
			itemType.RequiredUnlock = (Unlock)54;
			ItemBase.AddItem(itemType);
		}
    }
}