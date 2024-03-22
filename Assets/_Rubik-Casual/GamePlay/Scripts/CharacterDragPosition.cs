using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.EventSystems;
using RubikCasual.Battle;
using RubikCasual.Battle.UI;

namespace RubikCasual.Battle
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CharacterDragPosition : MonoBehaviour
    {
        // Start is called before the first frame update
        public SkeletonAnimation CharacterSke;
        public Transform posCharacter;
        public Vector2 oriPos, shopPos;

        private bool dragging = false;
        public SkeletonAnimation charAnim;
        public int indexOfCharacter;
        // public CardCharacter thisCardCharacter;
        public bool isInDeck = false;
        public int star, oriIndex;
        public string idChar, _ID;
        public List<GameObject> lsStars = new List<GameObject>();
        // public ChessData_AAC chessInfo;
        void Start()
        {
            oriPos = transform.position;
            this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 1f);
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2f, 2.5f);
            //charAnim = GetComponent<SkeletonAnimation>();
        }
        // public void SetInfoCharacter(ChessData_AAC chess, List<GameObject> stars)
        // {
        //     chessInfo = chess;
        //     idChar = chess.Index.ToString();
        //     star = 1;
        //     lsStars = stars;
        //     for(int i = 0; i < lsStars.Count; i++)
        //     {
        //         if (i < star)
        //         {
        //             lsStars[i].SetActive(true);
        //         }
        //         else
        //         {
        //             lsStars[i].SetActive(false);
        //         }
        //     }
        //     //indexOfCharacter = 
        // }
        // public void OnMergeStar()
        // {
        //     //gameObject.SetActive(false);
        //     GameControl.Instance.HeroPos[indexOfCharacter].character = null;
        //     GameControl.Instance.lsCharacters.Remove(this);
        //     Destroy(gameObject);
        // }
        public void UpdateStar(int index)
        {
            star += index;
            Debug.Log("Starrrr  " + star);
            for (int i = 0; i < lsStars.Count; i++)
            {
                if (i < star)
                {
                    lsStars[i].SetActive(true);
                }
                else
                {
                    lsStars[i].SetActive(false);
                }
            }
        }
        bool CheckSoldCharacter(Vector2 pos)
        {
            Debug.Log(pos + " == " + shopPos);
            if (pos.x < shopPos.x + 5f && pos.x > shopPos.x - 5f && pos.y < shopPos.y + 1.5f && pos.y > shopPos.y - 1.5f)
            {

                return true;
            }

            return false;
        }
        int GetStar()
        {
            int startCount = 0;
            for (int i = 0; i < lsStars.Count; i++)
            {
                if (lsStars[i].activeSelf)
                {
                    startCount++;
                }
            }
            return startCount;
        }
        // public void OnMouseDown()
        // {
        //     if(GameControl.Instance.isDragHero)
        //         isStartDragging = true;
        //     if (!isOver)
        //     {
        //         isOver = true;
        //         if (!Rubik_Casual.UI.UIController.Instance.characterInfo.checkHidePanel)
        //         {

        //             Rubik_Casual.UI.UIController.Instance.characterInfo.BtnHidePanel();
        //         }
        //         Rubik_Casual.UI.UIController.Instance.characterInfo.ShowInfo(AxieInit.instance.GetAxieStats(idChar), GameControl.Instance.connectManager.GetChessByID(_ID).Equips, _ID, GetStar());
        //     }
        // }


        public void OnMouseEnter()
        {

        }
        public void OnMouseExit()
        {


        }
        public void RefeshCharacterLayout()
        {
            if (indexOfCharacter < 9)
            {
                GetComponent<MeshRenderer>().sortingLayerName = "Character";
                GetComponent<MeshRenderer>().sortingOrder = 10;
            }
            else
            {
                GetComponent<MeshRenderer>().sortingLayerName = "Character";
                GetComponent<MeshRenderer>().sortingOrder = 10;
            }
        }
        public void OnMouseDrag()
        {
            if (BattleController.instance.gameState != GameState.WAIT_BATTLE || BattleController.instance.gameState == GameState.END)
            {
                gameObject.transform.position = oriPos;
                GetComponent<MeshRenderer>().sortingOrder = 10;
                GetComponent<MeshRenderer>().sortingLayerName = "Character";
                return;
            }

            //Debug.Log("Draw");
            dragging = true;
            if (!dragging) return;

            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (dragging)
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 newPos = objPosition + offset;
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveSpeed);
                // transform.position = newPos;
            }
            GetComponent<MeshRenderer>().sortingOrder = 100;
            GetComponent<MeshRenderer>().sortingLayerName = "ShowPopup";
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
        private void OnMouseDown()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            offset = transform.position - objPosition;
            dragging = true;
        }
        private Vector3 offset;
        float moveSpeed = 20f;
        private void OnMouseUp()
        {
            int temp = GameControl.instance.CheckNearPos(gameObject.transform.position);
            GetComponent<MeshRenderer>().sortingOrder = 10;
            GetComponent<MeshRenderer>().sortingLayerName = "Character";

            if (temp == -1 || BattleController.instance.gameState != GameState.WAIT_BATTLE || (BattleController.instance.lsSlotGbHero[temp] != null && BattleController.instance.lsSlotGbHero[temp].GetComponent<CharacterInBattle>().HpNow == 0))
            {
                gameObject.transform.position = oriPos;
                return;
            }

            gameObject.transform.position = oriPos;

            GameControl.instance.swapCharacter(oriIndex, temp);
            oriIndex = temp;
            oriPos = gameObject.transform.position;

            UIGamePlay.instance.isHaveChangeSlot = true;
            dragging = false;
        }


    }

}
