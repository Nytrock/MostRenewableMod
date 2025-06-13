using EverythingRenewableNow.Content.Projectiles.DroppedBiomeChests;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.BiomeChestMimics {
    public class IceChestMimic : BiomeChestMimic {
        public override int ProjectileType => ModContent.ProjectileType<DroppedIceChest>();
        public override string MimicName => nameof(IceChestMimic);
    }
}
