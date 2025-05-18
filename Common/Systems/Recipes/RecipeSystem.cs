using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems.Recipes {
    public class RecipeSystem : ModSystem {
        private int _jellyfishBanners, _slimeBanners, _batBanners, _anvils;
        private int _copperAxes, _copperPickaxes, _copperSwords, _woodenBows, _woodenHammers;

        public override void AddRecipeGroups() {
            RecipeGroup jellyfishBanners = new(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.JellyfishBanner)}",
                ItemID.JellyfishBanner, ItemID.PinkJellyfishBanner, ItemID.GreenJellyfishBanner, ItemID.BloodJellyBanner, ItemID.FungoFishBanner
            );
            _jellyfishBanners = RecipeGroup.RegisterGroup(nameof(ItemID.JellyfishBanner), jellyfishBanners);

            RecipeGroup slimeBanners = new(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SlimeBanner)}",
                ItemID.SlimeBanner, ItemID.GreenSlimeBanner, ItemID.PurpleSlimeBanner, ItemID.UmbrellaSlimeBanner, ItemID.RedSlimeBanner,
                ItemID.YellowSlimeBanner, ItemID.BlackSlimeBanner, ItemID.MotherSlimeBanner, ItemID.DungeonSlimeBanner, ItemID.PinkyBanner,
                ItemID.JungleSlimeBanner, ItemID.SpikedJungleSlimeBanner, ItemID.IceSlimeBanner, ItemID.SpikedIceSlimeBanner, ItemID.SandSlimeBanner,
                ItemID.LavaSlimeBanner, ItemID.ToxicSludgeBanner, ItemID.CorruptSlimeBanner, ItemID.SlimerBanner, ItemID.CrimslimeBanner,
                ItemID.GastropodBanner, ItemID.IlluminantSlimeBanner, ItemID.RainbowSlimeBanner
            );
            _slimeBanners = RecipeGroup.RegisterGroup(nameof(ItemID.SlimeBanner), slimeBanners);

            RecipeGroup batBanners = new(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.BatBanner)}",
                ItemID.BatBanner, ItemID.SporeBatBanner, ItemID.JungleBatBanner, ItemID.HellbatBanner, ItemID.IceBatBanner,
                ItemID.GiantBatBanner, ItemID.IlluminantBatBanner, ItemID.LavaBatBanner
            );
            _batBanners = RecipeGroup.RegisterGroup(nameof(ItemID.BatBanner), batBanners);

            RecipeGroup anvils = new(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.IronAnvil)}",
                ItemID.IronAnvil, ItemID.LeadAnvil
            );
            _anvils = RecipeGroup.RegisterGroup(nameof(ItemID.IronAnvil), anvils);

            RecipeGroup copperAxes = new(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CopperAxe)}",
                ItemID.CopperAxe, ItemID.TinAxe
            );
            _copperAxes = RecipeGroup.RegisterGroup(nameof(ItemID.CopperAxe), copperAxes);

            RecipeGroup copperPickaxes = new(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CopperPickaxe)}",
                ItemID.CopperPickaxe, ItemID.TinPickaxe
            );
            _copperPickaxes = RecipeGroup.RegisterGroup(nameof(ItemID.CopperPickaxe), copperPickaxes);

            RecipeGroup copperSwords = new(
               () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CopperBroadsword)}",
               ItemID.CopperBroadsword, ItemID.TinBroadsword
            );
            _copperSwords = RecipeGroup.RegisterGroup(nameof(ItemID.CopperBroadsword), copperSwords);

            RecipeGroup woodenBows = new(
               () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.WoodenBow)}",
               ItemID.WoodenBow, ItemID.BorealWoodBow, ItemID.PalmWoodBow, ItemID.RichMahoganyBow,
               ItemID.EbonwoodBow, ItemID.ShadewoodBow, ItemID.AshWoodBow, ItemID.PearlwoodBow
            );
            _woodenBows = RecipeGroup.RegisterGroup(nameof(ItemID.WoodenBow), woodenBows);

            RecipeGroup woodenHammers = new(
               () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.WoodenHammer)}",
               ItemID.WoodenHammer, ItemID.BorealWoodHammer, ItemID.PalmWoodHammer, ItemID.RichMahoganyHammer,
               ItemID.EbonwoodHammer, ItemID.ShadewoodHammer, ItemID.AshWoodHammer, ItemID.PearlwoodHammer
            );
            _woodenHammers = RecipeGroup.RegisterGroup(nameof(ItemID.WoodenHammer), woodenHammers);
        }

        public override void AddRecipes() {
            ChangeVanillaRecipes();
            AddObsidianFurnitureRecipes();
            AddDungeonFurnituresRecipes();
            AddStatuesRecipes();
            AddChestsRecipes();
            AddOtherRecipes();
        }

        public override void PostAddRecipes() {
            ChangeShimmerTransmutations();
        }

        private void AddStatuesRecipes() {
            List<StatueRecipeInfo> statuesInfo = [
                new(ItemID.ZombieBanner, ItemID.ZombieArmStatue, requireGraveyard: true),
                new(_batBanners, ItemID.BatStatue, requireGraveyard: true, itemIsGroup: true),
                new(ItemID.BloodZombieBanner, ItemID.BloodZombieStatue, requireGraveyard: true),
                new(ItemID.SkeletonBanner, ItemID.SkeletonStatue, requireGraveyard: true),
                new(ItemID.MimicBanner, ItemID.ChestStatue, requireGraveyard: true),
                new(ItemID.EaterofSoulsBanner, ItemID.CorruptStatue, requireGraveyard: true),
                new(ItemID.CrabBanner, ItemID.CrabStatue, requireGraveyard: true),
                new(ItemID.DripplerBanner, ItemID.DripplerStatue, requireGraveyard: true),
                new(ItemID.DemonEyeBanner, ItemID.EyeballStatue,requireGraveyard: true),
                new(ItemID.GoblinScoutBanner, ItemID.GoblinStatue, requireGraveyard: true),
                new(ItemID.GraniteGolemBanner, ItemID.GraniteGolemStatue,requireGraveyard: true),
                new(ItemID.HarpyBanner, ItemID.HarpyStatue, requireGraveyard: true),
                new(ItemID.GreekSkeletonBanner, ItemID.HopliteStatue, requireGraveyard: true),
                new(ItemID.HornetBanner, ItemID.HornetStatue, requireGraveyard: true),
                new(ItemID.FireImpBanner, ItemID.ImpStatue, requireGraveyard: true),
                new(_jellyfishBanners, ItemID.JellyfishStatue, requireGraveyard: true, itemIsGroup: true),
                new(ItemID.MedusaBanner, ItemID.MedusaStatue, requireGraveyard: true),
                new(ItemID.PigronBanner, ItemID.PigronStatue, requireGraveyard: true),
                new(ItemID.PiranhaBanner, ItemID.PiranhaStatue, requireGraveyard: true),
                new(ItemID.SharkBanner, ItemID.SharkStatue, requireGraveyard: true),
                new(ItemID.SkeletonBanner, ItemID.SkeletonStatue, requireGraveyard: true),
                new(_slimeBanners, ItemID.SlimeStatue, requireGraveyard: true, itemIsGroup: true),
                new(ItemID.UndeadVikingBanner, ItemID.UndeadVikingStatue, requireGraveyard: true),
                new(ItemID.UnicornBanner, ItemID.UnicornStatue, requireGraveyard: true),
                new(ItemID.SpiderBanner, ItemID.WallCreeperStatue, requireGraveyard: true),
                new(ItemID.WraithBanner, ItemID.WraithStatue, requireGraveyard: true),
                new(_anvils, ItemID.AnvilStatue, itemIsGroup: true),
                new(_copperAxes, ItemID.AxeStatue, itemIsGroup: true),
                new(_woodenBows, ItemID.BowStatue, itemIsGroup: true),
                new(_woodenHammers, ItemID.HammerStatue, itemIsGroup: true),
                new(_copperPickaxes, ItemID.PickaxeStatue, itemIsGroup: true),
                new(_copperSwords, ItemID.SwordStatue, itemIsGroup: true),
                new(ItemID.WoodenBoomerang, ItemID.BoomerangStatue),
                new(ItemID.HermesBoots, ItemID.BootStatue),
                new(ItemID.CobaltShield, ItemID.ShieldStatue),
                new(ItemID.Spear, ItemID.SpearStatue),
                new(ItemID.HealingPotion, ItemID.PotionStatue),
                new(ItemID.LifeCrystal, ItemID.HeartStatue),
                new(ItemID.ManaCrystal, ItemID.StarStatue),
                new(ItemID.Bomb, ItemID.BombStatue, itemsCount: 10),
                new(ItemID.Sunflower, ItemID.SunflowerStatue, itemsCount: 5),
                new(ItemID.ClayPot, ItemID.PotStatue, itemsCount: 5),
                new(RecipeGroupID.Wood, ItemID.TreeStatue, itemsCount: 25, itemIsGroup: true),
            ];

            foreach (var statueInfo in statuesInfo) {
                Recipe recipe = Recipe
                    .Create(statueInfo.StatueID)
                    .AddIngredient(ItemID.StoneBlock, 50)
                    .AddTile(TileID.HeavyWorkBench);

                if (statueInfo.IsItemGroup)
                    recipe.AddRecipeGroup(statueInfo.ItemID, statueInfo.ItemsCount);
                else
                    recipe.AddIngredient(statueInfo.ItemID, statueInfo.ItemsCount);

                if (statueInfo.RequireGraveyard)
                    recipe.AddCondition(Condition.InGraveyard);
                recipe.Register();
            }

            List<int> regularStatues = [ItemID.CrossStatue, ItemID.GargoyleStatue, ItemID.GloomStatue,
                ItemID.PillarStatue, ItemID.ReaperStatue, ItemID.WomanStatue];
            foreach (var statue in regularStatues) {
                Recipe
                    .Create(statue)
                    .AddIngredient(ItemID.StoneBlock, 50)
                    .AddTile(TileID.HeavyWorkBench)
                    .Register();
            }

            List<int> lihzahrdsStatues = [ItemID.LihzahrdStatue, ItemID.LihzahrdGuardianStatue, ItemID.LihzahrdWatcherStatue];
            foreach (var statue in lihzahrdsStatues) {
                Recipe
                    .Create(statue)
                    .AddIngredient(ItemID.LihzahrdBrick, 50)
                    .AddTile(TileID.LihzahrdFurnace)
                    .Register();
            }

            List<int> royaltyStatues = [ItemID.KingStatue, ItemID.QueenStatue];
            foreach (var statue in royaltyStatues) {
                Recipe
                    .Create(statue)
                    .AddIngredient(ItemID.StoneBlock, 50)
                    .AddIngredient(ItemID.TeleportationPotion)
                    .AddIngredient(ItemID.Throne)
                    .AddTile(TileID.HeavyWorkBench)
                    .Register();
            }
        }

        private static void AddChestsRecipes() {
            Recipe
                .Create(ItemID.GoldenChest)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddRecipeGroup(RecipeGroupID.IronBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.ShadowChest)
                .AddIngredient(ItemID.DemoniteBar, 8)
                .AddRecipeGroup(RecipeGroupID.IronBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.WaterChest)
                .AddIngredient(ItemID.ShellPileBlock, 8)
                .AddRecipeGroup(RecipeGroupID.IronBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

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
        }

        private static void AddOtherRecipes() {
            Recipe
                .Create(ItemID.Hellforge)
                .AddIngredient(ItemID.Furnace)
                .AddIngredient(ItemID.Hellstone, 30)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.HangingSkeleton)
                .AddIngredient(ItemID.Chain)
                .AddIngredient(ItemID.Bone, 10)
                .AddTile(TileID.HeavyWorkBench)
                .AddCondition(Condition.InGraveyard)
                .Register();

            Recipe
                .Create(ItemID.WallSkeleton)
                .AddIngredient(ItemID.Chain, 2)
                .AddIngredient(ItemID.Bone, 10)
                .AddTile(TileID.HeavyWorkBench)
                .AddCondition(Condition.InGraveyard)
                .Register();

            Recipe
                .Create(ItemID.Catacomb)
                .AddIngredient(ItemID.Bone, 15)
                .AddTile(TileID.HeavyWorkBench)
                .AddCondition(Condition.InGraveyard)
                .Register();

            Recipe
                .Create(ItemID.DartTrap)
                .AddIngredient(ItemID.PoisonDart, 250)
                .AddIngredient(ItemID.StoneBlock, 5)
                .AddTile(TileID.HeavyWorkBench)
                .Register();

            Recipe
                .Create(ItemID.LihzahrdAltar)
                .AddIngredient(ItemID.LihzahrdBrick, 50)
                .AddIngredient(ItemID.SunStone)
                .AddTile(TileID.LihzahrdFurnace)
                .Register();

            Recipe
                .Create(ItemID.FlareGun)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.Flare, 50)
                .AddTile(TileID.Anvils)
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

        private static void AddObsidianFurnitureRecipes() {
            Recipe
                .Create(ItemID.ObsidianBed)
                .AddIngredient(ItemID.ObsidianBrick, 15)
                .AddIngredient(ItemID.Silk, 5)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianChair)
                .AddIngredient(ItemID.ObsidianBrick, 4)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianWorkBench)
                .AddIngredient(ItemID.ObsidianBrick, 10)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianTable)
                .AddIngredient(ItemID.ObsidianBrick, 8)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianDoor)
                .AddIngredient(ItemID.ObsidianBrick, 6)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianBookcase)
                .AddIngredient(ItemID.ObsidianBrick, 20)
                .AddIngredient(ItemID.Book, 10)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianDresser)
                .AddIngredient(ItemID.ObsidianBrick, 16)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianPiano)
                .AddIngredient(ItemID.ObsidianBrick, 15)
                .AddIngredient(ItemID.Book, 1)
                .AddIngredient(ItemID.Bone, 4)
                .AddDecraftCondition(Condition.DownedSkeletron)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianClock)
                .AddIngredient(ItemID.ObsidianBrick, 10)
                .AddIngredient(ItemID.Glass, 6)
                .AddRecipeGroup(RecipeGroupID.IronBar, 3)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianChandelier)
                .AddIngredient(ItemID.ObsidianBrick, 4)
                .AddIngredient(ItemID.DemonTorch, 4)
                .AddIngredient(ItemID.Chain, 1)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianCandelabra)
                .AddIngredient(ItemID.ObsidianBrick, 5)
                .AddIngredient(ItemID.DemonTorch, 3)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianCandle)
                .AddIngredient(ItemID.ObsidianBrick, 4)
                .AddIngredient(ItemID.DemonTorch, 1)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianLantern)
                .AddIngredient(ItemID.ObsidianBrick, 6)
                .AddIngredient(ItemID.DemonTorch, 1)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianLamp)
                .AddIngredient(ItemID.ObsidianBrick, 3)
                .AddIngredient(ItemID.DemonTorch, 1)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianSofa)
                .AddIngredient(ItemID.ObsidianBrick, 5)
                .AddIngredient(ItemID.Silk, 2)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianBathtub)
                .AddIngredient(ItemID.ObsidianBrick, 14)
                .AddTile(TileID.Hellforge)
                .Register();

            Recipe
                .Create(ItemID.ObsidianVase)
                .AddIngredient(ItemID.ObsidianBrick, 9)
                .AddTile(TileID.Hellforge)
                .Register();
        }

        private static void AddDungeonFurnituresRecipes() {
            AddDungeonBrickFurnitureRecipes(ItemID.BlueBrick, ItemID.BlueDungeonBathtub, ItemID.BlueDungeonBed, ItemID.BlueDungeonBookcase, ItemID.BlueDungeonCandelabra,
                ItemID.BlueDungeonCandle, ItemID.BlueDungeonChair, ItemID.BlueDungeonChandelier, ItemID.DungeonClockBlue, ItemID.BlueDungeonDoor,
                ItemID.BlueDungeonDresser, ItemID.BlueDungeonLamp, ItemID.BlueDungeonPiano, ItemID.BlueDungeonSofa, ItemID.BlueDungeonTable,
                ItemID.BlueDungeonWorkBench, ItemID.BlueDungeonVase);

            AddDungeonBrickFurnitureRecipes(ItemID.GreenBrick, ItemID.GreenDungeonBathtub, ItemID.GreenDungeonBed, ItemID.GreenDungeonBookcase, ItemID.GreenDungeonCandelabra,
                ItemID.GreenDungeonCandle, ItemID.GreenDungeonChair, ItemID.GreenDungeonChandelier, ItemID.DungeonClockGreen, ItemID.GreenDungeonDoor,
                ItemID.GreenDungeonDresser, ItemID.GreenDungeonLamp, ItemID.GreenDungeonPiano, ItemID.GreenDungeonSofa, ItemID.GreenDungeonTable,
                ItemID.GreenDungeonWorkBench, ItemID.GreenDungeonVase);

            AddDungeonBrickFurnitureRecipes(ItemID.PinkBrick, ItemID.PinkDungeonBathtub, ItemID.PinkDungeonBed, ItemID.PinkDungeonBookcase, ItemID.PinkDungeonCandelabra,
                ItemID.PinkDungeonCandle, ItemID.PinkDungeonChair, ItemID.PinkDungeonChandelier, ItemID.DungeonClockPink, ItemID.PinkDungeonDoor,
                ItemID.PinkDungeonDresser, ItemID.PinkDungeonLamp, ItemID.PinkDungeonPiano, ItemID.PinkDungeonSofa, ItemID.PinkDungeonTable,
                ItemID.PinkDungeonWorkBench, ItemID.PinkDungeonVase);

            Recipe
                .Create(ItemID.OilRagSconse)
                .AddIngredient(ItemID.Candle)
                .AddIngredient(ItemID.Chain, 4)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.ChainLantern)
                .AddIngredient(ItemID.Torch)
                .AddIngredient(ItemID.Chain, 4)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.BrassLantern)
                .AddIngredient(ItemID.Torch)
                .AddIngredient(ItemID.CopperBar, 4)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.CagedLantern)
                .AddIngredient(ItemID.Torch)
                .AddRecipeGroup(RecipeGroupID.IronBar, 4)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.CarriageLantern)
                .AddIngredient(ItemID.Torch)
                .AddRecipeGroup(RecipeGroupID.IronBar, 4)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.AlchemyLantern)
                .AddIngredient(ItemID.Bottle)
                .AddIngredient(ItemID.Chain, 4)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(ItemID.DiablostLamp)
                .AddIngredient(ItemID.Bottle)
                .AddIngredient(ItemID.Lavafly)
                .Register();
        }

        private static void AddDungeonBrickFurnitureRecipes(int block, int bathtub, int bed, int bookcase, int candelabra, int candle, int chair, int chandelier, int clock, int door, int dresser, int lamp, int piano, int sofa, int table, int workbench, int vase) {
            Recipe
                .Create(bed)
                .AddIngredient(block, 15)
                .AddIngredient(ItemID.Silk, 5)
                .AddTile(TileID.Sawmill)
                .Register();

            Recipe
                .Create(chair)
                .AddIngredient(block, 4)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe
                .Create(workbench)
                .AddIngredient(block, 10)
                .Register();

            Recipe
                .Create(table)
                .AddIngredient(block, 8)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe
                .Create(door)
                .AddIngredient(block, 6)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe
                .Create(bookcase)
                .AddIngredient(block, 20)
                .AddIngredient(ItemID.Book, 10)
                .AddTile(TileID.Sawmill)
                .Register();

            Recipe
                .Create(dresser)
                .AddIngredient(block, 16)
                .AddTile(TileID.Sawmill)
                .Register();

            Recipe
                .Create(piano)
                .AddIngredient(block, 15)
                .AddIngredient(ItemID.Book, 1)
                .AddIngredient(ItemID.Bone, 4)
                .AddDecraftCondition(Condition.DownedSkeletron)
                .AddTile(TileID.Sawmill)
                .Register();

            Recipe
                .Create(clock)
                .AddIngredient(block, 10)
                .AddIngredient(ItemID.Glass, 6)
                .AddRecipeGroup(RecipeGroupID.IronBar, 3)
                .AddTile(TileID.Sawmill)
                .Register();

            Recipe
                .Create(chandelier)
                .AddIngredient(block, 4)
                .AddIngredient(ItemID.BoneTorch, 4)
                .AddIngredient(ItemID.Chain, 1)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe
                .Create(candelabra)
                .AddIngredient(block, 5)
                .AddIngredient(ItemID.BoneTorch, 3)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe
                .Create(candle)
                .AddIngredient(block, 4)
                .AddIngredient(ItemID.BoneTorch, 1)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe
                .Create(lamp)
                .AddIngredient(block, 3)
                .AddIngredient(ItemID.BoneTorch, 1)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe
                .Create(sofa)
                .AddIngredient(block, 5)
                .AddIngredient(ItemID.Silk, 2)
                .AddTile(TileID.Sawmill)
                .Register();

            Recipe
                .Create(bathtub)
                .AddIngredient(block, 14)
                .AddTile(TileID.Sawmill)
                .Register();

            Recipe
                .Create(vase)
                .AddIngredient(block, 9)
                .AddTile(TileID.Sawmill)
                .Register();
        }

        private static void ChangeShimmerTransmutations() {
            int[] transmutations = ItemID.Sets.ShimmerTransformToItem;
            transmutations[ItemID.AshWood] = ItemID.AshBlock;
            transmutations[ItemID.CobaltOre] = ItemID.Hellstone;
            transmutations[ItemID.Hellstone] = ItemID.PlatinumOre;
        }
    }
}
