using System.Collections;
using System.Collections.Generic;
using RubikCasual.ListWaifuPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace RubikCasual.Player
{
    public class PlayerSupportCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image  avaBox;
        public Slider sliderShadow;
        public TextMeshProUGUI unlockTxt, nameTxt;
        public int IndexCard;
        public Vector3 offset;
        [HideInInspector] public Transform parentAfterDrag;
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Bắt đầu kéo");
        }
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = MouseWorldPosittion() + offset;
            Debug.Log(MouseWorldPosittion());
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Thả kéo vào trong");
        }
        Vector3 MouseWorldPosittion()
        {
            var mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mouseScreenPos);
            
        }
        IEnumerator DestroyAndCheckCoroutine()
        {
            // Tạo đối tượng tạm thời để chạy coroutine
            GameObject tempObject = new GameObject("TempObject");
            TempCoroutine tempCoroutine = tempObject.AddComponent<TempCoroutine>();
            DontDestroyOnLoad(tempObject);

            // Chuyển coroutine sang đối tượng tạm thời
            yield return tempCoroutine.StartCoroutine(DestroyAndCheck());

            // Hủy đối tượng tạm thời sau khi coroutine hoàn tất
            Destroy(tempObject);
        }

        IEnumerator DestroyAndCheck()
        {
            Destroy(gameObject);
            Debug.Log("Xoá gameObject");
            yield return new WaitForEndOfFrame(); // Chờ đến cuối khung hình
            Debug.Log("Thực hiện sắp xếp");
            ListWaifus.instance.cardWaifuinHand.CheckAndShiftChildren(IndexCard);
        }
        private void OnDestroy()
        {
            if (ListWaifus.instance != null)
            {
                ListWaifus.instance.RemoveCardWaifu(gameObject.GetComponent<CardWaifu>());
            }
        }
        
        public void SetUpCard()
        {

        }
    }
}

