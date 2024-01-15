using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RubikCasual.DailyItem;
using RubikCasual.Data;
using RubikCasual.Lobby;
using UnityEngine;

namespace RubikCasual.Battle.Calculate
{
    public class Calculator
    {
        public static void CalculateHealth(CharacterInBattle CharacterInBattleAttack, CharacterInBattle CharacterInBattleAttacked)
        {

            CharacterInBattleAttacked.HpNow = HealthAmount((int)CharacterInBattleAttack.Atk, (int)CharacterInBattleAttacked.HpNow, (int)CharacterInBattleAttacked.Def);
            CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / CharacterInBattleAttacked.Hp;

        }
        static float HealthAmount(int dameAttack, int HealthAttacked, int defAttacked)
        {
            HealthAttacked = -dameAttack + HealthAttacked + defAttacked;
            if (HealthAttacked <= 0)
            {
                HealthAttacked = 0;
            }
            return HealthAttacked;
        }

        public static void CheckItemCalculate(int idItem, CharacterInBattle CharacterInBattleAttacked)
        {
            infoItem infoItem = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem);
            switch (infoItem.type)
            {
                case TypeItem.Heal:
                    CharacterInBattleAttacked.HpNow = HealthAmount(-infoItem.Dame, (int)CharacterInBattleAttacked.HpNow, 0);
                    if (CharacterInBattleAttacked.HpNow > CharacterInBattleAttacked.infoWaifuAsset.HP)
                    {
                        CharacterInBattleAttacked.HpNow = CharacterInBattleAttacked.infoWaifuAsset.HP;
                    }
                    CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / CharacterInBattleAttacked.infoWaifuAsset.HP;
                    break;

                case TypeItem.Poison:
                    CharacterInBattleAttacked.HpNow = HealthAmount(infoItem.Dame, (int)CharacterInBattleAttacked.HpNow, 0);
                    if (CharacterInBattleAttacked.HpNow > CharacterInBattleAttacked.infoWaifuAsset.HP)
                    {
                        CharacterInBattleAttacked.HpNow = CharacterInBattleAttacked.infoWaifuAsset.HP;
                    }
                    CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / CharacterInBattleAttacked.infoWaifuAsset.HP;
                    break;

                case TypeItem.Mana:
                    float valueOldCooldownSkillBar = CharacterInBattleAttacked.cooldownSkillBar.value;
                    CharacterInBattleAttacked.cooldownSkillBar.value = valueOldCooldownSkillBar + infoItem.Dame / 50f;
                    break;
            }
        }
    }
}
