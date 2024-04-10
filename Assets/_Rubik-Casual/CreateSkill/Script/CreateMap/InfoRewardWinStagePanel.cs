using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RubikCasual.DailyItem;
using RubikCasual.Data;
using TMPro;
using UnityEngine;

namespace RubikCasual.CreateSkill.Panel
{
    public class InfoRewardWinStagePanel : MonoBehaviour
    {
        public TMP_InputField inputFieldIdItem, inputFieldValuableItem, inputFieldWinWithStar;
        public TMP_Dropdown dropdownNameItem, dropdownTypeItem;
        public TextMeshProUGUI txtNumberStarNow, txtNotification;
        public List<Data.RewardWinLevelStage> lsRewardWinStage = new List<Data.RewardWinLevelStage>();
        RewardWinLevelStage rewardWinStage = new RewardWinLevelStage();
        float OriginPosX;
        bool isHidePopup;
        public static InfoRewardWinStagePanel instance;
        void Awake()
        {
            instance = this;
            OriginPosX = this.transform.position.x;
            LoadPanel();
        }
        void LoadPanel()
        {
            // Set NameItem
            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            foreach (NameItem nameItem in System.Enum.GetValues(typeof(NameItem)))
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(nameItem.ToString());
                optionDatas.Add(optionData);
            }

            dropdownNameItem.AddOptions(optionDatas);
            dropdownNameItem.onValueChanged.AddListener((int Value) =>
            {
                inputFieldIdItem.text = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.name == (NameItem)Value).id.ToString();
                if ((NameItem)Value != NameItem.Jar)
                {
                    dropdownTypeItem.gameObject.SetActive(false);
                }
                else
                {
                    dropdownTypeItem.gameObject.SetActive(true);
                }
            });
            // Set TypeItem
            optionDatas = new List<TMP_Dropdown.OptionData>();
            foreach (TypeItem typeItem in System.Enum.GetValues(typeof(TypeItem)))
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(typeItem.ToString());
                optionDatas.Add(optionData);
            }

            dropdownTypeItem.AddOptions(optionDatas);


        }
        public void AddItemIntoList()
        {
            rewardWinStage = new RewardWinLevelStage();
            if (lsRewardWinStage.Count == 0 || (lsRewardWinStage.Find(f => f.winWithStar == int.Parse(inputFieldWinWithStar.text)) == null) && int.Parse(inputFieldWinWithStar.text) < 6)
            {
                rewardWinStage.idItemReward = int.Parse(inputFieldIdItem.text);
                rewardWinStage.nameItem = (NameItem)dropdownNameItem.value;
                rewardWinStage.valuableItem = int.Parse(inputFieldValuableItem.text);
                rewardWinStage.typeItem = (TypeItem)dropdownTypeItem.value;
                rewardWinStage.winWithStar = int.Parse(inputFieldWinWithStar.text);

                lsRewardWinStage.Add(rewardWinStage);
            }

            txtNumberStarNow.text = "Number Star: " + lsRewardWinStage.Count.ToString();
        }
        public void ShowAndHidePopup()
        {
            if (!isHidePopup)
            {
                this.transform.DOMoveX(OriginPosX + 3f, 0.5f);
            }
            else
            {
                this.transform.DOMoveX(OriginPosX, 0.5f);
            }
            isHidePopup = !isHidePopup;
        }
    }
}
