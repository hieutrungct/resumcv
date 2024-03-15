using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle.UI.Result;
using RubikCasual.Data;
using RubikCasual.RewardInGame;
using RubikCasual.Roulette;
using UnityEngine;

namespace RubikCasual.Battle.UI
{
    public class UIGamePlay : MonoBehaviour
    {
        public PopupContinue popupContinue;
        public PopupResultWin popupResultWin;
        public InventoryUIPanel inventoryUIPanel;
        public RouletteController rouletteController;
        public GameObject TxtDame, ItemDrop, imageBackGround;
        public Canvas canvasUIGamePlay;
        public bool isSaveReward, chosePopupVictory, isHaveChangeSlot;
        public VerticalView.VerticalViewLeft verticalViewLeft;
        public List<CharacterInBattle> lsHeroState = new List<CharacterInBattle>();
        public static UIGamePlay instance;
        const string Name_Sorting_Layer = "ShowPopup", Name_Sorting_Layer_origin = "Battle";
        void Awake()
        {
            instance = this;

        }
        void Start()
        {
        }
        void Update()
        {
            ShowPopupContinue();
            ShowInventory();
            SetUIVerticalLeft();
        }
        void SetUIVerticalLeft()
        {

            if (isHaveChangeSlot || lsHeroState.Count == 0)
            {
                BattleController.instance.SetSlotHero();
                if (lsHeroState.Count != 0)
                {
                    verticalViewLeft.SetDataPopup(lsHeroState);
                    verticalViewLeft.SetImageItem(lsHeroState);
                    isHaveChangeSlot = false;
                }
            }
            if (lsHeroState.Count != 0)
            {
                verticalViewLeft.SetSliderBar(lsHeroState);
                verticalViewLeft.SetShowInfo(lsHeroState);
                verticalViewLeft.ShowFocus(lsHeroState);

            }

        }
        void ShowPopupContinue()
        {
            if (BattleController.instance.gameState == GameState.END)
            {
                if (!isSaveReward)
                {
                    RewardInGamePanel.instance.SaveRewardInGame();
                    isSaveReward = true;
                }
                canvasUIGamePlay.sortingLayerName = Name_Sorting_Layer;
                if (!chosePopupVictory)
                {
                    popupContinue.gameObject.SetActive(true);
                }
                else
                {
                    //lấy số sao thông qua hero còn lại trên bàn 
                    int Count = 0;
                    foreach (GameObject hero in BattleController.instance.lsSlotGbHero)
                    {
                        if (hero != null)
                        {
                            Count++;
                        }
                    }

                    popupResultWin.NumberStarReward = Count;
                    popupResultWin.gameObject.SetActive(true);

                }
                DataController.instance.CaculateLevelUp();
            }
            else
            {
                isSaveReward = false;
            }
        }
        void ShowInventory()
        {
            canvasUIGamePlay.sortingLayerName = Name_Sorting_Layer;
            inventoryUIPanel.gameObject.SetActive(true);
        }

        public void ClaimReward()
        {
            Data.Player.UserData userData = DataController.instance.userData;
            if (!popupResultWin.GetComponent<PopupResultWin>().isClaim)
            {
                for (int i = 0; i < popupResultWin.GetComponent<PopupResultWin>().NumberStarReward; i++)
                {
                    RewardWinStage rewardWinStage = DataController.instance.stageAssets.stageAssetsData.lsRewardWinStage[i];
                    switch (rewardWinStage.nameItem)
                    {
                        case DailyItem.NameItem.Coins:
                            userData.Gold += rewardWinStage.valuableItem;
                            break;
                        case DailyItem.NameItem.Gems:
                            userData.Gem += rewardWinStage.valuableItem;
                            break;
                        case DailyItem.NameItem.Energy:
                            userData.Energy += rewardWinStage.valuableItem;
                            break;
                    }
                }

                popupResultWin.GetComponent<PopupResultWin>().isClaim = true;
                bl_SceneLoaderManager.LoadScene(NameScene.HOME_SCENE);
            }

        }
    }
}
