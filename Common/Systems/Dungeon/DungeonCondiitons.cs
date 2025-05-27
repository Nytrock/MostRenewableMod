using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace EverythingRenewableNow.Common.Systems.Dungeon {
    public class DungeonCondiitons {
        public class PinkBrick : IItemDropRuleCondition, IProvideItemConditionDescription {
            public bool CanDrop(DropAttemptInfo info) => DungeonSystem.TileType == DungeonTileType.Pink;
            public bool CanShowItemDropInUI() => DungeonSystem.TileType == DungeonTileType.Pink;
            public string GetConditionDescription() => Language.GetTextValue("Mods.EverythingRenewableNow.Conditions.Dungeon.Pink");
        }

        public class BlueBrick : IItemDropRuleCondition, IProvideItemConditionDescription {
            public bool CanDrop(DropAttemptInfo info) => DungeonSystem.TileType == DungeonTileType.Blue;
            public bool CanShowItemDropInUI() => DungeonSystem.TileType == DungeonTileType.Blue;
            public string GetConditionDescription() => Language.GetTextValue("Mods.EverythingRenewableNow.Conditions.Dungeon.Blue");
        }

        public class GreenBrick : IItemDropRuleCondition, IProvideItemConditionDescription {
            public bool CanDrop(DropAttemptInfo info) => DungeonSystem.TileType == DungeonTileType.Green;
            public bool CanShowItemDropInUI() => DungeonSystem.TileType == DungeonTileType.Green;
            public string GetConditionDescription() => Language.GetTextValue("Mods.EverythingRenewableNow.Conditions.Dungeon.Green");
        }
    }
}
