using EverythingRenewableNow.Common.Configs;
using Terraria;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems
{
    public class SeasonalEventsSystem : ModSystem
    {
        private SeasonalEventsConfig _config;

        // I don't understand IL editing, so uuuuh...
        public override void PostUpdateTime()
        {
            Main.xMas |= _config.ChristmasToggle;
            Main.halloween |= _config.HalloweenToggle;
        }

        public override void Load()
        {
            _config = ModContent.GetInstance<SeasonalEventsConfig>();
        }
    }
}
