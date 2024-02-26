using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace RubikCasual.Battle.UI.Result
{
    public class NameRewardItemPopup
    {
        public const string name_Gb_Icon = "Icon", name_Gb_Bonus = "Bonus", name_Gb_Text_Value = "Text_Value",
        name_Gb_Lock = "Lock", name_Gb_Text_Exp = "Text_Exp", name_Gb_Text_Level = "Text_Level", name_Gb_Level_Frame = "Level_Frame";
    }
    public class RewardItem : MonoBehaviour
    {
        public GameObject gbBonus, gbLock;
        public TextMeshProUGUI txtValue;
        public UnityEngine.UI.Image Icon;
    }
    public class ExpPlayer : MonoBehaviour
    {
        public TextMeshProUGUI txtExp, txtLevel;
    }
    public class PopupResultWin : MonoBehaviour
    {
        public List<GameObject> lsStar, lsRewardItem;
        public GameObject gbExpBar;
        public UnityEngine.UI.Button btnExit;
        public float durationAnim = 0.25f;
        // public List<RewardItem> rewardItem;
        void Awake()
        {
            ClickExitPopup();
            LoadPopup();
        }
        void Update()
        {
            if (!this.gameObject.activeSelf)
            {
                ResetPopupResult();
            }
        }
        void LoadPopup()
        {
            SetScriptRewardItem();
            SetScriptExpPlayer();
        }
        void SetScriptRewardItem()
        {
            for (int i = 0; i < lsRewardItem.Count; i++)
            {
                int index = i;
                GameObject gbRewardItem = lsRewardItem[index];
                if (gbRewardItem.GetComponent<RewardItem>() == null)
                {
                    RewardItem rewardItemClone = gbRewardItem.AddComponent<RewardItem>();
                    rewardItemClone.gbBonus = rewardItemClone.transform.Find(NameRewardItemPopup.name_Gb_Bonus).gameObject;
                    rewardItemClone.Icon = rewardItemClone.transform.Find(NameRewardItemPopup.name_Gb_Icon).GetComponent<UnityEngine.UI.Image>();
                    rewardItemClone.txtValue = rewardItemClone.transform.Find(NameRewardItemPopup.name_Gb_Text_Value).GetComponent<TextMeshProUGUI>();
                    rewardItemClone.gbLock = rewardItemClone.transform.Find(NameRewardItemPopup.name_Gb_Lock).gameObject;

                    DailyItem.ItemData itemData = RubikCasual.Data.DataController.instance.itemData;
                    if (index < 2)
                    {
                        if (index == 0)
                        {
                            rewardItemClone.Icon.sprite = itemData.InfoItems.FirstOrDefault(f => f.id == 1).imageItem;
                        }
                        if (index == 1)
                        {
                            rewardItemClone.Icon.sprite = itemData.InfoItems.FirstOrDefault(f => f.id == 2).imageItem;
                        }
                    }
                    else
                    {
                        int random = UnityEngine.Random.Range(1, 5);
                        rewardItemClone.Icon.sprite = itemData.InfoItems.FirstOrDefault(f => f.id == random).imageItem;
                    }
                    rewardItemClone.Icon.preserveAspect = true;
                    rewardItemClone.txtValue.text = "0";
                }
            }
        }
        void SetScriptExpPlayer()
        {
            RubikCasual.Data.Player.UserData userData = RubikCasual.Data.DataController.instance.playerData.userData;
            if (gbExpBar.GetComponent<ExpPlayer>() == null)
            {
                ExpPlayer expPlayer = gbExpBar.AddComponent<ExpPlayer>();
                expPlayer.txtExp = expPlayer.transform.Find(NameRewardItemPopup.name_Gb_Text_Exp).GetComponent<TextMeshProUGUI>();
                expPlayer.txtLevel = expPlayer.transform.Find(NameRewardItemPopup.name_Gb_Level_Frame).Find(NameRewardItemPopup.name_Gb_Text_Level).GetComponent<TextMeshProUGUI>();

                expPlayer.txtLevel.text = userData.Level.ToString();
                expPlayer.txtExp.text = userData.Exp.ToString() + "/400";
            }
            // gbExpBar.GetComponent<UnityEngine.UI.Slider>().value = userData.Exp;

        }

        [Button]
        void SetDataPopup(int numberGet)
        {
            StartCoroutine(DelayAnimPopup(numberGet));
        }
        IEnumerator DelayAnimPopup(int numberGet)
        {
            SetAnimStar(numberGet);
            yield return new WaitForSeconds(durationAnim * numberGet);
            Tween animExp = SetAnimExp();
            yield return animExp.WaitForCompletion();
            SetAnimReward(numberGet);
        }
        void SetAnimStar(int numberStar, int index = 0)
        {
            if (numberStar > index)
            {
                Transform childStar = lsStar[index].transform.Find(NameRewardItemPopup.name_Gb_Icon);
                childStar.DOScale(new Vector3(1.2f, 1.2f, 1.2f), durationAnim)
                .OnComplete(() =>
                {
                    childStar.DOScale(new Vector3(1f, 1f, 1f), durationAnim);
                    SetAnimStar(numberStar, index + 1);
                });
            }
        }
        Tween SetAnimExp()
        {
            Tween tweenClone = gbExpBar.GetComponent<UnityEngine.UI.Slider>().DOValue(1f, durationAnim * 2);
            ExpPlayer expPlayer = gbExpBar.GetComponent<ExpPlayer>();
            // expPlayer.txtExp.dote
            return tweenClone;
        }
        void SetAnimReward(int numberUnLock, int index = 0)
        {
            if (numberUnLock > index)
            {

                RewardItem rewardItem = lsRewardItem[index].GetComponent<RewardItem>();

                rewardItem.gbLock.transform.DOScale(new Vector3(), durationAnim)
                .OnComplete(() =>
                {
                    SetAnimReward(numberUnLock, index + 1);
                });
            }
        }

        [Button]
        public void ResetPopupResult()
        {
            ResetStar();
            ResetExp();
            ResetReward();
        }
        void ResetStar()
        {
            foreach (GameObject star in lsStar)
            {
                star.transform.Find(NameRewardItemPopup.name_Gb_Icon).localScale = new Vector3();
            }
        }
        void ResetExp()
        {
            gbExpBar.GetComponent<UnityEngine.UI.Slider>().value = 0;
        }
        void ResetReward()
        {
            foreach (GameObject gbRewardItem in lsRewardItem)
            {
                RewardItem rewardItem = gbRewardItem.GetComponent<RewardItem>();
                rewardItem.gbLock.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        void ClickExitPopup()
        {
            this.btnExit.onClick.RemoveAllListeners();
            this.btnExit.onClick.AddListener(() =>
            {
                RubikCasual.Tool.LoadingScenes.BackHomeScene();
            });
        }

    }
}

