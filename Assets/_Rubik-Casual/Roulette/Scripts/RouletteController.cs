using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTPackage;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Roulette
{
    public class RouletteController : MonoBehaviour
    {
        public List<Image> lsSlotIcon = new List<Image>();
        public List<TextMeshProUGUI> lsTxtValueItem = new List<TextMeshProUGUI>();
        public NTDictionary<int, int> lsIdWithSlot = new NTDictionary<int, int>();
        public GameObject gbSpin, gbArrowFocus, gbCoverItem;
        // public TextMeshProUGUI txtNumberFocus;
        public Button btnSpin;
        public bool clockWise = true;
        public int slotTarget;
        public float durationsOneLoop, DegreeTarget, Loops, DistanceSpin;

        // [Button]
        // void TestConvert()
        // {
        //     DegreeTarget = ConvertSlotRotateToDegree(slotTarget, lsSlotIcon.Count);
        //     Debug.Log(DegreeTarget);
        // }

        int ConvertSlotRotateToDegree(int slotTarget, int totalSlot, bool isArrowFocusLine = true)
        {
            // if (!isArrowFocusLine)
            // {
            int DegreResult = 0;
            for (int i = 0; i < totalSlot; i++)
            {
                if (slotTarget == i)
                {
                    if (slotTarget != 0)
                    {
                        float valueDegreeMinCalculate = (360f / (float)totalSlot) * ((1f / 2f) + i - 1f) + 1f;
                        float valueDegreeMaxCalculate = (360f / (float)totalSlot) * ((1f / 2f) + i) - 1f;
                        DegreResult = (int)UnityEngine.Random.Range((int)valueDegreeMinCalculate, (int)valueDegreeMaxCalculate);
                    }
                    else
                    {
                        float valueDegreeMinCalculate = (360f / (float)totalSlot) * ((1f / 2f) + totalSlot - 1f) + 1f;
                        float valueDegreeMaxCalculate = (360f / (float)totalSlot) * ((1f / 2f) + i) - 1f;

                        if (UnityEngine.Random.Range(0, 1) == 0)
                        {
                            DegreResult = (int)UnityEngine.Random.Range(0, (int)valueDegreeMaxCalculate);
                        }
                        else
                        {
                            DegreResult = (int)UnityEngine.Random.Range((int)valueDegreeMinCalculate, 359);
                        }
                    }
                }
            }
            return DegreResult;
            // }
        }

        public void RotateSpin()
        {
            DegreeTarget = ConvertSlotRotateToDegree(slotTarget, lsSlotIcon.Count);
            gbSpin.transform.localRotation = new Quaternion();
            StartCoroutine(StopOrStartSpin(gbSpin, durationsOneLoop, (int)Loops, DistanceSpin, (int)DegreeTarget, clockWise));
        }

        IEnumerator StopOrStartSpin(GameObject gbTargetSpin, float durationRotate, int LoopsSpin, float Distance, int Degree, bool isClockwise = true)
        {
            if (!isClockwise)
            {
                Tween doTarget = gbSpin.transform.DOLocalRotate(gbSpin.transform.eulerAngles, 0f);
                float DegreeRotateLinear = 360f * Distance;
                Vector3 value = new Vector3(0f, 0f, DegreeRotateLinear);
                Vector3 valueRadian = new Vector3(0f, 0f, Degree);
                doTarget = gbTargetSpin.transform.DOLocalRotate(gbTargetSpin.transform.eulerAngles - value, durationRotate * Distance, RotateMode.FastBeyond360)
                                    .SetEase(Ease.InQuad);

                yield return doTarget.WaitForElapsedLoops(1);
                doTarget = gbTargetSpin.transform.DOLocalRotate(gbSpin.transform.eulerAngles - value, durationRotate, RotateMode.FastBeyond360)
                                                .SetLoops(LoopsSpin, LoopType.Incremental)
                                                .SetEase(EaseFactory.StopMotion(60, Ease.Linear));

                yield return doTarget.WaitForElapsedLoops(LoopsSpin);
                doTarget = gbTargetSpin.transform.DOLocalRotate(gbTargetSpin.transform.eulerAngles - valueRadian, durationRotate * Degree / DegreeRotateLinear, RotateMode.FastBeyond360)
                                                .SetEase(EaseFactory.StopMotion(60, Ease.Linear));

                yield return doTarget.WaitForElapsedLoops(1);
                doTarget = gbTargetSpin.transform.DOLocalRotate(gbTargetSpin.transform.eulerAngles - value, durationRotate * Distance, RotateMode.FastBeyond360)
                                                .SetEase(Ease.OutQuad);
                doTarget.OnComplete(() =>
                {
                    StartCoroutine(CompleRoulette());
                });
            }
            else
            {
                Tween doTarget = gbSpin.transform.DOLocalRotate(gbSpin.transform.eulerAngles, 0f);
                float DegreeRotateLinear = 360f * Distance;
                Vector3 value = new Vector3(0f, 0f, DegreeRotateLinear);
                Vector3 valueRadian = new Vector3(0f, 0f, Degree);
                doTarget = gbTargetSpin.transform.DOLocalRotate(gbTargetSpin.transform.eulerAngles + value, durationRotate * Distance, RotateMode.FastBeyond360)
                                    .SetEase(Ease.InQuad);

                yield return doTarget.WaitForElapsedLoops(1);
                doTarget = gbTargetSpin.transform.DOLocalRotate(gbSpin.transform.eulerAngles + value, durationRotate, RotateMode.FastBeyond360)
                                                .SetLoops(LoopsSpin, LoopType.Incremental)
                                                .SetEase(EaseFactory.StopMotion(60, Ease.Linear));

                yield return doTarget.WaitForElapsedLoops(LoopsSpin);
                doTarget = gbTargetSpin.transform.DOLocalRotate(gbTargetSpin.transform.eulerAngles + valueRadian, durationRotate * Degree / DegreeRotateLinear, RotateMode.FastBeyond360)
                                                .SetEase(EaseFactory.StopMotion(60, Ease.Linear));

                yield return doTarget.WaitForElapsedLoops(1);
                doTarget = gbTargetSpin.transform.DOLocalRotate(gbTargetSpin.transform.eulerAngles + value, durationRotate * Distance, RotateMode.FastBeyond360)
                                                .SetEase(Ease.OutQuad);
                doTarget.OnComplete(() =>
                {
                    StartCoroutine(CompleRoulette());
                });
            }
        }
        IEnumerator CompleRoulette()
        {
            gbCoverItem.SetActive(true);
            yield return new WaitForSeconds(1f);
            lsSlotIcon[slotTarget].transform.SetParent(gbCoverItem.transform);
            lsSlotIcon[slotTarget].transform.DOMove(new Vector3(), 0.5f);
            // txtNumberFocus.text = ;
        }

    }
}

