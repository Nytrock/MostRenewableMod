using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Projectiles {
    public class IceGrenadeProjectile : ModProjectile {
        public override void SetDefaults() {
            Projectile.CloneDefaults(ProjectileID.Grenade);
            Projectile.timeLeft = 180;
            Projectile.aiStyle = -1;
        }

        public override bool OnTileCollide(Vector2 lastVelocity) {
            if (Projectile.velocity.X != lastVelocity.X)
                Projectile.velocity.X = lastVelocity.X * -0.4f;
            if (Projectile.velocity.Y != lastVelocity.Y && lastVelocity.Y > 0.7)
                Projectile.velocity.Y = lastVelocity.Y * -0.4f;

            return false;
        }

        public override void PrepareBombToBlow() {
            Projectile.tileCollide = false;
            Projectile.damage = 100;
            Projectile.Resize(22, 22);
            Projectile.knockBack = 12f;
        }

        public override void OnKill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Color transparent2 = Color.Transparent;
            for (int num849 = 0; num849 < 20; num849++) {
                Dust dust51 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, transparent2, 1.5f);
                Dust dust2 = dust51;
                dust2.velocity *= 1.4f;
            }

            for (int num850 = 0; num850 < 40; num850++) {
                Dust dust52 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, 0f, 0f, 100, transparent2, 2.2f);
                dust52.noGravity = true;
                dust52.velocity.Y -= 1.2f;
                Dust dust2 = dust52;
                dust2.velocity *= 4f;
                dust52 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, 0f, 0f, 100, transparent2, 1.3f);
                dust52.velocity.Y -= 1.2f;
                dust2 = dust52;
                dust2.velocity *= 2f;
            }

            for (int num851 = 1; num851 <= 2; num851++) {
                for (int num852 = -1; num852 <= 1; num852 += 2) {
                    for (int num853 = -1; num853 <= 1; num853 += 2) {
                        Gore gore6 = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, Vector2.Zero, Main.rand.Next(61, 64));
                        Gore gore2 = gore6;
                        gore2.velocity *= ((num851 == 1) ? 0.4f : 0.8f);
                        gore2 = gore6;
                        gore2.velocity += new Vector2(num852, num853);
                    }
                }
            }

            Point pt = Projectile.Center.ToTileCoordinates();
            float radius = 3f;
            Projectile.Kill_DirtAndFluidProjectiles_RunDelegateMethodPushUpForHalfBricks(pt, radius, SpreadIce);
        }

        public override void AI() {
            if (Projectile.wet)
                Projectile.timeLeft = 1;

            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
                Projectile.PrepareBombToBlow();

            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 10f) {
                Projectile.ai[0] = 10f;
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f) {
                    Projectile.velocity.X *= 0.97f;
                    if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01) {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                Projectile.velocity.Y += 0.2f;
            }

            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }

        public bool SpreadIce(int x, int y) {
            Vector2 center = Projectile.Center.ToTileCoordinates().ToVector2();
            if (Vector2.Distance(center, new Vector2(x, y)) > 3.3f)
                return false;

            Tile waterTile = Main.tile[x, y];
            if (waterTile.LiquidAmount < 128 || waterTile.LiquidType != LiquidID.Water)
                return true;

            int tileID = TileID.IceBlock;
            WorldGen.TryKillingReplaceableTile(x, y, tileID);

            if (WorldGen.PlaceTile(x, y, tileID)) {
                waterTile.Clear(TileDataType.Liquid);
                if (Main.netMode != NetmodeID.SinglePlayer) {
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, x, y);
                    NetMessage.sendWater(x, y);
                }

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

            if (waterTile.LiquidAmount > 128)
                return true;

            if (waterTile.TileType < 0)
                return false;

            if (Main.tileSolid[waterTile.TileType] && !TileID.Sets.Platforms[waterTile.TileType])
                return waterTile.TileType == TileID.PlanterBox;

            return true;
        }
    }
}
