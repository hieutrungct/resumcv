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
        public int ID, Star, Exp, IndexSkin, IndexEvolution, frag, level;
        public int  HP, DEF, ATK, Skill, Pow;

    }
    [Serializable]
    public class UserData
    {
        public string UserName, UserId;
        public double Gold, Gem,  Hp, Exp, Ticket, DiamondTicket;
        public int Level, Energy, Rank;
        public List<int> CurentTeam = new List<int>();

        public UserData()
        {
            UserName  = "hieuct";
            UserId = DateTime.Now.ToString("yyyyMMddHHmmss"); 
            Gold = 1000;
            Level = 1;
            Energy = 60;
            Ticket = 10;
            for (int i = 0; i < 5; i++)
            {
                CurentTeam.Add(0);
            }

        }
        public static UserData GetDefaultUserData()
        {
            return new UserData();
        }
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

