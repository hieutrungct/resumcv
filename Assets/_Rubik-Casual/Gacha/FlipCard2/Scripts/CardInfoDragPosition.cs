using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using Rubik_Casual;
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
            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    ShowInfoCard();
                });
            }

        }
        void OnMouseExit()
        {
            gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            MoveImageBackGround(-ValuePosImageBackGround);
        }
        // void OnMouseDown()
        // {
        //     ShowInfoCard();
        // }
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

            // if (idSlot == 3 || idSlot == 8 || idSlot == 4 || idSlot == 9 || idSlot == 7)
            // {
            //     FlipCardController.instance.gbInfoCardWithMouse.transform.position = this.gameObject.transform.position - new Vector3(2f, 0, 0);
            // }
            // else
            // {
            //     FlipCardController.instance.gbInfoCardWithMouse.transform.position = this.gameObject.transform.position + new Vector3(2f, 0, 0);
            // }

            //gameObject.SetActive(true);  
            FlipCardController.instance.popup.SetActive(true);         
            // if (idSlot == 0 || idSlot == 1 || idSlot == 2 || idSlot == 3 || idSlot == 4)
            // {
            //     FlipCardController.instance.gbInfoCardWithMouse.transform.position = this.gameObject.transform.position - new Vector3(0, 2f, 0);
            // }
            // else
            // {
            //     FlipCardController.instance.gbInfoCardWithMouse.transform.position = this.gameObject.transform.position + new Vector3(0, 2f, 0);
            // }

            // InfoCard infoCard = FlipCardController.instance.gbInfoCardWithMouse.GetComponent<InfoCard>();
            //LoadDataCard(infoCard);
            Debug.Log("Đã Bấm");
            if (InfoCard.instance.SkeWaifu != null)
            {
                Destroy(InfoCard.instance.SkeWaifu.gameObject);
            }
            InfoCard.instance.LoadSpineCard(infoWaifuAsset);
            InfoCard.instance.LoadDataCard(infoWaifuAsset);
            InfoCard.instance.ShowStarCard(infoWaifuAsset);

        }
        
    }
}
