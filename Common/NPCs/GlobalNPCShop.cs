using EverythingRenewableNow.Content.Items.PaintingsBags;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCShop : GlobalNPC {
        public override void ModifyShop(NPCShop shop) {
            if (shop.NpcType == NPCID.Steampunker) {
                if (!Main.remixWorld) {
                    shop.Add(ItemID.PurpleSolution, Condition.EclipseOrBloodMoon, Condition.CrimsonWorld);
                    shop.Add(ItemID.RedSolution, Condition.EclipseOrBloodMoon, Condition.CorruptWorld);
                } else {
                    shop.Add(ItemID.Clentaminator);
                    shop.Add(ItemID.PurpleSolution, Condition.EclipseOrBloodMoon);
                    shop.Add(ItemID.RedSolution, Condition.EclipseOrBloodMoon);
                    shop.Add(ItemID.BlueSolution, Condition.InHallow);
                    shop.Add(ItemID.GreenSolution, Condition.NotEclipseAndNotBloodMoon, Condition.NotInHallow);
                    shop.Add(ItemID.SandSolution, Condition.DownedMoonLord);
                    shop.Add(ItemID.SnowSolution, Condition.DownedMoonLord);
                    shop.Add(ItemID.DirtSolution, Condition.DownedMoonLord);
                }
            }

            if (shop.NpcType == NPCID.Truffle) {
                shop.Add(ItemID.DarkBlueSolution, Condition.RemixWorld);
            }

            if (shop.NpcType == NPCID.Dryad) {
                shop.Add(ItemID.CorruptGrassEcho, Condition.CrimsonWorld, Condition.BloodMoon);
                shop.Add(ItemID.CrimsonGrassEcho, Condition.CorruptWorld, Condition.BloodMoon);
                shop.Add(ItemID.CrimsonPlanterBox, Condition.CorruptWorld, Condition.DownedEowOrBoc);
                shop.Add(ItemID.CorruptPlanterBox, Condition.CrimsonWorld, Condition.DownedEowOrBoc);
            }

            if (shop.NpcType == NPCID.Demolitionist) {
                shop.Add(new Item(ItemID.Detonator) {
                    shopCustomPrice = Item.buyPrice(gold: 2)
                }, Condition.NpcIsPresent(NPCID.Mechanic));
            }

            if (shop.NpcType == NPCID.DyeTrader) {
                List<int> dungeonBanners = [ItemID.MarchingBonesBanner, ItemID.NecromanticSign,
                    ItemID.RustedCompanyStandard, ItemID.RaggedBrotherhoodSigil,
                    ItemID.MoltenLegionFlag, ItemID.DiabolicSigil];
                List<int> spaceBanners = [ItemID.WorldBanner, ItemID.SunBanner, ItemID.GravityBanner];
                List<int> underworldBanners = [ItemID.HellboundBanner, ItemID.HellHammerBanner, ItemID.HelltowerBanner,
                    ItemID.LostHopesofManBanner, ItemID.ObsidianWatcherBanner, ItemID.LavaEruptsBanner];
                List<int> desertBanners = [ItemID.AnkhBanner, ItemID.SnakeBanner, ItemID.SnakeBanner];

                foreach (var dungeonBanner in dungeonBanners) {
                    shop.Add(new Item(dungeonBanner) {
                        shopCustomPrice = Item.buyPrice(gold: 1)
                    }, Condition.EclipseOrBloodMoon);
                }

                foreach (var spaceBanner in spaceBanners) {
                    shop.Add(new Item(spaceBanner) {
                        shopCustomPrice = Item.buyPrice(gold: 1)
                    }, Condition.InSpace);
                }

                foreach (var underworldBanner in underworldBanners) {
                    shop.Add(new Item(underworldBanner) {
                        shopCustomPrice = Item.buyPrice(gold: 1)
                    }, Condition.InUnderworld);
                }

                foreach (var desertBanner in desertBanners) {
                    shop.Add(new Item(desertBanner) {
                        shopCustomPrice = Item.buyPrice(gold: 1)
                    }, Condition.InDesert);
                }
            }

            if (shop.NpcType == NPCID.Painter && shop.Name == "Decor") {
                shop.Add(ModContent.ItemType<SkyPaintingsBag>(), Condition.InSpace);
                shop.Add(ModContent.ItemType<DesertPaintingsBag>(), Condition.InDesert);
                shop.Add(ModContent.ItemType<CavernPaintingsBag>(), Condition.InBelowSurface, Condition.NotInUnderworld);
                shop.Add(ModContent.ItemType<DungeonPaintingsBag>(), Condition.EclipseOrBloodMoon);
                shop.Add(ModContent.ItemType<HellPaintingsBag>(), Condition.InUnderworld);
            }
        }

        public override void SetupTravelShop(int[] shop, ref int nextSlot) {
            if (Main.rand.NextBool(20)) {
                shop[nextSlot] = ItemID.AngelStatue;
                nextSlot++;
            }
        }
    }
}
