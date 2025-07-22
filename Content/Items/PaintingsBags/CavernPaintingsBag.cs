using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class CavernPaintingsBag : PaintingBag {
        protected override IItemDropRule GeneratePaintingsDropRule() {
            IItemDropRule waldoRule = ItemDropRule.Common(ItemID.Waldo, 625);
            IItemDropRule otherPaintingsRule = ItemDropRule.OneFromOptions(1,
                ItemID.Sunflowers, ItemID.TerrarianGothic, ItemID.GuidePicasso, ItemID.FatherofSomeone, ItemID.NurseLisa,
                ItemID.Land, ItemID.FindingGold, ItemID.AmericanExplosive, ItemID.Discover, ItemID.OldMiner, ItemID.TheMerchant,
                ItemID.CrownoDevoursHisLunch, ItemID.RareEnchantment, ItemID.GloriousNight, ItemID.Outcast, ItemID.FairyGuides,
                ItemID.AHorribleNightforAlchemy, ItemID.MorningHunt, ItemID.CatSword, ItemID.SufficientlyAdvanced, ItemID.StrangeGrowth,
                ItemID.HappyLittleTree, ItemID.StrangeDeadFellows, ItemID.Secrets, ItemID.Bioluminescence, ItemID.Wildflowers,
                ItemID.VikingVoyage, ItemID.Bifrost, ItemID.Heartlands, ItemID.ForestTroll, ItemID.AuroraBorealis
            );

            waldoRule.OnFailedRoll(otherPaintingsRule);
            return waldoRule;
        }
    }
}
