using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using RubikCasual.FlipCard2;
using RubikCasual.Waifu;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik_Casual.Summon
{
    public class SummonController : MonoBehaviour
    {
        // public List<Sprite> imageSummon,Icon, imageBtn, imageWaifu, imageWaifuChibi;
        public FlipCardController GaCharCard;
        public Image iconWaifu, imageWaifu, iconTeckit_1, iconTeckit_10;
        public List<SummonSlot> lsBtnSummon;

    
        public void OnClickActiveSummon()
        {
            gameObject.SetActive(true);
            HUDController.instanse.UpdateTopPanel(Energe:false,Gold:true,Gem:true,Ticket: true);

        }
        public void OnClickHideSummon()
        {
            gameObject.SetActive(false);
            HUDController.instanse.UpdateTopPanel(Energe:true,Gold:true,Gem:true,Ticket: false);

        }
        public void OnclickButton(int id)
        {
            GaCharCard.gameObject.SetActive(true);
            GaCharCard.Id = id;
        }
        public void OnClickSummon(int id)
        {
            GaCharCard.idSummon = ((int)lsBtnSummon[id].key);
            GaCharCard.isClick = false;
            SetUpSummon(id);
        }
        public void SetUpSummon( int id)
        {
            imageWaifu.sprite = AssetLoader.instance.imageWaifu[id];
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(((int)lsBtnSummon[id].key));
            // iconWaifu.sprite = lsBtnSummon[id].iconWaifu.sprite;
            
            // iconWaifu.sprite = AssetLoader.instance.GetAvatarById(infoWaifu.Code);
            iconWaifu.sprite = AssetLoader.Instance.GetAvatarByIndex(DataController.instance.characterAssets.GetIndexWaifu(infoWaifu.ID));
        }
        string GetNameImageWaifu(SummonSlot summonSlot)
        {
            if (summonSlot != null)
            {
                string[] lsName = summonSlot.iconWaifu.sprite.name.Split("_");
                string NamePNG;
                if (lsName.Length == 4 && summonSlot.iconWaifu.sprite.name != (lsName[0] + "_" + lsName[1]))
                {
                    NamePNG = lsName[0] + "_" + lsName[1].Replace("0", "");
                }
                else
                {
                    NamePNG = summonSlot.iconWaifu.sprite.name;
                }
                return NamePNG.Replace("Pet", "");
            }
            else
            {
                return "1009_A";
            }
        }
        
    }
}

