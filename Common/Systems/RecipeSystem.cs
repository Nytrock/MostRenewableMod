using DuckLib;
using DuckLib.Utils;
using EverythingRenewableNow.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems {
    public class RecipeSystem : ModSystem {
        public override void AddRecipes() {
            ChangeVanillaRecipes();
            AddFurnitureSetsRecipes();
            AddOtherRecipes();

            AddBoulderRecipes();
        }

        public override void PostAddRecipes() {
            AddShimmerTransmutations();

            AddBoulderShimmerTransmutations();
        }

        private static void AddOtherRecipes() {
            FurnitureUtils.AddChest(ItemID.GoldChest, ItemID.GoldBar, TileID.Anvils);
            FurnitureUtils.AddChest(ItemID.ShadowChest, ItemID.DemoniteBar, TileID.Anvils);

            Recipe
                .Create(ItemID.IvyChest)
                .AddIngredient(ItemID.RichMahoganyChest, 1)
                .AddIngredient(ItemID.Vine, 8)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe
                .Create(ItemID.WebCoveredChest)
                .AddIngredient(ItemID.Chest, 1)
                .AddIngredient(ItemID.Cobweb, 8)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe
                .Create(ItemID.OilRagSconse)
                .AddIngredient(ItemID.Candle)
                .AddIngredient(ItemID.Chain, 4)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.HangingSkeleton)
                .AddIngredient(ItemID.Chain)
                .AddIngredient(ItemID.Bone, 10)
                .AddTile(TileID.BoneWelder)
                .Register();

            Recipe
                .Create(ItemID.WallSkeleton)
                .AddIngredient(ItemID.Chain, 2)
                .AddIngredient(ItemID.Bone, 10)
                .AddTile(TileID.BoneWelder)
                .Register();

            Recipe
                .Create(ItemID.Catacomb)
                .AddIngredient(ItemID.Bone, 15)
                .AddTile(TileID.HeavyWorkBench)
                .AddCondition(Condition.InGraveyard)
                .Register();

            Recipe
                .Create(ItemID.LihzahrdPressurePlate)
                .AddRecipeGroup(RecipeGroupID.PressurePlate)
                .AddIngredient(ItemID.LihzahrdBrick, 2)
                .AddIngredient(ItemID.Wire)
                .AddTile(TileID.LihzahrdFurnace)
                .Register();

            Recipe
                .Create(ItemID.GeyserTrap)
                .AddIngredient(ItemID.VolcanoSmall, 4)
                .AddIngredient(ItemID.Hellstone)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.MechdusaSummon)
                .AddIngredient(ItemID.MechanicalEye)
                .AddIngredient(ItemID.MechanicalSkull)
                .AddIngredient(ItemID.MechanicalWorm)
                .AddCondition(Condition.NotZenithWorld)
                .AddCondition(Condition.InGraveyard)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe
                .Create(ItemID.DungeonDoor)
                .AddIngredient(ItemID.Wood, 4)
                .AddIngredient(ItemID.Glass, 2)
                .AddTile(TileID.HeavyWorkBench)
                .Register();

            Recipe
                .Create(ItemID.ScarabFishingRod)
                .AddIngredient(ItemID.FossilOre, 15)
                .AddIngredient(ItemID.Amber, 8)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.SnowGlobe)
                .AddIngredient(ItemID.IceBlock, 50)
                .AddIngredient(ItemID.Glass, 20)
                .AddRecipeGroup(DuckGroup.AnyCobaltBar, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe
                .Create(ItemID.PotSuspended)
                .AddIngredient(ItemID.ClayPot)
                .AddIngredient(ItemID.Rope, 2)
                .Register();

            Recipe
                .Create(ItemID.RedPotion)
                .AddIngredient(ItemID.RegenerationPotion)
                .AddIngredient(ItemID.SwiftnessPotion)
                .AddIngredient(ItemID.IronskinPotion)
                .AddIngredient(ItemID.ObsidianSkinPotion)
                .AddIngredient(ItemID.ManaRegenerationPotion)
                .AddIngredient(ItemID.MagicPowerPotion)
                .AddIngredient(ItemID.FeatherfallPotion)
                .AddIngredient(ItemID.SpelunkerPotion)
                .AddIngredient(ItemID.ArcheryPotion)
                .AddIngredient(ItemID.HeartreachPotion)
                .AddIngredient(ItemID.HunterPotion)
                .AddIngredient(ItemID.EndurancePotion)
                .AddIngredient(ItemID.LifeforcePotion)
                .AddIngredient(ItemID.InfernoPotion)
                .AddIngredient(ItemID.MiningPotion)
                .AddIngredient(ItemID.RagePotion)
                .AddIngredient(ItemID.WrathPotion)
                .AddIngredient(ItemID.TrapsightPotion)
                .AddTile(TileID.AlchemyTable)
                .Register();
        }

        private static void ChangeVanillaRecipes() {
            foreach (var recipe in Main.recipe) {
                int itemType = recipe.createItem.type;

                if (itemType == ItemID.ToiletObsidian) {
                    recipe.RemoveIngredient(ItemID.Obsidian);
                    recipe.RemoveIngredient(ItemID.Hellstone);
                    recipe.AddIngredient(ItemID.ObsidianBrick, 6);
                    recipe.RemoveTile(TileID.Sawmill);
                    recipe.AddTile(TileID.Hellforge);
                }

                if (itemType == ItemID.ObsidianChest) {
                    recipe.RemoveIngredient(ItemID.Obsidian);
                    recipe.RemoveIngredient(ItemID.Hellstone);
                    recipe.AddIngredient(ItemID.ObsidianBrick, 8);
                    recipe.RemoveTile(TileID.WorkBenches);
                    recipe.AddTile(TileID.Hellforge);
                }

                if (itemType == ItemID.ObsidianSink) {
                    recipe.RemoveIngredient(ItemID.Obsidian);
                    recipe.RemoveIngredient(ItemID.Hellstone);
                    recipe.AddIngredient(ItemID.ObsidianBrick, 6);
                    recipe.RemoveTile(TileID.WorkBenches);
                    recipe.AddTile(TileID.Hellforge);
                }

                if (itemType == ItemID.ArmorStatue) {
                    recipe.RemoveTile(TileID.WorkBenches);
                    recipe.AddTile(TileID.HeavyWorkBench);
                }
            }
        }

        private static void AddBoulderRecipes() {
            FurnitureUtils.AddWorkbench(ItemID.LihzahrdWorkBench, ItemID.LihzahrdBrick, TileID.LihzahrdFurnace);
            FurnitureUtils.AddChest(ItemID.WaterChest, ItemID.ShellPileBlock, TileID.Anvils);

            Recipe
                .Create(ItemID.AshBlock)
                .AddRecipeGroup(RecipeGroupID.Wood, 5)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.GoldenDelight)
                .AddIngredient(ItemID.GoldenCarp)
                .AddTile(TileID.CookingPots)
                .Register();

            Recipe
                .Create(ItemID.HoneyBucket)
                .AddIngredient(ItemID.HoneyBlock)
                .AddIngredient(ItemID.EmptyBucket)
                .AddTile(TileID.Campfire)
                .Register();

            Recipe
                .Create(ItemID.ConveyorBeltLeft)
                .AddIngredient(ItemID.ConveyorBeltRight)
                .Register();

            Recipe
                .Create(ItemID.ConveyorBeltRight)
                .AddIngredient(ItemID.ConveyorBeltLeft)
                .Register();

            Dictionary<int, int> bricksCrafts = new() {
                {ItemID.CobaltOre, ItemID.BlueBrick },
                {ItemID.MythrilOre, ItemID.GreenBrick },
                {ItemID.TitaniumOre, ItemID.GreenBrick },
                {ItemID.PalladiumOre, ItemID.PinkBrick },
                {ItemID.OrichalcumOre, ItemID.PinkBrick },
                {ItemID.AdamantiteOre, ItemID.PinkBrick }
            };
            foreach (var brickCraft in bricksCrafts) {
                Recipe
                   .Create(brickCraft.Value)
                   .AddIngredient(ItemID.StoneBlock, 5)
                   .AddIngredient(brickCraft.Key, 1)
                   .AddTile(TileID.HeavyWorkBench)
                   .AddCondition(Condition.InGraveyard)
                   .Register();
            }

        }

        private static void AddFurnitureSetsRecipes() {
            FurnitureUtils.AddSpecificFurniture(ItemID.BlueBrick,
                bathtub: ItemID.BlueDungeonBathtub,
                bed: ItemID.BlueDungeonBed,
                bookcase: ItemID.BlueDungeonBookcase,
                candelabra: ItemID.BlueDungeonCandelabra,
                candle: ItemID.BlueDungeonCandle,
                chair: ItemID.BlueDungeonChair,
                chandelier: ItemID.BlueDungeonChandelier,
                clock: ItemID.DungeonClockBlue,
                door: ItemID.BlueDungeonDoor,
                dresser: ItemID.BlueDungeonDresser,
                lamp: ItemID.BlueDungeonLamp,
                piano: ItemID.BlueDungeonPiano,
                sofa: ItemID.BlueDungeonSofa,
                table: ItemID.BlueDungeonTable,
                workbench: ItemID.BlueDungeonWorkBench,
                vase: ItemID.BlueDungeonVase
            );

            FurnitureUtils.AddSpecificFurniture(ItemID.GreenBrick,
                bathtub: ItemID.GreenDungeonBathtub,
                bed: ItemID.GreenDungeonBed,
                bookcase: ItemID.GreenDungeonBookcase,
                candelabra: ItemID.GreenDungeonCandelabra,
                candle: ItemID.GreenDungeonCandle,
                chair: ItemID.GreenDungeonChair,
                chandelier: ItemID.GreenDungeonChandelier,
                clock: ItemID.DungeonClockGreen,
                door: ItemID.GreenDungeonDoor,
                dresser: ItemID.GreenDungeonDresser,
                lamp: ItemID.GreenDungeonLamp,
                piano: ItemID.GreenDungeonPiano,
                sofa: ItemID.GreenDungeonSofa,
                table: ItemID.GreenDungeonTable,
                workbench: ItemID.GreenDungeonWorkBench,
                vase: ItemID.GreenDungeonVase
            );

            FurnitureUtils.AddSpecificFurniture(ItemID.PinkBrick,
                bathtub: ItemID.PinkDungeonBathtub,
                bed: ItemID.PinkDungeonBed,
                bookcase: ItemID.PinkDungeonBookcase,
                candelabra: ItemID.PinkDungeonCandelabra,
                candle: ItemID.PinkDungeonCandle,
                chair: ItemID.PinkDungeonChair,
                chandelier: ItemID.PinkDungeonChandelier,
                clock: ItemID.DungeonClockPink,
                door: ItemID.PinkDungeonDoor,
                dresser: ItemID.PinkDungeonDresser,
                lamp: ItemID.PinkDungeonLamp,
                piano: ItemID.PinkDungeonPiano,
                sofa: ItemID.PinkDungeonSofa,
                table: ItemID.PinkDungeonTable,
                workbench: ItemID.PinkDungeonWorkBench,
                vase: ItemID.PinkDungeonVase
            );

            FurnitureUtils.AddSpecificFurniture(ItemID.ObsidianBrick,
                craftingStation: TileID.Hellforge,
                torch: ItemID.DemonTorch,
                bathtub: ItemID.ObsidianBathtub,
                bed: ItemID.ObsidianBed,
                bookcase: ItemID.ObsidianBookcase,
                candelabra: ItemID.ObsidianCandelabra,
                candle: ItemID.ObsidianCandle,
                chair: ItemID.ObsidianChair,
                chandelier: ItemID.ObsidianChandelier,
                clock: ItemID.ObsidianClock,
                door: ItemID.ObsidianDoor,
                dresser: ItemID.ObsidianDresser,
                lamp: ItemID.ObsidianLamp,
                piano: ItemID.ObsidianPiano,
                sofa: ItemID.ObsidianSofa,
                table: ItemID.ObsidianTable,
                workbench: ItemID.ObsidianWorkBench,
                vase: ItemID.ObsidianVase
            );
        }

        private static void AddShimmerTransmutations() {
            ShimmerUtils.InsertAfter(ItemID.CobaltOre, ItemID.Hellstone);
            ShimmerUtils.AddLoop(ItemID.SlushBlock, ItemID.SiltBlock);

            ShimmerUtils.Add(ItemID.HelFire, ItemID.Cascade);
            ShimmerUtils.Add(ItemID.ZapinatorOrange, ItemID.ZapinatorGray);
            ShimmerUtils.Add(ItemID.GoldChest, ItemID.DeadMansChest);
            ShimmerUtils.Add(ItemID.Trident, ItemID.Spear);

            ShimmerUtils.Add(ItemID.JungleKey, ModContentUtils.ItemType("AwakenedJungleKey"));
            ShimmerUtils.Add(ItemID.CorruptionKey, ModContentUtils.ItemType("AwakenedCorruptionKey"));
            ShimmerUtils.Add(ItemID.CrimsonKey, ModContentUtils.ItemType("AwakenedCrimsonKey"));
            ShimmerUtils.Add(ItemID.HallowedKey, ModContentUtils.ItemType("AwakenedHallowedKey"));
            ShimmerUtils.Add(ItemID.FrozenKey, ModContentUtils.ItemType("AwakenedFrozenKey"));
            ShimmerUtils.Add(ItemID.DungeonDesertKey, ModContentUtils.ItemType("AwakenedDesertKey"));
        }

        private static void AddBoulderShimmerTransmutations() {
            ShimmerUtils.AddLoop(ItemID.GladiatorHelmet, ItemID.GladiatorBreastplate, ItemID.GladiatorLeggings);
            ShimmerUtils.AddLoopWithConditions([ItemID.RedSolution, ItemID.PurpleSolution], Condition.DownedMoonLord);
            ShimmerUtils.AddLoop(ItemID.ChainLantern, ItemID.BrassLantern, ItemID.CagedLantern, ItemID.CarriageLantern, ItemID.AlchemyLantern, ItemID.DiablostLamp, ItemID.OilRagSconse);
            ShimmerUtils.AddLoop(ItemID.ClimbingClaws, ItemID.ShoeSpikes);
            ShimmerUtils.Add(ItemID.Mug, ItemID.Ale);
            ShimmerUtils.InsertAfter(ItemID.Amethyst, ItemID.ClayBlock);
            ShimmerUtils.InsertAfter(ItemID.CopperOre, ItemID.ClayBlock);
            ShimmerUtils.AddLoop(ItemID.PutridScent, ItemID.FleshKnuckles);
            ShimmerUtils.AddLoop(ItemID.SuperDartTrap, ItemID.FlameTrap, ItemID.SpikyBallTrap, ItemID.SpearTrap);
            ShimmerUtils.AddEvent(ItemID.SlimeCrown, () => Main.StartSlimeRain(), DuckCondition.NotSlimeRain);
        }
    }
}
