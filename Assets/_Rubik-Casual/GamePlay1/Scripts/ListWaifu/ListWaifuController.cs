using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Rubik.ListWaifu;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Waifu;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
namespace RubikCasual.ListWaifu
{
    public class ListWaifuController : MonoBehaviour
    {
        public static ListWaifuController instance;
        public CardWaifu slot_card;
        public List<PlayerOwnsWaifu> lsWaifu;
        public List<CardWaifu> lsInfoCardClone;
        public GameObject cardBack;
        public CardWaifuinHand cardWaifuinHand;
        public Transform posInstantiateCard;
        public TextMeshProUGUI remainingCards, isWaifuDead;
        void Awake()
        {
            instance = this;
            lsWaifu = DataController.instance.playerData.lsPlayerOwnsWaifu;
            SortRarityAndLevel();
            waifuNumber = lsWaifu.Count;
            remainingCards.text = (waifuNumber).ToString();
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
                slotWaifu.transform.localScale = new Vector3(-1f, 1f, 1f);
                
                lsInfoCardClone.Add(slotWaifu);
            }
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
        private int index;
        public int waifuNumber;
        public void ScaleCard()
        {
            float duration = 0.25f;
            int indexSlot = 0;
            waifuNumber--;
            remainingCards.text = (waifuNumber).ToString();
            if (cardWaifuinHand.lsSlot.All(item => item.GetComponentInChildren<CardWaifu>() != null) || index > lsInfoCardClone.Count)
            {
                Debug.Log("ko còn ô trống hoặc ko còn thẻ");
                return;
            }
            GameObject CardBack = Instantiate(cardBack, posInstantiateCard);
            
            CardWaifu infoCardClone = lsInfoCardClone[index];
            for (int i = 0; i < cardWaifuinHand.lsSlot.Count; i++)
            {
                if(cardWaifuinHand.lsSlot[i].GetComponentInChildren<CardWaifu>() == null)
                {
                    indexSlot = i;
                    break;
                }
            }
            Sequence sequence = DOTween.Sequence();

            sequence.Append(infoCardClone.transform.DOMove(cardWaifuinHand.lsSlot[indexSlot].transform.position, duration)
            .OnComplete(() =>
            {
                
                infoCardClone.transform.parent = cardWaifuinHand.lsSlot[indexSlot].transform;
                infoCardClone.transform.DOScale(new Vector3(0, 1, 1), duration / 2)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    // Khi thu nhỏ hoàn tất, lật đối tượng và mở rộng lại
                    infoCardClone.transform.DOScale(new Vector3(1, 1, 1), duration / 2)
                    .SetEase(Ease.InOutQuad).OnComplete(() => {
                        index++;
                    });
                });
                infoCardClone.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f);
            }));
            sequence.Join(CardBack.transform.DOMove(cardWaifuinHand.lsSlot[indexSlot].transform.position, duration)
            .OnComplete(() =>
            {
                CardBack.transform.DOScale(new Vector3(0, 1, 1), duration / 2)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    cardBack.SetActive(false);
                });
                CardBack.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f)
                .OnComplete(() => {
                    cardBack.SetActive(true);
                    Destroy(CardBack);
                });
                
                
            }));

            sequence.Play();
            
            
        }
        
    }
}

