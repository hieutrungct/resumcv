using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.MapControllers;
using RubikCasual.PlayerInGame;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
namespace RubikCasual.GamePlayManager
{
    public class MoveHero : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public SkeletonGraphic UI_Waifu;
        [HideInInspector] public Transform parentAfterDrag;
        public Transform posistionAfter;
        public Transform parentTransform;
        public Vector3 offset;
        public bool check = false;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Bắt đầu kéo");
            parentAfterDrag = transform.parent;
            transform.SetParent(parentTransform);
            
            transform.SetAsLastSibling();
            UI_Waifu.raycastTarget = false;
            offset = transform.position - MouseWorldPosittion();
            
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            transform.position = MouseWorldPosittion() + offset;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Thả kéo");
            if(parentAfterDrag != null && parentAfterDrag.GetComponent<InventorySlot>() != null ) 
            {
                transform.SetParent(parentAfterDrag);
                UI_Waifu.raycastTarget = true;
            }
            else
            {
                Debug.Log("Thả kéo ra ngoài");
                
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

