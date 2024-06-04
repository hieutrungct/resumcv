using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.ListWaifu;
using RubikCasual.MapControllers;
using Sirenix.OdinInspector;
using Spine;
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
        public Vector3 offset;
        private bool check = true;

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
            // Debug.Log(MouseWorldPosittion());

            // vector3 = gameObject.transform.position - MouseWorldPosittion();
            // Debug.Log(vector3);
            // if (check == true)
            // {
            //     posistionAfter = MouseWorldPosittion();
            //     check = false;
            // }
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
                Debug.Log(MapController.instance.posistionAfter);
                transform.DOMove(MapController.instance.posistionAfter - new Vector3(0f,0.65f,0f), 0.7f)
                .OnComplete(()=>{
                    Debug.Log("Đối tượng không được thả vào một InventorySlot hợp lệ, xóa đối tượng.");
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
        Spine.TrackEntry animTrack;
        public void SetUpHeroCombat()
        {

        }
        [Button]
        public void UpdateHeroCombat()
        {
            animTrack = UI_Waifu.AnimationState.SetAnimation(0, Config.Attack,false);
        }
        void Start()
        {
            UI_Waifu.AnimationState.Complete += delegate {

                CompleteAnimation();

            };
            GamePlayController.instance.entity.AnimationState.Complete += delegate {
                CompleteAnimationEntity();
            };
            UI_Waifu.AnimationState.Event += HandleEventHero;
            GamePlayController.instance.entity.AnimationState.Event += HandleEventEntity;
        }
        string GetCurrentAnimationName(SkeletonGraphic skeletonGraphic)
        {
            var trackEntry = skeletonGraphic.AnimationState.GetCurrent(0);
            if (trackEntry != null)
            {
                return trackEntry.Animation.Name;
            }
            return null;
        }
        public void CompleteAnimation(){
            string currentAnimationName = GetCurrentAnimationName(UI_Waifu);
            switch (currentAnimationName)
            {
                case "Attack" :
                    Debug.Log("Attack End");
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                    }
                    break;
                case "Attacked":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                    }
                    break;
                case "SkillCast":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                    }
                    break;
                case "Die":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                    }
                    break;
                default:

                    break;
            }
            
        }
        public void CompleteAnimationEntity(){
            string currentAnimationNameEntity = GetCurrentAnimationName(GamePlayController.instance.entity);
            switch (currentAnimationNameEntity)
            {
                case "Attack" :
                    Debug.Log("Attack End");
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                    }
                    break;
                case "Attacked":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                    }
                    break;
                case "SkillCast":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                    }
                    break;
                case "Die":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                    }
                    break;
                default:

                    break;
            }
            
        }
        void HandleEventHero (TrackEntry trackEntry, Spine.Event e) {
            // Play some sound if the event named "footstep" fired.
            if (e.Data.Name == Config.Hit) 
            {
                // TakeDamageHero(50);
                animTrack = GamePlayController.instance.entity.AnimationState.SetAnimation(0, Config.Attacked, false);
                // effect("-50", locationSubHealthEntity);
                Debug.Log("Attack_aaaa");
            }
        }
        void HandleEventEntity (TrackEntry trackEntry, Spine.Event e) {
            if (e.Data.Name == Config.Hit) 
            {
                animTrack = UI_Waifu.AnimationState.SetAnimation(0, Config.Attacked, false);
                
            }
        }
        
    }
}

