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
        public List<GameObject> lsStar;
        public Image iconImage;
        public TextMeshProUGUI txtName, txtAtk, txtDef, txtHp, txtDameSkill, txtRare;
        public Button btnHidePopup;
        private CharacterInBattle characterInBattleClone;
        public bool isShowPopup, isFirstSetData = false;
        float OriginPosX;
        public static VerticalViewRight instance;
        protected void Awake()
        {
            instance = this;
            OriginPosX = this.transform.position.x;
            this.transform.DOMoveX(OriginPosX + 3f, 0);
            btnHidePopup.onClick.AddListener(() => { ShowAndHidePopup(); });
        }
        public void SetDataPopup(CharacterInBattle characterInBattle)
        {
            characterInBattleClone = characterInBattle;
            txtName.text = characterInBattle.infoWaifuAsset.Name;

            int indexWaifu = Data.DataController.instance.characterAssets.GetIndexWaifu(characterInBattle.waifuIdentify.ID, characterInBattle.waifuIdentify.SkinCheck);
            Data.Waifu.WaifuSkill waifuSkill = new Data.Waifu.WaifuSkill();
            waifuSkill = Data.DataController.instance.characterAssets.GetSkillWaifuSOByIndex(indexWaifu);

            txtAtk.text = "ATK: " + characterInBattle.infoWaifuAsset.ATK.ToString();
            txtDef.text = "DEF: " + characterInBattle.infoWaifuAsset.DEF.ToString();
            txtHp.text = "HP: " + characterInBattle.infoWaifuAsset.HP.ToString();

            txtDameSkill.text = "Dame Skill: " + ((int)(characterInBattle.infoWaifuAsset.ATK * waifuSkill.percentDameSkill)).ToString();
            txtRare.text = "Rare: " + characterInBattle.infoWaifuAsset.Rare;

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
