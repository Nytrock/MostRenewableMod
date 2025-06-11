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

            if (itemDrop == ItemID.JungleFishingCrate && Player.ZoneLihzhardTemple) {
                itemDrop = ModContent.ItemType<TempleCrate>();
                return;
            }

            if (itemDrop == ItemID.JungleFishingCrateHard && Player.ZoneLihzhardTemple) {
                itemDrop = ModContent.ItemType<TempleCrateHardmode>();
                return;
            }

            if (!isBiomeCrateConditions) {
                if (Player.ZoneLihzhardTemple && inWater && !attempt.crate && (attempt.veryrare || attempt.legendary) && Main.rand.NextBool(10))
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
