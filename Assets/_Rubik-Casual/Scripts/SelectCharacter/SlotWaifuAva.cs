using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.EnemyData;
using RubikCasual.Lobby;
using RubikCasual.Waifu;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.Select
{
    public class SlotWaifuAva : MonoBehaviour
    {
        public SkeletonGraphic avaWaifu;
        private PlayerOwnsWaifu _waifu;
        public Image AvaWaifu, classWaifu,rareWaifu;
        public GameObject iconSelect;
        

        public void SetUpItemAvaWaifu(PlayerOwnsWaifu waifu)
        {
            _waifu = waifu;
            
            if (WaifuSelectController.instance == null)
            {
                Debug.LogError("WaifuSelectController.instance is not assigned.");
                return;
            }
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.ID);
            SkeletonDataAsset skeletonDataAsset = WaifuAssets.instance.GetWaifuSOByID(_waifu.ID.ToString()).SkeletonDataAsset;
            avaWaifu.skeletonDataAsset = skeletonDataAsset;
            // if(waifu.ID == 66)
            // {
            //     avaWaifu.initialSkinName = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[0].Name;
            // }
            // else
            // {
            //     avaWaifu.initialSkinName = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            // }
            avaWaifu.initialSkinName = "Pet" + infoWaifu.Code;
            avaWaifu.startingAnimation = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;
            avaWaifu.Initialize(true);
            
            // AvaWaifu.sprite = AssetLoader.Instance.GetAvatarById(MovePopup.GetNameImageWaifu(avaWaifu));
            AvaWaifu.sprite  = AssetLoader.Instance.GetAvatarByIndex(DataController.instance.characterAssets.GetIndexWaifu(infoWaifu.ID));

            AvaWaifu.preserveAspect = true;
            //classWaifu.sprite = AssetLoader.instance.
            iconSelect.SetActive(false);
            for (int i = 0; i < DataController.instance.userData.curentTeams.Count; i++)
            {
                if(waifu.ID == DataController.instance.userData.curentTeams[i].ID)
                {
                    //Debug.Log("Waifu thứ " + waifu.ID);
                    iconSelect.SetActive(true);
                    WaifuSelectController.instance.lsSlotWaifuSelectUI[i].SetUp(_waifu);
                    WaifuSelectController.instance.lsSlotWaifuSelectUI[i].avaBox_Obj.SetActive(true);
                    WaifuSelectController.instance.lsSlotWaifuSelectUI[i].slotWaifuAva = this;
                }
                
            }
            if (rareWaifu != null)
            {
                switch (infoWaifu.Rare)
                {
                    case RubikCasual.Waifu.Rare.R:
                        rareWaifu.sprite = AssetLoader.Instance.RarrityAvaBox[1];
                        break;
                    case RubikCasual.Waifu.Rare.SR:
                        rareWaifu.sprite = AssetLoader.Instance.RarrityAvaBox[2];
                        break;
                    case RubikCasual.Waifu.Rare.SSR:
                        rareWaifu.sprite = AssetLoader.Instance.RarrityAvaBox[3];
                        break;
                    case RubikCasual.Waifu.Rare.UR:
                        rareWaifu.sprite = AssetLoader.Instance.RarrityAvaBox[4];
                        break;
                }
            }
            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    SelectOnClick(_waifu);
                });
            }
            

        }
        // Xử lý phần Select
        public void SelectOnClick(PlayerOwnsWaifu _waifu)
        {
            int i; 
            bool isCurrentlySelected = false;
            for(i = 0; i < DataController.instance.userData.curentTeams.Count; i++)
            {
                if(DataController.instance.userData.curentTeams[i].ID == _waifu.ID)
                {
                    isCurrentlySelected = true;
                    break;
                }
            }
            if(isCurrentlySelected)
            {
                //Debug.Log("Nó sẽ nhảy vào i thứ: "+ i);
                DataController.instance.userData.curentTeams[i].ID = 0;
                WaifuSelectController.instance.lsSlotWaifuSelectUI[i].avaBox_Obj.SetActive(false);
                iconSelect.SetActive(false);
            }
            else
            {
                for(i = 0; i < DataController.instance.userData.curentTeams.Count; i++)
                {
                    if(DataController.instance.userData.curentTeams[i].ID == 0)
                    {
                        DataController.instance.userData.curentTeams[i].ID = _waifu.ID;

                        WaifuSelectController.instance.lsSlotWaifuSelectUI[i].SetUp(_waifu);
                        WaifuSelectController.instance.lsSlotWaifuSelectUI[i].avaBox_Obj.SetActive(true);
                        WaifuSelectController.instance.lsSlotWaifuSelectUI[i].slotWaifuAva = this;
                        iconSelect.SetActive(true);
                        isCurrentlySelected = true;
                        break;
                    }
                    
                }
                Debug.Log("Không còn ô trống");
            }
        }
    }
}

