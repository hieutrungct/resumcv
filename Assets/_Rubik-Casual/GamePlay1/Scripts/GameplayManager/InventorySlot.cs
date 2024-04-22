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
            
            if(transform.childCount == 0)
            {  
                GameObject droped = eventData.pointerDrag;
                MoveHero moveHero = droped.GetComponent<MoveHero>();
                moveHero.parentAfterDrag = transform;
            }
            else if(transform.childCount == 1)
            {
                GameObject dropped = eventData.pointerDrag;
                MoveHero moveHero = dropped.GetComponent<MoveHero>();

                GameObject child = transform.GetChild(0).gameObject;
                MoveHero childMoveHero = child.GetComponent<MoveHero>();

                // Di chuyển đối tượng con hiện tại đến InventorySlot của đối tượng mới được kéo vào
                childMoveHero.transform.SetParent(moveHero.parentAfterDrag);
                // childMoveHero.transform.position = moveHero.parentAfterDrag1;

                // Cập nhật parentAfterDrag của đối tượng mới được kéo vào
                moveHero.parentAfterDrag = transform;
            }
            
        }
    }
}

