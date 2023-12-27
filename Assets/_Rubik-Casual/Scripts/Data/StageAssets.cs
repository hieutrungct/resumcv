using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTPackage;
using SimpleJSON;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Data
{
    public class StageAssetsData
    {
        public float Stage, Attribute;
        public string Slot0, Slot1, Slot2, Slot3, Slot4;
    }
    [Serializable]
    public class ConvertStageAssetsData
    {
        public float Stage, Attribute;
        public List<string> lsValueSlot = new List<string>();
    }
    public class NameAndValueSlot
    {
        public string stringValue;
        public int intValue;
    }
    // public enum ValueIdInSlot
    // {
    //     Null = 0,
    //     Item = 1,
    //     Enemy = 2
    // }

    public class StageAssets : MonoBehaviour
    {
        private List<StageAssetsData> lsStageAssetsDatas;
        public List<ConvertStageAssetsData> lsConvertStageAssetsData;
        public TextAsset AssetData;
        public int idTest;
        public static StageAssets instance;
        void Awake()
        {
            instance = this;
            LoadStageAssets();

        }
        IEnumerator ConvertStageAssets()
        {
            yield return new WaitForSeconds(0.25f);
            foreach (var item in lsStageAssetsDatas)
            {
                ConvertStageAssetsData clone = new ConvertStageAssetsData();
                clone.Stage = item.Stage;
                clone.Attribute = item.Attribute;
                clone.lsValueSlot.Add(item.Slot0);
                clone.lsValueSlot.Add(item.Slot1);
                clone.lsValueSlot.Add(item.Slot2);
                clone.lsValueSlot.Add(item.Slot3);
                clone.lsValueSlot.Add(item.Slot4);

                lsConvertStageAssetsData.Add(clone);
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
        void LoadStageAssets()
        {
            lsStageAssetsDatas = new List<StageAssetsData>();

            foreach (JSONNode item in JSON.Parse(this.AssetData.text))
            {
                StageAssetsData waifuAssetData = JsonUtility.FromJson<StageAssetsData>(item.ToString());
                this.lsStageAssetsDatas.Add(waifuAssetData);
            }
            StartCoroutine(ConvertStageAssets());
        }
    }

}