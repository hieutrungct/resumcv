using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik_Casual
{
    [CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]


    public class CharacterInfo : ScriptableObject
    {
        public Character[] Characters;
    }

}
