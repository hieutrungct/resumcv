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

        public void SetUpItemGold()
        {
            
        }
        public void SetUpItemFree(ItemPass item)
        {
            if(id == DataController.instance.playerData.userData.item_Receive_Count)
            {
                itemChecked.SetActive(true);
            }
            else
            {
                itemChecked.SetActive(false);
            }
            if(DataController.instance.playerData.battlePass.LevelPass >= id)
            {
                itemClaim.SetActive(true);
            }
            else
            {
                itemClaim.SetActive(false);
            }
            itemImg.sprite = AssetLoader.instance.ItemPass[(int)item.itemName];
            txtItem.text = item.Count.ToString();
            
        }
    }
}

