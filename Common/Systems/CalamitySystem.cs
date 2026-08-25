using EverythingRenewableNow.Utils;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems {
    public class CalamitySystem : ModSystem {
        private static Dictionary<string, int> _draedonItems;
        private static Dictionary<int, bool> _doesPlayerHaveDraedonItems;
        private static int _timeFromLastPlayerCheck;
        private const int PLAYER_CHECK_INTERVAL = 60 * 60;

        public override void OnModLoad() {
            ILStuff();
        }

        public override void OnWorldLoad() {
            _timeFromLastPlayerCheck = 0;
            CheckPlayerInventory();
        }

        public override void PostSetupContent() {
            AddDraedonDialogues();
        }

        [JITWhenModsEnabled("CalamityMod")]
        private void ILStuff() {
            Type codebreakerUIType = typeof(CalamityMod.UI.DraedonSummoning.CodebreakerUI);
            MonoModHooks.Modify(codebreakerUIType.GetMethod("HandleCellSlotInteractions"), ILUnzenithContent);
            MonoModHooks.Modify(codebreakerUIType.GetMethod("HandleDraedonSummonButton"), ILUnzenithContent);
            MonoModHooks.Modify(typeof(CalamityMod.NPCs.ExoMechs.Draedon).GetMethod("AI"), ILUnzenithContent);

            Type SCalAltarType = typeof(CalamityMod.Tiles.Furniture.CraftingStations.SCalAltar);
            MonoModHooks.Modify(SCalAltarType.GetMethod("HoverItemIcon"), ILUnzenithContent);
            MonoModHooks.Modify(SCalAltarType.GetMethod("AttemptToSummonSCal"), ILUnzenithContent);
            MonoModHooks.Modify(typeof(CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas).GetMethod("AI"), ILUnzenithContent);

            MonoModHooks.Modify(typeof(CalamityMod.Tiles.CalamityGlobalTile).GetMethod("KillTile"), ILMiningSetChanges);
        }

        private void ILUnzenithContent(ILContext il) {
            try {
                ILCursor c = new(il);
                while (c.TryGotoNext(i => i.MatchLdsfld(out var field) && field.Name == "zenithWorld")) {
                    c.Next.OpCode = OpCodes.Ldc_I4_1;
                    c.Next.Operand = null;
                }
            } catch {
                MonoModHooks.DumpIL(ModContent.GetInstance<EverythingRenewableNow>(), il);
            }
        }

        private void ILMiningSetChanges(ILContext il) {
            int duplicationDenominator = 20;

            try {
                ILCursor c = new(il);
                c.TryGotoNext(i => i.MatchLdfld(out var field) && field.Name == "miningSetCooldown");
                c.TryGotoNext(i => i.OpCode == OpCodes.Ldc_I4_4);
                c.Next.OpCode = OpCodes.Ldc_I4;
                c.Next.Operand = duplicationDenominator;

                c.TryGotoNext(i => i.MatchStfld(out var field) && field.Name == "miningSetCooldown");
                c.TryGotoPrev(i => i.OpCode == OpCodes.Ldc_I4);
                c.Next.OpCode = OpCodes.Ldc_I4_0;
                c.TryGotoPrev(i => i.OpCode == OpCodes.Ldc_I4);
                c.Next.OpCode = OpCodes.Ldc_I4_0;
            } catch {
                MonoModHooks.DumpIL(ModContent.GetInstance<EverythingRenewableNow>(), il);
            }
        }

        public override void PostUpdateEverything() {
            _timeFromLastPlayerCheck++;
            if (_timeFromLastPlayerCheck >= PLAYER_CHECK_INTERVAL) {
                CheckPlayerInventory();
                _timeFromLastPlayerCheck = 0;
            }

            CheckDraedonDialogue();
        }

        [JITWhenModsEnabled("CalamityMod")]
        private static void CheckDraedonDialogue() {
            string fullText = CalamityMod.UI.DraedonSummoning.CodebreakerUI.FullDraedonText;
            if (string.IsNullOrEmpty(fullText) || fullText != CalamityMod.UI.DraedonSummoning.CodebreakerUI.WrittenDraedonText || CalamityMod.UI.DraedonSummoning.CodebreakerUI.DraedonTextCreationTimer != 0)
                return;

            int item = _draedonItems.GetValueOrDefault(fullText, ItemID.None);
            if (item == ItemID.None)
                return;

            _doesPlayerHaveDraedonItems[item] = true;
            Item.NewItem(Main.LocalPlayer.GetSource_FromThis(), Main.LocalPlayer.Center, item);
        }

        private static void CheckPlayerInventory() {
            foreach (var draedonItem in _doesPlayerHaveDraedonItems.Keys)
                _doesPlayerHaveDraedonItems[draedonItem] = Main.LocalPlayer.CheckPocketsForItem(draedonItem);
        }

        private static void AddDraedonDialogues() {
            Mod calamity = CrossModSystem.Calamity;
            string[] itemsOptions = ["OnyxExcavatorKey", "Murasama"];
            _draedonItems = [];
            _doesPlayerHaveDraedonItems = [];

            foreach (string option in itemsOptions) {
                int item = calamity.Find<ModItem>(option).Type;
                string question = LocalizationUtils.GetCalamityText($"DraedonDialogues.{option}.Question");
                string answer = LocalizationUtils.GetCalamityText($"DraedonDialogues.{option}.Response");
                Func<bool> func = () => !_doesPlayerHaveDraedonItems.GetValueOrDefault(item);
                calamity.Call("CreateCodebreakerDialogOption", question, answer, func);
                _draedonItems.Add(answer, item);
                _doesPlayerHaveDraedonItems[item] = false;
            }
        }

        public override bool IsLoadingEnabled(Mod mod) {
            return ModLoader.HasMod("CalamityMod");
        }
    }
}
