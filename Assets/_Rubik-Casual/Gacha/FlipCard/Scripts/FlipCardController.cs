using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.FlipCard
{
    public class FlipCardController : MonoBehaviour
    {
        public List<GameObject> lsGbCardInPanel, lsInfoCard;
        public List<Sprite> lsSpriteCard;
        public GameObject CardMove, GroupCard, CardClone;
        Vector3 originPosCardMove, originPosGroupCard;
        public Vector3 valueJumb;
        public float SpeedCard = 0.5f;
        void Start()
        {
            originPosGroupCard = GroupCard.transform.position;
            foreach (GameObject item in lsInfoCard)
            {
                item.GetComponent<Image>().sprite = lsSpriteCard[Random.Range(0, lsSpriteCard.Count)];
            }
        }
        [Button]
        void ResetCard()
        {
            foreach (GameObject item in lsGbCardInPanel)
            {
                item.SetActive(false);
                item.transform.localScale = new Vector3(1, 1f, 1f);
            }
            foreach (GameObject item in lsInfoCard)
            {
                item.transform.localScale = new Vector3(0, 1f, 1f);
                item.GetComponent<Image>().sprite = lsSpriteCard[Random.Range(0, lsSpriteCard.Count)];
            }
            Destroy(CardClone);
        }
        // [Button]
        void StartMoveCard(int idSlotMove)
        {

            if (idSlotMove < lsGbCardInPanel.Count)
            {
                CardClone.SetActive(true);
                CardClone.transform.DOMove(lsGbCardInPanel[idSlotMove].transform.position, SpeedCard)
                .OnComplete(() =>
                {
                    lsGbCardInPanel[idSlotMove].transform.transform.DOScaleX(0, SpeedCard)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        lsInfoCard[idSlotMove].transform.DOScaleX(1, SpeedCard)
                        .SetEase(Ease.Linear);
                    });

                    CardClone.SetActive(false);
                    CardClone.transform.position = originPosCardMove;
                    lsGbCardInPanel[idSlotMove].SetActive(true);
                    StartMoveCard(idSlotMove + 1);
                });

            }
            else
            {
                GroupCard.transform.DOJump(originPosGroupCard, 1, 1, SpeedCard);
            }
        }
        [Button]
        void MoveGroupCardBottom()
        {
            GroupCard.transform.DOJump(GroupCard.transform.position + valueJumb, 1, 1, SpeedCard)
            .OnComplete(() =>
            {
                CardClone = Instantiate(CardMove, GroupCard.transform);
                originPosCardMove = CardClone.transform.position;
                CardClone.SetActive(false);
                StartMoveCard(0);
            });
        }
    }
}
