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
        private Character _character;
        
        public void SetUp(Character character)
        {
            //_character = character;
            //avaCard.sprite = Common.GetAvatar(character.Name);
            //avaCard.sprite = AssetLoader.Instance.GetAvatarById(character.Name);
    
            hero.skeletonDataAsset = AssetLoader.instance.GetAvaById(character.Nameid);
            hero.initialSkinName = hero.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            hero.startingAnimation = hero.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;
            //hero.skeletonDataAsset.GetSkeletonData(true);
            //SpineEditorUtilities.ReloadSkeletonDataAsset(hero.skeletonDataAsset);
            SpineEditorUtilities.ReinitializeComponent(hero);
            
            // avaCard.SetNativeSize();
            if (avaBox != null)
            {
                
                switch(character.Rarity)
                {
                    case Rare.UnCommon:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[0];
                        break;
                    case Rare.Common:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[1];
                        Glow.GetComponent<Image>().color = new Color(0.043f, 0.455f, 0.808f, 1f);
                        BackGlow.GetComponent<Image>().color = new Color(0.474f, 0.918f, 1f, 1f);
                        break;
                    case Rare.Rare:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[2];
                        Glow.GetComponent<Image>().color = new Color(0f, 0.698f, 0.443f, 1f);
                        BackGlow.GetComponent<Image>().color = new Color(1f, 0.953f, 0f, 1f);
                        break;
                    case Rare.Epic:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[3];
                        Glow.GetComponent<Image>().color = new Color(0.886f, 0.58f, 0.173f, 1f);
                        BackGlow.GetComponent<Image>().color = new Color(1f, 0.313f, 0f, 1f);
                        break;
                    case Rare.Legend:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[4];
                        Glow.GetComponent<Image>().color = new Color(0.737f, 0.267f, 0.773f, 1f);
                        BackGlow.GetComponent<Image>().color = new Color(0.929f, 0.459f, 1f, 1f);
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

