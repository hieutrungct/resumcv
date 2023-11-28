using System.Collections;
using System.Collections.Generic;
using RubikCasual.Combat;
using RubikCasual.Combat.Character;
using Spine.Unity.Editor;
using UnityEngine;

namespace RubikCasual.Combat
{
    public class GamePlay : MonoBehaviour
    {
        public bool isHeroTurn = true, isEndTurn = true;
        public List<CharacterCombatUI> slotHeroClone, slotEnemyClone;
        public static GamePlay instance;

    }

}