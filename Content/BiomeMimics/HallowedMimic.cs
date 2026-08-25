using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class HallowedMimic : BiomeMimicTemplate {
        protected override int BaseKeyType => ItemID.HallowedKey;
        protected override int WeaponType => ItemID.RainbowGun;
        protected override int ChestStyle => 21;
        protected override string TemplateName => "Hallowed";
    }
}
