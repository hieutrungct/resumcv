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
    public class CharacterSetSkillController : MonoBehaviour
    {
        public DataController dataController;
        public MapBattleController mapBattleController;
        public string indexId = "1";
        public SkeletonAnimation characterClone;
        public bool isSkill;
        public List<GameObject> lsSlotGbEnemy;
        public CharacterInBattle EnemyInBattle;
        float attribute = 1;
        float SlotCharacter = 2;
        void Start()
        {
            dataController = DataController.instance;

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
        void SetAnimSkill()
        {
            characterClone.AnimationName = NameAnim.Anim_Character_Skill;
            SpineEditorUtilities.ReinitializeComponent(characterClone);
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
        void UseSkill(int row, int column)
        {
            int count = 0;
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                {
                    if (i < row)
                    {
                        if (j < SlotCharacter)
                        {

                        }
                    }

                    count++;
                }
            }
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
