using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle;
using RubikCasual.Data;
using Sirenix.OdinInspector;
using Spine.Unity;
using Spine.Unity.Editor;
using UnityEngine;

namespace RubikCasual.CreateSkill
{
    [Serializable]
    public class SkillHero
    {
        public int Id, Row, Column;
        public Type typeSkill;
        public float durationWave, durationAttacked;
    }
    public enum TypeSkill
    {
        Other = 0,
        Wave = 1,
    }
    public class CharacterSetSkillController : MonoBehaviour
    {
        public DataController dataController;
        public MapBattleController mapBattleController;
        public string indexId = "1";
        SkeletonAnimation characterClone;
        public bool isSkill;
        public List<GameObject> lsSlotGbEnemy;
        public CharacterInBattle EnemyInBattle;
        public int Row, Column, SlotCharacter;
        public TypeSkill typeSkill;
        public float durationWave = 0.25f, durationAttacked = 0.5f;
        float attribute = 1;
        // public float SlotCharacter = 2;
        void Start()
        {
            dataController = DataController.instance;
            StartCoroutine(LoadMap());
        }
        IEnumerator LoadMap()
        {
            yield return new WaitForSeconds(0.25f);
            CreateEnemy();
        }

        [Button]
        void CreateCharacter()
        {
            if (characterClone != null)
            {
                Destroy(characterClone.gameObject);
            }
            characterClone = dataController.characterAssets.WaifuAssets.Get2D(indexId, isSkill);
            characterClone.transform.SetParent(this.transform);
            characterClone.transform.position = this.transform.position;
            characterClone.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Character;
            characterClone.loop = true;
            characterClone.AnimationName = NameAnim.Anim_Character_Idle;
            SpineEditorUtilities.ReinitializeComponent(characterClone);

        }
        [Button]
        void BtnUseSkill()
        {
            SetAnimSkill();
        }
        void SetAnimSkill()
        {
            characterClone.AnimationName = NameAnim.Anim_Character_Skill;
            characterClone.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Skill, false);
            characterClone.AnimationState.Complete += delegate
            {
                if (characterClone.AnimationName != NameAnim.Anim_Character_Idle)
                {
                    characterClone.AnimationName = NameAnim.Anim_Character_Idle;
                }
            };
            UseSkill(Row, Column, SlotCharacter);

        }
        [Button]
        void SetAnimIdle()
        {
            characterClone.AnimationName = NameAnim.Anim_Character_Idle;
            SpineEditorUtilities.ReinitializeComponent(characterClone);
        }
        [Button]
        void CreateEnemy()
        {
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                SpawnEnemyForStage(i, i);
            }
        }
        void UseSkill(int row, int column, int slotCharacter = 2)
        {
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
            StartCoroutine(ShowSkill(row, column, minColumn));
        }
        IEnumerator ShowSkill(int row, int column, int minColumn)
        {
            yield return new WaitForSeconds(durationAttacked);
            if (typeSkill == TypeSkill.Wave)
            {
                int count = 0;
                for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                {
                    yield return new WaitForSeconds(durationWave);
                    for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                    {
                        if (lsSlotGbEnemy[count] != null)
                        {
                            if (i < row)
                            {
                                if (j < column && j >= minColumn)
                                {
                                    SetAttacked(count);
                                }
                            }
                        }

                        count++;
                    }
                }
            }
            else
            {
                int count = 0;
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
                                    SetAttacked(count);
                                }
                            }
                        }
                        count++;
                    }
                }
            }
        }
        void SetAttacked(int count)
        {
            SkeletonAnimation skeletonEnemy = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            skeletonEnemy.AnimationName = NameAnim.Anim_Character_Attacked;
            skeletonEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
            skeletonEnemy.AnimationState.Complete += delegate
            {
                skeletonEnemy.AnimationName = NameAnim.Anim_Character_Idle;
                skeletonEnemy.loop = true;
            };
        }
        void SpawnEnemyForStage(int idStage, int idRowSlot)
        {

            int index = idRowSlot;
            ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[index];

            for (int j = 0; j < lsPosSlot.lsPosCharacterSlot.Count; j++)
            {
                int indexOfSlot = j + 5 * idRowSlot;
                // Debug.Log(dataController.stageAssets.lsConvertStageAssetsData[0].lsValueSlot[0]);
                int idValueInSlot = dataController.stageAssets.GetNameAndId(dataController.stageAssets.lsConvertStageAssetsData[idStage].lsValueSlot[j]).intValue;
                string valueSlotStage = dataController.stageAssets.GetNameAndId(dataController.stageAssets.lsConvertStageAssetsData[idStage].lsValueSlot[j]).stringValue;

                PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[j];

                if (idValueInSlot != -1)
                {
                    // UnityEngine.Debug.Log(enemyAssets.WaifuEnemyAssetDatas.FirstOrDefault(f => f.Index == enemyAssets.lsIdEnemy[indexRand]).Is_Boss);
                    mapBattleController.lsPosEnemySlot[index].lsPosCharacterSlot[j].id = idValueInSlot;
                    attribute *= dataController.stageAssets.lsConvertStageAssetsData[idStage].Attribute;


                    CharacterInBattle enemyInBattle = Instantiate(EnemyInBattle, posSlot.gameObject.transform);
                    enemyInBattle.indexOfSlot = indexOfSlot;
                    enemyInBattle.isEnemy = true;

                    enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;

                    enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                    enemyInBattle.cooldownSkillBar.gameObject.SetActive(false);
                    enemyInBattle.healthBar.gameObject.SetActive(false);

                    SkeletonAnimation Enemy = Instantiate(dataController.characterAssets.enemyAssets.Get2D(idValueInSlot.ToString()));

                    Transform poscharacterInBattle = enemyInBattle.PosCharacter;

                    Enemy.transform.localScale = dataController.characterAssets.WaifuAssets.transform.localScale * 2f / 3f;
                    Enemy.gameObject.transform.SetParent(poscharacterInBattle);
                    Enemy.gameObject.transform.position = poscharacterInBattle.position;
                    Enemy.loop = true;
                    Enemy.AnimationName = NameAnim.Anim_Character_Idle;
                    if (enemyInBattle.isEnemy)
                    {
                        Enemy.initialFlipX = true;
                    }
                    Enemy.gameObject.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Character;
                    SpineEditorUtilities.ReinitializeComponent(Enemy);
                    enemyInBattle.skeletonCharacterAnimation = Enemy;
                    lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                }
                else
                {
                    lsSlotGbEnemy.Add(null);
                }
            }
        }
    }
}
