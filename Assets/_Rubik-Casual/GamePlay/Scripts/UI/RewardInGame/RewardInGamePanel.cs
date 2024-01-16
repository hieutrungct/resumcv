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
            dataController.BtnSaveUserDataToJson();
        }
        public void AddRewardTopBarGroup(float attributeReward)
        {
            float coins = 1f, gems = 1f;
            valueCoins += (coins * attributeReward);
            valueGems += (gems * attributeReward);
            txtCoins.text = valueCoins.ToString();
            txtGems.text = valueGems.ToString();
        }
    }
}
