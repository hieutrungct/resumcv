using System;
using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
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
        public AssetLoader assetLoader;
        public StageAssets stageAssets;
        public static DataController instance;
        void Awake()
        {
            instance = this;

        }
        public void initData()
        {

            DontDestroyOnLoad(this);
            LoadData();
        }

        public void initUserData()
        {
            LoadUserData();
        }
        public void initListOwnWaifu()
        {

            LoadlistOwnWaifu();
        }
        public void initPlayerData()
        {

            LoadPlayerData();
        }


        public InfoWaifuAsset GetInfoWaifuAssetsByIndex(int ID)
        {
            foreach (var item in WaifuAssets.instance.infoWaifuAssets.lsInfoWaifuAssets)
            {

                if (item.ID == ID)
                {
                    return item;
                }
            }
            return null;
        }
        public PlayerOwnsWaifu GetPlayerOwnsWaifuByID(int Id)
        {
            foreach (var item in listOwnsWaifu.lsOwnsWaifu)
            {
                if (item.ID == Id)
                {
                    return item;
                }
            }
            return null;
        }
        // void Start()
        // {
        // }

        void LoadData()
        {
            characterAssets = CharacterAssets.instance;
            stageAssets = StageAssets.instance;
            //LoadPlayerDataToJson();
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
            // SaveDataToJson();
            SaveUserDataToJson();
            SavePlayerOwnsWaifuDataToJson();
        }

        public void LoadPlayerDataToJson()
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


            if (PlayerPrefs.HasKey(NameKey.USER_DATA_KEY))
            {
                // string json = PlayerPrefs.GetString(NameKey.Data);
                // playerData = JsonUtility.FromJson<PlayerData>(json);
                listOwnsWaifu = JsonUtility.FromJson<ListOwnsWaifu>(PlayerPrefs.GetString(NameKey.USER_OWN_WAIFU_KEY));
                userData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(NameKey.USER_DATA_KEY));
            }
            else
            {
                userData = UserData.GetDefaultUserData();

            }
            playerData.lsPlayerOwnsWaifu = listOwnsWaifu.lsOwnsWaifu;
            playerData.userData = userData;

        }
        void LoadUserData()
        {

            if (PlayerPrefs.HasKey(NameKey.USER_DATA_KEY))
            {

                userData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(NameKey.USER_DATA_KEY));

            }
            else
            {
                userData = UserData.GetDefaultUserData();

            }
        }
        void LoadlistOwnWaifu()
        {
            if (PlayerPrefs.HasKey(NameKey.USER_OWN_WAIFU_KEY))
            {
                listOwnsWaifu = JsonUtility.FromJson<ListOwnsWaifu>(PlayerPrefs.GetString(NameKey.USER_OWN_WAIFU_KEY));


            }
            else
            {
                listOwnsWaifu = new ListOwnsWaifu();

            }
        }
        void LoadPlayerData()
        {
            playerData.userData = userData;
            playerData.lsPlayerOwnsWaifu = listOwnsWaifu.lsOwnsWaifu;

        }
        public void UpdateWaifu(PlayerOwnsWaifu waifu, float curGoldUpdate)
        {
            if (userData.Gold >= curGoldUpdate)
            {
                userData.Gold -= curGoldUpdate;
            }
            else
            {
                Debug.Log("Bạn ko đủ tiền");
                return;
            }
            waifu.level += 1;
            waifu.ATK += 10;
            waifu.DEF += 10;
            waifu.Pow += 10;
            waifu.HP += 10;
            SavePlayerOwnsWaifuDataToJson();



            BtnSaveUserDataToJson();
            HUDController.instanse.LoadStatusNumber();
        }




        // void SaveDataToJson()
        // {
        //     playerData.userData = userData;
        //     playerData.lsPlayerOwnsWaifu = listOwnsWaifu.lsOwnsWaifu;
        //     // Chuyển đổi ScriptableObject thành JSON
        //     string json = JsonUtility.ToJson(playerData);

        //     // Lưu JSON vào tệp
        //     // System.IO.File.WriteAllText(Application.dataPath + $"/_Data/Resources/{nameFile}.json", json);
        //     PlayerPrefs.SetString(NameKey.Data,json);
        // }
        void SaveUserDataToJson()
        {
            // Chuyển đổi ScriptableObject thành JSON
            string json = JsonUtility.ToJson(userData);
            PlayerPrefs.SetString(NameKey.USER_DATA_KEY, json);
        }
        void SavePlayerOwnsWaifuDataToJson()
        {
            // Chuyển đổi ScriptableObject thành JSON
            string json = JsonUtility.ToJson(listOwnsWaifu);
            PlayerPrefs.SetString(NameKey.USER_OWN_WAIFU_KEY, json);
        }

        // public void LoadDataTest()
        // {
        //     LoadPlayerDataToJson();
        // }



    }
}