using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace EverythingRenewableNow.Content.Tiles {
    public class Carrot : ModTile {
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = [18];
            TileObjectData.addTile(Type);

            DustType = DustID.Grass;
            HitSound = SoundID.Grass;

            AddMapEntry(new Color(27, 197, 109));
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j) {
            return [new Item(ItemID.Carrot)];
        }
    }
}
