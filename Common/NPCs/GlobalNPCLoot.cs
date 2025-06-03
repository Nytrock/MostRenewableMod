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
            if (npc.type == NPCID.WallCreeper || npc.type == NPCID.WallCreeperWall) {
                npcLoot.Add(ItemDropRule.Common(ItemID.WebSlinger, 50));
            }

            if (npc.type == NPCID.Golem) {
                LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.LihzahrdBrick, minimumDropped: 25, maximumDropped: 50));
                npcLoot.Add(notExpertRule);
            }

            if (npc.type == NPCID.DuneSplicerHead) {
                npcLoot.Add(ItemDropRule.Common(ItemID.DesertFossil, 2, 5, 20));
            }

            if (npc.type == NPCID.IceSlime || npc.type == NPCID.SpikedIceSlime) {
                npcLoot.Add(ItemDropRule.Common(ItemID.IceBlock, 100, 3, 13));
            }
        }
    }
}
