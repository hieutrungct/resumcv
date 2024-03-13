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
        public TextMeshProUGUI txtName, txtDameSkill, txtValueHp;
        public TextMeshProUGUI txtAtk, txtDef, txtRare;
        public CharacterInBattle characterInBattleClone;
        public List<GameObject> lsSlotEnemy, lsSlotHero;
        public GameObject focus;
        public UnityEngine.UI.Image BackgroundImage;
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
            SetSkillPopup(waifuSkill);

            txtAtk.text = characterInBattle.infoWaifuAsset.ATK.ToString();
            txtDef.text = characterInBattle.infoWaifuAsset.DEF.ToString();
            txtRare.text = characterInBattle.infoWaifuAsset.Rare.ToString();

            txtValueHp.text = characterInBattle.HpNow + "/" + characterInBattle.infoWaifuAsset.HP;
            txtDameSkill.text = ((int)(characterInBattle.infoWaifuAsset.ATK * waifuSkill.percentDameSkill)).ToString();
            // txtRow.text = "Row: " + waifuSkill.Row.ToString();
            // txtColumn.text = "Column: " + waifuSkill.Column.ToString();
            // txtTypeSkill.text = "Type Skill: " + waifuSkill.typeSkill;
            BackgroundImage.sprite = Data.DataController.instance.assetLoader.GetSpriteButtonWithRare(characterInBattle.infoWaifuAsset.Rare);

        }
        void SetSkillPopup(Data.Waifu.WaifuSkill waifuSkill)
        {
            foreach (GameObject slotHero in lsSlotHero)
            {
                slotHero.GetComponent<Image>().sprite = null;
            }
            foreach (GameObject slotEnemy in lsSlotEnemy)
            {
                slotEnemy.GetComponent<Image>().sprite = null;
                slotEnemy.GetComponent<Image>().color = Color.white;
            }

            switch (waifuSkill.typeSkill)
            {
                case CreateSkill.TypeSkill.BuffAtk:
                    foreach (GameObject slotHero in lsSlotHero)
                    {
                        slotHero.GetComponent<Image>().color = Color.green;
                        slotHero.GetComponent<Image>().preserveAspect = true;
                    }
                    break;
                case CreateSkill.TypeSkill.BuffDef:
                    foreach (GameObject slotHero in lsSlotHero)
                    {
                        slotHero.GetComponent<Image>().color = Color.green;
                        slotHero.GetComponent<Image>().preserveAspect = true;
                    }
                    break;
                case CreateSkill.TypeSkill.BuffHp:
                    foreach (GameObject slotHero in lsSlotHero)
                    {
                        slotHero.GetComponent<Image>().color = Color.green;
                        slotHero.GetComponent<Image>().preserveAspect = true;
                    }
                    break;
                case CreateSkill.TypeSkill.Wave:
                    ArraySkill(waifuSkill.Row, waifuSkill.Column, lsSlotEnemy);
                    break;
                case CreateSkill.TypeSkill.InTurn:
                    ArraySkill(waifuSkill.Row, waifuSkill.Column, lsSlotEnemy);
                    break;
                case CreateSkill.TypeSkill.InTurn2:
                    ArraySkill(waifuSkill.Row, waifuSkill.Column, lsSlotEnemy);
                    break;
                case CreateSkill.TypeSkill.Other:
                    ArraySkill(waifuSkill.Row, waifuSkill.Column, lsSlotEnemy);
                    break;
            }

            lsSlotHero[2].GetComponent<Image>().color = Color.yellow;
        }
        void ArraySkill(int row, int column, List<GameObject> listGameObject)
        {
            int minColumn = 0;
            switch (column)
            {
                case 1:
                    minColumn = 2;
                    break;
                case <= 3:
                    minColumn = 1;
                    break;
            }
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i >= minColumn && i < minColumn + column && row > j)
                    {
                        listGameObject[count].GetComponent<Image>().color = Color.red;
                    }
                    count++;
                }
            }
        }
    }
    public class ItemListener : MonoBehaviour
    {
        ItemViewUI itemViewUI;
        private void Awake()
        {
            itemViewUI = this.GetComponent<ItemViewUI>();
        }
        private float timeCount = 0, timeLimit = 1f;
        bool isDrag = false;
        private void OnMouseDrag()
        {
            timeCount += Time.deltaTime;
            if (timeCount >= timeLimit)
            {
                int indexWaifu = Data.DataController.instance.characterAssets.GetIndexWaifu(itemViewUI.characterInBattleClone.waifuIdentify.ID, itemViewUI.characterInBattleClone.waifuIdentify.SkinCheck);
                Data.Waifu.WaifuSkill waifuSkill = new Data.Waifu.WaifuSkill();
                waifuSkill = Data.DataController.instance.characterAssets.GetSkillWaifuSOByIndex(indexWaifu);

                // VerticalViewRight.instance.SetDataPopup(itemViewUI.characterInBattleClone);

                // VerticalViewRight.instance.ShowAndHidePopup(true);
                timeCount = 0;
                isDrag = true;
            }
        }


        private void OnMouseUp()
        {
            if (!isDrag && itemViewUI.characterInBattleClone.cooldownSkillBar.value >= 1 && BattleController.instance.gameState == GameState.BATTLE && !itemViewUI.characterInBattleClone.isAttack)
            {

                itemViewUI.characterInBattleClone.cooldownSkillBar.value = 0;
                SetAnimCharacter.instance.HeroUseSkillTest(itemViewUI.characterInBattleClone);
            }
            timeCount = 0;
            isDrag = false;
        }
    }
}
