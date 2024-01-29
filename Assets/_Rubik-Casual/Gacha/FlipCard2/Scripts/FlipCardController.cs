using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RubikCasual.Data;
using RubikCasual.Tool;
using RubikCasual.Waifu;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.FlipCard2
{
    [Serializable]
    public class CardForId
    {
        public int ID;
        public string Code;
        public string Rare;
    }
    public class FlipCardController : MonoBehaviour
    {
        public GameObject gbTest, gbInfoCard, imageBackGround, gbTicket, gbInfoCardWithMouse;
        public SlotInfoCard slotInfoCard;
        public Transform posInstantiateCard;
        public Button btnGetWaifu;
        public List<GameObject> lsInfocardClone;
        public List<Sprite> lsSpriteCard;
        public List<CardForId> lsCardForId, lsCardGacha;
        TextMeshProUGUI TxtBtn;
        DataController dataController;
        bool isClick = false, ChangeType, isHaveMove;
        const string NameGbIcon = "Icon", NameGbBackCard = "BackCard", NameGbFrontCard = "FrontCard",
                    NameGbTxtNewWaifu = "NewWaifu", NameGbTxtGetWaifu = "TxtGetWaifu", NameGbDimed = "Dimed",
                    NameGbTxtValue = "TxtValue";
        Rubik_Casual.AssetLoader assetLoader;
        Vector3 originOriginGbInfoCard;
        public static FlipCardController instance;
        protected void Awake()
        {
            instance = this;
            originOriginGbInfoCard = gbInfoCardWithMouse.transform.position;
        }
        void Start()
        {

            dataController = DataController.instance;
            TxtBtn = btnGetWaifu.transform.Find(NameGbTxtGetWaifu).GetComponent<TextMeshProUGUI>();
            gbTicket.transform.Find(NameGbTxtValue).GetComponent<TextMeshProUGUI>().text = dataController.userData.Ticket.ToString();
            if (dataController.userData.Ticket < 1)
            {
                TxtBtn.text = "Out Ticket";
            }
            CreateCard();
            SetLsCardForId();
            btnGetWaifu.onClick.AddListener(() =>
            {
                gbInfoCardWithMouse.transform.position = originOriginGbInfoCard;
                BtnGacha();
            });

            this.transform.Find(NameGbDimed).gameObject.AddComponent<Button>().onClick.AddListener(() =>
            {
                if (!isHaveMove)
                {
                    ResetGatcha();
                    this.gameObject.SetActive(false);
                    TxtBtn.text = "Get Waifu";
                    gbInfoCardWithMouse.transform.position = originOriginGbInfoCard;
                }
            });
        }
        void BtnGacha()
        {
            if (dataController.userData.Ticket > 0)
            {
                if (!isHaveMove)
                {
                    isHaveMove = true;
                    dataController.userData.Ticket = dataController.userData.Ticket - 1;
                    if (!isClick)
                    {
                        isClick = !isClick;
                        GachaCard(0);
                        BtnMoveCard(0);
                    }
                    else
                    {
                        ResetGatcha();
                        GachaCard(0);
                        BtnMoveCard(0);
                    }
                    TxtBtn.text = "Get Again";
                }
                else
                {
                    TxtBtn.text = "Don't Spawm";
                    TxtBtn.DOColor(Color.red, 1f)
                    .OnComplete(() =>
                    {
                        TxtBtn.DOColor(Color.white, 1f).OnComplete(() =>
                        {
                            TxtBtn.text = "Get Again";
                        });
                    });

                }
            }
            else
            {
                TxtBtn.text = "Out Ticket";
                Debug.Log("Hết vé");
            }
            TextMeshProUGUI txtGbTicket = gbTicket.transform.Find(NameGbTxtValue).GetComponent<TextMeshProUGUI>();
            txtGbTicket.text = dataController.userData.Ticket.ToString();
            TextMeshProUGUI txtGbTicketClone = Instantiate(txtGbTicket, txtGbTicket.transform);
            txtGbTicketClone.text = "-1";
            txtGbTicketClone.gameObject.transform.position = txtGbTicket.gameObject.transform.position;
            txtGbTicketClone.color = Color.red;
            txtGbTicketClone.transform.DOMoveY(txtGbTicketClone.gameObject.transform.position.y + 0.5f, 0.75f)
            .OnComplete(() => { Destroy(txtGbTicketClone.gameObject); });
        }
        void ResetGatcha()
        {
            foreach (var item in lsInfocardClone)
            {
                item.transform.position = posInstantiateCard.position;
                item.transform.localScale = new Vector3();

                Transform transBackCard = item.transform.Find(NameGbBackCard);
                transBackCard.localScale = new Vector3(1f, 1f, 1f);

                Transform transFrontCard = item.transform.Find(NameGbFrontCard);
                transFrontCard.localScale = new Vector3(0, 1f, 1f);

            }
            lsCardGacha.Clear();
        }
        void SetLsCardForId()
        {

            foreach (Waifu.InfoWaifuAsset InfoWaifuAssets in dataController.characterAssets.WaifuAssets.infoWaifuAssets.lsInfoWaifuAssets)
            {

                if (InfoWaifuAssets.Rare == Waifu.Rare.R.ToString())
                {
                    CardForId cardForId = new CardForId();
                    cardForId.ID = InfoWaifuAssets.ID;
                    cardForId.Code = InfoWaifuAssets.Code;
                    cardForId.Rare = InfoWaifuAssets.Rare;
                    lsCardForId.Add(cardForId);
                }
            }

        }
        void GachaCard(int countUp)
        {

            if (countUp < lsInfocardClone.Count)
            {
                int intRand = UnityEngine.Random.Range(0, lsCardForId.Count);
                if (lsCardGacha.FirstOrDefault(f => f.ID == lsCardForId[intRand].ID) == null)
                {
                    lsCardGacha.Add(lsCardForId[intRand]);
                    GachaCard(countUp + 1);
                }
                else
                {
                    GachaCard(countUp);
                }
            }
        }
        void CreateCard()
        {
            for (int i = 0; i < slotInfoCard.lsPosSlotInfoCard.Count; i++)
            {
                GameObject infoCardClone = Instantiate(gbInfoCard, posInstantiateCard);
                infoCardClone.transform.position = posInstantiateCard.position;
                infoCardClone.transform.localScale = new Vector3();
                lsInfocardClone.Add(infoCardClone);
            }
        }
        [Button]
        void ResetInfo()
        {
            foreach (var item in lsInfocardClone)
            {
                Destroy(item);
            }
            lsInfocardClone.Clear();
            CreateCard();
        }

        void BtnMoveCard(int index)
        {
            if (index < lsInfocardClone.Count)
            {
                GameObject infoCardClone = lsInfocardClone[index];
                infoCardClone.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
                infoCardClone.transform.DOMove(slotInfoCard.lsPosSlotInfoCard[index].position, 0.25f)
                .OnComplete(() =>
                {

                    CardInfoDragPosition cardInfoDragPosition = infoCardClone.GetComponent<CardInfoDragPosition>();
                    cardInfoDragPosition.idSlot = index;
                    cardInfoDragPosition.SetValueMoveBackGround(index);

                    infoCardClone.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f);

                    Transform transBackCard = infoCardClone.transform.Find(NameGbBackCard);
                    transBackCard.DOScale(new Vector3(0, 1f, 1f), 0.25f);

                    Transform transFrontCard = infoCardClone.transform.Find(NameGbFrontCard);
                    transFrontCard.GetComponent<Image>().sprite = lsSpriteCard[UnityEngine.Random.Range(0, lsSpriteCard.Count)];
                    transFrontCard.DOScale(new Vector3(1f, 1f, 1f), 0.25f);

                    InfoWaifuAsset infoWaifuAsset = dataController.characterAssets.WaifuAssets.infoWaifuAssets.lsInfoWaifuAssets.FirstOrDefault(f => f.ID == lsCardGacha[index].ID);
                    cardInfoDragPosition.infoWaifuAsset = infoWaifuAsset;

                    SetUpInfoCard(index, transFrontCard, cardInfoDragPosition);

                    if (!ChangeType)
                    {
                        BtnMoveCard(index + 1);
                    }
                });
                if (ChangeType)
                {
                    BtnMoveCard(index + 1);
                }
            }
            else
            {
                isHaveMove = false;
            }

        }
        void SetUpInfoCard(int index, Transform transFrontCard, CardInfoDragPosition cardInfoDragPosition)
        {
            string IDWaifu = cardInfoDragPosition.infoWaifuAsset.ID.ToString();
            Transform transIcon = transFrontCard.Find(NameGbIcon);

            GameObject gbIcon = transIcon.gameObject;
            assetLoader = dataController.listImage;

            string nameImage = MovePopup.GetNameImageWaifu(null, dataController.characterAssets.WaifuAssets.Get2D(IDWaifu));

            Sprite sprite = assetLoader.Avatars.Find(f => f.name == nameImage);
            gbIcon.GetComponent<Image>().sprite = sprite;

            TextMeshProUGUI txtNewWaifu = transFrontCard.Find(NameGbTxtNewWaifu).gameObject.GetComponent<TextMeshProUGUI>();
            txtNewWaifu.gameObject.SetActive(false);
            txtNewWaifu.text = "New Waifu";
            Data.Player.PlayerOwnsWaifu playerOwnsWaifuClone = dataController.playerData.lsPlayerOwnsWaifu.Find(f => f.ID.ToString() == IDWaifu);
            if (playerOwnsWaifuClone == null)
            {
                txtNewWaifu.gameObject.SetActive(true);
                playerOwnsWaifuClone = new Data.Player.PlayerOwnsWaifu();
                InfoWaifuAsset infoWaifuAssetClone = cardInfoDragPosition.infoWaifuAsset;
                playerOwnsWaifuClone.ID = infoWaifuAssetClone.ID;
                playerOwnsWaifuClone.HP = infoWaifuAssetClone.HP;
                playerOwnsWaifuClone.ATK = infoWaifuAssetClone.ATK;
                playerOwnsWaifuClone.DEF = infoWaifuAssetClone.DEF;
                playerOwnsWaifuClone.Star = infoWaifuAssetClone.Star;
                playerOwnsWaifuClone.Skill = infoWaifuAssetClone.Skill;
                playerOwnsWaifuClone.Pow = infoWaifuAssetClone.Pow;

                dataController.playerData.lsPlayerOwnsWaifu.Add(playerOwnsWaifuClone);
            }
            else
            {
                playerOwnsWaifuClone.frag += 1;
            }
            cardInfoDragPosition.frag = playerOwnsWaifuClone.frag;
        }
    }
}
