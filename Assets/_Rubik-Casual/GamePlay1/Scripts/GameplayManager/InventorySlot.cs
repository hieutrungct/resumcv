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
        public Vector2 positionSlot;
        void Start()
        {
            positionSlot = new Vector2((float)(idSlot%10),(float)(idSlot/10)); 
        }
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            if (dropped == null || !MapController.instance.lsWaifuLocations.Contains(this))
            {
                return; // Không có đối tượng được thả
            }
            MoveHero moveHero = dropped.GetComponent<MoveHero>();
            CardWaifu cardWaifu = dropped.GetComponent<CardWaifu>();
            PlayerWaifu playerWaifu = dropped.GetComponent<PlayerWaifu>();
            if (cardWaifu != null)
            {
                HandleCardWaifuDrop(cardWaifu);
            }
            // sử dụng khi sử dụng supportCard có tính năng đặt card lên vị trị cao hơn
            if (moveHero != null && moveHero.check == true)
            {
                HandleMoveHeroDrop(moveHero, playerWaifu);
            }
            
        }
        private void HandleCardWaifuDrop(CardWaifu cardWaifu)
        {
            if (transform.childCount == 0)
            {
                cardWaifu.parentAfterDrag = transform;
                MapController.instance.drag = false;
                // Destroy(cardWaifu.gameObject);
                // cardWaifu.playerWaifu.idSlotContainWaifu = idSlot;
                // if (playerWaifu != null)
                // {
                //     Debug.Log("idSlot là " + idSlot);
                //     playerWaifu.idSlotContainWaifu = idSlot;
                // }

                cardWaifu.waifu.idSlotContainWaifu = idSlot;
                cardWaifu.waifu.CheckPosWaifu(positionSlot);
            }
            else if(transform.childCount == 1 && MapController.instance.drag == true )
            {  
                Debug.Log("Kéo đè lên Hero khác");
                cardWaifu.uiWaifu.gameObject.transform.DOMove(MapController.instance.posistionAfter - new Vector3(0f,0.7f,0f), 0.5f)
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
        private void HandleMoveHeroDrop(MoveHero moveHero, PlayerWaifu playerWaifu)
        {
            if (transform.childCount == 0)
            {
                moveHero.parentAfterDrag = transform;
                MapController.instance.drag = false;

                playerWaifu.idSlotContainWaifu = idSlot;
                playerWaifu.CheckPosWaifu(positionSlot);
            }
            else if (transform.childCount == 1)
            {
                GameObject child = transform.GetChild(0).gameObject;
                MoveHero childMoveHero = child.GetComponent<MoveHero>();
                PlayerWaifu childPlayerWaifu = child.GetComponent<PlayerWaifu>();
                if (childMoveHero != null)
                {
                    childMoveHero.transform.SetParent(moveHero.parentAfterDrag);
                    childPlayerWaifu.idSlotContainWaifu = playerWaifu.idSlotContainWaifu;
                    childPlayerWaifu.CheckPosWaifu(playerWaifu.posWaifu);

                    moveHero.parentAfterDrag = transform;
                    playerWaifu.idSlotContainWaifu = idSlot;
                    playerWaifu.CheckPosWaifu(positionSlot);
                }
            }
        }
        
    }
}

