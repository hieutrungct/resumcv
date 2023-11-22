using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RubikCasual.Combat.Character;
using RubikCasual.Combat.SlotCharacterInfo;
using RubikCasual.EnemyData;
using RubikCasual.StageData;
using Spine.Unity;
using Spine.Unity.Editor;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Combat
{
    public class CombatController : MonoBehaviour
    {
        public List<Transform> listSlotHero, listSlotEnemy;
        public Transform slotHeroInfoPos;
        public CharacterCombatUI characterCombatUI;
        public SlotCharaterInfoUI slotCharaterInfoUI;
        public Rubik_Casual.CharacterInfo characterInfo;
        public List<SlotCharaterInfoUI> listSlotHeroInfoClone;
        public List<CharacterCombatUI> slotHeroClone, slotEnemyClone;
        public StageDataController stageData;
        public EnemyDataController enemyData;
        public int idLvl, idState;
        public List<int> listIdSlotHero, listIdSlotEnemy;
        public static CombatController instance;
        void Start()
        {
            instance = this;
            CreateCombat();
            instance.gameObject.AddComponent<GamePlay>();
        }
        void CreateCombat()
        {
            checkIsInDeck();
            checkEnemyInLevel();
            CreateSlotHero();
            CreateCombatEnemy();
        }
        void CreateSlotHeroInfo(SkeletonDataAsset heroIsInDeck, CharacterCombatUI heroClone)
        {
            var slotHeroInfoClone = Instantiate(slotCharaterInfoUI, slotHeroInfoPos);
            var heroData = characterInfo.Characters.FirstOrDefault(f => f.Nameid == heroIsInDeck.name);
            slotHeroInfoClone.gameObject.AddComponent<Button>().onClick.AddListener(() =>
            {
                slotHeroInfoClone.clickInfoCharacter(heroClone);
            });
            slotHeroInfoClone.idHero = heroData.ID;
            slotHeroInfoClone.heroIcon.skeletonDataAsset = heroIsInDeck;
            slotHeroInfoClone.heroIcon.initialSkinName = heroIsInDeck.GetSkeletonData(true).Skins.Items[1].Name;
            slotHeroInfoClone.textLvl.text = heroData.Level.ToString();
            slotHeroInfoClone.heroIcon.timeScale = heroClone.characterInCombat.timeScale;
            slotHeroInfoClone.heroIcon.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);

            float maxHp = heroData.Health;
            slotHeroInfoClone.sliderBlue.value = 5 / maxHp;
            slotHeroInfoClone.sliderBlue.interactable = false;
            slotHeroInfoClone.sliderYellow.value = 1;
            slotHeroInfoClone.sliderYellow.interactable = false;
            slotHeroInfoClone.sliderRed.value = 0;
            slotHeroInfoClone.sliderRed.interactable = false;

            switch (characterInfo.Characters.FirstOrDefault(f => f.ID == slotHeroInfoClone.idHero).Rarity)
            {
                // case Rare.UnCommon:
                //     avaBox.sprite = AssetLoader.Instance.RarrityBox[0];
                //     break;
                case Rubik_Casual.Rare.Common:

                    slotHeroInfoClone.backGlow.color = new Color(180 / 255f, 1f, 1f, 1f);
                    slotHeroInfoClone.bottomGlow.color = new Color(0.474f, 0.918f, 1f, 1f);
                    slotHeroInfoClone.frame.color = new Color(0.474f, 0.918f, 1f, 1f);
                    break;
                case Rubik_Casual.Rare.Rare:

                    slotHeroInfoClone.backGlow.color = new Color(162 / 255f, 1f, 162 / 255f, 1f);
                    slotHeroInfoClone.bottomGlow.color = new Color(70 / 255f, 1f, 70 / 255f, 1f);
                    slotHeroInfoClone.frame.color = new Color(100 / 255f, 1f, 100 / 255f, 1f);
                    break;
                case Rubik_Casual.Rare.Epic:

                    slotHeroInfoClone.backGlow.color = new Color(1f, 162 / 255f, 162 / 255f, 1f);
                    slotHeroInfoClone.bottomGlow.color = new Color(1f, 0.313f, 0f, 1f);
                    slotHeroInfoClone.frame.color = new Color(1f, 0.313f, 0f, 1f);
                    break;
                case Rubik_Casual.Rare.Legend:

                    slotHeroInfoClone.backGlow.color = new Color(1f, 162 / 255f, 1f, 1f);
                    slotHeroInfoClone.bottomGlow.color = new Color(0.929f, 0.459f, 1f, 1f);
                    slotHeroInfoClone.frame.color = new Color(0.737f, 0.267f, 0.773f, 1f);
                    break;
            }

            SpineEditorUtilities.ReinitializeComponent(slotHeroInfoClone.heroIcon);
            listSlotHeroInfoClone.Add(slotHeroInfoClone);
        }

        void CreateSlotHero()
        {
            int count = 0;
            foreach (var IdSlotHero in listIdSlotHero)
            {

                var heroClone = Instantiate(characterCombatUI, listSlotHero[count]);
                var heroDataAsset = Rubik_Casual.AssetLoader.instance.GetAvaById(characterInfo.Characters.FirstOrDefault(f => f.ID == IdSlotHero).Nameid);
                heroClone.characterInCombat.skeletonDataAsset = heroDataAsset;
                heroClone.characterInCombat.initialSkinName = heroDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
                heroClone.characterInCombat.timeScale = UnityEngine.Random.Range(1f, 1.5f);
                heroClone.healthSlider.value = 1;
                heroClone.healthSlider.interactable = false;
                heroClone.isHero = true;

                CreateSlotHeroInfo(heroDataAsset, heroClone);
                SpineEditorUtilities.ReinitializeComponent(heroClone.characterInCombat);
                slotHeroClone.Add(heroClone);
                count++;
            }
        }
        void checkIsInDeck()
        {
            foreach (var item in characterInfo.Characters)
            {
                if (item.isInDeck)
                {
                    listIdSlotHero.Add(item.ID);
                }
            }
        }
        void checkEnemyInLevel()
        {
            foreach (var item in stageData.stages.FirstOrDefault(f => f.idStage == idState).levelInStages.FirstOrDefault(f => f.idLvl == idLvl).enemyAtacks)
            {
                listIdSlotEnemy.Add(item.idEnemy);
            }
        }
        void CreateCombatEnemy()
        {
            var levelData = stageData.stages.FirstOrDefault(f => f.idStage == idState).levelInStages.FirstOrDefault(f => f.idLvl == idLvl);

            int count = 0;
            foreach (var IdSlotEnemy in listIdSlotEnemy)
            {

                var enemyClone = Instantiate(characterCombatUI, listSlotEnemy[count]);
                var enemyDataAsset = Rubik_Casual.AssetLoader.instance.GetAvaByNameEn(enemyData.enemy.FirstOrDefault(f => f.idEnemy == IdSlotEnemy).NameEnemyid);

                enemyClone.characterInCombat.skeletonDataAsset = enemyDataAsset;
                enemyClone.characterInCombat.initialSkinName = enemyData.enemy.FirstOrDefault(f => f.idEnemy == IdSlotEnemy).skinName;
                enemyClone.characterInCombat.initialFlipX = true;
                enemyClone.characterInCombat.timeScale = 1;
                enemyClone.characterInCombat.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                enemyClone.characterInCombat.timeScale = UnityEngine.Random.Range(1f, 1.5f);


                enemyClone.fill.sprite = enemyClone.healthSpriteEnemy;
                enemyClone.healthSlider.value = 1;
                enemyClone.healthSlider.interactable = false;
                // heroClone.characterInCombat.timeScale = UnityEngine.Random.Range(1f, 1.5f);


                SpineEditorUtilities.ReinitializeComponent(enemyClone.characterInCombat);
                slotEnemyClone.Add(enemyClone);
                count++;
            }

        }

    }

}
