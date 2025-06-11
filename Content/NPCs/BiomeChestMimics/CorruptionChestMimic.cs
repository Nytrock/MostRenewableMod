using EverythingRenewableNow.Content.Projectiles.DroppedBiomeChests;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.BiomeChestMimics {
    public class CorruptionChestMimic : BiomeChestMimic {
        public override int ProjectileType => ModContent.ProjectileType<DroppedCorruptionChest>();
    }
}
