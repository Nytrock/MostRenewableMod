using EverythingRenewableNow.Content.Tiles;
using EverythingRenewableNow.Utils;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Banners {
    public class SlushMermanBanner : ModItem {
        public override LocalizedText Tooltip => LocalizationUtils.GetText("BannersTooltip").WithFormatArgs(LocalizationUtils.GetTextValue("NPCs.SlushMerman.DisplayName"));

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.TileType<EnemyBanner>(), (int)EnemyBanner.StyleID.SlushMerman);
            Item.width = 12;
            Item.height = 28;
            Item.SetShopValues(ItemRarityColor.Blue1, Terraria.Item.buyPrice(silver: 10));
        }
    }
}
