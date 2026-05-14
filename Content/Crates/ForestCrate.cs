using DuckLib.Templates;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Crates {
    public class ForestCrate : CrateTemplate {
        protected override string Name => "Forest";

        public override IItemDropRule[] AddNonStandardLoot() {
            int[] surfaceLoot = [
                ItemID.Spear,
                ItemID.Blowpipe,
                ItemID.WoodenBoomerang,
                ItemID.Aglet,
                ItemID.ClimbingClaws,
                ItemID.Umbrella,
                ItemID.CordageGuide,
                ItemID.WandofSparking,
                ItemID.Radar,
                ItemID.PortableStool
            ];
            IItemDropRule surfaceLootRule = ItemDropRule.OneFromOptionsNotScalingWithLuck(1, surfaceLoot);

            IItemDropRule[] minecartsRules = [
                ItemDropRule.Common(ItemID.LadybugMinecart),
                ItemDropRule.Common(ItemID.SunflowerMinecart),
            ];
            IItemDropRule minecartsRule = new OneFromRulesRule(20, minecartsRules);

            return [surfaceLootRule, minecartsRule];
        }
    }
}
