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
        public InfoWaifuAsset infoWaifuAsset;
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
        public void SetCharacterInBattle(SlotInArea CharacterInArea, PositionCharacterSlot posSlot, float attribute)
        {
            DataController dataController = DataController.instance;
            if (!isEnemy)
            {
                waifuIdentify.ID = CharacterInArea.idCharacter;
                waifuIdentify.SkinCheck = CharacterInArea.isSkin;

                skeletonCharacterAnimation = SpawnCharacter(CharacterInArea, dataController);

                CharacterDragPosition CharacterHero = skeletonCharacterAnimation.gameObject.AddComponent<CharacterDragPosition>();
                CharacterHero.CharacterSke = skeletonCharacterAnimation;
                CharacterHero.posCharacter = posSlot.gameObject.transform;
                CharacterHero.oriIndex = indexOfSlot;

                Data.Player.PlayerOwnsWaifu playerOwnsWaifu = dataController.GetPlayerOwnsWaifuByID(CharacterInArea.idCharacter);

                infoWaifuAsset = dataController.characterAssets.GetInfoWaifuAsset(CharacterInArea.idCharacter);

                cooldownAttackBar.value = 1f;
                cooldownSkillBar.value = 0f;
                healthBar.value = 1f;
                Rage = 0;

                Hp = playerOwnsWaifu.HP;
                HpNow = playerOwnsWaifu.HP;
                Atk = playerOwnsWaifu.ATK;
                Def = playerOwnsWaifu.DEF;
                Skill = (int)(dataController.characterAssets.GetSkillWaifuSOByIndex(dataController.characterAssets.GetIndexWaifu(CharacterInArea.idCharacter, CharacterInArea.isSkin)).percentDameSkill * Atk);
            }
            else
            {

                skeletonCharacterAnimation = SpawnCharacter(CharacterInArea, dataController);


                infoWaifuAsset = dataController.characterAssets.GetInfoEnemyAsset(CharacterInArea.idCharacter);

                cooldownAttackBar.value = 0;
                cooldownSkillBar.value = 0;
                healthBar.value = 1;
                Rage = 0;

                Hp = (int)(infoWaifuAsset.HP * attribute);
                Def = (int)(infoWaifuAsset.DEF * attribute);
                Atk = (int)(infoWaifuAsset.ATK * attribute);
                HpNow = (int)(infoWaifuAsset.HP * attribute);

                if (isBoss)
                {
                    txtHealthBar.text = HpNow.ToString() + "/" + Hp.ToString();
                }
                else
                {
                    skeletonCharacterAnimation.gameObject.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_ShowPopup;
                }
            }


        }

        SkeletonAnimation SpawnCharacter(SlotInArea characterInArea, DataController dataController)
        {
            Transform poscharacterInBattle = PosCharacter;
            SkeletonAnimation character;
            if (!isEnemy)
            {
                character = dataController.characterAssets.WaifuAssets.Get2D(characterInArea.idCharacter.ToString(), characterInArea.isSkin);
            }
            else
            {
                character = Instantiate(dataController.characterAssets.enemyAssets.Get2D(characterInArea.idCharacter.ToString()));
            }

            if (isBoss)
            {
                character.transform.localScale = dataController.characterAssets.WaifuAssets.transform.localScale * 1f / 2f;
            }
            else
            {
                character.transform.localScale = dataController.characterAssets.WaifuAssets.transform.localScale * 2f / 3f;
            }
            character.gameObject.transform.SetParent(poscharacterInBattle);
            character.gameObject.transform.position = poscharacterInBattle.position;
            character.loop = true;
            character.AnimationName = NameAnim.Anim_Character_Idle;
            if (isEnemy)
            {
                character.transform.localScale = new Vector3(-character.transform.localScale.x, character.transform.localScale.y, character.transform.localScale.z);
            }
            character.gameObject.GetComponent<MeshRenderer>().sortingLayerName = NameLayer.Layer_Character;
            return character;
        }

        void AddRewardWhenKillEnemy()
        {
            Transform posItemReward = BattleController.instance.dameSlotTxtController.lsPosEnemySlot.Find(f => f.name == this.transform.parent.parent.name).lsPosCharacterSlot.Find(f => f.name == this.transform.parent.name).transform; ;
            ItemDropClone = Instantiate(UIGamePlay.instance.ItemDrop, posItemReward);
            ItemDropClone.SetActive(false);
            SlotInventory ItemDrop = ItemDropClone.GetComponent<SlotInventory>();
            if (UnityEngine.Random.Range(0, 5000000) != 50)
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
