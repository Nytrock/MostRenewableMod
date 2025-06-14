using EverythingRenewableNow.Utils;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public class ForestCrate : BaseCrate {
        protected override string _crateName => nameof(ForestCrate);

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.AddPreHardmodeFishingCrateLoot(LootUtils.GetForestCrateRules());
        }
    }
}
