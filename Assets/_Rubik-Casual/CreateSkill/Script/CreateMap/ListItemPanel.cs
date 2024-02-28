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
    public class ListItemPanel : MonoBehaviour
    {
        public GameObject SlotEnemy;
        public List<GameObject> lsGbItem = new List<GameObject>();
        public Transform transParentLsEnemy;
        public DataController dataController;
        public static bool isFocusItem;
        void Start()
        {
            CreateListEnemy();
        }

        void CreateListEnemy()
        {
            if (lsGbItem.Count != 0)
            {
                foreach (var enemy in lsGbItem)
                {
                    Destroy(enemy);
                }
                lsGbItem.Clear();
            }

            foreach (var infoItem in dataController.itemData.InfoItems)
            {
                if (infoItem.type != DailyItem.TypeItem.None)
                {
                    GameObject gbSlotClone = Instantiate(SlotEnemy, transParentLsEnemy);
                    gbSlotClone.SetActive(true);
                    gbSlotClone.GetComponent<Image>().sprite = infoItem.imageItem;
                    gbSlotClone.GetComponent<Image>().preserveAspect = true;
                    gbSlotClone.AddComponent<BoxCollider2D>().size = new Vector2(200f, 200f);

                    DragItemForSlot dragItemForSlot = gbSlotClone.AddComponent<DragItemForSlot>();
                    dragItemForSlot.index = infoItem.id;
                    dragItemForSlot.stateInfoWithSlot = "IndexItem: " + infoItem.id;
                    lsGbItem.Add(gbSlotClone);
                }
            }
        }
    }
    [Serializable]
    public class DragItemForSlot : MonoBehaviour
    {
        public int index;
        public string stateInfoWithSlot;
        public GameObject focus;
        public bool isMouseFocus;
        void Awake()
        {
            focus = this.transform.Find("Focus").gameObject;
        }
        void Update()
        {
            if (!ListItemPanel.isFocusItem && !isMouseFocus)
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
            InfoEnemyPanel.instance.SetInfoItemPanel(index - 1);
            if (!focus.activeSelf)
            {
                ListItemPanel.isFocusItem = false;
                focus.SetActive(true);
                CreateMapController.instance.itemWithSlot = this.gameObject;
            }
        }
        private void OnMouseUp()
        {
            ListItemPanel.isFocusItem = true;
        }
    }
}
