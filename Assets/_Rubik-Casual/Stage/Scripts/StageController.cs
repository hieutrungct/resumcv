using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RubikCasual.Data;
using RubikCasual.Stage.UI;
using RubikCasual.StageLevel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Stage
{
    public class StageController : MonoBehaviour
    {
        public ItemStageUI itemStageUI;
        public List<GameObject> lsGateStage;
        public StageLevelController stageLevelController;
        [Button]
        void LoadStageUI()
        {
            foreach (StageAssetData stageAssetData in DataController.instance.stageAssets.lsStageAssetData)
            {
                ItemStageUI itemStageUIClone = Instantiate(itemStageUI, itemStageUI.transform.parent);
                itemStageUIClone.gameObject.SetActive(true);
                itemStageUIClone.SetItemStageUI(stageAssetData, stageLevelController);
                lsGateStage.Add(itemStageUIClone.gameObject);
            }
        }
    }
}
