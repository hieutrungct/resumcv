using System.Collections.Generic;
using Pixelplacement;
using RubikCasual.Data;
using RubikCasual.RewardPass;
using TMPro;
using UnityEngine;

namespace Rubik_Casual
{
    public class HUDController : MonoBehaviour
    {
        
        public static HUDController instanse;
        public GameObject energerObj;
        public GameObject topPanel;
        public GameObject goldObj;
        public GameObject gemObj;
        public GameObject TicketObj;
        public GameObject goldProp,gemProp, energeProp;
        public Transform goldIcon, gemIcon, energeIcon;
        public ParticleSystem goldEffect, gemEffect, energeEffect;
        
        
        // public List<string> objectNamesToCheckHide;
        // public List<string> objectNamesToCheckActive;
        public TextMeshProUGUI textCoins, textGems, textEnergy, textTicket;

        public void Awake()
        {
            DontDestroyOnLoad(this);
            instanse = this;
            topPanel.SetActive(true);
            
        }
        
        // private void Update()
        // {
        //     CheckTabs();
        //     topPanel.SetActive(true);
        // }
        
        public void UpdateTopPanel(bool Energe, bool Gold, bool Gem, bool Ticket)
        {
            energerObj.SetActive(Energe);
            goldObj.SetActive(Gold);
            gemObj.SetActive(Gem);
            TicketObj.SetActive(Ticket);
        }
        public void LoadStatusNumber()
        {
            textCoins.text = DataController.instance.playerData.userData.Gold.ToString();
            textEnergy.text = DataController.instance.playerData.userData.Energy.ToString() + "/60";
            textGems.text = DataController.instance.playerData.userData.Gem.ToString();
            textTicket.text = DataController.instance.playerData.userData.Ticket.ToString();
        }
        public async void Increase(Vector3 spawnPos, int quantity_Received, ItemPass item)
        {
            GameObject gameObject;
            Transform transformObj;
            ParticleSystem particleSystem;
            switch (item.itemName)
            {
                case ItemEnum.Gold:
                    gameObject = goldProp;
                    transformObj = goldIcon;
                    particleSystem = goldEffect;
                    break;
                case ItemEnum.Gem:
                    gameObject = gemProp;
                    transformObj = gemIcon;
                    particleSystem = gemEffect;
                    break;
                case ItemEnum.Energy_20:
                    gameObject = energeProp;
                    transformObj = energeIcon;
                    particleSystem = energeEffect;
                    break;
                default:
                    gameObject = goldProp;
                    transformObj = goldIcon;
                    particleSystem = goldEffect;
                    break;
            }
            int[] items = new int[5];
            int averange = quantity_Received / 5;
            for (int i = 0; i < 5; i++)
                items[i] = averange;
            items[4] = quantity_Received - averange * 4;
            for (int i = 0; i < 5; i++)
            {
                GameObject Gameobj = Instantiate(gameObject, spawnPos + new Vector3(0, 0, -0.1f), Quaternion.identity);
                Vector3 target = Gameobj.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);//generate a random pos
                int reward = items[i];
                Tween.Position(Gameobj.transform, target, 0.5f + i * 0.06f, 0, Tween.EaseOutStrong);
                Tween.Position(Gameobj.transform, transformObj.position, 0.25f + i * 0.06f+0.3f, 0.5f + i * 0.06f, Tween.EaseInStrong, Tween.LoopType.None, null, () =>
                {
                    // UpdateCoin(reward);
                    UpdateTopBar(item,reward);
                    Destroy(Gameobj);
                });
            }
            float timeStamp = Time.time;
            while (Time.time - timeStamp < 1)
                await System.Threading.Tasks.Task.Yield();
            particleSystem.Play();
            //DataController.Instance.gameData.gold += totalGold;
            //DataController.Instance.SaveData();
        }


/// <summary>
/// tang item cho user, refresh status bar
/// </summary>
/// <param name="coin"></param>
/// <param name="enegy"></param>
/// <param name="gem"></param>
/// <param name="ticket_N"></param>
/// <param name="ticket_G"></param> 
        public void UpdateTopbarItem(double coin, int enegy, double gem, int ticket_N, int ticket_G)
        {
            DataController.instance.playerData.userData.Gold += coin;
            DataController.instance.playerData.userData.Gem += gem;
            DataController.instance.playerData.userData.Energy += enegy;
            DataController.instance.playerData.userData.Ticket += ticket_N;
            DataController.instance.playerData.userData.Ticket += ticket_G;
            DataController.instance.SaveUserDataToJson();
            LoadStatusNumber();
        }
        public void UpdateTopBar(ItemPass item, int value)
        {
            switch (item.itemName)
            {
                case ItemEnum.Gold:
                    UpdateTopbarItem(value,0,0,0,0);
                    break;
                case ItemEnum.Gem:
                    UpdateTopbarItem(0,0,value,0,0);
                    break;
                case ItemEnum.Energy_20:
                    UpdateTopbarItem(0,value,0,0,0);
                    break;
                case ItemEnum.Energy_50:
                    UpdateTopbarItem(0,value,0,0,0);
                    break;
                case ItemEnum.Ticket_Normal:
                    UpdateTopbarItem(0,0,0,value,0);
                    break;
                case ItemEnum.Ticket_Gold:
                    UpdateTopbarItem(0,0,0,0,value);
                    break;
                case ItemEnum.SmallExpPotion:
                    
                    break;
                case ItemEnum.MediumExpPotion:
                    
                    break;
                case ItemEnum.LargeExpPotion:
                    
                    break;
                case ItemEnum.UltraExpPotion:
                    
                    break;
                default:
                    break;
            }
        }

        
    }
}

