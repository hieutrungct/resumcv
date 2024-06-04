using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubikCasual.MapControllers
{
    public class MapController : MonoBehaviour
    {
        public GameObject CreatedHeroObj;
        public static MapController instance;
        public Vector3 posistionAfter;
        public bool drag;
        void Awake()
        {
            instance = this;
        }
    }
}

