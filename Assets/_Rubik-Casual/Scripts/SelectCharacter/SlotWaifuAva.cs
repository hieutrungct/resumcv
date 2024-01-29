using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.EnemyData;
using RubikCasual.Tool;
using RubikCasual.Waifu;
using Spine.Unity;
using Spine.Unity.Editor;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.Select
{
    public class SlotWaifuAva : MonoBehaviour
    {
        public SkeletonGraphic avaWaifu;
        private PlayerOwnsWaifu _waifu;
        public Image AvaWaifu, classWaifu,rareWaifu;
        public void SetUpItemAvaWaifu(PlayerOwnsWaifu waifu)
        {
            _waifu = waifu;
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.ID);
            SkeletonDataAsset skeletonDataAsset = WaifuAssets.instance.GetWaifuSOByID(_waifu.ID.ToString()).SkeletonDataAsset;
            avaWaifu.skeletonDataAsset = skeletonDataAsset;
            avaWaifu.initialSkinName = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            avaWaifu.startingAnimation = avaWaifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;
            SpineEditorUtilities.ReinitializeComponent(avaWaifu);
            
            AvaWaifu.sprite = AssetLoader.Instance.GetAvatarById(MovePopup.GetNameImageWaifu(avaWaifu));
            AvaWaifu.preserveAspect = true;
            //classWaifu.sprite = AssetLoader.instance.
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

        }
    }
}

