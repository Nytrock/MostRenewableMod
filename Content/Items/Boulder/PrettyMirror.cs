using EverythingRenewableNow.Content.Projectiles.Boulder;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Boulder {
    public class PrettyMirror : ModItem {
        public override void SetDefaults() {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 8f;
            Item.maxStack = 9999;
            Item.shoot = ModContent.ProjectileType<PrettyMirrorProjectile>();
            Item.damage = 13;
            Item.knockBack = 3f;
            Item.width = 24;
            Item.height = 24;
            Item.consumable = true;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 1);
            Item.rare = ItemRarityID.Blue;
        }
    }
}
