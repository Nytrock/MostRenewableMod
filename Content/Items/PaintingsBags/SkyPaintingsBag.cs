using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class SkyPaintingsBag : ModItem {
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
            IItemDropRule dropRule = ItemDropRule.OneFromOptions(1,
                ItemID.HighPitch, ItemID.BlessingfromTheHeavens, ItemID.Constellation, ItemID.SeeTheWorldForWhatItIs,
                ItemID.LoveisintheTrashSlot, ItemID.SunOrnament
            );
            itemLoot.Add(dropRule);
        }
    }
}
