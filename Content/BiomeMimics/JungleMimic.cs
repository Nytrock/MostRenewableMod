using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class JungleMimic : BiomeMimicTemplate {
        protected override int BaseKeyType => ItemID.JungleKey;
        protected override int WeaponType => ItemID.PiranhaGun;
        protected override int ChestStyle => 18;
        protected override string TemplateName => "Jungle";
    }
}
