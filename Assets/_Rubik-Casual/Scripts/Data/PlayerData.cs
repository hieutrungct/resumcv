using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTPackage;
using NTPackage.Functions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Data
{
    public class WaifuInBattleSage
    {
        public int indexOfSlot, Index;
        public float HpNow, Rage;
    }
    public class StagePlay
    {
        public int Index;
        public List<WaifuInBattleSage> CurentTeam = new List<WaifuInBattleSage>();
        public Dictionary<int, int> InventoryInStage = new Dictionary<int, int>();
    }
    public class PlayerOwnsWaifu
    {
        public int Index, Star, Exp, IndexSkin, IndexEvolution;
    }
    [Serializable]
    public class UserData
    {
        public string UserName, UserId;
        public float Gold, Gem, Level, Energy, Hp, Exp, Rank;
        public List<float> CurentTeam = new List<float>();
    }
    public class PlayerData : NTBehaviour
    {
        public UserData userData;
        public StagePlay stagePlay;
        // public List<PlayerOwnsWaifu> lsPlayerOwnsWaifus;

        [SerializeField] NTDictionary<int, int> Inventory = new NTDictionary<int, int>();
        public int CurentStage;

        public static PlayerData instance;
        protected override void Start()
        {
            instance = this;
            LoadDataToJson(this, "PlayerData");
        }
        protected override void Update()
        {

        }
        // IEnumerator setData()
        // {
        //     yield return new WaitForSeconds(0.25f);
        //     userData.UserName = "po123lop123";
        //     userData.Gold = Data.instance.itemData.datalobby.FirstOrDefault(f => f.name == "Coins").numberItem;
        //     userData.Gem = Data.instance.itemData.datalobby.FirstOrDefault(f => f.name == "Gems").numberItem;
        //     userData.Energy = Data.instance.itemData.datalobby.FirstOrDefault(f => f.name == "Energy").numberItem;
        //     userData.CurentTeam = Data.instance.itemData.lsIdSlotSetupCharacter;
        // }
        [Button]
        void BtnSaveJson()
        {
            SaveDataToJson(this, "PlayerData");
        }
        [Button]
        void BtnLoadJson()
        {
            LoadDataToJson(this, "PlayerData");
        }
        void SaveDataToJson(System.Object obj, string nameFile)
        {
            // Chuyển đổi ScriptableObject thành JSON
            string json = JsonUtility.ToJson(obj);

            // Lưu JSON vào tệp
            System.IO.File.WriteAllText(Application.dataPath + $"/_Rubik-Casual/Resources/{nameFile}.json", json);
        }
        string LoadDataToJson(System.Object obj, string nameFile)
        {
            string json = System.IO.File.ReadAllText(Application.dataPath + $"/_Rubik-Casual/Resources/{nameFile}.json"); ;
            Debug.Log(json);
            return json;

            // foreach (JSONNode item in JSON.Parse(this.AssetData.text))
            // {
            //     WaifuAssetData waifuAssetData = JsonUtility.FromJson<WaifuAssetData>(item.ToString());
            //     this.WaifuAssetDatas.Add(waifuAssetData);
            // }
        }

    }
}

