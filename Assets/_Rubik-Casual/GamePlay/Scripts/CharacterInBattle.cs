using System.Collections;
using System.Collections.Generic;
using RubikCasual.Waifu;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle
{
    public class CharacterInBattle : MonoBehaviour
    {
        public int indexOfSlot;
        public InfoWaifuAsset infoWaifuAsset;
        public SkeletonAnimation skeletonCharacterAnimation;
        public float HpNow, Hp, Def, Atk, Rage;
        public Transform PosCharacter;
        public Slider healthBar, cooldownAttackBar, cooldownSkillBar;
        public TextMeshProUGUI txtHealthBar;
        public bool isAttack, isUseSkill, isEnemy = false, isCompleteMove = true, isBoss = false;

    }
}
