using DuckLib.Utils;
using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace EverythingRenewableNow.Content.SkyblockSeed {
    public class SkyblockIslandPass() : GenPass("Skyblock", float.PositiveInfinity) {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
            progress.Set(1);
            progress.Message = LocalizationUtils.GetTextValue("WorldGen.Skyblock");

            int platformX = Main.spawnTileX - 2;
            int platformY = Main.spawnTileY + 1;
            for (int i = 0; i < 3; i++)
                WorldGen.PlaceTile(platformX + i, platformY, TileID.IceBrick);

            WorldGen.PlaceTile(platformX + 3, platformY, TileID.WoodBlock, forced: true);
            WorldGen.AddShadowOrb(Main.spawnTileX, Main.spawnTileY + 3);
            TileUtils.PlaceItemFrame(Main.spawnTileX, Main.spawnTileY - 1, ItemID.BottomlessShimmerBucket);

            int signX = Main.spawnTileX - 2, signY = Main.spawnTileY;
            WorldGen.PlaceSign(signX, signY, TileID.Signs, 0);
            int signID = Sign.ReadSign(signX, signY);
            Sign.TextSign(signID, LocalizationUtils.GetTextValue("Skyblock.SignText"));
        }
    }
}
