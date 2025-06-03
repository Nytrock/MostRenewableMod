using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public abstract class BaseCrate<TTile> : ModItem where TTile : ModTile {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.TileType<TTile>());
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 1);
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Crates;
        }

        public override bool CanRightClick() {
            return true;
        }
    }
}
