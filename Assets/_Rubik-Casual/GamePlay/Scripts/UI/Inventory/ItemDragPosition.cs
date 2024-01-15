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
using RubikCasual.Data;
using Spine.Unity;
using UnityEngine.UI;
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
            if (BattleController.instance.gameState != GameState.WAIT_BATTLE)
            {
                GetComponent<SlotInventory>().Icon.transform.DOComplete();
                gameObject.transform.position = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;
                GetComponent<SlotInventory>().Icon.transform.DOPunchScale(new Vector3(valueAnother, valueAnother, valueAnother), duration);
                return;
            }

            GetComponent<SlotInventory>().Icon.transform.DOComplete();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            oriPos = inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform.position;

            gameObject.transform.SetParent(inventorryUI.lsSlotInventory[gameObject.GetComponent<SlotInventory>().IdSlot].transform);

            int indexCheckHero = GameControl.instance.CheckItemNearPosHero(mousePosition);
            if (indexCheckHero == -1)
            {
                int indexCheckEnemy = GameControl.instance.CheckItemNearPosEnemy(mousePosition);

                if (indexCheckEnemy != -1 &&
                    BattleController.instance.lsSlotGbEnemy[indexCheckEnemy] != null &&
                    DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type == RubikCasual.DailyItem.TypeItem.Poison &&
                    BattleController.instance.lsSlotGbEnemy[indexCheckEnemy].GetComponent<CharacterInBattle>() != null
                    )
                {

                    CharacterInBattle EnemyInBattle = BattleController.instance.lsSlotGbEnemy[indexCheckEnemy].GetComponent<CharacterInBattle>();
                    SkeletonAnimation enemy = EnemyInBattle.skeletonCharacterAnimation;

                    Calculator.CheckItemCalculate(idItem, EnemyInBattle);

                    if (EnemyInBattle.HpNow == 0)
                    {
                        enemy.AnimationName = NameAnim.Anim_Character_Die;
                        enemy.AnimationState.SetAnimation(0, NameAnim.Anim_Character_Die, false);
                        enemy.AnimationState.Complete += delegate
                        {
                            EnemyInBattle.healthBar.gameObject.transform.SetParent(EnemyInBattle.cooldownAttackBar.transform.parent);
                            Destroy(EnemyInBattle.gameObject);
                        };
                    }
                    transform.parent.Find("IconPlus" + GetComponent<SlotInventory>().IdSlot).gameObject.SetActive(true);
                    StartCoroutine(MovePopup.ShowTxtDame(gameObject, UIGamePlay.instance.TxtDame, mousePosition, DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
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
                if (BattleController.instance.lsSlotGbHero[indexCheckHero] != null &&
                    DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type != DailyItem.TypeItem.Poison
                )
                {
                    Calculator.CheckItemCalculate(idItem, BattleController.instance.lsSlotGbHero[indexCheckHero].GetComponent<CharacterInBattle>());
                    transform.parent.Find("IconPlus" + GetComponent<SlotInventory>().IdSlot).gameObject.SetActive(true);
                    StartCoroutine(MovePopup.ShowTxtDame(gameObject, UIGamePlay.instance.TxtDame, mousePosition, DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));

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

}