using EverythingRenewableNow.Content.Projectiles.DroppedBiomeChests;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.BiomeChestMimics {
    public class HallowedChestMimic : BiomeChestMimic {
        public override int ProjectileType => ModContent.ProjectileType<DroppedHallowedChest>();
        public override string MimicName => nameof(HallowedChestMimic);
    }
}
