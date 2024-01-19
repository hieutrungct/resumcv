using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RubikCasual.FlipCard2
{
    public class CardInfoDragPosition : MonoBehaviour
    {
        void OnMouseEnter()
        {
            gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
        }
        void OnMouseExit()
        {
            gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        }
    }
}
