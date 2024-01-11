using System.Collections;
using System.Collections.Generic;
using RubikCasual.Character_ACC;
using RubikCasual.DailyItem;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.Lobby;
using RubikCasual.Waifu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Data
{
    public class DataController : MonoBehaviour
    {
        public PlayerData playerData;
        public ItemData itemData;
        public CharacterAssets characterAssets;
        public TextAsset AssetPlayerData;
        public StageAssets stageAssets;
        public static DataController instance;
        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);
            StartCoroutine(LoadData());
        }
        
        public InfoWaifuAsset GetInfoWaifuAssetsByIndex(int index)
        {
            foreach (var item in WaifuAssets.instance.infoWaifuAssets.lsInfoWaifuAssets)
            {
                
                if(item.ID == index){
                    return item;
                }
            }
            return null;
        }
        // void Start()
        // {
        // }

        IEnumerator LoadData()
        {
            yield return new WaitForSeconds(0.25f);
            characterAssets = CharacterAssets.instance;
            stageAssets = StageAssets.instance;
            LoadPlayerDataToJson("PlayerData");
        }
        [Button]
        public void BtnSavePlayerData()
        {

            SaveDataToJson(playerData, "PlayerData");

        }

        void LoadPlayerDataToJson(string nameFile)
        {
            string filePath = Application.dataPath + $"/_Data/Resources/{nameFile}.json";
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);

                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                if (data != null)
                {
                    playerData = data;
                }
                else
                {
                    Debug.LogError("Failed to load PlayerData!");
                }
            }
            else
            {
                Debug.LogError("File not found!");
            }
        }
        void SaveDataToJson(System.Object obj, string nameFile)
        {
            // Chuyển đổi ScriptableObject thành JSON
            string json = JsonUtility.ToJson(obj);

            // Lưu JSON vào tệp
            System.IO.File.WriteAllText(Application.dataPath + $"/_Data/Resources/{nameFile}.json", json);
        }


    }
}