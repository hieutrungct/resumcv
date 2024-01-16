using System.Collections;
using System.Collections.Generic;
using RubikCasual.Character_ACC;
using RubikCasual.DailyItem;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.Waifu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Data
{
    public class DataController : MonoBehaviour
    {
        public PlayerData playerData;
        public ListOwnsWaifu listOwnsWaifu;
        public UserData userData;
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
            LoadPlayerDataToJson();
        }
        [Button]
        public void BtnSaveUserDataToJson()
        {

            SaveUserDataToJson();

        }
        [Button]
        public void BtnSaveOwnWaifuDataToJson()
        {

            SavePlayerOwnsWaifuDataToJson();

        }
        void OnDestroy()
        {
            // SaveDataToJson(playerData, "PlayerData");
            SaveDataToJson();
        }

        void LoadPlayerDataToJson()
        {
            // string filePath = Application.dataPath + $"/_Data/Resources/{nameFile}.json";
            // if (System.IO.File.Exists(filePath))
            // {
            //     string json = System.IO.File.ReadAllText(filePath);

            //     PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            //     if (data != null)
            //     {
            //         playerData = data;
            //     }
            //     else
            //     {
            //         Debug.LogError("Failed to load PlayerData!");
            //     }
            // }
            // else
            // {
            //     Debug.LogError("File not found!");
            // }
            if(PlayerPrefs.HasKey(NameKey.Data))
            {
                string json = PlayerPrefs.GetString(NameKey.Data);
                playerData = JsonUtility.FromJson<PlayerData>(json);
                listOwnsWaifu = JsonUtility.FromJson<ListOwnsWaifu>(PlayerPrefs.GetString(NameKey.USER_OWN_WAIFU_KEY));
                userData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(NameKey.USER_DATA_KEY));

            }
            else
            {
                playerData = new PlayerData();
            }
        }
        void SaveDataToJson()
        {
            playerData.userData = userData;
            playerData.lsPlayerOwnsWaifu = listOwnsWaifu.lsOwnsWaifu;
            // Chuyển đổi ScriptableObject thành JSON
            string json = JsonUtility.ToJson(playerData);

            // Lưu JSON vào tệp
            // System.IO.File.WriteAllText(Application.dataPath + $"/_Data/Resources/{nameFile}.json", json);
            PlayerPrefs.SetString(NameKey.Data,json);
        }
        void SaveUserDataToJson()
        {
            // Chuyển đổi ScriptableObject thành JSON
            string json = JsonUtility.ToJson(userData);
            PlayerPrefs.SetString(NameKey.USER_DATA_KEY,json);
        }
        void SavePlayerOwnsWaifuDataToJson()
        {
            // Chuyển đổi ScriptableObject thành JSON
            string json = JsonUtility.ToJson(listOwnsWaifu);
            PlayerPrefs.SetString(NameKey.USER_OWN_WAIFU_KEY,json);
        }



    }
}