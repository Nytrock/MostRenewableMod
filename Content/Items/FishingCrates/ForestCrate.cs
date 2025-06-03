using EverythingRenewableNow.Utils;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public class ForestCrate : BaseCrate<Tiles.FishingCrates.ForestCrate> {
        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.AddPreHardmodeFishingCrateLoot(LootUtils.GetForestCrateRules());
        }
    }
}
