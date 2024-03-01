using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Battle;
using RubikCasual.Data;
using RubikCasual.Waifu;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.FlipCard2
{
    public class InfoCard : MonoBehaviour
    {
        public TextMeshProUGUI txtNameWaifu, txtRare, txtValueFrag, txtClass, txtDame, txtDefense, txtAttack, txtHealh;
        public List<GameObject> lsGbStar;
        public Transform posWaifu;
        public SkeletonAnimation SkeWaifu;
        public Image imgRare;
        public List<GameObject> SkillWaifu;
        // public InfoWaifuAsset infoWaifuAsset;
        public int frag;
        public static InfoCard instance;
        void Awake()
        {
            instance = this;
        }
        public void LoadDataCard(InfoWaifuAsset infoWaifuAsset)
        {
            txtRare.text = infoWaifuAsset.Rare.ToString();
            

            txtNameWaifu.text = infoWaifuAsset.Name;
            txtClass.text = infoWaifuAsset.ClassWaifu.ToString();
            txtDame.text = infoWaifuAsset.Pow.ToString();
            txtDefense.text = infoWaifuAsset.DEF.ToString();
            txtAttack.text = infoWaifuAsset.ATK.ToString();
            txtHealh.text = infoWaifuAsset.HP.ToString();
            imgRare.sprite = AssetLoader.instance.LabelRare[(int)(infoWaifuAsset.Rare)];
            txtValueFrag.text = this.frag.ToString();
        }
        public void ShowStarCard(InfoWaifuAsset infoWaifuAsset)
        {
            for (int i = 0; i < 5; i++)
            {
                if (infoWaifuAsset.Star >= i)
                {
                    lsGbStar[i].SetActive(true);
                }
                else
                {
                    lsGbStar[i].SetActive(false);
                }
            }
        }
        public void LoadSpineCard(InfoWaifuAsset infoWaifuAsset)
        {
            SkeWaifu = DataController.instance.characterAssets.WaifuAssets.Get2D(infoWaifuAsset.ID.ToString());
            SkeWaifu.Skeleton.ScaleX = 2.5f;
            SkeWaifu.Skeleton.ScaleY = 2.5f;
            
            SkeWaifu.transform.SetParent(posWaifu);
            SkeWaifu.transform.position = posWaifu.position;
            SkeWaifu.loop = true;
            SkeWaifu.AnimationName = NameAnim.Anim_Character_Idle;
            SkeWaifu.GetComponent<MeshRenderer>().sortingLayerName = "ShowPopup";
            SkeWaifu.GetComponent<MeshRenderer>().sortingOrder = 10;

            SkeWaifu.gameObject.transform.localScale = SkeWaifu.gameObject.transform.localScale * 2 / 3f;
        }
    }

}