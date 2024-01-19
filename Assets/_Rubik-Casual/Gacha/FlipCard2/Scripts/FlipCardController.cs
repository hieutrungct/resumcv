using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.FlipCard2
{
    public class FlipCardController : MonoBehaviour
    {
        public GameObject gbTest, GbInfoCard;
        public SlotInfoCard slotInfoCard;
        public Transform posInstantiateCard;
        public List<GameObject> lsInfocardClone;
        public List<Sprite> lsSpriteCard;
        public bool ChangeType;

        void Start()
        {
            CreateCard();
        }
        void CreateCard()
        {
            for (int i = 0; i < slotInfoCard.lsPosSlotInfoCard.Count; i++)
            {
                GameObject infoCardClone = Instantiate(GbInfoCard, posInstantiateCard);
                infoCardClone.transform.position = posInstantiateCard.position;
                infoCardClone.transform.localScale = new Vector3();
                lsInfocardClone.Add(infoCardClone);
            }
        }
        [Button]
        void ResetInfo()
        {
            foreach (var item in lsInfocardClone)
            {
                Destroy(item);
            }
            lsInfocardClone.Clear();
            CreateCard();
        }
        // void BtnTest()
        // {
        //     if (System.Math.Abs(gbTest.transform.localRotation.y) != 1)
        //     {
        //         // Debug.Log(gbTest.transform.localRotation.y.);
        //         gbTest.transform.DORotate(new Vector3(0, -180, 0), 0.5f).OnComplete(() =>
        //         {
        //             BtnTest();
        //         });
        //     }
        //     else
        //     {
        //         // Debug.Log(System.Math.Abs(gbTest.transform.localRotation.y));
        //         gbTest.transform.DORotate(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
        //         {
        //             BtnTest();
        //         });
        //     }
        // }
        [Button]
        void BtnMoveCard(int index)
        {
            if (index < lsInfocardClone.Count)
            {
                GameObject infoCardClone = lsInfocardClone[index];
                infoCardClone.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
                infoCardClone.transform.DOMove(slotInfoCard.lsPosSlotInfoCard[index].position, 0.25f)
                .OnComplete(() =>
                {
                    infoCardClone.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f);
                    infoCardClone.transform.Find("BackCard").transform.DOScale(new Vector3(0, 1f, 1f), 0.25f);
                    infoCardClone.transform.Find("FrontCard").GetComponent<Image>().sprite = lsSpriteCard[UnityEngine.Random.Range(0, lsSpriteCard.Count)];
                    infoCardClone.transform.Find("FrontCard").transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
                    if (!ChangeType)
                    {
                        BtnMoveCard(index + 1);
                    }
                });
                if (ChangeType)
                {
                    BtnMoveCard(index + 1);
                }
            }

        }
    }
}
