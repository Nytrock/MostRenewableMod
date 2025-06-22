using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace EverythingRenewableNow.Common.WorldGenerations.Skyblock {
    public class SkyblockSystem : ModSystem {
        private static bool _isSkyblock;

        private int _glitchFrameCounter;
        private int _glitchFrame;
        private int _glitchVariation;

        public override void SaveWorldHeader(TagCompound tag) {
            tag["isSkyblock"] = _isSkyblock;
        }

        public override void PreWorldGen() {
            string seedName = WorldGen.currentWorldSeed.ToLower();

            if (seedName == "parkour civilization")
                BecomeZenithSkyblock();

            if (seedName == "skyblock")
                BecomeSkyblock();
        }

        private static void BecomeSkyblock() {
            _isSkyblock = true;
            GenerateRandomSeed();
        }

        private static void BecomeZenithSkyblock() {
            _isSkyblock = true;
            WorldGen.noTrapsWorldGen = true;
            WorldGen.notTheBees = true;
            WorldGen.getGoodWorldGen = true;
            WorldGen.tenthAnniversaryWorldGen = true;
            WorldGen.dontStarveWorldGen = true;
            WorldGen.remixWorldGen = true;
            WorldGen.everythingWorldGen = true;
            Main.drunkWorld = true;
            Main.noTrapsWorld = true;
            Main.notTheBeesWorld = true;
            Main.getGoodWorld = true;
            Main.tenthAnniversaryWorld = true;
            Main.dontStarveWorld = true;
            Main.remixWorld = true;
            Main.zenithWorld = true;
            Main.drunkWorld = true;
            GenerateRandomSeed();
        }

        private static void GenerateRandomSeed() {
            int seed = Main.rand.Next();
            WorldGen.currentWorldSeed = seed.ToString();
            WorldGen._lastSeed = seed;
            WorldGen._genRand = new UnifiedRandom(seed);
            Main.rand = new UnifiedRandom(seed);
            Main.ActiveWorldFileData.SetSeed(WorldGen.currentWorldSeed);
        }

        public override void SaveWorldData(TagCompound tag) {
            tag["isSkyblock"] = _isSkyblock;
        }

        public override void LoadWorldData(TagCompound tag) {
            _isSkyblock = tag.GetBool("isSkyblock");
        }

        public override void ClearWorld() {
            _isSkyblock = false;
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
            if (_isSkyblock) {
                tasks.RemoveAll(task => task.Name is not ("Reset" or "Guide" or "Terrain" or "Spawn Point"));
                tasks.Insert(3, new RemovingTerrainPass());
                tasks.Insert(4, new SkyblockPass());
                tasks.Add(new SkyblockCleanUp());
            }
        }

        public override void OnModLoad() {
            MonoModHooks.Modify(typeof(AWorldListItem).GetMethod("GetIcon", BindingFlags.Instance | BindingFlags.NonPublic, Type.DefaultBinder, [], null), ILGetIcon);
            MonoModHooks.Modify(typeof(AWorldListItem).GetMethod("GetIconElement", BindingFlags.Instance | BindingFlags.NonPublic, Type.DefaultBinder, [], null), ILGetIconElement);
        }

        private void ILGetIcon(ILContext il) {
            try {
                ILCursor c = new(il);
                c.GotoNext(i => i.MatchLdarg0());
                c.Index--;
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);
                c.EmitDelegate(ModifyGetIcon);
            } catch {
                MonoModHooks.DumpIL(ModContent.GetInstance<EverythingRenewableNow>(), il);
            }
        }

        private void ILGetIconElement(ILContext il) {
            try {
                ILCursor c = new(il);
                for (int _ = 0; _ < 4; _++)
                    c.GotoNext(i => i.MatchLdarg0());
                c.Index--;
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);
                c.EmitDelegate(ModifyGetIconElement);
            } catch {
                MonoModHooks.DumpIL(ModContent.GetInstance<EverythingRenewableNow>(), il);
            }
        }

        private Asset<Texture2D> ModifyGetIcon(Asset<Texture2D> originalTexture, AWorldListItem list) {
            bool haveTags = list.Data.TryGetHeaderData<SkyblockSystem>(out TagCompound modTag);
            if (!haveTags) return originalTexture;

            if (modTag.GetBool("isSkyblock")) {
                string textureName = "Assets/Skyblock/Icon" + (list.Data.IsHardMode ? "Hallow" : "") + (list.Data.HasCorruption ? "Corruption" : "Crimson");
                return Mod.Assets.Request<Texture2D>(textureName, AssetRequestMode.ImmediateLoad);
            }

            return originalTexture;
        }

        private UIElement ModifyGetIconElement(UIElement originalElement, AWorldListItem list) {
            bool haveTags = list.Data.TryGetHeaderData<SkyblockSystem>(out TagCompound modTag);
            if (!haveTags) return originalElement;

            if (modTag.GetBool("isSkyblock") && list.Data.ZenithWorld) {
                Asset<Texture2D> asset = Mod.Assets.Request<Texture2D>("Assets/Skyblock/IconEverything");
                UIImageFramed uIImageFramed = new(asset, asset.Frame(7, 16)) {
                    Left = new StyleDimension(4f, 0f)
                };
                uIImageFramed.OnUpdate += UpdateGlitchAnimation;
                return uIImageFramed;
            }

            return originalElement;
        }

        private void UpdateGlitchAnimation(UIElement affectedElement) {
            int minValue = 3;
            int num = 3;
            if (_glitchFrame == 0) {
                minValue = 15;
                num = 120;
            }

            if (++_glitchFrameCounter >= Main.rand.Next(minValue, num + 1)) {
                _glitchFrameCounter = 0;
                _glitchFrame = (_glitchFrame + 1) % 16;
                if ((_glitchFrame == 4 || _glitchFrame == 8 || _glitchFrame == 12) && Main.rand.NextBool(3)) {
                    _glitchVariation = Main.rand.Next(7);
                }
            }
            (affectedElement as UIImageFramed).SetFrame(7, 16, _glitchVariation, _glitchFrame, 0, 0);
        }
    }
}
