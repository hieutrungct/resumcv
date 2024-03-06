using System.Collections;
using System.Collections.Generic;
using RubikCasual.Waifu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle.UI.VerticalView
{
    public class ItemViewUI : MonoBehaviour
    {
        public TextMeshProUGUI txtName, txtDameSkill, txtTypeSkill, txtRow, txtColumn, txtValueHp;
        private TextMeshProUGUI txtAtk, txtDef, txtHp;
        public Slider healthBar, RageBar;
        public void SetSliderBar(CharacterInBattle characterInBattle)
        {
            healthBar.value = characterInBattle.healthBar.value;
            RageBar.value = characterInBattle.cooldownSkillBar.value;
            txtValueHp.text = characterInBattle.HpNow + "/" + characterInBattle.infoWaifuAsset.HP;
        }
        public void SetDataPopup(CharacterInBattle characterInBattle)
        {
            txtName.text = characterInBattle.infoWaifuAsset.Name;

            int indexWaifu = Data.DataController.instance.characterAssets.GetIndexWaifu(characterInBattle.waifuIdentify.ID, characterInBattle.waifuIdentify.SkinCheck);
            Data.Waifu.WaifuSkill waifuSkill = new Data.Waifu.WaifuSkill();
            waifuSkill = Data.DataController.instance.characterAssets.GetSkillWaifuSOByIndex(indexWaifu);
            // txtAtk.text = "Atk: " + characterInBattle.infoWaifuAsset.ATK.ToString();
            // txtDef.text = "Def: " + characterInBattle.infoWaifuAsset.ATK.ToString();
            // txtHp.text = "Hp: " + characterInBattle.infoWaifuAsset.ATK.ToString();
            txtValueHp.text = characterInBattle.HpNow + "/" + characterInBattle.infoWaifuAsset.HP;
            txtDameSkill.text = "Dame Skill: " + (characterInBattle.infoWaifuAsset.ATK * waifuSkill.percentDameSkill).ToString();
            txtRow.text = "Row: " + waifuSkill.Row.ToString();
            txtColumn.text = "Column: " + waifuSkill.Column.ToString();
            txtTypeSkill.text = "Type Skill: " + waifuSkill.typeSkill;



        }
    }
}
