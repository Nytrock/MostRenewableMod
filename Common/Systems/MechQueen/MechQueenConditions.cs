using EverythingRenewableNow.Utils;
using Terraria.GameContent.ItemDropRules;

namespace EverythingRenewableNow.Common.Systems.MechQueen {
    public class MechQueenConditions {
        public class Died : IItemDropRuleCondition, IProvideItemConditionDescription {
            public bool CanDrop(DropAttemptInfo info) => MechQueenSystem.IsMechQueenDied;
            public bool CanShowItemDropInUI() => MechQueenSystem.IsMechQueenDied;
            public string GetConditionDescription() => LocalizationUtils.GetTextValue("Conditions.MechQueen.Died");
        }
    }
}
