using System.Collections;
using System.Collections.Generic;

using RubikCasual.FlipCard2;
using UnityEngine;

public class SummonController : MonoBehaviour
{
    public GameObject GaCharCard;
    public void OnclickButton(int id)
    {
        GaCharCard.SetActive(true);
        GaCharCard.GetComponent<FlipCardController>().Id = id;
    }
    
}
