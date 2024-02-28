using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.CreateSkill.Panel
{
    public class ChoseSlotMapPanel : MonoBehaviour
    {
        public TMP_InputField inputFieldRow;
        public TMP_Dropdown dropdownTypeMap;
        public static ChoseSlotMapPanel instance;
        void Awake()
        {
            instance = this;
            LoadPanel();
        }
        void LoadPanel()
        {
            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            foreach (TypeMap skill in System.Enum.GetValues(typeof(TypeMap)))
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(skill.ToString());
                optionDatas.Add(optionData);
            }

            dropdownTypeMap.AddOptions(optionDatas);
        }
    }
}
