using EverythingRenewableNow.Content.Items.Boulder;
using EverythingRenewableNow.Content.Tiles;
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

                if (WorldGen.PlaceTile(x, y - 1, TileID.GlowTulip, mute: true)) {
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, x, y);
                }
            } else if (TileUtils.IsFitForCarrot(x, y, type)) {
                if (!Main.rand.NextBool(15000))
                    return;

                if (WorldGen.PlaceTile(x, y - 1, ModContent.TileType<Carrot>(), mute: true)) {
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, x, y);
                }
            }
        }

        // Boulder
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem) {
            if (type != TileID.Plants2)
                return;

            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.TileFrameX >= 108)
                return;

            if (WorldGen.genRand.NextBool(1000))
                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<LuckyClover>());
        }
    }
}
