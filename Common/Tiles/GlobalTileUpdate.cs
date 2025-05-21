using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Tiles {
    public class GlobalTileUpdate : GlobalTile {
        public override void RandomUpdate(int x, int y, int type) {
            if (TileUtils.IsFitForGlowTulip(x, y, type)) {
                if (!Main.rand.NextBool(7500))
                    return;

                if (WorldGen.PlaceTile(x, y - 1, TileID.GlowTulip)) {
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, x, y);
                }
            }
        }
    }
}
