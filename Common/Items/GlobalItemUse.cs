using EverythingRenewableNow.Common.Systems.MechQueen;
using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Items {
    public class GlobalItemUse : GlobalItem {
        public override void SetDefaults(Item item) {
            if (item.type == ItemID.MechdusaSummon && !Main.zenithWorld) {
                item.useStyle = ItemUseStyleID.HoldUp;
                item.consumable = true;
                item.useAnimation = 45;
                item.useTime = 45;
            }
        }

        public override bool? UseItem(Item item, Player player) {
            if (item.type == ItemID.MechdusaSummon && !Main.IsItDay() && !Main.zenithWorld) {
                if (NPCUtils.SpawnMechQueenAnywhere(player)) {
                    MechQueenSystem.OnMechQueenSpawn();
                    SoundEngine.PlaySound(SoundID.Roar, player.position);
                    return true;
                }
            }

            return base.UseItem(item, player);
        }
    }
}
