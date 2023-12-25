using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using RubikCasual.DailyItem;
using UnityEngine;

namespace RubikCasual.Lobby
{
    [Serializable]
    public class InfoUserData
    {
        public string UserName, UserId;
        public float Gold, Gem, Level, Energy, Hp, Exp, Rank;
        public List<float> lsIdSlotCharacter = new List<float>();
    }
    public class UserData : MonoBehaviour
    {
        public InfoUserData data;
        public ItemData itemData;
        public Rubik_Casual.CharacterInfo characterInfo;
        public static UserData instance;
        void Awake()
        {
            instance = this;
            setData();
        }
        void Update()
        {

        }
        void setData()
        {
            data.UserName = "po123lop123";
            data.Gold = itemData.datalobby.FirstOrDefault(f => f.name == "Coins").numberItem;
            data.Gem = itemData.datalobby.FirstOrDefault(f => f.name == "Gems").numberItem;
            data.Energy = itemData.datalobby.FirstOrDefault(f => f.name == "Energy").numberItem;
            data.lsIdSlotCharacter = itemData.lsIdSlotSetupCharacter;

        }
    }
}
