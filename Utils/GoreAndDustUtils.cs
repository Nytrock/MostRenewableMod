using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace EverythingRenewableNow.Utils {
    public static class GoreAndDustUtils {
        public static void CreateExplosion(IEntitySource source, Vector2 center, int width, int height) {
            SoundEngine.PlaySound(SoundID.Item14, center);
            Vector2 position = new(center.X - width / 2, center.Y - height / 2);

            for (int i = 0; i < 50; i++) {
                Dust dust = Dust.NewDustDirect(position, width, height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                dust.velocity *= 1.4f;
            }

            for (int i = 0; i < 80; i++) {
                Dust dust = Dust.NewDustDirect(position, width, height, DustID.Torch, 0f, 0f, 100, default, 3f);
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust = Dust.NewDustDirect(position, width, height, DustID.Torch, 0f, 0f, 100, default, 2f);
                dust.velocity *= 3f;
            }

            for (int g = 0; g < 2; g++) {
                var goreSpawnPosition = new Vector2(position.X + width / 2 - 24f, position.Y + height / 2 - 24f);
                Gore gore = Gore.NewGoreDirect(source, goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X += 1.5f;
                gore.velocity.Y += 1.5f;
                gore = Gore.NewGoreDirect(source, goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X -= 1.5f;
                gore.velocity.Y += 1.5f;
                gore = Gore.NewGoreDirect(source, goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X += 1.5f;
                gore.velocity.Y -= 1.5f;
                gore = Gore.NewGoreDirect(source, goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X -= 1.5f;
                gore.velocity.Y -= 1.5f;
            }
        }
    }
}
