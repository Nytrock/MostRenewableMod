using EverythingRenewableNow.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.FishingCrates {
    public class CavernCrateHardmode : BaseCrate {
        protected override string _crateName => nameof(CavernCrateHardmode);

        public override void SetStaticDefaults() {
            base.SetStaticDefaults();
            ItemID.Sets.IsFishingCrateHardmode[Type] = true;
        }

        public override void SetDefaults() {
            base.SetDefaults();
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<CavernCrate>();
        }

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.AddHardmodeFishingCrateLoot(LootUtils.GetCavernCrateRules());
        }

        public override void RightClick(Player player) {
            if (!Main.rand.NextBool(100))
                return;

            PlayerDeathReason deathReason = PlayerDeathReason.ByCustomReason(LocalizationUtils.GetNetworkText("DeathMessages.DeadManCrate", player.name));
            IEntitySource source = player.GetSource_OnHurt(deathReason);

            GoreAndDustUtils.CreateExplosion(source, player.Center, 200, 200);
            player.QuickSpawnItem(source, ItemID.DeadMansSweater);
            player.Hurt(deathReason, 350, 1);
        }
    }
}
