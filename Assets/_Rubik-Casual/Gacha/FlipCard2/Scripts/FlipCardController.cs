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
        public GameObject gbTest, GbInfoCard;
        public SlotInfoCard slotInfoCard;
        public Transform posInstantiateCard;
        public GameObject imageBackGround;
        public Button btnGetWaifu;
        public List<GameObject> lsInfocardClone;
        public List<Sprite> lsSpriteCard;
        public List<CardForId> lsCardForId, lsCardGacha;
        DataController dataController;
        bool isClick, ChangeType, isHaveMove;
        const string NameGbIcon = "Icon", NameGbBackCard = "BackCard", NameGbFrontCard = "FrontCard", NameGbTxtNew = "NewWaifu";
        Rubik_Casual.AssetLoader assetLoader;
        public static FlipCardController instance;
        protected void Awake()
        {
            instance = this;
        }
        void Start()
        {

            dataController = DataController.instance;
            CreateCard();
            SetLsCardForId();
            BtnGacha();
            this.transform.Find("Dimed").gameObject.AddComponent<Button>().onClick.AddListener(() =>
            {
                this.gameObject.SetActive(false);
            });
        }
        void BtnGacha()
        {
            btnGetWaifu.onClick.AddListener(() =>
            {
                if (isClick)
                {
                    isHaveMove = true;
                    GachaCard(0);
                    BtnMoveCard(0);
                    isClick = !isClick;
                }
                else
                {
                    if (!isHaveMove)
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
                        btnGetWaifu.transform.Find("TxtGetWaifu").GetComponent<TextMeshProUGUI>().text = "Get Again";
                        lsCardGacha.Clear();
                        isHaveMove = !isHaveMove;
                        GachaCard(0);
                        BtnMoveCard(0);
                    }
                }
            });
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
                GameObject infoCardClone = Instantiate(GbInfoCard, posInstantiateCard);
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

                    SetUpInfoCard(index, transFrontCard, cardInfoDragPosition.infoWaifuAsset.ID.ToString());

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
                isHaveMove = !isHaveMove;
            }

        }
        void SetUpInfoCard(int index, Transform transFrontCard, string IDWaifu)
        {
            Transform transIcon = transFrontCard.Find(NameGbIcon);

            GameObject gbIcon = transIcon.gameObject;
            assetLoader = dataController.listImage;

            string nameImage = MovePopup.GetNameImageWaifu(null, dataController.characterAssets.WaifuAssets.Get2D(IDWaifu));

            Sprite sprite = assetLoader.Avatars.Find(f => f.name == nameImage);
            gbIcon.GetComponent<Image>().sprite = sprite;

            TextMeshProUGUI txtNewWaifu = transFrontCard.Find(NameGbTxtNew).gameObject.GetComponent<TextMeshProUGUI>();
            txtNewWaifu.gameObject.SetActive(false);
            txtNewWaifu.text = "New Waifu";
            Data.Player.PlayerOwnsWaifu playerOwnsWaifuClone = dataController.playerData.lsPlayerOwnsWaifu.Find(f => f.ID.ToString() == IDWaifu);
            if (playerOwnsWaifuClone == null)
            {
                txtNewWaifu.gameObject.SetActive(true);
                playerOwnsWaifuClone = new Data.Player.PlayerOwnsWaifu();
                InfoWaifuAsset infoWaifuAssetClone = lsInfocardClone[index].GetComponent<CardInfoDragPosition>().infoWaifuAsset;
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
        }
    }
}
