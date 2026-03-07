using EverythingRenewableNow.Content.Items.Boulder;
using EverythingRenewableNow.Utils;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.Boulder {
    public class WaterboltMimic : ModNPC {
        private static readonly Point[] _nearbyBooks = new Point[20];

        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = 30;
        }

        public override void SetDefaults() {
            NPC.width = 44;
            NPC.height = 44;
            NPC.aiStyle = -1;
            NPC.damage = 20;
            NPC.defense = 4;
            NPC.lifeMax = 60;
            NPC.HitSound = SoundID.DD2_BookStaffCast;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = 150f;
            NPC.knockBackResist = 0.2f;

            Banner = Type;
            BannerItem = ModContent.ItemType<WaterboltMimicBanner>();
        }

        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
                LocalizationUtils.GetBestiaryText("Bestiary.WaterboltMimic"),
            ]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            npcLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.WaterBolt, 40));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            if (!spawnInfo.Player.ZoneDungeon && !Main.rand.NextBool(8))
                return 0f;

            if (FindNearbyBook(new Point(spawnInfo.SpawnTileX - 16, spawnInfo.SpawnTileY - 16), 32, 32, out Point bookPosition, closestBook: true, checkPlayerScreenRanges: true)) {
                spawnInfo.SpawnTileX = bookPosition.X;
                spawnInfo.SpawnTileY = bookPosition.Y;
                return 1f;
            }

            return 0f;
        }

        private static bool FindNearbyBook(Point searchPosition, int searchWidth, int searchHeight, out Point bookPosition, bool closestBook = false, bool checkPlayerScreenRanges = false) {
            bookPosition = Point.Zero;
            Point[] nearbyBooks = _nearbyBooks;
            int num = nearbyBooks.Length;
            int num2 = 0;
            int num3 = Math.Max(0, searchPosition.X);
            int num4 = Math.Min(searchPosition.X + searchWidth, Main.maxTilesX);
            int num5 = Math.Max(0, searchPosition.Y);
            int num6 = Math.Min(searchPosition.Y + searchHeight, Main.maxTilesY);
            float num7 = 9999999f;
            Vector2 vector = searchPosition.ToVector2();
            Vector2 vector2 = vector;
            for (int i = num5; i < num6; i++) {
                for (int j = num3; j < num4; j++) {
                    Tile tile = Main.tile[j, i];
                    if (!tile.HasTile || tile.TileType != TileID.Books) {
                        continue;
                    }
                    Vector2 vector3 = new(j, i);
                    if (checkPlayerScreenRanges && !CheckNotSpawningOnScreen((int)vector3.X, (int)vector3.Y)) {
                        continue;
                    }
                    float num8 = vector3.Distance(vector2);
                    if (closestBook && num8 < num7) {
                        num7 = num8;
                        vector = vector3;
                        continue;
                    }
                    nearbyBooks[num2++] = new Point(j, i);
                    if (num2 >= num) {
                        break;
                    }
                }
            }
            if (closestBook) {
                bookPosition = vector.ToPoint();
                if (vector.X == vector2.X) {
                    return vector.Y != vector2.Y;
                }
                return true;
            }
            if (num2 == 0) {
                return false;
            }
            bookPosition = nearbyBooks[Main.rand.Next(num2)];
            return true;
        }

        public static bool CheckNotSpawningOnScreen(int spawnTileX, int spawnTileY) {
            Rectangle rectangle = new(spawnTileX * 16, spawnTileY * 16, 16, 16);
            int num = NPC.sWidth / 2;
            int num2 = NPC.sHeight / 2;
            for (int i = 0; i < 255; i++) {
                Player player = Main.player[i];
                if (player.active) {
                    Rectangle value = new((int)(player.Center.X - (float)num - (float)NPC.safeRangeX), (int)(player.Center.Y - (float)num2 - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                    if (rectangle.Intersects(value)) {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit) {
            if (NPC.life > 0) {
                int num707 = Math.Max(1, Math.Min(5, (int)(hit.Damage / (double)NPC.lifeMax * 3.0)));
                for (int num708 = 0; num708 < num707; num708++) {
                    Vector2 vector29 = NPC.Center + new Vector2((float)(-NPC.width + (int)((float)NPC.width * Main.rand.NextFloat())) * 0.5f, (float)(-NPC.height + (int)((float)NPC.height * Main.rand.NextFloat())) * 0.5f);
                    Gore.NewGore(NPC.GetSource_FromThis(), vector29, NPC.velocity, GoreID.PageScrap, NPC.scale);
                }
                return;
            }

            for (int num709 = 0; num709 < 5; num709++) {
                Vector2 vector30 = NPC.Center + new Vector2((float)(-NPC.width + (int)((float)NPC.width * Main.rand.NextFloat())) * 0.5f, (float)(-NPC.height + (int)((float)NPC.height * Main.rand.NextFloat())) * 0.5f);
                Gore.NewGore(NPC.GetSource_FromThis(), vector30, NPC.velocity, GoreID.PageScrap, NPC.scale);
                if (num709 < 3) {
                    vector30 = NPC.Center + new Vector2((float)(-NPC.width + (int)((float)NPC.width * Main.rand.NextFloat())) * 0.5f, (float)(-NPC.height + (int)((float)NPC.height * Main.rand.NextFloat())) * 0.5f);
                    Gore.NewGore(NPC.GetSource_FromThis(), vector30, NPC.velocity, GoreID.Pages, NPC.scale);
                }
            }
        }

        public override void FindFrame(int frameHeight) {
            int num3 = NPC.frame.Y / frameHeight;
            if (NPC.ai[3] == 3f) {
                num3 = 0;
                NPC.frameCounter = 0.0;
            } else if (NPC.ai[3] == 4f) {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5.0) {
                    NPC.frameCounter = 0.0;
                    num3++;
                    if (num3 > 16) {
                        num3 = 16;
                    }
                }
            } else if (NPC.ai[3] == 2f) {
                float num4 = NPC.ai[2];
                if (num4 == 0f) {
                    num3 = 17;
                }
                if (num3 < 17 || num3 > 23) {
                    num3 = 17;
                }
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5.0) {
                    NPC.frameCounter = 0.0;
                    num3++;
                    if (num4 < 40f && num3 > 19) {
                        num3 = 19;
                    }
                    if (num3 > 23) {
                        num3 = 23;
                    }
                }
            } else if (NPC.ai[3] == 1f) {
                if (num3 < 24 || num3 > 29) {
                    num3 = 24;
                }
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5.0) {
                    NPC.frameCounter = 0.0;
                    num3++;
                    if (num3 > 29) {
                        num3 = 29;
                    }
                }
            } else {
                if (num3 < 17 || num3 > 23) {
                    num3 = 17;
                }
                NPC.frameCounter++;
                if (NPC.frameCounter >= 6.0) {
                    NPC.frameCounter = 0.0;
                    num3 = num3 switch {
                        17 => 22,
                        22 => 23,
                        _ => 17,
                    };
                }
                if (num3 >= Main.npcFrameCount[NPC.type]) {
                    num3 = 0;
                }
            }
            NPC.frame.Y = num3 * frameHeight;
        }

        public override void AI() {
            bool flag9 = true;
            bool flag10 = flag9;
            bool flag11 = !flag9;
            bool flag12 = flag9;
            float num145 = 1f;
            float num146 = 0.011f;
            int num147 = 600;
            int num148 = num147 + 50;
            int num149 = 80;
            if (flag11 || !Main.player[NPC.target].active || Main.player[NPC.target].dead) {
                NPC.TargetClosest();
            }
            Vector2 center4 = NPC.Center;
            float num150 = Main.player[NPC.target].Center.X - center4.X;
            float num151 = Main.player[NPC.target].Center.Y - center4.Y;
            float num152 = (float)Math.Sqrt(num150 * num150 + num151 * num151);
            float num153 = num152;
            if (NPC.ai[3] != 3f) {
                NPC.ai[1]++;
            }
            bool flag13 = NPC.ai[2] >= 0f && NPC.ai[3] == 2f;
            bool flag14 = NPC.ai[2] >= 0f && NPC.ai[3] == 1f;
            bool flag15 = NPC.ai[1] > (float)num147;
            bool flag16 = NPC.ai[1] < -30f;
            bool flag17 = flag10 && !flag13 && !flag14 && !flag15 && flag16;
            if (NPC.ai[3] == 3f) {
                NPC.spriteDirection = 1;
                NPC.rotation = 0f;
                NPC.velocity = Vector2.Zero;
                NPC.knockBackResist = 0f;
                if (NPC.justHit) {
                    NPC.ai[3] = 4f;
                    NPC.netUpdate = true;
                }
                return;
            }
            if (NPC.ai[3] == 4f) {
                NPC.spriteDirection = 1;
                NPC.rotation = 0f;
                NPC.velocity = Vector2.Zero;
                NPC.knockBackResist = 1f;
                if (NPC.ai[1] > (float)num149) {
                    NPC.ai[1] = 0f;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
                return;
            }
            if (flag9) {
                NPC.knockBackResist = 0.2f;
                NPC.dontTakeDamage = NPC.ai[3] == 4f;
            }
            if (!flag13) {
                if (flag15) {
                    num146 *= 8f;
                    num145 = 4f;
                    if (NPC.ai[1] > (float)num148) {
                        NPC.ai[1] = 0f;
                    }
                    NPC.netUpdate = true;
                } else if (flag10 && num153 < 100f && NPC.ai[1] >= 0f) {
                    if (NPC.ai[1] != -59f) {
                        NPC.netUpdate = true;
                    }
                    NPC.ai[1] = -60f;
                } else if (num153 < 250f) {
                    NPC.ai[0] += 0.9f;
                    if (NPC.ai[0] > 0f) {
                        NPC.velocity.Y += 0.019f;
                    } else {
                        NPC.velocity.Y -= 0.019f;
                    }
                    if (NPC.ai[0] < -100f || NPC.ai[0] > 100f) {
                        NPC.velocity.X += 0.019f;
                    } else {
                        NPC.velocity.X -= 0.019f;
                    }
                    if (NPC.ai[0] > 200f) {
                        NPC.ai[0] = -200f;
                        NPC.netUpdate = true;
                    }
                }
            }
            if (flag17) {
                num145 = 8f;
                num146 = 0.25f;
            } else if (num153 > 350f) {
                num145 = 5f;
                num146 = 0.3f;
            } else if (num153 > 300f) {
                num145 = 3f;
                num146 = 0.2f;
            } else if (num153 > 250f) {
                num145 = 1.5f;
                num146 = 0.1f;
            }
            num152 = num145 / num152;
            num150 *= num152;
            num151 *= num152;
            float num154 = num150;
            float num155 = num151;
            if (flag17) {
                num150 *= -1f;
                num151 *= -1f;
            }
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead) {
                num150 = (float)NPC.direction * num145 / 2f;
                num151 = (0f - num145) / 2f;
            }
            if (flag13) {
                float num156 = NPC.ai[2];
                if (num156 < 10f) {
                    NPC.velocity *= 0.5f;
                    if (num156 >= 18f) {
                        NPC.velocity = Vector2.Zero;
                    }
                } else {
                    num146 = 14f;
                    Vector2 v = ((NPC.velocity.Length() < 0.1f) ? (Main.player[NPC.target].Center - center4) : NPC.velocity);
                    v = v.SafeNormalize(Vector2.UnitY) * num146;
                    NPC.velocity = v;
                }
            } else {
                if (NPC.velocity.X < num150) {
                    NPC.velocity.X += num146;
                } else if (NPC.velocity.X > num150) {
                    NPC.velocity.X -= num146;
                }
                if (NPC.velocity.Y < num151) {
                    NPC.velocity.Y += num146;
                } else if (NPC.velocity.Y > num151) {
                    NPC.velocity.Y -= num146;
                }
            }
            if (!flag13) {
                if (num154 > 0f) {
                    NPC.spriteDirection = -1;
                    NPC.rotation = (float)Math.Atan2(num155, num154);
                }
                if (num154 < 0f) {
                    NPC.spriteDirection = 1;
                    NPC.rotation = (float)Math.Atan2(num155, num154) + (float)Math.PI;
                }
                if (flag12) {
                    NPC.spriteDirection *= -1;
                }
            }

            if (NPC.justHit) {
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
            }
            center4 = NPC.Center + new Vector2(0f, 10f);
            num150 = Main.player[NPC.target].Center.X - center4.X;
            num151 = Main.player[NPC.target].Center.Y - center4.Y;
            num152 = (float)Math.Sqrt(num150 * num150 + num151 * num151);
            int num164 = 500;
            int num165 = 100;
            int num166 = 300;
            int num167 = 120;
            int num168 = 30;
            int num169 = 60;
            int num170 = 17;
            int num171 = 300;
            bool flag18 = num152 >= (float)num165 && num152 <= (float)num166 && NPC.ai[2] >= 0f && (NPC.ai[3] == 0f || NPC.ai[3] == 2f);
            bool flag19 = num152 <= (float)num164 && NPC.ai[2] >= 0f && (NPC.ai[3] == 0f || NPC.ai[3] == 1f);

            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;
            if (flag18 && (!flag19 || Main.rand.NextBool(3))) {
                NPC.ai[2]++;
                if (NPC.ai[3] == 0f) {
                    if (NPC.ai[2] > (float)num167) {
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 2f;
                        NPC.netUpdate = true;
                    }
                } else if (NPC.ai[3] == 2f && NPC.ai[2] > (float)num169) {
                    NPC.ai[2] = -num171;
                    NPC.ai[3] = 0f;
                    NPC.netUpdate = true;
                }
            } else if (flag19) {
                NPC.ai[2]++;
                if (NPC.ai[3] == 0f) {
                    if (NPC.ai[2] > (float)num167) {
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 1f;
                        NPC.netUpdate = true;
                    }
                } else {
                    if (NPC.ai[3] != 1f) {
                        return;
                    }
                    if (NPC.ai[2] > (float)num168) {
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                        NPC.netUpdate = true;
                    }
                    if (NPC.ai[2] == (float)num170) {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                        if (Main.netMode != NetmodeID.MultiplayerClient) {
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)center4.X, (int)center4.Y, 33);
                        }
                    }
                }
            } else {
                NPC.ai[2]++;
                if (NPC.ai[2] > 0f) {
                    NPC.ai[2] = 0f;
                }
                if (NPC.ai[3] != 0f) {
                    NPC.netUpdate = true;
                }
                NPC.ai[3] = 0f;
            }
        }
    }
}
