using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.StageData;
using UnityEngine;

namespace RubikCasual.Adventure.Data
{
    [CreateAssetMenu(fileName = "AdventureDatas", menuName = "ScriptableObject/AdventureDatas")]
    public class MapAdventureData : ScriptableObject
    {
        public List<DotItem> lsInfoDot;
    }


    [Serializable]
    public class DotItem
    {
        public int idStage, numberStarComplete;

        public bool isCompleteLevel, isBoss, isLevelPresent;
        public List<ItemRewardBonus> itemRewardBonus;
        public List<EnemyAtack> enemyAtacks;
    }
}
