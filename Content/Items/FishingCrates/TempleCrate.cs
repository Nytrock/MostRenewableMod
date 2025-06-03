using EverythingRenewableNow.Utils;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public class TempleCrate : BaseCrate<Tiles.FishingCrates.TempleCrate> {
        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.AddPreHardmodeFishingCrateLoot();
        }
    }
}
