using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.Waifu;
using UnityEngine;

namespace RubikCasual.Data
{
    public class ExpCaculator
    {
        public static void LoadAttribute(PlayerOwnsWaifu playerOwnsWaifu)
        {

            InfoWaifuAsset infoWaifuAsset = DataController.instance.characterAssets.GetInfoWaifuAsset(playerOwnsWaifu.ID);
            int originAttributeAtk = infoWaifuAsset.ATK;
            int originAttributeDef = infoWaifuAsset.DEF;
            int originAttributeHp = infoWaifuAsset.HP;

            // Debug.Log(playerOwnsWaifu.ID);
            // Debug.Log(originAttributeAtk);
            // Debug.Log(originAttributeDef);
            // Debug.Log(originAttributeHp);

            int nowAttributeAtk = ConfigLvl.GetAttributeStatByLevel(originAttributeAtk, playerOwnsWaifu.ATK, 1, playerOwnsWaifu.level);
            int nowAttributeDef = ConfigLvl.GetAttributeStatByLevel(originAttributeDef, playerOwnsWaifu.DEF, 1, playerOwnsWaifu.level);
            int nowAttributeHp = ConfigLvl.GetAttributeStatByLevel(originAttributeHp, playerOwnsWaifu.HP, 5, playerOwnsWaifu.level);

            if (nowAttributeAtk != originAttributeAtk)
            {
                playerOwnsWaifu.ATK = nowAttributeAtk;
            }
            if (nowAttributeDef != originAttributeDef)
            {
                playerOwnsWaifu.DEF = nowAttributeDef;
            }
            if (nowAttributeHp != originAttributeHp)
            {
                playerOwnsWaifu.HP = nowAttributeHp;
            }
        }
        public static ExpWithLevel GetLevelWithValueExp(int Level, int exp, int value)
        {
            ExpWithLevel expAndLevel = new ExpWithLevel();
            int MaxValue = value + exp;
            int count = 0;
            int levelMax = 1000;
            foreach (ExpWithLevel expWithLevel in DataController.instance.lsExpWithLevel)
            {
                if (expWithLevel.Level >= Level)
                {
                    count = count + expWithLevel.FinalEXP;
                    if (count > MaxValue && levelMax > expWithLevel.Level)
                    {
                        count = count - expWithLevel.FinalEXP;
                        levelMax = expWithLevel.Level;

                        expAndLevel.FinalEXP = MaxValue - count;
                        expAndLevel.Level = levelMax;
                    }
                }
            }
            return expAndLevel;
        }
    }
}