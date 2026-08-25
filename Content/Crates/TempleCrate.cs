using DuckLib.Templates;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Crates {
    public class TempleCrate : CrateTemplate {
        protected override string TemplateName => "Temple";

        public override IItemDropRule[] AddNonStandardLoot() {
            IItemDropRule[] trapsRules = [
                ItemDropRule.Common(ItemID.SpikyBallTrap, 1, 5, 10),
                ItemDropRule.Common(ItemID.SpearTrap, 1, 5, 10),
                ItemDropRule.Common(ItemID.FlameTrap, 1, 2, 5),
                ItemDropRule.Common(ItemID.SuperDartTrap, 1, 2, 5),
                ItemDropRule.Common(ItemID.WoodenSpike, 1, 10, 20),
            ];
            IItemDropRule trapsRule = new OneFromRulesRule(1, trapsRules);

            IItemDropRule furnaceRule = ItemDropRule.NotScalingWithLuck(ItemID.LihzahrdFurnace, 10);

            return [trapsRule, furnaceRule];
        }
    }
}
