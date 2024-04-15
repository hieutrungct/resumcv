
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.Waifu;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.ListWaifu
{
    public class CardWaifu : MonoBehaviour
    {
        public Image  avaBox, BackGlow, Glow, Role;
        public SkeletonGraphic UI_Waifu;
        public TextMeshProUGUI nameTxt, levelTxt;
        [SerializeField] GameObject[] stars;
        public PlayerOwnsWaifu _waifu;
        public void SetUp(PlayerOwnsWaifu waifu)
        {
            _waifu = waifu;
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.ID);
            SkeletonDataAsset skeletonDataAsset = WaifuAssets.instance.GetWaifuSOByID(_waifu.ID.ToString()).SkeletonDataAsset;
            UI_Waifu.skeletonDataAsset = skeletonDataAsset;
            UI_Waifu.initialSkinName = "Pet" + infoWaifu.Code;
            UI_Waifu.startingAnimation = UI_Waifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;

            UI_Waifu.Initialize(true);
            if (avaBox != null)
            {
                switch (infoWaifu.Rare)
                {
                    case Rare.R:
                        SetRarityColors(1, Config.color_Rare_R, Config.color_BackGlow_Rare_R);
                        break;
                    case Rare.SR:
                        SetRarityColors(2, Config.color_Rare_SR, Config.color_BackGlow_Rare_SR);
                        break;
                    case Rare.SSR:
                        SetRarityColors(3, Config.color_Rare_SSR, Config.color_BackGlow_Rare_SSR);
                        break;
                    case Rare.UR:
                        SetRarityColors(4, Config.color_Rare_UR, Config.color_BackGlow_Rare_UR);
                        break;
                }
            }
            nameTxt.text = infoWaifu.Name.ToString();
            levelTxt.text = waifu.level.ToString();
            for (int i = 0; i < waifu.Star; i++)
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
                    
                });
            }
        }
        private void SetRarityColors(int rarityIndex, string glowColor, string backGlowColor)
        {
            avaBox.sprite = AssetLoader.Instance.RarrityBox[rarityIndex];
            Config.SetColorFromHex(Glow.GetComponent<Image>(), glowColor);
            Config.SetColorFromHex(BackGlow.GetComponent<Image>(), backGlowColor);
        }
    }
}

