using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTPackage.Functions;
using RubikCasual.Battle.Calculate;
using RubikCasual.Battle.UI;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace RubikCasual.Battle
{
    public class SetAnimCharacter : MonoBehaviour
    {

        public void BossUseSkill(CharacterInBattle EnemyInBattle, MapBattleController dameSlotTxtController, List<GameObject> lsSlotGbHero, float durationsTxtDame)
        {
            EnemyInBattle.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Skill;
            // EnemyInBattle.skeletonCharacterAnimation.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attack;
            EnemyInBattle.skeletonCharacterAnimation.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Skill, false);
            EnemyInBattle.isUseSkill = true;
            EnemyInBattle.skeletonCharacterAnimation.AnimationState.Complete += delegate
            {
                EnemyInBattle.isUseSkill = false;
                EnemyInBattle.cooldownSkillBar.value = 0;
                EnemyInBattle.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Idle;
            };

            for (int i = 0; i < lsSlotGbHero.Count; i++)
            {
                int index = i;
                if (lsSlotGbHero[index] != null && lsSlotGbHero[index].GetComponent<CharacterInBattle>() != null)
                {
                    CharacterInBattle HeroInBattle = lsSlotGbHero[index].GetComponent<CharacterInBattle>();
                    SkeletonAnimation AnimHero = HeroInBattle.skeletonCharacterAnimation;
                    AnimHero.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attacked;
                    AnimHero.AnimationName = NameAnim.Anim_Character_Attacked;
                    AnimHero.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                    AnimHero.AnimationState.Complete += delegate
                    {
                        AnimHero.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Character;
                        AnimHero.AnimationName = NameAnim.Anim_Character_Idle;
                    };


                    float OldHp = HeroInBattle.HpNow;
                    Calculator.CalculateHealth(EnemyInBattle, HeroInBattle);

                    // GameObject txtDame = UnityEngine.Object.Instantiate(UIGamePlay.instance.TxtDame, dameSlotTxtController.lsPosHeroSlot.lsPosCharacterSlot[index].transform);

                    // txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - HeroInBattle.HpNow)).ToString();
                    // Tween animTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durationsTxtDame, durationsTxtDame);
                    // txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
                    // animTxtDame.OnComplete(() =>
                    // {
                    //     UnityEngine.Object.Destroy(txtDame);
                    // });
                    Transform PosTxt = dameSlotTxtController.lsPosHeroSlot.lsPosCharacterSlot[index].transform;
                    int lossHp = (int)(OldHp - HeroInBattle.HpNow);
                    FuntionTimeDelay.SpawnTxtDame(UIGamePlay.instance.TxtDame, PosTxt, lossHp, durationsTxtDame);

                    CheckHpEnemy(lsSlotGbHero[index]);
                }
            }

        }
        public void CharacterUseSkill(CharacterInBattle CharacterAttack, MapBattleController dameSlotTxtController, List<GameObject> lsSlotGbEnemy, float durationsTxtDame)
        {
            CharacterAttack.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Skill;
            CharacterAttack.skeletonCharacterAnimation.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attack;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Skill, false);
            CharacterAttack.isUseSkill = true;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.Complete += delegate
            {
                CharacterAttack.isUseSkill = false;
                CharacterAttack.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Idle;
            };
            int numberSlotBoss = 2;
            if (lsSlotGbEnemy[numberSlotBoss] != null && lsSlotGbEnemy[numberSlotBoss].GetComponent<CharacterInBattle>() != null && lsSlotGbEnemy[numberSlotBoss].GetComponent<CharacterInBattle>().isBoss)
            {
                CharacterInBattle EnemyBossInBattle = lsSlotGbEnemy[numberSlotBoss].GetComponent<CharacterInBattle>();
                SkeletonAnimation AnimEnemy = EnemyBossInBattle.skeletonCharacterAnimation;
                AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attacked;
                AnimEnemy.AnimationName = NameAnim.Anim_Character_Attacked;
                AnimEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                AnimEnemy.AnimationState.Complete += delegate
                {
                    // AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                    AnimEnemy.AnimationName = NameAnim.Anim_Character_Idle;
                };

                float OldHp = EnemyBossInBattle.HpNow;
                Calculator.CalculateHealth(CharacterAttack, EnemyBossInBattle);

                // GameObject txtDame = UnityEngine.Object.Instantiate(UIGamePlay.instance.TxtDame, dameSlotTxtController.lsPosEnemySlot[0].lsPosCharacterSlot[numberSlotBoss].transform);

                // txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - lsSlotGbEnemy[numberSlotBoss].GetComponent<CharacterInBattle>().HpNow)).ToString();
                // Tween animTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durationsTxtDame, durationsTxtDame);
                // txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
                // CheckHpEnemy(lsSlotGbEnemy[numberSlotBoss]);
                // animTxtDame.OnComplete(() =>
                // {
                //     UnityEngine.Object.Destroy(txtDame);
                // });
                Transform PosTxt = EnemyBossInBattle.healthBar.transform;
                int lossHp = (int)(OldHp - lsSlotGbEnemy[numberSlotBoss].GetComponent<CharacterInBattle>().HpNow);
                FuntionTimeDelay.SpawnTxtDame(UIGamePlay.instance.TxtDame, PosTxt, lossHp, durationsTxtDame);

                CheckHpEnemy(lsSlotGbEnemy[numberSlotBoss]);

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

                                AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attacked;
                                AnimEnemy.AnimationName = NameAnim.Anim_Character_Attacked;
                                AnimEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                                AnimEnemy.AnimationState.Complete += delegate
                                {
                                    // AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                                    AnimEnemy.AnimationName = NameAnim.Anim_Character_Idle;
                                };


                                float OldHp = EnemyBossInBattle.HpNow;
                                Calculator.CalculateHealth(CharacterAttack, EnemyBossInBattle);

                                // GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, dameSlotTxtController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform);

                                // txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().HpNow)).ToString();
                                // Tween animTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durationsTxtDame, durationsTxtDame);
                                // txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;

                                // animTxtDame.OnComplete(() =>
                                // {
                                //     Destroy(txtDame);
                                // }); 

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

        public void CharacterAtackAnimation(GameObject CharacterAttacked, GameObject CharacterAttack, MapBattleController dameSlotTxtController, float durationsTxtDame)
        {
            CharacterInBattle characterAttackInBattle = CharacterAttack.GetComponent<CharacterInBattle>();
            CharacterInBattle characterAttackedInBattle = CharacterAttacked.GetComponent<CharacterInBattle>();
            characterAttackInBattle.isAttack = true;
            if (!characterAttackInBattle.isBoss)
            {
                if (!characterAttackedInBattle.isBoss)
                {
                    float valueSliderBarCharacterAttack = characterAttackInBattle.cooldownSkillBar.value;
                    characterAttackInBattle.cooldownSkillBar.value = valueSliderBarCharacterAttack + 0.25f;

                    float valueSliderBarCharacterAttacked = characterAttackedInBattle.cooldownSkillBar.value;
                    characterAttackedInBattle.cooldownSkillBar.value = valueSliderBarCharacterAttacked + 0.5f;
                }
                else
                {
                    float valueSliderBarCharacterAttack = characterAttackInBattle.cooldownSkillBar.value;
                    characterAttackInBattle.cooldownSkillBar.value = valueSliderBarCharacterAttack + 0.25f;

                    float valueSliderBarCharacterAttacked = characterAttackedInBattle.cooldownSkillBar.value;
                    characterAttackedInBattle.cooldownSkillBar.value = valueSliderBarCharacterAttacked + 0.1f;
                }

            }
            else
            {
                float valueSliderBarCharacterAttack = characterAttackInBattle.cooldownSkillBar.value;
                characterAttackInBattle.cooldownSkillBar.value = valueSliderBarCharacterAttack + 0.01f;

                float valueSliderBarCharacterAttacked = characterAttackedInBattle.cooldownSkillBar.value;
                characterAttackedInBattle.cooldownSkillBar.value = valueSliderBarCharacterAttacked + 0.5f;
            }


            StartCoroutine(FuntionTimeDelay.MoveBackDelay(CharacterAttack, CharacterAttacked, dameSlotTxtController, durationsTxtDame));
        }

        void CheckHpEnemy(GameObject gbEnemy)
        {
            CharacterInBattle enemyInBattle = gbEnemy.GetComponent<CharacterInBattle>();
            SkeletonAnimation enemyAnim = enemyInBattle.skeletonCharacterAnimation;
            if (enemyInBattle.HpNow == 0)
            {

                enemyAnim.AnimationName = NameAnim.Anim_Character_Die;
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

            CharacterAttackAnim.AnimationName = NameAnim.Anim_Character_Attack;
            CharacterAttackAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attack, false);
            CharacterInBattleAttack.isAttack = true;
            CharacterAttackAnim.AnimationState.Complete += delegate
            {
                CharacterInBattleAttack.isAttack = false;
                // CharacterAttackAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                CharacterAttackAnim.AnimationName = NameAnim.Anim_Character_Idle;
            };

            yield return new WaitForSeconds(durationsTxtDame / 3);

            CharacterAttackedAnim.AnimationState.Complete += delegate
            {
                CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Attacked;
                CharacterAttackedAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                // CharacterAttackedAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Idle;
            };

            float OldHp = CharacterInBattleAttacked.HpNow;
            Calculator.CalculateHealth(CharacterInBattleAttack, CharacterInBattleAttacked);
            if (CharacterInBattleAttacked.txtHealthBar != null)
            {
                CharacterInBattleAttacked.txtHealthBar.text = ((int)CharacterInBattleAttacked.HpNow).ToString() + "/" + CharacterInBattleAttacked.infoWaifuAsset.HP.ToString();
            }

            // Transform TransTxt = CharacterInBattleAttacked.healthBar.transform;

            // GameObject txtDame = UnityEngine.Object.Instantiate(UIGamePlay.instance.TxtDame, TransTxt);
            // txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - CharacterInBattleAttacked.HpNow)).ToString();
            // txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
            // Tween moveTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durationsTxtDame, durationsTxtDame);
            // moveTxtDame.OnComplete(() =>
            // {
            //     UnityEngine.Object.Destroy(txtDame);
            // });

            int lossHp = (int)(OldHp - CharacterInBattleAttacked.HpNow);
            if (OldHp - CharacterInBattleAttacked.HpNow > 0 && OldHp - CharacterInBattleAttacked.HpNow < 1)
            {
                lossHp = 1;
            }

            if (!CharacterInBattleAttack.isEnemy)
            {
                Transform PosTxt = CharacterInBattleAttacked.healthBar.transform;
                SpawnTxtDame(UIGamePlay.instance.TxtDame, PosTxt, lossHp, durationsTxtDame);
            }
            else
            {

                Transform PosTxt = dameSlotTxtController.lsPosEnemySlot[0].lsPosCharacterSlot[CharacterInBattleAttacked.indexOfSlot].gameObject.transform;
                SpawnTxtDame(UIGamePlay.instance.TxtDame, PosTxt, lossHp, durationsTxtDame);

            }


            if (CharacterInBattleAttacked.HpNow == 0)
            {
                CharacterInBattleAttacked.isAttack = true;
                CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Die;
                CharacterAttackedAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Die, false);


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