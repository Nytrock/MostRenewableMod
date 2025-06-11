using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Projectiles.DroppedBiomeChests {
    public abstract class DroppedBiomeChest : ModProjectile {
        public virtual int ChestTileType => 21;
        public abstract int ChestTileSubType { get; }
        public abstract int ItemType { get; }

        public override void SetDefaults() {
            Projectile.aiStyle = -1;
        }

        public override void AI() {
            if (Projectile.velocity.Y == 0f)
                Projectile.velocity.X *= 0.98f;
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.velocity.Y += 0.2f;

            if (Projectile.owner != Main.myPlayer)
                return;

            int x = (int)((Projectile.position.X + Projectile.width / 2) / 16f);
            int y = (int)((Projectile.position.Y + Projectile.height - 4f) / 16f);

            if (Projectile.shimmerWet)
                Shimmer();

            if (Main.tile[x, y] == null)
                return;

            if (!TileObject.CanPlace(x, y, ChestTileType, ChestTileSubType, Projectile.direction, out TileObject objectData))
                return;

            int chestIndex = WorldGen.PlaceChest(x, y, (ushort)ChestTileType, style: ChestTileSubType); ;
            if (chestIndex == -1)
                return;

            NetMessage.SendObjectPlacement(-1, x, y, objectData.type, objectData.style, objectData.alternate, objectData.random, Projectile.direction);
            SoundEngine.PlaySound(SoundID.Dig, new Vector2(x * 16, y * 16));
            Projectile.Kill();

            Chest chest = Main.chest[chestIndex];
            chest.item[0] = new Item(ItemType);
        }

        private void Shimmer() {
            if (Projectile.velocity.Y > 10f)
                Projectile.velocity.Y *= 0.97f;
            Projectile.velocity.Y -= 0.7f;
            if (Projectile.velocity.Y < -10f)
                Projectile.velocity.Y = -10f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = oldVelocity.X * -0.75f;
            if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 1.5)
                Projectile.velocity.Y = oldVelocity.Y * -0.7f;
            return false;
        }
    }
}
