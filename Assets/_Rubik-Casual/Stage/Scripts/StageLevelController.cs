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

namespace RubikCasual.StageLevel
{
    public class StageLevelController : MonoBehaviour
    {
        public TextMeshProUGUI textCoins, textGems, textEnergy;
        public ItemData itemdata;
        public StageDataController stageDataController;
        public Transform levelClonePos, infoLevelPos;
        public InfoLevelController infoLevel, infoLevelClone;
        public LevelUI levelUI;
        public List<LevelUI> listLevelUIClone;
        public Button btnClose;
        public int numberEnergy;

        GameObject stageLevelClone;
        public static StageLevelController instance;

        void Start()
        {
            instance = this;
            btnClose.onClick.AddListener(() => { destroyStageLevel(); });
            loadItem();
        }
        public void destroyStageLevel()
        {
            Destroy(stageLevelClone);
        }
        public void setUp(int idStage, GameObject stageLevelClones)
        {
            stageLevelClone = stageLevelClones;
            createLevelUI(idStage);
        }
        void createLevelUI(int idStage)
        {
            var listLevelInStage = stageDataController.stages.FirstOrDefault(f => f.idStage == idStage);
            foreach (var item in listLevelInStage.levelInStages)
            {
                var levelUIClone = Instantiate(levelUI, levelClonePos);
                levelUIClone.textLevel.text = item.idLvl.ToString();
                if (item.isLevelPresent)
                {
                    levelUIClone.focus.SetActive(true);
                    createInfoLevel(item.idLvl, listLevelInStage.idStage);
                }
                else if (!item.isLevelPresent)
                {
                    levelUIClone.imageLvl.sprite = levelUIClone.notCompleteSprite;
                }
                if (item.isCompleteLevel)
                {
                    levelUIClone.imageLvl.sprite = levelUIClone.normalSprite;
                    for (int i = 0; i < item.numberStarComplete; i++)
                    {
                        levelUIClone.star[i].SetActive(true);
                    }
                }

                levelUIClone.lvlUICentrel.AddComponent<Button>().onClick.AddListener(() =>
                {
                    if (!infoLevelClone && levelUIClone.imageLvl.sprite != levelUIClone.notCompleteSprite)
                    {
                        buttonClick(item.idLvl, listLevelInStage.idStage);
                        if (true)
                        {
                            levelUIClone.focus.SetActive(true);
                        }
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
                            if (!item.isLevelBonusComplete && infoLevelClone == null)
                            {
                                createInfoLevelBonus(item.idLvl, listLevelInStage.idStage, "Coins");
                            }
                        });
                        if (item.isLevelBonusComplete)
                        {
                            levelUIClone.lvlUITop.GetComponent<Image>().sprite = levelUIClone.notCompleteSprite;
                        }
                        foreach (var path in levelUIClone.Path)
                        {
                            if (path.name == "Path_Top")
                            {
                                path.SetActive(true);
                            }
                        }
                    }
                    else
                    {
                        levelUIClone.lvlUIBottom.SetActive(true);
                        levelUIClone.lvlUIBottom.AddComponent<Button>().onClick.AddListener(() =>
                        {
                            if (!item.isLevelBonusComplete && infoLevelClone == null)
                            {
                                createInfoLevelBonus(item.idLvl, listLevelInStage.idStage, "Gems");
                            }
                        });
                        if (item.isLevelBonusComplete)
                        {
                            levelUIClone.lvlUITop.GetComponent<Image>().sprite = levelUIClone.notCompleteSprite;
                        }
                        foreach (var path in levelUIClone.Path)
                        {
                            if (path.name == "Path_Bottom")
                            {
                                path.SetActive(true);
                            }
                        }
                    }
                }

                foreach (var path in levelUIClone.Path)
                {
                    if (path.name != "Path_Top" && path.name != "Path_Bottom")
                    {
                        path.SetActive(true);
                        if (item.idLvl == 1 && path.name == "Path_Left")
                        {
                            path.SetActive(false);
                        }
                        if ((item.idLvl == listLevelInStage.levelInStages[listLevelInStage.levelInStages.Count - 1].idLvl) && path.name == "Path_Right")
                        {
                            path.SetActive(false);
                        }
                    }
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
                        // var 
                        // infoLevelClone.rewardUi.iconReward.sprite = 
                        itemRewardClone.textValue.text = item.itemRewardBonus[0].numberItem.ToString();
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

        }
        void buttonClick(int idInfoLevel, int idStage)
        {
            createInfoLevel(idInfoLevel, idStage);
            Debug.Log("đang bấm");
        }
        void loadItem()
        {
            foreach (var itemLobby in itemdata.datalobby)
            {
                numberEnergy = itemLobby.numberItem;
                if (itemLobby.name == "Coins")
                {
                    textCoins.text = itemLobby.numberItem.ToString();
                }
                if (itemLobby.name == "Gems")
                {
                    textGems.text = itemLobby.numberItem.ToString();
                }
                if (itemLobby.name == "Energy")
                {
                    textEnergy.text = numberEnergy.ToString() + "/60";
                }
            }
        }
    }
}
