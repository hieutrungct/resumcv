using System.Collections;
using System.Collections.Generic;
using Rubik.Waifu;
using RubikCasual.Waifu;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle
{
    public class CharacterInBattle : MonoBehaviour
    {
        public int indexOfSlot;
        public InfoWaifuAsset infoWaifuAsset;
        public SkeletonAnimation skeletonCharacterAnimation;
        public float HpNow, Rage;
        public Transform PosCharacter;
        public Slider healthBar, cooldownAttackBar, cooldownSkillBar;
        public bool isAttack;

    }
}
