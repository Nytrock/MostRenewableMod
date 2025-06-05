using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace EverythingRenewableNow.Content.Tiles.FishingCrates {
    public abstract class BaseCrate : ModTile {
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileTable[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(160, 120, 92));
        }

        public override bool CreateDust(int i, int j, ref int type) {
            return false;
        }
    }
}
