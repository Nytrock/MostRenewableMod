using DuckLib;
using EverythingRenewableNow.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EverythingRenewableNow.Content.NPCs.Boulder {
    public class StatueMimic : ModNPC {
        private static SoundStyle _laugh;
        private static SoundStyle _scare;
        private static SoundStyle _jump;

        public override void Load() {
            _laugh = this.CreateSound("Laugh", 3);
            _jump = this.CreateSound("Jump");
            _scare = this.CreateSound("Scare");
        }

        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard,
                LocalizationUtils.GetBestiaryText("Bestiary.StatueMimic"),
            ]);
        }

        public override void SetDefaults() {
            NPC.lavaImmune = true;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
            NPC.width = 28;
            NPC.height = 38;
            NPC.aiStyle = -1;
            NPC.damage = 40;
            NPC.defense = 10;
            NPC.lifeMax = 400;
            NPC.HitSound = this.CreateSound("Hit");
            NPC.DeathSound = this.CreateSound("Death");
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            npcLoot.Add(new StatueMimicItemDropRule());
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
            Texture2D value18 = TextureAssets.Tile[105].Value;
            Vector2 position10 = NPC.Center - screenPos;
            position10.Y -= 3f;
            float rotation9 = NPC.rotation;
            Color alpha2 = NPC.GetAlpha(drawColor);
            SpriteEffects effects = SpriteEffects.None;
            int num74 = (int)NPC.ai[1];
            int num75 = 55;
            int num76 = 3;
            int num77 = num74 % num75;
            int num78 = num74 / num75;
            if (NPC.direction == 1) {
                num78 += num76;
            }
            for (int num79 = 0; num79 < 2; num79++) {
                for (int num80 = 0; num80 < 3; num80++) {
                    int x = num77 * 36 + num79 * 18;
                    int y2 = num78 * 54 + num80 * 18;
                    Rectangle value19 = new(x, y2, 16, 16);
                    Vector2 origin8 = new Vector2(1f - (float)num79, 1.5f - (float)num80) * 16f;
                    spriteBatch.Draw(value18, position10, value19, alpha2, rotation9, origin8, 1f, effects, 0f);
                }
            }
            return false;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            bool baseConditions = NPC.downedBoss3
                && spawnInfo.Player.ZoneGraveyard
                && RollBadLuckExtreme(spawnInfo.Player.luck, 15) == 0
                && !NPC.AnyNPCs(Type);

            if (!baseConditions)
                return 0f;

            int x = spawnInfo.SpawnTileX;
            int y = spawnInfo.SpawnTileY;
            Main.NewText(IsThisAGoodPlaceForAStatueMimic(x, y));
            if (IsThisAGoodPlaceForAStatueMimic(x, y))
                return 1f;
            return 0f;
        }

        public override bool CheckActive() {
            return NPC.ai[0] == 0f;
        }

        public static int RollBadLuckExtreme(float luck, int range) {
            if (luck > 0f && Main.rand.NextFloat() < luck) {
                return Main.rand.Next(range * 10);
            }
            if (luck < 0f && Main.rand.NextFloat() < 0f - luck) {
                return Main.rand.Next(range / 10);
            }
            return Main.rand.Next(range);
        }

        public override void HitEffect(NPC.HitInfo hit) {
            for (int m = 0; m < 10; m++) {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Stone, hit.HitDirection, -1f, 0, Scale: 1.2f);
            }
        }

        public override void AI() {
            if (NPC.noTileCollide) {
                NPC.wet = false;
            }
            if (NPC.ai[0] == 0f) {
                NPC.timeLeft = 60;
                NPC.TargetClosest();
                NPC.direction = NPC.oldDirection;
                if (Main.netMode == NetmodeID.MultiplayerClient) {
                    return;
                }
                if (NPC.localAI[1] == 0f) {
                    NPC.localAI[1] = 1f;
                    NPC.ai[1] = GetRandomStatueStyleForStatueMimics();
                    NPC.netUpdate = true;
                }
                NPC.localAI[0] += 1f;
                Vector2 vector = NPC.Center - Main.player[NPC.target].Center;
                if (!Main.player[NPC.target].dead && vector.Length() < 96f) {
                    if (Collision.CanHit(NPC, Main.player[NPC.target])) {
                        NPC.ai[0] = 1f;
                        NPC.netUpdate = true;
                    }
                } else {
                    if (!(NPC.localAI[0] >= 10f)) {
                        return;
                    }
                    NPC.localAI[0] = 0f;
                    if (CanAnyPlayerSeeThisTile((int)NPC.Center.X / 16, (int)NPC.Bottom.Y / 16)) {
                        return;
                    }
                    int num = 0;
                    int num3 = -1;
                    int num2;
                    if (Math.Abs(Main.player[NPC.target].Center.X - NPC.Center.X) < (float)(NPC.sWidth / 2)) {
                        num2 = (int)((Main.player[NPC.target].Center.X - (float)(NPC.sWidth / 2) - (float)num) / 16f);
                        num3 = (int)((Main.player[NPC.target].Center.X + (float)(NPC.sWidth / 2) + (float)num) / 16f);
                    } else if (Main.player[NPC.target].Center.X < NPC.Center.X) {
                        num2 = (int)((Main.player[NPC.target].Center.X + (float)(NPC.sWidth / 2) + (float)num) / 16f);
                        num3 = (int)(NPC.Center.X / 16f);
                    } else {
                        num2 = (int)(NPC.Center.X / 16f);
                        num3 = (int)((Main.player[NPC.target].Center.X - (float)(NPC.sWidth / 2) - (float)num) / 16f);
                    }
                    int num4 = -1;
                    int num5 = -1;
                    if (Math.Abs(Main.player[NPC.target].Center.Y - NPC.Center.Y) < (float)(NPC.sHeight / 2)) {
                        num4 = (int)((Main.player[NPC.target].Center.Y - (float)(NPC.sHeight / 2) - (float)num) / 16f);
                        num5 = (int)((Main.player[NPC.target].Center.Y + (float)(NPC.sHeight / 2) + (float)num) / 16f);
                    } else if (Main.player[NPC.target].Center.Y < NPC.Center.Y) {
                        num4 = (int)((Main.player[NPC.target].Center.Y + (float)(NPC.sHeight / 2) + (float)num) / 16f);
                        num5 = (int)(NPC.Center.Y / 16f);
                    } else {
                        num4 = (int)(NPC.Center.Y / 16f);
                        num5 = (int)((Main.player[NPC.target].Center.Y - (float)(NPC.sHeight / 2) - (float)num) / 16f);
                    }
                    for (int num6 = 10; num6 > 0; num6--) {
                        int num7 = Main.rand.Next(num2, num3);
                        int num8 = Main.rand.Next(num4, num5);
                        bool flag = false;
                        for (int i = 0; i < 10; i++) {
                            if (WorldGen.SolidTile2(num7, num8) && WorldGen.SolidTile2(num7 + 1, num8)) {
                                if (!CanAnyPlayerSeeThisTile(num7 + 1, num8)) {
                                    flag = true;
                                }
                                break;
                            }
                            num8++;
                        }
                        if (flag && IsThisAGoodPlaceForAStatueMimic(num7, num8)) {
                            Vector2 vector2 = new(num7 * 16 + 16 - NPC.width / 2, num8 * 16 - NPC.height);
                            Vector2 vector3 = NPC.position - Main.player[NPC.target].position;
                            if ((vector2 - Main.player[NPC.target].position).Length() < vector3.Length()) {
                                if (vector2.X < NPC.position.X) {
                                    NPC.direction = -1;
                                } else if (vector2.X > NPC.position.X) {
                                    NPC.direction = 1;
                                }
                                NPC.position = vector2;
                                NPC.netUpdate = true;
                                break;
                            }
                        }
                    }
                }
                return;
            }
            NPC.immortal = false;
            NPC.dontTakeDamage = false;
            bool flag2 = false;
            if (NPC.localAI[2] == 0f) {
                NPC.localAI[2] = 1f;
                SoundEngine.PlaySound(_scare, NPC.Center);
            }
            if (NPC.velocity.Y > 0f && NPC.Top.Y > Main.player[NPC.target].Bottom.Y) {
                if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) {
                    flag2 = true;
                    SoundEngine.PlaySound(_jump, NPC.Center);
                    if (!((NPC.Center - Main.player[NPC.target].Center).Length() > 480f)) {
                    }
                }
            } else if (NPC.velocity.Y == 0f) {
                int num9 = 20;
                if (NPC.ai[2] == (float)num9) {
                    SoundEngine.PlaySound(_jump, NPC.Center);
                }
                NPC.velocity.X = 0f;
                NPC.ai[2] -= 1f;
                if (NPC.ai[2] <= 0f) {
                    NPC.ai[2] = num9;
                    flag2 = true;
                    NPC.netUpdate = true;
                }
            }
            bool flag3 = false;
            if (NPC.Right.X >= Main.player[NPC.target].Left.X && NPC.Left.X <= Main.player[NPC.target].Right.X) {
                if (NPC.Bottom.Y < Main.player[NPC.target].Top.Y) {
                    NPC.velocity.X *= 0.75f;
                    if (NPC.velocity.Y < 0f) {
                        NPC.velocity.Y *= 0.75f;
                    }
                }
                flag3 = true;
            }
            if (Main.player[NPC.target].dead) {
                if (NPC.ai[3] == 0f) {
                    SoundEngine.PlaySound(_laugh, NPC.Center);
                    NPC.ai[3] = 300f;
                }
                if (NPC.ai[3] > 200f) {
                    flag3 = true;
                    NPC.ai[3] -= 1f;
                }
            } else if (NPC.ai[3] > 0f) {
                NPC.ai[3] -= 1f;
            }
            if (flag2) {
                NPC.noTileCollide = true;
                NPC.TargetClosest();
                float num10 = (NPC.Bottom.Y - Main.player[NPC.target].Bottom.Y) / 40f;
                num10 = ((!Main.player[NPC.target].dead) ? Terraria.Utils.Clamp(num10, 0f, 10f) : 0f);
                NPC.velocity.Y = -9.01f - num10;
                float num11 = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) / 50f;
                if (num11 > 12f) {
                    num11 = 12f;
                }
                NPC.velocity.X = (4f + num11) * (float)NPC.direction;
                NPC.netUpdate = true;
            } else {
                if (NPC.velocity.Y == 0f) {
                    return;
                }
                if (Main.player[NPC.target].dead && flag3 && NPC.Bottom.Y < Main.player[NPC.target].Bottom.Y) {
                    NPC.velocity.Y = 16f;
                } else if (flag3 && NPC.Bottom.Y < Main.player[NPC.target].Top.Y) {
                    NPC.velocity.Y = 16f;
                } else {
                    if (NPC.velocity.Y > 0f) {
                        NPC.velocity.Y += NPC.gravity;
                    }
                    if (!Main.player[NPC.target].dead) {
                        if (NPC.direction > 0 && NPC.Center.X > Main.player[NPC.target].Center.X) {
                            NPC.velocity *= 0.96f;
                        }
                        if (NPC.direction < 0 && NPC.Center.X < Main.player[NPC.target].Center.X) {
                            NPC.velocity *= 0.96f;
                        }
                    }
                }
                if (NPC.velocity.Y < 0f) {
                    NPC.noTileCollide = true;
                } else if (flag3 && NPC.Bottom.Y < Main.player[NPC.target].Top.Y) {
                    NPC.noTileCollide = true;
                } else if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) {
                    NPC.noTileCollide = false;
                }
            }
        }

        private static bool CanAnyPlayerSeeThisTile(int x, int y) {
            int num = 32;
            Rectangle rectangle = new(x * 16 - NPC.sWidth / 2 - num / 2, y * 16 - NPC.sHeight / 2 - num / 2, NPC.sWidth + num, NPC.sHeight + num);
            for (int i = 0; i < 255; i++) {
                if (Main.player[i].active && Main.player[i].getRect().Intersects(rectangle)) {
                    return true;
                }
            }
            return false;
        }

        private static bool IsThisAGoodPlaceForAStatueMimic(int x, int y) {
            if (WorldGen.SolidTile2(x, y) && WorldGen.SolidTile2(x + 1, y) && !Main.tile[x, y - 1].HasTile && !Main.tile[x, y - 2].HasTile && !Main.tile[x, y - 3].HasTile && !Main.tile[x + 1, y - 1].HasTile && !Main.tile[x + 1, y - 2].HasTile && !Main.tile[x + 1, y - 3].HasTile)
                return true;
            return false;
        }

        private static float GetRandomStatueStyleForStatueMimics() {
            int num = WorldGen.genRand.Next(83);
            while (num >= 43 && num <= 49) {
                num = WorldGen.genRand.Next(83);
            }
            return num;
        }

        private class StatueMimicItemDropRule : IItemDropRule {
            public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

            public StatueMimicItemDropRule() {
                this.ChainedRules = new();
            }

            public bool CanDrop(DropAttemptInfo info) {
                if (info.npc.ai[1] > 0f) {
                    return info.npc.ai[1] < ItemID.Count;
                }
                return false;
            }

            public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo) {
                Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
            }

            public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info) {
                int itemId = StatueStyleToItem((int)info.npc.ai[1]);
                CommonCode.DropItem(info, itemId, 1);
                return new ItemDropAttemptResult {
                    State = ItemDropAttemptResultState.Success
                };
            }

            public static int StatueStyleToItem(int style) {
                switch (style) {
                    case 0:
                        style = 360;
                        break;
                    case 1:
                        style = 52;
                        break;
                    case 43:
                        style = 1152;
                        break;
                    case 44:
                        style = 1153;
                        break;
                    case 45:
                        style = 1154;
                        break;
                    case 46:
                        style = 1408;
                        break;
                    case 47:
                        style = 1409;
                        break;
                    case 48:
                        style = 1410;
                        break;
                    case 49:
                        style = 1462;
                        break;
                    case 50:
                        style = 2672;
                        break;
                    case 51:
                    case 52:
                    case 53:
                    case 54:
                    case 55:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                    case 62:
                        style = 3651 + style - 51;
                        break;
                    default:
                        style = ((style >= 63 && style <= 75) ? (3708 + style - 63) : (style switch {
                            76 => 4397,
                            77 => 4360,
                            78 => 4342,
                            79 => 4466,
                            80 => 5317,
                            81 => 5318,
                            82 => 5319,
                            _ => 438 + style - 2,
                        }));
                        break;
                }
                return style;
            }
        }
    }
}
