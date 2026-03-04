using EverythingRenewableNow.Content.Projectiles.Boulder;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Boulder {
    public class IceGrenade : ModItem {
        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.DirtBomb);
            Item.shoot = ModContent.ProjectileType<IceGrenadeProjectile>();
            Item.value = Item.buyPrice(0, 0, 10);
            Item.ResearchUnlockCount = 99;
        }
    }
}
