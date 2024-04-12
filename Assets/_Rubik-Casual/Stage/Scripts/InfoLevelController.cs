using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rubik_Casual;
using RubikCasual.InfoLevel.Reward;
using RubikCasual.StageLevel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.InfoLevel
{
    public class InfoLevelController : MonoBehaviour
    {
        public int id = 0;
        public TextMeshProUGUI textNameStage, textEnergy;
        public Image buttonFinish, lockFinishNow, buttonFight;
        public Button buttonFightNow;
        public EnemyLevelUI enemyUi;
        public RewardLevelUI rewardUi;
        public Transform rewardUiPos, enemyUiPos;
        public Sprite unlockFinishNow;
       

  
        public void LoadGamePlayScene()
        {
            HUDController.instanse.topPanel.SetActive(false);
            bl_SceneLoaderManager.LoadScene(NameScene.GAMEPLAY_SCENE);
        }
    }
    [Serializable]
    class EnemyUI
    {
        public Image iconEnemy;
        public List<GameObject> starEnemy;
    }
    

}