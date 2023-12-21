using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle.UI;
using RubikCasual.Lobby;
using UnityEngine;

namespace RubikCasual.Data
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
    public class PlayerData
    {
        public UserData userData;
        public StagePlay stagePlay;
        // public List<PlayerOwnsWaifu> lsPlayerOwnsWaifus;
        public List<int> CurentTeam = new List<int>();
        public Dictionary<int, int> Inventory = new Dictionary<int, int>();
        public int CurentStage;
    }
}

