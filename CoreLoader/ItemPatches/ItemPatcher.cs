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
			ItemType item = ItemBase.Types[(int)ItemId.HeartContainer];
			item.OnFound = delegate (Item curItem, Player player)
			{
				Damageable hp = player.Health;
				hp.NetworkMaxHealth = hp.MaxHealth + 10f;
				player.Health.Health += 10f;
			};
			ItemType itemType = new ItemType();
			itemType.Id = ItemId.BowlerHat + 1;
			itemType.Name = "Test Item"._();
			itemType.Category = ItemCategory.Upgrade;
			itemType.RequiredUnlock = Unlock.FighterVictory;
			itemType.Description = "Bruh"._();
			itemType.FlavorText = "This is the flavour text."._();
			itemType.OnFound = delegate (Item itemUse, Player player)
			{
				Damageable health = player.Health;
				health.NetworkMaxHealth = health.MaxHealth + 1f;
				player.Health.Health += 1f;
			};
			ItemBase.AddItem(itemType);
			itemType = new ItemType();
			itemType.Id = ItemId.BowlerHat + 2;
			itemType.Name = "Test Item 2"._();
			itemType.Category = ItemCategory.Upgrade;
			itemType.RequiredUnlock = Unlock.FighterVictory;
			itemType.Description = "Electric Boogaloo"._();
			itemType.FlavorText = "This is the flavour text."._();
			itemType.OnFound = delegate (Item itemUse, Player player)
			{
				Damageable health = player.Health;
				health.NetworkMaxHealth = health.MaxHealth + 1f;
				player.Health.Health += 1f;
			};
			ItemBase.AddItem(itemType);
		}
    }
}