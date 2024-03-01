using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RubikCasual.DailyItem;
using RubikCasual.Data;
using RubikCasual.Lobby;
using RubikCasual.Tool;
using UnityEngine;

namespace RubikCasual.Battle.Calculate
{
    public class Calculator
    {

        public static void CalculateHealth(CharacterInBattle CharacterInBattleAttack, CharacterInBattle CharacterInBattleAttacked, bool isUseSkill = false)
        {
            if (!isUseSkill)
            {
                CharacterInBattleAttacked.HpNow = HealthAmount(CharacterInBattleAttack.Atk, CharacterInBattleAttacked.HpNow, CharacterInBattleAttacked.Def);
                // CharacterInBattleAttacked.healthBar.value = CharacterInBattleAttacked.HpNow / (float)CharacterInBattleAttacked.Hp;
            }
            else
            {
                CharacterInBattleAttacked.HpNow = HealthAmount(CharacterInBattleAttack.Skill, CharacterInBattleAttacked.HpNow, CharacterInBattleAttacked.Def);
            }
            CharacterInBattleAttacked.healthBar = SliderTool.ChangeValueSlider(CharacterInBattleAttacked.healthBar, CharacterInBattleAttacked.healthBar.value, CharacterInBattleAttacked.HpNow / (float)CharacterInBattleAttacked.Hp);
        }
        static int HealthAmount(int dameAttack, int HealthAttacked, int defAttacked)
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
                    CharacterInBattleAttacked.healthBar = SliderTool.ChangeValueSlider(CharacterInBattleAttacked.healthBar, CharacterInBattleAttacked.healthBar.value, CharacterInBattleAttacked.HpNow / (float)CharacterInBattleAttacked.Hp);
                    break;

                case TypeItem.Poison:
                    CharacterInBattleAttacked.HpNow = HealthAmount(infoItem.Dame, (int)CharacterInBattleAttacked.HpNow, 0);
                    if (CharacterInBattleAttacked.HpNow > CharacterInBattleAttacked.infoWaifuAsset.HP)
                    {
                        CharacterInBattleAttacked.HpNow = CharacterInBattleAttacked.infoWaifuAsset.HP;
                    }
                    CharacterInBattleAttacked.healthBar = SliderTool.ChangeValueSlider(CharacterInBattleAttacked.healthBar, CharacterInBattleAttacked.healthBar.value, CharacterInBattleAttacked.HpNow / (float)CharacterInBattleAttacked.Hp);
                    break;

                case TypeItem.Mana:
                    float valueOldCooldownSkillBar = CharacterInBattleAttacked.cooldownSkillBar.value;
                    CharacterInBattleAttacked.cooldownSkillBar.value = valueOldCooldownSkillBar + infoItem.Dame / 40f;
                    CharacterInBattleAttacked.cooldownSkillBar = SliderTool.ChangeValueSlider(CharacterInBattleAttacked.cooldownSkillBar, valueOldCooldownSkillBar, valueOldCooldownSkillBar + infoItem.Dame / 40f, false);
                    break;
            }
        }

    }
}
