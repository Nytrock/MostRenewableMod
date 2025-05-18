using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class CavernPaintingsBag : ModItem {
        public override void SetDefaults() {
            Item.width = 16;
            Item.height = 16;
            Item.ResearchUnlockCount = 25;
            Item.shopCustomPrice = Terraria.Item.buyPrice(silver: 60);
            Item.value = Terraria.Item.buyPrice(silver: 30);
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.White;
        }

        public override bool CanRightClick() {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            IItemDropRule waldoRule = ItemDropRule.Common(625, ItemID.Waldo);
            IItemDropRule otherPaintingsRule = ItemDropRule.OneFromOptions(1,
                ItemID.Sunflowers, ItemID.TerrarianGothic, ItemID.GuidePicasso, ItemID.FatherofSomeone, ItemID.NurseLisa,
                ItemID.Land, ItemID.FindingGold, ItemID.AmericanExplosive, ItemID.Discover, ItemID.OldMiner, ItemID.TheMerchant,
                ItemID.CrownoDevoursHisLunch, ItemID.RareEnchantment, ItemID.GloriousNight, ItemID.Outcast, ItemID.FairyGuides,
                ItemID.AHorribleNightforAlchemy, ItemID.MorningHunt, ItemID.CatSword, ItemID.SufficientlyAdvanced, ItemID.StrangeGrowth,
                ItemID.HappyLittleTree, ItemID.StrangeDeadFellows, ItemID.Secrets, ItemID.Bioluminescence, ItemID.Wildflowers,
                ItemID.VikingVoyage, ItemID.Bifrost, ItemID.Heartlands, ItemID.ForestTroll, ItemID.AuroraBorealis
            );

            waldoRule.OnFailedRoll(otherPaintingsRule);
            itemLoot.Add(waldoRule);
        }
    }
}
