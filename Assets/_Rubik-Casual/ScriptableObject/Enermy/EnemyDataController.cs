using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace RubikCasual.EnermyData
{
    [CreateAssetMenu(fileName = "NewEnermyData", menuName = "ScriptableObject/EnermyData")]
    public class EnemyDataController : ScriptableObject
    {
        public List<Enermy> enermy;

    }
    [Serializable]
    public class Enermy
    {
        public int idEnermy, numberStar;
        public string nameEnermy;
        public Color backgroundColor, frameColor;
        public Sprite backgroundValue;
        public SkeletonDataAsset fileDataEnermy;
    }
}