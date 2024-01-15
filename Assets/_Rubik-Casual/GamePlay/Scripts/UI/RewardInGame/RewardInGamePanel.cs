using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle;
using RubikCasual.Data;
using TMPro;
using UnityEngine;

namespace RubikCasual.RewardInGame
{
    public class RewardInGamePanel : MonoBehaviour
    {
        public TextMeshProUGUI txtCoins, txtGems;
        float valueCoins = 1f, valueGems = 1f;
        private DataController dataController;
        public static RewardInGamePanel instance;
        void Start()
        {
            instance = this;
            dataController = DataController.instance;
        }
        public void SaveRewardInGame()
        {
            dataController.playerData.userData.Gold += valueCoins;
            dataController.playerData.userData.Gem += valueGems;
            dataController.BtnSavePlayerData();
        }
        public void AddRewardTopBarGroup(float attributeReward)
        {
            float coins = 1f * attributeReward;
            valueCoins += coins;
            txtCoins.text = valueCoins.ToString();

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                float gems = 1f * attributeReward;
                valueGems += gems;
                txtGems.text = valueGems.ToString();
            }
        }
    }
}
