using DuckLib;
using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCKill : GlobalNPC {
        public override void Load() {
            DuckHook.OnNPCDeath += TrySpawnDungeonGuardian;
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
