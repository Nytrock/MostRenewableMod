using Terraria.ID;

namespace EverythingRenewableNow.Content.Projectiles.DroppedBiomeChests {
    public class DroppedDesertChest : DroppedBiomeChest {
        public override int ChestTileType => TileID.Containers2;
        public override int ChestTileSubType => 12;
        public override int ItemType => ItemID.StormTigerStaff;
    }
}
