using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rubik_Casual.Stage.UI;
using RubikCasual.Lobby;
using RubikCasual.StageData;
using RubikCasual.StageLevel;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Stage
{
    public class StageController : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public Transform levelClonePos, stageClonePos;
        public GameObject stageLevelClone, stageLevel, stage;
        public StageDataController stageDataController;
        List<GameObject> listStage = new List<GameObject>();
        public float numberStage;
        public static StageController instance;
        void Start()
        {
            instance = this;
            createStage();

        }
        void createStage()
        {

            for (int i = 0; i < stageDataController.stages.Length; i++)
            {
                var stageClone = Instantiate(stage, stageClonePos);
                stageClone.GetComponent<Image>().enabled = false;
                var isUnlockStage = stageDataController.stages[i].unlockStage;
                var editStageClone = stageClone.GetComponent<StageUI>();
                var idStage = stageDataController.stages[i].idStage;
                editStageClone.gbIconImage.GetComponent<Button>().onClick.AddListener(() =>
                {

                    if (isUnlockStage == true)
                    {
                        createStageLevel(idStage);
                    }
                });
                editStageClone.iconImage.sprite = stageDataController.stages[i].imageStage;
                editStageClone.iconImage.preserveAspect = true;
                editStageClone.textLevelUnlock.enabled = false;
                if (!stageDataController.stages[i].unlockStage)
                {
                    editStageClone.iconLock.SetActive(true);
                    editStageClone.textLevelUnlock.enabled = true;
                    editStageClone.textLevelUnlock.text = "Require level " + stageDataController.stages[i].numberLevelUnlockStage.ToString();
                }

                if (stageDataController.stages[i].isNew)
                {
                    editStageClone.iconNew.SetActive(true);
                }

                editStageClone.iconImage.rectTransform.sizeDelta = new Vector2(editStageClone.iconImage.rectTransform.sizeDelta.x, 700f);
                if (stageDataController.stages[i].nameStage == "City of Shadow")
                {
                    var test = new Vector3(0f, 0.75f, 0f);
                    editStageClone.iconImage.rectTransform.position += test;
                }
                listStage.Add(stageClone);

            }
        }

        public void createStageLevel(int idStage)
        {
            var stageLevelClone = Instantiate(stageLevel, levelClonePos);
            var stageLevelControllerClone = stageLevelClone.GetComponent<StageLevelController>();
            stageLevelControllerClone.setUp(idStage, stageLevelClone);
        }

        public void hideStage()
        {
            LobbyController.instance.PopupStageSelect.SetActive(false);
        }

    }
}
