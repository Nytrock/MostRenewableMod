using Terraria;
using Terraria.ID;

namespace EverythingRenewableNow.Utils {
    public static class NPCUtils {
        public static bool SpawnMechQueenAnywhere(Player player) {
            int playerIndex = player.whoAmI;
            if (NPC.AnyNPCs(127) || NPC.AnyNPCs(134) || NPC.AnyNPCs(125) || NPC.AnyNPCs(126))
                return false;

            if (Main.netMode == NetmodeID.MultiplayerClient) {
                NetMessage.SendData(61, -1, -1, null, playerIndex, -16f);
            } else {
                NPC.mechQueen = -2;
                NPC.SpawnOnPlayer(playerIndex, 127);
                NPC.mechQueen = NPC.FindFirstNPC(127);
                NPC.NewNPC(NPC.GetBossSpawnSource(playerIndex), (int)Main.npc[NPC.mechQueen].Center.X, (int)Main.npc[NPC.mechQueen].Center.Y, 125, 1);
                NPC.NewNPC(NPC.GetBossSpawnSource(playerIndex), (int)Main.npc[NPC.mechQueen].Center.X, (int)Main.npc[NPC.mechQueen].Center.Y, 126, 1);
                int num = NPC.NewNPC(NPC.GetBossSpawnSource(playerIndex), (int)Main.npc[NPC.mechQueen].Center.X, (int)Main.npc[NPC.mechQueen].Center.Y, 134, 1);
                NPC.NewNPC(NPC.GetBossSpawnSource(playerIndex), (int)Main.npc[NPC.mechQueen].Center.X, (int)Main.npc[NPC.mechQueen].Center.Y, 139, 1, 0f, 0f, num, -1f);
                NPC.NewNPC(NPC.GetBossSpawnSource(playerIndex), (int)Main.npc[NPC.mechQueen].Center.X, (int)Main.npc[NPC.mechQueen].Center.Y, 139, 1, 0f, 0f, num, 1f);
            }

            return true;
        }
    }
}
