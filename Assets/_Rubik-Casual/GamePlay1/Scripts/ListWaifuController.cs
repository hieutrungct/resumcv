using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using UnityEngine;
namespace RubikCasual.ListWaifu
{
    public class ListWaifuController : MonoBehaviour
    {
        public static ListWaifuController instance;
        public CardWaifu slot_card;
        public Transform contentWaifu;
        void Awake()
        {
            instance = this;
        }
        public void SetUpListWaifu()
        {

            for (int i = 0; i < DataController.instance.playerData.lsPlayerOwnsWaifu.Count; i++)
            {
                CardWaifu cardWaifu = Instantiate(slot_card, contentWaifu);
                slot_card.SetUp(DataController.instance.playerData.lsPlayerOwnsWaifu[i]);
            }
        }


        
    }
}

