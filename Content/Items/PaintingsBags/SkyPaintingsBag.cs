using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class SkyPaintingsBag : PaintingBag {
        protected override IItemDropRule GeneratePaintingsDropRule() {
            IItemDropRule dropRule = ItemDropRule.OneFromOptions(1,
                ItemID.HighPitch, ItemID.BlessingfromTheHeavens, ItemID.Constellation, ItemID.SeeTheWorldForWhatItIs,
                ItemID.LoveisintheTrashSlot, ItemID.SunOrnament
            );
            return dropRule;
        }
    }
}
