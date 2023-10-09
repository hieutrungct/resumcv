using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik_Casual
{
    public class TopPanelManager : MonoBehaviour
    {
        public GameObject energer;
        public GameObject topPanel;
        public GameObject gold;
        public GameObject gem;
        

        public List<string> objectNamesToCheckHide;
        public List<string> objectNamesToCheckActive;

        private void Update()
        {
            CheckTabs();
            topPanel.SetActive(true);
        }

        public void CheckTabs()
        {
            bool hideTopPanel = false;
            bool hideEnerge = true;

            
            foreach (string objectName in objectNamesToCheckHide)
            {
                GameObject obj = GameObject.Find(objectName);
                if (obj != null && obj.activeSelf == true)
                {
                    hideTopPanel = true;
                    hideEnerge = false;
                    break; 
                }
            }

            
            foreach (string objectName1 in objectNamesToCheckActive)
            {
                GameObject objGameObject = GameObject.Find(objectName1);
                if (objGameObject != null && objGameObject.activeSelf == true)
                {
                    hideTopPanel = false; 
                    hideEnerge = false;
                    break; 
                }
            }
            
            topPanel.SetActive(hideTopPanel);
            energer.SetActive(hideEnerge);
            gold.SetActive(!hideTopPanel);
            gem.SetActive(!hideTopPanel);
        }
    }
}