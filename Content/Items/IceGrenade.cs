using EverythingRenewableNow.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items {
    public class IceGrenade : ModItem {
        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.DirtBomb);
            Item.shoot = ModContent.ProjectileType<IceGrenadeProjectile>();
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Grenade)
                .AddIngredient(ItemID.IceBlock, 15)
                .Register();
        }
    }
}
