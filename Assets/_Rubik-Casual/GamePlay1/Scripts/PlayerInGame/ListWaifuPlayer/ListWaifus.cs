using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RubikCasual.Data;
using RubikCasual.GamePlayManager;
using RubikCasual.PlayerInGame;
using RubikCasual.Waifu;
using TMPro;
using UnityEngine;
namespace RubikCasual.ListWaifuPlayer
{
    public class ListWaifus : MonoBehaviour
    {
        public static ListWaifus instance;
        public CardWaifu slot_card;
        public List<CardWaifu> lsInfoCardClone;
        public List<CardWaifu> lsCardWaifuInHand;
        public List<PlayerWaifu> lsWaifuInMap;
        public GameObject cardBack;
        public CardWaifuinHand cardWaifuinHand;
        public Transform posInstantiateCard;
        public TextMeshProUGUI remainingCards, isWaifuDead;
        void Awake()
        {
            instance = this;
            
            SortRarityAndLevel();
            waifuNumber = GamePlayController.instance.lsWaifu.Count;
            remainingCards.text = (waifuNumber).ToString();
            BattleCard();
        }
        public void SetUpListWaifu()
        {
            foreach (Transform child in posInstantiateCard)
            {
                Destroy(child.gameObject);
            }
            for (int i = GamePlayController.instance.lsWaifu.Count - 1; i > -1; i--)
            {
                CardWaifu slotWaifu = Instantiate(slot_card, posInstantiateCard);
                slotWaifu.SetUp(GamePlayController.instance.lsWaifu[i]);
                slotWaifu.transform.localScale = new Vector3(-1f, 1f, 1f);
                
                lsInfoCardClone.Add(slotWaifu);
            }
        }
        public void SortRarityAndLevel()
        {
            
            GamePlayController.instance.lsWaifu.Sort((charA, charB) =>
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
        public int waifuNumber;
        public void ScaleCard()
        {
            float duration = 0.25f;
            int indexSlot = lsCardWaifuInHand.Count;
            
            waifuNumber--;
            remainingCards.text = (waifuNumber).ToString();
            if (lsCardWaifuInHand.Count >= cardWaifuinHand.lsSlot.Count || lsInfoCardClone == null)
            {
                Debug.Log("ko còn ô trống hoặc ko còn thẻ");
                return;
            }
            GameObject CardBack = Instantiate(cardBack, posInstantiateCard);
            CardWaifu infoCardClone = lsInfoCardClone[0];
            Sequence sequence = DOTween.Sequence();

            sequence.Append(infoCardClone.transform.DOMove(cardWaifuinHand.lsSlot[indexSlot].position, duration)
            .OnComplete(() =>
            {
                
                infoCardClone.transform.parent = cardWaifuinHand.lsSlot[indexSlot];
                infoCardClone.transform.DOScale(new Vector3(0, 1, 1), duration / 2)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    // Khi thu nhỏ hoàn tất, lật đối tượng và mở rộng lại
                    infoCardClone.transform.DOScale(new Vector3(1, 1, 1), duration / 2)
                    .SetEase(Ease.InOutQuad).OnComplete(() => {
                        lsInfoCardClone.RemoveAt(0);
                    });
                });
                infoCardClone.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f);
            }));
            sequence.Join(CardBack.transform.DOMove(cardWaifuinHand.lsSlot[indexSlot].position, duration)
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
            lsCardWaifuInHand.Add(infoCardClone);

            SepUpIndexCard();
            
        }
        public void RemoveCardWaifu(CardWaifu cardWaifu)
        {
            if (lsCardWaifuInHand.Contains(cardWaifu))
            {
                lsCardWaifuInHand.Remove(cardWaifu);
            }
            else
            {
                Debug.Log("CardWaifu not found in the list.");
            }
        }
        public void SepUpIndexCard()
        {
            for (int i = 0; i < lsCardWaifuInHand.Count; i++)
            {
                lsCardWaifuInHand[i].IndexCard = i;
            }
        }
        private void BattleCard()
        {
            // có thể thêm card khi vào trận
            
            for (int i = 0; i < 2; i++)
            {
                CardWaifu infoCardClone = lsInfoCardClone[i];
                infoCardClone.transform.SetParent(cardWaifuinHand.lsSlot[i]);
                infoCardClone.transform.position = cardWaifuinHand.lsSlot[i].transform.position;
                infoCardClone.transform.localScale = Vector3.one;
                lsCardWaifuInHand.Add(infoCardClone);
                lsInfoCardClone.RemoveAt(i);
            }
        }
        
    }
}

