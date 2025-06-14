using EverythingRenewableNow.Content.Projectiles.DroppedBiomeChests;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.BiomeChestMimics {
    public class BiomeChestMimicLoader : ILoadable {
        public void Load(Mod mod) {
            Register(mod, "Corruption", 19, ItemID.ScourgeoftheCorruptor);
            Register(mod, "Crimson", 20, ItemID.VampireKnives);
            Register(mod, "Desert", 12, ItemID.StormTigerStaff, TileID.Containers2);
            Register(mod, "Hallowed", 21, ItemID.RainbowGun);
            Register(mod, "Ice", 22, ItemID.StaffoftheFrostHydra);
            Register(mod, "Jungle", 18, ItemID.PiranhaGun);
        }

        private static void Register(Mod mod, string biome, int chestTileStyle, int itemType, int chestTileType = TileID.Containers) {
            DroppedBiomeChest projectile = new(biome, chestTileStyle, itemType, chestTileType);
            mod.AddContent(projectile);
            mod.AddContent(new BiomeChestMimic(projectile.Type, biome));
        }

        public void Unload() { }
    }
}
