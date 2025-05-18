using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class DungeonPaintingsBag : ModItem {
        public override void SetDefaults() {
            Item.width = 16;
            Item.height = 16;
            Item.ResearchUnlockCount = 25;
            Item.shopCustomPrice = Terraria.Item.buyPrice(silver: 60);
            Item.value = Terraria.Item.buyPrice(silver: 30);
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.White;
        }

        public override bool CanRightClick() {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            IItemDropRule dropRule = ItemDropRule.OneFromOptions(1,
                ItemID.BloodMoonRising, ItemID.TheHangedMan, ItemID.GloryoftheFire, ItemID.BoneWarp, ItemID.SkellingtonJSkellingsworth,
                ItemID.TheCursedMan, ItemID.TheEyeSeestheEnd, ItemID.SomethingEvilisWatchingYou, ItemID.TheTwinsHaveAwoken, ItemID.TheScreamer,
                ItemID.GoblinsPlayingPoker, ItemID.Dryadisque, ItemID.Impact, ItemID.PoweredbyBirds, ItemID.TheDestroyer, ItemID.ThePersistencyofEyes,
                ItemID.UnicornCrossingtheHallows, ItemID.GreatWave, ItemID.StarryNight, ItemID.TheGuardiansGaze, ItemID.FacingtheCerebralMastermind,
                ItemID.TrioSuperHeroes, ItemID.TheCreationoftheGuide, ItemID.SparkyPainting, ItemID.RemnantsofDevotion
            );
            itemLoot.Add(dropRule);
        }
    }
}
