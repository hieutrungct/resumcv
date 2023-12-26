using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RubikCasual.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Data.Player
{
    public class PlayerAssetsLoader : MonoBehaviour
    {
        public PlayerData playerData;
        private PlayerData previousData;
        void Start()
        {
            BtnLoadJson();
            LoadPlayerDataToJson("PlayerData");
        }
        private void Update()
        {
            if (HasDataChanged())
            {
                BtnSaveJson();
                LoadPlayerDataToJson("PlayerData");
            }

        }
        IEnumerator setData()
        {
            yield return new WaitForSeconds(0.25f);
            playerData.userData.UserName = "po123lop123";
            playerData.userData.Gold = Data.instance.itemData.datalobby.FirstOrDefault(f => f.name == "Coins").numberItem;
            playerData.userData.Gem = Data.instance.itemData.datalobby.FirstOrDefault(f => f.name == "Gems").numberItem;
            playerData.userData.Energy = Data.instance.itemData.datalobby.FirstOrDefault(f => f.name == "Energy").numberItem;
            playerData.userData.CurentTeam = Data.instance.itemData.lsIdSlotSetupCharacter;
        }

        [Button]
        void BtnLoadJson()
        {
            LoadPlayerDataToJson("PlayerData");
        }
        void SaveDataToJson(System.Object obj, string nameFile)
        {
            // Chuyển đổi ScriptableObject thành JSON
            string json = JsonUtility.ToJson(obj);

            // Lưu JSON vào tệp
            System.IO.File.WriteAllText(Application.dataPath + $"/_Rubik-Casual/Resources/{nameFile}.json", json);
        }

        [Button]
        void BtnSaveJson()
        {
            SaveDataToJson(playerData, "PlayerData");
            previousData = ClonePlayerData(playerData); // Lưu lại dữ liệu sau khi đã lưu
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
                    previousData = ClonePlayerData(playerData); // Lưu trước khi thay đổi
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

        bool HasDataChanged()
        {
            if (playerData != previousData)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        PlayerData ClonePlayerData(PlayerData originalData)
        {
            // Tạo một bản sao của PlayerData để so sánh
            string json = JsonUtility.ToJson(originalData);
            return JsonUtility.FromJson<PlayerData>(json);
        }

    }

}