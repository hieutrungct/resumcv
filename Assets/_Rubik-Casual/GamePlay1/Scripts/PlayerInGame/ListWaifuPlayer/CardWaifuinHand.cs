using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace RubikCasual.ListWaifuPlayer
{
    public class CardWaifuinHand : MonoBehaviour
    {
        public List<Transform> lsSlot;
        public void CheckAndShiftChildren(int indexSlotRemove)
        {
            Debug.Log("After Destroy");
            for (int i = indexSlotRemove; i < ListWaifus.instance.lsCardWaifuInHand.Count; i++)
            {
                
                ListWaifus.instance.lsCardWaifuInHand[i].transform.SetParent(lsSlot[i]);
                // ListWaifus.instance.lsCardWaifuInHand[i].transform.DOMove(lsSlot[i].transform.position, 0.25f);
                ListWaifus.instance.lsCardWaifuInHand[i].transform.DOJump(lsSlot[i].transform.position, 1f / 3f, 1, 0.25f);
            }
            
        }
        
    }
}

