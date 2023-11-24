using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RubikCasual.Tool
{
    public class MovePopup : MonoBehaviour
    {
        static float durations = 0.5f;
        private static MovePopup instance;
        void Start()
        {
            instance = this;
        }
        public static void TransPopupHorizontal(GameObject gbTaget, GameObject gbPopupOpen)
        {
            gbPopupOpen.SetActive(true);
            float tagetPopupMoveX = -gbPopupOpen.transform.position.x;
            float popupOpenMoveX = gbTaget.transform.position.x;

            gbPopupOpen.transform.DOMoveX(popupOpenMoveX, durations);
            gbTaget.transform.DOMoveX(tagetPopupMoveX, durations);
            instance.StartCoroutine(DeactivateAfterDelay(gbTaget, durations));
        }
        static IEnumerator DeactivateAfterDelay(GameObject gbTagetclone, float delay)
        {
            yield return new WaitForSeconds(delay);
            gbTagetclone.SetActive(false);
        }
    }
}
