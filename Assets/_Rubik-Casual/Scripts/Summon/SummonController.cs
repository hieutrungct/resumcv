using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.FlipCard2;
using UnityEngine;
using UnityEngine.UI;

public class SummonController : MonoBehaviour
{
    // public List<Sprite> imageSummon,Icon, imageBtn, imageWaifu, imageWaifuChibi;
    public GameObject GaCharCard;
    public Image iconWaifu, imageWaifu, iconTeckit_1, iconTeckit_10;
    public List<SummonSlot> lsBtnSummon;
    public void OnclickButton(int id)
    {
        GaCharCard.SetActive(true);
        GaCharCard.GetComponent<FlipCardController>().Id = id;
    }
    public void OnClickSummon(int id)
    {
        GaCharCard.GetComponent<FlipCardController>().idSummon = id;
        SetUpSummon(id);
    }
    public void SetUpSummon( int id)
    {
        imageWaifu.sprite = AssetLoader.instance.imageWaifu[id];
        iconWaifu.sprite = lsBtnSummon[id].iconWaifu.sprite;
        
        // iconWaifu.sprite = AssetLoader.instance.GetAvatarById()
    }
    // public static string GetNameImageWaifu(SummonSlot summonSlot)
    // {
    //     if (summonSlot != null)
    //     {
    //         string[] lsName = summonSlot.gameObject..Split("_");
    //         string NamePNG;
    //         if (lsName.Length == 4 && skeletonGraphic.initialSkinName != (lsName[0] + "_" + lsName[1]))
    //         {
    //             NamePNG = lsName[0] + "_" + lsName[1].Replace("0", "");
    //         }
    //         else
    //         {
    //             NamePNG = skeletonGraphic.initialSkinName;
    //         }
    //         return NamePNG.Replace("Pet", "");
    //     }
    //     else
    //     {
    //         return "1009_A";
    //     }
    // }
    
}
