using DuckLib.Templates;
using EverythingRenewableNow.Common.Players;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.Templates.BiomeMimics {
    public abstract class BiomeMimicTemplate : BaseTemplate {
        protected abstract int KeyType { get; }
        protected abstract int WeaponType { get; }
        protected abstract int KeyHeight { get; }
        protected abstract int KeyWidth { get; }
        protected abstract int ChestStyle { get; }
        protected virtual int ChestType => TileID.Containers;

        public static int MimicType { get; private set; }
        public static int AwakenedKeyType { get; private set; }

        public override void Load(Mod mod) {
            DroppedBiomeChest projectile = new(Name, ChestStyle, WeaponType, ChestType);
            AwakenedBiomeKey key = new(KeyWidth, KeyHeight, Name, KeyType);
            mod.AddContent(key);
            mod.AddContent(projectile);

            BiomeChestMimic mimic = new(projectile.Type, Name);
            mod.AddContent(mimic);

            MimicType = mimic.Type;
            AwakenedKeyType = key.Type;
            PlayerBiomeMimicSpawn.AddMimicToSpawn(AwakenedKeyType, MimicType);
        }
    }
}
