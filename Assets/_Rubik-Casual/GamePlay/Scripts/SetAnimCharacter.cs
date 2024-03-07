using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTPackage.Functions;
using RubikCasual.Battle.Calculate;
using RubikCasual.Battle.UI;
using RubikCasual.Tool;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace RubikCasual.Battle
{
    [Serializable]
    public class NameAnim
    {
        public static string Anim_Character_Attack = "Attack",
        Anim_Character_Attacked = "Attacked",
        Anim_Character_Idle = "Idle",
        Anim_Character_Die = "Die",
        Anim_Character_Skill = "SkillCast";
    }
    [Serializable]
    public class NameLayer
    {
        public static string Layer_Attack = "Character_Attack",
        Layer_Attacked = "Character_Attacked",
        Layer_Character = "Character",
        Layer_ShowPopup = "ShowPopup";
    }
    public class SetAnimCharacter : MonoBehaviour
    {
        // private bool isAnimationCompleteHandled = false;
        private MapBattleController mapBattleController;
        private List<GameObject> lsSlotGbEnemy;
        public static SetAnimCharacter instance;
        protected void Awake()
        {
            instance = this;
            LoadData();
        }
        void LoadData()
        {
            if (BattleController.instance.lsSlotGbEnemy.Count == 0)
            {
                Debug.Log("lsSlotGbEnemy null");
            }
            else
            {
                lsSlotGbEnemy = BattleController.instance.lsSlotGbEnemy;
            }
            if (BattleController.instance.mapBattleController == null)
            {
                Debug.Log("mapBattleController null");
            }
            else
            {
                mapBattleController = BattleController.instance.mapBattleController;
            }
        }
        public void BossUseSkill(CharacterInBattle EnemyInBattle, MapBattleController dameSlotTxtController, List<GameObject> lsSlotGbHero, float durationsTxtDame)
        {
            // EnemyInBattle.skeletonCharacterAnimation.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attack;
            EnemyInBattle.skeletonCharacterAnimation.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Skill, false);
            EnemyInBattle.isUseSkill = true;
            EnemyInBattle.skeletonCharacterAnimation.AnimationState.Complete += delegate
            {
                EnemyInBattle.isUseSkill = false;
                // EnemyInBattle.cooldownSkillBar.value = 0;
                if (EnemyInBattle.skeletonCharacterAnimation.AnimationName != NameAnim.Anim_Character_Idle)
                {
                    EnemyInBattle.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Idle;
                }
            };

            for (int i = 0; i < lsSlotGbHero.Count; i++)
            {
                int index = i;
                if (lsSlotGbHero[index] != null && lsSlotGbHero[index].GetComponent<CharacterInBattle>() != null)
                {
                    CharacterInBattle HeroInBattle = lsSlotGbHero[index].GetComponent<CharacterInBattle>();
                    SkeletonAnimation AnimHero = HeroInBattle.skeletonCharacterAnimation;
                    AnimHero.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attacked;

                    AnimHero.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                    AnimHero.AnimationState.Complete += delegate
                    {
                        if (AnimHero.AnimationName != NameAnim.Anim_Character_Idle)
                        {
                            AnimHero.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Character;
                            AnimHero.AnimationName = NameAnim.Anim_Character_Idle;
                        }
                    };


                    float OldHp = HeroInBattle.HpNow;
                    Calculator.CalculateHealth(EnemyInBattle, HeroInBattle);

                    Transform PosTxt = dameSlotTxtController.lsPosHeroSlot.lsPosCharacterSlot[index].transform;
                    int lossHp = (int)(OldHp - HeroInBattle.HpNow);
                    FuntionTimeDelay.SpawnTxtDame(UIGamePlay.instance.TxtDame, PosTxt, lossHp, durationsTxtDame);

                    CheckHpEnemy(lsSlotGbHero[index]);
                }
            }

        }
        public void CharacterUseSkill(CharacterInBattle CharacterAttack, MapBattleController dameSlotTxtController, List<GameObject> lsSlotGbEnemy, float durationsTxtDame)
        {
            // CharacterAttack.skeletonCharacterAnimation.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attack;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Skill, false);
            CharacterAttack.isUseSkill = true;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.Complete += delegate
            {
                CharacterAttack.isUseSkill = false;
                if (CharacterAttack.skeletonCharacterAnimation.AnimationName != NameAnim.Anim_Character_Idle)
                {
                    CharacterAttack.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Idle;
                }
            };
            int numberSlotBoss = 2;
            if (lsSlotGbEnemy[numberSlotBoss] != null && lsSlotGbEnemy[numberSlotBoss].GetComponent<CharacterInBattle>() != null && lsSlotGbEnemy[numberSlotBoss].GetComponent<CharacterInBattle>().isBoss)
            {
                CharacterInBattle EnemyBossInBattle = lsSlotGbEnemy[numberSlotBoss].GetComponent<CharacterInBattle>();
                SkeletonAnimation AnimEnemy = EnemyBossInBattle.skeletonCharacterAnimation;
                // AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attacked;

                AnimEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                AnimEnemy.AnimationState.Complete += delegate
                {
                    // AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                    if (AnimEnemy.AnimationName != NameAnim.Anim_Character_Idle)
                    {
                        AnimEnemy.AnimationName = NameAnim.Anim_Character_Idle;
                    }
                };

            }
            else
            {
                int count = 0;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < dameSlotTxtController.lsPosEnemySlot[0].lsPosCharacterSlot.Count; j++)
                    {
                        if ((count - CharacterAttack.indexOfSlot) % 5 == 0)
                        {
                            // UnityEngine.Debug.Log(count);
                            if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null && !lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().isBoss)
                            {
                                CharacterInBattle EnemyBossInBattle = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();
                                SkeletonAnimation AnimEnemy = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;

                                // AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attacked;
                                AnimEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                                AnimEnemy.AnimationState.Complete += delegate
                                {
                                    // AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                                    if (AnimEnemy.AnimationName != NameAnim.Anim_Character_Idle)
                                    {
                                        AnimEnemy.AnimationName = NameAnim.Anim_Character_Idle;
                                    }
                                };


                                float OldHp = EnemyBossInBattle.HpNow;
                                Calculator.CalculateHealth(CharacterAttack, EnemyBossInBattle, true);

                                Transform PosTxt = EnemyBossInBattle.healthBar.transform;
                                int lossHp = (int)(OldHp - lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().HpNow);
                                FuntionTimeDelay.SpawnTxtDame(UIGamePlay.instance.TxtDame, PosTxt, lossHp, durationsTxtDame);
                                CheckHpEnemy(lsSlotGbEnemy[count]);
                            }
                        }

                        count++;
                    }
                }
            }
        }
        public void HeroUseSkillTest(CharacterInBattle CharacterAttack)
        {
            UseSkill(CharacterAttack);
        }
        void UseSkill(CharacterInBattle characterInBattle)
        {
            SkeletonAnimation characterClone = characterInBattle.skeletonCharacterAnimation;
            characterClone.AnimationName = NameAnim.Anim_Character_Skill;
            characterClone.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Skill, false);
            characterClone.AnimationState.Complete += delegate
            {
                if (characterClone.AnimationName != NameAnim.Anim_Character_Idle)
                {
                    characterClone.AnimationName = NameAnim.Anim_Character_Idle;
                }
            };

            Data.Waifu.WaifuSkill waifuSkill = new Data.Waifu.WaifuSkill();
            waifuSkill = Data.DataController.instance.characterAssets.GetSkillWaifuSOByIndex(Data.DataController.instance.characterAssets.GetIndexWaifu(characterInBattle.waifuIdentify.ID, characterInBattle.waifuIdentify.SkinCheck));
            int row = waifuSkill.Row;
            int column = waifuSkill.Column;
            int slotCharacter = characterInBattle.indexOfSlot + 1;
            // Debug.Log(row + "/" + column + "/" + slotCharacter);
            int minColumn = 0;
            for (int i = 0; i < 3; i++)
            {
                if (column == 1)
                {
                    minColumn = slotCharacter - 1;
                    column = column + slotCharacter - 1;
                    break;
                }
                else
                {
                    if (slotCharacter == 2 * i + 1 || slotCharacter == 2 * i)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (column == 2 * j + 1 || column == 2 * j)
                            {
                                minColumn = slotCharacter - (j + 1);
                                column = column + slotCharacter - (j + 1);
                                break;
                            }

                        }
                    }
                }
            }
            StartCoroutine(ShowSkill(row, column, minColumn, characterInBattle, waifuSkill, waifuSkill.NumberTurn));
        }
        IEnumerator ShowSkill(int row, int column, int minColumn, CharacterInBattle characterInBattle, Data.Waifu.WaifuSkill waifuSkill, int inTurn = 1)
        {
            yield return new WaitForSeconds(waifuSkill.DurationAttacked);
            int count = 0;
            switch (waifuSkill.typeSkill)
            {
                case CreateSkill.TypeSkill.Wave:
                    for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                    {
                        yield return new WaitForSeconds(waifuSkill.DurationWave);
                        for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                        {
                            if (lsSlotGbEnemy[count] != null)
                            {
                                if (i < row)
                                {
                                    if (j < column && j >= minColumn)
                                    {
                                        SetAttacked(count, characterInBattle);
                                    }
                                }
                            }

                            count++;
                        }
                    }
                    break;

                case CreateSkill.TypeSkill.InTurn:
                    if (inTurn > 0)
                    {
                        for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                        {
                            for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                            {
                                if (lsSlotGbEnemy[count] != null)
                                {
                                    if (i < row)
                                    {
                                        if (j < column && j >= minColumn)
                                        {
                                            SetAttacked(count, characterInBattle);
                                        }
                                    }
                                }
                                count++;
                            }
                        }
                        StartCoroutine(ShowSkill(row, column, minColumn, characterInBattle, waifuSkill, inTurn - 1));
                    }
                    break;

                default:
                    for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                    {
                        for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                        {
                            if (lsSlotGbEnemy[count] != null)
                            {
                                if (i < row)
                                {
                                    if (j < column && j >= minColumn)
                                    {
                                        SetAttacked(count, characterInBattle);
                                    }
                                }
                            }
                            count++;
                        }
                    }
                    break;
            }
        }
        void SetAttacked(int count, CharacterInBattle characterInBattle)
        {
            SkeletonAnimation skeletonEnemy = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            CharacterInBattle CharacterInBattleAttacked = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();
            Calculator.CalculateHealth(characterInBattle, lsSlotGbEnemy[count].GetComponent<CharacterInBattle>(), true);
            skeletonEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);

            skeletonEnemy.AnimationState.Complete += delegate
            {
                if (skeletonEnemy.AnimationName != NameAnim.Anim_Character_Idle)
                {
                    skeletonEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Idle, true);
                    CharacterInBattleAttacked.HpNow = CharacterInBattleAttacked.Hp;
                    CharacterInBattleAttacked.healthBar = SliderTool.ChangeValueSlider(CharacterInBattleAttacked.healthBar, CharacterInBattleAttacked.healthBar.value, CharacterInBattleAttacked.HpNow / (float)CharacterInBattleAttacked.Hp);
                }
            };
        }
        public void CharacterAtackAnimation(GameObject CharacterAttacked, GameObject CharacterAttack, MapBattleController dameSlotTxtController, float durationsTxtDame)
        {
            StartCoroutine(FuntionTimeDelay.MoveBackDelay(CharacterAttack, CharacterAttacked, dameSlotTxtController, durationsTxtDame));
        }

        void CheckHpEnemy(GameObject gbEnemy)
        {
            CharacterInBattle enemyInBattle = gbEnemy.GetComponent<CharacterInBattle>();
            SkeletonAnimation enemyAnim = enemyInBattle.skeletonCharacterAnimation;
            if (enemyInBattle.HpNow == 0)
            {
                enemyInBattle.GetRewardWhenKillEnemy();

                enemyAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Die, false);
                enemyInBattle.isAttack = true;

                enemyAnim.AnimationState.Complete += delegate
                {
                    enemyInBattle.cooldownSkillBar.gameObject.transform.SetParent(enemyInBattle.cooldownAttackBar.transform.parent);
                    enemyInBattle.healthBar.gameObject.transform.SetParent(enemyInBattle.cooldownAttackBar.transform.parent);
                    // UnityEngine.Debug.Log(enemyAnim.skeletonDataAsset.name);

                    UnityEngine.Object.Destroy(gbEnemy);
                };
            }
        }
    }
    [Serializable]
    public class FuntionTimeDelay
    {
        public static IEnumerator MoveBackDelay(GameObject CharacterAttack, GameObject CharacterAttacked, MapBattleController dameSlotTxtController, float durationsTxtDame)
        {

            SkeletonAnimation CharacterAttackAnim = CharacterAttack.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            SkeletonAnimation CharacterAttackedAnim = CharacterAttacked.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            CharacterInBattle CharacterInBattleAttack = CharacterAttack.GetComponent<CharacterInBattle>();
            CharacterInBattle CharacterInBattleAttacked = CharacterAttacked.GetComponent<CharacterInBattle>();

            // CharacterAttackAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attack;
            // CharacterAttackedAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attacked;

            CharacterAttackAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attack, false);
            CharacterInBattleAttack.isAttack = true;
            CharacterAttackAnim.AnimationState.Complete += delegate
            {
                CharacterInBattleAttack.isAttack = false;
                if (CharacterAttackAnim.AnimationName != NameAnim.Anim_Character_Idle)
                {

                    // CharacterAttackAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                    CharacterAttackAnim.AnimationName = NameAnim.Anim_Character_Idle;

                }

            };

            yield return new WaitForSeconds(durationsTxtDame / 3);

            CharacterAttackedAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
            CharacterAttackedAnim.AnimationState.Complete += delegate
            {

                // CharacterAttackedAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;

                if (CharacterAttackedAnim.AnimationName != NameAnim.Anim_Character_Idle)
                {
                    CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Idle;
                }
                // SpineEditorUtilities.ReinitializeComponent(CharacterAttackedAnim);
            };
            // Tăng thanh nộ
            AddValueRage(CharacterAttack, CharacterAttacked);

            float OldHp = CharacterInBattleAttacked.HpNow;
            Calculator.CalculateHealth(CharacterInBattleAttack, CharacterInBattleAttacked);
            if (CharacterInBattleAttacked.txtHealthBar != null)
            {
                CharacterInBattleAttacked.txtHealthBar.text = ((int)CharacterInBattleAttacked.HpNow).ToString() + "/" + CharacterInBattleAttacked.infoWaifuAsset.HP.ToString();
            }


            int lossHp = (int)(OldHp - CharacterInBattleAttacked.HpNow);
            if (OldHp - CharacterInBattleAttacked.HpNow > 0 && OldHp - CharacterInBattleAttacked.HpNow < 1)
            {
                lossHp = 1;
            }

            if (CharacterInBattleAttacked.isEnemy)
            {

                Transform PosTxt = CharacterInBattleAttacked.healthBar.transform;
                PosTxt.position = new Vector3(PosTxt.position.x, PosTxt.position.y, PosTxt.position.z);
                SpawnTxtDame(UIGamePlay.instance.TxtDame, PosTxt, lossHp, durationsTxtDame);
            }
            else
            {
                Transform PosTxt = dameSlotTxtController.lsPosHeroSlot.lsPosCharacterSlot[CharacterInBattleAttacked.indexOfSlot].gameObject.transform;
                SpawnTxtDame(UIGamePlay.instance.TxtDame, PosTxt, lossHp, durationsTxtDame);
            }


            if (CharacterInBattleAttacked.HpNow == 0)
            {
                if (CharacterInBattleAttacked.isEnemy || CharacterInBattleAttacked.isBoss)
                {
                    CharacterInBattleAttacked.GetRewardWhenKillEnemy();
                }
                CharacterInBattleAttacked.isAttack = true;

                CharacterAttackedAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Die, false);

                CharacterInBattleAttacked.cooldownSkillBar.gameObject.transform.SetParent(BattleController.instance.dameSlotTxtController.transform);
                CharacterInBattleAttacked.healthBar.gameObject.transform.SetParent(CharacterInBattleAttacked.cooldownSkillBar.transform);


                CharacterAttackedAnim.AnimationState.Complete += delegate
                {
                    CharacterInBattleAttacked.cooldownSkillBar.gameObject.transform.SetParent(CharacterInBattleAttacked.cooldownAttackBar.transform.parent);
                    CharacterInBattleAttacked.healthBar.gameObject.transform.SetParent(CharacterInBattleAttacked.cooldownAttackBar.transform.parent);
                    // Debug.Log("Character " + CharacterInBattleAttack.infoWaifuAsset.ID + " die");
                    CharacterInBattleAttack.isAttack = true;

                    UnityEngine.Object.Destroy(CharacterAttacked);
                };

            }
            else
            {
                CharacterInBattleAttack.isAttack = false;
            }

        }
        static void AddValueRage(GameObject CharacterAttack, GameObject CharacterAttacked)
        {
            CharacterInBattle characterAttackInBattle = CharacterAttack.GetComponent<CharacterInBattle>();
            CharacterInBattle characterAttackedInBattle = CharacterAttacked.GetComponent<CharacterInBattle>();
            characterAttackInBattle.isAttack = true;
            if (!characterAttackInBattle.isBoss)
            {
                if (!characterAttackedInBattle.isBoss)
                {
                    float valueSliderBarCharacterAttack = characterAttackInBattle.cooldownSkillBar.value;
                    characterAttackInBattle.cooldownSkillBar = SliderTool.ChangeValueSlider(characterAttackInBattle.cooldownSkillBar, valueSliderBarCharacterAttack, valueSliderBarCharacterAttack + 0.25f, false);

                    float valueSliderBarCharacterAttacked = characterAttackedInBattle.cooldownSkillBar.value;
                    characterAttackedInBattle.cooldownSkillBar = SliderTool.ChangeValueSlider(characterAttackedInBattle.cooldownSkillBar, valueSliderBarCharacterAttacked, valueSliderBarCharacterAttacked + 0.5f, false);
                }
                else
                {
                    float valueSliderBarCharacterAttack = characterAttackInBattle.cooldownSkillBar.value;
                    characterAttackInBattle.cooldownSkillBar = SliderTool.ChangeValueSlider(characterAttackInBattle.cooldownSkillBar, valueSliderBarCharacterAttack, valueSliderBarCharacterAttack + 0.25f, false);

                    float valueSliderBarCharacterAttacked = characterAttackedInBattle.cooldownSkillBar.value;
                    characterAttackedInBattle.cooldownSkillBar = SliderTool.ChangeValueSlider(characterAttackedInBattle.cooldownSkillBar, valueSliderBarCharacterAttacked, valueSliderBarCharacterAttacked + 0.1f, false);
                }

            }
            else
            {
                float valueSliderBarCharacterAttack = characterAttackInBattle.cooldownSkillBar.value;
                characterAttackInBattle.cooldownSkillBar = SliderTool.ChangeValueSlider(characterAttackInBattle.cooldownSkillBar, valueSliderBarCharacterAttack, valueSliderBarCharacterAttack + 0.01f, false);

                float valueSliderBarCharacterAttacked = characterAttackedInBattle.cooldownSkillBar.value;
                characterAttackedInBattle.cooldownSkillBar = SliderTool.ChangeValueSlider(characterAttackedInBattle.cooldownSkillBar, valueSliderBarCharacterAttacked, valueSliderBarCharacterAttacked + 0.5f, false);
            }

        }
        public static void SpawnTxtDame(GameObject gbTxt, Transform PosTxt, int LossHp, float durationsTxt)
        {
            GameObject txtDame = UnityEngine.Object.Instantiate(gbTxt, PosTxt);
            float PosX = 0;
            if (UnityEngine.Random.Range(0, 1) == 0)
            {
                PosX = txtDame.transform.position.x + (float)UnityEngine.Random.Range(0, 1);
            }
            else
            {
                PosX = txtDame.transform.position.x - (float)UnityEngine.Random.Range(1, 2);
            }
            txtDame.transform.position = new Vector3(PosX, txtDame.transform.position.y, txtDame.transform.position.z);
            txtDame.GetComponent<TextMeshProUGUI>().text = "-" + LossHp.ToString();
            txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
            if (txtDame != null)
            {
                Tween animTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durationsTxt, durationsTxt);
                animTxtDame.OnComplete(() =>
                {
                    UnityEngine.Object.Destroy(txtDame);
                });
            }
        }
    }

}