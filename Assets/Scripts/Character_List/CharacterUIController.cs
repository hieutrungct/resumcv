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
        public void Awake()
        {
            DontDestroyOnLoad(this);
            instance = this;
            CreateCharacter();
            characters = listCharacter.Characters;
        }

        void CreateCharacter()
    
        {
            for (int i = 0; i < listCharacter.Characters.Count; i++)
            {
            
                CharacterItem slotCharacter = Instantiate(slot_Character, transformSlot);
                slotCharacter.SetUp(listCharacter.Characters[i]);
            }
        
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

