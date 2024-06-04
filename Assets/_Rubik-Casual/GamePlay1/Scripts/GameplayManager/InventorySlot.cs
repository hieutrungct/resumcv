using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.ListWaifu;
using RubikCasual.MapControllers;
using UnityEngine;
using UnityEngine.EventSystems;
namespace RubikCasual.GamePlayManager
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        // public Transform parentTransform;
        public void OnDrop(PointerEventData eventData)
        {
            // GameObject droped = eventData.pointerDrag;
            // MoveHero moveHero = droped.GetComponent<MoveHero>();
            
            // if(transform.childCount == 0)
            // {  
            //     moveHero.parentAfterDrag = transform;
            // }
            // else if(transform.childCount == 1 && GamePlayController.instance.drag == true)
            // {  
            //     // Debug.Log("Kéo đè lên Hero khác");
            //     Destroy(moveHero.gameObject);
            //     GamePlayController.instance.drag = false;
            // }
            // else if(transform.childCount == 1)
            // {
                
            //     GameObject child = transform.GetChild(0).gameObject;
            //     MoveHero childMoveHero = child.GetComponent<MoveHero>();

            //     // Di chuyển đối tượng con hiện tại đến InventorySlot của đối tượng mới được kéo vào
            //     childMoveHero.transform.SetParent(moveHero.parentAfterDrag);
            //     // childMoveHero.transform.position = moveHero.parentAfterDrag1;

            //     // Cập nhật parentAfterDrag của đối tượng mới được kéo vào
            //     moveHero.parentAfterDrag = transform;
            // }
            // else
            // {
            //     Destroy(moveHero.gameObject);
            // }
            

            GameObject dropped = eventData.pointerDrag;
            if (dropped == null)
            {
                return; // Không có đối tượng được thả
            }
            MoveHero moveHero = dropped.GetComponent<MoveHero>();
            CardWaifu cardWaifu = dropped.GetComponent<CardWaifu>();
            if (cardWaifu != null)
            {
                if (transform.childCount == 0)
                {
                    cardWaifu.parentAfterDrag = transform;
                    MapController.instance.drag = false;
                    // Destroy(cardWaifu.gameObject);
                }
                else if(transform.childCount == 1 && MapController.instance.drag == true)
                {  

                    Debug.Log("Kéo đè lên Hero khác");
                    cardWaifu.uiWaifu.gameObject.transform.DOMove(MapController.instance.posistionAfter - new Vector3(0f,0.7f,0f), 0.7f)
                    .OnComplete(()=>{
                        Debug.Log("Đối tượng không được thả vào một InventorySlot hợp lệ, xóa đối tượng.");
                        cardWaifu.shadow.gameObject.SetActive(false);
                        Destroy(cardWaifu.uiWaifu.gameObject);
                    });
                    // cardWaifu.shadow.gameObject.SetActive(false);
                    // Destroy(cardWaifu.uiWaifu.gameObject);
                    MapController.instance.drag = false;
                }
                else if(transform.childCount == 2 && MapController.instance.drag == true)
                {
                    Destroy(cardWaifu.uiWaifu.gameObject);
                    MapController.instance.drag = false;
                }
            }
            if (moveHero != null)
            {
                if (transform.childCount == 0)
                {
                    moveHero.parentAfterDrag = transform;
                    MapController.instance.drag = false;
                }
                else if (transform.childCount == 1)
                {
                    GameObject child = transform.GetChild(0).gameObject;
                    MoveHero childMoveHero = child.GetComponent<MoveHero>();

                    if (childMoveHero != null)
                    {
                        childMoveHero.transform.SetParent(moveHero.parentAfterDrag);
                        moveHero.parentAfterDrag = transform;
                    }
                }
                
                
            }
            
        }
        
    }
}

