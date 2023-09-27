using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik_Casual
{
    public class Character_ListController : MonoBehaviour
    {
        
        public void OnSortButtonClickedLever()
        {
            CharacterUIController.instance.SortChar(1);
        }

        public void OnSortButtonClickedRarity()
        {
            CharacterUIController.instance.SortChar(0);
        }

        

        
        
    }
}

