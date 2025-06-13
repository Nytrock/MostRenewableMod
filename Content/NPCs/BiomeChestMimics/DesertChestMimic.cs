using EverythingRenewableNow.Content.Projectiles.DroppedBiomeChests;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.BiomeChestMimics {
    public class DesertChestMimic : BiomeChestMimic {
        public override int ProjectileType => ModContent.ProjectileType<DroppedDesertChest>();
        public override string MimicName => nameof(DesertChestMimic);
    }
}
