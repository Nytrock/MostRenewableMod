using Terraria;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems
{
    public class BunnySystem : ModSystem
    {
        public override void PostUpdateEverything()
        {
            Main.runningCollectorsEdition = true;
        }
    }
}
