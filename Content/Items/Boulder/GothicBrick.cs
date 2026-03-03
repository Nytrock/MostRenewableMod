using EverythingRenewableNow.Content.Tiles.Boulder;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.Boulder {
    public class GothicBrick : ModItem {
        public override void AddRecipes() {
            Recipe
                .Create(Type, 20)
                .AddIngredient(ItemID.StoneBlock, 20)
                .AddIngredient(ItemID.Cobweb, 5)
                .AddIngredient(ItemID.Bone, 5)
                .AddTile(TileID.BoneWelder)
                .Register();
        }

        public override void SetDefaults() {
            Item.height = 8;
            Item.width = 8;
            Item.rare = ItemRarityID.White;
            Item.DefaultToPlaceableTile(ModContent.TileType<GothicBrickTile>());
            Item.ResearchUnlockCount = 400;
        }
    }
}
