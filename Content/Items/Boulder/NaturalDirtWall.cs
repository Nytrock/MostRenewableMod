using DuckLib.Utils;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Boulder {
    public class NaturalDirtWall : ModItem {
        public override void SetStaticDefaults() {
            ShimmerUtils.Add(ItemID.DirtWall, Type);
            ItemID.Sets.DrawUnsafeIndicator[Type] = true;
        }

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.DirtWall);
            Item.ResearchUnlockCount = 400;
            Item.createWall = WallID.DirtUnsafe;
        }
    }
}
