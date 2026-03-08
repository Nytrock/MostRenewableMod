using DuckLib;
using DuckLib.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items {
    public class MiniDungeon : ModItem {
        public override void SetDefaults() {
            Item.width = 22;
            Item.height = 22;
            Item.rare = ItemRarityID.Blue;
            Item.value = 0;
        }

        public override bool CanRightClick() {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.Add(new DungeonBrickDropRule(1, 500, 600));
            itemLoot.Add(new DungeonWallDropRule(DungeonWallType.BrickUnsafe, 1, 400, 500));
            itemLoot.Add(new DungeonWallDropRule(DungeonWallType.SlabUnsafe, 1, 400, 500));
            itemLoot.Add(new DungeonWallDropRule(DungeonWallType.TiledUnsafe, 1, 400, 500));
        }
    }
}
