using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class HallowedMimic : BiomeMimicTemplate {
        protected override int KeyType => ItemID.HallowedKey;
        protected override int WeaponType => ItemID.RainbowGun;
        protected override int KeyHeight => 26;
        protected override int KeyWidth => 36;
        protected override int ChestStyle => 21;
        protected override string Name => "Hallowed";
    }
}
