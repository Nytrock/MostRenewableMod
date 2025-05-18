using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCLoot : GlobalNPC {
        public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot) {
            if (npc.type == NPCID.WallCreeper || npc.type == NPCID.WallCreeperWall) {
                npcLoot.Add(ItemDropRule.Common(ItemID.WebSlinger, 50));
            }

            if (npc.type == NPCID.Golem) {
                LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.LihzahrdBrick, minimumDropped: 25, maximumDropped: 50));
                npcLoot.Add(notExpertRule);
            }
        }
    }
}
