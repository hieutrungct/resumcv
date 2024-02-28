using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RubikCasual.CreateSkill.Panel
{
    public class InfoStagePanel : MonoBehaviour
    {
        public TMP_InputField NameStage, AttributeBeforeAttackBoss;
        public TextMeshProUGUI txtNumberTurn, txtTotalTurn;
        string originName;
        public static InfoStagePanel instance;

        void Awake()
        {
            instance = this;
            originName = AttributeBeforeAttackBoss.transform.parent.Find("TxtName").GetComponent<TextMeshProUGUI>().text;
        }
        void Update()
        {
            StatePanel((TypeMap)ChoseSlotMapPanel.instance.dropdownTypeMap.value);
        }
        void StatePanel(TypeMap typeMap)
        {
            switch (typeMap)
            {

                case TypeMap.Challenge_Map:
                    AttributeBeforeAttackBoss.transform.parent.gameObject.SetActive(false);
                    break;
                default:
                    if (typeMap == TypeMap.Default_Map)
                    {
                        AttributeBeforeAttackBoss.transform.parent.Find("TxtName").GetComponent<TextMeshProUGUI>().text = "Attribute Stage";
                    }
                    else
                    {
                        AttributeBeforeAttackBoss.transform.parent.Find("TxtName").GetComponent<TextMeshProUGUI>().text = originName;
                    }
                    AttributeBeforeAttackBoss.transform.parent.gameObject.SetActive(true);
                    break;
            }

        }
    }
}
