using System;
using System.Collections;
using System.Collections.Generic;
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
        
        // public List<string> objectNamesToCheckHide;
        // public List<string> objectNamesToCheckActive;


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
        
        public void UpdateTopPanel(bool Energe, bool Gold, bool Gem)
        {
            energerObj.SetActive(Energe);
            goldObj.SetActive(Gold);
            gemObj.SetActive(Gem);
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

