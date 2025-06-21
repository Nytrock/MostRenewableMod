using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace EverythingRenewableNow.Common.WorldGenerations.Skyblock {
    public class RemovingTerrainPass() : GenPass("Removing Terrain", float.PositiveInfinity) {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
            progress.Message = LocalizationUtils.GetTextValue("WorldGen.RemovingTerrain");
            for (int i = 0; i < Main.maxTilesX; i++) {
                progress.Set(i / Main.maxTilesX);
                for (int j = 0; j < Main.maxTilesY; j++) {
                    Main.tile[i, j].Clear(TileDataType.Tile);
                }
            }
        }
    }
}
