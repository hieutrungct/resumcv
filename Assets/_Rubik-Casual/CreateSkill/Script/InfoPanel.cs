using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle;
using TMPro;
using UnityEngine;

namespace RubikCasual.CreateSkill.InfoPanel
{
    public class InfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI txtDmg, txtValueOldDame;
        public TextMeshProUGUI txtRow, txtValueRow;
        public TextMeshProUGUI txtColumn, txtValueColumn;
        public CharacterSetSkillController characterSetSkillController;
        public static InfoPanel instance;
        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            StartCoroutine(Load());
        }
        IEnumerator Load()
        {
            yield return new WaitForSeconds(0.5f);
            txtValueOldDame.text = characterSetSkillController.transCharacter.GetComponent<CharacterInBattle>().infoWaifuAsset.Skill.ToString();
        }
    }
}
