using EverythingRenewableNow.Content.Items.Boulder;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCLoot : GlobalNPC {
        public override void AI(NPC npc) {
            if (npc.type != NPCID.CultistTablet)
                return;

            if (npc.ai[0] == -1f && npc.ai[3] > 300f) {
                if (!Main.rand.NextBool(10))
                    return;
                Item.NewItem(npc.GetSource_FromThis(), npc.Center, ItemID.MoonLordLegs);
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            AddBoulderNPCLoot(npc, npcLoot);

            if (npc.type == NPCID.WallCreeper || npc.type == NPCID.WallCreeperWall)
                npcLoot.Add(ItemDropRule.Common(ItemID.WebSlinger, 50));
        }

        private static void AddBoulderNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (npc.type == NPCID.Raven)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RavenFeather>(), 10));
        }
    }
}
