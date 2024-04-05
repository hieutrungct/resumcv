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
        public void OnClickItemNormal(double quantity,double purchaseCost, GameObject itemObj, ItemPass itemtype)
        {
            DataController.instance.playerData.userData.Gem -= purchaseCost;
            // HUDController.instanse.Increase(itemObj.transform.position, quantity, itemtype);
        }
        public void OnClickItemFree(double quantity,double purchaseCost)
        {
            
        }
        public void OnClickTopUpItem(double quantity,double purchaseCost)
        {
            
        }
    }
}

