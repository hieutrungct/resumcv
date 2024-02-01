using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using RubikCasual.Battle;
using RubikCasual.Data;
using RubikCasual.Waifu;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.FlipCard2
{
    public class CardInfoDragPosition : MonoBehaviour
    {
        public int idSlot;
        public float ValuePosImageBackGround, ValueMoveImageBackGround;
        public InfoWaifuAsset infoWaifuAsset;
        public int frag;
        GameObject imageBackGround;
        Vector3 posOriginImageBackGround;

        void Start()
        {
            imageBackGround = FlipCardController.instance.imageBackGround;
            posOriginImageBackGround = imageBackGround.transform.position;
        }
        void OnMouseEnter()
        {
            imageBackGround.transform.position = posOriginImageBackGround;
            gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
            MoveImageBackGround(ValuePosImageBackGround);

        }
        void OnMouseExit()
        {
            gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            MoveImageBackGround(-ValuePosImageBackGround);
        }
        void OnMouseDown()
        {
            ShowInfoCard();
        }
        public void SetValueMoveBackGround(int idSlot)
        {
            ValueMoveImageBackGround = 0.1f;
            if (idSlot == 0 || idSlot == 5)
            {
                ValuePosImageBackGround = ValueMoveImageBackGround * 2;
            }
            else if (idSlot == 1 || idSlot == 6)
            {
                ValuePosImageBackGround = ValueMoveImageBackGround;
            }
            else if (idSlot == 3 || idSlot == 8)
            {
                ValuePosImageBackGround = -ValueMoveImageBackGround;
            }
            else if (idSlot == 4 || idSlot == 9)
            {
                ValuePosImageBackGround = -(ValueMoveImageBackGround * 2);
            }
            else
            {
                ValuePosImageBackGround = posOriginImageBackGround.x;
            }
        }
        Tween MoveImageBackGround(float value)
        {
            return imageBackGround.transform.DOMoveX(imageBackGround.transform.position.x + value, 0.5f);
        }
        void ShowInfoCard()
        {
            // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            if (idSlot == 3 || idSlot == 8 || idSlot == 4 || idSlot == 9 || idSlot == 7)
            {
                FlipCardController.instance.gbInfoCardWithMouse.transform.position = this.gameObject.transform.position - new Vector3(2f, 0, 0);
            }
            else
            {
                FlipCardController.instance.gbInfoCardWithMouse.transform.position = this.gameObject.transform.position + new Vector3(2f, 0, 0);
            }
            InfoCard infoCard = FlipCardController.instance.gbInfoCardWithMouse.GetComponent<InfoCard>();

            if (infoCard.SkeWaifu != null)
            {
                Destroy(infoCard.SkeWaifu.gameObject);
            }
            infoCard.txtRare.text = this.infoWaifuAsset.Rare.ToString();
            infoCard.SkeWaifu = DataController.instance.characterAssets.WaifuAssets.Get2D(this.infoWaifuAsset.ID.ToString());
            infoCard.SkeWaifu.transform.SetParent(infoCard.posWaifu);
            infoCard.SkeWaifu.transform.position = infoCard.posWaifu.position;
            infoCard.SkeWaifu.loop = true;
            infoCard.SkeWaifu.AnimationName = NameAnim.Anim_Character_Idle;
            infoCard.SkeWaifu.GetComponent<MeshRenderer>().sortingLayerName = "ShowPopup";
            infoCard.SkeWaifu.GetComponent<MeshRenderer>().sortingOrder = 10;

            infoCard.SkeWaifu.gameObject.transform.localScale = infoCard.SkeWaifu.gameObject.transform.localScale * 2 / 3f;


            infoCard.txtNameWaifu.text = this.infoWaifuAsset.Name;
            infoCard.txtValueFrag.text = this.frag.ToString();

            for (int i = 0; i < 5; i++)
            {
                if (this.infoWaifuAsset.Star > i)
                {
                    infoCard.lsGbStar[i].SetActive(true);
                }
                else
                {
                    infoCard.lsGbStar[i].SetActive(false);
                }
            }


        }
    }
}
