using System.Collections;
using System.Collections.Generic;
using RubikCasual.Lobby;
using UnityEngine;

namespace RubikCasual.Data
{
    public class Data : MonoBehaviour
    {
        public UserData userData;
        public static Data instance;
        void Start()
        {
            instance = this;
            userData = UserData.instance;
            DontDestroyOnLoad(this);
        }
    }
}
