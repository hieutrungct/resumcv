using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle.UI.VerticalView
{
    public class VerticalViewRight : MonoBehaviour
    {
        public List<GameObject> lsStar, lsSlotEnemy, lsSlotHero;
        public Image iconImage;
        public TextMeshProUGUI txtName;
        public Button btnHidePopup;
        public Sprite spriteBuffAtk, spriteBuffDef, spriteBuffHp;
        private CharacterInBattle characterInBattleClone;
        bool isShowPopup, isFirstSetData = false;
        float OriginPosX;
        public static VerticalViewRight instance;
        protected void Awake()
        {
            instance = this;
            OriginPosX = this.transform.position.x;
            this.transform.DOMoveX(OriginPosX + 3f, 0);
            btnHidePopup.onClick.AddListener(() => { ShowAndHidePopup(); });

        }
        public void SetSkillPopup(Data.Waifu.WaifuSkill waifuSkill)
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
                case CreateSkill.TypeSkill.Buff_Atk:
                    foreach (GameObject slotHero in lsSlotHero)
                    {
                        slotHero.GetComponent<Image>().sprite = spriteBuffAtk;
                        slotHero.GetComponent<Image>().preserveAspect = true;
                    }
                    break;
                case CreateSkill.TypeSkill.Buff_Def:
                    foreach (GameObject slotHero in lsSlotHero)
                    {
                        slotHero.GetComponent<Image>().sprite = spriteBuffDef;
                        slotHero.GetComponent<Image>().preserveAspect = true;
                    }
                    break;
                case CreateSkill.TypeSkill.Buff_Hp:
                    foreach (GameObject slotHero in lsSlotHero)
                    {
                        slotHero.GetComponent<Image>().sprite = spriteBuffHp;
                        slotHero.GetComponent<Image>().preserveAspect = true;
                    }
                    break;
                case CreateSkill.TypeSkill.Wave:
                    ArraySkill(waifuSkill.Row, waifuSkill.Column, lsSlotEnemy);
                    break;
                case CreateSkill.TypeSkill.In_Turn:
                    ArraySkill(waifuSkill.Row, waifuSkill.Column, lsSlotEnemy);
                    break;
                case CreateSkill.TypeSkill.In_Turn_Plus:
                    ArraySkill(waifuSkill.Row, waifuSkill.Column, lsSlotEnemy);
                    break;
                case CreateSkill.TypeSkill.Default:
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
        public void SetDataPopup(CharacterInBattle characterInBattle)
        {
            characterInBattleClone = characterInBattle;
            // txtName.text = characterInBattle.infoWaifuAsset.Name;

            int indexWaifu = Data.DataController.instance.characterAssets.GetIndexWaifu(characterInBattle.waifuIdentify.ID, characterInBattle.waifuIdentify.SkinCheck);
            Data.Waifu.WaifuSkill waifuSkill = new Data.Waifu.WaifuSkill();
            waifuSkill = Data.DataController.instance.characterAssets.GetSkillWaifuSOByIndex(indexWaifu);



            iconImage.sprite = Data.DataController.instance.assetLoader.GetAvatarByIndex(indexWaifu);
            iconImage.preserveAspect = true;
            isFirstSetData = true;
        }
        public void ShowAndHidePopup(bool isChangeInfo = false)
        {
            if (isFirstSetData)
            {
                if (!isChangeInfo)
                {
                    if (isShowPopup)
                    {
                        this.transform.DOMoveX(OriginPosX + 3f, 0.5f);
                    }
                    else
                    {
                        this.transform.DOMoveX(OriginPosX, 0.5f);
                    }
                    isShowPopup = !isShowPopup;

                }
                else
                {
                    this.transform.DOMoveX(OriginPosX, 0.5f);
                    isShowPopup = true;
                }
            }

        }
    }
}
