using System.Collections;
using System.Collections.Generic;
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
        // public Vector3 parentAfterDrag1;
        public Transform parentTransform;
        public bool check;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Bắt đầu kéo");
            parentAfterDrag = transform.parent;
            transform.SetParent(parentTransform);
            
            transform.SetAsLastSibling();
            UI_Waifu.raycastTarget = false;
            
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            Debug.Log(MouseWorldPosittion());
            transform.position = MouseWorldPosittion();
            // if (check == false)
            // {
            //     parentAfterDrag1 = MouseWorldPosittion();
            //     check = true;
            //     Debug.Log(parentAfterDrag1);
            //     return;
            // }
            
            
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Thả kéo");
            transform.SetParent(parentAfterDrag);
            UI_Waifu.raycastTarget = true;
            check = false;
        }
        Vector3 MouseWorldPosittion()
        {
            var mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mouseScreenPos);
            
        }
        
    }
}

