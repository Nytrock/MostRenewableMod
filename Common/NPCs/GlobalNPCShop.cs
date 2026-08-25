using EverythingRenewableNow.Common.Systems;
using EverythingRenewableNow.Content.Items.Boulder;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.NPCs {
    public class GlobalNPCShop : GlobalNPC {
        public override void ModifyShop(NPCShop shop) {
            if (shop.NpcType == NPCID.Steampunker) {
                shop.Add(ItemID.Clentaminator, Condition.RemixWorld);
                shop.Add(ItemID.LihzahrdFurnace, Condition.InJungle, Condition.DownedGolem);
                shop.Add(ItemID.BlueSolution, Condition.InHallow, Condition.RemixWorld);
                shop.Add(ItemID.GreenSolution, Condition.NotEclipseAndNotBloodMoon, Condition.NotInHallow, Condition.RemixWorld);
                shop.Add(ItemID.SandSolution, Condition.DownedMoonLord, Condition.RemixWorld);
                shop.Add(ItemID.SnowSolution, Condition.DownedMoonLord, Condition.RemixWorld);
                shop.Add(ItemID.DirtSolution, Condition.DownedMoonLord, Condition.RemixWorld);
                shop.Add(ItemID.PurpleSolution, Condition.EclipseOrBloodMoon, Condition.CorruptWorld, Condition.RemixWorld);
                shop.Add(ItemID.RedSolution, Condition.EclipseOrBloodMoon, Condition.CrimsonWorld, Condition.RemixWorld);
            }

            if (shop.NpcType == NPCID.Truffle) {
                shop.Add(ItemID.DarkBlueSolution, Condition.RemixWorld);
            }

            if (shop.NpcType == NPCID.Dryad) {
                shop.Add(ItemID.CorruptGrassEcho, Condition.CrimsonWorld, Condition.BloodMoon, Condition.InGraveyard, Condition.NotDrunkWorld);
                shop.Add(ItemID.CrimsonGrassEcho, Condition.CorruptWorld, Condition.BloodMoon, Condition.InGraveyard, Condition.NotDrunkWorld);
                shop.Add(ItemID.CrimsonPlanterBox, Condition.CorruptWorld, Condition.DownedEowOrBoc, Condition.InGraveyard, Condition.NotDrunkWorld);
                shop.Add(ItemID.CorruptPlanterBox, Condition.CrimsonWorld, Condition.DownedEowOrBoc, Condition.InGraveyard, Condition.NotDrunkWorld);

                shop.Add(ItemID.PottedLavaPlantPalm, Condition.MoonPhasesQuarter0, Condition.InUnderworld);
                shop.Add(ItemID.PottedLavaPlantBush, Condition.MoonPhasesQuarter1, Condition.InUnderworld);
                shop.Add(ItemID.PottedLavaPlantBramble, Condition.MoonPhasesQuarter2, Condition.InUnderworld);
                shop.Add(ItemID.PottedLavaPlantBulb, Condition.MoonPhasesQuarter3, Condition.InUnderworld);
                shop.Add(ItemID.PottedLavaPlantTendrils, Condition.InUnderworld);
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
                List<int> desertBanners = [ItemID.AnkhBanner, ItemID.SnakeBanner, ItemID.OmegaBanner];

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

            if (CrossModSystem.Calamity != null)
                ModifyCalamityShop(shop);
            ModifyBoulderShop(shop);
        }

        [JITWhenModsEnabled("CalamityMod")]
        private static void ModifyCalamityShop(NPCShop shop) {
            Mod calamity = CrossModSystem.Calamity;

            if (shop.NpcType == NPCID.Steampunker)
                shop.Add(calamity.Find<ModItem>("AstralSolution").Type, Condition.RemixWorld);

            if (shop.NpcType == calamity.Find<ModNPC>("ShadySalesman").Type) {
                int logsPrice = Item.buyPrice(gold: 1);
                Dictionary<int, Condition> logs = new() {
                    {ModContent.ItemType<CalamityMod.Items.DraedonMisc.DraedonsLogHell>(), Condition.InUnderworld },
                    {ModContent.ItemType<CalamityMod.Items.DraedonMisc.DraedonsLogJungle>(), Condition.InJungle },
                    {ModContent.ItemType<CalamityMod.Items.DraedonMisc.DraedonsLogPlanetoid>(), Condition.InSpace },
                    {ModContent.ItemType<CalamityMod.Items.DraedonMisc.DraedonsLogSnowBiome>(), Condition.InSnow },
                    {ModContent.ItemType<CalamityMod.Items.DraedonMisc.DraedonsLogSunkenSea>(), CalamityMod.CalamityConditions.InSunken }
                };

                foreach (var log in logs) {
                    shop.Add(new Item(log.Key) { shopCustomPrice = logsPrice }, log.Value);
                    CalamityMod.Systems.Collections.CalamityItemSets.HasSalesmanText[log.Key] = true;
                }
            }
        }

        private static void ModifyBoulderShop(NPCShop shop) {
            if (shop.NpcType == NPCID.Stylist)
                shop.Add(ModContent.ItemType<PrettyMirror>(), Condition.InGraveyard);

            if (shop.NpcType == NPCID.Demolitionist)
                shop.Add(ModContent.ItemType<IceGrenade>(), Condition.TimeNight, Condition.DownedEyeOfCthulhu);
        }
    }
}
