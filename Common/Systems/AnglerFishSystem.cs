using DuckLib;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Common.Systems {
    public class AnglerFishSystem : ModSystem {
        private readonly List<int> _crimsonFishPrehardmode = [ItemID.BloodyManowar];
        private readonly List<int> _crimsonFishHardmode = [ItemID.Ichorfish];
        private readonly List<int> _corruptionFishPrehardmode = [ItemID.InfectedScabbardfish, ItemID.EaterofPlankton];
        private readonly List<int> _corruptionFishHardmode = [ItemID.Cursedfish];

        private int _nowFish;
        private int _newFish;

        public override void Load() {
            DuckHook.OnDayStarted += CheckAnglerFish;
        }

        private void CheckAnglerFish() {
            _nowFish = Main.anglerQuestItemNetIDs[Main.anglerQuest];
            _newFish = _nowFish;

            TryToChangeFish(_crimsonFishPrehardmode, WorldGen.crimson, _corruptionFishPrehardmode);
            TryToChangeFish(_corruptionFishPrehardmode, !WorldGen.crimson, _crimsonFishPrehardmode);

            if (Main.hardMode) {
                TryToChangeFish(_crimsonFishHardmode, WorldGen.crimson, _corruptionFishHardmode);
                TryToChangeFish(_corruptionFishHardmode, !WorldGen.crimson, _crimsonFishHardmode);
            }

            if (_newFish != _nowFish)
                Main.anglerQuest = Array.FindIndex(Main.anglerQuestItemNetIDs, ID => ID == _newFish);
        }

        private void TryToChangeFish(List<int> currentEvilFishes, bool isCurrentEvil, List<int> otherEvilFish) {
            if (currentEvilFishes.Contains(_nowFish) && isCurrentEvil && Main.rand.NextBool() && DuckWorldObserver.OppositeEvilObserver.HaveInWorld)
                _newFish = Main.rand.Next(otherEvilFish);
        }
    }
}
