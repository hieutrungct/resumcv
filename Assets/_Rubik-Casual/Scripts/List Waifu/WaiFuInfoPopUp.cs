using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Spine.Unity;
using Spine.Unity.Editor;
using RubikCasual.Data.Player;
using Rubik_Casual;
using RubikCasual.Waifu;
using RubikCasual.Data;
using RubikCasual.Data.Waifu;
using RubikCasual.Tool;
namespace Rubik.ListWaifu
{
    public class WaiFuInfoPopUp : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic avaWaifu;

        [SerializeField]
        private TextMeshProUGUI lvTxt, lvProcessTxt, damageTxt, defenseTxt, critTxt, healthTxt, moveSpeedTxt;

        public Button btn_Arrow_r, btn_Arrow_l;
        
        

        public Image role, avatar;
        
        
        private PlayerOwnsWaifu thisWaifu,_waifu;

        public void Start()
        {
            
        }
        public WaifuAssets waifuAssets;

        public void SetUp(PlayerOwnsWaifu waifu)
        {
            thisWaifu = waifu;
            _waifu = waifu;
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.Index);

            if (infoWaifu.Code == waifuAssets.GetWaifuSOByIndex(waifu.Index.ToString()).Code && infoWaifu.Rare == "R")
            {
                _waifu.Index = waifuAssets.GetWaifuSOByIndex(waifu.Index.ToString()).Index;
            }
            else if (infoWaifu.Code == waifuAssets.GetWaifuSOByIndex(waifu.Index.ToString()).Code && infoWaifu.Rare == "SR")
            {
                _waifu.Index = waifuAssets.GetWaifuSOByIndex(waifu.Index.ToString()).Index;
            }
            SkeletonDataAsset skeletonDataAsset = waifuAssets.GetWaifuSOByIndex(_waifu.Index.ToString()).SkeletonDataAsset;
            avaWaifu.skeletonDataAsset = skeletonDataAsset;
            avaWaifu.initialSkinName = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            avaWaifu.startingAnimation = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;
            SpineEditorUtilities.ReinitializeComponent(avaWaifu);
            
            avatar.sprite = AssetLoader.Instance.GetAvatarById(MovePopup.GetNameImageWaifu(avaWaifu));

            // Debug.Log(infoWaifu.Code.ToString());

            //role.sprite = AssetLoader.Instance.AttackSprite[waifu.Role];
            lvTxt.text = waifu.level.ToString();
            lvProcessTxt.text = waifu.Exp + "/" + waifu.Exp;
            damageTxt.text = infoWaifu.ATK.ToString();
            defenseTxt.text = infoWaifu.DEF.ToString();
            // critTxt.text = infoWaifu.Critical.ToString();
            healthTxt.text = infoWaifu.HP.ToString();
            // moveSpeedTxt.text = waifu.MoveSpeed.ToString();
        }

        public void Next()
        {
            int temp = WaifuController.instance.CheckIndexOfWaifu(thisWaifu);
            thisWaifu = WaifuController.instance.GetWaifu(temp - 1);
            SetUp(thisWaifu);
            
        }
        public void Back()
        {
            int temp = WaifuController.instance.CheckIndexOfWaifu(thisWaifu);
            thisWaifu = WaifuController.instance.GetWaifu(temp + 1);
            SetUp(thisWaifu);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            HUDController.instanse.UpdateTopPanel(Energe:false,Gold:false,Gem:false);
        }
        

        public void ShowCharaterfoPopup(PlayerOwnsWaifu waifu)
        {
            gameObject.SetActive(true);
            SetUp(waifu);
        }
    }
}

