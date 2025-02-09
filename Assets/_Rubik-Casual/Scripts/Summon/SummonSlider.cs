using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Rubik_Casual;
using Rubik_Casual.Summon;
using RubikCasual.Data;
using RubikCasual.Waifu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.SummonSliders
{
    public class SummonSlider : MonoBehaviour
    {
        public int indexSummon, idWaifu;
        public Image btnImg, avaWaifu;
        public TextMeshProUGUI nametxt;
        public SummonController summonController;
        void Start()
        {
            SetUpSlider();
        }
        
        
        public void SetUpSlider()
        {
            
            SetUpIdWaifuByIndexSummon(indexSummon);
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(idWaifu);
            // Debug.Log("id chuyển vào"+infoWaifu.ID);
            nametxt.text = infoWaifu.Name;
            avaWaifu.sprite = AssetLoader.Instance.GetAvatarByIndex(DataController.instance.characterAssets.GetIndexWaifu(infoWaifu.ID));
            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    summonController.OnClickScrollSummon(infoWaifu, indexSummon);
                });
            }
        }
        public void SetUpIdWaifuByIndexSummon(int index)
        {
            switch (index)
            {
                case 0:
                    idWaifu = (int)SummonKey.idOnSlot_0;
                    btnImg.sprite  = AssetLoader.Instance.Button[9];
                    
                    break;
                case 1:
                    idWaifu = (int)SummonKey.idOnSlot_1;
                    btnImg.sprite  = AssetLoader.Instance.Button[6];
                    break;
                case 2:
                    idWaifu = (int)SummonKey.idOnSlot_2;
                    btnImg.sprite  = AssetLoader.Instance.Button[5];

                    break;
                case 3:
                    idWaifu = (int)SummonKey.idOnSlot_3;
                    btnImg.sprite  = AssetLoader.Instance.Button[4];
                    break;
                case 4:
                    idWaifu = (int)SummonKey.idOnSlot_4;
                    btnImg.sprite  = AssetLoader.Instance.Button[7];
                    break;
                case 5:
                    idWaifu = (int)SummonKey.idOnSlot_5;
                    btnImg.sprite  = AssetLoader.Instance.Button[2];
                    break;
                case 6:
                    idWaifu = (int)SummonKey.idOnSlot_6;
                    btnImg.sprite  = AssetLoader.Instance.Button[3];
                    break;
                default:
                    Debug.Log("Không có id theo index chuyền vào!");
                    break;
            }
        }
        
    }
}

