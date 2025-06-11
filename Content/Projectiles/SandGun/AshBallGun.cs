using Terraria.ID;

namespace EverythingRenewableNow.Content.Projectiles.SandGun {
    public class AshBallGun : BaseBallGun {
        protected override int Tile => TileID.Ash;
        protected override int Dust => DustID.Ash;

        public override void SetDefaults() {
            base.SetDefaults();
            Projectile.damage = 31;
        }
    }
}
