using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rubik_Casual
{
    public class BtnOnClick : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
    {
        private bool pointerDown = false;
        [SerializeField] private float pointerDownTimer = 0, requireTimeHold = .3f;
        public UnityEvent onLongClick,onClick;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (pointerDown)
            {
                pointerDownTimer += Time.deltaTime;
                if (pointerDownTimer > requireTimeHold)
                {
                    if (onLongClick != null)
                    {
                        onLongClick.Invoke();
                    }
                    Reset();
                }
            
            }
        }
        public void OnPointerDown(PointerEventData evenData)
        {
            pointerDown = true;
        }
        public void OnPointerUp(PointerEventData evenData)
        {
            if (pointerDown)
            {
                if (pointerDownTimer < requireTimeHold)
                {
                    if (onClick != null)
                    {
                        onClick.Invoke();
                    }

                }
            }
            
            Reset();
        }
        void Reset()
        {
            pointerDownTimer = 0;
            pointerDown = false;
        }
    }
}

