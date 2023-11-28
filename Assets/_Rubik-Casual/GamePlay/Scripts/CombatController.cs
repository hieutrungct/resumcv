
using System.Collections.Generic;
using Rubik.Character_ACC;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.Combat
{
    public class CombatController : MonoBehaviour
    {
        public CharacterAssets characterAssets;
        public List<GameObject> lsSlotInChess;
        void Start()
        {
            characterAssets = CharacterAssets.instance;
        }
        [Button]
        void CreateCharacter()
        {

        }
    }

}
