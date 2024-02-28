using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTPackage;
using RubikCasual.CreateSkill.Panel;
using SimpleJSON;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Data
{
    [Serializable]
    public class RewardWinStage
    {
        public int idItemReward, valuableItem, winWithStar;
        public DailyItem.TypeItem typeItem;
        public DailyItem.NameItem nameItem;
    }
    [Serializable]
    public class InfoStageAssetsData
    {
        public int TurnStage;
        public float Attribute;
        public string Slot0, Slot1, Slot2, Slot3, Slot4;
    }
    [Serializable]
    public class ConvertStageAssetsData
    {
        public float TurnStage, Attribute;
        public List<string> lsValueSlot = new List<string>();
    }
    public class NameAndValueSlot
    {
        public string stringValue;
        public int intValue;
    }
    [Serializable]
    public class StageAssetsData
    {
        public List<RewardWinStage> lsRewardWinStage;
        public TypeMap typeMap;
        public List<InfoStageAssetsData> lsStageAssetsDatas;
    }

    public class StageAssets : MonoBehaviour
    {
        public List<ConvertStageAssetsData> lsConvertStageAssetsData;
        public StageAssetsData stageAssetsData;
        public List<TextAsset> lsAssetData;
        public int idTest, indexStage;
        public static StageAssets instance;
        protected void Awake()
        {
            instance = this;
            LoadStageAssets(indexStage);
        }
        IEnumerator ConvertStageAssets()
        {
            if (lsConvertStageAssetsData != null)
            {
                lsConvertStageAssetsData.Clear();
            }
            yield return new WaitForSeconds(0.25f);
            foreach (var item in stageAssetsData.lsStageAssetsDatas)
            {
                ConvertStageAssetsData clone = new ConvertStageAssetsData();
                clone.TurnStage = item.TurnStage;
                clone.Attribute = item.Attribute;
                clone.lsValueSlot.Add(item.Slot0);
                clone.lsValueSlot.Add(item.Slot1);
                clone.lsValueSlot.Add(item.Slot2);
                clone.lsValueSlot.Add(item.Slot3);
                clone.lsValueSlot.Add(item.Slot4);

                lsConvertStageAssetsData.Add(clone);
            }
        }
        [Button]
        public void TestRewardWinStage(int indexStar)
        {
            Debug.Log(GetRewardWinStage(indexStar).winWithStar);
        }
        public RewardWinStage GetRewardWinStage(int indexStar)
        {
            RewardWinStage rewardWinStage = new RewardWinStage();
            RewardWinStage rewardWinStageClone = stageAssetsData.lsRewardWinStage.Find(f => f.winWithStar == indexStar);
            if (rewardWinStageClone == null)
            {
                return null;
            }
            else
            {
                rewardWinStage.idItemReward = rewardWinStageClone.idItemReward;
                rewardWinStage.nameItem = rewardWinStageClone.nameItem;
                rewardWinStage.typeItem = rewardWinStageClone.typeItem;
                rewardWinStage.valuableItem = rewardWinStageClone.valuableItem;
                rewardWinStage.winWithStar = rewardWinStageClone.winWithStar;

                return rewardWinStage;
            }

        }
        public NameAndValueSlot GetNameAndId(string ValueNameIndex)
        {
            NameAndValueSlot result = new NameAndValueSlot();
            string[] lsString = ValueNameIndex.Split(":");

            if (lsString.Length > 1)
            {
                result.stringValue = lsString[0];
                result.intValue = int.Parse(lsString[1]);
            }
            else
            {
                result.stringValue = lsString[0];
                result.intValue = -1;
            }

            return result;
        }
        [Button]
        public void LoadStageAssets(int index)
        {
            stageAssetsData = JsonUtility.FromJson<StageAssetsData>(JSON.Parse(this.lsAssetData[index].text).ToString());
            StartCoroutine(ConvertStageAssets());
        }
    }

}