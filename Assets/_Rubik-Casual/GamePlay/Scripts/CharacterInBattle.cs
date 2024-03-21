using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RubikCasual.Battle.Inventory;
using RubikCasual.Battle.UI;
using RubikCasual.DailyItem;
using RubikCasual.Data;
using RubikCasual.RewardInGame;
using RubikCasual.Waifu;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RubikCasual.Battle
{
    public class CharacterInBattle : MonoBehaviour
    {
        public int indexOfSlot;
        public Data.Player.CurentTeam waifuIdentify;
        public InfoWaifuAsset infoWaifuAsset = new InfoWaifuAsset();
        public SkeletonAnimation skeletonCharacterAnimation;
        public int HpNow, Hp, Def, Atk, Skill, Rage;
        public Transform PosCharacter;
        public Slider healthBar, cooldownAttackBar, cooldownSkillBar;
        public TextMeshProUGUI txtHealthBar;
        public bool isAttack, isUseSkill, isEnemy = false, isBoss = false;
        GameObject ItemDropClone;

        bool isHaveReward = false;

        void Start()
        {
            Scene SceneTarget = SceneManager.GetSceneByName(NameScene.GAMEPLAY_SCENE);
            if (gameObject.scene == SceneTarget && (isEnemy || isBoss))
            {
                AddRewardWhenKillEnemy();
            }
        }

        void AddRewardWhenKillEnemy()
        {
            Transform posItemReward = BattleController.instance.dameSlotTxtController.lsPosEnemySlot.Find(f => f.name == this.transform.parent.parent.name).lsPosCharacterSlot.Find(f => f.name == this.transform.parent.name).transform; ;
            ItemDropClone = Instantiate(UIGamePlay.instance.ItemDrop, posItemReward);
            ItemDropClone.SetActive(false);
            SlotInventory ItemDrop = ItemDropClone.GetComponent<SlotInventory>();
            if (UnityEngine.Random.Range(0, 4) != 0)
            {
                DailyItem.infoItem infoItem = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.name == NameItem.Coins);
                ItemDrop.Icon.GetComponent<Image>().sprite = infoItem.imageItem;
                ItemDrop.valueCoins = 1;
            }
            else
            {

                DailyItem.infoItem infoItem = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.name == NameItem.Gems);
                ItemDrop.Icon.GetComponent<Image>().sprite = infoItem.imageItem;
                ItemDrop.ValueGems = 1;
            }
        }
        public void GetRewardWhenKillEnemy()
        {
            if (HpNow <= 0 && !isHaveReward)
            {
                ItemDropClone.transform.position = this.transform.position;
                if (ItemDropClone.GetComponent<SlotInventory>().valueCoins != 0)
                {
                    // Debug.Log("TxtCoins: " + RewardInGamePanel.instance.txtCoins.text);
                    ItemDropClone.SetActive(true);
                    ItemDropClone.GetComponent<SlotInventory>().BtnTest();
                    // RewardInGamePanel.instance.txtCoins.text = (valueCoins + float.Parse(RewardInGamePanel.instance.txtCoins.text)).ToString();
                }
                else
                {
                    ItemDropClone.SetActive(true);
                    ItemDropClone.GetComponent<SlotInventory>().BtnTest();
                    // RewardInGamePanel.instance.txtGems.text = (ValueGems + float.Parse(RewardInGamePanel.instance.txtGems.text)).ToString();
                }
                isHaveReward = !isHaveReward;
            }
        }
        
    }
}
