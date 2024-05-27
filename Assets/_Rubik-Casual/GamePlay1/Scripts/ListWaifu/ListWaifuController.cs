using System.Collections;
using System.Collections.Generic;
using Rubik.ListWaifu;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Waifu;
using Sirenix.OdinInspector;
using UnityEngine;
namespace RubikCasual.ListWaifu
{
    public class ListWaifuController : MonoBehaviour
    {
        public static ListWaifuController instance;
        public CardWaifu slot_card;
        public Transform contentWaifu;
        public List<PlayerOwnsWaifu> lsWaifu;
        void Awake()
        {
            instance = this;
            lsWaifu = DataController.instance.playerData.lsPlayerOwnsWaifu;
            // SortRarityAndLevel();
        }
        
        public void SetUpListWaifu()
        {

            // for (int i = 0; i < DataController.instance.playerData.lsPlayerOwnsWaifu.Count; i++)
            // {
            //     CardWaifu cardWaifu = Instantiate(slot_card, contentWaifu);
            //     cardWaifu.SetUp(DataController.instance.playerData.lsPlayerOwnsWaifu[i]);
            // }
            foreach (Transform child in contentWaifu)
            {
                Destroy(child.gameObject);
            }
            for (int i = lsWaifu.Count - 1; i > -1; i--)
            {
                CardWaifu slotWaifu = Instantiate(slot_card, contentWaifu);
                slotWaifu.SetUp(lsWaifu[i]);
            }
        }
        [Button]
        public void SortRarityAndLevel()
        {
            
            lsWaifu.Sort((charA, charB) =>
            {
                InfoWaifuAsset infoWaifuA = DataController.instance.GetInfoWaifuAssetsByIndex(charA.ID);
                InfoWaifuAsset infoWaifuB = DataController.instance.GetInfoWaifuAssetsByIndex(charB.ID);
                int result = charA.level.CompareTo(charB.level);
                if (result == 0)
                {
                    return infoWaifuA.Rare.CompareTo(infoWaifuB.Rare);
                }
                return result;
            });

            SetUpListWaifu();
        }



        
    }
}

