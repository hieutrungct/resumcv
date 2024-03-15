using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace RubikCasual.Data.Waifu
{
    [Serializable]
    public class ExpWithLevel
    {
        public int Level, FinalEXP;
    }
    public class ConfigLvl
    {
        // const string Path_Assets_SO = "Assets/_Data/Resources/Waifu";
        public static List<ExpWithLevel> LoadAsset(TextAsset UpLevel)
        {
            List<ExpWithLevel> lsExpWithLevel = new List<ExpWithLevel>();
            foreach (JSONNode item in JSON.Parse(UpLevel.text))
            {
                ExpWithLevel expWithLevel = JsonUtility.FromJson<ExpWithLevel>(item.ToString());
                lsExpWithLevel.Add(expWithLevel);
            }
            return lsExpWithLevel;
        }
        public static int GetAttributeStatByLevel(int originAttribute, int nowAttribute, int Factor, int level, RubikCasual.Waifu.ClassWaifu classWaifu = RubikCasual.Waifu.ClassWaifu.Warrior)
        {
            if (CheckAttributeStatByLevel(originAttribute, nowAttribute, Factor, level))
            {
                return nowAttribute;
            }
            else
            {
                return CaculateStatByLevel(originAttribute, Factor, level) + originAttribute;
            }
        }
        static bool CheckAttributeStatByLevel(int originAttribute, int nowAttribute, int Factor, int level)
        {
            int attributeCheck = CaculateStatByLevel(originAttribute, Factor, level) + originAttribute;

            if (attributeCheck <= nowAttribute)
            {
                return true;
            }
            return false;
        }
        static int CaculateStatByLevel(int originAttribute, int Factor, int level, RubikCasual.Waifu.ClassWaifu classWaifu = RubikCasual.Waifu.ClassWaifu.Warrior)
        {
            float ClassRate = 1.2f;
            float incrRate = 0.03f;

            return (int)(Math.Floor(originAttribute * level * ClassRate * 2f * incrRate) + Factor);
        }
    }
}
