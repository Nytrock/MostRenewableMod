using EverythingRenewableNow.Content.Tiles;
using Terraria.Enums;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Banners {
    public class SlushMermanBanner : ModItem {
        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.TileType<EnemyBanner>(), (int)EnemyBanner.StyleID.SlushMerman);
            Item.width = 12;
            Item.height = 28;
            Item.SetShopValues(ItemRarityColor.Blue1, Terraria.Item.buyPrice(silver: 10));
        }
    }
}
