using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RubikCasual.Battle.Inventory;
using RubikCasual.Data;
using RubikCasual.Lobby;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle.UI
{
    public class InventorryUIPanel : MonoBehaviour
    {
        public List<GameObject> lsSlotInventory;
        public GameObject itemInventory; 
        public static InventorryUIPanel instance;
        void Awake()
        {

            instance = this;
        }
        void Start()
        {
            BtnTestInventory();
        }
        [Button]
        void BtnTestInventory()
        {
            for (int i = 0; i < lsSlotInventory.Count; i++)
            {
                int index = i;
                lsSlotInventory[i].GetComponent<Button>().onClick.RemoveAllListeners();
                lsSlotInventory[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    int rand = UnityEngine.Random.Range(0, 5);
                    CreateSlotInventory(index, itemInventory.GetComponent<SlotInventory>().lsIdItem[rand]);
                });
            }
        }
        public void CreateSlotInventory(int idSlot, int idItem)
        {
            for (int i = 0; i < lsSlotInventory.Count; i++)
            {
                if (idSlot == i)
                {
                    GameObject ItemClone = Instantiate(itemInventory, lsSlotInventory[i].gameObject.transform);
                    SlotInventory Item = ItemClone.GetComponent<SlotInventory>();
                    Item.idItem = idItem;
                    Item.IdSlot = idSlot;
                    Item.Icon.GetComponent<Image>().sprite = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).imageItem;
                    ItemClone.GetComponent<ItemDragPosition>().idItem = idItem;
                }
            }
        }
    }


}
