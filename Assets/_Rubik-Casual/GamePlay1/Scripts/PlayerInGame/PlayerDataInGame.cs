using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RubikCasual.Player
{
    public class PlayerDataInGame 
    {
        private int  HP;
        private int DEF;
        // public int movementRange, attackRange;
    }
    public enum TypeSpportCard
    {
        healSkill = 0,
        damageBoostSkill = 1,
        heroSkill = 2,
        poisonSkill = 3,
        silenceSkill = 4,
    }
    public class SupportCardData
    {
        private int HP;
        private int DEF;

    }
}

