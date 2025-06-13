using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.BiomeChestMimics {
    public abstract class BiomeChestMimic : ModNPC {
        public abstract int ProjectileType { get; }
        public abstract string MimicName { get; }

        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = 10;
        }

        public override void SetDefaults() {
            NPC.CloneDefaults(NPCID.BigMimicCorruption);
            // AIType = NPCID.BigMimicCorruption;

            NPC.damage = 120;
            NPC.defense = 50;
            NPC.lifeMax = 7000;
            NPC.value = Item.buyPrice(gold: 7);

            NPC.aiStyle = -1;
        }

        public override void AI() {
            NPC.knockBackResist = 0.2f * Main.GameModeInfo.KnockbackToEnemiesMultiplier;
            NPC.dontTakeDamage = false;
            NPC.noTileCollide = false;
            NPC.noGravity = false;
            NPC.reflectsProjectiles = false;

            if (NPC.ai[0] != 7f && Main.player[NPC.target].dead) {
                NPC.TargetClosest();
                if (Main.player[NPC.target].dead) {
                    NPC.ai[0] = 7f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
            }
            if (NPC.ai[0] == 0f) {
                NPC.TargetClosest();
                Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
                if (Main.netMode != NetmodeID.MultiplayerClient && (NPC.velocity.X != 0f || NPC.velocity.Y > 100f || NPC.justHit || vector.Length() < 80f)) {
                    NPC.ai[0] = 1f;
                    NPC.ai[1] = 0f;
                    NPC.netUpdate = true;
                }
            } else if (NPC.ai[0] == 1f) {
                NPC.ai[1] += 1f;
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] > 24f) {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                    NPC.netUpdate = true;
                }
            } else if (NPC.ai[0] == 2f) {
                Vector2 vector1 = Main.player[NPC.target].Center - NPC.Center;
                if (Main.netMode != NetmodeID.MultiplayerClient && vector1.Length() > 600f) {
                    NPC.ai[0] = 5f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
                if (NPC.velocity.Y == 0f) {
                    NPC.TargetClosest();
                    NPC.velocity.X *= 0.85f;
                    NPC.ai[1] += 1f;
                    float num1396 = 15f + 30f * (NPC.life / (float)NPC.lifeMax);
                    float num1397 = 3f + 4f * (1f - NPC.life / (float)NPC.lifeMax);
                    float num1398 = 4f;
                    if (!Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1)) {
                        num1398 += 2f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] > num1396) {
                        NPC.ai[3] += 1f;
                        if (NPC.ai[3] >= 3f) {
                            NPC.ai[3] = 0f;
                            num1398 *= 2f;
                            num1397 /= 2f;
                        }
                        NPC.ai[1] = 0f;
                        NPC.velocity.Y -= num1398;
                        NPC.velocity.X = num1397 * NPC.direction;
                        NPC.netUpdate = true;
                    }
                } else {
                    NPC.knockBackResist = 0f;
                    NPC.velocity.X *= 0.99f;
                    if (NPC.direction < 0 && NPC.velocity.X > -1f) {
                        NPC.velocity.X = -1f;
                    }
                    if (NPC.direction > 0 && NPC.velocity.X < 1f) {
                        NPC.velocity.X = 1f;
                    }
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] > 210.0 && NPC.velocity.Y == 0f && Main.netMode != NetmodeID.MultiplayerClient) {
                    switch (Main.rand.Next(3)) {
                        case 0:
                            NPC.ai[0] = 3f;
                            break;
                        case 1:
                            NPC.ai[0] = 4f;
                            NPC.noTileCollide = true;
                            NPC.velocity.Y = -8f;
                            break;
                        case 2:
                            NPC.ai[0] = 6f;
                            break;
                        default:
                            NPC.ai[0] = 2f;
                            break;
                    }
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
            } else if (NPC.ai[0] == 3f) {
                NPC.velocity.X *= 0.85f;
                NPC.dontTakeDamage = true;
                NPC.ai[1] += 1f;
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] >= 180f) {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                    NPC.netUpdate = true;
                }
                if (Main.expertMode) {
                    NPC.ReflectProjectiles(NPC.Hitbox);
                    NPC.reflectsProjectiles = true;
                }
            } else if (NPC.ai[0] == 4f) {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                NPC.knockBackResist = 0f;
                if (NPC.velocity.X < 0f) {
                    NPC.direction = -1;
                } else {
                    NPC.direction = 1;
                }
                NPC.spriteDirection = NPC.direction;
                NPC.TargetClosest();
                Vector2 center35 = Main.player[NPC.target].Center;
                center35.Y -= 350f;
                Vector2 vector256 = center35 - NPC.Center;
                if (NPC.ai[2] == 1f) {
                    NPC.ai[1] += 1f;
                    vector256 = Main.player[NPC.target].Center - NPC.Center;
                    vector256.Normalize();
                    vector256 *= 8f;
                    NPC.velocity = (NPC.velocity * 4f + vector256) / 5f;
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] > 6f) {
                        NPC.ai[1] = 0f;
                        NPC.ai[0] = 4.1f;
                        NPC.ai[2] = 0f;
                        NPC.velocity = vector256;
                        NPC.netUpdate = true;
                    }
                } else if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 40f && NPC.Center.Y < Main.player[NPC.target].Center.Y - 300f) {
                    if (Main.netMode != NetmodeID.MultiplayerClient) {
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 1f;
                        NPC.netUpdate = true;
                    }
                } else {
                    vector256.Normalize();
                    vector256 *= 12f;
                    NPC.velocity = (NPC.velocity * 5f + vector256) / 6f;
                }
            } else if (NPC.ai[0] == 4.1f) {
                NPC.knockBackResist = 0f;
                if (NPC.ai[2] == 0f && Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1) && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) {
                    NPC.ai[2] = 1f;
                }
                if (NPC.position.Y + NPC.height >= Main.player[NPC.target].position.Y || NPC.velocity.Y <= 0f) {
                    NPC.ai[1] += 1f;
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] > 10f) {
                        NPC.ai[0] = 2f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                        NPC.netUpdate = true;
                        if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) {
                            NPC.ai[0] = 5f;
                        }
                    }
                } else if (NPC.ai[2] == 0f) {
                    NPC.noTileCollide = true;
                    NPC.noGravity = true;
                    NPC.knockBackResist = 0f;
                }
                NPC.velocity.Y += 0.2f;
                if (NPC.velocity.Y > 16f) {
                    NPC.velocity.Y = 16f;
                }
            } else if (NPC.ai[0] == 5f) {
                if (NPC.velocity.X > 0f) {
                    NPC.direction = 1;
                } else {
                    NPC.direction = -1;
                }
                NPC.spriteDirection = NPC.direction;
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                NPC.knockBackResist = 0f;
                Vector2 vector257 = Main.player[NPC.target].Center - NPC.Center;
                vector257.Y -= 4f;
                if (Main.netMode != NetmodeID.MultiplayerClient && vector257.Length() < 200f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
                if (vector257.Length() > 10f) {
                    vector257.Normalize();
                    vector257 *= 10f;
                }
                NPC.velocity = (NPC.velocity * 4f + vector257) / 5f;
            } else if (NPC.ai[0] == 6f) {
                NPC.knockBackResist = 0f;
                if (NPC.velocity.Y == 0f) {
                    NPC.TargetClosest();
                    NPC.velocity.X *= 0.8f;
                    NPC.ai[1] += 1f;
                    if (NPC.ai[1] > 5f) {
                        NPC.ai[1] = 0f;
                        NPC.velocity.Y -= 4f;
                        if (Main.player[NPC.target].position.Y + Main.player[NPC.target].height < NPC.Center.Y) {
                            NPC.velocity.Y -= 1.25f;
                        }
                        if (Main.player[NPC.target].position.Y + Main.player[NPC.target].height < NPC.Center.Y - 40f) {
                            NPC.velocity.Y -= 1.5f;
                        }
                        if (Main.player[NPC.target].position.Y + Main.player[NPC.target].height < NPC.Center.Y - 80f) {
                            NPC.velocity.Y -= 1.75f;
                        }
                        if (Main.player[NPC.target].position.Y + Main.player[NPC.target].height < NPC.Center.Y - 120f) {
                            NPC.velocity.Y -= 2f;
                        }
                        if (Main.player[NPC.target].position.Y + Main.player[NPC.target].height < NPC.Center.Y - 160f) {
                            NPC.velocity.Y -= 2.25f;
                        }
                        if (Main.player[NPC.target].position.Y + Main.player[NPC.target].height < NPC.Center.Y - 200f) {
                            NPC.velocity.Y -= 2.5f;
                        }
                        if (!Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1)) {
                            NPC.velocity.Y -= 2f;
                        }
                        NPC.velocity.X = 12 * NPC.direction;
                        NPC.ai[2] += 1f;
                        NPC.netUpdate = true;
                    }
                } else {
                    NPC.velocity.X *= 0.98f;
                    if (NPC.direction < 0 && NPC.velocity.X > -8f) {
                        NPC.velocity.X = -8f;
                    }
                    if (NPC.direction > 0 && NPC.velocity.X < 8f) {
                        NPC.velocity.X = 8f;
                    }
                }
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] >= 3f && NPC.velocity.Y == 0f) {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
            } else if (NPC.ai[0] == 7f) {
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.defense = 9999;
                NPC.noTileCollide = true;
                NPC.alpha += 7;
                if (NPC.alpha > 255) {
                    NPC.alpha = 255;
                }
                NPC.velocity.X *= 0.98f;
            }
        }

        public override void FindFrame(int frameHeight) {
            if (NPC.velocity.Y == 0f) {
                NPC.spriteDirection = NPC.direction;
            }
            if (NPC.ai[0] == 0f || NPC.ai[0] == 7f) {
                NPC.rotation = 0f;
                NPC.frameCounter = 0.0;
                NPC.frame.Y = 0;
            } else if (NPC.ai[0] == 1f) {
                NPC.rotation = 0f;
                NPC.frameCounter = 0.0;
                int num382 = 6;
                if (NPC.ai[1] < num382) {
                    NPC.frame.Y = frameHeight;
                } else if (NPC.ai[1] < num382 * 2) {
                    NPC.frame.Y = frameHeight * 2;
                } else if (NPC.ai[1] < num382 * 3) {
                    NPC.frame.Y = frameHeight * 3;
                } else if (NPC.ai[1] < num382 * 4) {
                    NPC.frame.Y = frameHeight * 4;
                }
            } else if (NPC.ai[0] == 2f || NPC.ai[0] == 6f) {
                NPC.rotation = 0f;
                if (NPC.velocity.Y == 0f) {
                    NPC.frame.Y = frameHeight * 8;
                } else {
                    NPC.frame.Y = frameHeight * 9;
                    NPC.frameCounter = 0.0;
                }
            } else if (NPC.ai[0] == 3f) {
                NPC.rotation = 0f;
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter > 6.0) {
                    NPC.frameCounter = 0.0;
                    if (NPC.frame.Y > frameHeight * 5) {
                        NPC.frame.Y -= frameHeight;
                    }
                }
            } else if (NPC.ai[0] == 4f || NPC.ai[0] == 5f) {
                if (NPC.ai[0] == 4f && NPC.ai[2] == 1f) {
                    NPC.rotation = 0f;
                }
                NPC.frame.Y = frameHeight * 9;
                NPC.frameCounter = 0.0;
            } else if (NPC.ai[0] == 4.1f) {
                NPC.rotation = 0f;
                if (NPC.frame.Y > frameHeight * 4) {
                    NPC.frameCounter = 0.0;
                }
                NPC.frameCounter += 1.0;
                int num384 = 4;
                if (NPC.frameCounter < num384) {
                    NPC.frame.Y = frameHeight * 4;
                    return;
                }
                if (NPC.frameCounter < num384 * 2) {
                    NPC.frame.Y = frameHeight * 3;
                    return;
                }
                if (NPC.frameCounter < num384 * 3) {
                    NPC.frame.Y = frameHeight * 2;
                    return;
                }
                if (NPC.frameCounter < num384 * 4) {
                    NPC.frame.Y = frameHeight * 1;
                    return;
                }

                NPC.frame.Y = frameHeight * 3;
                if (NPC.frameCounter >= num384 * 6 - 1) {
                    NPC.frameCounter = 0.0;
                }
            }
        }

        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
                new FlavorTextBestiaryInfoElement("Mods.EverythingRenewableNow.Bestiary." + MimicName),
            ]);
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
