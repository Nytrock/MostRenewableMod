using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class FrozenMimic : BiomeMimicTemplate {
        protected override int KeyType => ItemID.FrozenKey;
        protected override int WeaponType => ItemID.StaffoftheFrostHydra;
        protected override int KeyHeight => 26;
        protected override int KeyWidth => 36;
        protected override int ChestStyle => 22;
        protected override string Name => "Frozen";
    }
}
