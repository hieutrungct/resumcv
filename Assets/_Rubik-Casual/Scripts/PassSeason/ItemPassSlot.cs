using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
namespace RubikCasual.ItemPass
{
    public class ItemPassSlot : MonoBehaviour
    {
        public int id, index;
        public Image itemImg, itemChecked, itemClaim, itemUnlock;
        public TextMeshProUGUI txtItem;
    }
}

