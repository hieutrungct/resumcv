using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public EnermyLevelUI enermyUi;
        public RewardLevelUI rewardUi;
        public Transform rewardUiPos, enermyUiPos;
        public Sprite unlockFinishNow;
        public void closeButton()
        {
         
           StageLevelController.instance.infoLevelClone.gameObject.GetComponent<Animator>().SetTrigger("ClosePop");
            StartCoroutine(CloseButtonAfterAnimation());
        }

        IEnumerator CloseButtonAfterAnimation()
        {
            // Đợi một khoảng thời gian tương ứng với thời lượng của Animation Clip
            yield return new WaitForSeconds(0.5f);

            // Thực hiện các hành động sau khi Animation hoàn thành

            Destroy( StageLevelController.instance.infoLevelClone.gameObject);
           
        }
    }
    [Serializable]
    class EnemyUI
    {
        public Image iconEnermy;
        public List<GameObject> starEnermy;
    }

}