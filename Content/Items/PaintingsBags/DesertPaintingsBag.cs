using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class DesertPaintingsBag : PaintingBag {
        protected override IItemDropRule GeneratePaintingsDropRule() {
            IItemDropRule dropRule = ItemDropRule.OneFromOptions(1,
                ItemID.AndrewSphinx, ItemID.WatchfulAntlion, ItemID.BurningSpirit, ItemID.JawsOfDeath, ItemID.TheSandsOfSlime,
                ItemID.SnakesIHateSnakes, ItemID.LifeAboveTheSand, ItemID.Oasis, ItemID.PrehistoryPreserved, ItemID.AncientTablet,
                ItemID.Uluru, ItemID.VisitingThePyramids, ItemID.BandageBoy, ItemID.DivineEye
            );
            return dropRule;
        }
    }
}
