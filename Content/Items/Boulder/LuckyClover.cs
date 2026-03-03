using DuckLib.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Boulder {
    public class LuckyClover : ModItem {
        public override void SetStaticDefaults() {
            ShimmerUtils.Add(Type, ModContent.ItemType<WiltedClover>());
        }

        public override void SetDefaults() {
            Item.width = 14;
            Item.height = 14;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
        }
    }
}
