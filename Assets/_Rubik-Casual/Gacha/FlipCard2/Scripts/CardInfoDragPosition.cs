using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
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
    }
}
