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
            itemImg.sprite = AssetLoader.instance.ItemPass[(int)item.itemName];
            txtItem.text = item.Count.ToString();
            var btn = GetComponent<Button>();
            if(DataController.instance.playerData.userData.battlePass.LevelPass >= id)
            {
                itemClaim.SetActive(true);
                
                if (btn != null)
                {
                    btn.onClick.AddListener(() =>
                    {
                        //SetUpItemFree(item);
                        if(Checked != true)
                        {
                            ReceiveRewardGold(item);
                            btn.interactable = false;
                            Checked = true;
                            
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
                btn.interactable = false;
            }
            
            if(DataController.instance.playerData.userData.item_Receive_Count_Gold.Contains(id))
            {
                itemChecked.SetActive(true);
                itemClaim.SetActive(false);
                Checked = true;
                btn.interactable = false;
                Debug.Log("true: " + id);
            }
            else
            {
                itemChecked.SetActive(false);
                Debug.Log("false: " + id);

            }
        }
        public void SetUpItemFree(ItemPass item)
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
                        //SetUpItemFree(item);
                        if(Checked != true)
                        {
                            ReceiveRewardFree(item);
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
            
            if(DataController.instance.playerData.userData.item_Receive_Count_free.Contains(id))
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
        public void ReceiveRewardFree(ItemPass item)
        {
            
            if (!DataController.instance.playerData.userData.item_Receive_Count_free.Contains(id))
            {
                DataController.instance.playerData.userData.item_Receive_Count_free.Add(id);
            }
            itemClaim.SetActive(false);
            itemChecked.SetActive(true);
            AddItem(item);

        }
        public void ReceiveRewardGold(ItemPass item)
        {
            
            if (!DataController.instance.playerData.userData.item_Receive_Count_Gold.Contains(id))
            {
                DataController.instance.playerData.userData.item_Receive_Count_Gold.Add(id);
            }
            itemClaim.SetActive(false);
            itemChecked.SetActive(true);
            AddItem(item);
        }
        public void AddItem(ItemPass item)
        {
            switch (item.itemName)
            {
                case ItemEnum.Gold:
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

