using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RubikCasual.ListWaifu
{
    public class CardWaifuinHand : MonoBehaviour
    {
        public List<SlotWaifuInHand> lsSlot;
        public void CheckAndShiftChildren()
        {
            Debug.Log("After Destroy");
            for (int i = 0; i < lsSlot.Count; i++)
            {
                if (lsSlot[i].GetComponentInChildren<CardWaifu>() == null)
                {
                    // Di chuyển các phần tử con của các slot phía sau nó lên để lấp đầy chỗ trống
                    for (int j = i + 1; j < lsSlot.Count; j++)
                    {
                        CardWaifu cardWaifu = lsSlot[j].GetComponentInChildren<CardWaifu>();
                        if (cardWaifu != null)
                        {
                            cardWaifu.transform.SetParent(lsSlot[i].transform);
                            cardWaifu.transform.localPosition = Vector3.zero; 
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

