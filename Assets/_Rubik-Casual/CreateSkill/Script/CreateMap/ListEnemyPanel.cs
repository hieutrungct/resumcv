using System;
using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Waifu;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.CreateSkill.Panel
{
    public class ListEnemyPanel : MonoBehaviour
    {
        public GameObject SlotEnemy;
        public EnemyAssets enemyAssets;
        public List<GameObject> lsGbEnemy = new List<GameObject>();
        public Transform transParentLsEnemy;
        public DataController dataController;
        public static bool isFocusEnemy;
        void Start()
        {
            enemyAssets = dataController.characterAssets.enemyAssets;
            CreateListEnemy();
        }

        void CreateListEnemy()
        {
            if (lsGbEnemy.Count != 0)
            {
                foreach (var enemy in lsGbEnemy)
                {
                    Destroy(enemy);
                }
                lsGbEnemy.Clear();
            }

            foreach (var idEnemy in enemyAssets.lsIdEnemy)
            {
                GameObject gbSlotClone = Instantiate(SlotEnemy, transParentLsEnemy);
                gbSlotClone.SetActive(true);
                gbSlotClone.GetComponent<Image>().sprite = dataController.listImage.GetAvatarEnemyByIndex(idEnemy.ToString());
                gbSlotClone.AddComponent<BoxCollider2D>().size = new Vector2(200f, 200f);

                DragEnemyForSlot dragEnemyForSlot = gbSlotClone.AddComponent<DragEnemyForSlot>();
                dragEnemyForSlot.index = idEnemy;
                dragEnemyForSlot.stateInfoWithSlot = "IndexCreep: " + idEnemy;
                lsGbEnemy.Add(gbSlotClone);
            }
        }
    }
    [Serializable]
    public class DragEnemyForSlot : MonoBehaviour
    {
        public int index;
        public string stateInfoWithSlot;
        public GameObject focus;
        public bool isMouseFocus, isBoss;
        void Awake()
        {
            focus = this.transform.Find("Focus").gameObject;

        }

        void Update()
        {
            if (!ListEnemyPanel.isFocusEnemy && !isMouseFocus)
            {
                focus.SetActive(false);
            }
        }
        void OnMouseEnter()
        {
            isMouseFocus = true;
        }
        void OnMouseExit()
        {
            isMouseFocus = false;
        }
        void OnMouseDown()
        {
            InfoEnemyPanel.instance.SetInfoPanel(index);
            isBoss = DataController.instance.characterAssets.enemyAssets.GetWaifuSOEByIndex(index.ToString()).Is_Boss;
            if (!focus.activeSelf)
            {
                ListEnemyPanel.isFocusEnemy = false;
                focus.SetActive(true);
                CreateMapController.instance.itemWithSlot = this.gameObject;
            }
        }
        private void OnMouseUp()
        {
            ListEnemyPanel.isFocusEnemy = true;
        }
    }
}
