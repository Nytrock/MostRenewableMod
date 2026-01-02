using EverythingRenewableNow.Common.Configs;
using Terraria;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems {
    public class SeasonalEventsSystem : ModSystem {
        private SeasonalEventsConfig _config;

        public override void PostUpdateTime() {
            if (_config.HardToggle) {
                Main.xMas = _config.ChristmasToggle;
                Main.halloween = _config.HalloweenToggle;
            } else {
                Main.xMas |= _config.ChristmasToggle;
                Main.halloween |= _config.HalloweenToggle;
            }
        }

        public override void Load() {
            _config = ModContent.GetInstance<SeasonalEventsConfig>();
        }
    }
}
