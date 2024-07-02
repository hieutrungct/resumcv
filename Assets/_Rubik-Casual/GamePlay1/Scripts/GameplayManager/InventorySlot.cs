using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.ListWaifuPlayer;
using RubikCasual.MapControllers;
using RubikCasual.PlayerInGame;
using UnityEngine;
using UnityEngine.EventSystems;
namespace RubikCasual.GamePlayManager
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        // public Transform parentTransform;
        public int idSlot;
        public Vector2 posstionSlot;
        void Start()
        {
            posstionSlot = new Vector2((float)(idSlot%10 +1),(float)(idSlot/10 +1)); 
        }
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            if (dropped == null)
            {
                return; // Không có đối tượng được thả
            }
            MoveHero moveHero = dropped.GetComponent<MoveHero>();
            CardWaifu cardWaifu = dropped.GetComponent<CardWaifu>();
            PlayerWaifu playerWaifu = dropped.GetComponent<PlayerWaifu>();
            
            if (cardWaifu != null)
            {
                HandleCardWaifuDrop(cardWaifu, playerWaifu);
            }
            
            if (moveHero != null)
            {
                HandleMoveHeroDrop(moveHero, playerWaifu);
                
            }
            
        }
        private void HandleCardWaifuDrop(CardWaifu cardWaifu, PlayerWaifu playerWaifu)
        {
            if (transform.childCount == 0 && MapController.instance.lsWaifuLocations.Contains(this))
            {
                cardWaifu.parentAfterDrag = transform;
                MapController.instance.drag = false;
                // Destroy(cardWaifu.gameObject);
                playerWaifu.idSlotContainWaifu = idSlot;
                
            }
            else if(transform.childCount == 1 && MapController.instance.drag == true && MapController.instance.lsWaifuLocations.Contains(this))
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
            else if(transform.childCount == 2 && MapController.instance.drag == true && MapController.instance.lsWaifuLocations.Contains(this))
            {
                Destroy(cardWaifu.uiWaifu.gameObject);
                MapController.instance.drag = false;
            }
        }
        private void HandleMoveHeroDrop(MoveHero moveHero, PlayerWaifu playerWaifu)
        {
            if (transform.childCount == 0)
            {
                moveHero.parentAfterDrag = transform;
                MapController.instance.drag = false;
                playerWaifu.idSlotContainWaifu = idSlot;
            }
            else if (transform.childCount == 1)
            {
                GameObject child = transform.GetChild(0).gameObject;
                MoveHero childMoveHero = child.GetComponent<MoveHero>();
                PlayerWaifu childPlayerWaifu = child.GetComponent<PlayerWaifu>();
                if (childMoveHero != null)
                {
                    childMoveHero.transform.SetParent(moveHero.parentAfterDrag);
                    childPlayerWaifu.idSlotContainWaifu = idSlot;
                    moveHero.parentAfterDrag = transform;
                }
            }
        }
        
    }
}

