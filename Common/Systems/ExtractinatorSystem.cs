using DuckLib.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems {
    public class ExtractinatorSystem : ModSystem {
        private readonly static int[] _pooSeeds = [ItemID.GrassSeeds, ItemID.JungleGrassSeeds, ItemID.MushroomGrassSeeds];

        public override void PostSetupContent() {
            ExtractinatorUtils.AddDirectConversion(ItemID.ShellPileBlock, ItemID.SandBlock);
            ExtractinatorUtils.AddItemToConversionGroup(ItemID.ShellPileBlock, ItemID.Obsidian);
            ExtractinatorUtils.AddDirectConversion(ItemID.Hive, ItemID.HoneyBlock);
            ExtractinatorUtils.AddConversion(ItemID.PoopBlock, PooConversion);

            if (CrossModSystem.Calamity != null) {
                ExtractinatorUtils.AddConversion(CrossModSystem.Calamity.Find<ModItem>("PowerCell").Type, PowerCellConversion);
                ExtractinatorUtils.AddConversion(CrossModSystem.Calamity.Find<ModItem>("BrimstoneSlag").Type, BrimstoneConversion);
            }
        }

        private Item PooConversion(bool _) {
            Item result = new(ItemID.DirtBlock);
            if (Main.rand.NextBool(98, 100))
                return result;

            result.type = _pooSeeds[Main.rand.Next(_pooSeeds.Length)];
            return result;
        }

        private Item PowerCellConversion(bool isChlorophyte) {
            float dropRand = Main.rand.NextFloat();
            Item result = new(ItemID.CopperCoin);

            if (dropRand < 0.03f)
                result.type = CrossModSystem.Calamity.Find<ModItem>("MysteriousCircuitry").Type;
            else if (dropRand < 0.06f)
                result.type = CrossModSystem.Calamity.Find<ModItem>("DubiousPlating").Type;
            else if (dropRand < 0.11f)
                result.type = ItemID.SilverCoin;
            else if (dropRand < 0.115f)
                result.type = CrossModSystem.Calamity.Find<ModItem>("SuspiciousScrap").Type;
            else
                result.stack = Main.rand.Next(25, 100);

            return result;
        }

        private Item BrimstoneConversion(bool isChlorophyte) {
            Item result = new(ItemID.None);
            if (Main.rand.NextBool(10)) {
                result.type = CrossModSystem.Calamity.Find<ModItem>("SpineSapling").Type;
                result.stack = 1;
            }
            return result;
        }
    }
}
