using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;

namespace EverythingRenewableNow.Common.Configs {
    public class SeasonalEventsConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(true)]
        public bool ChristmasToggle;

        [DefaultValue(true)]
        public bool HalloweenToggle;

        public override void OnChanged() {
            Main.checkXMas();
            Main.checkHalloween();
        }

        public override void OnLoaded() {
            Mod.Logger.Info(ChristmasToggle);
        }
    }
}
