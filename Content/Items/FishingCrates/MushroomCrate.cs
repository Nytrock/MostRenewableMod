using EverythingRenewableNow.Utils;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public class MushroomCrate : BaseCrate {
        protected override string _crateName => nameof(MushroomCrate);

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.AddPreHardmodeFishingCrateLoot(LootUtils.GetMushroomCrateRules());
        }
    }
}
