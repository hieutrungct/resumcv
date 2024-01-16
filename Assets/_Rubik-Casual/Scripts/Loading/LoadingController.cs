using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public GameObject btnPlayGame;
    void Awake()
    {
        Loading();
    }
    public void Loading()
    {
        
    }
    public void PlayGameOnClick()
    {
        SceneManager.LoadScene(NameScene.HOME_SCENE);

    }
}
