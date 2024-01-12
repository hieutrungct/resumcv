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
        public DataController listWaifu;
        public WaifuItem slot_Waifu;
        public Transform transformSlot;
        public WaiFuInfoPopUp waifuInfoPopup;
        public List<PlayerOwnsWaifu> Waifus;
        private List<PlayerOwnsWaifu> sortedWaifus;
        //list
        public TextMeshProUGUI textListSortLever, textListSortRarity, textListSortPower;
        private int a, b, c;

        private float count;
        private bool isFirstClick = true;

        public void Awake()
        {
            DontDestroyOnLoad(this);
            instance = this;
            CreateWaifu();
            Waifus = listWaifu.playerData.lsPlayerOwnsWaifu;
            sortedWaifus = new List<PlayerOwnsWaifu>(Waifus);
            SortRarityAndLevel();
        }
        

        void CreateWaifu()
        {
            foreach (Transform child in transformSlot)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < listWaifu.playerData.lsPlayerOwnsWaifu.Count; i++)
            {
                WaifuItem slotWaifu = Instantiate(slot_Waifu, transformSlot);
                //Debug.Log("Id của waifu " + listWaifu.playerData.lsPlayerOwnsWaifu[i].Index.ToString());
                
                slotWaifu.SetUp(listWaifu.playerData.lsPlayerOwnsWaifu[i]);
            }

        }


        private void SortRarityAndLevel()
        {
            Waifus.Sort((charA, charB) =>
            {

                int result = charA.level.CompareTo(charB.level);
                if (result == 0)
                {
                    return charA.IndexEvolution.CompareTo(charB.IndexEvolution);
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

                int result = charA.IndexEvolution.CompareTo(charB.IndexEvolution);
                if (result == 0)
                {
                    result = charA.level.CompareTo(charB.level);
                }
                return result;
            });
        }

        private void SortLevel()
        {
            Waifus.Sort((charA, charB) =>
            {
                int result = charA.level.CompareTo(charB.level);
                if (result == 0)
                {
                    return charA.IndexEvolution.CompareTo(charB.IndexEvolution);
                }
                return result;
            });
        }
        private void SortPower()
        {
            Waifus.Sort((charA, charB) =>
            {
                int result = (charA.Star).CompareTo(charB.Star);
                if (result == 0)
                {
                    result = charA.level.CompareTo(charB.level);
                    if (result == 0)
                    {
                        return charA.IndexEvolution.CompareTo(charB.IndexEvolution);
                    }
                }
                return result;
            });
        }

        public void ShowWaifuInfoPopup(PlayerOwnsWaifu Waifu)
        {
            waifuInfoPopup.ShowCharaterfoPopup(Waifu);
        }

        public int CheckIndexOfWaifu(PlayerOwnsWaifu Waifu)
        {

            return Waifus.IndexOf(Waifu);

        }
        public PlayerOwnsWaifu GetWaifu(int index)
        {
            if (index >= listWaifu.playerData.lsPlayerOwnsWaifu.Count)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = listWaifu.playerData.lsPlayerOwnsWaifu.Count - 1;
            }

            return Waifus[index];
        }

        //list
        public void OnSortButtonClickedLever()
        {

            SortChar(SortingType.Lever);
            SetButtonColors(SortingType.Lever);
            if (isFirstClick)
            {
                a = a + 2;
                isFirstClick = false;
            }
            else
            {
                a++;
            }
            b = c = 0;
            if (a % 2 == 0)
            {
                RefreshWaifuUIOpp();
            }
            else
            {
                RefreshWaifuUI();
            }

        }

        public void OnSortButtonClickedRarity()
        {
            SortChar(SortingType.Rarity);
            SetButtonColors(SortingType.Rarity);
            b++;
            if (b % 2 == 0)
            {
                RefreshWaifuUIOpp();
            }
            else
            {
                RefreshWaifuUI();
            }
            a = c = 0;


        }
        public void OnSortButtonClickedPower()
        {
            SortChar(SortingType.Power);
            SetButtonColors(SortingType.Power);
            c++;
            a = b = 0;
            if (c % 2 == 0)
            {
                RefreshWaifuUIOpp();
            }
            else
            {
                RefreshWaifuUI();
            }
        }
        private void SetButtonColors(SortingType selectedSortingType)
        {
            // Đặt màu cho nút đã chọn.
            textListSortLever.color = (selectedSortingType == SortingType.Lever) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortRarity.color = (selectedSortingType == SortingType.Rarity) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortPower.color = (selectedSortingType == SortingType.Power) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
        }
        public void OpenPopup()
        {
            MovePopup.TransPopupHorizontal(gbTaget, gbPopupOpen);
        }
        public void ClosePopup()
        {
            MovePopup.TransPopupHorizontal(gbPopupOpen, gbTaget);
        }
    }
}

