using System.Collections;
using System.Collections.Generic;
using RubikCasual.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.CreateSkill.Panel
{
    public class InfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI txtValueOldDame;
        public TMP_InputField inputFieldDame, inputFieldRow, inputFieldColumn, inputFieldNumberTurn,
        inputFieldDurationAtack, inputFieldDurationWave;
        public TMP_Dropdown dropDownSkill;
        public CharacterSetSkillController characterSetSkillController;
        public string OldTxtDame;

        public static InfoPanel instance;
        void Awake()
        {
            instance = this;
            StartCoroutine(Load());
        }
        void Start()
        {


        }
        public void SetValue(CharacterInBattle characterInBattle)
        {
            int index = 0;
            index = Data.DataController.instance.characterAssets.GetIndexWaifu(characterInBattle.waifuIdentify.ID, characterInBattle.waifuIdentify.SkinCheck);
            Data.Waifu.WaifuSkill waifuSkill = Data.DataController.instance.characterAssets.GetSkillWaifuSOByIndex(index);

            inputFieldDame.text = waifuSkill.percentDameSkill.ToString();
            inputFieldRow.text = waifuSkill.Row.ToString();
            inputFieldColumn.text = waifuSkill.Column.ToString();
            inputFieldNumberTurn.text = waifuSkill.NumberTurn.ToString();
            inputFieldDurationAtack.text = waifuSkill.DurationAttacked.ToString();
            inputFieldDurationWave.text = waifuSkill.DurationWave.ToString();
        }


        IEnumerator Load()
        {
            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            foreach (TypeSkill skill in System.Enum.GetValues(typeof(TypeSkill)))
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(skill.ToString());
                optionDatas.Add(optionData);
            }

            dropDownSkill.AddOptions(optionDatas);

            yield return new WaitForSeconds(0.5f);
            OldTxtDame = txtValueOldDame.text;
            txtValueOldDame.text = OldTxtDame + characterSetSkillController.transCharacter.GetComponent<CharacterInBattle>().infoWaifuAsset.Skill.ToString();
            dropDownSkill.onValueChanged.AddListener((int value) =>
            {
                switch ((TypeSkill)value)
                {
                    case TypeSkill.InTurn:

                        inputFieldNumberTurn.transform.parent.gameObject.SetActive(true);
                        inputFieldDurationWave.transform.parent.gameObject.SetActive(false);
                        break;
                    case TypeSkill.Wave:

                        inputFieldDurationWave.transform.parent.gameObject.SetActive(true);
                        inputFieldNumberTurn.transform.parent.gameObject.SetActive(false);
                        break;
                    case TypeSkill.InTurn2:

                        inputFieldDurationWave.transform.parent.gameObject.SetActive(true);
                        inputFieldNumberTurn.transform.parent.gameObject.SetActive(true);
                        break;
                    default:
                        inputFieldDurationWave.transform.parent.gameObject.SetActive(false);
                        inputFieldNumberTurn.transform.parent.gameObject.SetActive(false);
                        break;
                }

            });
            yield return new WaitForSeconds(0.5f);
        }

    }
}
