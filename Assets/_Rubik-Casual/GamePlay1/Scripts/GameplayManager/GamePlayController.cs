using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using Spine.Unity;
using UnityEngine;
namespace RubikCasual.GamePlayManager
{
    public class GamePlayController : MonoBehaviour
    {
        public static GamePlayController instance;
        public List<PlayerOwnsWaifu> lsWaifu;
        public SkeletonGraphic entity;
        void Awake()
        {
            instance = this;
            lsWaifu = DataController.instance.playerData.lsPlayerOwnsWaifu;
        }
        
    }
}

