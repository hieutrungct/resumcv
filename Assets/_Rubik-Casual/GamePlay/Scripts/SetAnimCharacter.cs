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
        public static void BossUseSkill(CharacterInBattle EnemyInBattle, MapBattleController dameSlotTxtController, List<GameObject> lsSlotGbHero, float durationsTxtDame)
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

                    GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, dameSlotTxtController.lsPosHeroSlot.lsPosCharacterSlot[index].transform);

                    txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - HeroInBattle.HpNow)).ToString();
                    Tween animTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durationsTxtDame, durationsTxtDame);
                    txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
                    animTxtDame.OnComplete(() =>
                    {
                        Destroy(txtDame);
                    });
                    CheckHpEnemy(lsSlotGbHero[index]);
                }
            }

        }
        public static void CharacterUseSkill(CharacterInBattle CharacterAttack, MapBattleController dameSlotTxtController, List<GameObject> lsSlotGbEnemy, float durationsTxtDame)
        {
            CharacterAttack.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Skill;
            CharacterAttack.skeletonCharacterAnimation.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attack;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Skill, false);
            CharacterAttack.isUseSkill = true;
            CharacterAttack.cooldownSkillBar.value = 0;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.Complete += delegate
            {
                CharacterAttack.isUseSkill = false;

                CharacterAttack.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Idle;
            };

            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < dameSlotTxtController.lsPosEnemySlot[0].lsPosCharacterSlot.Count; j++)
                {
                    if ((count - CharacterAttack.indexOfSlot) % 5 == 0)
                    {
                        // UnityEngine.Debug.Log(count);
                        if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null)
                        {

                            SkeletonAnimation AnimEnemy = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
                            AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Attacked;
                            AnimEnemy.AnimationName = NameAnim.Anim_Character_Attacked;
                            AnimEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                            AnimEnemy.AnimationState.Complete += delegate
                            {
                                // AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                                AnimEnemy.AnimationName = NameAnim.Anim_Character_Idle;
                            };
                            CharacterInBattle CharacterInBattleAttacked = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();

                            float OldHp = CharacterInBattleAttacked.HpNow;
                            Calculator.CalculateHealth(CharacterAttack, CharacterInBattleAttacked);

                            GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, dameSlotTxtController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform);

                            txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().HpNow)).ToString();
                            Tween animTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durationsTxtDame, durationsTxtDame);
                            txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
                            CheckHpEnemy(lsSlotGbEnemy[count]);
                            animTxtDame.OnComplete(() =>
                            {
                                Destroy(txtDame);
                            });
                        }
                    }

                    count++;
                }
            }

        }

        public static void CharacterAtackAnimation(GameObject CharacterAttacked, GameObject CharacterAttack, MapBattleController dameSlotTxtController, float durationsTxtDame)
        {

            if (CharacterAttacked == null)
            {
                CharacterAttack.GetComponent<CharacterInBattle>().isAttack = false;
                return;
            }
            CharacterAttack.GetComponent<CharacterInBattle>().isAttack = true;
            float valueSliderBarCharacterAttack = CharacterAttack.GetComponent<CharacterInBattle>().cooldownSkillBar.value;
            CharacterAttack.GetComponent<CharacterInBattle>().cooldownSkillBar.value = valueSliderBarCharacterAttack + 0.25f;

            float valueSliderBarCharacterAttacked = CharacterAttacked.GetComponent<CharacterInBattle>().cooldownSkillBar.value;
            CharacterAttacked.GetComponent<CharacterInBattle>().cooldownSkillBar.value = valueSliderBarCharacterAttacked + 0.5f;

            MoveBackDelay(CharacterAttack, CharacterAttacked, dameSlotTxtController, durationsTxtDame);
        }
        static void MoveBackDelay(GameObject CharacterAttack, GameObject CharacterAttacked, MapBattleController dameSlotTxtController, float durationsTxtDame)
        {

            SkeletonAnimation CharacterAttackAnim = CharacterAttack.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            SkeletonAnimation CharacterAttackedAnim = CharacterAttacked.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            CharacterInBattle CharacterInBattleAttack = CharacterAttack.GetComponent<CharacterInBattle>();
            CharacterInBattle CharacterInBattleAttacked = CharacterAttacked.GetComponent<CharacterInBattle>();

            // CharacterAttackAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attack;
            // CharacterAttackedAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attacked;

            CharacterAttackAnim.AnimationName = NameAnim.Anim_Character_Attack;

            CharacterAttackAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attack, false);
            CharacterAttackAnim.AnimationState.Complete += delegate
            {
                CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Attacked;
                CharacterAttackedAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                CharacterAttackedAnim.AnimationState.Complete += delegate
                {
                    // CharacterAttackedAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                    CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Idle;
                };
                // CharacterAttackAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                CharacterAttackAnim.AnimationName = NameAnim.Anim_Character_Idle;
            };


            float OldHp = CharacterInBattleAttacked.HpNow;
            Calculator.CalculateHealth(CharacterInBattleAttack, CharacterInBattleAttacked);
            if (CharacterInBattleAttacked.txtHealthBar != null)
            {
                CharacterInBattleAttacked.txtHealthBar.text = CharacterInBattleAttacked.HpNow.ToString() + "/" + CharacterInBattleAttacked.infoWaifuAsset.HP.ToString();
            }

            Transform TransTxt;
            if (CharacterInBattleAttacked.isEnemy)
            {
                TransTxt = dameSlotTxtController.lsPosEnemySlot[0].lsPosCharacterSlot[CharacterInBattleAttacked.indexOfSlot].transform;
            }
            else
            {
                TransTxt = dameSlotTxtController.lsPosHeroSlot.lsPosCharacterSlot[CharacterInBattleAttacked.indexOfSlot].transform;
            }
            GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, TransTxt);
            txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - CharacterInBattleAttacked.HpNow)).ToString();
            txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
            Tween moveTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durationsTxtDame, durationsTxtDame);
            moveTxtDame.OnComplete(() =>
            {
                Destroy(txtDame);
            });
            if (CharacterInBattleAttacked.HpNow == 0)
            {

                CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Die;
                CharacterAttackedAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Die, false);
                CharacterAttackedAnim.AnimationState.Complete += delegate
                {
                    CharacterInBattleAttack.isAttack = true;
                    CharacterInBattleAttacked.healthBar.gameObject.transform.SetParent(CharacterInBattleAttacked.cooldownAttackBar.transform.parent);
                    if (CharacterInBattleAttacked.isBoss)
                    {
                        CharacterInBattleAttacked.cooldownSkillBar.gameObject.transform.SetParent(CharacterInBattleAttacked.cooldownAttackBar.transform.parent);
                    }
                    Destroy(CharacterAttacked);
                };

            }
            else
            {
                if (CharacterAttack != null)
                {
                    CharacterAttack.GetComponent<CharacterInBattle>().isAttack = false;
                }
            }

        }
        static void CheckHpEnemy(GameObject gbEnemy)
        {
            CharacterInBattle enemyInBattle = gbEnemy.GetComponent<CharacterInBattle>();
            SkeletonAnimation enemyAnim = enemyInBattle.skeletonCharacterAnimation;
            if (enemyInBattle.HpNow == 0)
            {

                enemyAnim.AnimationName = NameAnim.Anim_Character_Die;
                enemyAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Die, false);
                enemyAnim.AnimationState.Complete += delegate
                {
                    enemyInBattle.healthBar.gameObject.transform.SetParent(enemyInBattle.cooldownAttackBar.transform.parent);
                    if (enemyInBattle.isBoss)
                    {
                        enemyInBattle.cooldownSkillBar.gameObject.transform.SetParent(enemyInBattle.cooldownAttackBar.transform.parent);
                    }
                    // UnityEngine.Debug.Log(enemyAnim.skeletonDataAsset.name);
                    Destroy(gbEnemy);
                };
            }
        }
    }

}