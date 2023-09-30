using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik_Casual
{
    public class CharacterUIController : MonoBehaviour
    {
        public static CharacterUIController instance;
        public CharacterInfo listCharacter;
        public CharacterItem slot_Character;
        public Transform transformSlot;
        public CharacterInfoPopup characterInfoPopup;
        private List<Character> characters; 
        private List<Character> sortedCharacters;
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
            foreach (Transform child in transformSlot)
            {
                Destroy(child.gameObject);
            }
            
            for (int i = 0; i < characters.Count ; i++)
            {
                CharacterItem slotCharacter = Instantiate(slot_Character, transformSlot);
                slotCharacter.SetUp(characters[i]);
            }
        }
        
        // aaa
        public void SortChar(int typeSort)
        {
            switch (typeSort)
            {
                case 0:
                {
                    SortRarity();
                    break;
                }
                case 1:
                {
                    SortLevel();
                    break;
                }
                case 2:
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
                return result;
            });
        }
        
        private void SortLevel()
        {
            characters.Sort((charA, charB) =>
            {
                int result = charA.Level.CompareTo(charB.Level);
                return result;
            });
        }
        private void SortPower()
        {
            characters.Sort((charA, charB) =>
            {
                int result = (charA.Critical + charA.AttackDamage).CompareTo(charB.Critical + charB.AttackDamage);
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
            if (index >= listCharacter.Characters.Count || index < 0)
            {
                index = 0;
            }
            return characters[index];
        }
    }
}

