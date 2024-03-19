using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.RewardPass;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.ItemPassSlots
{
    public class ItemPassSlot : MonoBehaviour
    {
        public int id;
        public Image itemImg;
        public GameObject itemChecked, itemClaim;
        public TextMeshProUGUI txtItem;
        // public Button btn;
        public bool Checked = false;

        public void SetUpItemGold(ItemPass item)
        {
            SetUpItem(item,DataController.instance.playerData.userData.item_Receive_Count_Gold);
        }
        public void SetUpItemFree(ItemPass item)
        {
            SetUpItem(item,DataController.instance.playerData.userData.item_Receive_Count_free);
            
        }
        public void SetUpItem(ItemPass item, List<int> item_Receive_Count)
        {
            itemImg.sprite = AssetLoader.instance.ItemPass[(int)item.itemName];
            txtItem.text = item.Count.ToString();
            var btns = GetComponent<Button>();
            if(DataController.instance.playerData.userData.battlePass.LevelPass >= id)
            {
                itemClaim.SetActive(true);
                
                if (btns != null)
                {
                    btns.onClick.AddListener(() =>
                    {
                        if(Checked != true)
                        {
                            if (!item_Receive_Count.Contains(id))
                            {
                                item_Receive_Count.Add(id);
                            }
                            itemClaim.SetActive(false);
                            itemChecked.SetActive(true);
                            AddItem(item);
                            

                            Checked = true;
                            btns.interactable = false;
                        }
                        else
                        {
                            Debug.Log("Đã nhận vật phẩm rồi: " + id);
                        }
                    });
                }
                
            }
            else
            {
                itemClaim.SetActive(false);
                btns.interactable = false;
            }
            
            if(item_Receive_Count.Contains(id))
            {
                itemChecked.SetActive(true);
                itemClaim.SetActive(false);
                Checked = true;
                btns.interactable = false;
                Debug.Log("true: " + id);
            }
            else
            {
                itemChecked.SetActive(false);
                Debug.Log("false: " + id);

            }

        }
        
        
        public void AddItem(ItemPass item)
        {
            switch (item.itemName)
            {
                case ItemEnum.Gold:
                    HUDController.instanse.Increase(itemImg.transform.position,item.Count,item);
                    HUDController.instanse.updateTopbarItem(item.Count,0,0,0,0);
                    break;
                case ItemEnum.Gem:
                    HUDController.instanse.updateTopbarItem(0,0,item.Count,0,0);
                    break;
                case ItemEnum.Energy_20:
                    HUDController.instanse.updateTopbarItem(0,item.Count,0,0,0);
                    break;
                case ItemEnum.Energy_50:
                    HUDController.instanse.updateTopbarItem(0,item.Count,0,0,0);
                    break;
                case ItemEnum.Ticket_Normal:
                    HUDController.instanse.updateTopbarItem(0,0,0,item.Count,0);
                    break;
                case ItemEnum.Ticket_Gold:
                    HUDController.instanse.updateTopbarItem(0,0,0,0,item.Count);
                    break;
                case ItemEnum.SmallExpPotion:
                    
                    break;
                case ItemEnum.MediumExpPotion:
                    
                    break;
                case ItemEnum.LargeExpPotion:
                    
                    break;
                case ItemEnum.UltraExpPotion:
                    
                    break;
                default:
                    break;
            }
        }
    }
}

