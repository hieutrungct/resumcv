using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rubik_Casual
{
    
    public class CharacterUIController : MonoBehaviour
    {
        public static CharacterUIController instance;
        public CharacterInfo listCharacter;
        public CharacterItem slot_Character;
        public Transform transformSlot;
        public CharacterInfoPopup characterInfoPopup;
        public List<Character> characters; 
        private List<Character> sortedCharacters;
        //list
        public TextMeshProUGUI textListSortLever, textListSortRarity, textListSortPower;
        private int a, b, c;
        
        private float count;
        private bool isFirstClick = true;
        
        public void Awake()
        {
            DontDestroyOnLoad(this);
            instance = this;
            CreateCharacter();
            characters = listCharacter.Characters;
            sortedCharacters = new List<Character>(characters);
            SortRarityAndLevel();
            
        }

        void CreateCharacter()
        {
            
            for (int i = 0; i < listCharacter.Characters.Count; i++)
            {
                CharacterItem slotCharacter = Instantiate(slot_Character, transformSlot);
                slotCharacter.SetUp(listCharacter.Characters[i]);
            }

        }
        
        
        private void SortRarityAndLevel()
        {
            characters.Sort((charA, charB) =>
            {
                //int result = charA.Rarity.CompareTo(charB.Rarity);
                //if (result == 0)
                //{
                //    result = charA.Level.CompareTo(charB.Level);
                //}
                int result = charA.Level.CompareTo(charB.Level);
                if (result == 0)
                {
                    return charA.Rarity.CompareTo(charB.Rarity);
                }
                return result;
            });
            
            RefreshCharacterUI();
        }

        public void RefreshCharacterUI()
        {
            
            foreach (Transform child in transformSlot)
            {
                Destroy(child.gameObject);
            }
            
            for (int i = characters.Count-1; i > -1 ; i--)
            {
                CharacterItem slotCharacter = Instantiate(slot_Character, transformSlot);
                slotCharacter.SetUp(characters[i]);
                
            }
        }
        
        public void RefreshCharacterUIOpp()
        {
            characters.Reverse();
            foreach (Transform child in transformSlot)
            {
                Destroy(child.gameObject);
            }
            for (int i = characters.Count-1; i > -1 ; i--)
            {
                CharacterItem slotCharacter = Instantiate(slot_Character, transformSlot);
                slotCharacter.SetUp(characters[i]);
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
            RefreshCharacterUI();
        }

        private void SortRarity()
        {
            characters.Sort((charA, charB) =>
            {
                
                int result = charA.Rarity.CompareTo(charB.Rarity);
                if (result == 0)
                {
                    result = charA.Level.CompareTo(charB.Level);
                }
                return result;
            });
        }
        
        private void SortLevel()
        {
            characters.Sort((charA, charB) =>
            {
                int result = charA.Level.CompareTo(charB.Level);
                if (result == 0)
                {
                    return charA.Rarity.CompareTo(charB.Rarity);
                }
                return result;
            });
        }
        private void SortPower()
        {
            characters.Sort((charA, charB) =>
            {
                int result = (charA.Critical + charA.AttackDamage).CompareTo(charB.Critical + charB.AttackDamage);
                if (result == 0)
                {
                    result = charA.Level.CompareTo(charB.Level);
                    if (result == 0)
                    {
                        return charA.Rarity.CompareTo(charB.Rarity);
                    }
                }
                return result;
            });
        }
        
        public void ShowCharacterInfoPopup(Character character)
        {
            characterInfoPopup.ShowCharaterfoPopup(character);
        }

        public int CheckIndexOfCharacter(Character character)
        {
          
            return characters.IndexOf(character);
            
        }
        public Character GetCharacter(int index)
        {
            if (index >= listCharacter.Characters.Count )
            {
                index = listCharacter.Characters.Count -1;
            }
            else if (index < 0)
            {
                 index = 0;
            }
            
            return characters[index];
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
            b=c = 0;
            if (a % 2 == 0)
            {
                RefreshCharacterUIOpp();
            }
            else 
            {
                RefreshCharacterUI();
            }
            
        }

        public void OnSortButtonClickedRarity()
        {
            SortChar(SortingType.Rarity);
            SetButtonColors(SortingType.Rarity);
            b++;
            if (b % 2 == 0)
            {
                RefreshCharacterUIOpp();
            }
            else 
            {
                RefreshCharacterUI();
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
                RefreshCharacterUIOpp();
            }
            else 
            {
                RefreshCharacterUI();
            }
        }
        private void SetButtonColors(SortingType selectedSortingType)
        {
            // Đặt màu cho nút đã chọn.
            textListSortLever.color = (selectedSortingType == SortingType.Lever) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortRarity.color = (selectedSortingType == SortingType.Rarity) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
            textListSortPower.color = (selectedSortingType == SortingType.Power) ? Color.white : new Color(0.29f, 0.67f, 0.97f, 1f);
        }
    }
}

