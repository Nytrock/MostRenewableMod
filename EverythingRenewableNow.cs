using EverythingRenewableNow.Utils;
using Terraria.ModLoader;

namespace EverythingRenewableNow {
    public class EverythingRenewableNow : Mod {
        public override void Load() {
            CarrotUtils.UnlockCarrot();
        }

        public override void Unload() {
            CarrotUtils.LockCarrot();
        }
    }
}
