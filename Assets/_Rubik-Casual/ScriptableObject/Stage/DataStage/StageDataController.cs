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

        public List<Stage> stages;


    }
    [Serializable]
    public class LevelInStage
    {
        public int idLvl,numberStar;
        public string nameLevel;
        public bool isItem, isCompleteLevel;
        public ItemBonus itemBonus;

        public bool isBoss;
    }
    [Serializable]
    public class Stage
    {
        public int idStage, numberLevelUnlockStage;
        public string nameStage;
        public Image imageStage;
        public bool unlockStage;


        public List<LevelInStage> levelInStages;

    }
    [Serializable]
    public class ItemBonus
    {

        public int idItem, numberValueBonus;

    }
}
