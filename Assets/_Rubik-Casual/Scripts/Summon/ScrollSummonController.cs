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
        public int IndexSlider, indexSummon = 2;
        public Transform transformFut, transformCus;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("OpenPopup", 5f, 5f);
        }
        
        public void OpenPopup()
        {
            
            
            if(IndexSlider == 0)
            {
                lsSummonSlider[IndexSlider + 1].transform.DOMoveX(gameObject.transform.position.x,1f);
                lsSummonSlider[IndexSlider].transform.DOMoveX(transformCus.position.x,0.7f)
                .OnComplete(()=>{
                    SetUpSliderById();
                    IndexSlider = 1;
                });
                

            }
            else
            {
                lsSummonSlider[IndexSlider - 1].transform.DOMoveX(gameObject.transform.position.x,1f);
                lsSummonSlider[IndexSlider].transform.DOMoveX(transformCus.position.x,0.7f)
                .OnComplete(()=>{
                    SetUpSliderById();
                    IndexSlider = 0;
                });
                
            }
            

            
        }
        public void SetUpSliderById()
        {
            if(indexSummon > 6)
            {
                indexSummon = 1;
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

