using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using RubikCasual.ItemPassSlots;
using RubikCasual.RewardPass;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.Pass
{
    public class PassController : MonoBehaviour
    {
        public Button Activate_Gold_Pass;
        public List<ItemPassSlot> itemPassGold;
        public List<ItemPassSlot> itemPassFree;
        public Slider expPass, lvlPass;
        public TextAsset itemPassFreeTxt, itemPassGoldTxt;
        public ListItems listItemFree, listItemGold;
        void Awake()
        {
            listItemFree = JsonUtility.FromJson<ListItems>(itemPassFreeTxt.text);
            listItemGold = JsonUtility.FromJson<ListItems>(itemPassGoldTxt.text);
            // SetUp();
        }
        public void SetUp()
        {
            for (int i = 0; i < itemPassFree.Count; i++)
            {
                itemPassFree[i].SetUpItemFree(listItemFree.lsItem[i]);
            }
            for (int i = 0; i < itemPassGold.Count; i++)
            {
                itemPassGold[i].SetUpItemGold(listItemGold.lsItem[i]);
                
            }
        }
        public void OnClickOpenReward()
        {
            gameObject.SetActive(true);
            SetUp();
            lvlPass.value = DataController.instance.playerData.userData.battlePass.LevelPass - 1;
        }


    }
}

