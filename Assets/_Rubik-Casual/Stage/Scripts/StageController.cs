using System.Collections;
using System.Collections.Generic;
using RubikCasual.Lobby;
using RubikCasual.StageLevel;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Stage
{
    public class StageController : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public Transform levelClonePos, stageClonePos;
        public GameObject stageLevelClone, stageLevel, stage;
        List<GameObject> listStage = new List<GameObject>();
        public float numberStage, scrollStep;
        public static StageController instance;
        void Start()
        {


            instance = this;
            createStage();

        }
        void createStage()
        {
            for (int i = 0; i < numberStage; i++)
            {
                var stageClone = Instantiate(stage, stageClonePos);
                stageClone.AddComponent<Button>().onClick.AddListener(() => { createStageLevel(); });
                listStage.Add(stageClone);
            }
        }
        public void createStageLevel()
        {
            stageLevelClone = Instantiate(stageLevel, levelClonePos);
            var levelClone = stageLevelClone.GetComponent<StageLevelController>();
        }
        public void hideStage()
        {
            LobbyController.instance.PopupStageSelect.SetActive(false);
        }
        public void ScrollPrev()
        {
            scrollRect.horizontalNormalizedPosition -= scrollStep;
            
        }

        public void ScrollNext()
        {
            
            scrollRect.horizontalNormalizedPosition += scrollStep;
        }
    }
}
