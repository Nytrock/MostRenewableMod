using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EverythingRenewableNow.Common.Configs {
    public class SeasonalEventsConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(true)]
        public bool ChristmasToggle;

        [DefaultValue(true)]
        public bool HalloweenToggle;
    }
}
