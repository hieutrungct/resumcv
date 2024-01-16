using System.Collections;
using System.Collections.Generic;
using RubikCasual.RewardInGame;
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
        float valueCoins = 0, ValueGems = 0;
        bool isHaveReward = false;
        void Start()
        {
            AddRewardWhenKillEnemy();
        }

        void AddRewardWhenKillEnemy()
        {
            if (UnityEngine.Random.Range(0, 4) != 0)
            {
                valueCoins = 1;
            }
            else
            {
                ValueGems = 1;
            }
        }
        public void GetRewardWhenKillEnemy()
        {
            if (HpNow <= 0 && !isHaveReward)
            {
                if (valueCoins != 0)
                {
                    RewardInGamePanel.instance.txtCoins.text = (valueCoins + float.Parse(RewardInGamePanel.instance.txtCoins.text)).ToString();
                    // Debug.Log("TxtCoins: " + RewardInGamePanel.instance.txtCoins.text);
                }
                else
                {
                    RewardInGamePanel.instance.txtGems.text = (ValueGems + float.Parse(RewardInGamePanel.instance.txtGems.text)).ToString();
                }
                isHaveReward = !isHaveReward;
            }
        }
        void SetAnimCoins()
        {
            
        }
    }
}
