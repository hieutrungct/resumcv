using System;
using System.Collections.Generic;
using RubikCasual.Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RubikCasual.DailyLogin;
using RubikCasual.DailyItem;
using System.Linq;
namespace RubikCasual.RewardMonth
{
    public class RewardMonthController : MonoBehaviour
        {
        [SerializeField] NowDaily nowDaily;
        public ItemData ListItemData;
        public DailyLoginItem dailyLoginItem;
        public Transform slotDailyTransform;
        public List<DailyLoginItem> slotItem = new List<DailyLoginItem>();
        public int DailyLogin;
        bool check = false;
        public static RewardMonthController instance;
        public void CloseButton()
        {
            LobbyController.instance.PopupRewardMonth.SetActive(false);
        }
        
        private void Awake()
        {
            instance = this;
            createItemDaily();
        }
        
        public void Reset()
        {
            foreach (var item in slotItem)
            {
                
                item.focus.SetActive(false);
                item.Clear.SetActive(false);
                item.isCheckClear = false;
                if (item.idSlot == 1)
                {
                    item.focus.SetActive(true);
                }
                nowDaily.dayReward=0;
                nowDaily.numberItem=0;
            }
        }

        void createItemDaily()
        {
            DailyLogin = nowDaily.dayReward;
            for(int i = 1; i <= 28 ; i++)
            {
                    DailyLoginItem SlotClone = Instantiate(dailyLoginItem, slotDailyTransform);
                    SlotClone.textDayNumber.text = "DAY " + i.ToString();
                    SlotClone.itemIcon.sprite = ListItemData.InfoItems[ListItemData.daySlots[i-1].idItem].imageItem;
                    SlotClone.idSlot = i;
                    SlotClone.idItem = ListItemData.daySlots[i-1].idItem;
                    if (!check)
                    {
                        SlotClone.focus.SetActive(true);
                        check = true;
                    }
                    else
                    {
                        SlotClone.focus.SetActive(false);
                    }

                    if (i % 7 == 0)
                    {
                        
                        //Ngày cuối tuần
                        SlotClone.BackGlow.color = SlotClone.specialColor;
                        SlotClone.GetComponent<Image>().sprite = SlotClone.specialBackGlow;
                    }
                    slotItem.Add(SlotClone);
            }
        }
        public void showDayReward(int i)
        {
            
            foreach (var item in slotItem)
            {
                if (i == item.idSlot)
                {
                    
                    nowDaily.dayReward = i;
                    nowDaily.numberItem = ListItemData.daySlots[i-1].numberItemBonus;
                    if (checkItemById(item.idItem).name == "coins")
                    {
                        LobbyController.instance.textCoins.text = (int.Parse(LobbyController.instance.textCoins.text)+nowDaily.numberItem).ToString() ;
                    }
                    if (checkItemById(item.idItem).name == "gems")
                    {
                        LobbyController.instance.textGem.text = (int.Parse(LobbyController.instance.textGem.text)+nowDaily.numberItem).ToString() ;
                    }

                }
            }
            
            
            nowDaily.nameDayReward = "Ngày " + nowDaily.dayReward.ToString();
            // UnityEngine.Debug.Log(nowDaily.nameDayReward);
            // UnityEngine.Debug.Log(nowDaily.numberItem);
        }
        infoItem checkItemById(int id)
        {
            infoItem item = new infoItem();
            foreach (var itemInListItemData in ListItemData.InfoItems)
            {
                if (itemInListItemData.id == id)
                {
                    item = itemInListItemData;
                }
            }
            return item;
        }
    }
    [Serializable]
    class NowDaily
    {
        public string nameDayReward{
            set{
                PlayerPrefs.SetString("Name_Day_Reward",value);
            }
            get{
                return PlayerPrefs.GetString("Name_Day_Reward");
            }
        }
        public int numberItem{
            set{
                PlayerPrefs.SetInt("Number_Item",value);
            }
            get{
                return PlayerPrefs.GetInt("Number_Item");
            }
        }
        public int dayReward{
            set{
                PlayerPrefs.SetInt("Note_Day_Reward",value);
            }
            get{
                return PlayerPrefs.GetInt("Note_Day_Reward");
            }
        }
        public int idItemByDaily{
            set{
                PlayerPrefs.SetInt("Note_Day_item",value);
            }
            get{
                return PlayerPrefs.GetInt("Note_Day_item");
            }
        }
        
        
    }
}