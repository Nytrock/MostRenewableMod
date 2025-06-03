using EverythingRenewableNow.Content.Items.PaintingsBags;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Utils {
    public static class LootUtils {
        public static IItemDropRule[] GetForestCrateRules() {
            int[] surfaceLoot = [
                ItemID.Spear,
                ItemID.Blowpipe,
                ItemID.WoodenBoomerang,
                ItemID.Aglet,
                ItemID.ClimbingClaws,
                ItemID.Umbrella,
                ItemID.CordageGuide,
                ItemID.WandofSparking,
                ItemID.Radar,
                ItemID.PortableStool
            ];
            IItemDropRule surfaceLootRule = ItemDropRule.OneFromOptionsNotScalingWithLuck(1, surfaceLoot);

            IItemDropRule[] minecartsRules = [
                ItemDropRule.Common(ItemID.LadybugMinecart),
                ItemDropRule.Common(ItemID.SunflowerMinecart),
            ];
            IItemDropRule minecartsRule = new OneFromRulesRule(20, minecartsRules);

            return [surfaceLootRule, minecartsRule];
        }

        public static IItemDropRule[] GetCavernCrateRules() {
            int[] cavernLoot = [
                ItemID.BandofRegeneration,
                ItemID.MagicMirror,
                ItemID.CloudinaBottle,
                ItemID.HermesBoots,
                ItemID.Mace,
                ItemID.ShoeSpikes,
            ];
            IItemDropRule cavernLootRule = ItemDropRule.OneFromOptionsNotScalingWithLuck(1, cavernLoot);
            IItemDropRule paintingsRule = ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CavernPaintingsBag>());

            return [cavernLootRule, paintingsRule];
        }

        public static IItemDropRule[] GetMushroomCrateRules() {
            IItemDropRule clothRule = ItemDropRule.Common(ItemID.MushroomHat);
            clothRule.OnSuccess(ItemDropRule.Common(ItemID.MushroomVest));
            clothRule.OnSuccess(ItemDropRule.Common(ItemID.MushroomPants));

            IItemDropRule[] mushroomLootRules = [
                clothRule,
                ItemDropRule.Common(ItemID.Shroomerang),
                ItemDropRule.Common(ItemID.MushroomStatue),
                ItemDropRule.Common(ItemID.ShroomMinecart),
            ];
            IItemDropRule mushroomLootRule = new OneFromRulesRule(1, mushroomLootRules);

            return [mushroomLootRule];
        }

        public static IItemDropRule[] GetTempleCrateRules() {
            IItemDropRule[] trapsRules = [
                ItemDropRule.Common(ItemID.SpikyBallTrap, 1, 5, 10),
                ItemDropRule.Common(ItemID.SpearTrap, 1, 5, 10),
                ItemDropRule.Common(ItemID.FlameTrap, 1, 2, 5),
                ItemDropRule.Common(ItemID.SuperDartTrap, 1, 2, 5),
                ItemDropRule.Common(ItemID.WoodenSpike, 1, 10, 20),
            ];
            IItemDropRule trapsRule = new OneFromRulesRule(1, trapsRules);

            IItemDropRule furnaceRule = ItemDropRule.NotScalingWithLuck(ItemID.LihzahrdFurnace, 10);

            return [trapsRule, furnaceRule];
        }

        public static void AddPreHardmodeFishingCrateLoot(this ItemLoot itemLoot, params IItemDropRule[] extraRules) {
            itemLoot.AddPotionsAndBaitToLoot();

            IItemDropRule coinsRule = ItemDropRule.NotScalingWithLuck(ItemID.GoldCoin, 4, 5, 12);

            IItemDropRule[] oresRules = [
                ItemDropRule.Common(ItemID.CopperOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TinOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.IronOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.LeadOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.SilverOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TungstenOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.GoldOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.PlatinumOre, 1, 20, 35),
            ];
            IItemDropRule oresRule = new OneFromRulesRule(7, oresRules);

            IItemDropRule[] barsRules = [
                ItemDropRule.Common(ItemID.IronBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.SilverBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.GoldBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.LeadBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.TungstenBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.PlatinumBar, 1, 6, 16),
            ];
            IItemDropRule barsRule = new OneFromRulesRule(4, barsRules);

            IItemDropRule[] potionsRules = [
                ItemDropRule.Common(ItemID.ObsidianSkinPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.SpelunkerPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.HunterPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.GravitationPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.MiningPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.HeartreachPotion, 1, 2, 4),
            ];
            IItemDropRule potionsRule = new OneFromRulesRule(4, potionsRules);

            IItemDropRule[] lootDropRules = [coinsRule, oresRule, barsRule, potionsRule, .. extraRules];
            itemLoot.Add(ItemDropRule.AlwaysAtleastOneSuccess(lootDropRules));
        }

        public static void AddHardmodeFishingCrateLoot(this ItemLoot itemLoot, params IItemDropRule[] extraRules) {
            itemLoot.AddPotionsAndBaitToLoot();

            IItemDropRule coinsRule = ItemDropRule.NotScalingWithLuck(ItemID.GoldCoin, 4, 5, 12);

            IItemDropRule[] oresRules = [
               ItemDropRule.Common(ItemID.CopperOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TinOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.IronOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.LeadOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.SilverOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TungstenOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.GoldOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.PlatinumOre, 1, 20, 35),
            ];
            IItemDropRule oresRule = new OneFromRulesRule(14, oresRules);

            IItemDropRule[] hardmodeOresRules = [
               ItemDropRule.Common(ItemID.CobaltOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.PalladiumOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.MythrilOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.OrichalcumOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.AdamantiteOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TitaniumOre, 1, 20, 35),
            ];
            IItemDropRule hardmodeOresRule = new OneFromRulesRule(14, hardmodeOresRules);

            IItemDropRule[] barsRules = [
                ItemDropRule.Common(ItemID.IronBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.SilverBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.GoldBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.LeadBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.TungstenBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.PlatinumBar, 1, 6, 16),
            ];
            IItemDropRule barsRule = new OneFromRulesRule(12, barsRules);

            IItemDropRule[] hardmodeBarsRules = [
                ItemDropRule.Common(ItemID.CobaltBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.MythrilBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.AdamantiteBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.PalladiumBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.OrichalcumBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.TitaniumBar, 1, 5, 16),
            ];
            IItemDropRule hardmodeBarsRule = new OneFromRulesRule(6, hardmodeBarsRules);

            IItemDropRule[] potionsRules = [
                ItemDropRule.Common(ItemID.ObsidianSkinPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.SpelunkerPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.HunterPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.GravitationPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.MiningPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.HeartreachPotion, 1, 2, 4),
            ];
            IItemDropRule potionsRule = new OneFromRulesRule(4, potionsRules);

            IItemDropRule[] lootDropRules = [coinsRule, oresRule, hardmodeOresRule, barsRule, hardmodeBarsRule, potionsRule, .. extraRules];
            itemLoot.Add(ItemDropRule.AlwaysAtleastOneSuccess(lootDropRules));
        }

        private static void AddPotionsAndBaitToLoot(this ItemLoot itemLoot) {
            IItemDropRule[] potionsRules = [
                ItemDropRule.Common(ItemID.HealingPotion, 1, 5, 17),
                ItemDropRule.Common(ItemID.ManaPotion, 1, 5, 17),
            ];
            itemLoot.Add(new OneFromRulesRule(2, potionsRules));

            IItemDropRule[] baitRules = [
                ItemDropRule.Common(ItemID.JourneymanBait, 1, 2, 6),
                ItemDropRule.Common(ItemID.MasterBait, 1, 2, 6),
            ];
            itemLoot.Add(new OneFromRulesRule(2, baitRules));
        }
    }
}
