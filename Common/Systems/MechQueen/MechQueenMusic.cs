using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems.MechQueen {
    public class MechQueenMusic : ModSceneEffect {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override int Music => MusicID.OtherworldlyBoss1;

        public override bool IsSceneEffectActive(Player player) {
            return MechQueenSystem.IsMechQueenAlive;
        }
    }
}
