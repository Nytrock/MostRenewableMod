using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class CrimsonMimic : BiomeMimicTemplate {
        protected override int KeyType => ItemID.CrimsonKey;
        protected override int WeaponType => ItemID.VampireKnives;
        protected override int KeyHeight => 24;
        protected override int KeyWidth => 38;
        protected override int ChestStyle => 20;
        protected override string Name => "Crimson";
    }
}
