using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rubik_Casual.StageLevel.UI;
using RubikCasual.DailyItem;
using RubikCasual.Stage;
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
        public Transform levelClonePos;
        public LevelUI levelUI;
        public List<LevelUI> listLevelUIClone;
        public Button btnClose;
        public int numberEnergy;
        GameObject stageLevelClone;

        void Start()
        {
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
                if (item.isItemTop)
                {
                    levelUIClone.lvlUITop.SetActive(true);
                    levelUIClone.lvlUIBottom.SetActive(false);
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
                    levelUIClone.lvlUITop.SetActive(false);
                    levelUIClone.lvlUIBottom.SetActive(true);
                    foreach (var path in levelUIClone.Path)
                    {
                        if (path.name == "Path_Bottom")
                        {
                            path.SetActive(true);
                        }

                    }
                }
                foreach (var path in levelUIClone.Path)
                {
                    if (path.name != "Path_Top" && path.name != "Path_Bottom")
                    {
                        path.SetActive(true);
                    }
                }


                listLevelUIClone.Add(levelUIClone);
            }
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
