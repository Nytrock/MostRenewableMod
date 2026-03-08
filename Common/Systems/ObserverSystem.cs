using DuckLib;
using EverythingRenewableNow.Content.Items;
using EverythingRenewableNow.Content.Items.Boulder;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems {
    public class ObserverSystem : ModSystem {
        public override void PostSetupContent() {
            DuckWorldObserver.DemonAltarsObserver.Items.Observe(ModContent.ItemType<CrimsonAltar>());
            DuckWorldObserver.DemonAltarsObserver.Items.Observe(ModContent.ItemType<CorruptionAltar>());
            DuckWorldObserver.DungeonObserver.Items.Observe(ModContent.ItemType<MiniDungeon>());
            DuckWorldObserver.TempleObserver.Items.Observe(ModContent.ItemType<MiniTemple>());
        }
    }
}
