using System.Collections;
using System.Collections.Generic;
using RubikCasual.EnemyInGame;
using RubikCasual.GamePlayManager;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;
namespace RubikCasual.PlayerInGame
{
    public class PlayerWaifu : MonoBehaviour
    {
        TrackEntry animTrack;
        public SkeletonGraphic UI_Waifu;
        public int idSlotContainWaifu;
        public Vector2 posWaifu;
        [Button]
        public void SetUpHeroCombat()
        {
            foreach (var item in ListWaifuEnemy.instance.lsEnemyInMap)
            {
                if (item.posWaifuEnemy == new Vector2(posWaifu.x + 1, posWaifu.y)
                    ||item.posWaifuEnemy == new Vector2(posWaifu.x + 1, posWaifu.y + 1)
                    ||item.posWaifuEnemy == new Vector2(posWaifu.x + 1, posWaifu.y - 1)
                    ||item.posWaifuEnemy == new Vector2(posWaifu.x, posWaifu.y + 1)
                    ||item.posWaifuEnemy == new Vector2(posWaifu.x, posWaifu.y - 1))
                {
                    UpdateHeroCombat();
                    break;
                }
                else if(item.posWaifuEnemy == new Vector2(posWaifu.x - 1, posWaifu.y)
                ||item.posWaifuEnemy == new Vector2(posWaifu.x - 1, posWaifu.y + 1)
                ||item.posWaifuEnemy == new Vector2(posWaifu.x - 1, posWaifu.y - 1))
                {
                    gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                    UpdateHeroCombat();
                    break;
                }
            }

        }
        public void CheckPosWaifu(Vector2 pos)
        {
            posWaifu = pos;
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
                case "Attack":
                Debug.Log("CompleteAnimation"+ animTrack.IsComplete);
                    // if(animTrack.IsComplete){
                       
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    // }
                    break;
                case "Attacked":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                    break;
                case "SkillCast":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                    break;
                case "Die":
                    if(animTrack.IsComplete){
                        UI_Waifu.AnimationState.SetAnimation(0, Config.Idle,true);
                        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
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
    }
}

