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
            if(DataController.instance.playerData.userData.battlePass.LevelPass >= id)
            {
                itemClaim.SetActive(true);
                var btns = GetComponent<Button>();
                if (btns != null)
                {
                    btns.onClick.AddListener(() =>
                    {
                        //SetUpItemFree(item);
                        if(Checked != true)
                        {
                            ReceiveRewardGold(item);
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
            }
            
            if(DataController.instance.playerData.userData.item_Receive_Count_Gold.Contains(id))
            {
                itemChecked.SetActive(true);
                itemClaim.SetActive(false);
                Checked = true;
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
            if(DataController.instance.playerData.userData.battlePass.LevelPass >= id)
            {
                itemClaim.SetActive(true);
                var btns = GetComponent<Button>();
                if (btns != null)
                {
                    btns.onClick.AddListener(() =>
                    {
                        //SetUpItemFree(item);
                        if(Checked != true)
                        {
                            ReceiveRewardFree(item);
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
            }
            
            if(DataController.instance.playerData.userData.item_Receive_Count_free.Contains(id))
            {
                itemChecked.SetActive(true);
                itemClaim.SetActive(false);
                Checked = true;
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
                    DataController.instance.playerData.userData.Gold += item.Count;
                    break;
                case ItemEnum.Gem:
                    DataController.instance.playerData.userData.Gem += item.Count;
                    break;
                case ItemEnum.Energy_20:
                    DataController.instance.playerData.userData.Energy += item.Count;
                    break;
                case ItemEnum.Energy_50:
                    DataController.instance.playerData.userData.Energy += item.Count;
                    break;
                case ItemEnum.Ticket_Normal:
                    DataController.instance.playerData.userData.Ticket += item.Count;
                    break;
                case ItemEnum.Ticket_Gold:
                    DataController.instance.playerData.userData.Ticket += item.Count;
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

