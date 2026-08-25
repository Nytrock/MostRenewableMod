using DuckLib;
using EverythingRenewableNow.Common.Systems;
using EverythingRenewableNow.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs {
    public class AbyssSnail : ModNPC {
        private static int _abyssTreasure;

        private static int _headGore;
        private static int _bodyGore;
        private static int _dust;

        public override void Load() {
            _headGore = this.CreateGore("Head");
            _bodyGore = this.CreateGore("Body");
            _dust = this.CreateDust();
        }

        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Snail];

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new() {
                Position = new Vector2(0, 16),
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            _abyssTreasure = CrossModSystem.Calamity.Find<ModItem>("AbyssalTreasure").Type;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            if (!spawnInfo.Player.InCalamityZone("layer2")
                && !spawnInfo.Player.InCalamityZone("layer3")
                && !spawnInfo.Player.InCalamityZone("layer4"))
                return 0f;

            return 0.1f;
        }

        public override void SetDefaults() {
            NPC.CloneDefaults(NPCID.Snail);
            AnimationType = NPCID.Snail;
            NPC.catchItem = _abyssTreasure;
            SpawnModBiomes = [
                CrossModSystem.Calamity.Find<ModBiome>("AbyssLayer2Biome").Type,
                CrossModSystem.Calamity.Find<ModBiome>("AbyssLayer3Biome").Type,
                CrossModSystem.Calamity.Find<ModBiome>("AbyssLayer4Biome").Type
            ];

            NPCID.Sets.CountsAsCritter[Type] = true;
            Main.npcCatchable[Type] = true;
        }

        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange([
                LocalizationUtils.GetBestiaryText("Bestiary.AbyssSnail"),
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit) {
            if (NPC.life <= 0) {
                for (int i = 0; i < 6; i++) {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, _dust, 2 * hit.HitDirection, -2f);
                    if (Main.rand.NextBool(2)) {
                        dust.noGravity = true;
                        dust.scale = 1.2f * NPC.scale;
                    } else {
                        dust.scale = 0.7f * NPC.scale;
                    }
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, _headGore, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, _bodyGore, NPC.scale);
            }
        }

        public override bool IsLoadingEnabled(Mod mod) {
            return ModLoader.HasMod("CalamityMod");
        }
    }
}
