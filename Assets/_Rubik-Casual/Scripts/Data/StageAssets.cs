using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;

namespace RubikCasual.Data
{
    [Serializable]
    public class StageAssetsData
    {
        public float Stage, Attribute;
        public string Slot0, Slot1, Slot2, Slot3, Slot4;
    }
    public class StageAssets : MonoBehaviour
    {
        public List<StageAssetsData> lsStageAssetsDatas;
        public TextAsset AssetData;
        public int idTest;
        void Awake()
        {
            LoadStageAssets();
            string[] lsStringSlot = lsStageAssetsDatas[0].Slot0.Split(":");
            UnityEngine.Debug.Log(lsStringSlot[0] + ": " + lsStringSlot.Length);
            idTest = int.Parse(lsStringSlot[1]);
        }
        void LoadStageAssets()
        {
            lsStageAssetsDatas = new List<StageAssetsData>();

            foreach (JSONNode item in JSON.Parse(this.AssetData.text))
            {
                StageAssetsData waifuAssetData = JsonUtility.FromJson<StageAssetsData>(item.ToString());
                this.lsStageAssetsDatas.Add(waifuAssetData);
            }
        }
    }

}