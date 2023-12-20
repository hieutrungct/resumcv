using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RubikCasual.Battle.Calculate;
using RubikCasual.Battle.UI;
using RubikCasual.Lobby;
using TMPro;
using UnityEngine;
using DG.Tweening;
using RubikCasual.Tool;
namespace RubikCasual.Battle.Inventory
{
    public class ItemDragPosition : MonoBehaviour
    {
        public Vector2 oriPos;
        public int idItem;
        float duration = 0.5f, valueAnother = 0.1f;
        public InventorryUIPanel inventorryUI;
        void Start()
        {
            inventorryUI = InventorryUIPanel.instance;
        }
        public void OnMouseDrag()
        {
            if (BattleController.instance.gameState != GameState.WAIT_BATTLE)
            {
                GetComponent<SlotInventory>().Icon.transform.DOComplete();
                gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
                GetComponent<SlotInventory>().Icon.transform.DOPunchScale(new Vector3(valueAnother, valueAnother, valueAnother), duration);
                return;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
            gameObject.transform.SetParent(inventorryUI.lsSlotInventory[inventorryUI.lsSlotInventory.Count - 1].transform);
            // int temp = GameControl.instance.CheckNearPos(mousePosition);
        }

        public void OnMouseUp()
        {
            GetComponent<SlotInventory>().Icon.transform.DOComplete();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            oriPos = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;

            gameObject.transform.SetParent(inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform);

            int indexCheckHero = GameControl.instance.CheckItemNearPosHero(mousePosition);
            if (indexCheckHero == -1)
            {
                int indexCheckEnemy = GameControl.instance.CheckItemNearPosEnemy(mousePosition);
                if (indexCheckEnemy == -1)
                {
                    gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
                    GetComponent<SlotInventory>().Icon.transform.DOPunchScale(new Vector3(valueAnother, valueAnother, valueAnother), duration);
                    return;
                }
                else
                {
                    if (BattleController.instance.lsSlotGbEnemy[indexCheckEnemy] != null)
                    {
                        if (UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString() != "Heal")
                        {
                            Calculator.CheckItemCalculate(idItem, BattleController.instance.lsSlotGbEnemy[indexCheckEnemy].GetComponent<CharacterInBattle>());
                            StartCoroutine(MovePopup.ShowTxtDame(gameObject, UIGamePlay.instance.TxtDame, mousePosition, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
                        }
                        else
                        {
                            gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
                            GetComponent<SlotInventory>().Icon.transform.DOPunchScale(new Vector3(valueAnother, valueAnother, valueAnother), duration);
                            return;
                        }

                    }
                }
            }
            else
            {
                if (BattleController.instance.lsSlotGbHero[indexCheckHero] != null)
                {
                    if (UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString() != "Poison")
                    {
                        Calculator.CheckItemCalculate(idItem, BattleController.instance.lsSlotGbHero[indexCheckHero].GetComponent<CharacterInBattle>());
                        StartCoroutine(MovePopup.ShowTxtDame(gameObject, UIGamePlay.instance.TxtDame, mousePosition, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
                    }
                    else
                    {
                        gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
                        GetComponent<SlotInventory>().Icon.transform.DOPunchScale(new Vector3(valueAnother, valueAnother, valueAnother), duration);
                        return;
                    }

                }
                else
                {
                    gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
                    GetComponent<SlotInventory>().Icon.transform.DOPunchScale(new Vector3(valueAnother, valueAnother, valueAnother), duration);

                }
            }
        }


    }

}