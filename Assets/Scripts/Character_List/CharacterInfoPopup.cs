using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using System;
using Spine.Unity;

namespace Rubik_Casual
{
    public class CharacterInfoPopup : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic avaCharacter;

        [SerializeField]
        private TextMeshProUGUI lvTxt, lvProcessTxt, damageTxt, defenseTxt, critTxt, healthTxt, moveSpeedTxt;
    }
}

