using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.EventSystems;
using Rubik.Waifu;
using RubikCasual.Battle;

namespace Rubik.Axie
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CharacterDragPosition : MonoBehaviour
    {
        // Start is called before the first frame update
        public SkeletonAnimation CharacterSke;
        public Transform posCharacter;
        public Vector2 oriPos, shopPos;

        private bool dragging = false, isStartDragging = false, isSwap = false;
        public SkeletonAnimation charAnim;
        public int indexOfCharacter;
        // public CardCharacter thisCardCharacter;
        public bool isInDeck = false;
        public int star, oriIndex;
        public string idChar, _ID;
        public List<GameObject> lsStars = new List<GameObject>();
        List<GameObject> lsChars = new List<GameObject>();
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
            if (BattleController.instance.gameState != GameState.WAIT_BATTLE)
            {
                gameObject.transform.position = oriPos;
                GetComponent<MeshRenderer>().sortingOrder = 10;
                GetComponent<MeshRenderer>().sortingLayerName = "Character";
                return;
            }

            //Debug.Log("Draw");
            dragging = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
            GetComponent<MeshRenderer>().sortingOrder = 100;
            GetComponent<MeshRenderer>().sortingLayerName = "Character";
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
        bool isOver = false;
        private void OnMouseUp()
        {
            if (BattleController.instance.gameState != GameState.WAIT_BATTLE)
            {
                gameObject.transform.position = oriPos;
                GetComponent<MeshRenderer>().sortingOrder = 10;
                GetComponent<MeshRenderer>().sortingLayerName = "Character";
                return;
            }
            gameObject.transform.position = oriPos;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int temp = GameControl.instance.CheckNearPos(mousePosition);
            Debug.Log(temp);
            if (temp != -1)
            {
                GameControl.instance.swapCharacter(oriIndex, temp);
                oriIndex = temp;
            }

            oriPos = gameObject.transform.position;


            GetComponent<MeshRenderer>().sortingOrder = 10;
            GetComponent<MeshRenderer>().sortingLayerName = "Character";
        }
        // public void OnMouseUp()
        // {
        //     isOver = false;
        //     //Rubik_Casual.UI.UIController.Instance.characterInfo.BtnHidePanel();
        //     if (!GameControl.Instance.isDragHero)
        //         return;
        //     //gameObject.transform.position = oriPos;
        //     dragging = false;
        //     GetComponent<MeshRenderer>().sortingOrder = 2 + indexOfCharacter;
        //     GetComponent<MeshRenderer>().sortingLayerName = "Character";
        //     Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     if (!dragging && isStartDragging)
        //     {
        //         isStartDragging = false;
        //         shopPos = GameControl.Instance.shopTrans.position;
        //         if (CheckSoldCharacter(mousePosition))
        //         {
        //             GameControl.Instance.connectManager.SellChess(_ID);
        //             Debug.Log("Sell chess");
        //             GameControl.Instance.HightLightOff();
        //             OnMergeStar();
        //             return;
        //         }
        //         int temp = GameControl.Instance.CheckNearPos(mousePosition);
        //         //int tempWaitPos = GameControl.Instance.CheckNearInWaitPos(mousePosition);
        //         //Debug.Log("Temp : " + temp + "   TempWait : " + tempWaitPos);

        //         if (temp == -1|| GameControl.Instance.gameState == GameState.BATTLE)
        //         {

        //             gameObject.transform.position = oriPos;
        //             EffectController.Instance.SpawnEffect(oriPos, EffectName.SMOKE);
        //             GameControl.Instance.HightLightOff();
        //         }
        //         else //if (isInDeck)
        //         {

        //             if (GameControl.Instance.HeroPos[temp].character != null)
        //             {

        //                 GameControl.Instance.SwapHeroPosition(this, GameControl.Instance.HeroPos[temp]);
        //                 GameControl.Instance.connectManager.ChessChangeSlot(_ID, GameControl.Instance.HeroPos[temp].indexOfSlot);
        //                 EffectController.Instance.SpawnEffect(GameControl.Instance.HeroPos[temp].transform.position, EffectName.SMOKE);
        //                 SoundController.Instance.PlaySingle(FXSound.Instance.Fx_Button2);
        //             }
        //             else
        //             {

        //                 if (indexOfCharacter>8&& GameControl.Instance.HeroPos[temp].indexOfSlot<9 && GameControl.Instance.CheckCanSetHero())
        //                 {

        //                     gameObject.transform.position = oriPos;
        //                     SoundController.Instance.PlaySingle(FXSound.Instance.FX_Fail);
        //                     ZfxGameplayController.Instance.ShowNotiText("Deck is full");
        //                     GameControl.Instance.HightLightOff();
        //                     // EffectController.Instance.SpawnEffect(oriPos, EffectName.SMOKE);
        //                     return;
        //                 }
        //                 SoundController.Instance.PlaySingle(FXSound.Instance.Fx_Button2);
        //                 //Debug.LogError(indexOfCharacter + "  - " + GameControl.Instance.HeroPos[temp].indexOfSlot + "  --  "+GameControl.Instance.CheckCanSetHero());
        //                 //posImage.sprite = SpriteHelper.Instance.lsPosSprite[0];
        //                 //GameControl.Instance.HeroPos[temp].character.thisCardCharacter.gameObject.SetActive(true);
        //                 GameControl.Instance.HeroPos[this.indexOfCharacter].character=null;
        //                 //GameControl.Instance.HeroPos[temp].character.indexOfCharacter = -1;
        //                 GameControl.Instance.SetHeroPosition(temp, this);
        //                 EffectController.Instance.SpawnEffect(this.transform.position, EffectName.SMOKE);
        //                 isInDeck = true;
        //                 oriPos = transform.position;
        //                 GameControl.Instance.connectManager.ChessChangeSlot(_ID, temp);
        //             }
        //         }

        //     }
        //     GameControl.Instance.HightLightOff();
        // }

    }

}
