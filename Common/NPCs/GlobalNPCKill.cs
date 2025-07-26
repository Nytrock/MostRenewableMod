using EverythingRenewableNow.Common.Configs;
using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCKill : GlobalNPC {
        public override void HitEffect(NPC npc, NPC.HitInfo hit) {
            if (npc.life > 0) return;
            KillChecks(npc);
        }

        private static void KillChecks(NPC npc) {
            PreventBannerEnemiesCount(npc);
            TrySpawnDungeonGuardian(npc);
        }

        private static void PreventBannerEnemiesCount(NPC npc) {
            int bannerID = Item.NPCtoBanner(npc.BannerID());

            if (NPC.killCount[bannerID] == 0) return;
            if (!ModContent.GetInstance<GameplayConfig>().StatuesToggle) return;
            if (!npc.SpawnedFromStatue) return;

            NPC.killCount[bannerID]--;
        }

        private static void TrySpawnDungeonGuardian(NPC npc) {
            if (Main.netMode != NetmodeID.MultiplayerClient && Main.IsItDay() && npc.type == NPCID.Clothier && !NPC.AnyNPCs(NPCID.DungeonGuardian)) {
                for (int i = 0; i < 255; i++) {
                    if (Main.player[i].active && !Main.player[i].dead && Main.player[i].killClothier) {
                        NPCUtils.SpawnDungeonGuardian(i, npc);
                        break;
                    }
                }
            }
        }
    }
}
