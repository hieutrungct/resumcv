using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Rubik_Casual;
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
        public Rare rare;
    }
    public class NameGbFlipCard
    {
        public const string NameGbIcon = "Icon", NameGbBackCard = "BackCard", NameGbFrontCard = "FrontCard",
         NameGbTxtNewWaifu = "NewWaifu", NameGbTxtGetWaifu = "TxtGetWaifu", NameGbDimed = "Dimed",
         NameGbTxtValue = "TxtValue";
    }
    public class FlipCardController : MonoBehaviour
    {
        public GameObject popup;
        public GameObject gbTest, gbInfoCard, imageBackGround, gbTicket, gbInfoCardWithMouse;
        public SlotInfoCard slotInfoCard;
        public Transform posInstantiateCard;
        public Transform specificSlot;
        public Button btnGetWaifu;
        public List<GameObject> lsInfocardClone;
        public List<Sprite> lsSpriteCard;
        public List<CardForId> lsCardForId, lsCardGacha, lsCardForIdRare_R, lsCardForIdRare_SR, lsCardForIdRare_SSR, lsCardForIdRare_UR;
        public CardForId cardForIds;
        public int Id, idSummon;
        TextMeshProUGUI TxtBtn;
        DataController dataController;
        public bool isClick = false, ChangeType, isHaveMove;
        private bool isGachaInProgress = false;
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
            TxtBtn = btnGetWaifu.transform.Find(NameGbFlipCard.NameGbTxtGetWaifu).GetComponent<TextMeshProUGUI>();
            gbTicket.transform.Find(NameGbFlipCard.NameGbTxtValue).GetComponent<TextMeshProUGUI>().text = dataController.userData.Ticket.ToString();
            if (dataController.userData.Ticket < 1)
            {
                TxtBtn.text = "Out Ticket!";
            }
            CreateCard();
            SetLsCardForId();
            btnGetWaifu.onClick.AddListener(() =>
            {
                gbInfoCardWithMouse.transform.position = originOriginGbInfoCard;
                BtnGacha();
            });

            this.transform.Find(NameGbFlipCard.NameGbDimed).gameObject.AddComponent<Button>().onClick.AddListener(() =>
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
        public void OnClickSummon()
        {
            gameObject.SetActive(true);
            StartCoroutine(InvokeBtnGacha());
        }
        private IEnumerator InvokeBtnGacha()
        {
            // Chờ một khoảng thời gian nhất định trước khi gọi hàm BtnGacha()
            yield return new WaitUntil(() => gameObject.activeInHierarchy); // Thay đổi giá trị này nếu cần

            BtnGacha();
        }
        
        void BtnGacha()
        {
            int timeSpawnCard;
            if (!isGachaInProgress) // Kiểm tra xem quá trình Gacha có đang diễn ra hay không
            {
                isGachaInProgress = true; // Đặt cờ báo hiệu rằng quá trình Gacha đang diễn ra
                
                ResetInfo();
                Debug.Log("đã bấm quay");
                if(Id == 1)
                {
                    CreateCard_1Tecken();
                    timeSpawnCard = 1;
                }
                else
                {
                    CreateCard();
                    SummonCard();
                    timeSpawnCard = 5;
                    
                }
                
                if (dataController.userData.Ticket >= Id)
                {
                    if (!isHaveMove)
                    {
                        isHaveMove = true;
                        dataController.userData.Ticket = dataController.userData.Ticket - Id;
                        if (!isClick && Id != 1)
                        {
                            isClick = !isClick;
                            GachaCard(0);
                            BtnMoveCard(0);
                            // Debug.Log("ấn lần đầu");
                        }
                        else
                        {
                            // Debug.Log("đã ResetGatcha");
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
                    TextMeshProUGUI txtGbTicket = gbTicket.transform.Find(NameGbFlipCard.NameGbTxtValue).GetComponent<TextMeshProUGUI>();
                    txtGbTicket.text = dataController.userData.Ticket.ToString();
                    HUDController.instanse.LoadStatusNumber();
                    TextMeshProUGUI txtGbTicketClone = Instantiate(txtGbTicket, txtGbTicket.transform);
                    txtGbTicketClone.text = Id.ToString();
                    txtGbTicketClone.gameObject.transform.position = txtGbTicket.gameObject.transform.position;
                    txtGbTicketClone.color = Color.red;
                    txtGbTicketClone.transform.DOMoveY(txtGbTicketClone.gameObject.transform.position.y + 0.5f, 0.75f)
                    .OnComplete(() =>
                    {
                        Destroy(txtGbTicketClone.gameObject);
                    });
                }
                else
                {
                    TxtBtn.text = "Out Ticket!";
                    TxtBtn.DOColor(Color.red, 1f)
                    .OnComplete(() =>
                    {
                        TxtBtn.DOColor(Color.white, 1f);
                    });
                    // Debug.Log("Hết vé");
                }
                StartCoroutine(ResetGachaStateAfterDelay(timeSpawnCard)); // Reset trạng thái Gacha sau timeSpawnCard giây
            }
            

        }
        IEnumerator ResetGachaStateAfterDelay(float delay)
        {
            TxtBtn.text = "Don't Spawm";
            TxtBtn.DOColor(Color.red, delay)
            .OnComplete(() =>
            {
                TxtBtn.DOColor(Color.white, 1f).OnComplete(() =>
                {
                    TxtBtn.text = "Get Again";
                    isGachaInProgress = false;
                });
            });
            yield return new WaitForSeconds(delay);

            // Đặt lại cờ báo hiệu và cho phép Gacha lại
            
        }
        void ResetGatcha()
        {
            
            foreach (var item in lsInfocardClone)
            {
                item.transform.position = posInstantiateCard.position;
                item.transform.localScale = new Vector3();

                Transform transBackCard = item.transform.Find(NameGbFlipCard.NameGbBackCard);
                transBackCard.localScale = new Vector3(1f, 1f, 1f);

                Transform transFrontCard = item.transform.Find(NameGbFlipCard.NameGbFrontCard);
                transFrontCard.localScale = new Vector3(0, 1f, 1f);

            }
            
            lsCardGacha.Clear();
        }
        void SetLsCardForId()
        {

            foreach (Waifu.InfoWaifuAsset InfoWaifuAssets in dataController.characterAssets.WaifuAssets.infoWaifuAssets.lsInfoWaifuAssets)
            {
                
                if (InfoWaifuAssets.Rare == Waifu.Rare.R)
                {
                    CardForId cardForId = new CardForId();
                    cardForId.ID = InfoWaifuAssets.ID;
                    cardForId.Code = InfoWaifuAssets.Code;
                    cardForId.rare = InfoWaifuAssets.Rare;
                    lsCardForIdRare_R.Add(cardForId);
                }
                else if (InfoWaifuAssets.Rare == Waifu.Rare.SR)
                {
                    CardForId cardForId = new CardForId();
                    cardForId.ID = InfoWaifuAssets.ID;
                    cardForId.Code = InfoWaifuAssets.Code;
                    cardForId.rare = InfoWaifuAssets.Rare;
                    lsCardForIdRare_SR.Add(cardForId);
                }
                else if (InfoWaifuAssets.Rare == Waifu.Rare.SSR)
                {
                    CardForId cardForId = new CardForId();
                    cardForId.ID = InfoWaifuAssets.ID;
                    cardForId.Code = InfoWaifuAssets.Code;
                    cardForId.rare = InfoWaifuAssets.Rare;
                    lsCardForIdRare_SSR.Add(cardForId);
                }
                else
                {
                    CardForId cardForId = new CardForId();
                    cardForId.ID = InfoWaifuAssets.ID;
                    cardForId.Code = InfoWaifuAssets.Code;
                    cardForId.rare = InfoWaifuAssets.Rare;
                    lsCardForIdRare_UR.Add(cardForId);
                }
                
                

                
                
            }
            

        }
        
        void GachaCard(int countUp)
        {
            if (countUp < lsInfocardClone.Count)
            {
                float randomRare = UnityEngine.Random.Range(0f, 1f);
                if(idSummon == 0)
                {
                    if(randomRare <= 0.6)
                    {
                        Randomcard(lsCardForIdRare_R,countUp);
                    }
                    else
                    {
                        Randomcard(lsCardForIdRare_SR,countUp);
                    }
                }
                else
                {
                    if(randomRare <= 0.3){
                        Randomcard(lsCardForIdRare_R,countUp);
                    }
                    else if(randomRare > 0.3 && randomRare <= 0.8)
                    {
                        Randomcard(lsCardForIdRare_SR,countUp);
                    }
                    else if(randomRare > 0.7 && randomRare <= 0.95)
                    {
                        Randomcard(lsCardForIdRare_SSR,countUp);
                    }
                    else
                    {
                        Randomcard(lsCardForIdRare_UR,countUp);
                    }
                }
                

                // int intRand = UnityEngine.Random.Range(0, lsCardForId.Count);
                // if (lsCardGacha.FirstOrDefault(f => f.ID == lsCardForId[intRand].ID) == null)
                // {
                //     lsCardGacha.Add(lsCardForId[intRand]);
                //     GachaCard(countUp + 1);
                // }
                // else
                // {
                //     GachaCard(countUp);
                // }
            }
        }
        void Randomcard(List<CardForId> cardForIds, int countUp)
        {
            int intRand = UnityEngine.Random.Range(0, cardForIds.Count);
            if (lsCardGacha.FirstOrDefault(f => f.ID == cardForIds[intRand].ID) == null)
            {
                lsCardGacha.Add(cardForIds[intRand]);
                GachaCard(countUp + 1);
            }
            else
            {
                GachaCard(countUp);
            }
        }
        public void SummonCard()
        {
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(idSummon);
            cardForIds = new CardForId();
            cardForIds.ID = infoWaifu.ID;
            cardForIds.Code = infoWaifu.Code;
            cardForIds.rare = infoWaifu.Rare;
            lsCardGacha.Add(cardForIds);
            
        }
        void CreateCard()
        {
            foreach (Transform child in posInstantiateCard)
            {

                Destroy(child.gameObject);
            }
            lsInfocardClone.Clear();
            for (int i = 0; i < slotInfoCard.lsPosSlotInfoCard.Count; i++)
            {
                
                GameObject infoCardClone = Instantiate(gbInfoCard, posInstantiateCard);
                infoCardClone.transform.position = posInstantiateCard.position;
                infoCardClone.transform.localScale = new Vector3();
                lsInfocardClone.Add(infoCardClone);
            }
        }
        void CreateCard_1Tecken()
        {
            lsInfocardClone.Clear();
            GameObject infoCardClone = Instantiate(gbInfoCard, specificSlot); // Tạo card tại specificSlot
            infoCardClone.transform.localScale = Vector3.zero; // Thiết lập scale ban đầu
            lsInfocardClone.Add(infoCardClone);
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
                if(Id == 1)
                {
                    infoCardClone.transform.DOMove(specificSlot.position, 0.25f)
                    .OnComplete(() =>
                    {

                        CardInfoDragPosition cardInfoDragPosition = infoCardClone.GetComponent<CardInfoDragPosition>();
                        cardInfoDragPosition.idSlot = index;
                        cardInfoDragPosition.SetValueMoveBackGround(index);

                        infoCardClone.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f);

                        Transform transBackCard = infoCardClone.transform.Find(NameGbFlipCard.NameGbBackCard);
                        transBackCard.DOScale(new Vector3(0, 1f, 1f), 0.25f);

                        Transform transFrontCard = infoCardClone.transform.Find(NameGbFlipCard.NameGbFrontCard);
                        transFrontCard.GetComponent<Image>().sprite = lsSpriteCard[UnityEngine.Random.Range(0, lsSpriteCard.Count)];
                        transFrontCard.DOScale(new Vector3(1f, 1f, 1f), 0.25f);

                        InfoWaifuAsset infoWaifuAsset = dataController.GetInfoWaifuAssetsByIndex(lsCardGacha[index].ID);
                        cardInfoDragPosition.infoWaifuAsset = infoWaifuAsset;

                        SetUpInfoCard(index, transFrontCard, cardInfoDragPosition);

                        if (!ChangeType)
                        {
                            BtnMoveCard(index + 1);
                        }
                    });
                }
                else
                {
                    infoCardClone.transform.DOMove(slotInfoCard.lsPosSlotInfoCard[index].position, 0.25f)
                    .OnComplete(() =>
                    {

                        CardInfoDragPosition cardInfoDragPosition = infoCardClone.GetComponent<CardInfoDragPosition>();
                        cardInfoDragPosition.idSlot = index;
                        cardInfoDragPosition.SetValueMoveBackGround(index);

                        infoCardClone.transform.DOJump(infoCardClone.transform.position, 1f / 3f, 1, 0.5f);

                        Transform transBackCard = infoCardClone.transform.Find(NameGbFlipCard.NameGbBackCard);
                        transBackCard.DOScale(new Vector3(0, 1f, 1f), 0.25f);

                        Transform transFrontCard = infoCardClone.transform.Find(NameGbFlipCard.NameGbFrontCard);
                        transFrontCard.GetComponent<Image>().sprite = lsSpriteCard[UnityEngine.Random.Range(0, lsSpriteCard.Count)];
                        transFrontCard.DOScale(new Vector3(1f, 1f, 1f), 0.25f);

                        InfoWaifuAsset infoWaifuAsset = dataController.GetInfoWaifuAssetsByIndex(lsCardGacha[index].ID);
                        cardInfoDragPosition.infoWaifuAsset = infoWaifuAsset;

                        SetUpInfoCard(index, transFrontCard, cardInfoDragPosition);

                        if (!ChangeType)
                        {
                            BtnMoveCard(index + 1);
                        }
                    });
                }

                
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
            int IDWaifu = cardInfoDragPosition.infoWaifuAsset.ID;
            Transform transIcon = transFrontCard.Find(NameGbFlipCard.NameGbIcon);

            GameObject gbIcon = transIcon.gameObject;
            assetLoader = dataController.assetLoader;

            string nameImage = MovePopup.GetNameImageWaifu(null, dataController.characterAssets.WaifuAssets.Get2D(IDWaifu.ToString()));

            Sprite sprite = assetLoader.Avatars.Find(f => f.name == nameImage);
            gbIcon.GetComponent<Image>().sprite = sprite;

            TextMeshProUGUI txtNewWaifu = transFrontCard.Find(NameGbFlipCard.NameGbTxtNewWaifu).gameObject.GetComponent<TextMeshProUGUI>();
            txtNewWaifu.gameObject.SetActive(false);
            txtNewWaifu.text = "New Waifu";
            Data.Player.PlayerOwnsWaifu playerOwnsWaifuClone = dataController.playerData.lsPlayerOwnsWaifu.Find(f => f.ID == IDWaifu);
            if (playerOwnsWaifuClone == null)
            {
                txtNewWaifu.gameObject.SetActive(true);
                
                playerOwnsWaifuClone = new Data.Player.PlayerOwnsWaifu();
                InfoWaifuAsset infoWaifuAssetClone = cardInfoDragPosition.infoWaifuAsset;
                txtNewWaifu.text = infoWaifuAssetClone.Name;
                playerOwnsWaifuClone.ID = infoWaifuAssetClone.ID;
                // playerOwnsWaifuClone.HP = infoWaifuAssetClone.HP;
                // playerOwnsWaifuClone.ATK = infoWaifuAssetClone.ATK;
                // playerOwnsWaifuClone.DEF = infoWaifuAssetClone.DEF;
                // playerOwnsWaifuClone.Pow = infoWaifuAssetClone.Pow;
                playerOwnsWaifuClone.Star = 1;
                playerOwnsWaifuClone.Skill = infoWaifuAssetClone.Skill;
                
                playerOwnsWaifuClone.level = 1;


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
