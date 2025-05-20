using EverythingRenewableNow.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Projectiles {
    public class GlobalProjectileBombs : GlobalProjectile {
        public override bool PreKill(Projectile projectile, int timeLeft) {
            if (projectile.type == ProjectileID.DirtBomb || projectile.type == ProjectileID.DirtStickyBomb) {
                DirtBombCodeSurrogate(projectile);
                if (Main.netMode != NetmodeID.MultiplayerClient) {
                    Point pt = projectile.Center.ToTileCoordinates();
                    float radius = 4.2f;

                    TileUtils.SetDirtBombParameters(pt.ToVector2(), radius);
                    projectile.Kill_DirtAndFluidProjectiles_RunDelegateMethodPushUpForHalfBricks(pt, radius, TileUtils.SpreadDirtWithDirtiestBlockChance);
                }
                return false;
            }

            return true;
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
    }
}
