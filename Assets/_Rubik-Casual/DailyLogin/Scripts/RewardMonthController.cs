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
        public int curentTime, curentTimeMonth;
        public bool checkCloseBtn = false;
        public System.DateTime lastDayOfCurrentMonth;
        public static RewardMonthController instance;
        public void CloseButton()
        {
            LobbyController.instance.PopupRewardMonth.SetActive(false);
            checkCloseBtn = true;
        }

        private void Awake()
        {

            instance = this;
            curentTime = DateTime.Now.Day;
            curentTimeMonth = DateTime.Now.Month;

            createItemDaily();
            ResetTodayTomorrow();
        }
        void Update()
        {
            updateItemDaily();
        }
        void loadTime()
        {
            System.DateTime currentDate = System.DateTime.Now;

            // Lấy ngày đầu tiên của tháng tiếp theo
            System.DateTime firstDayOfNextMonth = new System.DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);

            // Lấy ngày cuối cùng của tháng hiện tại bằng cách trừ 1 ngày từ ngày đầu tiên của tháng tiếp theo
            lastDayOfCurrentMonth = firstDayOfNextMonth.AddDays(-1);


        }
        void CheckData()
        {
            loadTime();
            // check ngày cuối cùng của daily login
            if (curentTime == 28 && !ListItemData.daySlots[curentTime - 1].isClick)
            {
                ListItemData.daySlots[curentTime - 1].isToday = true;
            }
            // check data nếu quá ngày 28 trở đi loại bỏ check today và tomorrow
            if (curentTime >= 28)
            {
                for (int i = 0; i < 28; i++)
                {
                    if (i != 27)
                    {
                        ListItemData.daySlots[i].isToday = false;
                    }
                    ListItemData.daySlots[i].tomorrow = false;
                }
            }
            // ghi nhận today loại bỏ today và tomorrow khi ng chơi ko vào game lâu ngày trừ ngày hiện tại
            if (curentTime < 28 && !ListItemData.daySlots[curentTime].tomorrow)
            {
                ListItemData.daySlots[curentTime - 1].isToday = true;
                for (int i = 0; i < curentTime - 1; i++)
                {

                    ListItemData.daySlots[i].isToday = false;
                    ListItemData.daySlots[i].tomorrow = false;
                }
            }
        }

        void updateItemDaily()
        {

            int CountIsClick = 0;
            foreach (var item in ListItemData.daySlots)
            {
                if (item.isToday)
                {
                    CountIsClick++;
                }
            }
            if (nowDaily.dayReward > curentTime
            || curentTime == lastDayOfCurrentMonth.Day
            || CountIsClick > 1
            || nowDaily.MonthDaily != curentTimeMonth
            )
            {
                nowDaily.MonthDaily = curentTimeMonth;
                Reset();
            }
        }
        void ResetTodayTomorrow()
        {
            for (int i = 1; i <= curentTime; i++)
            {
                if (i != curentTime)
                {
                    ListItemData.daySlots[i - 1].isToday = false;
                    ListItemData.daySlots[i - 1].tomorrow = false;
                }
            }
        }
        public void Reset()
        {
            foreach (var item in slotItem)
            {

                item.focus.SetActive(false);
                item.Clear.SetActive(false);
                item.isCheckClear = false;

                nowDaily.dayReward = 0;
                nowDaily.numberItem = 0;
                if (item.idSlot <= curentTime)
                {

                    if (item.idSlot == curentTime)
                    {
                        item.focus.SetActive(true);
                        item.textTodayTomorrow.text = "Today item";
                    }
                    else
                    {
                        item.Clear.SetActive(true);
                        item.imageClear.SetActive(false);
                    }
                }
                if (curentTime != item.idSlot)
                {
                    ListItemData.daySlots[item.idSlot - 1].isToday = false;
                }

                ListItemData.daySlots[item.idSlot - 1].isClick = false;
                ListItemData.daySlots[item.idSlot - 1].tomorrow = false;
            }
        }

        void createItemDaily()
        {
            CheckData();
            for (int i = 1; i <= 28; i++)
            {
                DailyLoginItem SlotClone = Instantiate(dailyLoginItem, slotDailyTransform);
                SlotClone.textDayNumber.text = "DAY " + i.ToString();
                SlotClone.itemIcon.sprite = ListItemData.InfoItems[ListItemData.daySlots[i - 1].idItem - 1].imageItem;
                SlotClone.itemIcon.preserveAspect = true;
                SlotClone.idSlot = i;
                SlotClone.idItem = ListItemData.daySlots[i - 1].idItem;
                SlotClone.textValue.text = ListItemData.daySlots[i - 1].numberItemBonus.ToString();
                SlotClone.Item.AddComponent<Button>().onClick.AddListener(() => { onClickButton(SlotClone); });

                if (i < curentTime || ListItemData.daySlots[i - 1].isClick)
                {
                    SlotClone.Clear.SetActive(true);
                }

                if (!ListItemData.daySlots[i - 1].isClick && i < curentTime)
                {
                    SlotClone.imageClear.SetActive(false);
                }

                if (ListItemData.daySlots[i - 1].isToday)
                {
                    SlotClone.focus.SetActive(true);
                    SlotClone.textTodayTomorrow.text = "Today item";
                    ListItemData.daySlots[i - 1].tomorrow = false;
                }

                if (ListItemData.daySlots[i - 1].tomorrow)
                {
                    SlotClone.focus.SetActive(true);
                    SlotClone.textTodayTomorrow.text = "Tomorrow item";
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
        void onClickButton(DailyLoginItem slotClone)
        {
            if (slotClone.Clear.activeSelf)
            {
                return;
            }
            if (slotClone.focus.activeSelf && slotClone.textTodayTomorrow.text == "Today item")
            {
                if (slotClone.idSlot < 28)
                {
                    slotItem[slotClone.idSlot].focus.SetActive(true);
                    slotItem[slotClone.idSlot].textTodayTomorrow.text = "Tomorrow item";
                    ListItemData.daySlots[slotClone.idSlot - 1].isClick = true;
                    ListItemData.daySlots[slotClone.idSlot - 1].isToday = false;
                    ListItemData.daySlots[slotClone.idSlot].tomorrow = true;
                }
                else
                {
                    ListItemData.daySlots[slotClone.idSlot - 1].isClick = true;
                    ListItemData.daySlots[slotClone.idSlot - 1].isToday = false;
                    ListItemData.daySlots[slotClone.idSlot - 1].tomorrow = false;
                }
            }
            else
            {
                return;
            }
            if (!slotClone.isCheckClear)
            {
                slotClone.Clear.SetActive(true);
                slotClone.focus.SetActive(false);
                slotClone.isCheckClear = true;
                updateValueItemDaily(slotClone.idSlot);
            }
            ListItemData.daySlots[slotClone.idSlot - 1].isToday = false;

        }
        public void updateValueItemDaily(int i)
        {

            foreach (var item in slotItem)
            {
                if (i == item.idSlot)
                {

                    nowDaily.dayReward = i;
                    nowDaily.numberItem = ListItemData.daySlots[i - 1].numberItemBonus;




                    if (checkItemById(item.idItem).name.ToString() == "Coins")
                    {
                        var NumberItem = ListItemData.datalobby.FirstOrDefault(L => L.name == "Coins").numberItem;
                        ListItemData.datalobby.FirstOrDefault(L => L.name == "Coins").numberItem = NumberItem + nowDaily.numberItem;
                    }
                    if (checkItemById(item.idItem).name.ToString() == "Gems")
                    {
                        var NumberItem = ListItemData.datalobby.FirstOrDefault(L => L.name == "Gems").numberItem;
                        ListItemData.datalobby.FirstOrDefault(L => L.name == "Gems").numberItem = NumberItem + nowDaily.numberItem;
                    }
                    if (checkItemById(item.idItem).name.ToString() == "Energy")
                    {
                        var NumberItem = ListItemData.datalobby.FirstOrDefault(L => L.name == "Energy").numberItem;
                        if (NumberItem > 60)
                        {
                            NumberItem = 60;
                        }
                        ListItemData.datalobby.FirstOrDefault(L => L.name == "Energy").numberItem = NumberItem + nowDaily.numberItem;

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
        public string nameDayReward
        {
            set
            {
                PlayerPrefs.SetString("Name_Day_Reward", value);
            }
            get
            {
                return PlayerPrefs.GetString("Name_Day_Reward");
            }
        }
        public int numberItem
        {
            set
            {
                PlayerPrefs.SetInt("Number_Item", value);
            }
            get
            {
                return PlayerPrefs.GetInt("Number_Item");
            }
        }
        public int dayReward
        {
            set
            {
                PlayerPrefs.SetInt("Note_Day_Reward", value);
            }
            get
            {
                return PlayerPrefs.GetInt("Note_Day_Reward");
            }
        }
        public int MonthDaily
        {
            set
            {
                PlayerPrefs.SetInt("Note_Month_Reward", value);
            }
            get
            {
                return PlayerPrefs.GetInt("Note_Month_Reward");
            }
        }


    }
}