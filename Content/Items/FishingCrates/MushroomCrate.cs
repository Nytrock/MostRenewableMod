using EverythingRenewableNow.Utils;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public class MushroomCrate : BaseCrate<Tiles.FishingCrates.MushroomCrate> {
        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.AddPreHardmodeFishingCrateLoot(LootUtils.GetMushroomCrateRules());
        }
    }
}
