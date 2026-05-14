using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class JungleMimic : BiomeMimicTemplate {
        protected override int KeyType => ItemID.JungleKey;
        protected override int WeaponType => ItemID.PiranhaGun;
        protected override int KeyHeight => 24;
        protected override int KeyWidth => 36;
        protected override int ChestStyle => 18;
        protected override string Name => "Jungle";
    }
}
