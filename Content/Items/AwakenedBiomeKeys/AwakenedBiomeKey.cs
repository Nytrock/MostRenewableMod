using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.AwakenedBiomeKeys {
    public class AwakenedBiomeKey(int width, int heigth, string biome) : ModItem {
        private static LocalizedText _planteraCondition;

        public override string Name => $"Awakened{biome}Key";
        protected override bool CloneNewInstances => true;

        public override void SetStaticDefaults() {
            _planteraCondition = Language.GetText("LegacyTooltip.59");
        }

        public override void SetDefaults() {
            Item.width = width;
            Item.height = heigth;
            Item.maxStack = 9999;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            if (NPC.downedPlantBoss)
                return;

            TooltipLine planteraLine = new(Mod, "KeyLocked", _planteraCondition.Value);
            tooltips.Insert(2, planteraLine);
        }
    }
}
