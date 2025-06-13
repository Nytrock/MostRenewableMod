using EverythingRenewableNow.Content.Dusts;
using EverythingRenewableNow.Content.Gores;
using EverythingRenewableNow.Content.Items.Banners;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs {
    public class SlushMerman : ModNPC {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.IcyMerman];

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new() {
                Velocity = 2f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override bool? CanBeHitByProjectile(Projectile projectile) {
            if (projectile.type == ProjectileID.SlushBall)
                return false;
            return base.CanBeHitByProjectile(projectile);
        }

        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
                new FlavorTextBestiaryInfoElement("Mods.EverythingRenewableNow.Bestiary.SlushMerman"),
            ]);
        }

        public override void SetDefaults() {
            NPC.CloneDefaults(NPCID.IcyMerman);
            AnimationType = NPCID.IcyMerman;
            NPC.aiStyle = -1;
            NPC.damage = 10;
            NPC.defense = 8;
            NPC.lifeMax = 55;

            Banner = Type;
            BannerItem = ModContent.ItemType<SlushMermanBanner>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            if (!spawnInfo.Player.ZoneSnow || !spawnInfo.Player.ZoneRockLayerHeight)
                return 0f;

            if (!TileID.Sets.IcesSnow[spawnInfo.SpawnTileType])
                return 0f;

            return 0.1f;
        }

        public override void HitEffect(NPC.HitInfo hit) {
            if (NPC.life > 0) {
                for (int num525 = 0; num525 < hit.Damage / (double)NPC.lifeMax * 150.0; num525++) {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<SlushMermanDust>(), hit.HitDirection, -1f);
                }
                return;
            }

            for (int num526 = 0; num526 < 75; num526++) {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<SlushMermanDust>(), 2 * hit.HitDirection, -2f);
            }
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, ModContent.GoreType<SlushMermanHead>(), NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, ModContent.GoreType<SlushMermanHand>(), NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, ModContent.GoreType<SlushMermanHand>(), NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, ModContent.GoreType<SlushMermanLeg>(), NPC.scale);
            Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, ModContent.GoreType<SlushMermanLeg>(), NPC.scale);
        }

        public override void AI() {
            if (Main.player[NPC.target].position.Y + Main.player[NPC.target].height == NPC.position.Y + NPC.height) {
                NPC.directionY = -1;
            }
            bool flag5 = false;
            bool flag6 = false;
            if (NPC.velocity.X == 0f) {
                flag6 = true;
            }
            if (NPC.justHit) {
                flag6 = false;
            }
            int num56 = 60;
            bool flag7 = false;
            bool flag8 = false;
            bool flag9 = false;
            bool flag10 = true;
            if (NPC.ai[2] > 0f) {
                flag10 = false;
            }

            if (!flag9 && flag10) {
                if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0))) {
                    flag7 = true;
                }
                if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= num56 || flag7) {
                    NPC.ai[3] += 1f;
                } else if ((double)Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f) {
                    NPC.ai[3] -= 1f;
                }
                if (NPC.ai[3] > num56 * 10) {
                    NPC.ai[3] = 0f;
                }
                if (NPC.justHit) {
                    NPC.ai[3] = 0f;
                }
                if (NPC.ai[3] == num56) {
                    NPC.netUpdate = true;
                }
                if (Main.player[NPC.target].Hitbox.Intersects(NPC.Hitbox)) {
                    NPC.ai[3] = 0f;
                }
            }

            if (NPC.ai[3] < num56) {
                NPC.TargetClosest();
                if (NPC.directionY > 0 && Main.player[NPC.target].Center.Y <= NPC.Bottom.Y) {
                    NPC.directionY = -1;
                }
            } else if (!(NPC.ai[2] > 0f) && !NPC.DespawnEncouragement_AIStyle3_Fighters_CanBeBusyWithAction(NPC.type)) {
                if (Main.IsItDay() && (double)(NPC.position.Y / 16f) < Main.worldSurface) {
                    NPC.EncourageDespawn(10);
                }
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

            bool flag16 = false;
            bool flag17 = false;
            bool flag18 = true;
            int num155 = -1;
            int num156 = -1;
            if (NPC.ai[1] > 0f) {
                NPC.ai[1] -= 1f;
            }
            if (NPC.justHit) {
                NPC.ai[1] = 30f;
                NPC.ai[2] = 0f;
            }
            int rapid = 120;
            int num158 = rapid / 2;
            if (NPC.confused) {
                NPC.ai[2] = 0f;
            }
            if (NPC.ai[2] > 0f) {
                if (flag18) {
                    NPC.TargetClosest();
                }
                if (NPC.ai[1] == num158) {
                    float num159 = 15f;
                    Vector2 chaserPosition2 = new(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    chaserPosition2.Y -= 10f;
                    float speedX = Main.player[NPC.target].position.X + Main.player[NPC.target].width * 0.5f - chaserPosition2.X;
                    float speedY = Main.player[NPC.target].position.Y - chaserPosition2.Y;
                    float num163 = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                    NPC.netUpdate = true;
                    num163 = num159 / num163;
                    speedX *= num163;
                    speedY *= num163;
                    int damage = 10;
                    int type = 179;

                    Player player3 = Main.player[NPC.target];
                    Vector2? vector35 = null;
                    if (vector35.HasValue) {
                        Terraria.Utils.ChaseResults chaseResults2 = Terraria.Utils.GetChaseResults(chaserPosition2, num159, vector35.Value, player3.velocity);
                        if (chaseResults2.InterceptionHappens) {
                            Vector2 vector36 = Terraria.Utils.FactorAcceleration(chaseResults2.ChaserVelocity, chaseResults2.InterceptionTime, new Vector2(0f, 0.1f), 15);
                            speedX = vector36.X;
                            speedY = vector36.Y;
                        }
                    }
                    chaserPosition2.X += speedX;
                    chaserPosition2.Y += speedY;
                    if (Main.netMode != NetmodeID.MultiplayerClient) {
                        SoundEngine.PlaySound(in SoundID.Item17, NPC.position);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), chaserPosition2.X, chaserPosition2.Y, speedX, speedY, type, damage, 0f, Main.myPlayer);
                    }
                    if (Math.Abs(speedY) > Math.Abs(speedX) * 2f) {
                        if (speedY > 0f) {
                            NPC.ai[2] = 1f;
                        } else {
                            NPC.ai[2] = 5f;
                        }
                    } else if (Math.Abs(speedX) > Math.Abs(speedY) * 2f) {
                        NPC.ai[2] = 3f;
                    } else if (speedY > 0f) {
                        NPC.ai[2] = 2f;
                    } else {
                        NPC.ai[2] = 4f;
                    }
                }
                if ((NPC.velocity.Y != 0f && !flag17) || NPC.ai[1] <= 0f) {
                    NPC.ai[2] = 0f;
                    NPC.ai[1] = 0f;
                } else if (!flag16 || (num155 != -1 && NPC.ai[1] >= num155 && NPC.ai[1] < num155 + num156 && (!flag17 || NPC.velocity.Y == 0f))) {
                    NPC.velocity.X *= 0.9f;
                    NPC.spriteDirection = NPC.direction;
                }
            }
            if ((NPC.ai[2] <= 0f || flag16) && (NPC.velocity.Y == 0f || flag17) && NPC.ai[1] <= 0f && !Main.player[NPC.target].dead) {
                bool flag20 = Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height);
                if (Main.player[NPC.target].stealth == 0f && Main.player[NPC.target].itemAnimation == 0) {
                    flag20 = false;
                }
                if (flag20) {
                    float num169 = 10f;
                    Vector2 vector37 = new(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    float num170 = Main.player[NPC.target].position.X + Main.player[NPC.target].width * 0.5f - vector37.X;
                    float num171 = Math.Abs(num170) * 0.1f;
                    float num172 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height * 0.5f - vector37.Y - num171;
                    num170 += Main.rand.Next(-40, 41);
                    num172 += Main.rand.Next(-40, 41);
                    float num173 = (float)Math.Sqrt(num170 * num170 + num172 * num172);
                    float num174 = 700f;
                    if (num173 < num174) {
                        NPC.netUpdate = true;
                        NPC.velocity.X *= 0.5f;
                        num173 = num169 / num173;
                        num170 *= num173;
                        num172 *= num173;
                        NPC.ai[2] = 3f;
                        NPC.ai[1] = rapid;
                        if (Math.Abs(num172) > Math.Abs(num170) * 2f) {
                            if (num172 > 0f) {
                                NPC.ai[2] = 1f;
                            } else {
                                NPC.ai[2] = 5f;
                            }
                        } else if (Math.Abs(num170) > Math.Abs(num172) * 2f) {
                            NPC.ai[2] = 3f;
                        } else if (num172 > 0f) {
                            NPC.ai[2] = 2f;
                        } else {
                            NPC.ai[2] = 4f;
                        }
                    }
                }
            }
            if (NPC.ai[2] <= 0f || (flag16 && (num155 == -1 || !(NPC.ai[1] >= num155) || !(NPC.ai[1] < num155 + num156)))) {
                float num175 = 1f;
                float num176 = 0.07f;
                float num177 = 0.8f;
                bool flag21 = false;
                if (NPC.velocity.X < 0f - num175 || NPC.velocity.X > num175 || flag21) {
                    if (NPC.velocity.Y == 0f) {
                        NPC.velocity *= num177;
                    }
                } else if (NPC.velocity.X < num175 && NPC.direction == 1) {
                    NPC.velocity.X += num176;
                    if (NPC.velocity.X > num175) {
                        NPC.velocity.X = num175;
                    }
                } else if (NPC.velocity.X > 0f - num175 && NPC.direction == -1) {
                    NPC.velocity.X -= num176;
                    if (NPC.velocity.X < 0f - num175) {
                        NPC.velocity.X = 0f - num175;
                    }
                }
            }

            if (NPC.velocity.Y == 0f) {
                int num181 = (int)(NPC.position.Y + NPC.height + 7f) / 16;
                int num182 = (int)(NPC.position.Y - 9f) / 16;
                int num183 = (int)NPC.position.X / 16;
                int num184 = (int)(NPC.position.X + NPC.width) / 16;
                int num185 = (int)(NPC.position.X + 8f) / 16;
                int num186 = (int)(NPC.position.X + NPC.width - 8f) / 16;
                bool flag22 = false;
                for (int num187 = num185; num187 <= num186; num187++) {
                    if (num187 >= num183 && num187 <= num184 && Main.tile[num187, num181] == null) {
                        flag22 = true;
                        continue;
                    }
                    if (Main.tile[num187, num182] != null && Main.tile[num187, num182].HasUnactuatedTile && Main.tileSolid[Main.tile[num187, num182].TileType]) {
                        flag5 = false;
                        break;
                    }
                    if (!flag22 && num187 >= num183 && num187 <= num184 && Main.tile[num187, num181].HasUnactuatedTile && Main.tileSolid[Main.tile[num187, num181].TileType]) {
                        flag5 = true;
                    }
                }
                if (!flag5 && NPC.velocity.Y < 0f) {
                    NPC.velocity.Y = 0f;
                }
                if (flag22) {
                    return;
                }
            }

            if (NPC.velocity.Y >= 0f && NPC.directionY != 1) {
                int num188 = 0;
                if (NPC.velocity.X < 0f) {
                    num188 = -1;
                }
                if (NPC.velocity.X > 0f) {
                    num188 = 1;
                }
                Vector2 vector39 = NPC.position;
                vector39.X += NPC.velocity.X;
                int num189 = (int)((vector39.X + NPC.width / 2 + (NPC.width / 2 + 1) * num188) / 16f);
                int num190 = (int)((vector39.Y + NPC.height - 1f) / 16f);
                if (WorldGen.InWorld(num189, num190, 4)) {
                    if (num189 * 16 < vector39.X + NPC.width && num189 * 16 + 16 > vector39.X && ((Main.tile[num189, num190].HasUnactuatedTile && !Main.tile[num189, num190].TopSlope && !Main.tile[num189, num190 - 1].TopSlope && Main.tileSolid[Main.tile[num189, num190].TileType] && !Main.tileSolidTop[Main.tile[num189, num190].TileType]) || (Main.tile[num189, num190 - 1].IsHalfBlock && Main.tile[num189, num190 - 1].HasUnactuatedTile)) && (!Main.tile[num189, num190 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num189, num190 - 1].TileType] || Main.tileSolidTop[Main.tile[num189, num190 - 1].TileType] || (Main.tile[num189, num190 - 1].IsHalfBlock && (!Main.tile[num189, num190 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num189, num190 - 4].TileType] || Main.tileSolidTop[Main.tile[num189, num190 - 4].TileType]))) && (!Main.tile[num189, num190 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num189, num190 - 2].TileType] || Main.tileSolidTop[Main.tile[num189, num190 - 2].TileType]) && (!Main.tile[num189, num190 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num189, num190 - 3].TileType] || Main.tileSolidTop[Main.tile[num189, num190 - 3].TileType]) && (!Main.tile[num189 - num188, num190 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num189 - num188, num190 - 3].TileType])) {
                        float num191 = num190 * 16;
                        if (Main.tile[num189, num190].IsHalfBlock) {
                            num191 += 8f;
                        }
                        if (Main.tile[num189, num190 - 1].IsHalfBlock) {
                            num191 -= 8f;
                        }
                        if (num191 < vector39.Y + NPC.height) {
                            float num192 = vector39.Y + NPC.height - num191;
                            float num193 = 16.1f;
                            if (num192 <= num193) {
                                NPC.gfxOffY += NPC.position.Y + NPC.height - num191;
                                NPC.position.Y = num191 - NPC.height;
                                if (num192 < 9f) {
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
                int num194 = (int)((NPC.position.X + NPC.width / 2 + 15 * NPC.direction) / 16f);
                int num195 = (int)((NPC.position.Y + NPC.height - 15f) / 16f);
                if (Main.tile[num194, num195 - 1].HasUnactuatedTile && (TileLoader.IsClosedDoor(Main.tile[num194, num195 - 1]) || Main.tile[num194, num195 - 1].TileType == 388) && flag8) {
                    NPC.ai[2] += 1f;
                    NPC.ai[3] = 0f;
                    if (NPC.ai[2] >= 60f) {
                        bool flag23 = false;
                        bool flag24 = Main.player[NPC.target].ZoneGraveyard && Main.rand.NextBool(60);
                        if ((!Main.bloodMoon || Main.getGoodWorld) && !flag24 && flag23) {
                            NPC.ai[1] = 0f;
                        }
                        NPC.velocity.X = 0.5f * -NPC.direction;
                        int num196 = 5;
                        if (Main.tile[num194, num195 - 1].TileType == 388) {
                            num196 = 2;
                        }
                        NPC.ai[1] += num196;
                        NPC.ai[2] = 0f;
                        bool flag25 = false;
                        if (NPC.ai[1] >= 10f) {
                            flag25 = true;
                            NPC.ai[1] = 10f;
                        }
                        WorldGen.KillTile(num194, num195 - 1, fail: true);
                        if ((Main.netMode != NetmodeID.MultiplayerClient || !flag25) && flag25 && Main.netMode != NetmodeID.MultiplayerClient) {
                            if (TileLoader.IsClosedDoor(Main.tile[num194, num195 - 1])) {
                                bool flag26 = WorldGen.OpenDoor(num194, num195 - 1, NPC.direction);
                                if (!flag26) {
                                    NPC.ai[3] = num56;
                                    NPC.netUpdate = true;
                                }
                                if (Main.netMode == NetmodeID.Server && flag26) {
                                    NetMessage.SendData(MessageID.ToggleDoorState, -1, -1, null, 0, num194, num195 - 1, NPC.direction);
                                }
                            }
                            if (Main.tile[num194, num195 - 1].TileType == 388) {
                                bool flag27 = WorldGen.ShiftTallGate(num194, num195 - 1, closing: false);
                                if (!flag27) {
                                    NPC.ai[3] = num56;
                                    NPC.netUpdate = true;
                                }
                                if (Main.netMode == NetmodeID.Server && flag27) {
                                    NetMessage.SendData(MessageID.ToggleDoorState, -1, -1, null, 4, num194, num195 - 1);
                                }
                            }
                        }
                    }
                } else {
                    int num197 = NPC.spriteDirection;
                    if ((NPC.velocity.X < 0f && num197 == -1) || (NPC.velocity.X > 0f && num197 == 1)) {
                        if (NPC.height >= 32 && Main.tile[num194, num195 - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[num194, num195 - 2].TileType]) {
                            if (Main.tile[num194, num195 - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[num194, num195 - 3].TileType]) {
                                NPC.velocity.Y = -8f;
                                NPC.netUpdate = true;
                            } else {
                                NPC.velocity.Y = -7f;
                                NPC.netUpdate = true;
                            }
                        } else if (Main.tile[num194, num195 - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[num194, num195 - 1].TileType]) {
                            NPC.velocity.Y = -6f;
                            NPC.netUpdate = true;
                        } else if (NPC.position.Y + NPC.height - num195 * 16 > 20f && Main.tile[num194, num195].HasUnactuatedTile && !Main.tile[num194, num195].TopSlope && Main.tileSolid[Main.tile[num194, num195].TileType]) {
                            NPC.velocity.Y = -5f;
                            NPC.netUpdate = true;
                        } else if (NPC.directionY < 0 && (!Main.tile[num194, num195 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num194, num195 + 1].TileType]) && (!Main.tile[num194 + NPC.direction, num195 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num194 + NPC.direction, num195 + 1].TileType])) {
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
                        if (NPC.velocity.Y == 0f && Main.expertMode && Main.player[NPC.target].Bottom.Y < NPC.Top.Y && Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < Main.player[NPC.target].width * 3 && Collision.CanHit(NPC, Main.player[NPC.target])) {
                            if (NPC.velocity.Y == 0f) {
                                int num200 = 6;
                                if (Main.player[NPC.target].Bottom.Y > NPC.Top.Y - num200 * 16) {
                                    NPC.velocity.Y = -7.9f;
                                } else {
                                    int num201 = (int)(NPC.Center.X / 16f);
                                    int num202 = (int)(NPC.Bottom.Y / 16f) - 1;
                                    for (int num203 = num202; num203 > num202 - num200; num203--) {
                                        if (Main.tile[num201, num203].HasUnactuatedTile && TileID.Sets.Platforms[Main.tile[num201, num203].TileType]) {
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