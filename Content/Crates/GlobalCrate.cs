using DuckLib;
using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Crates {
    internal class GlobalCrate : GlobalItem {
        public override void RightClick(Item item, Player player) {
            if (!ModContent.GetInstance<CavernCrate>().IsCrate(item.type))
                return;

            if (!Main.rand.NextBool(100))
                return;

            PlayerDeathReason deathReason = PlayerDeathReason.ByCustomReason(LocalizationUtils.GetNetworkText("DeathMessages.DeadManCrate", player.name));
            IEntitySource source = player.GetSource_OnHurt(deathReason);

            DuckEffect.CreateExplosion(source, player.Center, 200, 200);
            player.QuickSpawnItem(source, ItemID.DeadMansSweater);
            player.Hurt(deathReason, Main.hardMode ? 350 : 200, 1);
        }
    }
}
