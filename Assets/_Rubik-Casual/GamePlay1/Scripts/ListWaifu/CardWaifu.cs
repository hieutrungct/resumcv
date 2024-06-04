using DG.Tweening;
using Rubik_Casual;
using RubikCasual.Character;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.GamePlayManager;
using RubikCasual.MapControllers;
using RubikCasual.Waifu;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace RubikCasual.ListWaifu
{
    public class CardWaifu : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image  avaBox, BackGlow, Glow, Role, shadow;
        public SkeletonGraphic UI_Waifu;
        public TextMeshProUGUI nameTxt, levelTxt;
        [SerializeField] GameObject[] stars;
        public PlayerOwnsWaifu _waifu;
        public SkeletonGraphic uiWaifu, hero;
        [HideInInspector] public Transform parentAfterDrag;
        public Vector3 offset;
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Bắt đầu kéo");
            uiWaifu = Instantiate(UI_Waifu, MapController.instance.CreatedHeroObj.transform);
            uiWaifu.transform.localScale = Vector3.one;
            uiWaifu.transform.position = gameObject.transform.position;
            // parentAfterDrag = uiWaifu.transform.parent;
            uiWaifu.raycastTarget = false;
            uiWaifu.transform.SetAsLastSibling();
            // offset = uiWaifu.transform.position - MouseWorldPosittion();
            
            AddMoveHeroComponent();
            AddWaifuComponent();

            MapController.instance.drag = true;
            MapController.instance.posistionAfter = gameObject.transform.position;
            // eventData.pointerDrag = uiWaifu.gameObject;

            shadow.gameObject.SetActive(true);
        }
        void IDragHandler.OnDrag(PointerEventData eventData)
        { 
            uiWaifu.transform.position = MouseWorldPosittion() + offset;
            // Debug.Log(MouseWorldPosittion());

        }
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            // Kiểm tra xem hero có được thả vào một InventorySlot hợp lệ hay không
            bool validDrop = eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<InventorySlot>() != null;
            if(validDrop) 
            {
                shadow.gameObject.SetActive(false);
                Debug.Log("Thả kéo vào trong");
                uiWaifu.gameObject.transform.SetParent(parentAfterDrag);
                uiWaifu.raycastTarget = true;
                Destroy(gameObject);
            }
            else 
            {
                Debug.Log("Thả kéo ra ngoài");
                Debug.Log(MapController.instance.posistionAfter);
                uiWaifu.gameObject.transform.DOMove(MapController.instance.posistionAfter - new Vector3(0f,0.7f,0f), 0.7f)
                .OnComplete(()=>{
                    Debug.Log("Đối tượng không được thả vào một InventorySlot hợp lệ, xóa đối tượng.");
                    Destroy(uiWaifu.gameObject);
                    shadow.gameObject.SetActive(false);
                });
                
            }
            
        }
        Vector3 MouseWorldPosittion()
        {
            var mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mouseScreenPos);
            
        }

        private void AddMoveHeroComponent()
        {
            MoveHero moveHero = uiWaifu.gameObject.AddComponent<MoveHero>();
            moveHero.UI_Waifu = uiWaifu;
            moveHero.parentTransform = MapController.instance.CreatedHeroObj.transform;

            RectTransform rectTransform = uiWaifu.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
        }
        private void AddWaifuComponent()
        {
            WaifuHero waifu = uiWaifu.gameObject.AddComponent<WaifuHero>();
            waifu.UI_Waifu = uiWaifu;

        }
        public void SetUp(PlayerOwnsWaifu waifu)
        {
            _waifu = waifu;
            InfoWaifuAsset infoWaifu = DataController.instance.GetInfoWaifuAssetsByIndex(waifu.ID);
            SkeletonDataAsset skeletonDataAsset = WaifuAssets.instance.GetWaifuSOByID(_waifu.ID.ToString()).SkeletonDataAsset;
            UI_Waifu.skeletonDataAsset = skeletonDataAsset;
            UI_Waifu.initialSkinName = "Pet" + infoWaifu.Code;
            UI_Waifu.startingAnimation = UI_Waifu.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;

            UI_Waifu.Initialize(true);
            if (avaBox != null)
            {
                switch (infoWaifu.Rare)
                {
                    case Rare.R:
                        SetRarityColors(1, Config.color_Rare_R, Config.color_BackGlow_Rare_R);
                        break;
                    case Rare.SR:
                        SetRarityColors(2, Config.color_Rare_SR, Config.color_BackGlow_Rare_SR);
                        break;
                    case Rare.SSR:
                        SetRarityColors(3, Config.color_Rare_SSR, Config.color_BackGlow_Rare_SSR);
                        break;
                    case Rare.UR:
                        SetRarityColors(4, Config.color_Rare_UR, Config.color_BackGlow_Rare_UR);
                        break;
                }
            }
            nameTxt.text = infoWaifu.Name.ToString();
            levelTxt.text = waifu.level.ToString();
            for (int i = 0; i < waifu.Star; i++)
            {
                stars[i].SetActive(true);
                // if (i < waifu.Ascend)
                // {
                //     stars[i].GetComponent<Image>().color = Color.red;
                // }
            }
            var btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    
                });
            }
        }
        private void SetRarityColors(int rarityIndex, string glowColor, string backGlowColor)
        {
            avaBox.sprite = AssetLoader.Instance.RarrityBox[rarityIndex];
            Config.SetColorFromHex(Glow.GetComponent<Image>(), glowColor);
            Config.SetColorFromHex(BackGlow.GetComponent<Image>(), backGlowColor);
        }
    }
}

