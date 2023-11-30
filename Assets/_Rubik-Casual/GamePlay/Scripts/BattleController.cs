using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rubik.Waifu;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace RubikCasual.Battle
{
    public class BattleController : MonoBehaviour
    {
        public MapBattleController mapBattleController;
        public CharacterInBattle characterInBattle;
        public WaifuAssets waifuAssets;
        public EnemyAssets enemyAssets;
        public List<SlotInArea> HeroInArea;
        public List<GameObject> lsSlotGbEnemy, lsSlotGbHero;
        void Start()
        {
            checkAssets();
            CreateBattlefield();
        }
        void checkAssets()
        {


        }
        void CreateBattlefield()
        {
            CreateAreaHeroStart(HeroInArea);
            CreateAreaEnemyStart();
        }
        
        SkeletonAnimation SpawnCharacter(CharacterInBattle characterInBattle, SkeletonAnimation WaifuCharacter)
        {
            SkeletonAnimation character = Instantiate(WaifuCharacter);
            character.transform.localScale = waifuAssets.transform.localScale * 2f / 3f;
            character.gameObject.transform.SetParent(characterInBattle.PosCharacter.gameObject.transform);
            character.gameObject.transform.position = characterInBattle.PosCharacter.gameObject.transform.position;
            character.loop = true;
            character.AnimationName = "Idle";
            return character;
        }
        void CreateAreaHeroStart(List<SlotInArea> lsHeroInArea)
        {
            for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
            {
                int index = i;
                PositionCharacterSlot posSlot = mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i];

                if (lsHeroInArea[index].idCharacter != 0)
                {
                    CharacterInBattle heroInBattle = Instantiate(characterInBattle, posSlot.gameObject.transform);
                    heroInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;
                    SkeletonAnimation Hero = SpawnCharacter(heroInBattle, waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString()));

                    lsSlotGbHero.Add(heroInBattle.gameObject);
                }
            }
        }

        [Button]
        void CreateAreaEnemyStart()
        {
            foreach (var item in lsSlotGbEnemy)
            {
                Destroy(item);
            }
            lsSlotGbEnemy.Clear();
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                int index = i;
                int indexRand = Random.Range(0, enemyAssets.lsIdEnemy.Count);
                ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[index];

                for (int j = 0; j < lsPosSlot.lsPosCharacterSlot.Count; j++)
                {
                    mapBattleController.lsPosEnemySlot[index].lsPosCharacterSlot[j].id = enemyAssets.lsIdEnemy[indexRand];
                    PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[j];

                    CharacterInBattle enemyInBattle = Instantiate(characterInBattle, posSlot.gameObject.transform);
                    enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;
                    if (i > 0)
                    {
                        enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                        enemyInBattle.cooldownSkillBar.gameObject.SetActive(false);
                    }
                    SkeletonAnimation Enemy = SpawnCharacter(enemyInBattle, enemyAssets.Get2D(enemyAssets.lsIdEnemy[indexRand].ToString()));
                    enemyInBattle.gameObject.transform.localScale = new Vector3(-enemyInBattle.gameObject.transform.localScale.x, enemyInBattle.gameObject.transform.localScale.y, enemyInBattle.gameObject.transform.localScale.z);
                    lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                }
            }
        }
        [System.Serializable]
        public class SlotInArea
        {
            public float slotCharacter, idCharacter;
        }
    }

}