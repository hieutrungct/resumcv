using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubikCasual.Battle.Calculate
{
    public class Calculator
    {
        public static void Calculate(CharacterInBattle CharacterInBattleAttack, CharacterInBattle CharacterInBattleAttacked)
        {
            CharacterInBattleAttacked.HpNow = HealthAmount(CharacterInBattleAttack.infoWaifuAsset.DmgPhysic, CharacterInBattleAttacked.HpNow, CharacterInBattleAttacked.infoWaifuAsset.Def);
            CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / CharacterInBattleAttacked.infoWaifuAsset.HP;

        }
        static float HealthAmount(float dameAttack, float HealthAttacked, float defAttacked)
        {
            HealthAttacked = -dameAttack + HealthAttacked + defAttacked * 0.1f;
            if (HealthAttacked <= 0)
            {
                HealthAttacked = 0;
            }
            return HealthAttacked;
        }
    }
}
