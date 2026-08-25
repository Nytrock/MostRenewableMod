using EverythingRenewableNow.Common.Systems;
using Terraria;

namespace EverythingRenewableNow.Utils {
    public static class PlayerUtils {
        public static bool InCalamityZone(this Player player, string zoneName) {
            if (CrossModSystem.Calamity == null)
                return false;
            return (bool)CrossModSystem.Calamity.Call("GetInZone", player, zoneName);
        }

        public static bool CheckPocketsForItem(this Player player, int itemID) {
            Item[][] containers = [player.inventory, player.bank.item, player.bank2.item, player.bank3.item, player.bank4.item];

            for (int i = 0; i < containers.Length; i++)
                for (int j = 0; j < containers[i].Length; j++)
                    if (containers[i][j].type == itemID)
                        return true;
            return false;
        }
    }
}
