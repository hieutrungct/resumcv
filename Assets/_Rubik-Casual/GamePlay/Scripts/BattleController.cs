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
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle
{

    public class BattleController : MonoBehaviour
    {
        public MapBattleController mapBattleController, dameSlotTxtController;
        public CharacterInBattle HeroInBattle, EnemyInBattle, BossInBattle;
        public GamePlayUI gamePlayUI;
        public List<SlotInArea> HeroInArea;
        public List<GameObject> lsSlotGbEnemy, lsSlotGbHero;
        public DataController dataController;
        public int expBonus;
        private float attribute = 1;
        public GameState gameState;
        SetAnimCharacter setAnimCharacter;
        bool isEndBattle = false, isRangeRemoved = false, isCompleteMove = true;
        int CountState = 1, numberSlot = 5;

        public static BattleController instance;
        void Awake()
        {
            instance = this;
            gameState = GameState.WAIT_BATTLE;
            expBonus = 100;

        }
        void Start()
        {
            dataController = DataController.instance;
            StartCoroutine(CreateBattlefield());
        }
        void Update()
        {
            CheckHeroInBattle();
            BaseState(gameState);
        }
        public void SetSlotHero()
        {

            List<CharacterInBattle> lsHeroState = new List<CharacterInBattle>();
            foreach (GameObject item in lsSlotGbHero)
            {
                if (item != null)
                {
                    lsHeroState.Add(item.GetComponent<CharacterInBattle>());
                }
                else
                {
                    lsHeroState.Add(null);
                }
            }
            UIGamePlay.instance.lsHeroState = lsHeroState;

        }
        public void StartGame()
        {

            if (gameState == GameState.WAIT_BATTLE && isCompleteMove && CountState != (dataController.stageAssets.lsConvertLevelStageAssetsData.Count + 1))
            {
                // UnityEngine.Debug.Log("h ms dc bấm");
                gameState = GameState.START;
            }
        }
        public void ResetGame()
        {
            foreach (var item in lsSlotGbEnemy)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }
            lsSlotGbEnemy.Clear();
            CountState = 1;
            attribute = 1;
            CreateAreaHeroStart(HeroInArea);
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                SpawnEnemyForStage(i, i);
            }
            // CreateAreaEnemyStart(dataController.stageAssets.lsConvertStageAssetsData);

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
                    gamePlayUI.txtTime.text = "Turn: " + CountState.ToString();
                    SetIsAttackAgain();
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
        void SetIsAttackAgain()
        {
            if (lsSlotGbHero.Count != 0)
            {
                for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                {
                    if (i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count && lsSlotGbHero[i] != null && lsSlotGbHero[i].GetComponent<CharacterInBattle>().isAttack)
                    {
                        lsSlotGbHero[i].GetComponent<CharacterInBattle>().isAttack = false;
                        // lsSlotGbHero[i].GetComponent<CharacterInBattle>().skeletonCharacterAnimation.AnimationName = Anim_Character_Idle;
                    }
                }
            }
        }

        IEnumerator CreateBattlefield()
        {
            yield return new WaitForSeconds(0.25f);

            HeroInArea.Clear();

            foreach (Data.Player.CurentTeam floatValue in dataController.playerData.userData.curentTeams)
            {
                int intValue = (int)floatValue.ID;
            }

            for (int i = 0; i < numberSlot; i++)
            {
                SlotInArea slotInArea = new SlotInArea();
                slotInArea.idCharacter = dataController.playerData.userData.curentTeams[i].ID;
                slotInArea.isSkin = dataController.playerData.userData.curentTeams[i].SkinCheck;
                slotInArea.slotCharacter = i + 1;
                HeroInArea.Add(slotInArea);
            }
            CreateAreaHeroStart(HeroInArea);
            // CreateAreaEnemyStart(dataController.stageAssets.lsConvertStageAssetsData);
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                SpawnEnemyForStage(i, i);
            }
            yield return new WaitForSeconds(0.05f);
            setAnimCharacter = gameObject.AddComponent<SetAnimCharacter>();
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

                    heroInBattle.SetCharacterInBattle(lsHeroInArea[index], posSlot, 0);

                    lsSlotGbHero.Add(heroInBattle.gameObject);
                }
                else
                {
                    lsSlotGbHero.Add(null);
                }

            }
        }


        void SpawnEnemyForStage(int idStage, int idRowSlot)
        {
            int index = idRowSlot;
            ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[index];
            try
            {
                for (int j = 0; j < lsPosSlot.lsPosCharacterSlot.Count; j++)
                {
                    int indexOfSlot = j + 5 * idRowSlot;
                    int idValueInSlot = dataController.stageAssets.GetNameAndId(dataController.stageAssets.lsConvertLevelStageAssetsData[idStage].lsValueSlot[j]).intValue;
                    string valueSlotStage = dataController.stageAssets.GetNameAndId(dataController.stageAssets.lsConvertLevelStageAssetsData[idStage].lsValueSlot[j]).stringValue;
                    PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[j];

                    if (idValueInSlot != -1)
                    {
                        if (valueSlotStage != "IndexItem")
                        {
                            // UnityEngine.Debug.Log(enemyAssets.WaifuEnemyAssetDatas.FirstOrDefault(f => f.Index == enemyAssets.lsIdEnemy[indexRand]).Is_Boss);
                            mapBattleController.lsPosEnemySlot[index].lsPosCharacterSlot[j].id = idValueInSlot;
                            switch (dataController.stageAssets.levelStageAssetsData.typeMap)
                            {
                                case CreateSkill.Panel.TypeMap.Default_Map:
                                    attribute = dataController.stageAssets.lsConvertLevelStageAssetsData[idStage].Attribute;
                                    break;
                                case CreateSkill.Panel.TypeMap.Infinity_Map:
                                    attribute *= dataController.stageAssets.lsConvertLevelStageAssetsData[idStage].Attribute;
                                    break;
                                    // case CreateSkill.Panel.TypeMap.Challenge_Map:
                                    // attribute = dataController.stageAssets.lsConvertStageAssetsData[idStage].Attribute;
                                    // break;
                            }
                            SlotInArea CharacterInArea = new SlotInArea();
                            CharacterInArea.idCharacter = idValueInSlot;
                            CharacterInArea.slotCharacter = indexOfSlot;
                            CharacterInBattle enemyInBattle;

                            if (dataController.characterAssets.enemyAssets.WaifuEnemyAssetDatas.Find(f => f.Index == idValueInSlot).Is_Boss)
                            {
                                enemyInBattle = Instantiate(BossInBattle, posSlot.gameObject.transform);
                                enemyInBattle.indexOfSlot = indexOfSlot;
                                enemyInBattle.isEnemy = true;
                                enemyInBattle.isBoss = true;
                                enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;

                                enemyInBattle.SetCharacterInBattle(CharacterInArea, posSlot, attribute);

                                enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                                enemyInBattle.cooldownSkillBar.gameObject.SetActive(true);

                            }
                            else
                            {
                                enemyInBattle = Instantiate(EnemyInBattle, posSlot.gameObject.transform);
                                enemyInBattle.indexOfSlot = indexOfSlot;
                                enemyInBattle.isEnemy = true;
                                enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;

                                enemyInBattle.SetCharacterInBattle(CharacterInArea, posSlot, attribute);

                                enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                                enemyInBattle.cooldownSkillBar.gameObject.SetActive(false);
                                if (index == 0)
                                {
                                    enemyInBattle.cooldownAttackBar.gameObject.SetActive(true);
                                }
                                enemyInBattle.skeletonCharacterAnimation.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_ShowPopup;

                            }
                            lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                        }
                        else
                        {

                            GameObject ItemClone = Instantiate(InventoryUIPanel.instance.itemInventory, posSlot.gameObject.transform);
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
                }

            }
            catch (System.Exception)
            {
                for (int j = 0; j < lsPosSlot.lsPosCharacterSlot.Count; j++)
                {
                    lsSlotGbEnemy.Add(null);
                }
            }
        }
        void Atack()
        {
            // UpdateHpBarEnemyForState();

            if (lsSlotGbEnemy[2] != null && lsSlotGbEnemy[2].GetComponent<CharacterInBattle>() != null && lsSlotGbEnemy[2].GetComponent<CharacterInBattle>().isBoss)
            {
                List<int> lsIndexHero = new List<int>();
                foreach (GameObject item in lsSlotGbHero)
                {
                    if (item != null)
                    {
                        int index = (int)item.GetComponent<CharacterInBattle>().infoWaifuAsset.ID;
                        lsIndexHero.Add(index);
                    }
                }

                // Debug.Log(MovePopup.RandomIntWithList(lsIndexHero));

                CharacterInBattle Enemy = lsSlotGbEnemy[2].GetComponent<CharacterInBattle>();
                float randomCooldownTimeEnemy = (float)UnityEngine.Random.Range(1, 10);

                if (!Enemy.isAttack && !Enemy.isUseSkill)
                {
                    float IdRandom = MovePopup.RandomIntWithList(lsIndexHero);
                    CharacterInBattle Hero = lsSlotGbHero.Find(f => f != null && f.GetComponent<CharacterInBattle>().infoWaifuAsset.ID == IdRandom).GetComponent<CharacterInBattle>();
                    if (!Hero.isUseSkill)
                    {
                        if (Enemy.HpNow == 0)
                        {
                            return;
                        }
                        if (Enemy.cooldownSkillBar.value == 1)
                        {
                            Enemy.cooldownSkillBar.value = 0;
                            setAnimCharacter.BossUseSkill(Enemy, dameSlotTxtController, lsSlotGbHero, durations);

                        }
                        else
                        {
                            if (Enemy.cooldownAttackBar.value == 1)
                            {
                                Enemy.cooldownAttackBar.value = 0;
                                setAnimCharacter.CharacterAtackAnimation(Hero.gameObject, Enemy.gameObject, dameSlotTxtController, durations);
                                // Debug.Log((i + 1) + " enemy atack");
                            }
                            if (Enemy.cooldownAttackBar.value == 0)
                            {
                                StartCoroutine(MovePopup.StartCooldown(Enemy.cooldownAttackBar, randomCooldownTimeEnemy));
                            }
                        }
                    }

                }


                for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                {
                    if (lsSlotGbHero[i] != null && !Enemy.isAttack && !Enemy.isUseSkill)
                    {
                        CharacterInBattle Hero = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                        if (!Hero.isUseSkill && !Hero.isAttack)
                        {
                            float randomCooldownTimeHero = (float)UnityEngine.Random.Range(1, 10);
                            {
                                if (Hero.HpNow != 0)
                                {

                                    if (Hero.cooldownAttackBar.value == 1)
                                    {
                                        Hero.cooldownAttackBar.value = 0;
                                        setAnimCharacter.CharacterAtackAnimation(Enemy.gameObject, Hero.gameObject, dameSlotTxtController, durations);
                                        // Debug.Log((i + 1) + " hero atack");
                                    }
                                    if (Hero.cooldownAttackBar.value == 0)
                                    {
                                        StartCoroutine(MovePopup.StartCooldown(Hero.cooldownAttackBar, randomCooldownTimeHero));
                                    }

                                }
                            }
                        }

                    }
                }
            }
            else
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
                            float randomCooldownTimeHero = (float)UnityEngine.Random.Range(1, 10);
                            float randomCooldownTimeEnemy = (float)UnityEngine.Random.Range(1, 10);
                            if (!Hero.isAttack && !Enemy.isAttack && !Hero.isUseSkill)
                            {
                                if (Hero.cooldownAttackBar.value == 1)
                                {
                                    Hero.cooldownAttackBar.value = 0;
                                    setAnimCharacter.CharacterAtackAnimation(Enemy.gameObject, Hero.gameObject, dameSlotTxtController, durations);

                                    // Debug.Log((i + 1) + " hero atack");

                                }
                                if (Hero.cooldownAttackBar.value == 0)
                                {
                                    StartCoroutine(MovePopup.StartCooldown(Hero.cooldownAttackBar, randomCooldownTimeHero));
                                }


                                // if (Enemy.cooldownSkillBar.value == 1)
                                // {
                                //     // CharacterUseSkill(Enemy);
                                //     Enemy.cooldownSkillBar.value = 0;
                                // }
                                // else
                                // {
                                if (Enemy.cooldownAttackBar.value == 1)
                                {
                                    Enemy.cooldownAttackBar.value = 0;
                                    setAnimCharacter.CharacterAtackAnimation(Hero.gameObject, Enemy.gameObject, dameSlotTxtController, durations);
                                    // Debug.Log((i + 1) + " enemy atack");

                                }
                                if (Enemy.cooldownAttackBar.value == 0)
                                {
                                    StartCoroutine(MovePopup.StartCooldown(Enemy.cooldownAttackBar, randomCooldownTimeEnemy));
                                }
                                // }
                            }
                        }
                    }
                }
                else
                {
                    gameState = GameState.END_BATTLE;
                }
            }
        }

        void CheckEndBattle()
        {

            int Count = 0, CountCheckAnimHero = 0, CountCurrentHero = 0;
            bool isDoneUseSkill = false;
            for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
            {
                if (lsSlotGbHero[i] == null || lsSlotGbEnemy[i] == null || lsSlotGbEnemy[i].GetComponent<CharacterInBattle>() == null)
                {
                    Count++;
                }
            }

            foreach (var gbHero in lsSlotGbHero)
            {
                if (gbHero != null)
                {
                    SkeletonAnimation HeroAnim = gbHero.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
                    if (HeroAnim.AnimationName == NameAnim.Anim_Character_Idle)
                    {
                        CountCheckAnimHero++;
                    }
                    CountCurrentHero++;
                }

            }

            if (CountCheckAnimHero == CountCurrentHero)
            {
                isDoneUseSkill = true;
            }
            if (mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count == Count)
            {
                if (isDoneUseSkill)
                {
                    isEndBattle = true;
                }
            }
        }
        float durations = 0.5f;




        void EndBattleMoveCharacter()
        {
            this.isCompleteMove = false;
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

                        }
                        Tween TMoveEnemy = lsSlotGbEnemy[index].transform.DOMoveX(mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[index].transform.position.x, durations * 2);
                        if (lsSlotGbEnemy[index].GetComponent<CharacterInBattle>() != null)
                        {
                            CharacterInBattle EnemyClone = lsSlotGbEnemy[index].GetComponent<CharacterInBattle>();
                            EnemyClone.healthBar.gameObject.SetActive(false);
                            EnemyClone.cooldownSkillBar.gameObject.SetActive(false);

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


                                    EnemyClone.healthBar.gameObject.SetActive(false);
                                    EnemyClone.cooldownSkillBar.gameObject.SetActive(false);
                                }
                                Tween TMoveEnemy = lsSlotGbEnemy[Count].transform.DOMoveX(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform.position.x, durations * 2);
                                Quaternion originRotate = lsSlotGbEnemy[Count].transform.localRotation;

                                // lsSlotGbEnemy[Count].transform.DORotateQuaternion();

                                if (lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>() != null)
                                {

                                    CharacterInBattle EnemyClone = lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>();

                                    TMoveEnemy.OnComplete(() =>
                                    {
                                        this.isCompleteMove = true;

                                        if (!EnemyClone.isBoss)
                                        {
                                            EnemyClone.healthBar.gameObject.SetActive(true);
                                        }
                                        else
                                        {
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
                    CharacterInBattle HeroTarget = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                    RubikCasual.DailyItem.infoItem infoItem = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem);
                    switch (infoItem.type)
                    {
                        case DailyItem.TypeItem.Heal:
                            switch (HeroTarget.healthBar.value)
                            {
                                case 1f:
                                    InventoryUIPanel.instance.CreateItemInInventory(gb, idItem);
                                    break;
                                default:
                                    SetAnimTxt(gb, i, idItem, HeroTarget);
                                    break;
                            }
                            break;
                        case DailyItem.TypeItem.Mana:
                            switch (HeroTarget.cooldownSkillBar.value)
                            {
                                case 1f:
                                    InventoryUIPanel.instance.CreateItemInInventory(gb, idItem);
                                    break;
                                default:
                                    SetAnimTxt(gb, i, idItem, HeroTarget);
                                    break;
                            }
                            break;
                        default:
                            SetAnimTxt(gb, i, idItem, HeroTarget);
                            break;
                    }
                }
                else
                {
                    Destroy(gb);
                }
            });
        }

        void SetAnimTxt(GameObject gbParent, int i, int idItem, CharacterInBattle HeroTarget)
        {
            Calculator.CheckItemCalculate(idItem, HeroTarget);
            StartCoroutine(MovePopup.ShowTxtDame(gbParent, UIGamePlay.instance.TxtDame, mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i].transform.position, dataController.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, dataController.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
            if (lsSlotGbHero[i].GetComponent<CharacterInBattle>().HpNow <= 0)
            {
                SkeletonAnimation heroAnim = lsSlotGbHero[i].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;

                heroAnim.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Die, false);
                heroAnim.AnimationState.Complete += delegate
                {
                    Destroy(lsSlotGbHero[i]);
                };
            }
        }

        IEnumerator WaitBattleDelay()
        {
            // khi clear xong 1 stage
            yield return new WaitForSeconds(durations);
            // khởi tạo quái ở hàng cuối cùng
            SpawnEnemyForStage(CountState + mapBattleController.lsPosEnemySlot.Count - 1, mapBattleController.lsPosEnemySlot.Count - 1);
            isEndBattle = false;
            isRangeRemoved = false;
            CountState++;
            if (dataController.stageAssets.lsConvertLevelStageAssetsData.Count < CountState && dataController.stageAssets.lsConvertLevelStageAssetsData.Count != 0)
            {
                gameState = GameState.END;
                UIGamePlay.instance.chosePopupVictory = true;
            }
            // phần thưởng sau khi kết thúc trận
            RewardInGame.RewardInGamePanel.instance.AddRewardTopBarGroup(2f);

        }

    }
    [System.Serializable]
    public class SlotInArea
    {
        public int slotCharacter, idCharacter;
        public bool isSkin;
    }
}