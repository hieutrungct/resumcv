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
        
        R = 0,
        SR = 1,
        SSR = 2,
        UR = 3
    }
    public enum ClassWaifu
    {
        Warrior = 0,
        Mage = 1,
        Witcher = 2,
        Marksman = 3,
        Tanker = 4,
        Assasin = 5
    }
    public enum Element
    {
        Water = 0,
        fire = 1,
        Ari = 2,
        Electric = 3,
        Spatial_Magic = 4,
        Magic = 5,
        Physics = 6,
        Poison = 7,
        Dark_Magic = 8,

    }

    [Serializable]
    public class InfoWaifuAsset
    {
        public int ID;
        public float Distance_Skill;
        public int Star, HP, DEF, ATK, Pow, Skill;
        public string Name, Code;
        public Rare Rare;
        public Element Element;
        public ClassWaifu ClassWaifu;
    }
}
