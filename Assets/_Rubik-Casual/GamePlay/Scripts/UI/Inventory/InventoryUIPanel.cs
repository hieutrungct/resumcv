using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RubikCasual.Battle.Inventory;
using RubikCasual.Data;
using RubikCasual.Lobby;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle.UI
{
    public class InventoryUIPanel : MonoBehaviour
    {
        public List<GameObject> lsSlotInventory;
        public GameObject itemInventory;
        Vector3 OriginPos;
        public Button btnIconInventory;
        public float duration = 0.25f;
        bool isHidePopup;
        public GameObject gbInventory;
        public static InventoryUIPanel instance;
        void Awake()
        {
            instance = this;
            gbInventory = lsSlotInventory[0].transform.parent.parent.gameObject;
            OriginPos = gbInventory.transform.position;
            btnIconInventory.onClick.AddListener(ShowAndHideInventory);

            ShowAndHideInventory();
        }


        public void ShowAndHideInventory()
        {
            if (!isHidePopup)
            {
                gbInventory.transform.DOMoveX(OriginPos.x - 20f, duration);
            }
            else
            {
                gbInventory.transform.DOMoveX(OriginPos.x, duration);
            }
            isHidePopup = !isHidePopup;
        }
        // [Button]
        // void BtnTestInventory()
        // {
        //     for (int i = 0; i < lsSlotInventory.Count; i++)
        //     {
        //         int index = i;
        //         lsSlotInventory[i].GetComponent<Button>().onClick.RemoveAllListeners();
        //         lsSlotInventory[i].GetComponent<Button>().onClick.AddListener(() =>
        //         {
        //             int rand = UnityEngine.Random.Range(0, 5);
        //             CreateSlotInventory(index, itemInventory.GetComponent<SlotInventory>().lsIdItem[rand]);
        //         });
        //     }
        // }

        public int GetSlotInventory()
        {
            int resultSlot = -1;
            for (int i = 0; i < lsSlotInventory.Count; i++)
            {
                if (lsSlotInventory[i].GetComponentsInChildren<SlotInventory>().Length == 0)
                {
                    resultSlot = i;
                    return resultSlot;
                }
            }
            return resultSlot;
        }
        public Transform GetTransformSlotInventory(int idSlot)
        {
            Transform ResultTranform = lsSlotInventory[idSlot].gameObject.transform;
            return ResultTranform;
        }
        public SlotInventory CreateSlotInventory(int idSlot, int idItem)
        {
            SlotInventory ResultItem = null;
            for (int i = 0; i < lsSlotInventory.Count; i++)
            {
                if (idSlot == i)
                {
                    GameObject ItemClone = Instantiate(itemInventory, lsSlotInventory[i].gameObject.transform);
                    SlotInventory Item = ItemClone.GetComponent<SlotInventory>();
                    Item.idItem = idItem;
                    Item.IdSlot = idSlot;
                    Item.Icon.GetComponent<Image>().sprite = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).imageItem;
                    Item.Icon.GetComponent<Image>().preserveAspect = true;
                    ItemClone.GetComponent<ItemDragPosition>().idItem = idItem;
                    ResultItem = Item;
                }
            }
            return ResultItem;
        }
        public void CreateItemInInventory(GameObject gbItem, int idItem)
        {
            int idSlot = GetSlotInventory();
            if (idSlot == -1)
            {
                Destroy(gbItem);
                return;
            }
            gbItem.transform.SetParent(GameObject.Find("CanvasUI").transform);

            SlotInventory itemClone = CreateSlotInventory(idSlot, idItem);
            itemClone.Icon.SetActive(false);

            Tween itemMoveInInventory = gbItem.transform.DOMove(GetTransformSlotInventory(idSlot).position, 0.5f);
            itemMoveInInventory.OnComplete(() =>
                                        {
                                            GameObject.Find("IconPlus" + itemClone.IdSlot).SetActive(false);
                                            itemClone.Icon.SetActive(true);
                                            Destroy(gbItem);
                                        });
        }
    }


}
