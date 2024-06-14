using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public GameObject cardBack;
        public Transform posInstantiateCard;
        void Awake()
        {
            instance = this;
            lsWaifu = DataController.instance.playerData.lsPlayerOwnsWaifu;
            SortRarityAndLevel();
            
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
        public void ScaleCard()
        {
            float duration = 0.25f;
            int indexSlot = 0;
            if (lsSlot.All(item => item.transform.childCount > 0) || index > lsInfoCardClone.Count)
            {
                Debug.Log("ko còn ô trống hoặc ko còn thẻ");
                return;
            }
            GameObject CardBack = Instantiate(cardBack, posInstantiateCard);
            
            CardWaifu infoCardClone = lsInfoCardClone[index];
            for (int i = 0; i < lsSlot.Count; i++)
            {
                if(lsSlot[i].transform.childCount == 0)
                {
                    indexSlot = i;
                    break;
                }
            }
            Sequence sequence = DOTween.Sequence();

            sequence.Append(infoCardClone.transform.DOMove(lsSlot[indexSlot].position, duration)
            .OnComplete(() =>
            {
                
                infoCardClone.transform.parent = lsSlot[indexSlot].transform;
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
            sequence.Join(CardBack.transform.DOMove(lsSlot[indexSlot].position, duration)
            .OnComplete(() =>
            {
                CardBack.transform.DOScale(new Vector3(0, 1, 1), duration / 2)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    cardBack.SetActive(false);
                    // Khi thu nhỏ hoàn tất, lật đối tượng và mở rộng lại
                    // CardBack.transform.DOScale(new Vector3(1, 1, 1), duration / 2)
                    // .SetEase(Ease.InOutQuad);
                });
                CardBack.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f)
                .OnComplete(() => {
                    cardBack.SetActive(true);
                    Destroy(CardBack);
                });
                
                
            }));

            sequence.Play();
            
            
        }
        
        public void CheckAndShiftChildren()
        {
            
            Debug.Log("After Destroy");
            for (int i = 0; i < lsSlot.Count; i++)
            {
                if (lsSlot[i].transform.childCount == 0)
                {
                    // Di chuyển các phần tử con của các slot phía sau nó lên để lấp đầy chỗ trống
                    for (int j = i + 1; j < lsSlot.Count; j++)
                    {
                        if (lsSlot[j].transform.childCount > 0)
                        {
                            // Lấy phần tử con đầu tiên của slot phía sau
                            Transform child = lsSlot[j].transform.GetChild(0);

                            // Di chuyển phần tử con này vào slot trống
                            child.SetParent(lsSlot[i].transform);

                            // Thay đổi vị trí của phần tử con này (nếu cần)
                            child.localPosition = Vector3.zero; // Đặt vị trí cục bộ về (0,0,0) nếu cần

                            // Break sau khi di chuyển một phần tử con
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("không có ô bị biến mất" + i);
                }
            }
        }
    }
}

