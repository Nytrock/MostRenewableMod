using Terraria.GameContent.Bestiary;
using Terraria.Localization;

namespace EverythingRenewableNow.Utils {
    public static class LocalizationUtils {
        private const string _modName = "Mods.EverythingRenewableNow.";

        public static LocalizedText GetText(string key) {
            return Language.GetText(_modName + key);
        }

        public static string GetTextValue(string key) {
            return Language.GetTextValue(_modName + key);
        }

        public static NetworkText GetNetworkText(string key, params object[] substitutions) {
            return NetworkText.FromKey(_modName + key, substitutions);
        }

        public static FlavorTextBestiaryInfoElement GetBestiaryText(string key) {
            return new(_modName + key);
        }
    }
}
