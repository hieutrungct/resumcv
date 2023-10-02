using System.Collections;
using System.Collections.Generic;
using RubikCasual.DailyLogin;
using TMPro;
using UnityEngine;
using RubikCasual.DailyItem;
using RubikCasual.RewardMonth;
using RubikCasual.RewardWeek;
namespace RubikCasual.Lobby
{
    public class LobbyController : MonoBehaviour
    {
        public GameObject PopupRewardMonth, PopupRewardWeek;
        public TextMeshProUGUI textCoins, textGems, textEnergy;
        public ItemData itemdata;
        public int numberEnergy;
        public static LobbyController instance;

        void Awake()
        {
            // openRewardMonth();
            instance = this;
            // PopupRewardMonth.SetActive(true);
            // PopupRewardWeek.SetActive(true);
        }


        void Update()
        {

            loadItem();
        }


        // load item trong data
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
        // lưu value item  vào trong data

        public void openRewardMonth()
        {
            PopupRewardMonth.SetActive(true);
        }
        public void openRewardWeek()
        {
            PopupRewardWeek.SetActive(true);
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