using System.Collections;
using System.Collections.Generic;
using RubikCasual.DailyLogin;
using TMPro;
using UnityEngine;
using RubikCasual.DailyItem;
namespace RubikCasual.Lobby
{
    public class LobbyController : MonoBehaviour
    {
        public GameObject PopupRewardMonth,PopupRewardWeek;
        public TextMeshProUGUI textCoins, textGems, textEnergy;
        public ItemData itemdata;
        public int curentTime, numberEnergy, limitEnergy;

        public static LobbyController instance;
        
        void Awake()
        {
            // openRewardMonth();
            curentTime = 31;
            limitEnergy = 60;
            instance = this;
            loadItem();
        }

        void Update()
        {
            saveItem();
        }
        void loadItem()
        {
            
            foreach (var itemLobby in itemdata.datalobby)
            {
                numberEnergy = itemLobby.numberItem;
                if (itemLobby.name == "coins")
                {
                    textCoins.text = itemLobby.numberItem.ToString();
                }
                if (itemLobby.name == "gems")
                {
                    textGems.text = itemLobby.numberItem.ToString();
                }
                if (itemLobby.name == "energy")
                {
                    textEnergy.text = numberEnergy.ToString() + "/" + limitEnergy.ToString();
                }
            }
        }
        void saveItem()
        {
            foreach (var itemLobby in itemdata.datalobby)
            {
                if (itemLobby.name == "coins")
                {
                    itemLobby.numberItem = int.Parse(textCoins.text);
                }
                if (itemLobby.name == "gems")
                {
                    itemLobby.numberItem = int.Parse(textGems.text);
                }
                if (itemLobby.name == "energy")
                {
                    itemLobby.numberItem = numberEnergy;
                }
            }
        }
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
            
            if (curentTime == 31)
            {
                curentTime = 1;
            }
            else{
                curentTime ++;
            }
        }
    }
}