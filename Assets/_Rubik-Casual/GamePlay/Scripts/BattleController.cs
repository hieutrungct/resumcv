using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DG.Tweening;
using Rubik.Axie;
using Rubik.Waifu;
using RubikCasual.Battle.Calculate;
using RubikCasual.Battle.Inventory;
using RubikCasual.Battle.UI;
using RubikCasual.Lobby;
using RubikCasual.Tool;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using TMPro;
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
        public GamePlayUI gamePlayUI;
        public List<SlotInArea> HeroInArea;
        public List<GameObject> lsSlotGbEnemy, lsSlotGbHero;
        const string Anim_Character_Attack = "Attack",
        Anim_Character_Attacked = "Attacked",
        Anim_Character_Idle = "Idle",
        Anim_Character_Die = "Die",
        Anim_Character_Skill = "SkillCast";
        const string Layer_Attack = "Character_Attack", Layer_Attacked = "Character_Attacked", Layer_Character = "Character";
        bool isEndBattle = false;
        public static BattleController instance;
        public GameState gameState;
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
            instance = this;
            gameState = GameState.WAIT_BATTLE;
            CreateBattlefield();
            // Cooldown();
        }
        void Update()
        {
            CheckHeroInBattle();
            StartCoroutine(BaseState(gameState));
        }

        public void StartGame()
        {
            if (gameState == GameState.WAIT_BATTLE)
            {
                gameState = GameState.START;
            }
        }
        public void ResetGame()
        {
            CountState = 1;
            CreateBattlefield();

        }
        int CountState = 1;
        IEnumerator BaseState(GameState state)
        {

            switch (state)
            {
                case GameState.START:

                    yield return new WaitForSeconds(durations * 2);
                    gameState = GameState.BATTLE;
                    break;
                case GameState.WAIT_BATTLE:
                    gamePlayUI.txtTime.text = CountState.ToString();
                    break;
                case GameState.BATTLE:
                    Atack();
                    break;
                case GameState.END_BATTLE:
                    EndBattleMoveCharacter();
                    break;
                case GameState.END:

                    break;
            }
        }
        void CreateBattlefield()
        {

            CreateAreaHeroStart(HeroInArea);
            CreateAreaEnemyStart();
        }
        void CheckHeroInBattle()
        {
            int count = 0;
            foreach (var item in lsSlotGbHero)
            {
                if (item == null)
                {
                    count++;
                }
            }
            if (count == 5)
            {
                gameState = GameState.END;
                // UnityEngine.Debug.Log("Hết Hero");
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

            character.gameObject.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
            return character;
        }

        void CreateAreaHeroStart(List<SlotInArea> lsHeroInArea)
        {
            foreach (var item in lsSlotGbHero)
            {
                Destroy(item);
            }
            lsSlotGbHero.Clear();

            for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
            {
                int index = i;
                PositionCharacterSlot posSlot = mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i];

                if (lsHeroInArea[index].idCharacter != 0)
                {
                    CharacterInBattle heroInBattle = Instantiate(characterInBattle, posSlot.gameObject.transform);
                    heroInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;
                    heroInBattle.indexOfSlot = index;

                    SkeletonAnimation Hero = SpawnCharacter(heroInBattle.PosCharacter, waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString()));

                    CharacterDragPosition CharacterHero = Hero.gameObject.AddComponent<CharacterDragPosition>();
                    CharacterHero.CharacterSke = waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString());
                    CharacterHero.posCharacter = posSlot.gameObject.transform;
                    CharacterHero.oriIndex = index;
                    heroInBattle.skeletonCharacterAnimation = Hero;
                    heroInBattle.infoWaifuAsset = waifuAssets.infoWaifuAssets.lsInfoWaifuAssets.Find(f => f.Index == lsHeroInArea[index].idCharacter);

                    heroInBattle.cooldownAttackBar.value = 0;
                    heroInBattle.cooldownSkillBar.value = 0;
                    heroInBattle.healthBar.value = 1;
                    heroInBattle.Rage = 0;
                    heroInBattle.HpNow = heroInBattle.infoWaifuAsset.HP;

                    lsSlotGbHero.Add(heroInBattle.gameObject);
                }
                else
                {
                    lsSlotGbHero.Add(null);
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

                ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[index];

                for (int j = 0; j < lsPosSlot.lsPosCharacterSlot.Count; j++)
                {
                    int indexRand = Random.Range(0, enemyAssets.lsIdEnemy.Count);
                    PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[j];
                    if (indexRand > 10)
                    {
                        mapBattleController.lsPosEnemySlot[index].lsPosCharacterSlot[j].id = enemyAssets.lsIdEnemy[indexRand];


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
                    else
                    {
                        int rand = UnityEngine.Random.Range(0, 5);
                        if (rand == 0)
                        {
                            lsSlotGbEnemy.Add(null);
                        }
                        else
                        {
                            int randIdItem = UnityEngine.Random.Range(0, 5);
                            GameObject ItemClone = Instantiate(InventorryUIPanel.instance.itemInventory, posSlot.gameObject.transform);
                            ItemClone.transform.position = new Vector3(ItemClone.transform.position.x, ItemClone.transform.position.y + durations, ItemClone.transform.position.z);
                            SlotInventory Item = ItemClone.GetComponent<SlotInventory>();
                            Item.idItem = Item.lsIdItem[randIdItem];
                            Item.Icon.GetComponent<Image>().sprite = UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == Item.lsIdItem[randIdItem]).imageItem;

                            Destroy(ItemClone.GetComponent<ItemDragPosition>());
                            lsSlotGbEnemy.Add(ItemClone);
                        }
                    }
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
            CheckEndBattle();
            if (!isEndBattle)
            {

                for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                {
                    if (lsSlotGbHero[i] != null && lsSlotGbEnemy[i] != null && lsSlotGbEnemy[i].GetComponent<CharacterInBattle>() != null)
                    {
                        CharacterInBattle Hero = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                        CharacterInBattle Enemy = lsSlotGbEnemy[i].GetComponent<CharacterInBattle>();
                        float randomCooldownTimeHero = (float)Random.Range(1, 10);
                        float randomCooldownTimeEnemy = (float)Random.Range(1, 10);
                        if (!Hero.isAttack && !Enemy.isAttack && !Hero.isUseSkill)
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
                                // CharacterUseSkill(Enemy);
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


            }
            if (isEndBattle)
            {
                gameState = GameState.END_BATTLE;
            }

        }
        void CheckEndBattle()
        {
            int Count = 0;
            for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
            {
                if (lsSlotGbHero[i] == null || lsSlotGbEnemy[i] == null || lsSlotGbEnemy[i].GetComponent<CharacterInBattle>() == null)
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

        void CharacterUseSkill(CharacterInBattle CharacterAttack)
        {
            CharacterAttack.skeletonCharacterAnimation.AnimationName = Anim_Character_Skill;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.SetAnimation(0, Anim_Character_Skill, false);
            CharacterAttack.isUseSkill = true;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.Complete += delegate
            {
                CharacterAttack.isUseSkill = false;
                CharacterAttack.skeletonCharacterAnimation.AnimationName = Anim_Character_Idle;
            };

            int count = 0;
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                for (int j = 0; j < mapBattleController.lsPosEnemySlot[0].lsPosCharacterSlot.Count; j++)
                {
                    if ((count - CharacterAttack.indexOfSlot) % 5 == 0)
                    {
                        // UnityEngine.Debug.Log(count);
                        if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null)
                        {
                            SkeletonAnimation AnimEnemy = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
                            AnimEnemy.AnimationName = Anim_Character_Attacked;
                            AnimEnemy.AnimationState.SetAnimation(0, Anim_Character_Attacked, false);
                            AnimEnemy.AnimationState.Complete += delegate
                            {
                                AnimEnemy.AnimationName = Anim_Character_Idle;
                            };
                            CharacterInBattle CharacterInBattleAttacked = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();

                            float OldHp = CharacterInBattleAttacked.HpNow;
                            Calculator.CalculateHealth(CharacterAttack, CharacterInBattleAttacked);
                            // if (CharacterInBattleAttacked.HpNow == 0)
                            // {
                            //     AnimEnemy.AnimationName = Anim_Character_Die;
                            //     AnimEnemy.AnimationState.SetAnimation(0, Anim_Character_Die, false);
                            //     AnimEnemy.AnimationState.Complete += delegate
                            //     {
                            //         CharacterAttack.GetComponent<CharacterInBattle>().isAttack = true;
                            //         try
                            //         {
                            //             if (lsSlotGbEnemy[count] != null)
                            //             {
                            //                 Destroy(lsSlotGbEnemy[count]);
                            //             }
                            //         }
                            //         catch (System.Exception)
                            //         {
                            //             UnityEngine.Debug.Log(count);
                            //         }

                            //     };
                            // }
                            // else
                            // {
                            //     CharacterAttack.GetComponent<CharacterInBattle>().isAttack = false;
                            // }

                            GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().healthBar.gameObject.transform);

                            if (lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().transform.localScale.x < 0)
                            {
                                txtDame.transform.rotation = Quaternion.Euler(0, 180, 0);
                            }
                            else
                            {
                                txtDame.transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().HpNow)).ToString();
                            txtDame.transform.DOMoveY(txtDame.transform.position.y + durations, durations);
                            txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
                            StartCoroutine(DelayUseSkill(txtDame, durations));
                        }
                    }

                    count++;
                }
            }
        }
        IEnumerator DelayUseSkill(GameObject txtDame, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(txtDame);
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
            CharacterAttackAnim.AnimationState.SetAnimation(0, Anim_Character_Attack, false);
            CharacterAttackAnim.AnimationState.Complete += delegate
            {
                CharacterAttackAnim.AnimationName = Anim_Character_Idle;
            };
            yield return new WaitForSeconds(delay / 2);
            CharacterAttackedAnim.AnimationName = Anim_Character_Attacked;
            CharacterAttackedAnim.AnimationState.SetAnimation(0, Anim_Character_Attacked, false);
            CharacterAttackedAnim.AnimationState.Complete += delegate
            {
                CharacterAttackedAnim.AnimationName = Anim_Character_Idle;
            };
            float OldHp = CharacterInBattleAttacked.HpNow;
            Calculator.CalculateHealth(CharacterInBattleAttack, CharacterInBattleAttacked);
            GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, CharacterInBattleAttacked.healthBar.gameObject.transform);
            if (CharacterInBattleAttacked.transform.localScale.x < 0)
            {
                txtDame.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                txtDame.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - CharacterInBattleAttacked.HpNow)).ToString();
            txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
            txtDame.transform.DOMoveY(txtDame.transform.position.y + delay, delay);

            yield return new WaitForSeconds(delay / 2);


            if (CharacterInBattleAttacked.HpNow == 0)
            {
                CharacterAttackedAnim.AnimationName = Anim_Character_Die;
                CharacterAttackedAnim.AnimationState.SetAnimation(0, Anim_Character_Die, false);
                CharacterAttackedAnim.AnimationState.Complete += delegate
                {
                    CharacterAttack.GetComponent<CharacterInBattle>().isAttack = true;
                    Destroy(CharacterAttacked);
                };

            }
            else
            {
                CharacterAttack.GetComponent<CharacterInBattle>().isAttack = false;
            }
            yield return new WaitForSeconds(delay / 2);
            Destroy(txtDame);
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

                        StartCoroutine(MoveEnemy(lsSlotGbEnemy[i], i));
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
                        // lsSlotGbHero[i].GetComponent<CharacterInBattle>().skeletonCharacterAnimation.AnimationName = Anim_Character_Idle;
                    }
                    for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                    {
                        if (i != (lsSlotGbEnemy.Count / 5))
                        {
                            if (lsSlotGbEnemy[Count] != null)
                            {
                                lsSlotGbEnemy[Count].transform.SetParent(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform);
                                lsSlotGbEnemy[Count].transform.DOMoveX(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform.position.x, durations * 2);
                                if (i == 0 && lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>() != null)
                                {
                                    CharacterInBattle EnemyClone = lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>();
                                    EnemyClone.cooldownAttackBar.gameObject.SetActive(true);
                                    // EnemyClone.cooldownSkillBar.gameObject.SetActive(true);

                                }
                            }

                            Count++;
                        }
                    }
                }
                StartCoroutine(WaitBattleDelay());
                gameState = GameState.WAIT_BATTLE;
            }
        }
        IEnumerator MoveEnemy(GameObject gb, int i)
        {
            yield return new WaitForSeconds(durations * 2);
            if (gb.GetComponent<SlotInventory>() != null)
            {
                int idItem = gb.GetComponent<SlotInventory>().idItem;
                Calculator.CheckItemCalculate(idItem, lsSlotGbHero[i].GetComponent<CharacterInBattle>());
                StartCoroutine(MovePopup.ShowTxtDame(gb, UIGamePlay.instance.TxtDame, mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i].transform.position, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
            }
            else
            {
                Destroy(gb);
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
                    PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[index];

                    if (indexRand > 10)
                    {
                        lsPosSlot.lsPosCharacterSlot[index].id = enemyAssets.lsIdEnemy[indexRand];


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
                    else
                    {
                        int rand = UnityEngine.Random.Range(0, 5);
                        if (rand == 0)
                        {
                            lsSlotGbEnemy.Add(null);
                        }
                        else
                        {
                            int randIdItem = UnityEngine.Random.Range(0, 5);
                            GameObject ItemClone = Instantiate(InventorryUIPanel.instance.itemInventory, posSlot.gameObject.transform);
                            ItemClone.transform.position = new Vector3(ItemClone.transform.position.x, ItemClone.transform.position.y + durations, ItemClone.transform.position.z);
                            SlotInventory Item = ItemClone.GetComponent<SlotInventory>();
                            Item.idItem = Item.lsIdItem[randIdItem];
                            Item.Icon.GetComponent<Image>().sprite = UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == Item.lsIdItem[randIdItem]).imageItem;

                            Destroy(ItemClone.GetComponent<ItemDragPosition>());
                            lsSlotGbEnemy.Add(ItemClone);
                        }

                    }
                }
            }
        }
        IEnumerator WaitBattleDelay()
        {

            yield return new WaitForSeconds(durations);
            SpawnEnemyEndBattle();
            isEndBattle = false;
            isRangeRemoved = false;
            CountState++;


        }

    }
    [System.Serializable]
    public class SlotInArea
    {
        public float slotCharacter, idCharacter;
    }
}