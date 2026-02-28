using EverythingRenewableNow.Utils;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Players {
    public class PlayerBiomeMimicSpawn : ModPlayer {
        public override void PreUpdate() {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            bool needToCheckChest = Player.chest == -1 && Player.lastChest >= 0 && Main.chest[Player.lastChest] != null;
            if (!needToCheckChest)
                return;

            Chest chest = Main.chest[Player.lastChest];
            int mimicType = WhichBiomeChestMimicCanSpawn(chest);
            if (mimicType == -1)
                return;

            Chest.DestroyChestDirect(chest.x, chest.y, Player.lastChest);
            for (int k = chest.x; k <= chest.x + 1; k++) {
                for (int l = chest.y; l <= chest.y + 1; l++) {
                    if (TileID.Sets.BasicChest[Main.tile[k, l].TileType]) {
                        Main.tile[k, l].ClearTile();
                    }
                }
            }

            int number2 = 1;
            if (Main.tile[chest.x, chest.y].TileType == TileID.Containers2)
                number2 = 5;
            if (Main.tile[chest.x, chest.y].TileType >= TileID.Count)
                number2 = 101;
            NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, number2, chest.x, chest.y, 0f, Player.lastChest, Main.tile[chest.x, chest.y].TileType);
            NetMessage.SendTileSquare(-1, chest.x, chest.y, 3);

            int mimicIndex = NPC.NewNPC(Player.GetSource_TileInteraction(chest.x, chest.y), chest.x * 16 + 16, chest.y * 16 + 32, mimicType);
            Main.npc[mimicIndex].whoAmI = mimicIndex;
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, mimicIndex);
            Main.npc[mimicIndex].BigMimicSpawnSmoke();
        }

        public static int WhichBiomeChestMimicCanSpawn(Chest chest) {
            if (!NPC.downedPlantBoss)
                return -1;

            Dictionary<int, int> keysAndMimics = new() {
                { ModContentUtils.ItemType("AwakenedCorruptionKey"), ModContentUtils.NPCType("CorruptionChestMimic") },
                { ModContentUtils.ItemType("AwakenedCrimsonKey"), ModContentUtils.NPCType("CrimsonChestMimic") },
                { ModContentUtils.ItemType("AwakenedDesertKey"), ModContentUtils.NPCType("DesertChestMimic") },
                { ModContentUtils.ItemType("AwakenedFrozenKey"), ModContentUtils.NPCType("IceChestMimic") },
                { ModContentUtils.ItemType("AwakenedHallowedKey"), ModContentUtils.NPCType("HallowedChestMimic") },
                { ModContentUtils.ItemType("AwakenedJungleKey"), ModContentUtils.NPCType("JungleChestMimic") },
            };
            Tile chestTile = Main.tile[chest.x, chest.y];
            int chestStyle = chestTile.TileFrameX / 36;

            bool chestFits = TileID.Sets.BasicChest[chestTile.TileType] && (chestTile.TileType != TileID.Containers || chestStyle != 5 && chestStyle != 6);
            if (!chestFits)
                return -1;

            int keyInChest = -1;
            int[] keys = [.. keysAndMimics.Keys];
            foreach (var item in chest.item) {
                if (item == null) continue;
                if (item.type <= ItemID.None) continue;

                if (keys.Contains(item.type) && item.stack == 1) {
                    if (keyInChest != -1)
                        return -1;
                    keyInChest = item.type;
                }
            }

            if (keyInChest == -1)
                return -1;
            return keysAndMimics[keyInChest];
        }
    }
}
