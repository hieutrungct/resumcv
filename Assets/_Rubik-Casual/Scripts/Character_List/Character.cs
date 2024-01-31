using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rubik_Casual
{
    [Serializable]
    
    
    public enum CharacterType
    {
        Melee = 0,
        Ranged = 1
    }
    public enum EffectType
    {
        None = -1,
        Frost = 0,
        Fire = 1,
        Poison = 2,
        Lightning = 3,
        Multi
    }
    
    public enum Rarity
    {
        UnCommon = 0,
        Common = 1,
        Rare = 2,
        Epic = 3,
        Legend = 4
    }
    public enum SortingType
    {
        None,
        Lever,
        Rarity,
        Power
    }

    [Serializable]
    public class Character
    {
        public int ID;
        public string id;
        public string Nameid;
        public string Name;
        public CharacterType CharacterType;
        public EffectType EffectType;
        public Rarity Rarity;
        public int Role;
        public List<SkillName> skills;
        public int Critical;
        public int AttackDamage;
        public int Depense;
        public int Health;
        public int MoveSpeed;
        public int Star;
        public int Ascend;
        public int Level;
        public int Exp;
        public int MaxExp;
        public int Target = 1;
        public bool isInDeck = false;
    }
    
    
    [Serializable]
    public class ListCharacterData
    {
        public List<Character> CharacterDatas = new List<Character>();
    }
}


