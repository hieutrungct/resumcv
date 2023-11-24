using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rubik.Axie
{
    public class AxieModel : MonoBehaviour, IPointerDownHandler
    {
        public AxieData AxieData = new AxieData();

        public void OnPointerDown(PointerEventData eventData){
            Debug.Log(AxieData.ToString());
        }
    }
}