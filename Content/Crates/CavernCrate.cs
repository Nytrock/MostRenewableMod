using DuckLib.Templates;
using EverythingRenewableNow.Content.Items.PaintingsBags;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Crates {
    public class CavernCrate : CrateTemplate {
        protected override string Name => "Cavern";

        public override IItemDropRule[] AddNonStandardLoot() {
            int[] cavernLoot = [
                ItemID.BandofRegeneration,
                ItemID.MagicMirror,
                ItemID.CloudinaBottle,
                ItemID.HermesBoots,
                ItemID.Mace,
                ItemID.ShoeSpikes,
            ];
            IItemDropRule cavernLootRule = ItemDropRule.OneFromOptionsNotScalingWithLuck(1, cavernLoot);
            IItemDropRule paintingsRule = ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CavernPaintingsBag>());

            return [cavernLootRule, paintingsRule];
        }
    }
}
