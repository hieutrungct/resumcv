using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RubikCasual.InfoLevel;
using RubikCasual.StageLevel.UI;
using RubikCasual.DailyItem;
using RubikCasual.StageData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RubikCasual.EnemyData;
using Spine.Unity.Editor;
using Rubik_Casual;

namespace RubikCasual.StageLevel
{
    public class StageLevelController : MonoBehaviour
    {


        public Transform levelClonePos, infoLevelPos;
        public InfoLevelController infoLevel, infoLevelClone;
        public LevelUI levelUI;
        public List<LevelUI> listLevelUIClone;
        public Button btnClose;
        public int numberEnergy;
        public ItemData itemdata;
        public StageDataController stageDataController;
        public EnemyDataController enemyDataController;
        int idStage;
        GameObject stageLevelClone;
        public static StageLevelController instance;

        void Start()
        {
            instance = this;
            btnClose.onClick.AddListener(() => { destroyStageLevel(); });

        }
        void Update()
        {

        }
        public void destroyStageLevel()
        {
            Destroy(stageLevelClone);
        }
        public void setUp(int idStages, GameObject stageLevelClones)
        {
            stageLevelClone = stageLevelClones;
            idStage = idStages;
            createLevelUI(idStage);
        }
        void createLevelUI(int idStage)
        {

            var listLevelInStage = stageDataController.stages.FirstOrDefault(f => f.idStage == idStage);
            foreach (var item in listLevelInStage.levelInStages)
            {
                var levelUIClone = Instantiate(levelUI, levelClonePos);
                levelUIClone.textLevel.text = item.idLvl.ToString();

                if (item.isCompleteLevel)
                {

                    levelUIClone.imageLvl.sprite = levelUIClone.normalSprite;
                    for (int i = 0; i < item.numberStarComplete; i++)
                    {
                        levelUIClone.star[i].SetActive(true);
                    }
                }
                else
                {
                    if (item.isLevelPresent)
                    {
                        levelUIClone.focusCentrel.SetActive(true);
                        createInfoLevel(item.idLvl, listLevelInStage.idStage);
                    }
                    else
                    {
                        levelUIClone.imageLvl.sprite = levelUIClone.notCompleteSprite;
                    }
                }

                //Tạo button cho mọi level kể cả màn chưa hoàn thành
                levelUIClone.lvlUICentrel.AddComponent<Button>().onClick.AddListener(() =>
                {
                    if (item.isCompleteLevel || item.isLevelPresent)
                    {
                        var checkFocus = listLevelUIClone.FirstOrDefault(f => f.focusCentrel.activeSelf || f.focusTop.activeSelf || f.focusBotton.activeSelf);
                        if (checkFocus != null)
                        {
                            checkFocus.focusTop.SetActive(false);
                            checkFocus.focusCentrel.SetActive(false);
                            checkFocus.focusBotton.SetActive(false);
                            Destroy(infoLevelClone.gameObject);
                        }
                        levelUIClone.focusCentrel.SetActive(true);
                        buttonClick(item.idLvl, listLevelInStage.idStage);

                    }
                });

                if (item.isBoss)
                {
                    levelUIClone.iconBoss.SetActive(true);
                    levelUIClone.textLevel.gameObject.SetActive(false);
                }

                if (item.itemBonus.Count != 0)
                {

                    if (item.isItemTop)
                    {
                        levelUIClone.lvlUITop.SetActive(true);
                        levelUIClone.lvlUITop.AddComponent<Button>().onClick.AddListener(() =>
                        {
                            if (!item.isLevelBonusComplete && item.isCompleteLevel)
                            {
                                var checkFocus = listLevelUIClone.FirstOrDefault(f => f.focusCentrel.activeSelf || f.focusTop.activeSelf || f.focusBotton.activeSelf);
                                if (checkFocus != null)
                                {
                                    checkFocus.focusTop.SetActive(false);
                                    checkFocus.focusCentrel.SetActive(false);
                                    checkFocus.focusBotton.SetActive(false);
                                    Destroy(infoLevelClone.gameObject);
                                }
                                levelUIClone.focusTop.SetActive(true);
                                createInfoLevelBonus(item.idLvl, listLevelInStage.idStage, "Coins");
                            }
                        });
                        if (item.isLevelBonusComplete)
                        {
                            levelUIClone.lvlUITop.GetComponent<Image>().sprite = levelUIClone.notCompleteSprite;
                        }
                        levelUIClone.Path.FirstOrDefault(f => f.name == "Path_Top").SetActive(true);
                    }
                    else
                    {
                        levelUIClone.lvlUIBottom.SetActive(true);
                        levelUIClone.lvlUIBottom.AddComponent<Button>().onClick.AddListener(() =>
                        {
                            if (!item.isLevelBonusComplete && item.isCompleteLevel)
                            {
                                var checkFocus = listLevelUIClone.FirstOrDefault(f => f.focusCentrel.activeSelf || f.focusTop.activeSelf || f.focusBotton.activeSelf);
                                if (checkFocus != null)
                                {
                                    checkFocus.focusTop.SetActive(false);
                                    checkFocus.focusCentrel.SetActive(false);
                                    checkFocus.focusBotton.SetActive(false);
                                    Destroy(infoLevelClone.gameObject);
                                }
                                levelUIClone.focusBotton.SetActive(true);
                                createInfoLevelBonus(item.idLvl, listLevelInStage.idStage, "Gems");
                            }
                        });
                        if (item.isLevelBonusComplete)
                        {
                            levelUIClone.lvlUITop.GetComponent<Image>().sprite = levelUIClone.notCompleteSprite;
                        }

                        levelUIClone.Path.FirstOrDefault(f => f.name == "Path_Bottom").SetActive(true);
                    }
                }


                if (item.idLvl == 1)
                {
                    levelUIClone.Path.FirstOrDefault(f => f.name == "Path_Right").SetActive(true);
                }
                else if (item.idLvl == listLevelInStage.levelInStages[listLevelInStage.levelInStages.Count - 1].idLvl)
                {
                    levelUIClone.Path.FirstOrDefault(f => f.name == "Path_Left").SetActive(true);
                }
                else
                {
                    levelUIClone.Path.FirstOrDefault(f => f.name == "Path_Right").SetActive(true);
                    levelUIClone.Path.FirstOrDefault(f => f.name == "Path_Left").SetActive(true);
                }


                listLevelUIClone.Add(levelUIClone);
            }
            
        }
        void createInfoLevel(int idInfoLevel, int idStage)
        {
            var levelInData = stageDataController.stages.FirstOrDefault(f => f.idStage == idStage).levelInStages;

            infoLevelClone = Instantiate(infoLevel, infoLevelPos);
            infoLevelClone.id = idInfoLevel;
            infoLevelClone.textNameStage.text = "STAGE " + idInfoLevel.ToString();


            foreach (var item in levelInData)
            {
                if (item.idLvl == idInfoLevel)
                {
                    foreach (var itemReward in item.itemRewardBonus)
                    {
                        var itemRewardClone = Instantiate(infoLevelClone.rewardUi, infoLevelClone.rewardUiPos);
                        itemRewardClone.iconReward.sprite = itemdata.InfoItems.FirstOrDefault(f => f.id == itemReward.idItem).imageItem;
                        itemRewardClone.iconReward.preserveAspect = true;
                        itemRewardClone.textValue.text = item.itemRewardBonus.FirstOrDefault(f => f.idItem == itemReward.idItem).numberItem.ToString();
                    }

                    foreach (var itemEnemy in item.enemyAtacks)
                    {
                        var enemyData = enemyDataController.enemy.FirstOrDefault(f => f.idEnemy == itemEnemy.idEnemy);
                        var itemEnemyClone = Instantiate(infoLevelClone.enemyUi, infoLevelClone.enemyUiPos);
                        var enemy = AssetLoader.instance.GetAvaByNameEn(enemyData.NameEnemyid);
                        var checkIdEnemyColor = enemyDataController.colorEnemies.FirstOrDefault(f => f.idColor == enemyData.idColor);

                        itemEnemyClone.backgroundColor.color = checkIdEnemyColor.backgroundColor;
                        itemEnemyClone.bottomGlowColor.color = checkIdEnemyColor.backgroundColor;
                        itemEnemyClone.mask.color = checkIdEnemyColor.backgroundColor;
                        itemEnemyClone.frameColor.color = checkIdEnemyColor.frameColor;
                        itemEnemyClone.backgroundValue.sprite = checkIdEnemyColor.backgroundValue;
                        itemEnemyClone.textValue.text = "x" + itemEnemy.numberEnemy.ToString();

                        itemEnemyClone.iconEnemy.skeletonDataAsset = enemy;
                        itemEnemyClone.iconEnemy.initialSkinName = enemy.GetSkeletonData(true).Skins.Items[1].Name;

                        for (int i = 0; i < enemyData.numberStar; i++)
                        {
                            itemEnemyClone.star[i].SetActive(true);
                        }
                        SpineEditorUtilities.ReinitializeComponent(itemEnemyClone.iconEnemy);

                    }

                    if (item.isCompleteLevel)
                    {
                        infoLevelClone.buttonFinish.sprite = infoLevelClone.unlockFinishNow;
                        infoLevelClone.lockFinishNow.enabled = false;
                    }
                }

            }

        }
        void createInfoLevelBonus(int idInfoLevel, int idStage, string nameItemBonnus)
        {
            var levelInData = stageDataController.stages.FirstOrDefault(f => f.idStage == idStage).levelInStages;

            infoLevelClone = Instantiate(infoLevel, infoLevelPos);
            infoLevelClone.id = idInfoLevel;
            infoLevelClone.textNameStage.text = nameItemBonnus + " Bonus";

            foreach (var item in levelInData)
            {
                if (item.idLvl == idInfoLevel)
                {
                    foreach (var itemBonus in item.itemBonus)
                    {
                        var itemRewardClone = Instantiate(infoLevelClone.rewardUi, infoLevelClone.rewardUiPos);
                        itemRewardClone.iconReward.sprite = itemdata.InfoItems.FirstOrDefault(f => f.id == itemBonus.idItem).imageItem;
                        itemRewardClone.iconReward.preserveAspect = true;
                        itemRewardClone.textValue.text = item.itemBonus[0].numberValueBonus.ToString();
                    }

                    foreach (var itemEnemy in item.enemyAtacks)
                    {
                        var enemyData = enemyDataController.enemy.FirstOrDefault(f => f.idEnemy == itemEnemy.idEnemy);
                        var itemEnemyClone = Instantiate(infoLevelClone.enemyUi, infoLevelClone.enemyUiPos);
                        var enemy = AssetLoader.instance.GetAvaByNameEn(enemyData.NameEnemyid);
                        var checkIdEnemyColor = enemyDataController.colorEnemies.FirstOrDefault(f => f.idColor == enemyData.idColor);

                        itemEnemyClone.backgroundColor.color = checkIdEnemyColor.backgroundColor;
                        itemEnemyClone.bottomGlowColor.color = checkIdEnemyColor.backgroundColor;
                        itemEnemyClone.mask.color = checkIdEnemyColor.backgroundColor;
                        itemEnemyClone.frameColor.color = checkIdEnemyColor.frameColor;
                        itemEnemyClone.backgroundValue.sprite = checkIdEnemyColor.backgroundValue;
                        itemEnemyClone.textValue.text = "x" + itemEnemy.numberEnemy.ToString();

                        itemEnemyClone.iconEnemy.skeletonDataAsset = enemy;
                        itemEnemyClone.iconEnemy.initialSkinName = enemy.GetSkeletonData(true).Skins.Items[1].Name;

                        for (int i = 0; i < enemyData.numberStar; i++)
                        {
                            itemEnemyClone.star[i].SetActive(true);
                        }
                        SpineEditorUtilities.ReinitializeComponent(itemEnemyClone.iconEnemy);

                    }
                    infoLevelClone.buttonFinish.gameObject.SetActive(false);
                    var Pos = new Vector3(5.8f, infoLevelClone.buttonFight.rectTransform.position.y, infoLevelClone.buttonFight.rectTransform.position.z);
                    infoLevelClone.buttonFight.rectTransform.position = Pos;
                    Debug.Log(infoLevelClone.buttonFight.rectTransform.position);
                }
            }

        }
        void buttonClick(int idInfoLevel, int idStage)
        {
            createInfoLevel(idInfoLevel, idStage);
            Debug.Log("đang bấm");
        }

    }
}
