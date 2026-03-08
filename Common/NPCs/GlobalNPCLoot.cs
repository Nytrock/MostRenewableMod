using DuckLib;
using DuckLib.Utils;
using EverythingRenewableNow.Content.Items;
using EverythingRenewableNow.Content.Items.Boulder;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCLoot : GlobalNPC {
        public override void Load() {
            SetSlimeBodyDrops();
        }

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

            if (npc.type == NPCID.Golem)
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.LihzahrdBrick, minimumDropped: 25, maximumDropped: 50));

            if (npc.type == NPCID.Plantera)
                npcLoot.Add(ItemDropRule.ByCondition(new DuckLootCondition.NoTemple(), ModContent.ItemType<MiniTemple>()));

            if (npc.type == NPCID.BrainofCthulhu || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
                npcLoot.Add(ItemDropRule.ByCondition(new DuckLootCondition.NoDungeon(), ModContent.ItemType<MiniDungeon>()));
        }

        private static void AddBoulderNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (npc.type == NPCID.Raven)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RavenFeather>(), 10));

            if (npc.type == NPCID.EyeofCthulhu) {
                LeadingConditionRule crimsonRule = new(new Conditions.IsCrimson());
                crimsonRule.OnSuccess(ItemDropRule.ByCondition(new DuckLootCondition.NoDemonAltars(), ModContent.ItemType<CrimsonAltar>()));
                crimsonRule.OnFailedConditions(ItemDropRule.ByCondition(new DuckLootCondition.NoDemonAltars(), ModContent.ItemType<CorruptionAltar>()));
                npcLoot.Add(crimsonRule);
            }

            if (npc.type == NPCID.DemonEye || npc.type == NPCID.WanderingEye)
                npcLoot.Add(ItemDropRule.ByCondition(new DuckLootCondition.NoDemonAltars(), ItemID.SuspiciousLookingEye, 50));
        }

        // Boulder
        private static void SetSlimeBodyDrops() {
            SlimeBodyItemUtils.AddSlimeBodyItem(CavernSlimeDropCondition, 10, 25, ItemID.StoneBlock, ItemID.Granite, ItemID.Marble);
            SlimeBodyItemUtils.AddSlimeBodyItem(DartTrapSlimeDropCondition, items: ItemID.DartTrap);
            SlimeBodyItemUtils.AddSlimeBodyItem(FossilSlimeDropCondition, 3, 13, ItemID.DesertFossil);
            SlimeBodyItemUtils.AddSlimeBodyItem(LifeCrystalSlimeDropCondition, items: ItemID.LifeCrystal);
        }

        private static bool CavernSlimeDropCondition(NPC npc) {
            return IsThisInTheRockLayer((int)(npc.position.Y / 16f));
        }

        public static bool IsThisInTheRockLayer(int y) {
            if (Main.remixWorld) {
                if (y > Main.worldSurface && y <= Main.rockLayer)
                    return true;
            } else if (y > Main.rockLayer) {
                return true;
            }
            return false;
        }

        private static bool DartTrapSlimeDropCondition(NPC npc) {
            int dartTrapChance = 500;
            if (Main.noTrapsWorld)
                dartTrapChance = 20;
            else if (Main.getGoodWorld)
                dartTrapChance = 100;
            else if (npc.Center.Y < Main.worldSurface * 16.0)
                dartTrapChance = -1;
            return dartTrapChance > 0 && Main.rand.Next(dartTrapChance) == 0;
        }

        private static bool FossilSlimeDropCondition(NPC npc) {
            return npc.netID == NPCID.SandSlime && Main.rand.NextBool(100) && !DuckWorldObserver.FossilObserver.HaveInWorld;
        }

        private static bool LifeCrystalSlimeDropCondition(NPC npc) {
            return Main.rand.NextBool(2000) && !DuckWorldObserver.LifeCrystalObserver.HaveInWorld;
        }
    }
}
