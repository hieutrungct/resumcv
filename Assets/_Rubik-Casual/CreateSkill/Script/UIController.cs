using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.CreateSkill.UI
{
    public class UIController : MonoBehaviour
    {
        public Canvas canvasCreateMap, canvasCreateSkill;
        public CharacterSetSkillController characterSetSkillController;
        public Toggle toggleChoice;
        public bool isCreateMap = false;

        void Awake()
        {
            ShowCreateMapOrShowCreateSkill();
        }
        [Button]
        public void ShowCreateMapOrShowCreateSkill()
        {
            isCreateMap = toggleChoice.isOn;
            if (isCreateMap)
            {
                canvasCreateMap.gameObject.SetActive(true);
                canvasCreateSkill.gameObject.SetActive(false);
                characterSetSkillController.gameObject.SetActive(false);
            }
            else
            {
                canvasCreateMap.gameObject.SetActive(false);
                canvasCreateSkill.gameObject.SetActive(true);
                characterSetSkillController.gameObject.SetActive(true);
            }
        }
    }
}