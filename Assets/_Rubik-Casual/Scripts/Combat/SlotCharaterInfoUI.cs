using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Combat.SlotCharacterInfo
{
    public class SlotCharaterInfoUI : MonoBehaviour
    {
        public int idHero;
        public SkeletonGraphic heroIcon;
        public TextMeshProUGUI textLvl;
        public Image backGlow, bottomGlow, frame;
        public Slider sliderRed, sliderYellow, sliderBlue;
    }
}
