using DuckLib;
using EverythingRenewableNow.Common.Systems;
using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
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

            if (CrossModSystem.Calamity == null) {
                WorldGen.PlaceTile(platformX + 3, platformY, TileID.WoodBlock, forced: true);
                WorldGen.AddShadowOrb(Main.spawnTileX, Main.spawnTileY + 3);
                DuckGen.PlaceItemFrame(Main.spawnTileX, Main.spawnTileY - 1, ItemID.BottomlessShimmerBucket);

                int signX = Main.spawnTileX - 2, signY = Main.spawnTileY;
                WorldGen.PlaceSign(signX, signY, TileID.Signs, 0);
                int signID = Sign.ReadSign(signX, signY);
                Sign.TextSign(signID, LocalizationUtils.GetTextValue("Skyblock.SignText"));
            } else {
                WorldGen.PlaceTile(platformX, platformY, CrossModSystem.Calamity.Find<ModTile>("EutrophicSand").Type, forced: true);
                WorldGen.PlaceTile(platformX + 3, platformY, CrossModSystem.Calamity.Find<ModTile>("InfernalSuevite").Type, forced: true);

                TileObject.CanPlace(platformX + 1, platformY - 1, CrossModSystem.Calamity.Find<ModTile>("PowerCellFactory").Type, 0, 1, out TileObject data);
                TileObject.Place(data);

                DuckGen.PlaceItemFrame(Main.spawnTileX - 1, Main.spawnTileY + 2, ItemID.BottomlessShimmerBucket);
            }
        }
    }
}
