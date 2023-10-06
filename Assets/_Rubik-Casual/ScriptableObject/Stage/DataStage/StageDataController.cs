using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace RubikCasual.StageData
{
    [CreateAssetMenu(fileName = "NewStage", menuName = "ScriptableObject/LevelInStage")]
    public class StageDataController : ScriptableObject
    {

        public Stage[] stages;


    }
    [Serializable]
    public class ItemRewardBonus
    {
        public int idItem;
        public int numberItem;

    }
    [Serializable]
    public class LevelInStage
    {
        public int idLvl, numberStarComplete, numberEnergyAtack;

        public bool isItemTop, isCompleteLevel, isBoss, isLevelPresent, isLevelBonusComplete;
        public List<ItemBonus> itemBonus;
        public List<ItemRewardBonus> itemRewardBonus;
        public List<EnermyAtack> enermyAtacks;

    }
    [Serializable]
    public class Stage
    {
        public int idStage, numberLevelUnlockStage;
        public string nameStage;
        public Sprite imageStage;
        public bool unlockStage, isNew;
        public List<LevelInStage> levelInStages;
    }
    [Serializable]
    public class ItemBonus
    {
        public int idItem, numberValueBonus;
    }
    [Serializable]
    public class EnermyAtack
    {
        public int idEnermy, numberEnermy;
    }
}
