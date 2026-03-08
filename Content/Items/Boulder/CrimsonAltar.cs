using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Boulder {
    public class CrimsonAltar : ModItem {
        public override void SetStaticDefaults() {
            ItemID.Sets.DrawUnsafeIndicator[Type] = true;
        }

        public override void SetDefaults() {
            Item.SetShopValues(ItemRarityColor.Blue1, 0);
            Item.DefaultToPlaceableTile(TileID.DemonAltar, 1);
        }
    }
}
