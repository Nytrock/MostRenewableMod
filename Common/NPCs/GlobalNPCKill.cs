using Terraria;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCKill : GlobalNPC {
        public override void HitEffect(NPC npc, NPC.HitInfo hit) {
            if (npc.life > 0) return;
            if (!npc.SpawnedFromStatue) return;

            NPC.killCount[Item.NPCtoBanner(npc.BannerID())]--;
        }
    }
}
