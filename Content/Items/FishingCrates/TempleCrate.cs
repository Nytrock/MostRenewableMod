using EverythingRenewableNow.Utils;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public class TempleCrate : BaseCrate {
        protected override string _crateName => nameof(TempleCrate);

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.AddPreHardmodeFishingCrateLoot();
        }
    }
}
