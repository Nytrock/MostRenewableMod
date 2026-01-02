using EverythingRenewableNow.Common.Systems.Dungeon;
using EverythingRenewableNow.Content.Items.PaintingsBags;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Items {
    public class GlobalItemLoot : GlobalItem {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
            if (item.type == ItemID.GolemBossBag)
                itemLoot.Add(ItemDropRule.Common(ItemID.LihzahrdBrick, 1, 35, 70));

            if (item.type == ItemID.WoodenCrate || item.type == ItemID.WoodenCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is SequentialRulesNotScalingWithLuckRule baitRule
                            && baitRule.rules[0] is CommonDropNotScalingWithLuck journeymanRule
                            && journeymanRule.itemId == ItemID.JourneymanBait) {
                        baitRule.rules[0] = ItemDropRule.NotScalingWithLuck(ItemID.CanOfWorms, 9, 1, 2);
                    }

                    if (rule is not AlwaysAtleastOneSuccessDropRule targetRule)
                        continue;

                    for (int i = 0; i < targetRule.rules.Length; i++) {
                        if (targetRule.rules[i] is OneFromOptionsNotScaledWithLuckDropRule woodenRule && woodenRule.dropIds.Contains(ItemID.Aglet)) {
                            targetRule.rules[i] = ItemDropRule.OneFromOptionsNotScalingWithLuck(50, ItemID.Extractinator, ItemID.FlareGun);
                        }

                        if (targetRule.rules[i] is SequentialRulesNotScalingWithLuckRule crateRule
                            && crateRule.rules[0] is CommonDropNotScalingWithLuck bootsRule
                            && bootsRule.itemId == ItemID.SailfishBoots) {
                            crateRule.rules = [.. crateRule.rules.Take(2)];
                        }
                    }
                }
            }

            if (item.type == ItemID.IronCrate || item.type == ItemID.IronCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is SequentialRulesNotScalingWithLuckRule baitRule
                            && baitRule.rules[0] is CommonDropNotScalingWithLuck masterRule
                            && masterRule.itemId == ItemID.MasterBait) {
                        baitRule.rules[0] = ItemDropRule.NotScalingWithLuck(ItemID.CanOfWorms, 9, 1, 3);
                    }
                }
            }

            if (item.type == ItemID.GoldenCrate || item.type == ItemID.GoldenCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is not AlwaysAtleastOneSuccessDropRule targetRule)
                        continue;

                    for (int i = 0; i < targetRule.rules.Length; i++) {
                        if (targetRule.rules[i] is CommonDropNotScalingWithLuck childRule && childRule.itemId == ItemID.EnchantedSword) {
                            IItemDropRule terragrimRule = ItemDropRule.NotScalingWithLuck(ItemID.Terragrim, 20);
                            IItemDropRule enchantedSwordRule = ItemDropRule.NotScalingWithLuck(ItemID.EnchantedSword);
                            targetRule.rules[i] = ItemDropRule.SequentialRulesNotScalingWithLuck(25, terragrimRule, enchantedSwordRule);
                        }
                    }
                }
            }

            if (item.type == ItemID.JungleFishingCrate || item.type == ItemID.JungleFishingCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is not AlwaysAtleastOneSuccessDropRule targetRule)
                        continue;

                    targetRule.rules = [.. targetRule.rules, ItemDropRule.NotScalingWithLuck(ItemID.BeeMinecart, 20)];
                }
            }

            if (item.type == ItemID.FloatingIslandFishingCrate || item.type == ItemID.FloatingIslandFishingCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is not AlwaysAtleastOneSuccessDropRule targetRule)
                        continue;

                    targetRule.rules = [..
                        Array.FindAll(targetRule.rules, (rule) => !(rule is OneFromOptionsNotScaledWithLuckDropRule paintingsRule && paintingsRule.dropIds.Contains(ItemID.HighPitch)))
                    ];
                }
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SkyPaintingsBag>()));
            }

            if (item.type == ItemID.DungeonFishingCrate || item.type == ItemID.DungeonFishingCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is not AlwaysAtleastOneSuccessDropRule targetRule)
                        continue;

                    for (int i = 0; i < targetRule.rules.Length; i++) {
                        if (targetRule.rules[i] is CommonDropNotScalingWithLuck bookRule && bookRule.itemId == ItemID.Book) {
                            IItemDropRule waterBoltRule = ItemDropRule.NotScalingWithLuck(ItemID.WaterBolt, 100);
                            IItemDropRule booksRule = ItemDropRule.NotScalingWithLuck(ItemID.Book, 1, 5, 15);
                            targetRule.rules[i] = ItemDropRule.SequentialRulesNotScalingWithLuck(2, waterBoltRule, bookRule);
                        }
                    }

                    IItemDropRule spikesRule = ItemDropRule.NotScalingWithLuck(ItemID.Spike, 5, 10, 25);
                    targetRule.rules = [.. targetRule.rules, spikesRule];
                }

                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DungeonPaintingsBag>()));
                itemLoot.Add(ItemDropRule.ByCondition(new DungeonConditions.PinkBrick(), ItemID.PinkBrick, 1, 25, 50));
                itemLoot.Add(ItemDropRule.ByCondition(new DungeonConditions.GreenBrick(), ItemID.GreenBrick, 1, 25, 50));
                itemLoot.Add(ItemDropRule.ByCondition(new DungeonConditions.BlueBrick(), ItemID.BlueBrick, 1, 25, 50));
            }

            if (item.type == ItemID.FrozenCrate || item.type == ItemID.FrozenCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is not AlwaysAtleastOneSuccessDropRule targetRule)
                        continue;

                    for (int i = 0; i < targetRule.rules.Length; i++) {
                        if (targetRule.rules[i] is OneFromRulesRule chestRule
                            && chestRule.options[0] is CommonDropNotScalingWithLuck boomerangRule
                            && boomerangRule.itemId == ItemID.IceBoomerang) {
                            chestRule.options = [.. chestRule.options, ItemDropRule.NotScalingWithLuck(ItemID.IceMirror)];
                        }
                    }
                }
            }

            if (item.type == ItemID.OasisCrate || item.type == ItemID.OasisCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is not AlwaysAtleastOneSuccessDropRule targetRule)
                        continue;

                    for (int i = 0; i < targetRule.rules.Length; i++) {
                        if (targetRule.rules[i] is OneFromOptionsNotScaledWithLuckDropRule chestRule
                            && chestRule.dropIds.Contains(ItemID.AncientChisel)) {

                            for (int j = 0; j < chestRule.dropIds.Length; j++)
                                if (chestRule.dropIds[j] == ItemID.ScarabFishingRod)
                                    chestRule.dropIds[j] = ItemID.UncumberingStone;

                        }

                        if (targetRule.rules[i] is CommonDropNotScalingWithLuck bottleRule && bottleRule.itemId == ItemID.SandstorminaBottle) {
                            targetRule.rules[i] = ItemDropRule.OneFromOptionsNotScalingWithLuck(35, ItemID.SandstorminaBottle, ItemID.FlyingCarpet);
                        }

                        if (targetRule.rules[i] is CommonDropNotScalingWithLuck bombRule && bombRule.itemId == ItemID.ScarabBomb) {
                            targetRule.rules = Array.FindAll(targetRule.rules, rule => rule != bombRule);
                            i--;
                        }
                    }
                }
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DesertPaintingsBag>()));
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.DesertMinecart, 20));
            }

            if (item.type == ItemID.ObsidianLockbox) {
                itemLoot.Add(itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Hellstone, 1, 20, 35)));
            }

            if (item.type == ItemID.LavaCrate || item.type == ItemID.LavaCrateHard) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is not AlwaysAtleastOneSuccessDropRule targetRule)
                        continue;

                    for (int i = 0; i < targetRule.rules.Length; i++) {
                        if (targetRule.rules[i] is CommonDropNotScalingWithLuck potRule && potRule.itemId == ItemID.PotSuspended) {
                            targetRule.rules = Array.FindAll(targetRule.rules, rule => rule != potRule);
                            i--;
                        }

                        if (targetRule.rules[i] is OneFromOptionsNotScaledWithLuckDropRule plantsRule && plantsRule.dropIds.Contains(ItemID.PottedLavaPlantPalm)) {
                            targetRule.rules = Array.FindAll(targetRule.rules, rule => rule != plantsRule);
                            i--;
                        }
                    }
                }
;
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<HellPaintingsBag>()));
            }

            if (item.type == ItemID.Present) {
                foreach (var rule in itemLoot.Get()) {
                    if (rule is not SequentialRulesNotScalingWithLuckRule presentRule)
                        continue;

                    presentRule.rules = Array.FindAll(presentRule.rules, rule => !(rule is CommonDrop globeRule && globeRule.itemId == ItemID.SnowGlobe));
                }
            }
        }
    }
}
