using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubikCasual.Battle.Inventory
{
    public class ItemDragPosition : MonoBehaviour
    {
        public Vector2 oriPos;
        void Start()
        {
            oriPos = transform.position;
            //charAnim = GetComponent<SkeletonAnimation>();
        }
        public void OnMouseDrag()
        {
            if (BattleController.instance.gameState != GameState.WAIT_BATTLE)
            {
                gameObject.transform.position = oriPos;
                GetComponent<MeshRenderer>().sortingOrder = 10;
                GetComponent<MeshRenderer>().sortingLayerName = "Item";
                return;
            }

            //Debug.Log("Draw");

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
            GetComponent<MeshRenderer>().sortingOrder = 100;
            GetComponent<MeshRenderer>().sortingLayerName = "Item";
            // int temp = GameControl.instance.CheckNearPos(mousePosition);

            // if (temp == -1 || GameControl.Instance.gameState == GameState.BATTLE)
            // {
            //     // GameControl.Instance.HeroPos[temp].posImage.sprite = SpriteHelper.Instance.lsPosSprite[1];
            //     GameControl.Instance.HightLightOff();
            // }
            // else
            // {
            //     GameControl.Instance.HightLightOff();
            //     GameControl.Instance.HightlightInPos(temp, SpriteHelper.Instance.lsPosSprite[1]);
            // }
        }
    }

}