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
            txtAtk.text = infoWaifuAsset.ATK.ToString();
            txtDef.text = infoWaifuAsset.ATK.ToString();
            txtHp.text = infoWaifuAsset.ATK.ToString();
            txtDameSkill.text = infoWaifuAsset.ATK.ToString();
            txtRow.text = infoWaifuAsset.ATK.ToString();
            txtColumn.text = infoWaifuAsset.ATK.ToString();
        }
    }
}
