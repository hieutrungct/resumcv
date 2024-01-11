using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Rubik_Casual;
using RubikCasual.Battle;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.Waifu;
using Spine.Unity;
using Spine.Unity.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ListWaifu
{
    public class WaifuItem : MonoBehaviour
    {
        public Image avaCard,attackType,avaBox, BackGlow,Glow,Role;
        public SkeletonGraphic UI_Waifu;
        private SkeletonAnimation ui_Waifu;
        public TextMeshProUGUI nameTxt, levelTxt;
        [SerializeField] GameObject[] stars;
        [SerializeField] BtnOnClick btnClick;
        //[SerializeField] public GameObject inDeck;
        private PlayerOwnsWaifu _waifu;
        public WaifuAssets waifuAssets;
        private InfoWaifuAsset infoWaifu;
        
        public void SetUp(PlayerOwnsWaifu waifu)
        {
            _waifu = waifu;
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.Index);
            
            
            //Debug.Log("class" + infoWaifu.Code);

            //ui_Waifu = SpawnCharacter(gameObject.transform, waifuAssets.Get2D(_waifu.Index.ToString()));
            //ui_Waifu =  waifuAssets.Get2D(_waifu.Index.ToString());

            if (infoWaifu.Code == waifuAssets.GetWaifuSOByIndex(waifu.Index.ToString()).Code && infoWaifu.Rare == "R")
            {
                _waifu.Index = waifuAssets.GetWaifuSOByIndex(waifu.Index.ToString()).Index;
            }
            else if (infoWaifu.Code == waifuAssets.GetWaifuSOByIndex(waifu.Index.ToString()).Code && infoWaifu.Rare == "SR")
            {
                _waifu.Index = waifuAssets.GetWaifuSOByIndex(waifu.Index.ToString()).Index;
            }


            SkeletonDataAsset skeletonDataAsset = waifuAssets.GetWaifuSOByIndex(_waifu.Index.ToString()).SkeletonDataAsset;
            UI_Waifu.skeletonDataAsset = skeletonDataAsset;


            UI_Waifu.initialSkinName = UI_Waifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            UI_Waifu.startingAnimation = UI_Waifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;

            
            SpineEditorUtilities.ReinitializeComponent(UI_Waifu);


            
            // // avaCard.SetNativeSize();

            if (avaBox != null)
            {
                
                switch(infoWaifu.Rare)
                {
                    // case Rare.UnCommon:
                    //     avaBox.sprite = AssetLoader.Instance.RarrityBox[0];
                    //     break;
                    case "R":
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[1];
                        Glow.GetComponent<Image>().color = new Color(0.043f, 0.455f, 0.808f, 1f);
                        BackGlow.GetComponent<Image>().color = new Color(0.474f, 0.918f, 1f, 1f);
                        break;
                    // case Rare.Rare:
                    //     avaBox.sprite = AssetLoader.Instance.RarrityBox[2];
                    //     Glow.GetComponent<Image>().color = new Color(0f, 0.698f, 0.443f, 1f);
                    //     BackGlow.GetComponent<Image>().color = new Color(1f, 0.953f, 0f, 1f);
                    //     break;
                    case "SR":
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[3];
                        Glow.GetComponent<Image>().color = new Color(0.886f, 0.58f, 0.173f, 1f);
                        BackGlow.GetComponent<Image>().color = new Color(1f, 0.313f, 0f, 1f);
                        break;
                    case "SSR":
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[4];
                        Glow.GetComponent<Image>().color = new Color(0.737f, 0.267f, 0.773f, 1f);
                        BackGlow.GetComponent<Image>().color = new Color(0.929f, 0.459f, 1f, 1f);
                        break;
                }
                 
            }

            //Role.sprite = AssetLoader.Instance.AttackSprite[infoWaifu.Class];
            
            // if (attackType != null)
            // {
            //     switch (character.CharacterType)
            //     {
            //         case CharacterType.Melee:
            //             //attackType.sprite = AssetLoader.Instance.AttackSprite[0];
            //             break;
            //         case CharacterType.Ranged:
            //             //attackType.sprite = AssetLoader.Instance.AttackSprite[1];
            //             break;
            //     }
            // }
           
            //nameTxt.text = infoWaifu.Name.ToString();
            levelTxt.text = "" + waifu.level;
            for(int i = 0; i < waifu.Star; i++)
            {
                stars[i].SetActive(true);
                // if (i < waifu.Ascend)
                // {
                //     stars[i].GetComponent<Image>().color = Color.red;
                // }
            }

            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    WaifuController.instance.ShowWaifuInfoPopup(waifu);
                    HUDController.instanse.UpdateTopPanel(Energe:false,Gold:true,Gem:true);
                });
            }

            //inDeck.SetActive(character.isInDeck);

        }
        
        

    }
}

