using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Projectiles.SandGun {
    public abstract class BaseBallGun : ModProjectile {
        protected abstract int Tile { get; }
        protected abstract int Dust { get; }

        public override void SetDefaults() {
            Projectile.CloneDefaults(ProjectileID.SandBallGun);
            Projectile.aiStyle = 0;
        }

        public override void AI() {
            if (Main.rand.NextBool(2)) {
                int index = Terraria.Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Dust);
                Main.dust[index].velocity.X *= 0.4f;
            }

            Projectile.tileCollide = true;
            Projectile.localAI[1] = 0.0f;
            if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0.0) {
                Projectile.tileCollide = false;
                if (Main.player[Projectile.owner].channel) {
                    Projectile.localAI[1] = -1f;
                    float num1 = 12f;
                    Vector2 vector2 = new(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                    float num2 = Main.mouseX + Main.screenPosition.X - vector2.X;
                    float num3 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    if (Main.player[Projectile.owner].gravDir == -1.0)
                        num3 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
                    float num5 = (float)Math.Sqrt((double)num2 * (double)num2 + (double)num3 * (double)num3);
                    if ((double)num5 > (double)num1) {
                        float num6 = num1 / num5;
                        float num7 = num2 * num6;
                        float num8 = num3 * num6;
                        if ((double)num7 != Projectile.velocity.X || (double)num8 != Projectile.velocity.Y)
                            Projectile.netUpdate = true;
                        Projectile.velocity.X = num7;
                        Projectile.velocity.Y = num8;
                    } else {
                        if ((double)num2 != Projectile.velocity.X || (double)num3 != Projectile.velocity.Y)
                            Projectile.netUpdate = true;
                        Projectile.velocity.X = num2;
                        Projectile.velocity.Y = num3;
                    }
                } else {
                    Projectile.ai[0] = 1f;
                    Projectile.netUpdate = true;
                }
            }

            if (Projectile.ai[0] == 1.0) {
                ++Projectile.ai[1];
                if (Projectile.ai[1] >= 60.0) {
                    Projectile.ai[1] = 60f;
                    Projectile.velocity.Y += 0.2f;
                }
            }

            Projectile.rotation += 0.1f;
            if (Projectile.velocity.Y <= 10.0)
                return;
            Projectile.velocity.Y = 10f;
        }

        public override void OnKill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 5; i++) {
                int dustIndex = Terraria.Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Dust);
                Dust dust2 = Main.dust[dustIndex];
                dust2.velocity *= 0.6f;
            }

            int x = (int)(Projectile.position.X + Projectile.width / 2) / 16;
            int y = (int)(Projectile.position.Y + Projectile.height / 2) / 16;

            if (Main.tile[x, y].HasUnactuatedTile && Main.tile[x, y].IsHalfBlock && Projectile.velocity.Y > 0f && Math.Abs(Projectile.velocity.Y) > Math.Abs(Projectile.velocity.X))
                y--;

            if (!Main.tile[x, y].HasTile) {
                bool flag = false;
                if (y < Main.maxTilesY - 2) {
                    Tile tile2 = Main.tile[x, y + 1];
                    if (tile2 != null && tile2.HasTile) {
                        if (tile2.HasTile && tile2.TileType == 314)
                            flag = true;

                        if (tile2.HasTile && WorldGen.BlockBelowMakesSandFall(x, y))
                            flag = true;
                    }
                }

                WorldGen.PlaceTile(x, y, Tile, mute: false, forced: true);

                if (!flag && Main.tile[x, y].HasTile && Main.tile[x, y].TileType == Tile) {
                    if (Main.tile[x, y + 1].IsHalfBlock || Main.tile[x, y + 1].Slope != 0) {
                        WorldGen.SlopeTile(x, y + 1);
                        if (Main.netMode != NetmodeID.SinglePlayer)
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 14, x, y + 1);
                    }

                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, x, y, Tile);
                }
            }
        }
    }
}
