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
			newItem.RequiredUnlock = Unlock.FighterVictory;
			return newItem;
		}

        [HarmonyPostfix]
        static void addNewItem()
		{
			ItemType itemType = ItemBase.Types[(int)ItemId.HeartContainer];
			itemType.OnFound = delegate (Item curItem, Player player)
			{
				Damageable hp = player.Health;
				hp.NetworkMaxHealth = hp.MaxHealth + 10f;
				player.Health.Health += 10f;
			};
			itemType = AddItem((ItemId)157, "Test Item", ItemCategory.Upgrade, "Bruh", "This is the flavour text",
				delegate (Item itemUse, Player player)
				{
					Damageable health = player.Health;
					health.NetworkMaxHealth = health.MaxHealth + 1f;
					health.Health += 1f;
				});
			ItemBase.AddItem(itemType);
			itemType = AddItem((ItemId)158, "Penetration Boogaloo", ItemCategory.Upgrade, "Electric Boogaloo", "This is the flavour text", 
				delegate (Item itemUse, Player player)
			{
				player.Damage /= 1.5f;
			});
			ItemBase.AddItem(itemType);
		}
    }
}