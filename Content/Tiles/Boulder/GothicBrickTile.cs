using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Tiles.Boulder {
    public class GothicBrickTile : ModTile {
        public override void SetStaticDefaults() {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.Bone;
            AddMapEntry(new Color(65, 75, 65));
        }
    }
}
