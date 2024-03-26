using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using RubikCasual.Waifu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonSlot : MonoBehaviour
{
    public SummonKey key;
    
    public Image iconWaifu;
    public TextMeshProUGUI nameWaifuTxt;
    void Awake()
    {
        SetUpButton();
    }
    public void SetUpButton()
    {
        InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex((int)key);
        nameWaifuTxt.text = infoWaifu.Name;
    }
}
