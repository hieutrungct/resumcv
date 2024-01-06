using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubikCasual.Battle.UI
{
    public class UIGamePlay : MonoBehaviour
    {
        public PopupContinue popupContinue;
        public InventorryUIPanel inventorryUIPanel;
        public GameObject TxtDame;
        public Canvas canvasUIGamePlay;
        public static UIGamePlay instance;
        const string Name_Sorting_Layer = "ShowPopup", Name_Sorting_Layer_origin = "Battle";
        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            ClickBtnContinue();
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
                canvasUIGamePlay.sortingLayerName = Name_Sorting_Layer;
                popupContinue.gameObject.SetActive(true);
            }
        }
        void ShowInventory()
        {
            canvasUIGamePlay.sortingLayerName = Name_Sorting_Layer;
            inventorryUIPanel.gameObject.SetActive(true);
        }
        void ClickBtnContinue()
        {
            popupContinue.btnContinue.onClick.RemoveAllListeners();
            popupContinue.btnContinue.onClick.AddListener(() =>
            {
                BattleController.instance.gameState = GameState.WAIT_BATTLE;
                popupContinue.gameObject.SetActive(false);
                canvasUIGamePlay.sortingLayerName = Name_Sorting_Layer_origin;
                BattleController.instance.ResetGame();
            });
        }
    }
}
