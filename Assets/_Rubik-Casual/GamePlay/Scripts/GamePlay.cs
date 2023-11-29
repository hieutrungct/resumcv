using System.Collections;
using System.Collections.Generic;
using Spine.Unity.Editor;
using UnityEngine;

namespace RubikCasual.Battle
{
    public class GamePlay : MonoBehaviour
    {
        public bool isHeroTurn = true, isEndTurn = true;
        // public List<CharacterCombatUI> slotHeroClone, slotEnemyClone;
        public static GamePlay instance;

    }

}