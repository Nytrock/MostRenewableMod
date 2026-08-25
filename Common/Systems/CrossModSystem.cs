using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems {
    public class CrossModSystem : ModSystem {
        public static Mod Calamity { get; private set; }

        public override void Load() {
            Calamity = null;
            ModLoader.TryGetMod("CalamityMod", out Mod calamity);
            Calamity = calamity;
        }
    }
}
