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
        public TextMeshProUGUI textCoins, textGem, textEnergy;
        public ItemData itemdata;
        public static LobbyController instance;
        void Awake()
        {
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
                if (itemLobby.name == "coins")
                {
                    textCoins.text = itemLobby.numberItem.ToString();
                }
                if (itemLobby.name == "gems")
                {
                    textGem.text = itemLobby.numberItem.ToString();
                }
                if (itemLobby.name == "energy")
                {
                    textEnergy.text = itemLobby.numberItem.ToString() + "/60";
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
            }
        }
        public void openRewardMonth()
        {
            PopupRewardMonth.SetActive(true);
        }
    }
}