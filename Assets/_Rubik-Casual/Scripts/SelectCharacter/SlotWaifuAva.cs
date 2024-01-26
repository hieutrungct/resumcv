using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.EnemyData;
using RubikCasual.Waifu;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.Select
{
    public class SlotWaifuAva : MonoBehaviour
    {
        private SkeletonGraphic avaWaifu;
        private PlayerOwnsWaifu _waifu;
        public Image AvaWaifu, classWaifu,rareWaifu;
        public void SetUpItemAvaWaifu(PlayerOwnsWaifu waifu)
        {
            _waifu = waifu;
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.ID);
            //classWaifu.sprite = AssetLoader.instance.
            if (rareWaifu != null)
            {
                switch (infoWaifu.Rare)
                {
                    case RubikCasual.Waifu.Rare.R:
                        rareWaifu.sprite = AssetLoader.Instance.RarrityBox[0];
                        break;
                    case RubikCasual.Waifu.Rare.SR:
                        rareWaifu.sprite = AssetLoader.Instance.RarrityBox[1];
                        break;
                    case RubikCasual.Waifu.Rare.SSR:
                        rareWaifu.sprite = AssetLoader.Instance.RarrityBox[2];
                        break;
                    case RubikCasual.Waifu.Rare.UR:
                        rareWaifu.sprite = AssetLoader.Instance.RarrityBox[3];
                        break;
                }
            }

        }
    }
}

