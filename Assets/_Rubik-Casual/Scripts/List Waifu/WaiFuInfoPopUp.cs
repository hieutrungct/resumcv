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
        private TextMeshProUGUI lvTxt, lvProcessTxt, damageTxt, defenseTxt, critTxt, healthTxt, moveSpeedTxt, goldTxt, selectTxt;

        // public Button btn_Arrow_r, btn_Arrow_l, btn_Update_Waifu, btn_Select;


        public Image role, avatar, btnselect, btnUpdate;

        
        private PlayerOwnsWaifu thisWaifu,_waifu;

        public void Start()
        {
            
        }
        float curGoldUpdate = 1;

        public void SetUp(PlayerOwnsWaifu waifu)
        {
            thisWaifu = waifu;
            _waifu = waifu;
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.Index);

           

            SkeletonDataAsset skeletonDataAsset = WaifuAssets.instance.GetWaifuSOByIndex(_waifu.Index.ToString()).SkeletonDataAsset;
            avaWaifu.skeletonDataAsset = skeletonDataAsset;
            avaWaifu.initialSkinName = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            avaWaifu.startingAnimation = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;
            SpineEditorUtilities.ReinitializeComponent(avaWaifu);
            
            avatar.sprite = AssetLoader.Instance.GetAvatarById(MovePopup.GetNameImageWaifu(avaWaifu));
            avatar.preserveAspect = true;

            // Debug.Log(infoWaifu.Code.ToString());

            //role.sprite = AssetLoader.Instance.AttackSprite[waifu.Role];
            lvTxt.text = waifu.level.ToString();
            lvProcessTxt.text = waifu.Exp + "/" + waifu.Exp;
            damageTxt.text = (infoWaifu.ATK + waifu.ATK).ToString();
            defenseTxt.text = (infoWaifu.DEF + waifu.DEF).ToString();
            critTxt.text = (infoWaifu.Pow + waifu.Pow).ToString();
            healthTxt.text = (infoWaifu.HP + waifu.HP).ToString();

            selectTxt.text = "Select";
            btnselect.sprite = AssetLoader.instance.Button[9];
            foreach (var curentWaifu in DataController.instance.userData.CurentTeam)
            {
                if(waifu.Index == curentWaifu)
                {
                    //Debug.Log("Waifu thứ " + waifu.Index);
                    selectTxt.text = "Deselect";
                    btnselect.sprite = AssetLoader.instance.Button[6];
                    
                }
            }
            curGoldUpdate = (1000*waifu.level);
            goldTxt.text = curGoldUpdate.ToString();

            
            // moveSpeedTxt.text = waifu.MoveSpeed.ToString();

            //Update_Waifu(waifu);


        }

        public void Next()
        {
            int temp = WaifuController.instance.CheckIndexOfWaifu(thisWaifu);
            thisWaifu = WaifuController.instance.GetWaifu(temp - 1);
            SetUp(thisWaifu);
            
        }
        public void Update_Waifu()
        {
            DataController.instance.UpdateWaifu(thisWaifu,curGoldUpdate);
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
        
        public void SelectOnClick()
        {
            int i; 
            bool isCurrentlySelected = false;
            for(i = 0; i < DataController.instance.userData.CurentTeam.Count; i++)
            {
                if(DataController.instance.userData.CurentTeam[i] == thisWaifu.Index)
                {
                    isCurrentlySelected = true;
                    break;
                }
            }
            if(isCurrentlySelected)
            {
                //Debug.Log("Nó sẽ nhảy vào i thứ: "+ i);
                DataController.instance.userData.CurentTeam[i] = 0;
                selectTxt.text = "Select";
                btnselect.sprite = AssetLoader.instance.Button[9];
            }
            else
            {
                for(i = 0; i < DataController.instance.userData.CurentTeam.Count; i++)
                {
                    if(DataController.instance.userData.CurentTeam[i] == 0)
                    {
                        DataController.instance.userData.CurentTeam[i] = thisWaifu.Index;
                        selectTxt.text = "Deselect";
                        btnselect.sprite = AssetLoader.instance.Button[6];
                        break;
                    }
                    else
                    {
                        Debug.Log("Không còn ô trống");
                    }
                }
            }
        }
    }
}

