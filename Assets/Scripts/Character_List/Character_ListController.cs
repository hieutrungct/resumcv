using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik_Casual
{
    public class Character_ListController : MonoBehaviour
    {
        public TextMeshProUGUI textListSortLever, textListSortRarity, textListSortPower;

        public void OnSortButtonClickedLever()
        {
            CharacterUIController.instance.SortChar(1);
            textListSortLever.color = new Color(1f, 1f, 1f, 1f);
            textListSortPower.color = new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortRarity.color = new Color(0.29f, 0.67f, 0.97f, 1f);

        }

        public void OnSortButtonClickedRarity()
        {
            CharacterUIController.instance.SortChar(0);
            textListSortRarity.color = new Color(1f, 1f, 1f, 1f);
            textListSortLever.color = new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortPower.color = new Color(0.29f, 0.67f, 0.97f, 1f);
            
        }
        public void OnSortButtonClickedPower()
        {
            CharacterUIController.instance.SortChar(2);
            textListSortPower.color = new Color(1f, 1f, 1f, 1f);
            textListSortLever.color = new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortRarity.color = new Color(0.29f, 0.67f, 0.97f, 1f);
            
        }
    }
}

