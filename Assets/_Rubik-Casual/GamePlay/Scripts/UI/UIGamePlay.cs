using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle.UI.Result;
using RubikCasual.Data;
using RubikCasual.RewardInGame;
using RubikCasual.Roulette;
using TMPro;
using UnityEngine;

namespace RubikCasual.Battle.UI
{
    public class UIGamePlay : MonoBehaviour
    {
        public PopupContinue popupContinue;
        public PopupResultWin popupResultWin;
        public InventoryUIPanel inventoryUIPanel;
        public RouletteController rouletteController;
        public CardExpUp cardExpUp;
        public GameObject TxtDame, ItemDrop, imageBackGround;
        public Canvas canvasUIGamePlay;
        public bool isSaveReward, chosePopupVictory, isHaveChangeSlot, isDoneShowResult;
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
            if (BattleController.instance.gameState == GameState.END && !isDoneShowResult)
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
                isDoneShowResult = true;
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
            if (!popupResultWin.isClaim)
            {
                for (int i = 0; i < popupResultWin.NumberStarReward; i++)
                {
                    RewardWinLevelStage rewardWinStage = DataController.instance.stageAssets.levelStageAssetsData.lsRewardWinStage[i];
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

                popupResultWin.isClaim = true;

                ShowCardUpLevel();
            }
            else
            {
                ClickOutGamePlay();
            }

        }
        void ShowCardUpLevel()
        {
            popupResultWin.transform.Find("GbResult").gameObject.SetActive(false);
            popupResultWin.transform.Find("Button_Claim").Find("Text_Claim").GetComponent<TextMeshProUGUI>().text = "Ok";
            cardExpUp.gameObject.SetActive(true);
            cardExpUp.SetAnimExp(BattleController.instance.expBonus + (int)System.Math.Floor(BattleController.instance.expBonus * 0.1f), 0);
        }
        public void ClickOutGamePlay()
        {
            if (cardExpUp.isShowAnimDone)
            {
                bl_SceneLoaderManager.LoadScene(NameScene.HOME_SCENE);
            }
        }
    }
}
