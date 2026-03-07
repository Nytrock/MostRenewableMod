using DuckLib;
using EverythingRenewableNow.Utils;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.Boulder {
    public class MossZombie : ModNPC {
        private static int _headGore;
        private static int _handGore;
        private static int _legGore;

        public override void Load() {
            _headGore = this.CreateGore("Head");
            _handGore = this.CreateGore("Hand");
            _legGore = this.CreateGore("Leg");
        }

        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard,
                LocalizationUtils.GetBestiaryText("Bestiary.MossZombie"),
            ]);
        }

        public override void SetDefaults() {
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = -1;
            NPC.damage = 18;
            NPC.defense = 10;
            NPC.lifeMax = 60;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.knockBackResist = 0.45f;
            NPC.value = 80f;

            AnimationType = NPCID.Zombie;
            Banner = NPCID.Zombie;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            Player player = spawnInfo.Player;
            if (!player.ZoneGraveyard || !player.ZoneOverworldHeight)
                return 0;

            if (player.luck < 0f && Main.rand.NextFloat() < -player.luck && Main.rand.NextBool(15))
                return 1;
            return 0;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            npcLoot.Add(ItemDropRule.OneFromOptions(1,
                ItemID.GreenMoss, ItemID.BrownMoss, ItemID.RedMoss, ItemID.BlueMoss, ItemID.PurpleMoss,
                ItemID.LavaMoss, ItemID.KryptonMoss, ItemID.XenonMoss, ItemID.ArgonMoss, ItemID.VioletMoss
            ));
        }

        public override void HitEffect(NPC.HitInfo hit) {
            if (NPC.life > 0) {
                for (int num555 = 0; num555 < hit.Damage / (double)NPC.lifeMax * 100.0; num555++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.Damage, -1f);
                return;
            } else {
                for (int num556 = 0; num556 < 50; num556++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f);

                IEntitySource source = NPC.GetSource_FromThis();
                Gore.NewGore(source, NPC.position, NPC.velocity, _headGore, NPC.scale);
                Gore.NewGore(source, new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, _legGore, NPC.scale);
                Gore.NewGore(source, new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, _legGore, NPC.scale);
                Gore.NewGore(source, new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, _handGore, NPC.scale);
                Gore.NewGore(source, new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, _handGore, NPC.scale);
            }
        }

        public override void AI() {
            if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height == NPC.position.Y + (float)NPC.height) {
                NPC.directionY = -1;
            }
            bool flag = false;
            bool flag5 = false;
            bool flag6 = false;
            if (NPC.velocity.X == 0f) {
                flag6 = true;
            }
            if (NPC.justHit) {
                flag6 = false;
            }

            int num58 = 60;
            bool flag7 = false;
            bool flag8 = true;
            bool flag9 = false;
            bool flag10 = true;
            if (!flag9 && flag10) {
                if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0))) {
                    flag7 = true;
                }
                if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)num58 || flag7) {
                    NPC.ai[3] += 1f;
                } else if ((double)Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f) {
                    NPC.ai[3] -= 1f;
                }
                if (NPC.ai[3] > (float)(num58 * 10)) {
                    NPC.ai[3] = 0f;
                }
                if (NPC.justHit) {
                    NPC.ai[3] = 0f;
                }
                if (NPC.ai[3] == (float)num58) {
                    NPC.netUpdate = true;
                }
                if (Main.player[NPC.target].Hitbox.Intersects(NPC.Hitbox)) {
                    NPC.ai[3] = 0f;
                }
            }

            if (NPC.ai[3] < (float)num58) {
                if (NPC.shimmerTransparency < 1f && Main.rand.NextBool(1000)) {
                    SoundEngine.PlaySound(SoundID.Zombie1, NPC.position);
                }
                NPC.TargetClosest();
                if (NPC.directionY > 0 && Main.player[NPC.target].Center.Y <= NPC.Bottom.Y) {
                    NPC.directionY = -1;
                }
            } else if (!(NPC.ai[2] > 0f) || !NPC.DespawnEncouragement_AIStyle3_Fighters_CanBeBusyWithAction(NPC.type)) {
                if (NPC.velocity.X == 0f) {
                    if (NPC.velocity.Y == 0f) {
                        NPC.ai[0] += 1f;
                        if (NPC.ai[0] >= 2f) {
                            NPC.direction *= -1;
                            NPC.spriteDirection = NPC.direction;
                            NPC.ai[0] = 0f;
                        }
                    }
                } else {
                    NPC.ai[0] = 0f;
                }
                if (NPC.direction == 0) {
                    NPC.direction = 1;
                }
            }

            float num108 = 0.85f;
            if (NPC.velocity.X < 0f - num108 || NPC.velocity.X > num108) {
                if (NPC.velocity.Y == 0f) {
                    NPC.velocity *= 0.8f;
                }
            } else if (NPC.velocity.X < num108 && NPC.direction == 1) {
                NPC.velocity.X += 0.07f;
                if (NPC.velocity.X > num108) {
                    NPC.velocity.X = num108;
                }
            } else if (NPC.velocity.X > 0f - num108 && NPC.direction == -1) {
                NPC.velocity.X -= 0.07f;
                if (NPC.velocity.X < 0f - num108) {
                    NPC.velocity.X = 0f - num108;
                }
            }

            if (NPC.velocity.Y == 0f || flag) {
                int num186 = (int)(NPC.position.Y + (float)NPC.height + 7f) / 16;
                int num187 = (int)(NPC.position.Y - 9f) / 16;
                int num188 = (int)NPC.position.X / 16;
                int num189 = (int)(NPC.position.X + (float)NPC.width) / 16;
                int num190 = (int)(NPC.position.X + 8f) / 16;
                int num191 = (int)(NPC.position.X + (float)NPC.width - 8f) / 16;
                bool flag23 = false;
                for (int num192 = num190; num192 <= num191; num192++) {
                    Tile tile = Main.tile[num192, num186];
                    Tile tile2 = Main.tile[num192, num187];
                    if (num192 >= num188 && num192 <= num189 && tile == null) {
                        flag23 = true;
                        continue;
                    }
                    if (tile2 != null && WorldGen.SolidTile(num192, num187)) {
                        flag5 = false;
                        break;
                    }
                    if (!flag23 && num192 >= num188 && num192 <= num189 && WorldGen.SolidTileAllowBottomSlope(num192, num186)) {
                        flag5 = true;
                    }
                }
                if (!flag5 && NPC.velocity.Y < 0f) {
                    NPC.velocity.Y = 0f;
                }
                if (flag23) {
                    return;
                }
            }

            if (NPC.velocity.Y >= 0f && NPC.directionY != 1) {
                int num193 = 0;
                if (NPC.velocity.X < 0f) {
                    num193 = -1;
                }
                if (NPC.velocity.X > 0f) {
                    num193 = 1;
                }
                Vector2 vector39 = NPC.position;
                vector39.X += NPC.velocity.X;
                int num194 = (int)((vector39.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * num193)) / 16f);
                int num195 = (int)((vector39.Y + (float)NPC.height - 1f) / 16f);
                if (WorldGen.InWorld(num194, num195, 4)) {
                    if ((float)(num194 * 16) < vector39.X + (float)NPC.width && (float)(num194 * 16 + 16) > vector39.X && ((Main.tile[num194, num195].HasUnactuatedTile && !Main.tile[num194, num195].TopSlope && !Main.tile[num194, num195 - 1].TopSlope && Main.tileSolid[Main.tile[num194, num195].TileType] && !Main.tileSolidTop[Main.tile[num194, num195].TileType]) || (Main.tile[num194, num195 - 1].IsHalfBlock && Main.tile[num194, num195 - 1].HasUnactuatedTile)) && (!Main.tile[num194, num195 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num194, num195 - 1].TileType] || Main.tileSolidTop[Main.tile[num194, num195 - 1].TileType] || (Main.tile[num194, num195 - 1].IsHalfBlock && (!Main.tile[num194, num195 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num194, num195 - 4].TileType] || Main.tileSolidTop[Main.tile[num194, num195 - 4].TileType]))) && (!Main.tile[num194, num195 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num194, num195 - 2].TileType] || Main.tileSolidTop[Main.tile[num194, num195 - 2].TileType]) && (!Main.tile[num194, num195 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num194, num195 - 3].TileType] || Main.tileSolidTop[Main.tile[num194, num195 - 3].TileType]) && (!Main.tile[num194 - num193, num195 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num194 - num193, num195 - 3].TileType])) {
                        float num196 = num195 * 16;
                        if (Main.tile[num194, num195].IsHalfBlock) {
                            num196 += 8f;
                        }
                        if (Main.tile[num194, num195 - 1].IsHalfBlock) {
                            num196 -= 8f;
                        }
                        if (num196 < vector39.Y + (float)NPC.height) {
                            float num197 = vector39.Y + (float)NPC.height - num196;
                            float num198 = 16.1f;
                            if (NPC.type == 163 || NPC.type == 164 || NPC.type == 236 || NPC.type == 239 || NPC.type == 530) {
                                num198 += 8f;
                            }
                            if (num197 <= num198) {
                                NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num196;
                                NPC.position.Y = num196 - (float)NPC.height;
                                if (num197 < 9f) {
                                    NPC.stepSpeed = 1f;
                                } else {
                                    NPC.stepSpeed = 2f;
                                }
                            }
                        }
                    }
                }
            }
            if (flag5) {
                int num199 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)(15 * NPC.direction)) / 16f);
                int num200 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
                if (Main.tile[num199, num200 - 1].HasUnactuatedTile && (Main.tile[num199, num200 - 1].TileType == 10 || Main.tile[num199, num200 - 1].TileType == 388) && flag8) {
                    NPC.ai[2] += 1f;
                    NPC.ai[3] = 0f;
                    if (NPC.ai[2] >= 60f) {
                        bool flag24 = true;
                        bool flag25 = Main.player[NPC.target].ZoneGraveyard && Main.rand.Next(60) == 0;
                        bool flag26 = false;
                        if (NPC.target >= 0) {
                            Player player4 = Main.player[NPC.target];
                            if (!player4.dead && !player4.ghost) {
                                flag26 = true;
                            }
                        }
                        bool flag27 = (!Main.bloodMoon || Main.getGoodWorld) && !flag25 && flag24;
                        if (flag26) {
                            flag27 = false;
                        }
                        if (flag27) {
                            NPC.ai[1] = 0f;
                        }
                        NPC.velocity.X = 0.5f * (float)(-NPC.direction);
                        int num201 = 5;
                        if (Main.tile[num199, num200 - 1].TileType == 388) {
                            num201 = 2;
                        }
                        NPC.ai[1] += num201;
                        if (flag26) {
                            NPC.ai[1] += 6f;
                        } else if (NPC.type == 27) {
                            NPC.ai[1] += 1f;
                        } else if (NPC.type == 31 || NPC.type == 294 || NPC.type == 295 || NPC.type == 296) {
                            NPC.ai[1] += 6f;
                        }
                        NPC.ai[2] = 0f;
                        bool flag28 = false;
                        if (NPC.ai[1] >= 10f) {
                            flag28 = true;
                            NPC.ai[1] = 10f;
                        }
                        if (NPC.type == 460) {
                            flag28 = true;
                        }
                        WorldGen.KillTile(num199, num200 - 1, fail: true);
                        if ((Main.netMode != 1 || !flag28) && flag28 && Main.netMode != 1) {
                            if (Main.tile[num199, num200 - 1].TileType == 10) {
                                bool flag29 = WorldGen.OpenDoor(num199, num200 - 1, NPC.direction);
                                if (!flag29) {
                                    NPC.ai[3] = num58;
                                    NPC.netUpdate = true;
                                }
                                if (Main.netMode == 2 && flag29) {
                                    NetMessage.SendData(19, -1, -1, null, 0, num199, num200 - 1, NPC.direction);
                                }
                            }
                            if (Main.tile[num199, num200 - 1].TileType == 388) {
                                bool flag30 = WorldGen.ShiftTallGate(num199, num200 - 1, closing: false);
                                if (!flag30) {
                                    NPC.ai[3] = num58;
                                    NPC.netUpdate = true;
                                }
                                if (Main.netMode == 2 && flag30) {
                                    NetMessage.SendData(19, -1, -1, null, 4, num199, num200 - 1);
                                }
                            }
                        }
                    }
                } else {
                    int num202 = NPC.spriteDirection;
                    if ((NPC.velocity.X < 0f && num202 == -1) || (NPC.velocity.X > 0f && num202 == 1)) {
                        if (NPC.height >= 32 && WorldGen.SolidTile(num199, num200 - 2)) {
                            if (WorldGen.SolidTile(num199, num200 - 3)) {
                                NPC.velocity.Y = -8f;
                                NPC.netUpdate = true;
                            } else {
                                NPC.velocity.Y = -7f;
                                NPC.netUpdate = true;
                            }
                        } else if (WorldGen.SolidTile(num199, num200 - 1)) {
                            NPC.velocity.Y = -6f;
                            NPC.netUpdate = true;
                        } else if (NPC.position.Y + (float)NPC.height - (float)(num200 * 16) > 20f && !Main.tile[num199, num200].TopSlope && WorldGen.SolidTile(num199, num200)) {
                            NPC.velocity.Y = -5f;
                            NPC.netUpdate = true;
                        } else if (NPC.directionY < 0 && !WorldGen.SolidTileAllowBottomSlope(num199, num200 + 1) && !WorldGen.SolidTileAllowBottomSlope(num199 + NPC.direction, num200 + 1)) {
                            NPC.velocity.Y = -8f;
                            NPC.velocity.X *= 1.5f;
                            NPC.netUpdate = true;
                        } else if (flag8) {
                            NPC.ai[1] = 0f;
                            NPC.ai[2] = 0f;
                        }
                        if (NPC.velocity.Y == 0f && flag6 && NPC.ai[3] == 1f) {
                            NPC.velocity.Y = -5f;
                        }
                        if (NPC.velocity.Y == 0f && Main.expertMode && Main.player[NPC.target].Bottom.Y < NPC.Top.Y && Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < (float)(Main.player[NPC.target].width * 3) && Collision.CanHit(NPC, Main.player[NPC.target])) {
                            if (NPC.velocity.Y == 0f) {
                                int num205 = 6;
                                if (Main.player[NPC.target].Bottom.Y > NPC.Top.Y - (float)(num205 * 16)) {
                                    NPC.velocity.Y = -7.9f;
                                } else {
                                    int num206 = (int)(NPC.Center.X / 16f);
                                    int num207 = (int)(NPC.Bottom.Y / 16f) - 1;
                                    for (int num208 = num207; num208 > num207 - num205; num208--) {
                                        if (Main.tile[num206, num208] != null && Main.tile[num206, num208].HasUnactuatedTile && TileID.Sets.Platforms[Main.tile[num206, num208].TileType]) {
                                            NPC.velocity.Y = -7.9f;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } else if (flag8) {
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
            }
        }
    }
}
