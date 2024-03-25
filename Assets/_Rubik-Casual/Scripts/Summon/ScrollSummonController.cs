using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.SummonSliders;
using RubikCasual.Tool;
using UnityEngine;
namespace RubikCasual.ScrollSummon
{
    public class ScrollSummonController : MonoBehaviour
    {
        public List<SummonSlider> lsSummonSlider;
        public int a;
        public Transform transformFut, transformCus;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("OpenPopup", 5f, 5f);
        }
        
        public void OpenPopup()
        {
            
            
            if(a == 0)
            {
                lsSummonSlider[a + 1].transform.DOMoveX(gameObject.transform.position.x,1f);
                lsSummonSlider[a].transform.DOMoveX(transformCus.position.x,0.7f)
                .OnComplete(()=>{
                    SetUpSliderById();
                    a = 1;
                });
                

            }
            else
            {
                lsSummonSlider[a - 1].transform.DOMoveX(gameObject.transform.position.x,1f);
                lsSummonSlider[a].transform.DOMoveX(transformCus.position.x,0.7f)
                .OnComplete(()=>{
                    SetUpSliderById();
                    a = 0;
                });
                
            }
            

            
        }
        public void SetUpSliderById()
        {
            lsSummonSlider[a].transform.position = transformFut.position;
            lsSummonSlider[a].SetUpSlider();
            Debug.Log("Xét lại toạ độ");
        }

        
    }
}

