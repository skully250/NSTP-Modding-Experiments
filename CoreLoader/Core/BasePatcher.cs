using BepInEx;
using UnityEngine;
using HarmonyLib;
using Legend;
using System;
using System.Reflection;

namespace CoreLoader.Core
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    class BasePatcher : BaseUnityPlugin
    {
        public const string ModGUID = "com.faedar.loader";
        public const string ModName = "LoadCore";
        public const string ModVersion = "1.0.0";

        public static BasePatcher instance = null;
        public Harmony harmony = null;

        public void Awake()
        {
            BasePatcher.instance = this;
            harmony = new Harmony(ModGUID);
            harmony.PatchAll();
			addClass();
		}

        public void addClass()
        {
			CharacterClasses.AddItem(new CharacterClass
			{
				Id = CharacterClassType.Lancer + 1,
				Name = "Test Class",
				Description = "A test class to see if this works",
				IsPremium = false,
				Level5 = ItemId.HeartContainer,
				Level10 = ItemId.SpicyBoar,
				Level15 = ItemId.DuelistsGauntlet,
				Level20 = ItemId.WhiteLotus,
				OnSpawn = delegate (Player player)
				{
					player.Coins = 26;
					player.Keys = 1;
					player.Bombs = 1;
					player.Speed = 3f;
					player.Damage = 5f;
					player.BulletSpeed = 13f;
					player.AttackRate = 8f;
					player.Range = 6f;
					player.Health.NetworkMaxHealth = 1f;
					player.Health.Health = player.Health.MaxHealth;
				}
			});
		}
    }
}
