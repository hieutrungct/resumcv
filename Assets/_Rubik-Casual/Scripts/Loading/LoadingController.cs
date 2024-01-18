using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public GameObject btnPlayGame;
    public DataController dataController;
    public Slider slider;
    public TextMeshProUGUI textLoading;


    private float loadingProgress = 0f;
    private float percent = 1f;
    void Awake()
    {
        dataController.initData();
        
        dataController.initUserData();
        Loading();
        dataController.initListOwnWaifu();
        Loading();
        dataController.initPlayerData();
        Loading();
    }
    public void Loading()
    {
        loadingProgress += 1f;
        slider.value = loadingProgress;
        textLoading.text = "Loading..." + percent*loadingProgress*100/3 + "%";
        if(loadingProgress == 3f)
        {
            btnPlayGame.SetActive(true);
        }
        else
        {
            btnPlayGame.SetActive(false);
        }
        
        
    }
    public void PlayGameOnClick()
    {
        
        
        SceneManager.LoadScene(NameScene.HOME_SCENE);
        
    
    }
    // public void UpdateSlide()
    // {
    //     btnPlayGame.SetActive(false);
    //     if (loadingProgress == 3)
    //     {
    //         btnPlayGame.SetActive(true);
    //     }
    //     else
    //     {
    //         slider.value = loadingProgress;
    //     }
        
    // }
}
