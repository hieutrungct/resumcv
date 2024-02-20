using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.Battle;
using TMPro;
using UnityEngine;

namespace RubikCasual.CreateSkill.Panel
{
    public class InfoWaifuPanel : MonoBehaviour
    {
        public TextMeshProUGUI txtIndexID, txtName, txtHp, txtAtk, txtDef, txtPow, txtRare, txtElement, txtClassWaifu, txtSkill;
        public static InfoWaifuPanel instance;
        float OriginPosX;
        bool isHidePopup;
        void Awake()
        {
            instance = this;
            OriginPosX = this.transform.position.x;
        }
        public void SetInfoPanel(CharacterInBattle characterSetSkill)
        {
            Waifu.InfoWaifuAsset infoWaifuAsset = characterSetSkill.infoWaifuAsset;
            txtIndexID.text = "Index Id: " + infoWaifuAsset.ID.ToString();
            txtName.text = "Name: " + infoWaifuAsset.Name.ToString();
            txtHp.text = "HP: " + infoWaifuAsset.HP.ToString();
            txtAtk.text = "ATK: " + infoWaifuAsset.ATK.ToString();
            txtDef.text = "DEF: " + infoWaifuAsset.DEF.ToString();
            txtPow.text = "POW: " + infoWaifuAsset.Pow.ToString();
            txtSkill.text = "Skill Dmg: " + characterSetSkill.Skill.ToString();
            txtRare.text = "Rare: " + infoWaifuAsset.Rare.ToString();
            txtElement.text = "Element: " + infoWaifuAsset.Element.ToString();
            txtClassWaifu.text = "Class Waifu: " + infoWaifuAsset.ClassWaifu.ToString();
        }
        public void ShowAndHidePopup()
        {
            if (!isHidePopup)
            {
                this.transform.DOMoveX(OriginPosX + 3f, 0.5f);
            }
            else
            {
                this.transform.DOMoveX(OriginPosX, 0.5f);
            }
            isHidePopup = !isHidePopup;
        }
    }
}
