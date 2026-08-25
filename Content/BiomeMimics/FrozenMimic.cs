using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class FrozenMimic : BiomeMimicTemplate {
        protected override int BaseKeyType => ItemID.FrozenKey;
        protected override int WeaponType => ItemID.StaffoftheFrostHydra;
        protected override int ChestStyle => 22;
        protected override string TemplateName => "Frozen";
    }
}
