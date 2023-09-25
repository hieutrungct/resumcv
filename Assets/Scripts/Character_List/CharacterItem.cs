using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Spine;
using Spine.Unity;
using Spine.Unity.Editor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Rubik_Casual
{
    public class CharacterItem : MonoBehaviour
    {
        public Image avaCard,attackType,avaBox, BackGlow,Glow,Role;
        public SkeletonGraphic hero;
        public TextMeshProUGUI nameTxt, levelTxt;
        [SerializeField] GameObject[] stars;
        [SerializeField] BtnOnClick btnClick;
        //[SerializeField] public GameObject inDeck;
        
        
        public void SetUp(Character character)
        {
           
            //avaCard.sprite = Common.GetAvatar(character.Name);
            //avaCard.sprite = AssetLoader.Instance.GetAvatarById(character.Name);
    
            hero.skeletonDataAsset = AssetLoader.instance.GetAvaById(character.Name);
            hero.initialSkinName = hero.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            hero.startingAnimation = hero.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;
            //hero.skeletonDataAsset.GetSkeletonData(true);
            //SpineEditorUtilities.ReloadSkeletonDataAsset(hero.skeletonDataAsset);
            SpineEditorUtilities.ReinitializeComponent(hero);
            
            // avaCard.SetNativeSize();
            if (avaBox != null)
            {
                avaBox.sprite = AssetLoader.Instance.RarrityBox[character.Rarity];
                switch(character.Rarity)
                {
                    case 1:
                        Glow.GetComponent<Image>().color = Color.blue;
                        break;
                    case 2:
                        Glow.GetComponent<Image>().color = Color.green;
                        break;
                    case 3:
                        Glow.GetComponent<Image>().color = Color.yellow;
                        break;
                    case 4:
                        Glow.GetComponent<Image>().color = Color.magenta;
                        break;
                }
                 
            }

            Role.sprite = AssetLoader.Instance.AttackSprite[character.Role];
            
            if (attackType != null)
            {
                switch (character.CharacterType)
                {
                    case CharacterType.Melee:
                        //attackType.sprite = AssetLoader.Instance.AttackSprite[0];
                        break;
                    case CharacterType.Ranged:
                        //attackType.sprite = AssetLoader.Instance.AttackSprite[1];
                        break;
                }
            }
           
            nameTxt.text = character.Name.ToString();
            levelTxt.text = "" + character.Level;
            for(int i = 0; i < character.Star; i++)
            {
                stars[i].SetActive(true);
                if (i < character.Ascend)
                {
                    stars[i].GetComponent<Image>().color = Color.red;
                }
            }

            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(()=>
                    {CharacterUIController.instance.ShowCharacterInfoPopup(character);});
            }

            //inDeck.SetActive(character.isInDeck);

        }
        
    }
}

