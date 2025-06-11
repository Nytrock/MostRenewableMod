using EverythingRenewableNow.Content.Projectiles.SandGun;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Items {
    public class GlobalWeapon : GlobalItem {
        public override bool? CanBeChosenAsAmmo(Item ammo, Item weapon, Player player) {
            if (weapon.type == ItemID.Sandgun && ammo.type == ItemID.AshBlock)
                return true;
            return base.CanBeChosenAsAmmo(ammo, weapon, player);
        }

        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback) {
            if (weapon.type != ItemID.Sandgun)
                return;

            if (ammo.type == ItemID.AshBlock)
                type = ModContent.ProjectileType<AshBallGun>();
        }
    }
}
