using System.Collections;
using System.Collections.Generic;
using RubikCasual.Combat.Character;
using Spine.Unity;
using Spine.Unity.Editor;
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
        public List<CharacterCombatUI> slotHeroClone, slotEnemyClone;
        bool doneSkill;
        Vector3 startTurnPos;

    }
}
