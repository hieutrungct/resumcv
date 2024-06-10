using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        public List<CardWaifu> lsInfoCardClone;
        public List<Transform> lsSlot;
        public Transform posInstantiateCard;
        void Awake()
        {
            instance = this;
            // lsWaifu = DataController.instance.playerData.lsPlayerOwnsWaifu;
            // SortRarityAndLevel();
        }
        public void SetUpListWaifu()
        {
            foreach (Transform child in posInstantiateCard)
            {
                Destroy(child.gameObject);
            }
            for (int i = lsWaifu.Count - 1; i > -1; i--)
            {
                CardWaifu slotWaifu = Instantiate(slot_card, posInstantiateCard);
                slotWaifu.SetUp(lsWaifu[i]);
                lsInfoCardClone.Add(slotWaifu);
            }
        }
        [Button]
        public void LoadScene()
        {
            lsWaifu = DataController.instance.playerData.lsPlayerOwnsWaifu;
            SortRarityAndLevel();
        }
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

        public void ScaleCard(int index)
        {
            CardWaifu infoCardClone = lsInfoCardClone[index];
            infoCardClone.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
            infoCardClone.transform.DOMove(lsSlot[index].position, 0.25f)
            .OnComplete(() =>
            {
                infoCardClone.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f);
                
            });
            index++;
        }
    }
}

