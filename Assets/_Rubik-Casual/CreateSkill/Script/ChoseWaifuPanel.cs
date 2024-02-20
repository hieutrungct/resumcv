using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.CreateSkill.Panel
{
    public class ChoseWaifuPanel : MonoBehaviour
    {
        public TMP_InputField inputFieldIndexId;
        public Toggle toggleIsSkin;
        public static ChoseWaifuPanel instance;
        void Awake()
        {
            instance = this;
        }
    }
}
