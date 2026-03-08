using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items {
    public class MiniTemple : ModItem {
        public override bool CanRightClick() {
            return true;
        }

        public override void SetDefaults() {
            Item.width = 24;
            Item.height = 22;
            Item.rare = ItemRarityID.Blue;
            Item.value = 0;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.Add(ItemDropRule.Common(ItemID.LihzahrdAltar));
            itemLoot.Add(ItemDropRule.Common(ItemID.LihzahrdBrick, 1, 500, 600));
            itemLoot.Add(ItemDropRule.Common(ItemID.LihzahrdWallUnsafe, 1, 1200, 1500));
        }
    }
}
