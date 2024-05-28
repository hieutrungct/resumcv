using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RubikCasual.GamePlayManager
{
    public class GamePlayController : MonoBehaviour
    {
        public GameObject CreatedHeroObj;
        public static GamePlayController instance;
        public Vector3 posistionAfter;
        public bool drag;
        void Awake()
        {
            instance = this;
        }
    }
}

