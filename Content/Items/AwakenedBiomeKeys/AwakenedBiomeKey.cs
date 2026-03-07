using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.AwakenedBiomeKeys {
    public class AwakenedBiomeKey(int width, int height, string biome, int keyID) : ModItem {
        public override string Name => $"Awakened{biome}Key";
        protected override bool CloneNewInstances => true;

        public override void SetDefaults() {
            Item.width = width;
            Item.height = height;
            Item.maxStack = 9999;
        }

        public override void AddRecipes() {
            Recipe
                .Create(Type)
                .AddIngredient(keyID)
                .AddIngredient(ItemID.Ectoplasm, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
