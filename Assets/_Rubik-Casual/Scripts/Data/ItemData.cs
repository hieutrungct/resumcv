using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace RubikCasual.DailyItem
{
    public enum TypeItem
    {
        None = 0,
        Heal = 1,
        Poison = 2,
        Mana = 3,
    }
    [CreateAssetMenu(fileName = "NewlistItem", menuName = "ScriptableObject/listItem")]
    public class ItemData : ScriptableObject
    {
        public infoItem[] InfoItems;
        public DaySlot[] daySlots;
        public DaySlotWeek[] daySlotWeeks;
        public DataLobby[] datalobby;
        public List<float> lsIdSlotSetupCharacter;
    }
    [Serializable]
    public class infoItem
    {
        public int id;
        public string name;
        public TypeItem type;
        public int numberItem;
        public int Dame;
        public Sprite imageItem;
    }
    [Serializable]
    public class DaySlot
    {
        public int idItem, numberItemBonus;
        public bool isToday, tomorrow, isClick;

    }
    [Serializable]
    public class DaySlotWeek
    {
        public int idItem, numberItemBonus, DayPresent;
        public bool isToday, tomorrow, isClick;

    }
    [Serializable]
    public class DataLobby
    {
        public int id;
        public string name;
        public string Skin;
        public int numberItem;
        public Sprite imageItem;
    }
}

