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
    public class RewardWinLevelStage
    {
        public int idItemReward, valuableItem, winWithStar;
        public DailyItem.TypeItem typeItem;
        public DailyItem.NameItem nameItem;
    }
    [Serializable]
    public class InfoLevelStageAssetsData
    {
        public int TurnLevelStage;
        public float Attribute;
        public string Slot0, Slot1, Slot2, Slot3, Slot4;
    }
    [Serializable]
    public class ConvertLevelStageAssetsData
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
    public class LevelStageAssetsData
    {
        public List<RewardWinLevelStage> lsRewardWinStage;
        public TypeMap typeMap;
        public List<InfoLevelStageAssetsData> lsStageAssetsDatas;
    }
    [Serializable]
    public class StageAssetData
    {
        public int id;
        public string NameStage, Path;
        public int NumberLevelAttack, NumberShop;
    }
    public class StageAssets : MonoBehaviour
    {
        public List<ConvertLevelStageAssetsData> lsConvertLevelStageAssetsData;
        public LevelStageAssetsData levelStageAssetsData;
        public List<StageAssetData> lsStageAssetData;
        public List<TextAsset> lsAssetData;
        public TextAsset stageAssetDatas;
        public int indexStage;
        public static StageAssets instance;
        protected void Awake()
        {
            instance = this;
            // LoadLevelStageAssets(indexStage);
            LoadStageAssets();
        }


        public RewardWinLevelStage GetRewardWinStage(int indexStar)
        {
            RewardWinLevelStage rewardWinStage = new RewardWinLevelStage();
            RewardWinLevelStage rewardWinStageClone = levelStageAssetsData.lsRewardWinStage.Find(f => f.winWithStar == indexStar);
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
        // [Button]
        void LoadStageAssets()
        {
            foreach (JSONNode item in JSON.Parse(this.stageAssetDatas.text))
            {
                StageAssetData stageAssetData = JsonUtility.FromJson<StageAssetData>(item.ToString());
                lsStageAssetData.Add(stageAssetData);
            }
        }
        [Button]
        public void SetLsAssetData(int idStage)
        {
            lsAssetData.Clear();
            StageAssetData stageAssetData = lsStageAssetData.Find(f => f.id == idStage);
            if (stageAssetData == null)
            {
                Debug.LogError("stageAssetData null");
                return;
            }
            for (int i = 0; i < stageAssetData.NumberLevelAttack; i++)
            {
                TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(stageAssetData.Path + "/Attack" + i + ".json");
                lsAssetData.Add(textAsset);
            }

        }
        public string GetNameStage(int idStage)
        {
            string nameStage = "";
            foreach (var item in lsStageAssetData)
            {
                if (item.id == idStage)
                {
                    nameStage = item.NameStage;
                }
            }
            return nameStage;
        }



        [Button]
        public void LoadLevelStageAssets(int index)
        {
            levelStageAssetsData = JsonUtility.FromJson<LevelStageAssetsData>(JSON.Parse(this.lsAssetData[index].text).ToString());
            ConvertStageAssets();
        }
        void ConvertStageAssets()
        {
            if (lsConvertLevelStageAssetsData != null)
            {
                lsConvertLevelStageAssetsData.Clear();
            }
            // yield return new WaitForSeconds(0.25f);
            foreach (var item in levelStageAssetsData.lsStageAssetsDatas)
            {
                ConvertLevelStageAssetsData clone = new ConvertLevelStageAssetsData();
                clone.TurnStage = item.TurnLevelStage;
                clone.Attribute = item.Attribute;
                clone.lsValueSlot.Add(item.Slot0);
                clone.lsValueSlot.Add(item.Slot1);
                clone.lsValueSlot.Add(item.Slot2);
                clone.lsValueSlot.Add(item.Slot3);
                clone.lsValueSlot.Add(item.Slot4);

                lsConvertLevelStageAssetsData.Add(clone);
            }
        }
    }

}