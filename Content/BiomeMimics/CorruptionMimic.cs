using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class CorruptionMimic : BiomeMimicTemplate {
        protected override int KeyType => ItemID.CorruptionKey;
        protected override int WeaponType => ItemID.ScourgeoftheCorruptor;
        protected override int KeyHeight => 26;
        protected override int KeyWidth => 36;
        protected override int ChestStyle => 19;
        protected override string Name => "Corruption";
    }
}
