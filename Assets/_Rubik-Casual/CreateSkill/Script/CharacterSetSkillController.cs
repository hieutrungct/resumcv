using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle;
using RubikCasual.Data;
using RubikCasual.Waifu;
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
        public TypeSkill typeSkill;
        public float durationWave, durationAttacked;
    }
    public enum TypeSkill
    {
        Other = 0,
        Wave = 1,
        InTurn = 2,

    }
    public class CharacterSetSkillController : MonoBehaviour
    {
        public DataController dataController;
        public MapBattleController mapBattleController;
        public string indexId = "1";
        SkeletonAnimation characterClone;
        public bool isSkin;
        public List<GameObject> lsSlotGbEnemy;
        public CharacterInBattle EnemyInBattle;
        public int Row, Column, SlotCharacter, InTurn = 1;
        public TypeSkill typeSkill;
        public float durationWave = 0.25f, durationAttacked = 0.5f;
        public Transform transCharacter;
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
            CreateCharacter();
        }

        [Button]
        void CreateCharacter()
        {
            if (characterClone != null)
            {
                Destroy(characterClone.gameObject);
            }
            characterClone = dataController.characterAssets.WaifuAssets.Get2D(indexId, isSkin);
            characterClone.transform.SetParent(transCharacter);
            characterClone.transform.position = transCharacter.position;
            characterClone.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Character;
            characterClone.loop = true;
            characterClone.AnimationName = NameAnim.Anim_Character_Idle;
            SpineEditorUtilities.ReinitializeComponent(characterClone);

            if (transCharacter.gameObject.GetComponent<CharacterInBattle>() == null)
            {
                CharacterInBattle characterTest = transCharacter.gameObject.AddComponent<CharacterInBattle>();
                characterTest.infoWaifuAsset = dataController.characterAssets.WaifuAssets.infoWaifuAssets.lsInfoWaifuAssets.Find(f => f.ID == int.Parse(indexId));
                characterTest.Atk = (int)(characterTest.infoWaifuAsset.ATK * attribute);
                characterTest.Skill = (int)(characterTest.infoWaifuAsset.Skill * attribute);
            }
            else
            {
                CharacterInBattle characterTest = transCharacter.gameObject.GetComponent<CharacterInBattle>();
                characterTest.infoWaifuAsset = dataController.characterAssets.WaifuAssets.infoWaifuAssets.lsInfoWaifuAssets.Find(f => f.ID == int.Parse(indexId));
                characterTest.Atk = (int)(characterTest.infoWaifuAsset.ATK * attribute);
                characterTest.Skill = (int)(characterTest.infoWaifuAsset.Skill * attribute);
            }
        }
        [Button]
        void BtnUseSkill()
        {
            string value = InfoPanel.InfoPanel.instance.txtRow.text;
            int IntTest = int.Parse(value);
            Debug.Log(IntTest);
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
            StartCoroutine(ShowSkill(row, column, minColumn, InTurn));
        }
        IEnumerator ShowSkill(int row, int column, int minColumn, int inTurn = 1)
        {
            yield return new WaitForSeconds(durationAttacked);
            int count = 0;
            switch (typeSkill)
            {
                case TypeSkill.Wave:
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
                    break;

                case TypeSkill.InTurn:
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
                                            SetAttacked(count);
                                        }
                                    }
                                }
                                count++;
                            }
                        }
                        StartCoroutine(ShowSkill(row, column, minColumn, inTurn - 1));
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
                                        SetAttacked(count);
                                    }
                                }
                            }
                            count++;
                        }
                    }
                    break;
            }
        }
        void SetAttacked(int count)
        {
            SkeletonAnimation skeletonEnemy = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;


            skeletonEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
            skeletonEnemy.AnimationState.Complete += delegate
            {
                if (skeletonEnemy.AnimationName != NameAnim.Anim_Character_Idle)
                {
                    skeletonEnemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Idle, true);
                }
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
                    enemyInBattle.healthBar.gameObject.SetActive(true);

                    SkeletonAnimation Enemy = Instantiate(dataController.characterAssets.enemyAssets.Get2D(idValueInSlot.ToString()));

                    Transform poscharacterInBattle = enemyInBattle.PosCharacter;

                    Enemy.transform.localScale = dataController.characterAssets.WaifuAssets.transform.localScale * 2f / 3f;
                    Enemy.gameObject.transform.SetParent(poscharacterInBattle);
                    Enemy.gameObject.transform.position = poscharacterInBattle.position;
                    Enemy.loop = true;
                    Enemy.AnimationName = NameAnim.Anim_Character_Idle;

                    enemyInBattle.infoWaifuAsset = dataController.characterAssets.enemyAssets.infoEnemyAssets.lsInfoWaifuAssets.Find(f => f.Code == idValueInSlot.ToString());

                    enemyInBattle.cooldownAttackBar.value = 0;
                    enemyInBattle.cooldownSkillBar.value = 0;
                    enemyInBattle.healthBar.value = 1;
                    enemyInBattle.Rage = 0;
                    enemyInBattle.Hp = (int)(enemyInBattle.infoWaifuAsset.HP * attribute);
                    enemyInBattle.Def = (int)(enemyInBattle.infoWaifuAsset.DEF * attribute);
                    enemyInBattle.Atk = (int)(enemyInBattle.infoWaifuAsset.ATK * attribute);
                    enemyInBattle.HpNow = (int)(enemyInBattle.infoWaifuAsset.HP * attribute);

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
        [Button]
        void SaveCharacterSkill()
        {
            CharacterInBattle Character = transCharacter.GetComponent<CharacterInBattle>();
            InfoWaifuAsset infoWaifuAsset = dataController.characterAssets.WaifuAssets.infoWaifuAssets.lsInfoWaifuAssets.Find(f => f.ID == Character.infoWaifuAsset.ID);

            infoWaifuAsset = Character.infoWaifuAsset;
        }

    }
}
