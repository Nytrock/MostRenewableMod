using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace EverythingRenewableNow.Utils {
    public static class TileUtils {
        private static readonly List<int> _tilesForGlowTulip = [TileID.Dirt, TileID.CorruptGrass, TileID.CrimsonGrass, TileID.HallowedGrass, TileID.Mud, TileID.Hive, TileID.Ebonstone, TileID.Stone, TileID.Crimstone, TileID.Pearlstone, TileID.GreenMoss, TileID.BrownMoss, TileID.RedMoss, TileID.BlueMoss, TileID.PurpleMoss, TileID.LavaMoss, TileID.KryptonMoss, TileID.XenonMoss, TileID.ArgonMoss, TileID.VioletMoss, TileID.RainbowMoss];
        private static readonly List<int> _ambientTiles = [TileID.FallenLog, TileID.DyePlants, TileID.Stalactite, TileID.SmallPiles, TileID.SmallPiles1x1Echo, TileID.SmallPiles2x1Echo, TileID.LargePiles, TileID.LargePiles2, TileID.LargePilesEcho, TileID.LargePiles2Echo, TileID.Pots, TileID.PotsEcho];
        private static readonly List<int> _carrotTiles = [TileID.Grass];

        public static bool IsFitForCarrot(int x, int y, int type) {
            if (y > Main.worldSurface)
                return false;

            if (!_carrotTiles.Contains(type))
                return false;

            Tile tileAbove = Framing.GetTileSafely(x, y - 1);
            if (tileAbove.HasTile)
                return false;

            if (tileAbove.CheckingLiquid)
                return false;

            return true;
        }

        public static bool IsFitForGlowTulip(int x, int y, int type) {
            int hellHeight = 200;
            if (Main.remixWorld)
                hellHeight = 350;

            if (y < Main.worldSurface || y > Main.maxTilesY - hellHeight)
                return false;

            if (x < 100 || x > Main.maxTilesX - 100 || x > 300 && x < Main.maxTilesX - 300)
                return false;

            if (!_tilesForGlowTulip.Contains(type))
                return false;

            Tile tileAbove = Framing.GetTileSafely(x, y - 1);
            if (tileAbove.HasTile)
                return false;

            if (tileAbove.CheckingLiquid)
                return false;

            if (!NoNearbyGlowTulips(x, y))
                return false;

            return true;
        }

        private static bool NoNearbyGlowTulips(int i, int j) {
            int num = Terraria.Utils.Clamp(i - 120, 10, Main.maxTilesX - 1 - 10);
            int num2 = Terraria.Utils.Clamp(i + 120, 10, Main.maxTilesX - 1 - 10);
            int num3 = Terraria.Utils.Clamp(j - 120, 10, Main.maxTilesY - 1 - 10);
            int num4 = Terraria.Utils.Clamp(j + 120, 10, Main.maxTilesY - 1 - 10);
            for (int k = num; k <= num2; k++) {
                for (int l = num3; l <= num4; l++) {
                    Tile tile = Main.tile[k, l];
                    if (tile.HasTile && tile.TileType == TileID.GlowTulip)
                        return false;
                }
            }

            return true;
        }
    }
}
