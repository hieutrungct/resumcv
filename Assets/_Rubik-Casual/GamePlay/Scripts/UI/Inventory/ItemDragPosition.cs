using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RubikCasual.Battle.Calculate;
using RubikCasual.Battle.UI;
using RubikCasual.Lobby;
using TMPro;
using UnityEngine;
using DG.Tweening;
namespace RubikCasual.Battle.Inventory
{
    public class ItemDragPosition : MonoBehaviour
    {
        public Vector2 oriPos;
        public int idItem;
        public InventorryUIPanel inventorryUI;
        void Start()
        {
            inventorryUI = InventorryUIPanel.instance;
        }
        public void OnMouseDrag()
        {
            if (BattleController.instance.gameState != GameState.WAIT_BATTLE)
            {
                gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
                return;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
            gameObject.transform.SetParent(inventorryUI.lsSlotInventory[inventorryUI.lsSlotInventory.Count - 1].transform);
            // int temp = GameControl.instance.CheckNearPos(mousePosition);
        }
        public void OnMouseUp()
        {
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
                    return;
                }
                else
                {
                    if (BattleController.instance.lsSlotGbEnemy[indexCheckEnemy] != null)
                    {
                        if (UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString() != "Heal")
                        {
                            Calculator.CheckItemCalculate(idItem, BattleController.instance.lsSlotGbEnemy[indexCheckEnemy].GetComponent<CharacterInBattle>());
                            StartCoroutine(ShowTxtDame(gameObject, UIGamePlay.instance.TxtDame, mousePosition, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
                        }
                        else
                        {
                            gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
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
                        StartCoroutine(ShowTxtDame(gameObject, UIGamePlay.instance.TxtDame, mousePosition, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, UserData.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
                    }
                    else
                    {
                        gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
                        return;
                    }

                }
            }
        }
        float duration = 0.5f, valueMoveTxtDame = 0.5f;
        IEnumerator ShowTxtDame(GameObject gb, GameObject gbTxtDame, Vector3 posTxtDame, float DameItem, string typeItem)
        {
            switch (typeItem)
            {
                case "Heal":
                    GameObject txtDame = Instantiate(gbTxtDame, gb.transform);
                    txtDame.transform.position = new Vector2(posTxtDame.x, posTxtDame.y);
                    txtDame.GetComponent<TextMeshProUGUI>().text = "+" + DameItem.ToString();
                    txtDame.GetComponent<TextMeshProUGUI>().color = Color.green;
                    txtDame.transform.DOMoveY(valueMoveTxtDame * 1.1f, duration);
                    gb.GetComponent<SlotInventory>().Icon.GetComponent<UnityEngine.UI.Image>().enabled = false;
                    yield return new WaitForSeconds(duration);
                    Destroy(txtDame);
                    Destroy(gb);
                    break;

                case "Poison":
                    GameObject txtDame2 = Instantiate(gbTxtDame, gb.transform);
                    txtDame2.transform.position = new Vector2(posTxtDame.x, posTxtDame.y);
                    txtDame2.GetComponent<TextMeshProUGUI>().text = "-" + DameItem.ToString();
                    txtDame2.GetComponent<TextMeshProUGUI>().color = Color.red;
                    txtDame2.transform.DOMoveY(valueMoveTxtDame, duration);
                    gb.GetComponent<SlotInventory>().Icon.GetComponent<UnityEngine.UI.Image>().enabled = false;
                    yield return new WaitForSeconds(duration);
                    Destroy(txtDame2);
                    Destroy(gb);
                    break;
            }
        }
    }

}