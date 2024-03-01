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
        public void SetImageItem(List<int> lsIndex)
        {
            for (int i = 0; i < lsImageIcon.Count; i++)
            {
                lsImageIcon[i].sprite = Data.DataController.instance.assetLoader.GetAvatarById(lsIndex[i].ToString());
            }
        }
        public void SetDataPopup(List<string> lsCode)
        {
            for (int i = 0; i < lsItemViewUI.Count; i++)
            {
                lsItemViewUI[i].SetDataPopup(Data.DataController.instance.characterAssets.GetInfoWaifuAsset(lsCode[i]));
            }
        }
    }
}
