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
        public double count;
        public bool check = false;
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
            if(check == false)
            {
                count = purchaseCost;
                check = true;
            }
            if(count == 0)
            {
                HUDController.instanse.Increase(itemObj.transform.position, quantity, itemtype);
                check = false;
            }
            else
            {
                count--;
                Debug.Log("bạn cần coi " + count + " quảng cáo nữa mới được nhận");
            }
            
        }
        public void OnClickTopUpItem(int quantity,double purchaseCost, GameObject itemObj, ItemPass itemtype)
        {
            Debug.Log("bạn cần nạp tiền để nhận");
        }
    }
}

