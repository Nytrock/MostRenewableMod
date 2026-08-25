using DuckLib.Templates;
using EverythingRenewableNow.Common.Players;
using Terraria.ID;

namespace EverythingRenewableNow.Content.Templates.BiomeMimics {
    public abstract class BiomeMimicTemplate : BaseTemplate {
        protected abstract int ChestStyle { get; }
        protected virtual int ChestType => TileID.Containers;
        protected abstract int WeaponType { get; }

        protected abstract int BaseKeyType { get; }
        protected virtual bool HaveAwakenedKey => true;

        private int _keyType;
        protected BiomeChestMimic _mimic;

        protected override void AddContent() {
            DroppedBiomeChest projectile = new(TemplateName, ChestStyle, WeaponType, ChestType);
            Mod.AddContent(projectile);
            _mimic = new(projectile.Type, TemplateName);
            Mod.AddContent(_mimic);

            if (HaveAwakenedKey) {
                AwakenedBiomeKey key = new(TemplateName, BaseKeyType);
                Mod.AddContent(key);
                _keyType = key.Type;
            }
        }

        public override void PostSetupContent() {
            if (_mimic == null)
                return;

            if (HaveAwakenedKey)
                PlayerBiomeMimicSpawn.AddMimicToSpawn(_keyType, _mimic.Type);
            else
                PlayerBiomeMimicSpawn.AddMimicToSpawn(BaseKeyType, _mimic.Type);
        }
    }
}
