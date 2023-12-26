using System.Collections;
using System.Collections.Generic;
using RubikCasual.Character_ACC;
using RubikCasual.DailyItem;
using RubikCasual.Data.Player;
using RubikCasual.Lobby;
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

        public static DataController instance;
        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);
            StartCoroutine(LoadData());
        }
        // void Start()
        // {
        // }

        [Button]
        void BtnSavePlayerData()
        {
            SaveDataToJson(playerData, "PlayerData");
            Debug.Log(playerData.userData.Hp);
        }
        IEnumerator LoadData()
        {
            yield return new WaitForSeconds(0.25f);
            characterAssets = CharacterAssets.instance;
            LoadPlayerDataToJson("PlayerData");
        }
        void LoadPlayerDataToJson(string nameFile)
        {
            string filePath = Application.dataPath + $"/_Rubik-Casual/Resources/{nameFile}.json";
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
            System.IO.File.WriteAllText(Application.dataPath + $"/_Rubik-Casual/Resources/{nameFile}.json", json);
        }


    }
}