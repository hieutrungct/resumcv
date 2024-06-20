using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace RubikCasual.ListWaifuPlayer
{
    public class CardWaifuinHand : MonoBehaviour
    {
        public List<Transform> lsSlot;
        public void CheckAndShiftChildren()
        {
            Debug.Log("After Destroy");
            for (int i = 0; i < ListWaifus.instance.lsCardWaifuInHand.Count; i++)
            {
                // if (lsSlot[i].GetComponentInChildren<CardWaifu>() == null)
                // {
                //     // Di chuyển các phần tử con của các slot phía sau nó lên để lấp đầy chỗ trống
                //     for (int j = i + 1; j < lsSlot.Count; j++)
                //     {
                //         CardWaifu cardWaifu = lsSlot[j].GetComponentInChildren<CardWaifu>();
                //         if (cardWaifu != null)
                //         {
                //             cardWaifu.transform.SetParent(lsSlot[i].transform);
                //             cardWaifu.transform.localPosition = Vector3.zero; 
                //             break;
                //         }
                //     }
                // }
                // else
                // {
                //     Debug.Log("không có ô bị biến mất" + i);
                // }
                ListWaifus.instance.lsCardWaifuInHand[i].transform.SetParent(lsSlot[i]);
                // ListWaifus.instance.lsCardWaifuInHand[i].transform.localPosition = Vector3.one;
                ListWaifus.instance.lsCardWaifuInHand[i].transform.DOMove(lsSlot[i].transform.position, 0.25f);
                // ListWaifus.instance.lsCardWaifuInHand[i].transform.DOJump(lsSlot[i].transform.position, 1f / 3f, 1, 0.5f);
            }
        }
        
    }
}

