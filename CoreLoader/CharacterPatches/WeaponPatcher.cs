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
        static bool prefixEquipWeapon(Weapon __instance, ref Player ___player)
        {
            if ((int)___player.CharacterClassType >= 8)
            {
                ___player = __instance.GetComponent<Player>();
                __instance.ProjectilePrefab = __instance.ClassProjecilePrefabs[(int)CharacterClassType.Fighter];
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Weapon), "DoShoot", new Type[] { typeof(Direction) })]
    class DoShootPatcher
    {
        [HarmonyPrefix]
        static bool prefixDoShoot(Direction direction, Weapon __instance, ref Player ___player, 
            ref Rigidbody2D ___rigid, ref Damageable ___health, ref TargetType ___Targets, ref PlayerVisual ___visual)
        {
            float num = ___player.DamageAmount;
            Vector3 position = SetDirection(__instance, direction);
            num *= 1f - UnityEngine.Random.value * 0.25f;
            if (___player.CharacterClassType == (CharacterClassType)9)
            {
                if (___visual == null)
                    ___visual = __instance.GetComponentInChildren<PlayerVisual>();
                ___visual.SetTemporaryAnimation(CharacterAnimation.Shoot, 0.25f, direction);
                if (__instance.ProjectilePrefab != null)
                {
                    if (___rigid == null)
                        ___rigid = __instance.GetComponent<Rigidbody2D>();
                    GameObject gameObject = Projectile.Create(__instance.ProjectilePrefab, position, Weapon.DirectionRotation(direction),
                        ___health, num, ___player.BulletImpulseForce, ___player.ActualRange, ___rigid.velocity * 0.5f);
                    Projectile component = gameObject.GetComponent<Projectile>();
                    component.Targets = ___Targets;
                    checkItems(___player, component, 5f);
                }
                return false;
            }
            return true;
        }

        [HarmonyPostfix]
        static void postfixDoShoot(Direction direction, Weapon __instance, ref Player ___player, ref Damageable ___health, ref Rigidbody2D ___rigid, ref TargetType ___Targets)
        {
            if (___player.Inventory.HasItem((ItemId)158))
            {
                Vector3 position = SetDirection(__instance, direction);
                float num = ___player.DamageAmount;
                num *= 1f - UnityEngine.Random.value * 0.25f;
                GameObject gameObject = Projectile.Create(__instance.ProjectilePrefab, position, Weapon.DirectionRotation(direction),
                    ___health, num, ___player.BulletImpulseForce, ___player.ActualRange, ___rigid.velocity * 0.5f);
                Projectile component = gameObject.GetComponent<Projectile>();
                component.Targets = ___Targets;
                checkItems(___player, component, 2f);
            }
        }

        static void checkItems(Player player, Projectile component, float homingAmount)
        {
            if (player.Inventory.HasItem(ItemId.Whetstone))
                component.Pierce = true;
            if (player.Inventory.HasItem(ItemId.Seeker))
                component.Homing += homingAmount;
        }

        static Vector3 SetDirection(Weapon weapon, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return weapon.transform.position + weapon.ProjectileOffset + new Vector3(0f, 0.25f);
                case Direction.Down:
                    return weapon.transform.position + weapon.ProjectileOffset + new Vector3(0.25f, -0.025f);
                case Direction.Left:
                    return weapon.transform.position + weapon.ProjectileOffset + new Vector3(0f, -0.25f);
                case Direction.Right:
                    return weapon.transform.position + weapon.ProjectileOffset + new Vector3(-0.25f, -0.025f);
            }
            return Vector3.zero;
        }
    }
}