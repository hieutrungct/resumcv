using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle.UI.Result;
using RubikCasual.RewardInGame;
using RubikCasual.Roulette;
using UnityEngine;

namespace RubikCasual.Battle.UI
{
    public class UIGamePlay : MonoBehaviour
    {
        public PopupContinue popupContinue;
        public PopupResultWin popupResultWin;
        public InventorryUIPanel inventorryUIPanel;
        public RouletteController rouletteController;
        public GameObject TxtDame, ItemDrop, imageBackGround;
        public Canvas canvasUIGamePlay;
        public bool isSaveReward, chosePopupVictory;
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
                    popupResultWin.gameObject.SetActive(true);
                }
            }
            else
            {
                isSaveReward = false;
            }
        }
        void ShowInventory()
        {
            canvasUIGamePlay.sortingLayerName = Name_Sorting_Layer;
            inventorryUIPanel.gameObject.SetActive(true);
        }
        
    }
}
