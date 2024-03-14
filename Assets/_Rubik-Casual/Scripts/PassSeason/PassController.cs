using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.ItemPassSlots;
using RubikCasual.RewardPass;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.Pass
{
    public class PassController : MonoBehaviour
    {
        
        public List<ItemPassSlot> itemPassGold;
        public List<ItemPassSlot> itemPassFree;
        public Slider expPass, lvlPass;
        public TextAsset itemPassTxt;
        public ListItems listItems;
        void Awake()
        {
            listItems = JsonUtility.FromJson<ListItems>(itemPassTxt.text);
            SetUp();
        }
        public void SetUp()
        {
            for (int i = 0; i < itemPassFree.Count; i++)
            {
                itemPassFree[i].SetUpItemFree(listItems.lsItem[i]);
            }
        }


    }
}

