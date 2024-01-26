using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Lobby
{
    public class SlotWaifuSelectUI : MonoBehaviour
    {
        public TextMeshProUGUI nameTxt, lvlTxt, expTxt, rateTxt;
        public List<GameObject> lsStar;
        public GameObject waittingSlot, slotCharacter;
        public Slider expSlider;
        public SkeletonGraphic skeletonWaifu;
    }
}