using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RubikCasual.DailyLogin;
using RubikCasual.Day7Login;
using System;
using UnityEngine.Assertions.Must;
using RubikCasual.DailyItem;
using System.Linq;
using RubikCasual.RewardMonth;

namespace RubikCasual.RewardWeek
{
    public class RewardWeekController : MonoBehaviour
    {
        public Transform slotDailyTransform;
        public ItemData itemData;
        public Transform slotDay7Transform;
        public GameObject CloseBtn, dailyLoginItem, day7LoginBonus;
        public bool checkCloseBtn = false;
        bool check = false;
        List<DailyLoginItem> listDailyLoginItems = new List<DailyLoginItem>();
        Day7LoginBonus lastDayLoginBonnus = new Day7LoginBonus();
        int curentTime;

        public static RewardWeekController instance;
        void Awake()
        {
            instance = this;
            curentTime = 14;
            CheckDateContinuously();
            createItemDaily();
        }
        void CheckDateContinuously()
        {

            var lastDailyLogin = itemData.daySlotWeeks.FirstOrDefault(d => d.DayPresent == (curentTime - 1));
            var presentDailyLogin = itemData.daySlotWeeks.FirstOrDefault(d => d.DayPresent == curentTime);
            if (presentDailyLogin != null)
            {
                if (presentDailyLogin.isClick)
                {
                    return;
                }
            }
            if (lastDailyLogin == null ||
                itemData.daySlotWeeks[0].DayPresent == 0 ||
                !lastDailyLogin.isClick ||
                (itemData.daySlotWeeks[6].isClick == true && curentTime != itemData.daySlotWeeks[6].DayPresent)
                )
            {
                ResetData();
                itemData.daySlotWeeks[0].isToday = true;
                itemData.daySlotWeeks[0].DayPresent = curentTime;
            }
            else
            {
                for (int i = 0; i < itemData.daySlotWeeks.Length; i++)
                {
                    if (lastDailyLogin.DayPresent == itemData.daySlotWeeks[i].DayPresent)
                    {
                        itemData.daySlotWeeks[i].isToday = false;
                        itemData.daySlotWeeks[i + 1].DayPresent = curentTime;
                        if (!itemData.daySlotWeeks[i + 1].isClick)
                        {
                            itemData.daySlotWeeks[i + 1].isToday = true;
                        }
                        itemData.daySlotWeeks[i + 1].tomorrow = false;
                    }
                }
            }

        }
        void ResetData()
        {
            for (int i = 0; i < itemData.daySlotWeeks.Length; i++)
            {
                itemData.daySlotWeeks[i].isClick = false;
                itemData.daySlotWeeks[i].isToday = false;
                itemData.daySlotWeeks[i].tomorrow = false;
                itemData.daySlotWeeks[i].DayPresent = 0;
            }
        }
        void createItemDaily()
        {

            for (int i = 1; i <= 7; i++)
            {
                if (i < 7)
                {
                    GameObject SlotClone = Instantiate(dailyLoginItem, slotDailyTransform);
                    var SlotCloneDaily = SlotClone.GetComponent<DailyLoginItem>();
                    SlotCloneDaily.textDayNumber.text = "DAY" + i.ToString();
                    SlotCloneDaily.Item.AddComponent<Button>().onClick.AddListener(() => { onClickButton(SlotClone); });
                    SlotCloneDaily.textValue.text = itemData.daySlotWeeks[i - 1].numberItemBonus.ToString();
                    SlotCloneDaily.idSlot = i;
                    SlotCloneDaily.idItem = itemData.daySlotWeeks[i - 1].idItem;
                    SlotCloneDaily.itemIcon.sprite = itemData.InfoItems.FirstOrDefault(b => b.id == SlotCloneDaily.idItem).imageItem;
                    if (itemData.daySlotWeeks[i - 1].isClick)
                    {
                        SlotCloneDaily.Clear.SetActive(true);
                    }
                    if (itemData.daySlotWeeks[i - 1].isToday)
                    {
                        SlotCloneDaily.focus.SetActive(true);
                        SlotCloneDaily.textTodayTomorrow.text = "Today item";
                    }
                    if (itemData.daySlotWeeks[i - 1].tomorrow)
                    {
                        SlotCloneDaily.focus.SetActive(true);
                        SlotCloneDaily.textTodayTomorrow.text = "Tomorrow item";
                    }

                    listDailyLoginItems.Add(SlotCloneDaily);
                }
                else
                {
                    GameObject SlotClone7 = Instantiate(day7LoginBonus, slotDay7Transform);
                    var SlotCloneDaily7 = SlotClone7.GetComponent<Day7LoginBonus>();
                    SlotCloneDaily7.idSlot = i;
                    SlotCloneDaily7.idItem = itemData.daySlotWeeks[i - 1].idItem;
                    SlotCloneDaily7.textDay.text = "DAY" + i.ToString();
                    SlotCloneDaily7.Item.AddComponent<Button>().onClick.AddListener(() => { onClickButton(SlotClone7); });


                    SlotCloneDaily7.textValue.text = itemData.InfoItems.FirstOrDefault(b => b.id == SlotCloneDaily7.idItem).name + " x " + itemData.daySlotWeeks[i - 1].numberItemBonus.ToString();
                    SlotCloneDaily7.iconImage.sprite = itemData.InfoItems.FirstOrDefault(b => b.id == SlotCloneDaily7.idItem).imageItem;
                    SlotCloneDaily7.iconImage.preserveAspect = true;

                    if (itemData.daySlotWeeks[i - 1].isClick)
                    {
                        SlotCloneDaily7.Clear.SetActive(true);
                    }
                    if (itemData.daySlotWeeks[i - 1].isToday)
                    {
                        SlotCloneDaily7.focus.SetActive(true);
                        SlotCloneDaily7.textTodayTomorrow.text = "Today item";
                    }
                    if (itemData.daySlotWeeks[i - 1].tomorrow)
                    {
                        SlotCloneDaily7.focus.SetActive(true);
                        SlotCloneDaily7.textTodayTomorrow.text = "Tomorrow item";
                    }
                    lastDayLoginBonnus = SlotCloneDaily7;
                }

            }


        }
        void onClickButton(GameObject slot)
        {
            if (slot.GetComponent<DailyLoginItem>())
            {
                var item = slot.GetComponent<DailyLoginItem>();
                ClickDailyButton(item);
            }
            if (slot.GetComponent<Day7LoginBonus>())
            {
                var item = slot.GetComponent<Day7LoginBonus>();
                ClickDaily7Button(item);
            }
            addValue(slot);
        }

        void ClickDailyButton(DailyLoginItem slot)
        {
            if (!slot.focus.activeSelf || slot.Clear.activeSelf || slot.textTodayTomorrow.text == "Tomorrow item")
            {
                Debug.Log("ko được click vào ngày thường");
                return;
            }
            if (slot.focus.activeSelf && slot.textTodayTomorrow.text == "Today item")
            {
                for (int i = 1; i <= itemData.daySlotWeeks.Length - 1; i++)
                {
                    if (slot.idSlot == i)
                    {
                        if (i != 6)
                        {
                            itemData.daySlotWeeks[i].tomorrow = true;
                            itemData.daySlotWeeks[i - 1].isToday = false;
                            listDailyLoginItems[i - 1].focus.SetActive(false);
                            listDailyLoginItems[i - 1].Clear.SetActive(true);
                            listDailyLoginItems[i].focus.SetActive(true);
                            listDailyLoginItems[i].textTodayTomorrow.text = "Tomorrow item";
                            itemData.daySlotWeeks[i - 1].isClick = true;
                        }
                        else
                        {
                            itemData.daySlotWeeks[i - 1].isToday = false;
                            itemData.daySlotWeeks[i - 1].isClick = true;
                            itemData.daySlotWeeks[i].tomorrow = true;
                            listDailyLoginItems[i - 1].focus.SetActive(false);
                            listDailyLoginItems[i - 1].Clear.SetActive(true);
                            lastDayLoginBonnus.focus.SetActive(true);
                            lastDayLoginBonnus.textTodayTomorrow.text = "Tomorrow Item";
                            return;
                        }

                    }

                    Debug.Log("Được click vào ngày thường");
                }
            }

        }
        void ClickDaily7Button(Day7LoginBonus slot)
        {
            if (!slot.focus.activeSelf || slot.Clear.activeSelf)
            {
                Debug.Log("ko được click vào ngày thường");
                return;
            }
            if (slot.focus.activeSelf && slot.textTodayTomorrow.text == "Today item")
            {
                slot.Clear.SetActive(true);
                slot.focus.SetActive(false);
                itemData.daySlotWeeks[6].isToday = false;
                itemData.daySlotWeeks[6].isClick = true;
            }
        }
        void addValue(GameObject slot)
        {
            if (slot.GetComponent<DailyLoginItem>())
            {
                var item = slot.GetComponent<DailyLoginItem>();

                itemData.datalobby.FirstOrDefault(f => f.id == item.idItem).numberItem += itemData.daySlotWeeks.FirstOrDefault(f => f.idItem == item.idItem).numberItemBonus;

                if (itemData.datalobby.FirstOrDefault(f => f.name == "Energy").numberItem > 60)
                {
                    itemData.datalobby.FirstOrDefault(f => f.name == "Energy").numberItem = 60;
                }
            }
            if (slot.GetComponent<Day7LoginBonus>())
            {
                var item = slot.GetComponent<Day7LoginBonus>();
                itemData.datalobby.FirstOrDefault(f => f.id == item.idItem).numberItem += itemData.daySlotWeeks.FirstOrDefault(f => f.idItem == item.idItem).numberItemBonus;

                if (itemData.datalobby.FirstOrDefault(f => f.name == "Energy").numberItem > 60)
                {
                    itemData.datalobby.FirstOrDefault(f => f.name == "Energy").numberItem = 60;
                }
            }
        }
        public void closeBtn()
        {
            CloseBtn.SetActive(false);
            checkCloseBtn = true;
        }
    }

}