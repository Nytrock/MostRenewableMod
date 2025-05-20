using EverythingRenewableNow.Common.Systems.MechQueen;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCMechQueen : GlobalNPC {
        private static readonly List<int> _mechQueenParts = [NPCID.Retinazer, NPCID.Spazmatism, NPCID.TheDestroyer, NPCID.SkeletronPrime];

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (_mechQueenParts.Contains(npc.type)) {
                LeadingConditionRule mechQueenDiedRule = new(new MechQueenConditions.Died());
                mechQueenDiedRule.OnSuccess(ItemDropRule.Common(ItemID.WaffleIron));
                npcLoot.Add(mechQueenDiedRule);
            }
        }

        public override bool PreKill(NPC npc) {
            if (MechQueenSystem.IsMechQueenAlive && _mechQueenParts.Contains(npc.type))
                MechQueenSystem.MechQueenPartKilled();
            return base.PreKill(npc);
        }

        public override bool CheckActive(NPC npc) {
            if (MechQueenSystem.IsMechQueenAlive && npc.type == NPCID.SkeletronPrime && npc.timeLeft <= 1)
                MechQueenSystem.MechQueenDisable();
            return true;
        }
    }
}
