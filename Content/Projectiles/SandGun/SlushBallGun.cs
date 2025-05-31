using Terraria.ID;

namespace EverythingRenewableNow.Content.Projectiles.SandGun {
    public class SlushBallGun : BaseBallGun {
        protected override int Tile => TileID.Slush;
        protected override int Dust => DustID.Slush;

        public override void SetDefaults() {
            base.SetDefaults();
            Projectile.damage = 31;
        }
    }
}
