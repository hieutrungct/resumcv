using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle;
using UnityEngine;

public class MapBattleController : MonoBehaviour
{
    public List<PositionHeroSlot> lsPosHeroSlot, lsPosEnemySlot;
    public static MapBattleController instance;
    void Start()
    {
        instance = this;
    }

}
