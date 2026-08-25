using DuckLib.Extensions;
using EverythingRenewableNow.Content.Crates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Players {
    public class PlayerFishing : ModPlayer {
        private readonly int[] _fishingPotions = [ItemID.SonarPotion, ItemID.FishingPotion, ItemID.CratePotion];
        private readonly int[] _anglerSet = [ItemID.AnglerHat, ItemID.AnglerVest, ItemID.AnglerPants];

        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems) {
            Item rewardItem = FindPotion(rewardItems);
            if (rewardItem == null)
                return;

            if (Player.anglerQuestsFinished > 20) {
                if (Main.rand.NextBool((int)(100f * rareMultiplier))) {
                    rewardItem.stack = 1;
                    rewardItem.SetDefaults(_anglerSet[Main.rand.Next(3)]);
                }
            }

            if (Player.anglerQuestsFinished > 5) {
                if (Main.rand.NextBool((int)(70f * rareMultiplier))) {
                    rewardItem.stack = 1;
                    rewardItem.SetDefaults(ItemID.FuzzyCarrot);
                }
            }
        }

        private Item FindPotion(List<Item> rewardItems) {
            foreach (var item in rewardItems)
                if (_fishingPotions.Contains(item.type))
                    return item;
            return null;
        }

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition) {
            bool inWater = !attempt.inLava && !attempt.inHoney;
            bool isBiomeCrateConditions = inWater && attempt.crate && !attempt.veryrare && !attempt.legendary && attempt.rare;

            if (itemDrop.IsType(ItemID.JungleFishingCrate, ItemID.JungleFishingCrateHard) && Player.ZoneLihzhardTemple) {
                itemDrop = ModContent.GetInstance<TempleCrate>().CurrentCrateType;
                return;
            }

            if (!isBiomeCrateConditions) {
                if (Player.ZoneLihzhardTemple && inWater && !attempt.crate && (attempt.veryrare || attempt.legendary) && Main.rand.NextBool(10))
                    itemDrop = ItemID.LizardKing;
                return;
            }

            if (Player.ZoneLihzhardTemple)
                itemDrop = ModContent.GetInstance<TempleCrate>().CurrentCrateType;
            else if (Player.ZoneGlowshroom)
                itemDrop = ModContent.GetInstance<MushroomCrate>().CurrentCrateType;
            else if (Player.ZoneNormalCaverns)
                itemDrop = ModContent.GetInstance<CavernCrate>().CurrentCrateType;
            else if (Player.ZoneForest)
                itemDrop = ModContent.GetInstance<ForestCrate>().CurrentCrateType;
        }
    }
}
