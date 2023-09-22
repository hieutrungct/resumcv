using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik_Casual
{
    public class CharaterUIController : MonoBehaviour
    {
        public static CharaterUIController instance;
        public CharacterInfo listCharacter;
        public CharacterItem slot_Character;
        public Transform transformSlot;
    
        public void Awake()
        {
            DontDestroyOnLoad(this);
            instance = this;
            CreateCharacter();
            Character[] characters = listCharacter.Characters;
        }

        void CreateCharacter()
    
        {
            for (int i = 0; i < listCharacter.Characters.Length; i++)
            {
            
                CharacterItem slotCharacter = Instantiate(slot_Character, transformSlot);
                slotCharacter.SetUp(listCharacter.Characters[i]);
            }
        
        }

        public void ShowCharacterInfoPopup(Character character)
        {
            
        }
    
    }
}

