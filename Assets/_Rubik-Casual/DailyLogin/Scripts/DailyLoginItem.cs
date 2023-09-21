using System.Collections;
using System.Collections.Generic;
using RubikCasual.RewardMonth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.DailyLogin
{
    public class DailyLoginItem : MonoBehaviour
{
    public int idSlot; 
    public int idItem;
    public GameObject Clear, focus, Item;
    public TextMeshProUGUI textDayNumber, textValue;
    public Image BackGlow, itemIcon;
    public Sprite nomalBackGlow, specialBackGlow;
    public bool isCheckClear;
    public Color nomalColor = new Color(100/255f,240/255f,1f,90/255f),
    specialColor = new Color(252/255f,208/255f,1f,166/255f);
    bool isLoad;

    void Awake()
    {
        Item.AddComponent<Button>().onClick.AddListener(() =>{onClickButton();});
        
    }
    void Update()
    {
        if (!isLoad)
        {
            loadDailyReward();
        }
    }
    public void loadDailyReward()
    {
        if (RewardMonthController.instance.DailyLogin == idSlot)
        {
            for (int i = 0; i < idSlot; i++)
            {
                var itemClear =RewardMonthController.instance.slotItem[i];
                itemClear.Clear.SetActive(true);
                itemClear.focus.SetActive(false);
                RewardMonthController.instance.slotItem[idSlot].focus.SetActive(true) ;
            }
        }
        isLoad = true;
    }
    void onClickButton()
    {
        if (Clear.activeSelf)
        {
            return;
        }
        if (focus.activeSelf)
        {
            if (idSlot+1!=29)
            {
                RewardMonthController.instance.slotItem[idSlot].focus.SetActive(true);
            }
            
        }
        else
        {
            return;
        }
        if (!isCheckClear)
        {
            Clear.SetActive(true);
            focus.SetActive(false);
            isCheckClear = true;
            RewardMonthController.instance.showDayReward(idSlot);
        }
    }
    
}

}