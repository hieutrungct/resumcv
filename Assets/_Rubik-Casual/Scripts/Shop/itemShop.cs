using System.Collections;
using System.Collections.Generic;
using RubikCasual.RewardPass;
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
        void Start()
        {
            SetUpItem();
        }
        public void SetUpItem()
        {
            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    
                    if(itemType == shopItems.normalItem)
                    {
                        ShopController.instance.OnClickItemNormal(quantity,purchaseCost);
                    }
                    else if(itemType == shopItems.freeItem)
                    {
                        ShopController.instance.OnClickItemFree(quantity,purchaseCost);
                    }
                    else
                    {
                        ShopController.instance.OnClickTopUpItem(quantity,purchaseCost);
                        Debug.Log("Nạp tiền đi bạn ơi :) ");
                    }
                });
            }
        }
        
    }

}
