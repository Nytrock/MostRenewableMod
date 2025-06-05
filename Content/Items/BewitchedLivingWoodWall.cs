using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items {
    public class BewitchedLivingWoodWall : ModItem {
        public override void SetStaticDefaults() {
            ItemID.Sets.ShimmerTransformToItem[ItemID.LivingWoodWall] = Type;
            ItemID.Sets.DrawUnsafeIndicator[Type] = true;
        }

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.LivingWoodWall);
            Item.ResearchUnlockCount = 400;
            Item.createWall = WallID.LivingWoodUnsafe;
        }
    }
}
