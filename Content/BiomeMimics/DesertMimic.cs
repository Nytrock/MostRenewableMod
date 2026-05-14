using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ID;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class DesertMimic : BiomeMimicTemplate {
        protected override int KeyType => ItemID.DungeonDesertKey;
        protected override int WeaponType => ItemID.StormTigerStaff;
        protected override int KeyHeight => 28;
        protected override int KeyWidth => 38;
        protected override int ChestStyle => 12;
        protected override int ChestType => TileID.Containers2;
        protected override string Name => "Desert";
    }
}
