using DuckLib.Extensions;
using EverythingRenewableNow.Content.Items.Boulder;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace EverythingRenewableNow.Common.Players {
    // Boulder
    public class PlayerLuck : ModPlayer {
        private bool _brokenMirrorBadLuck;
        private double _brokenMirrorBadLuckTime = 0;

        public override void ModifyLuck(ref float luck) {
            if (Player.HaveBuff(BuffID.Stinky))
                luck -= 0.25f;

            if (_brokenMirrorBadLuck)
                luck -= 0.25f;
            UpdateMirrorLuck();

            luck += CalculateInventoryItemsLuck();
        }

        private float CalculateInventoryItemsLuck() {
            float result = 0;
            bool useVoidBag = false;

            for (int i = 0; i < 58; i++) {
                Item item = Player.inventory[i];
                if (item.stack <= 0)
                    continue;

                if (item.type == ItemID.VoidLens)
                    useVoidBag = true;

                result += GetItemLuck(item);
            }

            if (!useVoidBag)
                return result;

            for (int i = 0; i < 58; i++) {
                Item item = Player.bank4.item[i];
                if (item.stack <= 0)
                    continue;

                result += GetItemLuck(item);
            }

            return result;
        }

        private static float GetItemLuck(Item item) {
            if (item.type == ModContent.ItemType<LuckyClover>())
                return 0.03f;
            if (item.type == ModContent.ItemType<WiltedClover>())
                return -0.1f;
            if (item.type == ModContent.ItemType<RavenFeather>())
                return -0.1f;
            return 0;
        }

        public void StartBadLuckFromMirror() {
            _brokenMirrorBadLuckTime = 25200;
        }

        public override void SaveData(TagCompound tag) {
            tag.Set("brokenMirrorTime", _brokenMirrorBadLuckTime);
        }

        public override void LoadData(TagCompound tag) {
            _brokenMirrorBadLuckTime = tag.GetAsDouble("brokenMirrorTime");
        }

        private void UpdateMirrorLuck() {
            if (_brokenMirrorBadLuckTime > 0) {
                _brokenMirrorBadLuck = true;
                _brokenMirrorBadLuckTime -= Main.dayRate;
                if (_brokenMirrorBadLuckTime < 0)
                    _brokenMirrorBadLuckTime = 0;
            } else {
                _brokenMirrorBadLuck = false;
            }
        }
    }
}
