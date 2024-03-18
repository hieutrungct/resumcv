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


/// <summary>
/// tang item cho user, refresh status bar
/// </summary>
/// <param name="coin"></param>
/// <param name="enegy"></param>
/// <param name="gem"></param>
/// <param name="ticket_N"></param>
/// <param name="ticket_G"></param> 
        public void updateTopbarItem(double coin, int enegy, double gem, int ticket_N, int ticket_G)
        {
            DataController.instance.playerData.userData.Gold += coin;
            DataController.instance.playerData.userData.Gem += gem;
            DataController.instance.playerData.userData.Energy += enegy;
            DataController.instance.playerData.userData.Ticket += ticket_N;
            DataController.instance.playerData.userData.Ticket += ticket_G;
            LoadStatusNumber();
        }

            
           
        // public void CheckTabs(bool hideTopPanel,bool hideEnerge)
        // {
        //     
        //     //hideTopPanel = false;
        //     //hideEnerge = true;
        //
        //     
        //     foreach (string objectName in objectNamesToCheckHide)
        //     {
        //         GameObject obj = GameObject.Find(objectName);
        //         if (obj != null && obj.activeSelf == true)
        //         {
        //             hideTopPanel = true;
        //             hideEnerge = false;
        //             break; 
        //         }
        //     }
        //
        //     
        //     foreach (string objectName1 in objectNamesToCheckActive)
        //     {
        //         GameObject objGameObject = GameObject.Find(objectName1);
        //         if (objGameObject != null && objGameObject.activeSelf == true)
        //         {
        //             hideTopPanel = false; 
        //             hideEnerge = false;
        //             break; 
        //         }
        //     }
        //     
        //     topPanel.SetActive(hideTopPanel);
        //     energer.SetActive(hideEnerge);
        //     gold.SetActive(!hideTopPanel);
        //     gem.SetActive(!hideTopPanel);
        // }
    }
}

