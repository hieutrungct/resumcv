using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rubik_Casual;
using RubikCasual.Combat.Character;
using RubikCasual.Combat.SlotCharacterInfo;
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
        public List<int> listIdSlotHero;
        void Start()
        {
            CreateCombat();
        }
        void CreateCombat()
        {
            checkIsInDeck();
            CreateSlotHero();
        }
        void CreateSlotHeroInfo(SkeletonDataAsset heroIsInDeck)
        {
            var slotHeroInfoClone = Instantiate(slotCharaterInfoUI, slotHeroInfoPos);
            slotHeroInfoClone.heroIcon.skeletonDataAsset = heroIsInDeck;
            slotHeroInfoClone.heroIcon.initialSkinName = heroIsInDeck.GetSkeletonData(true).Skins.Items[1].Name;

            SpineEditorUtilities.ReinitializeComponent(slotHeroInfoClone.heroIcon);
        }
        void CreateSlotHero()
        {
            int count = 0;
            foreach (var IdSlotHero in listIdSlotHero)
            {

                var heroClone = Instantiate(characterCombatUI, listSlotHero[count]);
                var heroDataAsset = AssetLoader.instance.GetAvaById(characterInfo.Characters.FirstOrDefault(f => f.ID == IdSlotHero).Nameid);
                heroClone.characterInCombat.skeletonDataAsset = heroDataAsset;
                heroClone.characterInCombat.initialSkinName = heroDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
                CreateSlotHeroInfo(heroDataAsset);
                
                SpineEditorUtilities.ReinitializeComponent(heroClone.characterInCombat);

                count++;
            }

            // foreach (var slotHero in listSlotHero)
            // {


            //     if (listHeroClone == null)
            //     {
            //         var checkInDeck = characterInfo.Characters.FirstOrDefault(f => f.isInDeck);
            //         heroClone.idHero = int.Parse(checkInDeck.id);

            //         if (checkInDeck.isInDeck)
            //         {

            //             var heroData = AssetLoader.instance.GetAvaById(checkIsInDeck.Nameid);
            //             heroClone.characterInCombat.skeletonDataAsset = heroData;
            //             heroClone.characterInCombat.initialSkinName = heroData.GetSkeletonData(true).Skins.Items[1].Name;
            //             CreateSlotHeroInfo(heroData);
            //         }

            //         SpineEditorUtilities.ReinitializeComponent(heroClone.characterInCombat);
            //         listHeroClone.Add(heroClone);
            //     }
            //     else
            //     {
            //         var checkInDeck = characterInfo.Characters.FirstOrDefault(f => f.isInDeck);
            //         foreach (var item in listHeroClone)
            //         {
            //             if (item.idHero != int.Parse(checkInDeck.id))
            //             {

            //             }
            //         }
            //     }

            // }
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
        void CreateCombatEnemy()
        {
            for (int i = 0; i < listSlotHero.Count; i++)
            {
                var enemyClone = Instantiate(characterCombatUI, listSlotHero[i]);
                SpineEditorUtilities.ReinitializeComponent(enemyClone.characterInCombat);
            }
        }

    }

}
