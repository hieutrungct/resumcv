using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.Tool;
using RubikCasual.Waifu;
using Spine.Unity;
using Spine.Unity.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Lobby
{
    public class SlotWaifuSelectUI : MonoBehaviour
    {
        public TextMeshProUGUI nameTxt, lvlTxt, expTxt, rareTxt;
        public List<GameObject> lsStar;
        public GameObject waittingSlot, slotCharacter,avaBox_Obj;
        public Image avaBox, classWaifu, rarity;
        public Slider expSlider;
        public SkeletonGraphic avaWaifu;
        public PlayerOwnsWaifu thisWaifu;

        public void SetUp(PlayerOwnsWaifu waifu)
        {
            thisWaifu = waifu;
            
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.ID);

           

            SkeletonDataAsset skeletonDataAsset = WaifuAssets.instance.GetWaifuSOByID(waifu.ID.ToString()).SkeletonDataAsset;
            avaWaifu.skeletonDataAsset = skeletonDataAsset;
            avaWaifu.initialSkinName = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            avaWaifu.startingAnimation = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;
            SpineEditorUtilities.ReinitializeComponent(avaWaifu);
            
            // avatar.sprite = AssetLoader.Instance.GetAvatarById(MovePopup.GetNameImageWaifu(avaWaifu));
            // avatar.preserveAspect = true;

            // Debug.Log(infoWaifu.Code.ToString());

            //role.sprite = AssetLoader.Instance.AttackSprite[waifu.Role];
            lvlTxt.text = "" + waifu.level;
            for(int i = 0; i < waifu.Star; i++)
            {
                lsStar[i].SetActive(true);
                // if (i < waifu.Ascend)
                // {
                //     stars[i].GetComponent<Image>().color = Color.red;
                // }
            }

            if (avaBox != null)
            {
                switch (infoWaifu.Rare)
                {
                    case RubikCasual.Waifu.Rare.R:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[1];
                        break;
                    case RubikCasual.Waifu.Rare.SR:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[2];
                        break;
                    case RubikCasual.Waifu.Rare.SSR:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[3];
                        break;
                    case RubikCasual.Waifu.Rare.UR:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[4];
                        break;
                }
            }

            
            
            
            // moveSpeedTxt.text = waifu.MoveSpeed.ToString();

            //Update_Waifu(waifu);


        }
    }
}