using System.Collections;
using System.Collections.Generic;
using Rubik.Select;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.Waifu;
using Spine.Unity;
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
        public SlotWaifuAva slotWaifuAva;
        public PlayerOwnsWaifu thisWaifu;

        public void SetUp(PlayerOwnsWaifu waifu)
        {
            thisWaifu = waifu;
            
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.ID);

           

            SkeletonDataAsset skeletonDataAsset = WaifuAssets.instance.GetWaifuSOByID(waifu.ID.ToString()).SkeletonDataAsset;
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
            
            // avaBox.sprite = AssetLoader.Instance.GetAvatarById(MovePopup.GetNameImageWaifu(avaWaifu));
            // avaBox.preserveAspect = true;

            // Debug.Log(infoWaifu.Code.ToString());

            //role.sprite = AssetLoader.Instance.AttackSprite[waifu.Role];
            lvlTxt.text = "" + waifu.level;
            nameTxt.text = infoWaifu.Name;
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
                    case Rare.R:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[1];
                        rarity.sprite = AssetLoader.Instance.LabelRare[0];
                        rareTxt.text = Rare.R.ToString();
                        break;
                    case Rare.SR:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[2];
                        rarity.sprite = AssetLoader.Instance.LabelRare[1];
                        rareTxt.text = Rare.SR.ToString();
                        break;
                    case Rare.SSR:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[3];
                        rarity.sprite = AssetLoader.Instance.LabelRare[2];
                        rareTxt.text = Rare.SSR.ToString();
                        break;
                    case Rare.UR:
                        avaBox.sprite = AssetLoader.Instance.RarrityBox[4];
                        rarity.sprite = AssetLoader.Instance.LabelRare[3];
                        rareTxt.text = Rare.UR.ToString();
                        break;
                }
            }
            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    //SelectOnClick();
                    OutSelectOnClick();
                });
            }

        }
        public void OutSelectOnClick()
        {
            int temp = WaifuSelectController.instance.CheckIndexOfWaifu(thisWaifu);
            thisWaifu = WaifuSelectController.instance.GetWaifu(temp);
            // Debug.Log("Số index của ava slot là: " + temp);
            
            // slotWaifuAva.iconSelect.SetActive(false);
            // avaBox_Obj.SetActive(false);
            // slotWaifuAva = null;

            int i; 
            bool isCurrentlySelected = false;
            for(i = 0; i < DataController.instance.userData.curentTeams.Count; i++)
            {
                if(DataController.instance.userData.curentTeams[i].ID == thisWaifu.ID)
                {
                    isCurrentlySelected = true;
                    break;
                }
            }
            if(isCurrentlySelected)
            {
                //Debug.Log("Nó sẽ nhảy vào i thứ: "+ i);
                DataController.instance.userData.curentTeams[i].ID = 0;
                slotWaifuAva.iconSelect.SetActive(false);
                avaBox_Obj.SetActive(false);
                slotWaifuAva = null;
            }
            
        }
    }
}