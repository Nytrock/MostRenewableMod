using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.BiomeChestMimics {
    public abstract class BiomeChestMimic : ModNPC {
        public abstract int ProjectileType { get; }

        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BigMimicCorruption];
        }

        public override void SetDefaults() {
            NPC.CloneDefaults(NPCID.BigMimicCorruption);
            AIType = NPCID.BigMimicCorruption;
            AnimationType = NPCID.BigMimicCorruption;
        }

        public override void HitEffect(NPC.HitInfo hit) {
            if (NPC.life > 0)
                return;

            float hitDirection = hit.HitDirection;
            float rand;
            for (rand = Main.rand.Next(-35, 36) * 0.1f; rand < 2f && rand > -2f; rand += Main.rand.Next(-30, 31) * 0.1f) { }

            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, Main.rand.Next(10, 30) * 0.1f * (float)hitDirection + rand, Main.rand.Next(-40, -20) * 0.1f, ProjectileType, 0, 0);
        }
    }
}
