using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.Battle;
using RubikCasual.Data;
using RubikCasual.Data.Waifu;
using TMPro;
using UnityEngine;

namespace RubikCasual.CreateSkill.Panel
{
    public class InfoEnemyPanel : MonoBehaviour
    {
        public TextMeshProUGUI txtIndexID, txtName, txtHp, txtAtk, txtDef, txtPow, txtRare, txtElement, txtClassWaifu, txtSkill, txtIsBoss;
        public static InfoEnemyPanel instance;
        float OriginPosX;
        bool isHidePopup;
        void Awake()
        {
            instance = this;
            OriginPosX = this.transform.position.x;
        }
        public void SetInfoPanel(int index)
        {
            Waifu.InfoWaifuAsset infoWaifuAsset = DataController.instance.characterAssets.enemyAssets.GetInfoEnemyAsset(index);

            txtIndexID.text = "Index: " + infoWaifuAsset.Code.ToString();
            if (infoWaifuAsset.Name != null)
            {
                txtName.text = "Name: " + infoWaifuAsset.Name.ToString();
            }
            else
            {
                txtName.text = "Name: null";
            }

            txtHp.text = "HP: " + infoWaifuAsset.HP.ToString();
            txtAtk.text = "ATK: " + infoWaifuAsset.ATK.ToString();
            txtDef.text = "DEF: " + infoWaifuAsset.DEF.ToString();
            txtPow.text = "POW: " + infoWaifuAsset.Pow.ToString();
            txtSkill.text = "Skill Dmg: " + infoWaifuAsset.Skill.ToString();
            txtRare.text = "Rare: " + infoWaifuAsset.Rare.ToString();
            txtElement.text = "Element: " + infoWaifuAsset.Element.ToString();
            txtClassWaifu.text = "Class Enemy: " + infoWaifuAsset.ClassWaifu.ToString();
            txtIsBoss.text = "Is Boss: " + DataController.instance.characterAssets.enemyAssets.GetWaifuSOEByIndex(index.ToString()).Is_Boss.ToString();

            txtAtk.gameObject.SetActive(true);
            txtDef.gameObject.SetActive(true);
            txtPow.gameObject.SetActive(true);
            txtSkill.gameObject.SetActive(true);
            txtRare.gameObject.SetActive(true);
            txtElement.gameObject.SetActive(true);
            txtClassWaifu.gameObject.SetActive(true);
            txtIsBoss.gameObject.SetActive(true);
        }
        public void SetInfoItemPanel(int index)
        {
            DailyItem.infoItem infoItem = DataController.instance.itemData.InfoItems[index];

            txtIndexID.text = "Index: " + infoItem.id;
            txtName.text = "Name: " + infoItem.name;
            txtHp.text = "Type: " + infoItem.type;
            txtAtk.gameObject.SetActive(false);
            txtDef.gameObject.SetActive(false);
            txtPow.gameObject.SetActive(false);
            txtSkill.gameObject.SetActive(false);
            txtRare.gameObject.SetActive(false);
            txtElement.gameObject.SetActive(false);
            txtClassWaifu.gameObject.SetActive(false);
            txtIsBoss.gameObject.SetActive(false);
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
