using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RubikCasual.RewardPass
{
    public class Reward_Pass : MonoBehaviour
    {
        [Serializable]
        public class ItemPass
        {
            public int Id, Index;
            public bool ItemActive = false;
            public ItemEnum itemName;
            public int Quanlity;
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
}

