using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class CrimsonMimic : BiomeMimicTemplate {
        protected override int BaseKeyType => ItemID.CrimsonKey;
        protected override int WeaponType => ItemID.VampireKnives;
        protected override int ChestStyle => 20;
        protected override string TemplateName => "Crimson";
    }
}
