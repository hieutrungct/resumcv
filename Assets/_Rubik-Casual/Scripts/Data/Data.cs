using System.Collections;
using System.Collections.Generic;
using RubikCasual.Character_ACC;
using RubikCasual.DailyItem;
using RubikCasual.Lobby;
using UnityEngine;

namespace RubikCasual.Data
{
    public class Data : MonoBehaviour
    {
        public PlayerData playerData;
        public ItemData itemData;
        public CharacterAssets characterAssets;
        
        public static Data instance;
        void Start()
        {
            instance = this;
            DontDestroyOnLoad(this);
            StartCoroutine(LoadData());
        }
        IEnumerator LoadData()
        {
            yield return new WaitForSeconds(0.25f);
            playerData = PlayerData.instance;
            characterAssets = CharacterAssets.instance;
        }


    }
}
