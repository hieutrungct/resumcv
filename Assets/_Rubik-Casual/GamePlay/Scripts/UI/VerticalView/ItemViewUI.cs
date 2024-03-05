using System.Collections;
using System.Collections.Generic;
using RubikCasual.Waifu;
using TMPro;
using UnityEngine;

namespace RubikCasual.Battle.UI.VerticalView
{
    public class ItemViewUI : MonoBehaviour
    {
        public TextMeshProUGUI txtName, txtAtk, txtDef, txtHp, txtDameSkill, txtRow, txtColumn;
        public void SetDataPopup(InfoWaifuAsset infoWaifuAsset)
        {
            txtName.text = infoWaifuAsset.Name;
            txtAtk.text = "Atk: " + infoWaifuAsset.ATK.ToString();
            txtDef.text = "Def: " + infoWaifuAsset.ATK.ToString();
            txtHp.text = "Hp: " + infoWaifuAsset.ATK.ToString();
            txtDameSkill.text = "DameSkill: " + infoWaifuAsset.ATK.ToString();
            txtRow.text = "Row: " + infoWaifuAsset.ATK.ToString();
            txtColumn.text = "Column: " + infoWaifuAsset.ATK.ToString();
        }
    }
}
