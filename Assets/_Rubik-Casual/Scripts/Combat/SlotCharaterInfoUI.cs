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
        void Start()
        {
            slotEnemyClone = CombatController.instance.slotEnemyClone;

        }
        public void clickInfoCharacter(CharacterCombatUI HeroClone)
        {

            if (!doneSkill && HeroClone.doneTurn)
            {
                
                startTurnPos = HeroClone.transform.position;
                doneSkill = true;
                StartCoroutine(UseSkill(HeroClone));
            }

        }
        IEnumerator UseSkill(CharacterCombatUI HeroClone)
        {
            HeroClone.transform.position = (slotEnemyClone[0].transform.position + slotEnemyClone[0].transform.position) / 2 - new Vector3(1f, 0.5f, 0);
            HeroClone.characterInCombat.startingAnimation = "SkillCast";
            HeroClone.characterInCombat.startingLoop = false;
            SpineEditorUtilities.ReinitializeComponent(HeroClone.characterInCombat);

            yield return new WaitForSeconds(2f);
            HeroClone.transform.position = startTurnPos;
            HeroClone.characterInCombat.startingLoop = true;
            HeroClone.characterInCombat.startingAnimation = "Idle";
            SpineEditorUtilities.ReinitializeComponent(HeroClone.characterInCombat);

            yield return new WaitForSeconds(1f);
            doneSkill = false;
           
        }
    }
}
