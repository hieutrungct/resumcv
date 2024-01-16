using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubikCasual.Waifu
{
    [Serializable]
    public class InfoWaifuAssets
    {
        public List<InfoWaifuAsset> lsInfoWaifuAssets = new List<InfoWaifuAsset>();
    }
    public enum Rare
    {
        R = 1,
        SR = 2,
        SSR = 3
    }

    [Serializable]
    public class InfoWaifuAsset
    {
        public int ID;
        public float  Code, Star, HP, DEF, ATK, Skill, Pow, Distance_Skill;
        public string Name, Class, Element, Rare;
    }
}
