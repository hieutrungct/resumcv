using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RubikCasual.Tool
{
    public class SliderTool : MonoBehaviour
    {
        public static UnityEngine.UI.Slider ChangeValueSlider(UnityEngine.UI.Slider sliderTarget, float oldValue, float newValue, bool isHaveSlider_Down = true)
        {
            if (isHaveSlider_Down)
            {
                sliderTarget.DOValue(newValue, 0.25f).OnComplete(() =>
                {
                    if (sliderTarget.transform.Find("Slider_Down").GetComponent<UnityEngine.UI.Slider>() != null)
                    {
                        UnityEngine.UI.Slider sliderTargetDown = sliderTarget.transform.Find("Slider_Down").GetComponent<UnityEngine.UI.Slider>();
                        sliderTargetDown.value = oldValue;
                        sliderTargetDown.DOValue(newValue, 0.15f);
                    }
                });

                return sliderTarget;
            }
            else
            {
                sliderTarget.DOValue(newValue, 0.25f);

                return sliderTarget;
            }
        }
    }
}
