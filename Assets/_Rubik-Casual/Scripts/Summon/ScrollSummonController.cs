using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.SummonSliders;
using UnityEngine;
namespace RubikCasual.ScrollSummon
{
    public class ScrollSummonController : MonoBehaviour
    {
        public List<SummonSlider> lsSummonSlider;
        public int IndexSlider, indexSummon = 1;
        public Transform transformFut, transformCus;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("ScrollSlide", 5f, 5f);
        }
        
        public void ScrollSlide()
        {
            
            
            // if(IndexSlider == 0)
            // {
            //     lsSummonSlider[IndexSlider + 1].transform.DOMoveX(gameObject.transform.position.x,1f);
            //     lsSummonSlider[IndexSlider].transform.DOMoveX(transformCus.position.x,1f)
            //     .OnComplete(()=>{
            //         SetUpSliderById();
            //         IndexSlider = 1;
            //     });
                

            // }
            // else
            // {
            //     lsSummonSlider[IndexSlider - 1].transform.DOMoveX(gameObject.transform.position.x,1f);
            //     lsSummonSlider[IndexSlider].transform.DOMoveX(transformCus.position.x,1f)
            //     .OnComplete(()=>{
            //         SetUpSliderById();
            //         IndexSlider = 0;
            //     });
                
            // }
            int nextIndexSlider = (IndexSlider == 0) ? 1 : 0;
            int value = (IndexSlider == 0) ? -1 : 1;
            lsSummonSlider[nextIndexSlider].transform.DOMoveX(gameObject.transform.position.x,1f);
            lsSummonSlider[nextIndexSlider + value].transform.DOMoveX(transformCus.position.x,1f)
            .OnComplete(()=>{
                SetUpSliderById();
                IndexSlider = nextIndexSlider;
            });

        }
        public void SetUpSliderById()
        {
            if(indexSummon > 5)
            {
                indexSummon = 0;
            }
            else
            {
                indexSummon++;
            }
            lsSummonSlider[IndexSlider].indexSummon = indexSummon;
            lsSummonSlider[IndexSlider].transform.position = transformFut.position;
            lsSummonSlider[IndexSlider].SetUpSlider();
            // Debug.Log("Xét lại toạ độ");
        }

        
    }
}

