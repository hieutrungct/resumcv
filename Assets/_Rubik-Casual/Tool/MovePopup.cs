using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RubikCasual.Tool
{
    public class MovePopup : MonoBehaviour
    {
        static float durations = 0.5f;

        public static void transPopupHorizontal(GameObject gbTaget, GameObject gbPopupOpen)
        {
            float tagetPopupMoveX = -gbPopupOpen.transform.position.x;
            float popupOpenMoveX = gbTaget.transform.position.x;

            gbPopupOpen.transform.DOMoveX(popupOpenMoveX, durations);
            gbTaget.transform.DOMoveX(tagetPopupMoveX, durations);
        }
    }
}
