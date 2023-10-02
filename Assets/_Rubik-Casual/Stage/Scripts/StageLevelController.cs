using System.Collections;
using System.Collections.Generic;
using RubikCasual.Stage;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.StageLevel
{
    public class StageLevelController : MonoBehaviour
    {

        public Button btnClose;
        void Start()
        {
            btnClose.onClick.AddListener(() => { destroyStageLevel(StageController.instance.stageLevelClone); });
        }
        public void destroyStageLevel(GameObject stageLevelClone)
        {
            Destroy(stageLevelClone);
        }
    }
}
