using System;
using System.Collections;
using System.Collections.Generic;
using NTPackage;
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
        public List<ExpWithLevel> lsExpWithLevel = new List<ExpWithLevel>();
        // Dictionary
        public CharacterAssets characterAssets;
        public TextAsset AssetPlayerData, AssetUpLevel;
        public AssetLoader assetLoader;
        public StageAssets stageAssets;
        NTDictionary<int, int> dicExpWithLevel = new NTDictionary<int, int>();
        public static DataController instance;
        void Awake()
        {
            if (DataController.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            DataController.instance = this;
            lsExpWithLevel = ConfigLvl.LoadAsset(AssetUpLevel);
            SetDicExpWithLevel();
        }


        void LoadAttribute(PlayerOwnsWaifu playerOwnsWaifu)
        {

            InfoWaifuAsset infoWaifuAsset = characterAssets.GetInfoWaifuAsset(playerOwnsWaifu.ID);
            int originAttributeAtk = infoWaifuAsset.ATK;
            int originAttributeDef = infoWaifuAsset.DEF;
            int originAttributeHp = infoWaifuAsset.HP;

            // Debug.Log(playerOwnsWaifu.ID);
            // Debug.Log(originAttributeAtk);
            // Debug.Log(originAttributeDef);
            // Debug.Log(originAttributeHp);

            int nowAttributeAtk = ConfigLvl.GetAttributeStatByLevel(originAttributeAtk, playerOwnsWaifu.ATK, 1, playerOwnsWaifu.level);
            int nowAttributeDef = ConfigLvl.GetAttributeStatByLevel(originAttributeDef, playerOwnsWaifu.DEF, 1, playerOwnsWaifu.level);
            int nowAttributeHp = ConfigLvl.GetAttributeStatByLevel(originAttributeHp, playerOwnsWaifu.HP, 5, playerOwnsWaifu.level);

            if (nowAttributeAtk != originAttributeAtk)
            {
                playerOwnsWaifu.ATK = nowAttributeAtk;
            }
            if (nowAttributeDef != originAttributeDef)
            {
                playerOwnsWaifu.DEF = nowAttributeDef;
            }
            if (nowAttributeHp != originAttributeHp)
            {
                playerOwnsWaifu.HP = nowAttributeHp;
            }


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
        void SetDicExpWithLevel()
        {
            foreach (ExpWithLevel expWithLevel in lsExpWithLevel)
            {
                dicExpWithLevel.Add(expWithLevel.Level, expWithLevel.FinalEXP);
            }
        }
        bool CheckLevelUp(int levelNow, int expNow)
        {
            if (dicExpWithLevel.Get(levelNow) <= expNow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [Button]
        public void CaculateLevelUp()
        {
            foreach (PlayerOwnsWaifu playerOwnsWaifu in listOwnsWaifu.lsOwnsWaifu)
            {
                if (CheckLevelUp(playerOwnsWaifu.level, playerOwnsWaifu.Exp))
                {
                    playerOwnsWaifu.Exp = playerOwnsWaifu.Exp - dicExpWithLevel.Get(playerOwnsWaifu.level);
                    playerOwnsWaifu.level++;

                    LoadAttribute(playerOwnsWaifu);
                }
            }
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