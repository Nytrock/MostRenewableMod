using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Players {
    public class PlayerInventory : ModPlayer {
        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.Carrot);
        }
    }
}
