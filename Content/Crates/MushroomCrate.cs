using DuckLib.Templates;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Crates {
    public class MushroomCrate : CrateTemplate {
        protected override string TemplateName => "Mushroom";

        public override IItemDropRule[] AddNonStandardLoot() {
            IItemDropRule clothRule = ItemDropRule.Common(ItemID.MushroomHat);
            clothRule.OnSuccess(ItemDropRule.Common(ItemID.MushroomVest));
            clothRule.OnSuccess(ItemDropRule.Common(ItemID.MushroomPants));

            IItemDropRule[] mushroomLootRules = [
                clothRule,
                ItemDropRule.Common(ItemID.ShroomMinecart),
            ];
            IItemDropRule mushroomLootRule = new OneFromRulesRule(1, mushroomLootRules);

            return [mushroomLootRule];
        }
    }
}
