using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle;
using UnityEngine;

public class MapBattleController : MonoBehaviour
{
    public List<ListSlotPos> lsPosEnemySlot;
    public ListSlotPos lsPosHeroSlot;
    public static MapBattleController instance;
    void Start()
    {
        instance = this;
    }

}
