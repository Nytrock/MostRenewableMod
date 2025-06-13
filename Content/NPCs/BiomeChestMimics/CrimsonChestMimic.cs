using EverythingRenewableNow.Content.Projectiles.DroppedBiomeChests;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.BiomeChestMimics {
    public class CrimsonChestMimic : BiomeChestMimic {
        public override int ProjectileType => ModContent.ProjectileType<DroppedCrimsonChest>();
        public override string MimicName => nameof(CrimsonChestMimic);
    }
}
