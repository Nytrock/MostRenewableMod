using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class HellPaintingsBag : PaintingBag {
        protected override IItemDropRule GeneratePaintingsDropRule() {
            IItemDropRule dropRule = ItemDropRule.OneFromOptions(1,
                ItemID.Darkness, ItemID.DarkSoulReaper, ItemID.TrappedGhost, ItemID.DemonsEye,
                ItemID.HandEarth, ItemID.Skelehead, ItemID.LakeofFire, ItemID.ImpFace,
                ItemID.OminousPresence, ItemID.ShiningMoon, ItemID.LivingGore, ItemID.FlowingMagma
            );
            return dropRule;
        }
    }
}
