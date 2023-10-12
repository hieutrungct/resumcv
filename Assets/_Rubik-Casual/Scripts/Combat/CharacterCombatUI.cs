using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Combat.Character
{
    public class CharacterCombatUI : MonoBehaviour
    {
        public int id, idHero;
        public SkeletonGraphic characterInCombat;
        public Slider healthSlider;
        public Sprite healthSpriteHero, healthSpriteEnemy;
        public Image fill;
        public bool isHero, doneTurn;

    }

}
