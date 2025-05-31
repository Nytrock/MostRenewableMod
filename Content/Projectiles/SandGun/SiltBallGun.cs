using Terraria.ID;

namespace EverythingRenewableNow.Content.Projectiles.SandGun {
    public class SiltBallGun : BaseBallGun {
        protected override int Tile => TileID.Silt;
        protected override int Dust => DustID.Silt;

        public override void SetDefaults() {
            base.SetDefaults();
            Projectile.damage = 31;
        }
    }
}
