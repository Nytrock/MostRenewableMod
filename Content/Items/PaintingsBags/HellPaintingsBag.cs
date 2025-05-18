using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class HellPaintingsBag : ModItem {
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
                ItemID.Darkness, ItemID.DarkSoulReaper, ItemID.TrappedGhost, ItemID.DemonsEye,
                ItemID.HandEarth, ItemID.Skelehead, ItemID.LakeofFire, ItemID.ImpFace,
                ItemID.OminousPresence, ItemID.ShiningMoon, ItemID.LivingGore, ItemID.FlowingMagma
            );
            itemLoot.Add(dropRule);
        }
    }
}
