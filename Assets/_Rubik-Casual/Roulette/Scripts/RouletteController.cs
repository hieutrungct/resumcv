using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Roulette
{
    public class RouletteController : MonoBehaviour
    {
        public List<Image> lsSlotIcon = new List<Image>();
        public List<TextMeshProUGUI> lsTxtValueItem = new List<TextMeshProUGUI>();
        public GameObject gbSpin, gbArrowFocus;
        public Button btnSpin;
        public float rotationSpeed = 0.5f;
        public void RotateSpin()
        {
            float Number = Random.Range(360, 720), randomElapsedTime = Random.Range(5, 10);
            Vector3 value = new Vector3(0f, 0f, Number);

            Tween doTarget = gbSpin.transform.DOLocalRotate(gbSpin.transform.eulerAngles + value, rotationSpeed, RotateMode.FastBeyond360)
                .SetLoops((int)randomElapsedTime, LoopType.Incremental)
                .SetEase(EaseFactory.StopMotion(120, Ease.Linear));
            // doTarget.;
            
            Debug.Log(randomElapsedTime);
            Debug.Log(doTarget.Duration());
            // doTarget.OnComplete(() =>
            // {
            //     // Kiểm tra xem có phải vòng lặp cuối cùng không
            //     if (doTarget.WaitForElapsedLoops() == randomElapsedTime - 2)
            //     {

            //         doTarget.SetEase(EaseFactory.StopMotion(60, Ease.InOutQuad));
            //     }
            // });
            // StartCoroutine(StopSpin(doTarget, (int)randomElapsedTime, (int)Number));

        }
        // IEnumerator StopSpin(Tween doTarget, int NumberLoops, int number)
        // {
        //     Debug.Log(number);
        //     yield return doTarget.WaitForElapsedLoops(NumberLoops - 1);

        //     doTarget.OnUpdate
        // }

    }
}
