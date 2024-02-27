using System.Collections;
using System.Collections.Generic;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Tool;
using RubikCasual.Waifu;
using TMPro;
using UnityEngine;
namespace Rubik.ListWaifu
{
    public class WaifuController : MonoBehaviour
    {
        public GameObject gbTaget, gbPopupOpen;
        public static WaifuController instance;
        // public DataController listWaifu;
        public WaifuItem slot_Waifu;
        public Transform transformSlot;
        public WaiFuInfoPopUp waifuInfoPopup;
        public List<PlayerOwnsWaifu> Waifus;
        private List<PlayerOwnsWaifu> sortedWaifus;
        //list
        public TextMeshProUGUI textListSortLever, textListSortRarity, textListSortPower;
        public int a, b, c;

        private float count;
        private bool isFirstClick = true;

        public void Start()
        {
            // DontDestroyOnLoad(this);
            
            instance = this;
            CreateWaifu();
            Waifus = DataController.instance.playerData.lsPlayerOwnsWaifu;
            sortedWaifus = new List<PlayerOwnsWaifu>(Waifus);
            SortRarityAndLevel();
        }
        

        void CreateWaifu()
        {
            foreach (Transform child in transformSlot)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < DataController.instance.playerData.lsPlayerOwnsWaifu.Count; i++)
            {
                WaifuItem slotWaifu = Instantiate(slot_Waifu, transformSlot);

                // Debug.Log("Id của waifu " + DataController.instance.playerData.lsPlayerOwnsWaifu[i].ID.ToString());
                slotWaifu.SetUp(DataController.instance.playerData.lsPlayerOwnsWaifu[i]);
            }

        }


        private void SortRarityAndLevel()
        {
            
            Waifus.Sort((charA, charB) =>
            {
                InfoWaifuAsset infoWaifuA = DataController.instance.GetInfoWaifuAssetsByIndex(charA.ID);
                InfoWaifuAsset infoWaifuB = DataController.instance.GetInfoWaifuAssetsByIndex(charB.ID);
                int result = charA.level.CompareTo(charB.level);
                if (result == 0)
                {
                    return infoWaifuA.Rare.CompareTo(infoWaifuB.Rare);
                }
                return result;
            });

            RefreshWaifuUI();
        }

        public void RefreshWaifuUI()
        {

            foreach (Transform child in transformSlot)
            {
                Destroy(child.gameObject);
            }

            for (int i = Waifus.Count - 1; i > -1; i--)
            {
                WaifuItem slotWaifu = Instantiate(slot_Waifu, transformSlot);
                slotWaifu.SetUp(Waifus[i]);
            }
        }

        public void RefreshWaifuUIOpp()
        {
            Waifus.Reverse();
            foreach (Transform child in transformSlot)
            {
                Destroy(child.gameObject);
            }
            for (int i = Waifus.Count - 1; i > -1; i--)
            {
                WaifuItem slotWaifu = Instantiate(slot_Waifu, transformSlot);
                slotWaifu.SetUp(Waifus[i]);
            }
        }

        // aaa

        public void SortChar(SortingType typeSort)
        {
            switch (typeSort)
            {
                case SortingType.Rarity:
                    {
                        SortRarity();
                        break;
                    }
                case SortingType.Lever:
                    {
                        SortLevel();
                        break;
                    }
                case SortingType.Power:
                    SortPower();
                    break;
                default:
                    {
                        break;
                    }
            }
            RefreshWaifuUI();
        }

        private void SortRarity()
        {
            Waifus.Sort((charA, charB) =>
            {
                InfoWaifuAsset infoWaifuA = DataController.instance.GetInfoWaifuAssetsByIndex(charA.ID);
                InfoWaifuAsset infoWaifuB = DataController.instance.GetInfoWaifuAssetsByIndex(charB.ID);

                int result = infoWaifuB.Rare.CompareTo(infoWaifuA.Rare);
                if (result == 0)
                {
                    result = charB.level.CompareTo(charA.level);
                }
                return result;
            });
        }

        private void SortLevel()
        {
            Waifus.Sort((charA, charB) =>
            {
                InfoWaifuAsset infoWaifuA = DataController.instance.GetInfoWaifuAssetsByIndex(charA.ID);
                InfoWaifuAsset infoWaifuB = DataController.instance.GetInfoWaifuAssetsByIndex(charB.ID);

                int result = charA.level.CompareTo(charB.level);
                if (result == 0)
                {
                    return infoWaifuA.Rare.CompareTo(infoWaifuB.Rare);
                }
                return result;
            });
        }
        private void SortPower()
        {
            Waifus.Sort((charA, charB) =>
            {
                InfoWaifuAsset infoWaifuA = DataController.instance.GetInfoWaifuAssetsByIndex(charA.ID);
                InfoWaifuAsset infoWaifuB = DataController.instance.GetInfoWaifuAssetsByIndex(charB.ID);
                int result = (charA.Pow + charA.ATK).CompareTo(charB.Pow + charB.ATK);
                if (result == 0)
                {
                    result = charA.level.CompareTo(charB.level);
                    if (result == 0)
                    {
                        return infoWaifuA.Rare.CompareTo(infoWaifuB.Rare);
                    }
                }
                return result;
            });
        }

        public void ShowWaifuInfoPopup(PlayerOwnsWaifu Waifu)
        {
            waifuInfoPopup.ShowWaifufoPopup(Waifu);
        }

        public int CheckIndexOfWaifu(PlayerOwnsWaifu Waifu)
        {

            return Waifus.IndexOf(Waifu);

        }
        public PlayerOwnsWaifu GetWaifu(int index)
        {
            if (index >= DataController.instance.playerData.lsPlayerOwnsWaifu.Count)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = DataController.instance.playerData.lsPlayerOwnsWaifu.Count - 1;
            }

            return Waifus[index];
        }

        //list
        private bool isFirstClickLever = true;
        private bool isFirstClickRarity = true;
        private bool isFirstClickPower = true;

        public void OnSortButtonClickedLever()
        {
            isFirstClickLever = !isFirstClickLever;
            SortAndRefreshUI(SortingType.Lever, isFirstClickLever);
            isFirstClickRarity = true;
            isFirstClickPower = true;
        }

        public void OnSortButtonClickedRarity()
        {
            isFirstClickRarity = !isFirstClickRarity;
            SortAndRefreshUI(SortingType.Rarity, isFirstClickRarity);
            isFirstClickLever = false;
            isFirstClickPower = true;
        }

        public void OnSortButtonClickedPower()
        {
            isFirstClickPower = !isFirstClickPower;
            SortAndRefreshUI(SortingType.Power, isFirstClickPower);
            isFirstClickLever = false;
            isFirstClickRarity = true;
        }

        private void SortAndRefreshUI(SortingType sortType, bool isFirstClick)
        {
            SortChar(sortType);
            SetButtonColors(sortType);

            if (isFirstClick)
            {
                RefreshWaifuUI();
                // Debug.Log("Ngược lại");
            }
            else
            {
                RefreshWaifuUIOpp();
                // Debug.Log("Sắp lại");
            }
        }
        private void SetButtonColors(SortingType selectedSortingType)
        {
            Config.SetTextColorWithHex(textListSortLever, selectedSortingType == SortingType.Lever ? Config.color_White : Config.color_Blue);
            Config.SetTextColorWithHex(textListSortRarity, selectedSortingType == SortingType.Rarity ? Config.color_White : Config.color_Blue);
            Config.SetTextColorWithHex(textListSortPower, selectedSortingType == SortingType.Power ? Config.color_White : Config.color_Blue);
        }
        public void OpenPopup()
        {
            gbPopupOpen.SetActive(true);
            HUDController.instanse.UpdateTopPanel(Energe:false,Gold:false,Gem:false);
            CreateWaifu();
        }
        public void ClosePopup()
        {
            gbPopupOpen.SetActive(false);
            HUDController.instanse.UpdateTopPanel(Energe:true,Gold:true,Gem:true);
        }
    }
}

