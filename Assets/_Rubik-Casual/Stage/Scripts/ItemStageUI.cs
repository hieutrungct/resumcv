using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using RubikCasual.StageLevel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.Stage.UI
{
    public class ItemStageUI : MonoBehaviour
    {
        public Image iconImage;
        public GameObject iconLock, levelUnlock, iconNew, gbIconImage;
        public TextMeshProUGUI txtNameStage, textLevelUnlock;
        public void SetItemStageUI(StageAssetData stageAssetData, StageLevelController stageLevelController)
        {
            txtNameStage.text = stageAssetData.NameStage;
            iconImage.GetComponent<Button>().onClick.AddListener(() =>
            {
                DataController.instance.stageAssets.SetLsAssetData(stageAssetData.id);

                stageLevelController.gameObject.SetActive(true);
                // stageLevelController.CreateLevel(stageAssetData.NumberLevelAttack);
            });
        }
    }

}