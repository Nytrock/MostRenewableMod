using EverythingRenewableNow.Utils;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public class TempleCrateHardmode : BaseCrate {
        protected override string _crateName => nameof(TempleCrateHardmode);

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();
            ItemID.Sets.IsFishingCrateHardmode[Type] = true;
        }

        public override void SetDefaults() {
            base.SetDefaults();
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<TempleCrate>();
        }

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.AddHardmodeFishingCrateLoot(LootUtils.GetTempleCrateRules());
        }
    }
}
