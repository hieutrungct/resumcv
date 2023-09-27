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

public class RewardWeekController : MonoBehaviour
{
    public Transform slotDailyTransform;
    public ItemData itemData;
    public Transform slotDay7Transform;
    public GameObject CloseBtn, dailyLoginItem, day7LoginBonus;
    bool check = false;
    int curentTime;

    void Awake()
    {
        curentTime = 30;
        CheckDateContinuously();
        createItemDaily();
    }
    void CheckDateContinuously()
    {
        if (itemData.daySlotWeeks[0].DayPresent == 0)
        {
            itemData.daySlotWeeks[0].isToday = true;
            itemData.daySlotWeeks[0].DayPresent = curentTime;
        }
        var lastDailyLogin = itemData.daySlotWeeks.FirstOrDefault(d => d.DayPresent == (curentTime - 1));
        if (lastDailyLogin == null)
        {
            ResetData();
        }
        else
        {
            for (int i = 0; i < itemData.daySlotWeeks.Length; i++)
            {
                // if (itemData.daySlotWeeks)
                // {
                    
                // }
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
            itemData.daySlotWeeks[0].DayPresent = curentTime;
            itemData.daySlotWeeks[0].isClick = true;
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

                if (!check)
                {
                    SlotCloneDaily.focus.SetActive(true);
                    check = true;
                }
                else
                {
                    SlotCloneDaily.focus.SetActive(false);
                }
            }
            else
            {
                GameObject SlotClone7 = Instantiate(day7LoginBonus, slotDay7Transform);
                var SlotCloneDaily7 = SlotClone7.GetComponent<Day7LoginBonus>();
                SlotCloneDaily7.textDay.text = "DAY" + i.ToString();
                SlotCloneDaily7.Item.AddComponent<Button>().onClick.AddListener(() => { onClickButton(SlotClone7); });

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
    }

    void ClickDailyButton(DailyLoginItem slot)
    {
        Debug.Log("đang click vào ngày thường");
    }
    void ClickDaily7Button(Day7LoginBonus slot)
    {
        Debug.Log("đang click vào ngày cuối cùng");
    }
    public void closeBtn()
    {
        CloseBtn.SetActive(false);
    }
}
