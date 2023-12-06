using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Rubik.Axie;
using Rubik.Waifu;
using RubikCasual.Battle.Calculate;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        const string Anim_Character_Attack = "Attack",
        Anim_Charater_Attacked = "Attacked",
        Anim_Character_Idle = "Idle",
        Anim_Character_Die = "Die",
        Anim_Character_Skill = "SkillCast";
        const string Layer_Attack = "Character_Attack", Layer_Attacked = "Character_Attacked";
        bool isEndBattle = false;
        GameState gameState;
        [Button]
        void UpSpeed()
        {
            Time.timeScale = 2f;
        }
        [Button]
        void DownSpeed()
        {
            Time.timeScale = 1f;
        }
        void Start()
        {
            gameState = GameState.START;
            CreateBattlefield();
            // Cooldown();
        }
        void Update()
        {
            StartCoroutine(BaseState(gameState));
        }
        IEnumerator BaseState(GameState state)
        {

            switch (state)
            {
                case GameState.START:
                    yield return new WaitForSeconds(durations * 2);
                    gameState = GameState.BATTLE;
                    break;
                case GameState.WAIT_BATTLE:

                    break;
                case GameState.BATTLE:
                    Atack();
                    break;
                case GameState.END:
                    EndBattleMoveCharacter();
                    break;
            }
        }
        void CreateBattlefield()
        {
            CreateAreaHeroStart(HeroInArea);
            CreateAreaEnemyStart();


        }

        SkeletonAnimation SpawnCharacter(Transform poscharacterInBattle, SkeletonAnimation WaifuCharacter)
        {
            SkeletonAnimation character = Instantiate(WaifuCharacter);
            character.transform.localScale = waifuAssets.transform.localScale * 2f / 3f;
            character.gameObject.transform.SetParent(poscharacterInBattle);
            character.gameObject.transform.position = poscharacterInBattle.position;
            character.loop = true;
            character.AnimationName = Anim_Character_Idle;

            character.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Character";
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

                    SkeletonAnimation Hero = SpawnCharacter(heroInBattle.PosCharacter, waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString()));

                    CharacterDragPosition CharacterHero = Hero.gameObject.AddComponent<CharacterDragPosition>();
                    CharacterHero.CharacterSke = waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString());
                    CharacterHero.posCharacter = posSlot.gameObject.transform;
                    heroInBattle.skeletonCharacterAnimation = Hero;
                    heroInBattle.infoWaifuAsset = waifuAssets.infoWaifuAssets.lsInfoWaifuAssets.Find(f => f.Index == lsHeroInArea[index].idCharacter);

                    heroInBattle.cooldownAttackBar.value = 0;
                    heroInBattle.cooldownSkillBar.value = 0;
                    heroInBattle.healthBar.value = 1;
                    heroInBattle.Rage = 0;
                    heroInBattle.HpNow = heroInBattle.infoWaifuAsset.HP;

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
                    SkeletonAnimation Enemy = SpawnCharacter(enemyInBattle.PosCharacter, enemyAssets.Get2D(enemyAssets.lsIdEnemy[indexRand].ToString()));
                    enemyInBattle.gameObject.transform.localScale = new Vector3(-enemyInBattle.gameObject.transform.localScale.x, enemyInBattle.gameObject.transform.localScale.y, enemyInBattle.gameObject.transform.localScale.z);
                    enemyInBattle.skeletonCharacterAnimation = Enemy;


                    enemyInBattle.cooldownAttackBar.value = 0;
                    enemyInBattle.cooldownSkillBar.value = 0;
                    enemyInBattle.healthBar.value = 1;
                    enemyInBattle.Rage = 0;
                    enemyInBattle.HpNow = 300f;
                    enemyInBattle.infoWaifuAsset.HP = 300f;
                    enemyInBattle.infoWaifuAsset.DmgPhysic = 10f;

                    lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                }
            }
        }




        IEnumerator StartCooldown(Slider slider, float cooldownTime)
        {
            float timer = 0f;
            while (timer < cooldownTime)
            {
                timer += Time.deltaTime;
                slider.value = timer / cooldownTime;
                yield return null;
            }

            slider.value = 1f;
        }

        void Atack()
        {
            if (!isEndBattle)
            {
                for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                {
                    if (lsSlotGbHero[i] != null && lsSlotGbEnemy[i] != null)
                    {
                        CharacterInBattle Hero = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                        CharacterInBattle Enemy = lsSlotGbEnemy[i].GetComponent<CharacterInBattle>();
                        float randomCooldownTimeHero = (float)Random.Range(1, 10);
                        float randomCooldownTimeEnemy = (float)Random.Range(1, 10);
                        if (!Hero.isAttack && !Enemy.isAttack)
                        {
                            if (Hero.cooldownSkillBar.value == 1)
                            {
                                CharacterUseSkill(Hero);
                                Hero.cooldownSkillBar.value = 0;
                            }
                            else
                            {
                                if (Hero.cooldownAttackBar.value == 1)
                                {
                                    CharacterAtackAnimation(Enemy.gameObject, Hero.gameObject);
                                    // Debug.Log((i + 1) + " hero atack");
                                    Hero.cooldownAttackBar.value = 0;
                                }
                                if (Hero.cooldownAttackBar.value == 0)
                                {
                                    StartCoroutine(StartCooldown(Hero.cooldownAttackBar, randomCooldownTimeHero));
                                }
                            }

                            if (Enemy.cooldownSkillBar.value == 1)
                            {
                                // CharacterUseSkill();
                                Enemy.cooldownSkillBar.value = 0;
                            }
                            else
                            {
                                if (Enemy.cooldownAttackBar.value == 1)
                                {
                                    CharacterAtackAnimation(Hero.gameObject, Enemy.gameObject);
                                    // Debug.Log((i + 1) + " enemy atack");
                                    Enemy.cooldownAttackBar.value = 0;
                                }
                                if (Enemy.cooldownAttackBar.value == 0)
                                {
                                    StartCoroutine(StartCooldown(Enemy.cooldownAttackBar, randomCooldownTimeEnemy));
                                }
                            }
                        }
                    }
                }
                CheckEndBattle();
            }
            if (isEndBattle)
            {
                gameState = GameState.END;
            }

        }
        void CheckEndBattle()
        {
            int Count = 0;
            for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
            {
                if (lsSlotGbHero[i] == null || lsSlotGbEnemy[i] == null)
                {
                    Count++;
                }
            }
            if (mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count == Count)
            {
                isEndBattle = true;
            }
        }
        float durations = 0.5f;
        void CompleteAnimation(SkeletonAnimation skeletonAnimation, int layerIndex)
        {
            

        }
        void CharacterUseSkill(CharacterInBattle CharacterAttack)
        {
            CharacterAttack.skeletonCharacterAnimation.AnimationName = Anim_Character_Skill;
            CompleteAnimation(CharacterAttack.skeletonCharacterAnimation, 4);



        }
        void CharacterAtackAnimation(GameObject CharacterAttacked, GameObject CharacterAttack)
        {
            CharacterAttack.GetComponent<CharacterInBattle>().isAttack = true;
            float valueSliderBarCharacterAttack = CharacterAttack.GetComponent<CharacterInBattle>().cooldownSkillBar.value;
            CharacterAttack.GetComponent<CharacterInBattle>().cooldownSkillBar.value = valueSliderBarCharacterAttack + 0.05f;

            float valueSliderBarCharacterAttacked = CharacterAttacked.GetComponent<CharacterInBattle>().cooldownSkillBar.value;
            CharacterAttacked.GetComponent<CharacterInBattle>().cooldownSkillBar.value = valueSliderBarCharacterAttacked + 0.1f;

            StartCoroutine(MoveBackDelay(CharacterAttack, CharacterAttacked, durations));
        }
        IEnumerator MoveBackDelay(GameObject CharacterAttack, GameObject CharacterAttacked, float delay)
        {
            SkeletonAnimation CharacterAttackAnim = CharacterAttack.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            SkeletonAnimation CharacterAttackedAnim = CharacterAttacked.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            CharacterInBattle CharacterInBattleAttack = CharacterAttack.GetComponent<CharacterInBattle>();
            CharacterInBattle CharacterInBattleAttacked = CharacterAttacked.GetComponent<CharacterInBattle>();

            CharacterAttackAnim.AnimationName = Anim_Character_Attack;

            yield return new WaitForSeconds(delay / 2);
            CharacterAttackedAnim.AnimationName = Anim_Charater_Attacked;

            Calculator.Calculate(CharacterInBattleAttack, CharacterInBattleAttacked);

            yield return new WaitForSeconds(delay / 2);
            CharacterAttackedAnim.AnimationName = Anim_Character_Idle;
            CharacterAttackAnim.AnimationName = Anim_Character_Idle;
            if (CharacterInBattleAttacked.HpNow == 0)
            {
                CharacterAttackedAnim.AnimationName = Anim_Character_Die;
                CharacterAttack.GetComponent<CharacterInBattle>().isAttack = true;
                yield return new WaitForSeconds(delay * 3 / 2f);
                Destroy(CharacterAttacked);
            }
            else
            {
                CharacterAttack.GetComponent<CharacterInBattle>().isAttack = false;
            }
        }
        bool isRangeRemoved = false;
        void EndBattleMoveCharacter()
        {


            if (!isRangeRemoved)
            {
                for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                {
                    int index = i;
                    if (lsSlotGbEnemy[i] != null)
                    {
                        lsSlotGbEnemy[i].transform.SetParent(mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i].transform);
                        lsSlotGbEnemy[i].transform.DOMoveX(mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i].transform.position.x, durations * 2);
                        Destroy(lsSlotGbEnemy[i]);
                        // Debug.LogWarning("Chưa null "+ i);
                    }
                }
                lsSlotGbEnemy.RemoveRange(0, 5); // Xóa 5 phần tử đầu tiên một lần duy nhất
                isRangeRemoved = true; // Đánh dấu rằng đã xóa
            }
            else
            {
                int Count = 0;
                for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                {
                    if (lsSlotGbHero[i] != null)
                    {
                        lsSlotGbHero[i].GetComponent<CharacterInBattle>().isAttack = false;
                    }
                    for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                    {
                        if (i != (lsSlotGbEnemy.Count / 5))
                        {

                            lsSlotGbEnemy[Count].transform.SetParent(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform);
                            lsSlotGbEnemy[Count].transform.DOMoveX(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform.position.x, durations * 2);
                            if (i == 0)
                            {
                                CharacterInBattle EnemyClone = lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>();
                                EnemyClone.cooldownAttackBar.gameObject.SetActive(true);
                                // EnemyClone.cooldownSkillBar.gameObject.SetActive(true);

                            }
                            Count++;
                        }
                    }
                }
                StartCoroutine(WaitBattleDelay());
            }

        }
        void SpawnEnemyEndBattle()
        {
            if (lsSlotGbEnemy.Count < 25)
            {
                for (int i = 0; i < 5; i++)
                {
                    int index = i;
                    int indexRand = Random.Range(0, enemyAssets.lsIdEnemy.Count);
                    ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[4];

                    mapBattleController.lsPosEnemySlot[4].lsPosCharacterSlot[index].id = enemyAssets.lsIdEnemy[indexRand];
                    PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[index];

                    CharacterInBattle enemyInBattle = Instantiate(characterInBattle, posSlot.gameObject.transform);
                    enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;

                    enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                    enemyInBattle.cooldownSkillBar.gameObject.SetActive(false);

                    SkeletonAnimation Enemy = SpawnCharacter(enemyInBattle.PosCharacter, enemyAssets.Get2D(enemyAssets.lsIdEnemy[indexRand].ToString()));
                    enemyInBattle.gameObject.transform.localScale = new Vector3(-enemyInBattle.gameObject.transform.localScale.x, enemyInBattle.gameObject.transform.localScale.y, enemyInBattle.gameObject.transform.localScale.z);
                    enemyInBattle.skeletonCharacterAnimation = Enemy;


                    enemyInBattle.cooldownAttackBar.value = 0;
                    enemyInBattle.cooldownSkillBar.value = 0;
                    enemyInBattle.healthBar.value = 1;
                    enemyInBattle.Rage = 0;
                    enemyInBattle.HpNow = 300f;
                    enemyInBattle.infoWaifuAsset.HP = 300f;
                    enemyInBattle.infoWaifuAsset.DmgPhysic = 10f;

                    lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                }
            }
        }
        IEnumerator WaitBattleDelay()
        {

            yield return new WaitForSeconds(durations);
            SpawnEnemyEndBattle();
            isEndBattle = false;
            isRangeRemoved = false;
            gameState = GameState.START;

        }

    }
    [System.Serializable]
    public class SlotInArea
    {
        public float slotCharacter, idCharacter;
    }
}