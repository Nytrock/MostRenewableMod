using EverythingRenewableNow.Content.Items.FishingCrates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Players {
    public class PlayerFishing : ModPlayer {
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition) {
            bool inWater = !attempt.inLava && !attempt.inHoney;
            bool isBiomeCrateConditions = inWater && attempt.crate && !attempt.veryrare && !attempt.legendary && attempt.rare;

            if (!isBiomeCrateConditions) {
                if (Player.ZoneLihzhardTemple && inWater && !attempt.crate && (attempt.veryrare || attempt.legendary))
                    itemDrop = ItemID.LizardKing;
                return;
            }

            if (Player.ZoneLihzhardTemple) {
                itemDrop = Main.hardMode ? ModContent.ItemType<TempleCrateHardmode>() : ModContent.ItemType<TempleCrate>();
                return;
            }

            if (Player.ZoneGlowshroom) {
                itemDrop = Main.hardMode ? ModContent.ItemType<MushroomCrateHardmode>() : ModContent.ItemType<MushroomCrate>();
                return;
            }

            if (Player.ZoneNormalCaverns) {
                itemDrop = Main.hardMode ? ModContent.ItemType<CavernCrateHardmode>() : ModContent.ItemType<CavernCrate>();
                return;
            }

            if (Player.ZoneForest) {
                itemDrop = Main.hardMode ? ModContent.ItemType<ForestCrateHardmode>() : ModContent.ItemType<ForestCrate>();
                return;
            }
        }
    }
}
