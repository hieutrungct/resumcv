using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle.UI.VerticalView
{
    public class VerticalViewLeft : MonoBehaviour
    {
        public List<ItemViewUI> lsItemViewUI = new List<ItemViewUI>();
        public List<Image> lsImageIcon = new List<Image>();


        public void SetImageItem(List<Data.Player.CurentTeam> waifuIdentifies)
        {
            for (int i = 0; i < lsImageIcon.Count; i++)
            {
                if (waifuIdentifies[i] != null)
                {
                    // Debug.Log(waifuIdentifies[i].SkinCheck);
                    lsImageIcon[i].sprite = Data.DataController.instance.assetLoader.GetAvatarByIndex(Data.DataController.instance.characterAssets.GetIndexWaifu(waifuIdentifies[i].ID, waifuIdentifies[i].SkinCheck));
                }
            }
        }
        public void SetDataPopup(List<string> lsCode)
        {

            for (int i = 0; i < lsItemViewUI.Count; i++)
            {
                if (lsCode[i] != "0")
                {
                    lsItemViewUI[i].SetDataPopup(Data.DataController.instance.characterAssets.GetInfoWaifuAsset(lsCode[i]));
                }
            }
        }
    }
}
