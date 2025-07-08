using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EverythingRenewableNow.Common.Configs {
    public class GameplayConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(true)]
        public bool StatuesToggle;
    }
}
