using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.RewardPass
{
    public class Reward_Pass : MonoBehaviour
    {
        
    }
    [Serializable]
    public class ListItems
    {
        public List<ItemPass> lsItem;
    }
    [Serializable]
    public class ItemPass
    {
        public int Id;
        // public bool ItemActive = false;
        public ItemEnum itemName;
        public int Count;
    }
    public enum ItemEnum
    {
        Gold = 0,
        Gem = 1,
        Energy_20 = 2,
        Energy_50 = 3,
        Ticket_Normal = 4,
        Ticket_Gold = 5,
        SmallExpPotion = 6,
        MediumExpPotion = 7,
        LargeExpPotion = 8,
        UltraExpPotion = 9

    }
}

