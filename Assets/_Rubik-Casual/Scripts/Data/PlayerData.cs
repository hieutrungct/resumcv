using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTPackage;
using NTPackage.Functions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Data.Player
{
    public class WaifuInBattleSage
    {
        public int indexOfSlot, Index;
        public float HpNow, Rage;
    }
    [Serializable]
    public class StagePlay
    {
        public int Index;
        public List<WaifuInBattleSage> CurentTeam = new List<WaifuInBattleSage>();
        public Dictionary<int, int> InventoryInStage = new Dictionary<int, int>();
    }
    [Serializable]
    public class PlayerOwnsWaifu
    {
        public int Index, Star, Exp, IndexSkin, IndexEvolution, frag, level;
        public float  HP, DEF, ATK, Skill, Pow;

    }
    [Serializable]
    public class UserData
    {
        public string UserName, UserId;
        public float Gold, Gem, Level, Energy, Hp, Exp, Rank;
        public List<float> CurentTeam = new List<float>();

        // public UserData()
        // {
        //     UserName 

        // }
    }
    [Serializable]
    public class ListOwnsWaifu
    {
        public List<PlayerOwnsWaifu> lsOwnsWaifu = new List<PlayerOwnsWaifu>();
    }
    [Serializable]
    public class PlayerData
    {
        public UserData userData;
        // public StagePlay stagePlay;
        public List<PlayerOwnsWaifu> lsPlayerOwnsWaifu = new List<PlayerOwnsWaifu>();
        public NTDictionary<int, int> Inventory = new NTDictionary<int, int>();
        public int CurentStage;

    }
    [Serializable]
    public class Item
    {
        // public int Index, 
    }
}

