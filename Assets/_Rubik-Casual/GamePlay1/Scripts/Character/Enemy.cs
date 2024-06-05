using System.Collections;
using System.Collections.Generic;
using RubikCasual.GamePlayManager;
using Spine;
using Spine.Unity;
using UnityEngine;
namespace RubikCasual.Character
{
    public class Enemy : MonoBehaviour
    {
        TrackEntry animTrack;
        public SkeletonGraphic UI_Waifu;
        public void SetUpHeroCombat()
        {

        }
        
        public void UpdateHeroCombat()
        {
            // animTrack = UI_Waifu.AnimationState.SetAnimation(0, Config.Attack,false);
        }
        void Start()
        {
            animTrack = UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
            UI_Waifu.AnimationState.Complete += delegate {

                CompleteAnimation();

            };
            UI_Waifu.AnimationState.Event += HandleEventHero;
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
                    // Debug.Log("Attack End");
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle, true);
                    }
                    break;
                case "Attacked":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle, true);
                    }
                    break;
                case "SkillCast":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle, true);
                    }
                    break;
                case "Die":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle, true);
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

                // animTrack = GamePlayController.instance.entity.AnimationState.SetAnimation(0, Config.Attacked, false);
                
                // effect("-50", locationSubHealthEntity);
                
            }
        }
        
    }
}

