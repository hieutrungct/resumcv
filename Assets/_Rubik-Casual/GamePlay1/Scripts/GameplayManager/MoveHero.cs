using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.ListWaifu;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace RubikCasual.GamePlayManager
{
    public class MoveHero : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public SkeletonGraphic UI_Waifu;
        [HideInInspector] public Transform parentAfterDrag;
        // public Transform posistionAfter;
        public Transform parentTransform;
        public Vector3 offset, posistionAfter, vector3;
        private bool check = true;

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Debug.Log("Bắt đầu kéo");
            parentAfterDrag = transform.parent;
            transform.SetParent(parentTransform);
            
            transform.SetAsLastSibling();
            UI_Waifu.raycastTarget = false;
            offset = transform.position - MouseWorldPosittion();
            
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            // Debug.Log(MouseWorldPosittion());
            vector3 = gameObject.transform.position - MouseWorldPosittion();
            Debug.Log(vector3);
            if (check == true)
            {
                
                posistionAfter = MouseWorldPosittion();
                check = false;
            }
            transform.position = MouseWorldPosittion() + offset;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Thả kéo");
            check = true;
            if(parentAfterDrag != null && parentAfterDrag.GetComponent<InventorySlot>() != null ) 
            {
                transform.SetParent(parentAfterDrag);
                UI_Waifu.raycastTarget = true;
                
            }
            else
            {
                Debug.Log("Thả kéo ra ngoài");
                // Debug.Log(posistionAfter);
                transform.DOMove(posistionAfter - vector3, 0.7f)
                .OnComplete(()=>{
                    Debug.Log("xoá");
                    Destroy(gameObject);
                });
                
            }
            
            
        }
        Vector3 MouseWorldPosittion()
        {
            var mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mouseScreenPos);
            
        }
        
    }
}

