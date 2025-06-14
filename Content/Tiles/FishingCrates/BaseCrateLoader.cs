using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Tiles.FishingCrates {
    public class BaseCrateLoader : ILoadable {
        public void Load(Mod mod) {
            Register(mod, "CavernCrate");
            Register(mod, "CavernCrateHardmode");
            Register(mod, "ForestCrate");
            Register(mod, "ForestCrateHardmode");
            Register(mod, "MushroomCrate");
            Register(mod, "MushroomCrateHardmode");
            Register(mod, "TempleCrate");
            Register(mod, "TempleCrateHardmode");
        }

        public static void Register(Mod mod, string crateName) {
            BaseCrate crate = new(crateName);
            mod.AddContent(crate);
        }

        public void Unload() { }
    }
}
