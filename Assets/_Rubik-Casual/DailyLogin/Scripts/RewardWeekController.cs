using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RubikCasual.DailyLogin;
using System;

public class RewardWeekController : MonoBehaviour
{
    
    public DailyLoginItem dailyLoginItem;
    public Transform slotDailyTransform;
    public Transform slotDay7Transform;
    public GameObject CloseBtn;
    bool check = false;
    void Awake()
    {
        createItemDaily();
    }
    void createItemDaily()
    {
        
        for (int i = 1; i <= 6; i++)
        {
            DailyLoginItem SlotClone = Instantiate(dailyLoginItem, slotDailyTransform);
            
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
                SlotClone.BackGlow.color = SlotClone.specialColor;
                SlotClone.GetComponent<Image>().sprite = SlotClone.specialBackGlow;
            }
            
        }
    }
    public void closeBtn()
    {
        CloseBtn.SetActive(false);
    }
}
