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
        public CharacterInBattle characterInBattleClone;
        public GameObject focus;
        public Slider healthBar, RageBar;
        void Awake()
        {
            this.gameObject.AddComponent<ItemListener>();

        }
        public void ShowFocus(CharacterInBattle characterInBattle)
        {
            if (characterInBattle.cooldownSkillBar.value >= 1)
            {
                focus.SetActive(true);
            }
            else
            {
                focus.SetActive(false);
            }
        }
        public void SetSliderBar(CharacterInBattle characterInBattle)
        {
            healthBar.value = characterInBattle.healthBar.value;
            RageBar.value = characterInBattle.cooldownSkillBar.value;
            txtValueHp.text = characterInBattle.HpNow + "/" + characterInBattle.infoWaifuAsset.HP;
        }
        public void SetDataPopup(CharacterInBattle characterInBattle)
        {
            characterInBattleClone = characterInBattle;
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
    public class ItemListener : MonoBehaviour
    {
        ItemViewUI itemViewUI;
        private void Awake()
        {
            itemViewUI = this.GetComponent<ItemViewUI>();
        }
        private void OnMouseDown()
        {
            if (itemViewUI.characterInBattleClone.cooldownSkillBar.value >= 1 && BattleController.instance.gameState == GameState.BATTLE)
            {
                itemViewUI.characterInBattleClone.cooldownSkillBar.value = 0;
                SetAnimCharacter.instance.HeroUseSkillTest(itemViewUI.characterInBattleClone);
            }
        }
    }
}
