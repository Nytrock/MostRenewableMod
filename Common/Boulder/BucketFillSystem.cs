using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Boulder {
    public class BucketFillSystem : GlobalItem {
        public override void PostUpdate(Item item) {
            if (item.beingGrabbed)
                return;

            if (item.type != ItemID.EmptyBucket || !Main.raining)
                return;

            if (item.playerIndexTheItemIsReservedFor == Main.myPlayer && (Main.worldSurface > 50.0 || Main.remixWorld) && IsSurfaceForAtmospherics(item.position.ToTileCoordinates())) {
                int x = (int)item.Center.X / 16;
                int y = (int)item.Center.Y / 16;
                if (WorldGen.InWorld(x, y) && WallID.Sets.AllowsWind[Main.tile[x, y].WallType]) {
                    int chance = 600;
                    if (Main.dayRate > 0 && Main.dayRate < chance) {
                        chance /= (int)Main.dayRate;
                    }

                    if (Main.rand.Next(chance) == 0 && Main.rand.NextFloat() < Main.maxRaining) {
                        int num4 = item.stack;
                        item.SetDefaults(ItemID.WaterBucket);
                        item.playerIndexTheItemIsReservedFor = Main.myPlayer;
                        item.stack = num4;
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item.netID);
                    }
                }
            }
        }

        private static bool IsSurfaceForAtmospherics(Point tileCoords) {
            if (Main.remixWorld) {
                if (tileCoords.Y > Main.rockLayer) {
                    return tileCoords.Y < Main.maxTilesY - 350;
                }
                return false;
            }
            return tileCoords.Y <= Main.worldSurface;
        }
    }
}
