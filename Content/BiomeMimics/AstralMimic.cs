using EverythingRenewableNow.Common.Systems;
using EverythingRenewableNow.Content.Items.Calamity;
using EverythingRenewableNow.Content.Templates.BiomeMimics;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.BiomeMimics {
    public class AstralMimic : BiomeMimicTemplate {
        protected override int BaseKeyType => ModContent.ItemType<AstralKey>();
        protected override string TemplateName => "Astral";
        protected override int ChestStyle => 1;
        protected override bool HaveAwakenedKey => false;

        protected override int WeaponType {
            get {
                CrossModSystem.Calamity.TryFind("HeavenfallenStardisk", out ModItem weapon);
                return weapon.Type;
            }
        }

        protected override int ChestType {
            get {
                CrossModSystem.Calamity.TryFind("AstralChestLocked", out ModTile chest);
                return chest.Type;
            }
        }

        protected override void AddContent() {
            if (!ModLoader.HasMod("CalamityMod"))
                return;

            base.AddContent();
        }
    }
}
