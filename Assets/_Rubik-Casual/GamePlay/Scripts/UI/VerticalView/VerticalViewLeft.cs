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

        public void ShowFocus(List<CharacterInBattle> lsHeroState)
        {
            for (int i = 0; i < lsItemViewUI.Count; i++)
            {
                if (lsHeroState[i] != null)
                {
                    lsItemViewUI[i].ShowFocus(lsHeroState[i]);
                }
            }
        }
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
        public void SetShowInfo(List<CharacterInBattle> lsHeroState)
        {
            for (int i = 0; i < lsItemViewUI.Count; i++)
            {
                if (lsHeroState[i] != null)
                {
                    lsItemViewUI[i].gameObject.SetActive(true);
                }
                else
                {
                    lsItemViewUI[i].gameObject.SetActive(false);
                }
            }
        }
        public void SetImageItem(List<CharacterInBattle> lsHeroState)
        {
            for (int i = 0; i < lsImageIcon.Count; i++)
            {
                if (lsHeroState[i] != null)
                {
                    // Debug.Log(waifuIdentifies[i].SkinCheck);
                    lsImageIcon[i].sprite = Data.DataController.instance.assetLoader.GetAvatarByIndex(Data.DataController.instance.characterAssets.GetIndexWaifu(lsHeroState[i].waifuIdentify.ID, lsHeroState[i].waifuIdentify.SkinCheck));
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
