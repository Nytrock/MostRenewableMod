using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace EverythingRenewableNow.Common.WorldGenerations.Skyblock {
    internal class SkyblockCleanUpPass() : GenPass("SkyblockCleanUp", float.PositiveInfinity) {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
            progress.Set(1);
            progress.Message = LocalizationUtils.GetTextValue("WorldGen.SkyblockCleanUp");

            WorldGen.notTheBees = false;
            WorldGen.getGoodWorldGen = false;
            WorldGen.noTileActions = false;
            Main.tileSolid[659] = true;
            Main.tileSolid[GenVars.crackedType] = true;
            Main.tileSolid[484] = true;
            Main.forceXMasForToday = true;
            WorldGen.gen = false;
            Main.AnglerQuestSwap();
            WorldGen.generatingWorld = false;
            progress.Message = Lang.gen[87].Value;
        }
    }
}
