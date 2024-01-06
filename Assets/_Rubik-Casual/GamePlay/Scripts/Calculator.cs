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

            CharacterInBattleAttacked.HpNow = HealthAmount(CharacterInBattleAttack.Atk, CharacterInBattleAttacked.HpNow, CharacterInBattleAttacked.Def);
            CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / CharacterInBattleAttacked.Hp;

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

        public static void CheckItemCalculate(int idItem, CharacterInBattle CharacterInBattleAttacked)
        {
            infoItem infoItem = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem);
            string Type = infoItem.type.ToString();
            switch (Type)
            {
                case "Heal":
                    CharacterInBattleAttacked.HpNow = HealthAmount(-infoItem.Dame, CharacterInBattleAttacked.HpNow, 0);
                    if (CharacterInBattleAttacked.HpNow > CharacterInBattleAttacked.infoWaifuAsset.HP)
                    {
                        CharacterInBattleAttacked.HpNow = CharacterInBattleAttacked.infoWaifuAsset.HP;
                    }
                    CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / CharacterInBattleAttacked.infoWaifuAsset.HP;
                    break;
                case "Poison":
                    CharacterInBattleAttacked.HpNow = HealthAmount(infoItem.Dame, CharacterInBattleAttacked.HpNow, 0);
                    if (CharacterInBattleAttacked.HpNow > CharacterInBattleAttacked.infoWaifuAsset.HP)
                    {
                        CharacterInBattleAttacked.HpNow = CharacterInBattleAttacked.infoWaifuAsset.HP;
                    }
                    CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / CharacterInBattleAttacked.infoWaifuAsset.HP;
                    break;
                case "Mana":
                    float valueOldCooldownSkillBar = CharacterInBattleAttacked.cooldownSkillBar.value;
                    CharacterInBattleAttacked.cooldownSkillBar.value = valueOldCooldownSkillBar + infoItem.Dame / 100f;
                    break;
            }
        }
    }
}
