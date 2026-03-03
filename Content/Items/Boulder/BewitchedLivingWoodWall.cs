using DuckLib.Utils;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Boulder {
    public class BewitchedLivingWoodWall : ModItem {
        public override void SetStaticDefaults() {
            ShimmerUtils.Add(ItemID.LivingWoodWall, Type);
            ItemID.Sets.DrawUnsafeIndicator[Type] = true;
        }

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.LivingWoodWall);
            Item.ResearchUnlockCount = 400;
            Item.createWall = WallID.LivingWoodUnsafe;
        }
    }
}
