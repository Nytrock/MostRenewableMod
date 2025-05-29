using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Items.PaintingsBags {
    public class DungeonPaintingsBag : PaintingBag {
        protected override IItemDropRule GeneratePaintingsDropRule() {
            IItemDropRule dropRule = ItemDropRule.OneFromOptions(1,
                ItemID.BloodMoonRising, ItemID.TheHangedMan, ItemID.GloryoftheFire, ItemID.BoneWarp, ItemID.SkellingtonJSkellingsworth,
                ItemID.TheCursedMan, ItemID.TheEyeSeestheEnd, ItemID.SomethingEvilisWatchingYou, ItemID.TheTwinsHaveAwoken, ItemID.TheScreamer,
                ItemID.GoblinsPlayingPoker, ItemID.Dryadisque, ItemID.Impact, ItemID.PoweredbyBirds, ItemID.TheDestroyer, ItemID.ThePersistencyofEyes,
                ItemID.UnicornCrossingtheHallows, ItemID.GreatWave, ItemID.StarryNight, ItemID.TheGuardiansGaze, ItemID.FacingtheCerebralMastermind,
                ItemID.TrioSuperHeroes, ItemID.TheCreationoftheGuide, ItemID.SparkyPainting, ItemID.RemnantsofDevotion
            );
            return dropRule;
        }
    }
}
