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

        public void SetSliderBar(List<CharacterInBattle> lsHeroState)
        {

            for (int i = 0; i < lsItemViewUI.Count; i++)
            {
                if (lsHeroState[i] != null)
                {
                    lsItemViewUI[i].SetSliderBar(lsHeroState[i]);
                }
            }
        }
        public void SetImageItem(List<CharacterInBattle> lsHeroState)
        {
            for (int i = 0; i < lsImageIcon.Count; i++)
            {
                if (lsHeroState[i] != null)
                {
                    lsImageIcon[i].transform.parent.gameObject.SetActive(true);
                    // Debug.Log(waifuIdentifies[i].SkinCheck);
                    lsImageIcon[i].sprite = Data.DataController.instance.assetLoader.GetAvatarByIndex(Data.DataController.instance.characterAssets.GetIndexWaifu(lsHeroState[i].waifuIdentify.ID, lsHeroState[i].waifuIdentify.SkinCheck));
                }
                else
                {
                    lsImageIcon[i].transform.parent.gameObject.SetActive(false);
                }
            }
        }
        public void SetDataPopup(List<CharacterInBattle> lsHeroState)
        {

            for (int i = 0; i < lsItemViewUI.Count; i++)
            {
                if (lsHeroState[i] != null)
                {
                    lsItemViewUI[i].SetDataPopup(lsHeroState[i]);
                }
            }
        }
    }
}
