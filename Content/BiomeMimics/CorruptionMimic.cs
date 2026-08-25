using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class CorruptionMimic : BiomeMimicTemplate {
        protected override int BaseKeyType => ItemID.CorruptionKey;
        protected override int WeaponType => ItemID.ScourgeoftheCorruptor;
        protected override int ChestStyle => 19;
        protected override string TemplateName => "Corruption";
    }
}
