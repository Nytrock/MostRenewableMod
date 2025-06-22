using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace EverythingRenewableNow.Common.WorldGenerations.Skyblock {
    public class SkyblockPass() : GenPass("Skyblock", float.PositiveInfinity) {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
            progress.Set(1);
            progress.Message = LocalizationUtils.GetTextValue("WorldGen.Skyblock");

            int x = Main.spawnTileX - 3;
            int y = Main.spawnTileY + 1;
            for (int i = 0; i < 5; i++)
                WorldGen.PlaceTile(x + i, y, TileID.IceBrick);

            int isCrimson = WorldGen.crimson ? 1 : 0;
            WorldGen.Place3x2(Main.spawnTileX - 2, Main.spawnTileY, TileID.DemonAltar, isCrimson);
            WorldGen.AddShadowOrb(Main.spawnTileX, Main.spawnTileY + 3);

            WorldGen.PlaceChest(Main.spawnTileX, Main.spawnTileY);
            int chestIndex = Chest.FindChest(Main.spawnTileX, Main.spawnTileY - 1);
            Chest chest = Main.chest[chestIndex];

            chest.item[0] = new Item(ItemID.BottomlessShimmerBucket);
            chest.item[1] = new Item(ItemID.LihzahrdWallUnsafe);
            chest.item[2] = new Item(ItemID.LihzahrdAltar);
            chest.item[3] = new Item(ItemID.PinkBrick, Main.GameModeInfo.IsJourneyMode ? 100 : 256);
            chest.item[4] = new Item(ItemID.RainbowMoss);

            int chestItemIndex = 5;
            if (!(Main.getGoodWorld && Main.remixWorld))
                chest.item[chestItemIndex++] = new Item(ItemID.LihzahrdBrick);

            if (!Main.zenithWorld && !Main.tenthAnniversaryWorld) {
                chest.item[chestItemIndex++] = new Item(ItemID.Granite);
                chest.item[chestItemIndex++] = new Item(ItemID.AshBlock);
            }

            if (!Main.GameModeInfo.IsExpertMode && !Main.GameModeInfo.IsMasterMode && !Main.GameModeInfo.IsJourneyMode)
                chest.item[chestItemIndex++] = new Item(ItemID.WaterBucket);
        }
    }
}
