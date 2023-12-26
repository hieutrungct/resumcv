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
    public class StagePlay
    {
        public int Index;
        public List<WaifuInBattleSage> CurentTeam = new List<WaifuInBattleSage>();
        public Dictionary<int, int> InventoryInStage = new Dictionary<int, int>();
    }
    public class PlayerOwnsWaifu
    {
        public int Index, Star, Exp, IndexSkin, IndexEvolution;
    }
    [Serializable]
    public class UserData
    {
        public string UserName, UserId;
        public float Gold, Gem, Level, Energy, Hp, Exp, Rank;
        public List<float> CurentTeam = new List<float>();
    }
    [Serializable]
    public class PlayerData 
    {
        public UserData userData;
        public StagePlay stagePlay;
        [SerializeField] NTDictionary<int, int> Inventory = new NTDictionary<int, int>();
        public int CurentStage;

    }
}

