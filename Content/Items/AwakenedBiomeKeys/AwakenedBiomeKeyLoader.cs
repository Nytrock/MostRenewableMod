using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.AwakenedBiomeKeys {
    public class AwakenedBiomeKeyLoader : ILoadable {
        public void Load(Mod mod) {
            Register(mod, 26, 36, "Corruption");
            Register(mod, 24, 38, "Crimson");
            Register(mod, 28, 38, "Desert");
            Register(mod, 26, 36, "Frozen");
            Register(mod, 26, 36, "Hallowed");
            Register(mod, 24, 36, "Jungle");
        }

        private static void Register(Mod mod, int width, int heigth, string biome) {
            AwakenedBiomeKey key = new(width, heigth, biome);
            mod.AddContent(key);
        }

        public void Unload() { }
    }
}
