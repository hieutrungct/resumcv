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
        SSR = 3,
        UR = 4
    }
    public enum Class
    {
        Warrior = 1,
        Mage = 2,
        Witcher = 3,
        Marksman = 4,
        Tanker = 5,
        Assasin = 6
    }
    public enum Element
    {
        Water = 1,
        fire = 2,
        Ari = 3,
        Electric = 4,
        Spatial_Magic = 5,
        Magic = 6,
        Physics = 7,
        Poison = 8,
        Dark_Magic = 9,

    }

    [Serializable]
    public class InfoWaifuAsset
    {
        public int ID;
        public float Distance_Skill;
        public int Star, HP, DEF, ATK, Pow, Skill;
        public string Name, Class, Element, Code;
        public Rare Rare;
    }
}
