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
        public List<GameObject> lsInfocardClone;
        public List<Sprite> lsSpriteCard;
        public DataController dataController;
        public List<CardForId> lsCardForId, lsCardGacha;
        public bool ChangeType;
        const string NameGbIcon = "Icon", NameGbBackCard = "BackCard", NameGbFrontCard = "FrontCard", NameGbTxtNew = "NewWaifu";

        void Start()
        {
            dataController = DataController.instance;
            CreateCard();
            SetLsCardForId();
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
            GachaCard(0);
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
        Rubik_Casual.AssetLoader assetLoader;
        [Button]
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

                    string IndexWaifu = dataController.characterAssets.WaifuAssets.WaifuAssetDatas.FirstOrDefault(f => f.Code == lsCardGacha[index].Code).Index.ToString();
                    SetUpInfoCard(index, transFrontCard, IndexWaifu);
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

        }
        void SetUpInfoCard(int index, Transform transFrontCard, string IndexWaifu)
        {
            Debug.Log(IndexWaifu);
            Transform transIcon = transFrontCard.Find(NameGbIcon);

            GameObject gbIcon = transIcon.gameObject;
            assetLoader = dataController.listImage;

            string nameImage = MovePopup.GetNameImageWaifu(null, dataController.characterAssets.WaifuAssets.Get2D(IndexWaifu));

            Sprite sprite = assetLoader.Avatars.Find(f => f.name == nameImage);
            gbIcon.GetComponent<Image>().sprite = sprite;

            transFrontCard.Find(NameGbTxtNew).gameObject.SetActive(true);
            TextMeshProUGUI txtNewWaifu = transFrontCard.Find(NameGbTxtNew).gameObject.GetComponent<TextMeshProUGUI>();

            // if (dataController.playerData.lsPlayerOwnsWaifu.Find(f=>f.))
            {

            }
        }
    }
}
