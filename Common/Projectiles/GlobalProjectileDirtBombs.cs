using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Projectiles {
    public class GlobalProjectileDirtBombs : GlobalProjectile {
        private static Vector2 _dirtBombCenter;
        private static float _dirtBombRadius;

        private const int DIRTIEST_BLOCK_CHANCE = 100000;
        private static int _dirtBlocksPlaced = 0;

        public override bool PreKill(Projectile projectile, int timeLeft) {
            if (projectile.type == ProjectileID.DirtBomb || projectile.type == ProjectileID.DirtStickyBomb) {
                DirtBombCodeSurrogate(projectile);
                if (Main.netMode != NetmodeID.MultiplayerClient) {
                    Point pt = projectile.Center.ToTileCoordinates();
                    float radius = 4.2f;

                    SetDirtBombParameters(pt.ToVector2(), radius);
                    projectile.Kill_DirtAndFluidProjectiles_RunDelegateMethodPushUpForHalfBricks(pt, radius, SpreadDirtWithDirtiestBlockChance);
                }
                return false;
            }

            return true;
        }

        public static void SetDirtBombParameters(Vector2 center, float radius) {
            _dirtBombCenter = center;
            _dirtBombRadius = radius;
        }

        private static void DirtBombCodeSurrogate(Projectile projectile) {
            projectile.Resize(22, 22);
            SoundEngine.PlaySound(SoundID.Item14, projectile.position);
            Color transparent2 = Color.Transparent;
            int num848 = 0;
            for (int num849 = 0; num849 < 30; num849++) {
                Dust dust51 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, transparent2, 1.5f);
                Dust dust2 = dust51;
                dust2.velocity *= 1.4f;
            }

            for (int num850 = 0; num850 < 80; num850++) {
                Dust dust52 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, num848, 0f, 0f, 100, transparent2, 2.2f);
                dust52.noGravity = true;
                dust52.velocity.Y -= 1.2f;
                Dust dust2 = dust52;
                dust2.velocity *= 4f;
                dust52 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, num848, 0f, 0f, 100, transparent2, 1.3f);
                dust52.velocity.Y -= 1.2f;
                dust2 = dust52;
                dust2.velocity *= 2f;
            }

            for (int num851 = 1; num851 <= 2; num851++) {
                for (int num852 = -1; num852 <= 1; num852 += 2) {
                    for (int num853 = -1; num853 <= 1; num853 += 2) {
                        Gore gore6 = Gore.NewGoreDirect(projectile.GetSource_Death(), projectile.position, Vector2.Zero, Main.rand.Next(61, 64));
                        Gore gore2 = gore6;
                        gore2.velocity *= ((num851 == 1) ? 0.4f : 0.8f);
                        gore2 = gore6;
                        gore2.velocity += new Vector2(num852, num853);
                    }
                }
            }
        }

        public static bool SpreadDirtWithDirtiestBlockChance(int x, int y) {
            if (Vector2.Distance(_dirtBombCenter, new Vector2(x, y)) > _dirtBombRadius)
                return false;

            WorldGen.TryKillingReplaceableTile(x, y, TileID.Dirt);
            if (WorldGen.PlaceTile(x, y, TileID.Dirt)) {
                _dirtBlocksPlaced++;

                if (Main.rand.NextBool(DIRTIEST_BLOCK_CHANCE) || _dirtBlocksPlaced == DIRTIEST_BLOCK_CHANCE) {
                    WorldGen.PlaceTile(x, y, TileID.DirtiestBlock, forced: true);
                    _dirtBlocksPlaced = 0;
                }

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

            if (tile2.TileType < TileID.Dirt)
                return false;

            if (Main.tileSolid[tile2.TileType] && !TileID.Sets.Platforms[tile2.TileType])
                return tile2.TileType == TileID.PlanterBox;

            return true;
        }
    }
}
