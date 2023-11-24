using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rubik.Axie
{
    [System.Serializable]
    public class AxieData
    {
        public int Index;
        public string Origin;
        public string back = "";
        public string body = "";
        public string ears = "";
        public string ear = "";
        public string eyes = "";
        public string horn = "";
        public string mouth = "";
        public string tail = "";
        public string body_class = "";
        public int colorVariant = -1;
        public string accssory_slot = "";
        public int accessoryIdx = 0;

        public override string ToString()
        {
            return back+", "+body+", "+ears+", "+ear+", "+eyes+", "+horn+", "+mouth+", "+tail+", "+body_class+", "+colorVariant+", "+accssory_slot+", "+accessoryIdx;

        }
    }
}