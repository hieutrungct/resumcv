using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Battle.UI.Result
{
    public class NameRewardItemPopup
    {
        public const string name_Gb_Reward_Item = "Reward_Item", name_Gb_Icon = "Icon", name_Gb_Bonus = "Bonus", name_Gb_Text_Value = "Text_Value",
        name_Gb_Lock = "Lock";
    }
    public class RewardItem : MonoBehaviour
    {
        public GameObject gbIcon, gbBonus, gbTextValue, gbLock;
    }

    public class PopupResultWin : MonoBehaviour
    {
        public List<GameObject> lsStar, lsRewardItem;
        public UnityEngine.UI.Button btnExit;
        // public List<RewardItem> rewardItem;
        void Awake()
        {
            SetScriptRewardItem();
        }
        void Update()
        {
            if (!this.gameObject.activeSelf)
            {
                ResetPopupResult();
            }
        }

        void SetScriptRewardItem()
        {
            foreach (GameObject gbRewardItem in lsRewardItem)
            {
                if (gbRewardItem.GetComponent<RewardItem>() == null)
                {
                    RewardItem rewardItemClone = gbRewardItem.AddComponent<RewardItem>();
                    rewardItemClone.gbBonus = rewardItemClone.transform.Find(NameRewardItemPopup.name_Gb_Bonus).gameObject;
                    rewardItemClone.gbIcon = rewardItemClone.transform.Find(NameRewardItemPopup.name_Gb_Icon).gameObject;
                    rewardItemClone.gbTextValue = rewardItemClone.transform.Find(NameRewardItemPopup.name_Gb_Text_Value).gameObject;
                    rewardItemClone.gbLock = rewardItemClone.transform.Find(NameRewardItemPopup.name_Gb_Lock).gameObject;
                }
            }
        }
        [Button]
        public void SetStar(int numberStar)
        {
            if (numberStar > 0)
            {
                lsStar[numberStar - 1].transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f)
                .OnComplete(() =>
                {
                    lsStar[numberStar - 1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
                    SetStar(numberStar - 1);
                });
            }
        }
        [Button]
        public void ResetPopupResult()
        {
            ResetStar();
        }
        public void ResetStar()
        {
            foreach (GameObject star in lsStar)
            {
                star.transform.localScale = new Vector3();
            }
        }
        void ClickExitPopup()
        {

        }

    }
}

