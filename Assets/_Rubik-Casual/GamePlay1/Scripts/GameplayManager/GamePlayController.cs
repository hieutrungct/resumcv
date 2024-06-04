using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
namespace RubikCasual.GamePlayManager
{
    public class GamePlayController : MonoBehaviour
    {
        public static GamePlayController instance;
        public SkeletonGraphic entity;
        void Awake()
        {
            instance = this;
        }
        
    }
}

