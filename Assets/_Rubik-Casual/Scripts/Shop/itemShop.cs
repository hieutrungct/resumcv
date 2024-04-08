using System.Collections;
using System.Collections.Generic;
using RubikCasual.RewardPass;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.Shop
{
    public enum shopItems
    {
        normalItem = 0,
        freeItem = 1,
        TopUpItem = 2
    }
    public class itemShop : MonoBehaviour
    {
        public ItemEnum itemEnum;
        public double quantity, purchaseCost;
        public shopItems itemType;
        public ItemPass itemtype;
        void Start()
        {
            SetUpItem();
        }
        
        public void SetUpItem()
        {
            itemtype.itemName = itemEnum;
            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    
                    if(itemType == shopItems.normalItem)
                    {
                        ShopController.instance.OnClickItemNormal((int)quantity,purchaseCost,gameObject, itemtype);
                    }
                    else if(itemType == shopItems.freeItem)
                    {
                        ShopController.instance.OnClickItemFree((int)quantity,purchaseCost,gameObject, itemtype);
                    }
                    else
                    {
                        ShopController.instance.OnClickTopUpItem((int)quantity,purchaseCost,gameObject, itemtype);
                        // Debug.Log("Nạp tiền đi bạn ơi :) ");
                    }
                });
            }
        }
        
    }

}
