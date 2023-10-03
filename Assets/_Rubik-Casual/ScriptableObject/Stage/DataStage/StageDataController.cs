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
    public class LevelInStage
    {
        public int idLvl, numberStarComplete;
        public string nameLevel;
        public bool isItemTop, isCompleteLevel;
        public List<ItemBonus> itemBonus;
        public List<EnermyAtack> enermyAtacks;
        public bool isBoss;
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
        public Sprite spriteEnermy;
    }
}
