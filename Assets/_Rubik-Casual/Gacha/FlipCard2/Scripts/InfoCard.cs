using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Battle;
using RubikCasual.Data;
using RubikCasual.Data.Waifu;
using RubikCasual.Waifu;
using Spine.Unity;
using Spine.Unity.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.FlipCard2
{
    public class InfoCard : MonoBehaviour
    {
        public TextMeshProUGUI txtNameWaifu, txtRare, txtValueFrag, txtClass, txtDame, txtDefense, txtAttack, txtHealh;
        public List<GameObject> lsGbStar, lsAttackRange;
        public Transform posWaifu;
        public SkeletonAnimation SkeWaifu;
        public SkeletonGraphic UI_Waifu;
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
            // SkeWaifu = DataController.instance.characterAssets.WaifuAssets.Get2D(infoWaifuAsset.ID.ToString());
            // SkeWaifu.Skeleton.ScaleX = 2.5f;
            // SkeWaifu.Skeleton.ScaleY = 2.5f;
            
            // SkeWaifu.transform.SetParent(posWaifu);
            // SkeWaifu.transform.position = posWaifu.position;
            // SkeWaifu.loop = true;
            // SkeWaifu.AnimationName = NameAnim.Anim_Character_Idle;
            // SkeWaifu.GetComponent<MeshRenderer>().sortingLayerName = "ShowPopup";
            // SkeWaifu.GetComponent<MeshRenderer>().sortingOrder = 10;

            // SkeWaifu.gameObject.transform.localScale = SkeWaifu.gameObject.transform.localScale * 2 / 3f;

            SkeletonDataAsset skeletonDataAsset = WaifuAssets.instance.GetWaifuSOByID(infoWaifuAsset.ID.ToString()).SkeletonDataAsset;
            UI_Waifu.skeletonDataAsset = skeletonDataAsset;

            if(infoWaifuAsset.ID == 66)
            {
                UI_Waifu.initialSkinName = UI_Waifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[0].Name;
            }
            else
            {
                UI_Waifu.initialSkinName = UI_Waifu.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            }

            UI_Waifu.startingAnimation = UI_Waifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;

            
            SpineEditorUtilities.ReinitializeComponent(UI_Waifu);
        }
        public void matrix()
        {
            int[,] matrix = new int[5, 5];
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    
                    matrix[i, j] = count;
                    count++; 
                }
            }
        }
        public void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    
                    Debug.Log(matrix[i, j]); 
                }
            }
        }
    }

}