using BepInEx;
using HarmonyLib;
using Legend;
using CoreLoader.ItemPatches;

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
				Id = (CharacterClassType)9,
				Name = "Danmaku",
				Description = "Danmaku class made for Millie",
				Victory = (Unlock)54,
				Level5 = ItemId.StaffOfLordSeigen,
				Level10 = ItemId.FieldRepair,
				Level15 = ItemId.Seeker,
				Level20 = (ItemId)ItemEnum.DoublePen,
				IsPremium = false,
				OnSpawn = delegate (Player player)
				{
					player.Coins = 0;
					player.Keys = 0;
					player.Bombs = 1;
					player.Speed = 3f;
					player.Damage = 3f;
					player.BulletSpeed = 35f;
					player.AttackRate = 60f;
					player.Range = 6f;
					player.Health.NetworkMaxHealth = 0.5f;
					player.Health.Health = player.Health.MaxHealth;
					if (player.isLocalPlayer)
					{
						//Adding items if needed
					}
				}
			});
		}
    }
}
