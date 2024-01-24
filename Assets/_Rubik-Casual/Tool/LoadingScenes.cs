using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubikCasual.Tool
{
    public class LoadingScenes : MonoBehaviour
    {
        static void LoadScene(string nameScene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
        }
        public static void BackHomeScene()
        {
            LoadScene(NameScene.HOME_SCENE);
        }
        public static void LoadGamePlay()
        {
            LoadScene(NameScene.GAMEPLAY_SCENE);
        }
    }

}