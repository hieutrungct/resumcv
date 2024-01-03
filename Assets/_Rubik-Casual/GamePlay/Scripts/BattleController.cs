using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Rubik.Axie;
using RubikCasual.Battle.Calculate;
using RubikCasual.Battle.Inventory;
using RubikCasual.Battle.UI;
using RubikCasual.Data;
using RubikCasual.Data.Waifu;
using RubikCasual.Tool;
using Sirenix.OdinInspector;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public class BattleController : MonoBehaviour
    {
        public MapBattleController mapBattleController, dameSlotTxtController;
        public CharacterInBattle HeroInBattle, EnemyInBattle, BossInBattle;
        public WaifuAssets waifuAssets;
        public EnemyAssets enemyAssets;
        public GamePlayUI gamePlayUI;
        public List<SlotInArea> HeroInArea;
        public List<GameObject> lsSlotGbEnemy, lsSlotGbHero;
        public List<int> idCurrentTeam = new List<int>();
        public DataController dataController;
        const string Layer_Attack = "Character_Attack",
        Layer_Attacked = "Character_Attacked",
        Layer_Character = "Character";
        bool isEndBattle = false, isRangeRemoved = false;
        public bool isUpdateDmgEnemy = false;
        int CountState = 1, numberSlot = 5;
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
            StartCoroutine(CreateBattlefield());
            // Cooldown();
        }
        void Update()
        {

            CheckHeroInBattle();
            BaseState(gameState);
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
            CreateAreaHeroStart(HeroInArea);
            CreateAreaEnemyStart(dataController.stageAssets.lsConvertStageAssetsData);

        }

        void BaseState(GameState state)
        {

            // if (lsSlotGbEnemy.Count != 0)
            // {
            //     UpdateHpBarEnemyForState(lsSlotGbEnemy.FirstOrDefault(f => f != null && f.GetComponent<CharacterInBattle>() != null).GetComponent<CharacterInBattle>().isCompleteMove);
            //     // if (lsSlotGbEnemy.LastOrDefault(f => f != null && f.GetComponent<CharacterInBattle>() != null).GetComponent<CharacterInBattle>().isCompleteMove)
            //     // {
            //     //     UnityEngine.Debug.Log(lsSlotGbEnemy.LastOrDefault(f => f != null && f.GetComponent<CharacterInBattle>() != null).GetComponent<CharacterInBattle>().isCompleteMove);
            //     // }
            // }
            switch (state)
            {
                case GameState.START:
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

        public List<float> lsIdTeam = new List<float>();
        IEnumerator CreateBattlefield()
        {
            yield return new WaitForSeconds(0.25f);
            dataController = DataController.instance;

            HeroInArea.Clear();
            lsIdTeam = dataController.playerData.userData.CurentTeam;

            for (int i = 0; i < numberSlot; i++)
            {
                SlotInArea slotInArea = new SlotInArea();
                if (lsIdTeam.Count > i)
                {
                    slotInArea.idCharacter = lsIdTeam[i];
                }
                else
                {
                    slotInArea.idCharacter = 0;
                }
                slotInArea.slotCharacter = i + 1;
                HeroInArea.Add(slotInArea);
            }
            CreateAreaHeroStart(HeroInArea);
            CreateAreaEnemyStart(dataController.stageAssets.lsConvertStageAssetsData);
        }
        void CheckHeroInBattle()
        {
            if (lsSlotGbEnemy.Count == mapBattleController.lsPosEnemySlot.Count * mapBattleController.lsPosEnemySlot[0].lsPosCharacterSlot.Count)
            {
                int count = 0;
                for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                {
                    for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                    {
                        if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null)
                        {
                            CharacterInBattle enemyInBattle = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();
                            SkeletonAnimation Enemy = enemyInBattle.skeletonCharacterAnimation;
                            if (Enemy.AnimationName == NameAnim.Anim_Character_Idle)
                            {

                                Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Map";
                                Enemy.GetComponent<MeshRenderer>().sortingOrder = 1;
                            }
                        }
                        count++;
                    }
                }
            }
            {
                int countCheck = 0;
                foreach (var item in lsSlotGbHero)
                {
                    if (item == null)
                    {
                        countCheck++;
                    }
                }
                if (countCheck == 5)
                {
                    gameState = GameState.END;
                }
            }
        }
        SkeletonAnimation SpawnCharacter(Transform poscharacterInBattle, SkeletonAnimation WaifuCharacter)
        {
            SkeletonAnimation character = Instantiate(WaifuCharacter);
            character.transform.localScale = waifuAssets.transform.localScale * 2f / 3f;
            character.gameObject.transform.SetParent(poscharacterInBattle);
            character.gameObject.transform.position = poscharacterInBattle.position;
            character.loop = true;
            character.AnimationName = NameAnim.Anim_Character_Idle;

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
                    CharacterInBattle heroInBattle = Instantiate(HeroInBattle, posSlot.gameObject.transform);
                    heroInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;
                    heroInBattle.indexOfSlot = index;

                    SkeletonAnimation Hero = SpawnCharacter(heroInBattle.PosCharacter, waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString()));

                    CharacterDragPosition CharacterHero = Hero.gameObject.AddComponent<CharacterDragPosition>();
                    CharacterHero.CharacterSke = waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString());
                    CharacterHero.posCharacter = posSlot.gameObject.transform;
                    CharacterHero.oriIndex = index;
                    heroInBattle.skeletonCharacterAnimation = Hero;
                    heroInBattle.infoWaifuAsset = waifuAssets.infoWaifuAssets.lsInfoWaifuAssets.Find(f => f.ID == lsHeroInArea[index].idCharacter);

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
        void CreateAreaEnemyStart(List<ConvertStageAssetsData> lsConvertStageAssetsDatas)
        {

            foreach (var item in lsSlotGbEnemy)
            {
                if (item != null && item.GetComponent<CharacterInBattle>() != null)
                {
                    item.GetComponent<CharacterInBattle>().healthBar.gameObject.transform.SetParent(item.GetComponent<CharacterInBattle>().cooldownSkillBar.gameObject.transform.parent);
                }
                Destroy(item);
            }
            lsSlotGbEnemy.Clear();
            int Count = 0;
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                int index = i;
                ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[index];

                for (int j = 0; j < lsPosSlot.lsPosCharacterSlot.Count; j++)
                {
                    int idValueInSlot = dataController.stageAssets.GetNameAndId(dataController.stageAssets.lsConvertStageAssetsData[index].lsValueSlot[j]).intValue;
                    string valueSlotStage = dataController.stageAssets.GetNameAndId(dataController.stageAssets.lsConvertStageAssetsData[index].lsValueSlot[j]).stringValue;
                    PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[j];

                    if (idValueInSlot != -1)
                    {
                        if (valueSlotStage != "IndexItem")
                        {
                            // UnityEngine.Debug.Log(enemyAssets.WaifuEnemyAssetDatas.FirstOrDefault(f => f.Index == enemyAssets.lsIdEnemy[indexRand]).Is_Boss);
                            mapBattleController.lsPosEnemySlot[index].lsPosCharacterSlot[j].id = idValueInSlot;
                            attribute = dataController.stageAssets.lsConvertStageAssetsData[index].Attribute;

                            CharacterInBattle enemyInBattle = Instantiate(EnemyInBattle, posSlot.gameObject.transform);
                            enemyInBattle.indexOfSlot = Count;
                            enemyInBattle.isEnemy = true;
                            enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;
                            enemyInBattle.cooldownSkillBar.gameObject.SetActive(false);
                            if (i > 0)
                            {
                                enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                            }
                            SkeletonAnimation Enemy = SpawnCharacter(enemyInBattle.PosCharacter, enemyAssets.Get2D(idValueInSlot.ToString()));
                            // Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Enemy";


                            enemyInBattle.gameObject.transform.localScale = new Vector3(-enemyInBattle.gameObject.transform.localScale.x, enemyInBattle.gameObject.transform.localScale.y, enemyInBattle.gameObject.transform.localScale.z);
                            enemyInBattle.skeletonCharacterAnimation = Enemy;
                            enemyInBattle.infoWaifuAsset = enemyAssets.infoEnemyAssets.lsInfoWaifuAssets.Find(f => f.Code == idValueInSlot);
                            enemyInBattle.healthBar.gameObject.transform.SetParent(dameSlotTxtController.gameObject.transform);

                            enemyInBattle.cooldownAttackBar.value = 0;
                            enemyInBattle.cooldownSkillBar.value = 0;
                            enemyInBattle.healthBar.value = 1;
                            enemyInBattle.Rage = 0;
                            enemyInBattle.infoWaifuAsset.ATK = attribute * enemyInBattle.infoWaifuAsset.ATK;
                            enemyInBattle.infoWaifuAsset.HP = attribute * enemyInBattle.infoWaifuAsset.HP;
                            enemyInBattle.infoWaifuAsset.DEF = attribute * enemyInBattle.infoWaifuAsset.DEF;
                            enemyInBattle.HpNow = enemyInBattle.infoWaifuAsset.HP;
                            lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                        }
                        else
                        {

                            GameObject ItemClone = Instantiate(InventorryUIPanel.instance.itemInventory, posSlot.gameObject.transform);
                            ItemClone.transform.position = new Vector3(ItemClone.transform.position.x, ItemClone.transform.position.y + durations, ItemClone.transform.position.z);
                            SlotInventory Item = ItemClone.GetComponent<SlotInventory>();
                            Item.idItem = idValueInSlot;
                            Item.Icon.GetComponent<Image>().sprite = dataController.itemData.InfoItems.FirstOrDefault(f => f.id == idValueInSlot).imageItem;

                            Destroy(ItemClone.GetComponent<ItemDragPosition>());
                            lsSlotGbEnemy.Add(ItemClone);
                        }

                    }
                    else
                    {
                        lsSlotGbEnemy.Add(null);

                    }
                    Count++;
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
            if (lsSlotGbEnemy.Count == 25)
            {
                UpdateHpBarEnemyForState();
            }
            CheckEndBattle();
            if (!isEndBattle)
            {
                if (lsSlotGbEnemy[2] != null && lsSlotGbEnemy[2].GetComponent<CharacterInBattle>() != null && lsSlotGbEnemy[2].GetComponent<CharacterInBattle>().isBoss)
                {
                    for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                    {
                        if (lsSlotGbHero[i] != null)
                        {
                            CharacterInBattle Hero = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                            CharacterInBattle Enemy = lsSlotGbEnemy[2].GetComponent<CharacterInBattle>();
                            float randomCooldownTimeHero = (float)UnityEngine.Random.Range(1, 10);
                            float randomCooldownTimeEnemy = (float)UnityEngine.Random.Range(1, 10);
                            if (!Hero.isAttack && !Enemy.isAttack && !Hero.isUseSkill)
                            {
                                if (Hero.cooldownSkillBar.value == 1)
                                {
                                    CharacterUseSkill(Hero);

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
                                    BossUseSkill(Enemy);
                                    // Enemy.cooldownSkillBar.value = 0;
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
                else
                {
                    for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                    {
                        if (lsSlotGbHero[i] != null && lsSlotGbEnemy[i] != null && lsSlotGbEnemy[i].GetComponent<CharacterInBattle>() != null)
                        {
                            CharacterInBattle Hero = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                            CharacterInBattle Enemy = lsSlotGbEnemy[i].GetComponent<CharacterInBattle>();
                            float randomCooldownTimeHero = (float)UnityEngine.Random.Range(1, 10);
                            float randomCooldownTimeEnemy = (float)UnityEngine.Random.Range(1, 10);
                            if (!Hero.isAttack && !Enemy.isAttack && !Hero.isUseSkill)
                            {
                                if (Hero.cooldownSkillBar.value == 1)
                                {
                                    CharacterUseSkill(Hero);
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


            }
            if (isEndBattle)
            {
                gameState = GameState.END_BATTLE;
            }

        }
        void UpdateHpBarEnemyForState()
        {


            int count = 0;
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                {
                    if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null)
                    {
                        CharacterInBattle enemyInBattle = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();
                        enemyInBattle.healthBar.gameObject.transform.SetParent(dameSlotTxtController.transform);

                        if (count < 5 && enemyInBattle.HpNow == 0)
                        {
                            enemyInBattle.healthBar.gameObject.transform.SetParent(enemyInBattle.cooldownAttackBar.gameObject.transform.parent);
                        }

                    }
                    count++;
                }

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

        void BossUseSkill(CharacterInBattle EnemyInBattle)
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
                    AnimHero.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attacked;
                    AnimHero.AnimationName = NameAnim.Anim_Character_Attacked;
                    AnimHero.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
                    AnimHero.AnimationState.Complete += delegate
                    {
                        AnimHero.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                        AnimHero.AnimationName = NameAnim.Anim_Character_Idle;
                    };


                    float OldHp = HeroInBattle.HpNow;
                    Calculator.CalculateHealth(EnemyInBattle, HeroInBattle);

                    GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, dameSlotTxtController.lsPosHeroSlot.lsPosCharacterSlot[index].transform);

                    txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - HeroInBattle.HpNow)).ToString();
                    Tween animTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durations, durations);
                    txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
                    animTxtDame.OnComplete(() =>
                    {
                        Destroy(txtDame);
                    });
                    CheckHpEnemy(lsSlotGbHero[index]);
                }
            }

        }
        void CharacterUseSkill(CharacterInBattle CharacterAttack)
        {
            CharacterAttack.skeletonCharacterAnimation.AnimationName = NameAnim.Anim_Character_Skill;
            CharacterAttack.skeletonCharacterAnimation.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attack;
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
                for (int j = 0; j < mapBattleController.lsPosEnemySlot[0].lsPosCharacterSlot.Count; j++)
                {
                    if ((count - CharacterAttack.indexOfSlot) % 5 == 0)
                    {
                        // UnityEngine.Debug.Log(count);
                        if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null)
                        {

                            SkeletonAnimation AnimEnemy = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
                            AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attacked;
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
                            Tween animTxtDame = txtDame.transform.DOMoveY(txtDame.transform.position.y + durations, durations);
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

        void CharacterAtackAnimation(GameObject CharacterAttacked, GameObject CharacterAttack)
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

            StartCoroutine(MoveBackDelay(CharacterAttack, CharacterAttacked, durations));
        }
        IEnumerator MoveBackDelay(GameObject CharacterAttack, GameObject CharacterAttacked, float delay)
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
                // CharacterAttackAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                CharacterAttackAnim.AnimationName = NameAnim.Anim_Character_Idle;
            };
            yield return new WaitForSeconds(delay / 2);
            CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Attacked;
            CharacterAttackedAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Attacked, false);
            CharacterAttackedAnim.AnimationState.Complete += delegate
            {
                // CharacterAttackedAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                CharacterAttackedAnim.AnimationName = NameAnim.Anim_Character_Idle;
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
            txtDame.transform.DOMoveY(txtDame.transform.position.y + delay, delay);

            yield return new WaitForSeconds(delay / 2);


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

            yield return new WaitForSeconds(delay / 2);
            Destroy(txtDame);

        }
        void CheckHpEnemy(GameObject gbEnemy)
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

        void EndBattleMoveCharacter()
        {

            if (!isRangeRemoved)
            {

                for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                {
                    int index = i;
                    if (lsSlotGbEnemy[index] != null)
                    {
                        lsSlotGbEnemy[index].transform.SetParent(mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[index].transform);
                        if (lsSlotGbEnemy[index].GetComponent<CharacterInBattle>() != null)
                        {
                            CharacterInBattle EnemyClone = lsSlotGbEnemy[index].GetComponent<CharacterInBattle>();
                            EnemyClone.healthBar.gameObject.transform.SetParent(EnemyClone.cooldownAttackBar.gameObject.transform.parent);
                            EnemyClone.healthBar.gameObject.SetActive(false);
                        }
                        Tween TMoveEnemy = lsSlotGbEnemy[index].transform.DOMoveX(mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[index].transform.position.x, durations * 2);
                        if (lsSlotGbEnemy[index].GetComponent<CharacterInBattle>() != null)
                        {
                            CharacterInBattle EnemyClone = lsSlotGbEnemy[index].GetComponent<CharacterInBattle>();
                            TMoveEnemy.OnComplete(() =>
                            {
                                Destroy(EnemyClone.gameObject);
                                // UnityEngine.Debug.Log(EnemyClone.isCompleteMove + " " + EnemyClone.indexOfSlot);
                            });
                        }
                        else
                        {
                            MoveItem(lsSlotGbEnemy[index], index, TMoveEnemy);
                        }

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
                    if (i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count && lsSlotGbHero[i] != null)
                    {
                        lsSlotGbHero[i].GetComponent<CharacterInBattle>().isAttack = false;
                        // lsSlotGbHero[i].GetComponent<CharacterInBattle>().skeletonCharacterAnimation.AnimationName = Anim_Character_Idle;
                    }
                    for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                    {
                        // UnityEngine.Debug.Log(lsSlotGbEnemy.Count / 5);
                        if (i != (lsSlotGbEnemy.Count / 5))
                        {
                            // UnityEngine.Debug.Log("Count = " + Count);
                            if (lsSlotGbEnemy[Count] != null)
                            {
                                lsSlotGbEnemy[Count].transform.SetParent(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform);
                                if (lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>() != null)
                                {
                                    CharacterInBattle EnemyClone = lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>();
                                    EnemyClone.indexOfSlot = Count;
                                    EnemyClone.healthBar.gameObject.transform.SetParent(EnemyClone.cooldownAttackBar.gameObject.transform.parent);
                                    EnemyClone.cooldownSkillBar.gameObject.transform.SetParent(EnemyClone.cooldownAttackBar.gameObject.transform.parent);
                                    EnemyClone.healthBar.gameObject.SetActive(false);
                                    EnemyClone.cooldownSkillBar.gameObject.SetActive(false);
                                }
                                Tween TMoveEnemy = lsSlotGbEnemy[Count].transform.DOMoveX(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform.position.x, durations * 2);
                                if (lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>() != null)
                                {

                                    CharacterInBattle EnemyClone = lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>();
                                    TMoveEnemy.OnComplete(() =>
                                    {
                                        if (!EnemyClone.isBoss)
                                        {
                                            EnemyClone.healthBar.gameObject.transform.SetParent(dameSlotTxtController.transform);
                                            EnemyClone.healthBar.gameObject.SetActive(true);
                                        }
                                        else
                                        {
                                            EnemyClone.cooldownSkillBar.gameObject.transform.SetParent(dameSlotTxtController.transform);
                                            EnemyClone.healthBar.gameObject.transform.SetParent(EnemyClone.cooldownSkillBar.gameObject.transform);
                                            EnemyClone.healthBar.gameObject.SetActive(true);
                                            EnemyClone.cooldownSkillBar.gameObject.SetActive(true);
                                        }

                                        // Debug.Log(EnemyClone.indexOfSlot);
                                    });
                                }

                                if (i == 0 && lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>() != null)
                                {

                                    CharacterInBattle EnemyClone = lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>();

                                    EnemyClone.indexOfSlot = Count;
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
        void MoveItem(GameObject gb, int i, Tween TMoveEnemy)
        {
            TMoveEnemy.OnComplete(() =>
            {
                if (gb.GetComponent<SlotInventory>() != null && lsSlotGbHero[i] != null)
                {
                    int idItem = gb.GetComponent<SlotInventory>().idItem;
                    Calculator.CheckItemCalculate(idItem, lsSlotGbHero[i].GetComponent<CharacterInBattle>());
                    StartCoroutine(MovePopup.ShowTxtDame(gb, UIGamePlay.instance.TxtDame, mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i].transform.position, dataController.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, dataController.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
                    if (lsSlotGbHero[i].GetComponent<CharacterInBattle>().HpNow <= 0)
                    {
                        SkeletonAnimation heroAnim = lsSlotGbHero[i].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
                        heroAnim.AnimationName = NameAnim.Anim_Character_Die;
                        heroAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Die, false);
                        heroAnim.AnimationState.Complete += delegate
                        {
                            Destroy(lsSlotGbHero[i]);
                        };
                    }
                }
                else
                {
                    Destroy(gb);
                }
            });



        }
        float attribute = 1;
        void SpawnEnemyEndBattle(int StageValue)
        {
            if (lsSlotGbEnemy.Count < mapBattleController.lsPosEnemySlot.Count * mapBattleController.lsPosEnemySlot[0].lsPosCharacterSlot.Count)
            {
                ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[mapBattleController.lsPosEnemySlot.Count - 1];

                int Count = (mapBattleController.lsPosEnemySlot.Count - 1) * mapBattleController.lsPosEnemySlot[0].lsPosCharacterSlot.Count;
                for (int i = 0; i < mapBattleController.lsPosEnemySlot[0].lsPosCharacterSlot.Count; i++)
                {
                    int index = i;

                    int idValueInSlot = dataController.stageAssets.GetNameAndId(dataController.stageAssets.lsConvertStageAssetsData.FirstOrDefault(f => f.Stage == StageValue).lsValueSlot[index]).intValue;
                    string valueSlotStage = dataController.stageAssets.GetNameAndId(dataController.stageAssets.lsConvertStageAssetsData.FirstOrDefault(f => f.Stage == StageValue).lsValueSlot[index]).stringValue;

                    PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[index];

                    if (idValueInSlot != -1)
                    {
                        if (valueSlotStage != "IndexItem")
                        {
                            lsPosSlot.lsPosCharacterSlot[index].id = idValueInSlot;
                            attribute *= dataController.stageAssets.lsConvertStageAssetsData.FirstOrDefault(f => f.Stage == StageValue).Attribute;
                            if (dataController.characterAssets.enemyAssets.WaifuEnemyAssetDatas.Find(f => f.Index == idValueInSlot).Is_Boss)
                            {
                                CharacterInBattle enemyInBattle = Instantiate(BossInBattle, posSlot.gameObject.transform);
                                enemyInBattle.indexOfSlot = Count;
                                enemyInBattle.isEnemy = true;
                                enemyInBattle.isBoss = true;
                                enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;

                                enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                                enemyInBattle.cooldownSkillBar.gameObject.SetActive(true);

                                SkeletonAnimation Enemy = SpawnCharacter(enemyInBattle.PosCharacter, enemyAssets.Get2D(idValueInSlot.ToString()));
                                // Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Enemy";

                                enemyInBattle.gameObject.transform.localScale = new Vector3(-enemyInBattle.gameObject.transform.localScale.x, enemyInBattle.gameObject.transform.localScale.y, enemyInBattle.gameObject.transform.localScale.z);
                                enemyInBattle.skeletonCharacterAnimation = Enemy;
                                enemyInBattle.infoWaifuAsset = enemyAssets.infoEnemyAssets.lsInfoWaifuAssets.Find(f => f.Code == idValueInSlot);
                                enemyInBattle.healthBar.gameObject.transform.SetParent(dameSlotTxtController.gameObject.transform);

                                enemyInBattle.cooldownAttackBar.value = 0;
                                enemyInBattle.cooldownSkillBar.value = 0;
                                enemyInBattle.healthBar.value = 1;
                                enemyInBattle.Rage = 0;
                                enemyInBattle.infoWaifuAsset.ATK = attribute * enemyInBattle.infoWaifuAsset.ATK;
                                enemyInBattle.infoWaifuAsset.HP = attribute * enemyInBattle.infoWaifuAsset.HP;
                                enemyInBattle.infoWaifuAsset.DEF = attribute * enemyInBattle.infoWaifuAsset.DEF;

                                enemyInBattle.txtHealthBar.text = enemyInBattle.infoWaifuAsset.HP.ToString() + "/" + enemyInBattle.infoWaifuAsset.HP.ToString();

                                enemyInBattle.HpNow = enemyInBattle.infoWaifuAsset.HP * attribute;


                                lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                            }
                            else
                            {
                                CharacterInBattle enemyInBattle = Instantiate(EnemyInBattle, posSlot.gameObject.transform);
                                enemyInBattle.indexOfSlot = Count;
                                enemyInBattle.isEnemy = true;

                                enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;

                                enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                                enemyInBattle.cooldownSkillBar.gameObject.SetActive(false);

                                SkeletonAnimation Enemy = SpawnCharacter(enemyInBattle.PosCharacter, enemyAssets.Get2D(idValueInSlot.ToString()));
                                // Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Enemy";

                                enemyInBattle.gameObject.transform.localScale = new Vector3(-enemyInBattle.gameObject.transform.localScale.x, enemyInBattle.gameObject.transform.localScale.y, enemyInBattle.gameObject.transform.localScale.z);
                                enemyInBattle.skeletonCharacterAnimation = Enemy;
                                enemyInBattle.infoWaifuAsset = enemyAssets.infoEnemyAssets.lsInfoWaifuAssets.Find(f => f.Code == idValueInSlot);
                                enemyInBattle.healthBar.gameObject.transform.SetParent(dameSlotTxtController.gameObject.transform);

                                enemyInBattle.cooldownAttackBar.value = 0;
                                enemyInBattle.cooldownSkillBar.value = 0;
                                enemyInBattle.healthBar.value = 1;
                                enemyInBattle.Rage = 0;
                                enemyInBattle.infoWaifuAsset.ATK = attribute * enemyInBattle.infoWaifuAsset.ATK;
                                enemyInBattle.infoWaifuAsset.HP = attribute * enemyInBattle.infoWaifuAsset.HP;
                                enemyInBattle.infoWaifuAsset.DEF = attribute * enemyInBattle.infoWaifuAsset.DEF;

                                enemyInBattle.HpNow = enemyInBattle.infoWaifuAsset.HP * attribute;
                                lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                            }
                        }
                        else
                        {

                            GameObject ItemClone = Instantiate(InventorryUIPanel.instance.itemInventory, posSlot.gameObject.transform);
                            ItemClone.transform.position = new Vector3(ItemClone.transform.position.x, ItemClone.transform.position.y + durations, ItemClone.transform.position.z);
                            SlotInventory Item = ItemClone.GetComponent<SlotInventory>();
                            Item.idItem = idValueInSlot;
                            Item.Icon.GetComponent<Image>().sprite = dataController.itemData.InfoItems.FirstOrDefault(f => f.id == idValueInSlot).imageItem;

                            Destroy(ItemClone.GetComponent<ItemDragPosition>());
                            lsSlotGbEnemy.Add(ItemClone);
                        }


                    }
                    else
                    {
                        lsSlotGbEnemy.Add(null);

                    }
                    Count++;
                }




            }
        }
        IEnumerator WaitBattleDelay()
        {

            yield return new WaitForSeconds(durations);
            SpawnEnemyEndBattle(CountState + 5);
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