using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace EverythingRenewableNow.Utils {
    public static class TileUtils {
        private static readonly List<int> _tilesForGlowTulip = [TileID.Dirt, TileID.CorruptGrass, TileID.CrimsonGrass, TileID.HallowedGrass, TileID.Mud, TileID.Hive, TileID.Ebonstone, TileID.Stone, TileID.Crimstone, TileID.Pearlstone, TileID.GreenMoss, TileID.BrownMoss, TileID.RedMoss, TileID.BlueMoss, TileID.PurpleMoss, TileID.LavaMoss, TileID.KryptonMoss, TileID.XenonMoss, TileID.ArgonMoss, TileID.VioletMoss, TileID.RainbowMoss];
        private static readonly List<int> _ambientTiles = [TileID.FallenLog, TileID.DyePlants, TileID.Stalactite, TileID.SmallPiles, TileID.SmallPiles1x1Echo, TileID.SmallPiles2x1Echo, TileID.LargePiles, TileID.LargePiles2, TileID.LargePilesEcho, TileID.LargePiles2Echo, TileID.Pots, TileID.PotsEcho];

        private static Vector2 _dirtBombCenter;
        private static float _dirtBombRadius;

        public static void SetDirtBombParameters(Vector2 center, float radius) {
            _dirtBombCenter = center;
            _dirtBombRadius = radius;
        }

        public static bool SpreadDirtWithDirtiestBlockChance(int x, int y) {
            if (Vector2.Distance(_dirtBombCenter, new Vector2(x, y)) > _dirtBombRadius)
                return false;

            int tileID = TileID.Dirt;
            if (Main.rand.NextBool(100000))
                tileID = TileID.DirtiestBlock;
            WorldGen.TryKillingReplaceableTile(x, y, tileID);

            if (WorldGen.PlaceTile(x, y, tileID)) {
                if (Main.netMode != NetmodeID.SinglePlayer)
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, x, y);

                Vector2 position = new(x * 16, y * 16);
                int num = 0;
                for (int i = 0; i < 3; i++) {
                    Dust dust = Dust.NewDustDirect(position, 16, 16, num, 0f, 0f, 100, Color.Transparent, 2.2f);
                    dust.noGravity = true;
                    dust.velocity.Y -= 1.2f;
                    dust.velocity *= 4f;
                    Dust dust2 = Dust.NewDustDirect(position, 16, 16, num, 0f, 0f, 100, Color.Transparent, 1.3f);
                    dust2.velocity.Y -= 1.2f;
                    dust2.velocity *= 2f;
                }

                int num2 = y + 1;
                if (Main.tile[x, num2] != null && !TileID.Sets.Platforms[Main.tile[x, num2].TileType] && (Main.tile[x, num2].TopSlope || Main.tile[x, num2].IsHalfBlock)) {
                    WorldGen.SlopeTile(x, num2);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 14, x, num2);
                }

                num2 = y - 1;
                if (Main.tile[x, num2] != null && !TileID.Sets.Platforms[Main.tile[x, num2].TileType] && Main.tile[x, num2].BottomSlope) {
                    WorldGen.SlopeTile(x, num2);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 14, x, num2);
                }

                List<int> permittedTileTypes = [TileID.Grass, TileID.CorruptGrass, TileID.JungleGrass, TileID.MushroomGrass,
                    TileID.HallowedGrass, TileID.CrimsonGrass, TileID.GolfGrass, TileID.GolfGrassHallowed];
                for (int j = x - 1; j <= x + 1; j++) {
                    for (int k = y - 1; k <= y + 1; k++) {
                        Tile tile = Main.tile[j, k];
                        if (!tile.HasTile || num == tile.TileType || !permittedTileTypes.Contains(tile.TileType))
                            continue;

                        bool flag = true;
                        for (int l = j - 1; l <= j + 1; l++) {
                            for (int m = k - 1; m <= k + 1; m++) {
                                if (!WorldGen.SolidTile(l, m))
                                    flag = false;
                            }
                        }

                        if (flag) {
                            WorldGen.KillTile(j, k, fail: true);
                            if (Main.netMode != NetmodeID.SinglePlayer)
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, j, k, 1f);
                        }
                    }
                }

                return true;
            }

            Tile tile2 = Main.tile[x, y];
            if (tile2 == null)
                return false;

            if (tile2.TileType < 0)
                return false;

            if (Main.tileSolid[tile2.TileType] && !TileID.Sets.Platforms[tile2.TileType])
                return tile2.TileType == TileID.PlanterBox;

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
