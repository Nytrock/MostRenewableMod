using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Items {
    public class BossBagLoot : GlobalItem {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
            if (item.type == ItemID.GolemBossBag)
                itemLoot.Add(ItemDropRule.Common(ItemID.LihzahrdBrick, minimumDropped: 35, maximumDropped: 70));
        }
    }
}
