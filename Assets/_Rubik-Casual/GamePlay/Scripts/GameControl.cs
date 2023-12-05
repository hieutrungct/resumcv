using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubikCasual.Battle
{
    public enum GameState
    {
        START = 0,
        WAIT_BATTLE = 1,
        BATTLE = 2,
        END = 3
    }
    public class GameControl : MonoBehaviour
    {
        float duaration = 10f;

        public static GameControl instance;

        void Awake()
        {
            instance = this;
        }

    }
}
