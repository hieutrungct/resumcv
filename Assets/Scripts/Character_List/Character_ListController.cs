using System;
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
        private int  b, c;
        private int a = 3;
        private bool isFirstClick = true;
        
        public void OnSortButtonClickedLever()
        {
            
            CharacterUIController.instance.SortChar(1);
            SetButtonColors(SortingType.Lever);
            if (isFirstClick)
            {
                a = a - 2;
                isFirstClick = false;
            }
            else
            {
                a--;
            }
            b = c = 0;
            if (a < 1)
            {
                a = 2;
            }
            Debug.Log(a);
            switch (a)
            {
                case 2:
                    CharacterUIController.instance.RefreshCharacterUI();
                    break;
                case 1:
                    CharacterUIController.instance.RefreshCharacterUIOpp();
                    break;
            }
            
        }

        public void OnSortButtonClickedRarity()
        {
            CharacterUIController.instance.SortChar(0);
            SetButtonColors(SortingType.Rarity);
            b++;
            c = 0;
            a = 3;
            if (b > 2)
            {
                b = 1;
            }
            //Debug.Log(b);
            switch (b)
            {
                case 1:
                    CharacterUIController.instance.RefreshCharacterUI();
                    break;
                case 2:
                    CharacterUIController.instance.RefreshCharacterUIOpp();
                    break;
            }
            
        }
        public void OnSortButtonClickedPower()
        {
            CharacterUIController.instance.SortChar(2);
            SetButtonColors(SortingType.Power);
            c++;
            b = 0;
            a = 3;
            if (c > 2)
            {
                c = 1;
            }
            switch (c)
            {
                case 1:
                    CharacterUIController.instance.RefreshCharacterUI();
                    break;
                case 2:
                    CharacterUIController.instance.RefreshCharacterUIOpp();
                    break;
            }
        }
        private void SetButtonColors(SortingType selectedSortingType)
        {
            // Đặt màu cho nút đã chọn.
            textListSortLever.color = (selectedSortingType == SortingType.Lever) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortRarity.color = (selectedSortingType == SortingType.Rarity) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortPower.color = (selectedSortingType == SortingType.Power) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
        }
    }
}

