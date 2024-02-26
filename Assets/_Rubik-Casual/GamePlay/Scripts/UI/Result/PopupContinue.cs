using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle.UI.Result
{
    public class PopupContinue : MonoBehaviour
    {
        public Button btnContinue, btnExit;
        void Start()
        {
            ClickBtnContinue();
            ClickBtnBackHomeScene();
        }
        void ClickBtnContinue()
        {
            this.btnContinue.onClick.RemoveAllListeners();
            this.btnContinue.onClick.AddListener(() =>
            {
                BattleController.instance.gameState = GameState.WAIT_BATTLE;
                this.gameObject.SetActive(false);
                BattleController.instance.ResetGame();
            });
        }
        void ClickBtnBackHomeScene()
        {
            this.btnExit.onClick.RemoveAllListeners();
            this.btnExit.onClick.AddListener(() =>
            {
                RubikCasual.Tool.LoadingScenes.BackHomeScene();
            });
        }
    }
}
