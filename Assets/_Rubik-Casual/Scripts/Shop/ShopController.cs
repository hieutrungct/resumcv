using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.RewardPass;
using UnityEngine;
namespace RubikCasual.Shop
{
    public class ShopController : MonoBehaviour
    {
        public static ShopController instance;
        void Awake()
        {
            instance = this;
        }
        public void OnClickItemNormal(int quantity,double purchaseCost, GameObject itemObj, ItemPass itemtype)
        {
            Debug.Log("bạn đã mất " + purchaseCost + " Gem");
            if(DataController.instance.playerData.userData.Gem >= purchaseCost)
            {
                DataController.instance.playerData.userData.Gem -= purchaseCost;
                HUDController.instanse.LoadStatusNumber();
                HUDController.instanse.Increase(itemObj.transform.position, quantity, itemtype);
            }
            else
            {
                Debug.Log("bạn không đủ Gem");
            }
            
        }
        public void OnClickItemFree(int quantity,double purchaseCost, GameObject itemObj, ItemPass itemtype)
        {
            HUDController.instanse.Increase(itemObj.transform.position, quantity, itemtype);
            Debug.Log("bạn đã nhận free item");
        }
        public void OnClickTopUpItem(int quantity,double purchaseCost, GameObject itemObj, ItemPass itemtype)
        {
            Debug.Log("bạn cần nạp tiền để nhận");
        }
    }
}

