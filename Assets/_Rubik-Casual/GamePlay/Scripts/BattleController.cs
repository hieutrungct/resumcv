using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Rubik.Axie;
using Rubik.Waifu;
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
        Anim_Character_Die = "Die";
        const string Layer_Attack = "Character_Attack", Layer_Attacked = "Character_Attacked";
        void Start()
        {
            checkAssets();
            CreateBattlefield();
            // Cooldown();
        }
        void Update()
        {
            Atack();
        }
        void checkAssets()
        {


        }
        void CreateBattlefield()
        {
            CreateAreaHeroStart(HeroInArea);
            CreateAreaEnemyStart();

            for (int i = 0; i < lsSlotGbHero.Count; i++)
            {
                CharacterInBattle Enemy = lsSlotGbEnemy[i].GetComponent<CharacterInBattle>();
                Enemy.cooldownAttackBar.value = 0;
                Enemy.cooldownSkillBar.value = 0;
                Enemy.healthBar.value = 1;
                Enemy.Rage = 0;
                Enemy.HpNow = 300f;
                Enemy.infoWaifuAsset.HP = 300f;
                Enemy.infoWaifuAsset.DmgPhysic = 10f;

                CharacterInBattle Hero = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                Hero.cooldownAttackBar.value = 0;
                Hero.cooldownSkillBar.value = 0;
                Hero.healthBar.value = 1;
                Hero.Rage = 0;
                Hero.HpNow = Hero.infoWaifuAsset.HP;
            }
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

                    lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                }
            }
        }

        // void Cooldown()
        // {
        //     for (int i = 0; i < lsSlotGbHero.Count; i++)
        //     {
        //         float randomCooldownTimeHero = (float)Random.Range(1, 10);
        //         StartCoroutine(StartCooldown(lsSlotGbHero[i].GetComponent<CharacterInBattle>().cooldownAttackBar, randomCooldownTimeHero));

        //         float randomCooldownTimeEnemy = (float)Random.Range(1, 10);
        //         StartCoroutine(StartCooldown(lsSlotGbEnemy[i].GetComponent<CharacterInBattle>().cooldownAttackBar, randomCooldownTimeEnemy));
        //     }
        // }


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
            for (int i = 0; i < lsSlotGbHero.Count; i++)
            {
                if (lsSlotGbHero[i] != null && lsSlotGbEnemy[i] != null)
                {
                    CharacterInBattle Hero = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                    CharacterInBattle Enemy = lsSlotGbEnemy[i].GetComponent<CharacterInBattle>();
                    float randomCooldownTimeHero = (float)Random.Range(1, 10);
                    float randomCooldownTimeEnemy = (float)Random.Range(1, 10);
                    if (!Hero.isAttack && !Enemy.isAttack)
                    {
                        if (Hero.cooldownAttackBar.value == 1)
                        {
                            CharacterAtackAnimation(Enemy.gameObject, Hero.gameObject);
                            Debug.Log((i + 1) + " hero atack");

                            Hero.cooldownAttackBar.value = 0;
                        }
                        if (Hero.cooldownAttackBar.value == 0)
                        {
                            StartCoroutine(StartCooldown(Hero.cooldownAttackBar, randomCooldownTimeHero));
                        }

                        if (Enemy.cooldownAttackBar.value == 1)
                        {
                            CharacterAtackAnimation(Hero.gameObject, Enemy.gameObject);
                            Debug.Log((i + 1) + " enemy atack");
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
        float durations = 0.5f;
        void CharacterAtackAnimation(GameObject CharacterAttacked, GameObject CharacterAttack)
        {
            CharacterAttack.GetComponent<CharacterInBattle>().isAttack = true;
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
            CharacterInBattleAttacked.HpNow = HealthAmount(CharacterInBattleAttack.infoWaifuAsset.DmgPhysic, CharacterInBattleAttacked.HpNow);
            CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / CharacterInBattleAttacked.infoWaifuAsset.HP;
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
        float HealthAmount(float dameAttack, float HealthAttacked)
        {
            HealthAttacked = -dameAttack + HealthAttacked;
            if (HealthAttacked <= 0)
            {
                HealthAttacked = 0;
            }
            return HealthAttacked;
        }
    }
    [System.Serializable]
    public class SlotInArea
    {
        public float slotCharacter, idCharacter;
    }
}