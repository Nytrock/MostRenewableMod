using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems.Dungeon {
    public class DungeonSystem : ModSystem {
        private static DungeonTileType _tileType;

        public static DungeonTileType TileType => _tileType;

        public override void OnWorldLoad() {
            for (int x = 0; x < Main.maxTilesX; x++) {
                if (_tileType != DungeonTileType.None)
                    break;

                for (int y = 0; y < Main.maxTilesY; y++) {
                    int tileType = Framing.GetTileSafely(x, y).TileType;
                    if (tileType == TileID.PinkDungeonBrick) {
                        Mod.Logger.Info("Dungeon pink");
                        _tileType = DungeonTileType.Pink;
                        break;
                    } else if (tileType == TileID.BlueDungeonBrick) {
                        Mod.Logger.Info("Dungeon blue");
                        _tileType = DungeonTileType.Blue;
                        break;
                    } else if (tileType == TileID.GreenDungeonBrick) {
                        Mod.Logger.Info("Dungeon green");
                        _tileType = DungeonTileType.Green;
                        break;
                    }
                }
            }
        }

        public override void OnWorldUnload() {
            Mod.Logger.Info("Dungeon unload");
            _tileType = DungeonTileType.None;
        }
    }
}
