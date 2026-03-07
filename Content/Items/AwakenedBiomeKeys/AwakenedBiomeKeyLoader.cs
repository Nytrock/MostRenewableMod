using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.AwakenedBiomeKeys {
    public class AwakenedBiomeKeyLoader : ILoadable {
        public void Load(Mod mod) {
            Register(mod, 26, 36, "Corruption", ItemID.CorruptionKey);
            Register(mod, 24, 38, "Crimson", ItemID.CrimsonKey);
            Register(mod, 28, 38, "Desert", ItemID.DungeonDesertKey);
            Register(mod, 26, 36, "Frozen", ItemID.FrozenKey);
            Register(mod, 26, 36, "Hallowed", ItemID.HallowedKey);
            Register(mod, 24, 36, "Jungle", ItemID.JungleKey);
        }

        private static void Register(Mod mod, int width, int height, string biome, int biomeKey) {
            AwakenedBiomeKey key = new(width, height, biome, biomeKey);
            mod.AddContent(key);
        }

        public void Unload() { }
    }
}
