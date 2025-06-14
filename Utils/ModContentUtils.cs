using Terraria.ModLoader;

namespace EverythingRenewableNow.Utils {
    public static class ModContentUtils {
        public static int ItemType(string fullname) {
            return ModContent.Find<ModItem>("EverythingRenewableNow", fullname).Type;
        }

        public static int NPCType(string fullname) {
            return ModContent.Find<ModNPC>("EverythingRenewableNow", fullname).Type;
        }

        public static int TileType(string fullname) {
            return ModContent.Find<ModTile>("EverythingRenewableNow", fullname).Type;
        }
    }
}
