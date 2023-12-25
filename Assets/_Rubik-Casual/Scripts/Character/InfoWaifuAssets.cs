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

    [Serializable]
    public class InfoWaifuAsset
    {
        public float ID, Code, Star, HP, DEF, ATK, Skill, Pow, Distance_Skill;
        public string Name, Class, Element, Rare;
    }
}
