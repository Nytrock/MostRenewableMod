using Terraria;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace EverythingRenewableNow.Common.WorldGenerations.Skyblock {
    public class SkyblockDungeonPass() : GenPass("SkyblockDungeon", float.PositiveInfinity) {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
            progress.Set(1);
            Main.dungeonX = GenVars.dungeonLocation;
            Main.dungeonY = Main.spawnTileY;
        }
    }
}
