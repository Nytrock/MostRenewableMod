using Terraria;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems.MechQueen {
    public class MechQueenSystem : ModSystem {
        private static int _mechQueenPartsCount = -1;

        public static bool IsMechQueenAlive => _mechQueenPartsCount != -1;
        public static bool IsMechQueenDied {
            get {
                if (_mechQueenPartsCount == 0) {
                    MechQueenDisable();
                    return true;
                }
                return false;
            }
        }

        public static void OnMechQueenSpawn() {
            _mechQueenPartsCount = 4;
        }

        public static void MechQueenPartKilled() {
            _mechQueenPartsCount--;
        }

        public static void MechQueenDisable() {
            _mechQueenPartsCount = -1;
        }

        public override void PreSaveAndQuit() {
            MechQueenDisable();
        }

        public override void PostUpdateTime() {
            if (Main.IsItDay() && !Main.zenithWorld)
                MechQueenDisable();
        }
    }
}
