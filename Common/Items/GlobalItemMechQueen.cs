using EverythingRenewableNow.Common.Systems.MechQueen;
using EverythingRenewableNow.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Items {
    public class GlobalItemMechQueen : GlobalItem {
        private static LocalizedText _mechQueenTooltip;

        public override void SetStaticDefaults() {
            _mechQueenTooltip = LocalizationUtils.GetText("Items.MechQueen.Tooltip");
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (item.type == ItemID.MechdusaSummon && !Main.zenithWorld) {
                tooltips.ForEach(tooltip => {
                    if (tooltip.Name == "Tooltip0")
                        tooltip.Hide();
                });
                TooltipLine line = new(Mod, "MechQueenInRegularWorldTooltip", _mechQueenTooltip.Value);
                tooltips.Add(line);
            }
        }

        public override void SetDefaults(Item item) {
            if (item.type == ItemID.MechdusaSummon && !Main.zenithWorld) {
                item.useStyle = ItemUseStyleID.HoldUp;
                item.consumable = true;
                item.useAnimation = 45;
                item.useTime = 45;
            }
        }

        public override bool CanUseItem(Item item, Player player) {
            if (item.type == ItemID.MechdusaSummon) {
                if (NPC.AnyNPCs(127) || NPC.AnyNPCs(134) || NPC.AnyNPCs(125) || NPC.AnyNPCs(126))
                    return false;
                return Main.zenithWorld || (!Main.IsItDay() && !Main.zenithWorld);
            }

            return base.CanUseItem(item, player);
        }

        public override bool? UseItem(Item item, Player player) {
            if (item.type == ItemID.MechdusaSummon) {
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
