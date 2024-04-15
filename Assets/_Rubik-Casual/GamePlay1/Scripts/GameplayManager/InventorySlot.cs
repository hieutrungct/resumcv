using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace RubikCasual.GamePlayManager
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject droped = eventData.pointerDrag;
            MoveHero moveHero = droped.GetComponent<MoveHero>();
            moveHero.parentAfterDrag = transform;
        }
    }
}

