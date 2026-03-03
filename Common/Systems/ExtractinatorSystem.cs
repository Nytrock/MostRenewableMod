using DuckLib.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems {
    // BOULDER
    public class ExtractinatorSystem : ModSystem {
        private readonly static int[] _pooSeeds = [ItemID.GrassSeeds, ItemID.JungleGrassSeeds, ItemID.MushroomGrassSeeds];

        public override void Load() {
            ExtractinatorUtils.AddDirectConversion(ItemID.ShellPileBlock, ItemID.SandBlock);
            ExtractinatorUtils.AddItemToConversionGroup(ItemID.ShellPileBlock, ItemID.Obsidian);
            ExtractinatorUtils.AddDirectConversion(ItemID.Hive, ItemID.HoneyBlock);

            ExtractinatorUtils.AddConversion(ItemID.PoopBlock, PooConversion);
        }

        private Item PooConversion(bool _) {
            Item result = new(ItemID.DirtBlock);
            if (Main.rand.NextBool(98, 100))
                return result;

            result.type = _pooSeeds[Main.rand.Next(_pooSeeds.Length)];
            return result;
        }
    }
}
