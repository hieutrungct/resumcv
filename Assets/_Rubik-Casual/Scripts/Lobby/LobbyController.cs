using System.Collections;
using System.Collections.Generic;
using RubikCasual.DailyLogin;
using TMPro;
using UnityEngine;
using RubikCasual.DailyItem;
using RubikCasual.RewardMonth;
using RubikCasual.RewardWeek;
using System;
using RubikCasual.Data.Player;
using RubikCasual.Data;
namespace RubikCasual.Lobby
{
    public class LobbyController : MonoBehaviour
    {
        public GameObject PopupRewardMonth, PopupRewardWeek, PopupStageSelect;
        public TextMeshProUGUI textCoins, textGems, textEnergy;
        public ItemData itemdata;
        public int numberEnergy;
        public string Game_Play_Screen = "CombatScene";
        public static LobbyController instance;

        void Awake()
        {
            // openRewardMonth();
            instance = this;
            // PopupRewardMonth.SetActive(true);
            // PopupRewardWeek.SetActive(true);

            //loadItem();
        }
        
        void Update()
        {
            loadItem();
            
        }


        // load item trong data
        public void loadItem()
        {
            // foreach (var itemLobby in itemdata.datalobby)
            // {
            //     numberEnergy = itemLobby.numberItem;
            //     if (itemLobby.name == "Coins")
            //     {
            //         textCoins.text = itemLobby.numberItem.ToString();
            //     }
            //     if (itemLobby.name == "Gems")
            //     {
            //         textGems.text = itemLobby.numberItem.ToString();
            //     }
            //     if (itemLobby.name == "Energy")
            //     {
            //         textEnergy.text = numberEnergy.ToString() + "/60";
            //     }
            // }

            textCoins.text = DataController.instance.playerData.userData.Gold.ToString();
            textEnergy.text = DataController.instance.playerData.userData.Energy.ToString() + "/60";
            textGems.text = DataController.instance.playerData.userData.Gem.ToString();
        }
        int abc = 10;
        public void ClickGold()
        {
            DataController.instance.playerData.userData.Energy += abc;
        }

        // lưu value item  vào trong data

        public void openRewardMonth()
        {
            PopupRewardMonth.SetActive(true);
        }
        public void openRewardWeek()
        {
            PopupRewardWeek.SetActive(true);
        }
        public void openStage()
        {
            PopupStageSelect.SetActive(true);
        }

        public void upCount()
        {

            if (RewardMonthController.instance.curentTime == 31)
            {
                RewardMonthController.instance.curentTime = 1;
            }
            else
            {
                RewardMonthController.instance.curentTime++;
            }
        }

    }
}