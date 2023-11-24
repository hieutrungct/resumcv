using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.Axie{
    [System.Serializable]
    public class AxieStats
    {
      public int Index;
      public Origin Origin;
      public string Name;
      public Class Class;
      public int Cost;
      public float[] HP;
      public float Def;
      public float MagicDef;
      public float[] Dmg;
      public float CritRate;
      public float Value;
      public int SkillIndex;
      public int Amount;
      public string Skill;
      public string Note;
    }
}