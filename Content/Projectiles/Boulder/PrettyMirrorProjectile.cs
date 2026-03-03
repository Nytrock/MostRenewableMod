using DuckLib.Utils;
using EverythingRenewableNow.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Projectiles.Boulder {
    public class PrettyMirrorProjectile : ModProjectile {
        private static int[] _gores;
        private static int _dust;

        public override void Load() {
            int gore1 = this.CreateGore("1");
            int gore2 = this.CreateGore("2");
            int gore3 = this.CreateGore("3");
            int gore4 = this.CreateGore("4");
            _gores = [gore1, gore2, gore3, gore4];

            _dust = this.CreateDust();
        }

        public override void SetDefaults() {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
        }

        public override void AI() {
            Projectile.rotation += Projectile.direction * 0.4f;
        }

        public override void OnKill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Item106, Projectile.Center);
            Projectile.oldVelocity *= 0.2f;
            Projectile.position -= Projectile.oldVelocity;

            for (int num8 = 0; num8 < 10; num8++) {
                int num9 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, _dust);
                Dust dust2 = Main.dust[num9];
                dust2.velocity -= Projectile.oldVelocity;
            }

            foreach (int gore in _gores)
                Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Projectile.oldVelocity, gore);

            Rectangle rect = Projectile.getRect();
            int num10 = 150;
            rect.Inflate(num10, num10);
            if (Main.netMode != NetmodeID.Server && Main.LocalPlayer.getRect().Intersects(rect))
                Main.LocalPlayer.GetModPlayer<PlayerLuck>().StartBadLuckFromMirror();
        }
    }
}
